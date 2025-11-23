using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Buffers;
using System.Collections;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected;

/// <summary>
/// A simple undirected adjacency graph with no isolated nodes that stores
/// a list of nodes of generic type along with the neighbor nodes for each
/// node. The graph is undirected and ignores duplicate edges, self-edges,
/// and isolated nodes. Any update to the graph might render node indices
/// invalid for external use.
/// </summary>
/// <typeparam name="TNode"></typeparam>
/// <param name="comparer"></param>
public class UndirectedAdjacencyGraph<TNode>(IEqualityComparer<TNode> comparer) :
    IReadOnlyList<TNode> where TNode : notnull
{
    private static ulong NodeIndicesToEdgeId(int nodeIndex1, int nodeIndex2)
    {
        Debug.Assert(
            nodeIndex1 >= 0 && nodeIndex2 >= 0 && nodeIndex1 != nodeIndex2
        );

        return nodeIndex1 < nodeIndex2
            ? (uint)nodeIndex1 | ((ulong)nodeIndex2 << 32)
            : (uint)nodeIndex2 | ((ulong)nodeIndex1 << 32);
    }

    private static (int, int) EdgeIdToNodeIndices(ulong edgeId)
    {
        var nodeIndex1 = (int)(edgeId & ((1UL << 32) - 1));
        var nodeIndex2 = (int)(edgeId >> 32);

        Debug.Assert(
            nodeIndex1 >= 0 && nodeIndex2 >= 0 && nodeIndex1 < nodeIndex2
        );

        return (nodeIndex1, nodeIndex2);
    }


    public static UndirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer)
    {
        return new UndirectedAdjacencyGraph<TNode>(comparer);
    }

    public static UndirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer, IEnumerable<(TNode, TNode)> edgeList)
    {
        var graph = new UndirectedAdjacencyGraph<TNode>(comparer);

        graph.AddEdges(edgeList);

        return graph;
    }

    public static UndirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer, params (TNode, TNode)[] edgeList)
    {
        var graph = new UndirectedAdjacencyGraph<TNode>(comparer);

        graph.AddEdges(edgeList);

        return graph;
    }

    public static UndirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer, IEnumerable<IPair<TNode>> edgeList)
    {
        var graph = new UndirectedAdjacencyGraph<TNode>(comparer);

        graph.AddEdges(edgeList);

        return graph;
    }


    private readonly Dictionary<TNode, int> _nodeToIndex = new Dictionary<TNode, int>(comparer);
    private readonly List<TNode> _indexToNode = [];
    private readonly List<List<int>> _indexToNodeNeighborIndices = [];
    private readonly HashSet<ulong> _edgeIDs = [];
    
    public IEqualityComparer<TNode> NodeComparer { get; } 
        = comparer ?? throw new ArgumentNullException(nameof(comparer));

    public int Count
        => _indexToNode.Count;

    public int NodeCount
        => _indexToNode.Count;

    public int EdgeCount
        => _edgeIDs.Count;

    public IEnumerable<TNode> Nodes
        => _indexToNode;
    
    public IEnumerable<TNode> LeafNodes
        => _indexToNodeNeighborIndices.Where(
            indexList => indexList.Count < 2
        ).Select((_, idx) => _indexToNode[idx]);
    
    public IEnumerable<int> LeafNodeIndices
        => _indexToNodeNeighborIndices.Where(
            indexList => indexList.Count < 2
        ).Select((_, idx) => idx);

    /// <summary>
    /// Enumerates all edges as unique (node1, node2) pairs.
    /// Each undirected edge appears once.
    /// </summary>
    public IEnumerable<(TNode, TNode)> Edges
    {
        get
        {
            foreach (var edgeId in _edgeIDs)
            {
                var (nodeIndex1, nodeIndex2) = EdgeIdToNodeIndices(edgeId);

                yield return (
                    _indexToNode[nodeIndex1],
                    _indexToNode[nodeIndex2]
                );
            }
        }
    }

    /// <summary>
    /// Return all unique leaf edges in the graph.
    /// A leaf edge is one where at least one endpoint has degree = 1.
    /// An isolated edge (two nodes connected only to each other) also counts as a leaf edge.
    /// Each edge is returned only once.
    /// </summary>
    public IEnumerable<(TNode, TNode)> LeafEdges
    {
        get
        {
            foreach (var edgeId in _edgeIDs)
            {
                var (i, j) = EdgeIdToNodeIndices(edgeId);

                var degI = _indexToNodeNeighborIndices[i].Count;
                var degJ = _indexToNodeNeighborIndices[j].Count;

                // If either endpoint is degree 1, this is a leaf edge
                if (degI == 1 || degJ == 1)
                    yield return (_indexToNode[i], _indexToNode[j]);
            }
        }
    }

    public TNode this[int index]
        => _indexToNode[index];


    private int GetOrAddNodeIndex(TNode node)
    {
        // Node already in graph
        if (_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return nodeIndex;

        // Add new node to graph
        nodeIndex = _indexToNode.Count;

        _indexToNode.Add(node);
        _indexToNodeNeighborIndices.Add([]);
        _nodeToIndex.Add(node, nodeIndex);

        return nodeIndex;
    }


    public void Clear()
    {
        _nodeToIndex.Clear();

        _indexToNode.Clear();
        _indexToNodeNeighborIndices.Clear();

        _edgeIDs.Clear();
    }

    public void EnsureCapacity(int nodeCount, int edgeCount)
    {
        if (nodeCount < 0) throw new ArgumentOutOfRangeException(nameof(nodeCount));
        if (edgeCount < 0) throw new ArgumentOutOfRangeException(nameof(edgeCount));

        _indexToNode.Capacity = Math.Max(_indexToNode.Capacity, nodeCount);
        _indexToNodeNeighborIndices.Capacity = Math.Max(_indexToNodeNeighborIndices.Capacity, nodeCount);
        _edgeIDs.EnsureCapacity(edgeCount);
    }


    public bool ContainsEdge(IPair<TNode> nodePair)
    {
        return ContainsEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool ContainsEdge((TNode, TNode) nodePair)
    {
        return ContainsEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool ContainsEdge(TNode node1, TNode node2)
    {
        if (!_nodeToIndex.TryGetValue(node1, out var nodeIndex1)) return false;
        if (!_nodeToIndex.TryGetValue(node2, out var nodeIndex2)) return false;

        return ContainsEdgeAt(nodeIndex1, nodeIndex2);
    }

    public bool ContainsEdgeAt(int nodeIndex1, int nodeIndex2)
    {
        //return _indexToNodeNeighborIndices[nodeIndex1].Contains(nodeIndex2);

        var edgeId = NodeIndicesToEdgeId(nodeIndex1, nodeIndex2);
        return _edgeIDs.Contains(edgeId);
    }


    public bool AddEdge(IPair<TNode> nodePair)
    {
        return AddEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool AddEdge((TNode, TNode) nodePair)
    {
        return AddEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool AddEdge(TNode node1, TNode node2)
    {
        if (NodeComparer.Equals(node1, node2))
            return false; // self-edge not allowed

        var nodeIndex1 = GetOrAddNodeIndex(node1);
        var nodeIndex2 = GetOrAddNodeIndex(node2);

        return AddEdgeAt(nodeIndex1, nodeIndex2);
    }

    public bool AddEdgeAt(int nodeIndex1, int nodeIndex2)
    {
        if (nodeIndex1 == nodeIndex2) return false;

        var edgeId = NodeIndicesToEdgeId(nodeIndex1, nodeIndex2);
        if (!_edgeIDs.Add(edgeId))
            return false;

        _indexToNodeNeighborIndices[nodeIndex1].AddDistinct(nodeIndex2);
        _indexToNodeNeighborIndices[nodeIndex2].AddDistinct(nodeIndex1);

        return true;
    }


    public void AddEdges(IEnumerable<IPair<TNode>> edgeList)
    {
        foreach (var nodePair in edgeList)
            AddEdge(nodePair.Item1, nodePair.Item2);
    }

    public void AddEdges(IEnumerable<(TNode, TNode)> edgeList)
    {
        foreach (var nodePair in edgeList)
            AddEdge(nodePair.Item1, nodePair.Item2);
    }

    public void AddEdges(params (TNode, TNode)[] edgeList)
    {
        foreach (var nodePair in edgeList)
            AddEdge(nodePair.Item1, nodePair.Item2);
    }

    public void AddEdgesAt(params (int, int)[] edgeList)
    {
        foreach (var (i, j) in edgeList)
        {
            if (i == j) continue;

            var edgeId = NodeIndicesToEdgeId(i, j);
            if (_edgeIDs.Add(edgeId))
            {
                _indexToNodeNeighborIndices[i].AddDistinct(j);
                _indexToNodeNeighborIndices[j].AddDistinct(i);
            }
        }
    }

    public void AddEdgesAt(IEnumerable<(int, int)> edgeList)
    {
        foreach (var (i, j) in edgeList)
        {
            if (i == j) continue;

            var edgeId = NodeIndicesToEdgeId(i, j);
            if (_edgeIDs.Add(edgeId))
            {
                _indexToNodeNeighborIndices[i].AddDistinct(j);
                _indexToNodeNeighborIndices[j].AddDistinct(i);
            }
        }
    }


    public bool RemoveEdge(IPair<TNode> nodePair)
    {
        return RemoveEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool RemoveEdge((TNode, TNode) nodePair)
    {
        return RemoveEdge(nodePair.Item1, nodePair.Item2);
    }

    public bool RemoveEdge(TNode node1, TNode node2)
    {
        if (!_nodeToIndex.TryGetValue(node1, out var nodeIndex1)) return false;
        if (!_nodeToIndex.TryGetValue(node2, out var nodeIndex2)) return false;

        return RemoveEdgeAt(nodeIndex1, nodeIndex2);
    }

    public bool RemoveEdgeAt(int nodeIndex1, int nodeIndex2)
    {
        var edgeId = NodeIndicesToEdgeId(nodeIndex1, nodeIndex2);
        if (!_edgeIDs.Remove(edgeId))
            return false;

        _indexToNodeNeighborIndices[nodeIndex1].RemoveBySwap(nodeIndex2);
        _indexToNodeNeighborIndices[nodeIndex2].RemoveBySwap(nodeIndex1);

        // Clean up isolated nodes
        if (_indexToNodeNeighborIndices[nodeIndex1].Count == 0) RemoveNodeAt(nodeIndex1);
        if (_indexToNodeNeighborIndices[nodeIndex2].Count == 0) RemoveNodeAt(nodeIndex2);

        return true;
    }

    public void RemoveLeafEdges(Func<TNode, TNode, bool> filterFunc)
    {
        var nodePairs =
            LeafEdges.Where(nodePair =>
                filterFunc(nodePair.Item1, nodePair.Item2)
            ).ToArray();

        foreach (var nodePair in nodePairs)
            RemoveEdge(nodePair);
    }


    public bool ContainsNode(TNode node)
    {
        return _nodeToIndex.ContainsKey(node);
    }

    
    public bool RemoveNode(TNode node)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return false;

        // Remove all edges incident to this node
        var neighborIndices =
            _indexToNodeNeighborIndices[nodeIndex].ToArray();

        foreach (var neighbor in neighborIndices)
            _indexToNodeNeighborIndices[neighbor].RemoveBySwap(nodeIndex);

        RemoveNodeAt(nodeIndex);
        return true;
    }

    public void RemoveNodeAt(int index)
    {
        if (index < 0 || index >= _indexToNode.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        // Remove node and related edges
        var node = _indexToNode[index];

        // 1) Remove all edgeIDs incident to this node and remove neighbor references
        var neighborsToRemove = _indexToNodeNeighborIndices[index].ToArray(); // snapshot
        foreach (var neighbor in neighborsToRemove)
        {
            // Remove the neighbor reference to this node
            _indexToNodeNeighborIndices[neighbor].RemoveBySwap(index);

            // Remove the edge ID for (index, neighbor)
            var removedEdgeId = NodeIndicesToEdgeId(index, neighbor);
            _edgeIDs.Remove(removedEdgeId);
        }

        // Now remove the node mapping
        _nodeToIndex.Remove(node);

        var lastIdx = _indexToNode.Count - 1;
        if (index != lastIdx)
        {
            // Move last node into this slot
            var lastNode = _indexToNode[lastIdx];
            var lastNodeNeighbors = _indexToNodeNeighborIndices[lastIdx];

            // Place last node data into index slot
            _indexToNode[index] = lastNode;
            _indexToNodeNeighborIndices[index] = lastNodeNeighbors;

            // Update dictionary mapping for the moved node
            _nodeToIndex[lastNode] = index;

            // For every neighbor of the moved node, update their neighbor sets and update edge IDs:
            var neighborsOfMoved = lastNodeNeighbors.ToArray(); // snapshot
            foreach (var neighbor in neighborsOfMoved)
            {
                // Update neighbor sets: replace lastIdx with index
                var neighborSet = _indexToNodeNeighborIndices[neighbor];

                // remove old ref
                neighborSet.RemoveBySwap(lastIdx);

                // add new ref
                neighborSet.AddDistinct(index);

                // Update _edgeIDs: remove old entry (lastIdx, neighbor) and add new (index, neighbor)
                var oldEdgeId = NodeIndicesToEdgeId(lastIdx, neighbor);

                // Only remove existing; some edges may have been removed earlier
                _edgeIDs.Remove(oldEdgeId);

                var newEdgeId = NodeIndicesToEdgeId(index, neighbor);
                _edgeIDs.Add(newEdgeId);
            }
        }

        // Remove last node storage (either duplicated slot or the moved-from last)
        _indexToNode.RemoveAt(lastIdx);
        _indexToNodeNeighborIndices.RemoveAt(lastIdx);
    }


    public int GetNodeIndex(TNode node)
    {
        return _nodeToIndex[node];
    }

    public int GetNodeDegree(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIndex)
            ? _indexToNodeNeighborIndices[nodeIndex].Count : 0;
    }

    public int GetNodeDegreeAt(int nodeIndex)
    {
        return _indexToNodeNeighborIndices[nodeIndex].Count;
    }
    
    public IReadOnlyList<int> GetNodeNeighborIndices(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIdx)
            ? _indexToNodeNeighborIndices[nodeIdx]
            : [];
    }

    public IReadOnlyList<int> GetNodeNeighborIndicesAt(int nodeIndex)
    {
        return _indexToNodeNeighborIndices[nodeIndex];
    }
    
    public IEnumerable<TNode> GetNodeNeighbors(TNode node)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return [];

        return _indexToNodeNeighborIndices[nodeIndex]
            .Select(
                i => _indexToNode[i]
            );
    }

    public IEnumerable<TNode> GetNodeNeighborsAt(int nodeIndex)
    {
        return _indexToNodeNeighborIndices[nodeIndex]
            .Select(
                i => _indexToNode[i]
            );
    }
    
    public IEnumerable<(TWeight, TNode)> GetNodeWeightedNeighbors<TWeight>(TNode node, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return [];

        return _indexToNodeNeighborIndices[nodeIndex]
            .Select(
                i => (edgeWeightFunc(node, _indexToNode[i]), _indexToNode[i])
            );
    }
    
    public IEnumerable<(TWeight, TNode)> GetNodeWeightedNeighborsAt<TWeight>(int nodeIndex, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        var node = _indexToNode[nodeIndex];

        return _indexToNodeNeighborIndices[nodeIndex]
            .Select(
                i => (edgeWeightFunc(node, _indexToNode[i]), _indexToNode[i])
            );
    }
    
    public TNode GetNearestNode(TNode node, Func<TNode, TNode, double> distanceFunc)
    {
        var nodeIndex = GetNearestNodeIndex(node, distanceFunc);

        return _indexToNode[nodeIndex];
    }
    
    public int GetNearestNodeIndex(TNode node, Func<TNode, TNode, double> distanceFunc)
    {
        var nodeIndex = -1;
        var minDistance = double.PositiveInfinity;

        for (var i = 0; i < _indexToNode.Count; i++)
        {
            var distance = distanceFunc(node, _indexToNode[i]);

            if (minDistance <= distance) continue;

            nodeIndex = i;
            minDistance = distance;
        }

        return nodeIndex;
    }

    
    public bool TryGetNodeIndex(TNode node, out int index)
    {
        return _nodeToIndex.TryGetValue(node, out index);
    }

    public bool TryGetNodeNeighbors(TNode node, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeNeighborsAt(nodeIndex).ToArray();
        return true;
    }

    public bool TryGetNodeNeighborsAt(int nodeIndex, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (nodeIndex < 0 || nodeIndex >= _indexToNode.Count)
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeNeighborsAt(nodeIndex).ToArray();
        return true;
    }

    
    public bool TryRemoveNode(TNode node, out IReadOnlyList<TNode> removedNeighbors)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
        {
            removedNeighbors = [];
            return false;
        }

        removedNeighbors = GetNodeNeighborsAt(nodeIndex).ToArray();

        RemoveNodeAt(nodeIndex);
        return true;
    }

    public bool TryRemoveNodeAt(int nodeIndex, out IReadOnlyList<TNode> removedNeighbors)
    {
        if (nodeIndex < 0 || nodeIndex >= _indexToNode.Count)
        {
            removedNeighbors = [];
            return false;
        }

        removedNeighbors = GetNodeNeighborsAt(nodeIndex).ToArray();

        RemoveNodeAt(nodeIndex);
        return true;
    }


    /// <summary>
    /// Use the adjacency graph of a set of edges to divide the graph into
    /// a list of edge-chains. Each edge-chain is a connected set of edges
    /// where each of the two end nodes of the chian is either a leaf
    /// (has one neighbor) or a junction (has more than two neighbors).
    /// Aside from the two end nodes, any other node inside an edge chain
    /// has exactly two neighbors.
    /// </summary>
    /// <returns></returns>
    public IReadOnlyList<List<TNode>> GetEdgeChains()
    {
        var chains = new List<List<TNode>>(EdgeCount);
        var visitedEdges = new HashSet<ulong>(EdgeCount);
        var n = _indexToNode.Count;

        // ---- Step 1: chains from leaves/junctions ----
        var endNodes = Enumerable.Range(0, n)
            .Where(i =>
            {
                var deg = _indexToNodeNeighborIndices[i].Count;
                return deg != 2 && deg > 0; // exclude isolated nodes
            }).ToArray();

        foreach (var startIndex in endNodes)
        {
            foreach (var neighborIndex in _indexToNodeNeighborIndices[startIndex])
            {
                var edgeId = NodeIndicesToEdgeId(startIndex, neighborIndex);
                if (!visitedEdges.Add(edgeId)) continue;

                var buffer = ArrayPool<int>.Shared.Rent(n);
                var len = 0;
                buffer[len++] = startIndex;
                buffer[len++] = neighborIndex;

                int prevIndex = startIndex, currIndex = neighborIndex;

                while (_indexToNodeNeighborIndices[currIndex].Count == 2)
                {
                    var neighbors = _indexToNodeNeighborIndices[currIndex];
                    var next = neighbors[0] == prevIndex ? neighbors[1] : neighbors[0];

                    var nextEdge = NodeIndicesToEdgeId(currIndex, next);
                    if (!visitedEdges.Add(nextEdge)) break;

                    buffer[len++] = next;
                    prevIndex = currIndex;
                    currIndex = next;
                }

                chains.Add(ToNodeList(buffer, len));
                ArrayPool<int>.Shared.Return(buffer);
            }
        }

        // ---- Step 2: cycles ----
        foreach (var edgeId in _edgeIDs)
        {
            if (!visitedEdges.Add(edgeId)) continue;
            var (nodeIndex, neighborIndex) = EdgeIdToNodeIndices(edgeId);

            var buffer = ArrayPool<int>.Shared.Rent(n);
            var len = 0;
            buffer[len++] = nodeIndex;
            buffer[len++] = neighborIndex;

            int prevIndex = nodeIndex, currIndex = neighborIndex;

            while (true)
            {
                var neighbors = _indexToNodeNeighborIndices[currIndex];
                var next = neighbors[0] == prevIndex ? neighbors[1] : neighbors[0];

                var nextEdge = NodeIndicesToEdgeId(currIndex, next);
                if (!visitedEdges.Add(nextEdge)) break;
                if (next == nodeIndex) break; // closed cycle

                buffer[len++] = next;
                prevIndex = currIndex;
                currIndex = next;
            }

            chains.Add(ToNodeList(buffer, len));
            ArrayPool<int>.Shared.Return(buffer);
        }

        return chains;

        // helper: convert buffer to a List<TNode>
        List<TNode> ToNodeList(int[] buffer, int length)
        {
            var result = new List<TNode>(length);
            for (var k = 0; k < length; k++)
                result.Add(_indexToNode[buffer[k]]);
            return result;
        }
    }


    public UndirectedAStarPathFinder<TNode> GetAStarPathFinder(Func<TNode, TNode, double> edgeWeightFunc, Func<TNode, TNode, double> heuristic)
    {
        return new UndirectedAStarPathFinder<TNode>(
            this,
            edgeWeightFunc,
            heuristic
        );
    }

    public UndirectedAStarWithLandmarksPathFinder<TNode> GetAStarWithLandmarksPathFinder(Func<TNode, TNode, double> edgeWeightFunc, int landmarkCount)
    {
        var pathFinder = new UndirectedAStarWithLandmarksPathFinder<TNode>(
            this,
            edgeWeightFunc
        );

        pathFinder.Preprocess(landmarkCount);

        return pathFinder;
    }

    public UndirectedBidirectionalAStarPathFinder<TNode> GetBidirectionalAStarPathFinder(Func<TNode, TNode, double> edgeWeightFunc, Func<TNode, TNode, double> heuristic)
    {
        return new UndirectedBidirectionalAStarPathFinder<TNode>(
            this,
            edgeWeightFunc,
            heuristic
        );
    }

    public UndirectedBidirectionalDijkstraPathFinder<TNode> GetBidirectionalDijkstraPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        return new UndirectedBidirectionalDijkstraPathFinder<TNode>(
            this,
            edgeWeightFunc
        );
    }

    public UndirectedBreadthFirstPathFinder<TNode> GetBreadthFirstPathFinder()
    {
        return new UndirectedBreadthFirstPathFinder<TNode>(this);
    }

    public UndirectedContractionHierarchiesPathFinder<TNode> GetContractionHierarchiesPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        var pathFinder = new UndirectedContractionHierarchiesPathFinder<TNode>(
            this,
            edgeWeightFunc
        );

        pathFinder.Preprocess();

        return pathFinder;
    }

    public UndirectedDijkstraPathFinder<TNode> GetDijkstraPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        return new UndirectedDijkstraPathFinder<TNode>(
            this, 
            edgeWeightFunc
        );
    }

    public UndirectedHubLabelingPathFinder<TNode> GetHubLabelingPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        var pathFinder = new UndirectedHubLabelingPathFinder<TNode>(
            this, 
            edgeWeightFunc
        );

        pathFinder.Preprocess();

        return pathFinder;
    }


    public IEnumerator<TNode> GetEnumerator()
    {
        return _indexToNode.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}