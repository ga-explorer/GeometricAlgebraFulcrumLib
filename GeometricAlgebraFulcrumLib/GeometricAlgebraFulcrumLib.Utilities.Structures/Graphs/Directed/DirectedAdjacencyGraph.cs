using GeometricAlgebraFulcrumLib.Utilities.Structures.Extensions;
using System.Buffers;
using System.Collections;
using System.Diagnostics;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed;

/// <summary>
/// A simple directed adjacency graph.
/// - No self-loops
/// - No duplicate edges
/// - No isolated nodes (removes them automatically)
/// </summary>
/// <typeparam name="TNode"></typeparam>
public class DirectedAdjacencyGraph<TNode>(IEqualityComparer<TNode> comparer) :
    IReadOnlyList<TNode> where TNode : notnull
{
    private static ulong NodeIndicesToEdgeId(int sourceIndex, int targetIndex)
    {
        Debug.Assert(
            sourceIndex >= 0 && targetIndex >= 0 && sourceIndex != targetIndex
        );

        return (uint)sourceIndex | (ulong)targetIndex << 32;
    }

    private static (int, int) EdgeIdToNodeIndices(ulong edgeId)
    {
        var sourceIndex = (int)(edgeId & (1UL << 32) - 1);
        var targetIndex = (int)(edgeId >> 32);

        Debug.Assert(sourceIndex >= 0 && targetIndex >= 0);

        return (sourceIndex, targetIndex);
    }


    public static DirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer)
        => new(comparer);

    public static DirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer, IEnumerable<(TNode, TNode)> edgeList)
    {
        var graph = new DirectedAdjacencyGraph<TNode>(comparer);
        graph.AddEdges(edgeList);
        return graph;
    }

    public static DirectedAdjacencyGraph<TNode> Create(IEqualityComparer<TNode> comparer, params (TNode, TNode)[] edgeList)
    {
        var graph = new DirectedAdjacencyGraph<TNode>(comparer);
        graph.AddEdges(edgeList);
        return graph;
    }


    private readonly Dictionary<TNode, int> _nodeToIndex = new Dictionary<TNode, int>(comparer);
    private readonly List<TNode> _indexToNode = [];
    private readonly List<List<int>> _indexToNodeOutNeighborIndices = []; // outgoing neighbors
    private readonly List<List<int>> _indexToNodeInNeighborIndices = [];  // incoming neighbors
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
    
    /// <summary>
    /// Return all leaf nodes in the directed graph.
    /// A leaf node has exactly one incident edge total (in-degree + out-degree = 1).
    /// </summary>
    public IEnumerable<TNode> LeafNodes
    {
        get
        {
            for (var nodeIndex = 0; nodeIndex < _indexToNode.Count; nodeIndex++)
            {
                var degree = _indexToNodeInNeighborIndices[nodeIndex].Count + _indexToNodeOutNeighborIndices[nodeIndex].Count;
                
                if (degree == 1)
                    yield return _indexToNode[nodeIndex];
            }
        }
    }
    
    /// <summary>
    /// Return all leaf node indices in the directed graph.
    /// A leaf node has exactly one incident edge total (in-degree + out-degree = 1).
    /// </summary>
    public IEnumerable<int> LeafNodeIndices
    {
        get
        {
            for (var nodeIndex = 0; nodeIndex < _indexToNode.Count; nodeIndex++)
            {
                var degree = _indexToNodeInNeighborIndices[nodeIndex].Count + _indexToNodeOutNeighborIndices[nodeIndex].Count;
                
                if (degree == 1)
                    yield return nodeIndex;
            }
        }
    }

    public IEnumerable<(TNode, TNode)> Edges
    {
        get
        {
            foreach (var edgeId in _edgeIDs)
            {
                var (src, dst) = EdgeIdToNodeIndices(edgeId);
                yield return (_indexToNode[src], _indexToNode[dst]);
            }
        }
    }

    /// <summary>
    /// Return all directed leaf edges.
    /// A directed edge (u→v) is a leaf edge if either u or v is a leaf node
    /// (in-degree + out-degree = 1).
    /// </summary>
    public IEnumerable<(TNode, TNode)> LeafEdges
    {
        get
        {
            foreach (var edgeId in _edgeIDs)
            {
                var (srcIndex, dstIndex) = EdgeIdToNodeIndices(edgeId);

                var degSrc = _indexToNodeInNeighborIndices[srcIndex].Count + _indexToNodeOutNeighborIndices[srcIndex].Count;
                var degDst = _indexToNodeInNeighborIndices[dstIndex].Count + _indexToNodeOutNeighborIndices[dstIndex].Count;

                if (degSrc == 1 || degDst == 1)
                    yield return (_indexToNode[srcIndex], _indexToNode[dstIndex]);
            }
        }
    }

    public TNode this[int index] 
        => _indexToNode[index];


    private int GetOrAddNodeIndex(TNode node)
    {
        if (_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return nodeIndex;

        nodeIndex = _indexToNode.Count;
        _indexToNode.Add(node);
        _indexToNodeOutNeighborIndices.Add([]);
        _indexToNodeInNeighborIndices.Add([]);
        _nodeToIndex[node] = nodeIndex;

        return nodeIndex;
    }

    public void Clear()
    {
        _nodeToIndex.Clear();
        _indexToNode.Clear();
        _indexToNodeOutNeighborIndices.Clear();
        _indexToNodeInNeighborIndices.Clear();
        _edgeIDs.Clear();
    }


    public bool ContainsEdge(TNode source, TNode target)
    {
        if (!_nodeToIndex.TryGetValue(source, out var i)) return false;
        if (!_nodeToIndex.TryGetValue(target, out var j)) return false;

        var edgeId = NodeIndicesToEdgeId(i, j);
        return _edgeIDs.Contains(edgeId);
    }

    public bool AddEdge(TNode source, TNode target)
    {
        if (NodeComparer.Equals(source, target))
            return false; // no self-loop

        var srcIndex = GetOrAddNodeIndex(source);
        var dstIndex = GetOrAddNodeIndex(target);

        var edgeId = NodeIndicesToEdgeId(srcIndex, dstIndex);
        if (!_edgeIDs.Add(edgeId))
            return false; // duplicate

        _indexToNodeOutNeighborIndices[srcIndex].AddDistinct(dstIndex);
        _indexToNodeInNeighborIndices[dstIndex].AddDistinct(srcIndex);

        return true;
    }

    public void AddEdges(IEnumerable<(TNode, TNode)> edgeList)
    {
        foreach (var (src, dst) in edgeList)
            AddEdge(src, dst);
    }


    public bool RemoveEdge(TNode source, TNode target)
    {
        if (!_nodeToIndex.TryGetValue(source, out var srcIndex)) return false;
        if (!_nodeToIndex.TryGetValue(target, out var dstIndex)) return false;

        var edgeId = NodeIndicesToEdgeId(srcIndex, dstIndex);
        if (!_edgeIDs.Remove(edgeId))
            return false;

        _indexToNodeOutNeighborIndices[srcIndex].RemoveBySwap(dstIndex);
        _indexToNodeInNeighborIndices[dstIndex].RemoveBySwap(srcIndex);

        // clean isolated nodes
        if (_indexToNodeOutNeighborIndices[srcIndex].Count == 0 && _indexToNodeInNeighborIndices[srcIndex].Count == 0)
            RemoveNodeAt(srcIndex);

        if (_indexToNodeOutNeighborIndices[dstIndex].Count == 0 && _indexToNodeInNeighborIndices[dstIndex].Count == 0)
            RemoveNodeAt(dstIndex);

        return true;
    }
    
    
    public bool RemoveNode(TNode node)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return false;

        RemoveNodeAt(nodeIndex);
        return true;
    }

    public void RemoveNodeAt(int index)
    {
        if (index < 0 || index >= _indexToNode.Count)
            throw new ArgumentOutOfRangeException(nameof(index));

        var node = _indexToNode[index];

        // remove all outgoing edges
        foreach (var dst in _indexToNodeOutNeighborIndices[index].ToArray())
        {
            _indexToNodeInNeighborIndices[dst].RemoveBySwap(index);
            _edgeIDs.Remove(NodeIndicesToEdgeId(index, dst));
        }

        // remove all incoming edges
        foreach (var src in _indexToNodeInNeighborIndices[index].ToArray())
        {
            _indexToNodeOutNeighborIndices[src].RemoveBySwap(index);
            _edgeIDs.Remove(NodeIndicesToEdgeId(src, index));
        }

        // remove node mapping
        _nodeToIndex.Remove(node);

        var lastIdx = _indexToNode.Count - 1;
        if (index != lastIdx)
        {
            var lastNode = _indexToNode[lastIdx];
            _indexToNode[index] = lastNode;
            _indexToNodeOutNeighborIndices[index] = _indexToNodeOutNeighborIndices[lastIdx];
            _indexToNodeInNeighborIndices[index] = _indexToNodeInNeighborIndices[lastIdx];
            _nodeToIndex[lastNode] = index;

            // update neighbors of moved node
            foreach (var dst in _indexToNodeOutNeighborIndices[index].ToArray())
            {
                _indexToNodeInNeighborIndices[dst].RemoveBySwap(lastIdx);
                _indexToNodeInNeighborIndices[dst].AddDistinct(index);

                _edgeIDs.Remove(NodeIndicesToEdgeId(lastIdx, dst));
                _edgeIDs.Add(NodeIndicesToEdgeId(index, dst));
            }

            foreach (var src in _indexToNodeInNeighborIndices[index].ToArray())
            {
                _indexToNodeOutNeighborIndices[src].RemoveBySwap(lastIdx);
                _indexToNodeOutNeighborIndices[src].AddDistinct(index);

                _edgeIDs.Remove(NodeIndicesToEdgeId(src, lastIdx));
                _edgeIDs.Add(NodeIndicesToEdgeId(src, index));
            }
        }

        _indexToNode.RemoveAt(lastIdx);
        _indexToNodeOutNeighborIndices.RemoveAt(lastIdx);
        _indexToNodeInNeighborIndices.RemoveAt(lastIdx);
    }


    public int GetNodeOutDegree(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIndex)
            ? _indexToNodeOutNeighborIndices[nodeIndex].Count
            : 0;
    }

    public int GetNodeInDegree(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIndex)
            ? _indexToNodeInNeighborIndices[nodeIndex].Count
            : 0;
    }


    public IEnumerable<TNode> GetNodeOutNeighbors(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIndex) 
            ? _indexToNodeOutNeighborIndices[nodeIndex].Select(i => _indexToNode[i]) 
            : [];
    }

    public IEnumerable<TNode> GetNodeInNeighbors(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIndex) 
            ? _indexToNodeInNeighborIndices[nodeIndex].Select(i => _indexToNode[i]) 
            : [];
    }
    
    public IEnumerable<TNode> GetNodeOutNeighborsAt(int nodeIndex)
    {
        return _indexToNodeOutNeighborIndices[nodeIndex]
            .Select(
                i => _indexToNode[i]
            );
    }
    
    public IEnumerable<TNode> GetNodeInNeighborsAt(int nodeIndex)
    {
        return _indexToNodeInNeighborIndices[nodeIndex]
            .Select(
                i => _indexToNode[i]
            );
    }

    public IReadOnlyList<int> GetNodeOutNeighborIndices(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIdx)
            ? _indexToNodeOutNeighborIndices[nodeIdx]
            : [];
    }
    
    public IReadOnlyList<int> GetNodeInNeighborIndices(TNode node)
    {
        return _nodeToIndex.TryGetValue(node, out var nodeIdx)
            ? _indexToNodeInNeighborIndices[nodeIdx]
            : [];
    }

    public IReadOnlyList<int> GetNodeOutNeighborIndicesAt(int nodeIndex)
    {
        return _indexToNodeOutNeighborIndices[nodeIndex];
    }
    
    public IReadOnlyList<int> GetNodeInNeighborIndicesAt(int nodeIndex)
    {
        return _indexToNodeInNeighborIndices[nodeIndex];
    }

    public IEnumerable<(TWeight, TNode)> GetNodeWeightedInNeighbors<TWeight>(TNode node, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return [];

        return _indexToNodeInNeighborIndices[nodeIndex]
            .Select(
                i => (edgeWeightFunc(node, _indexToNode[i]), _indexToNode[i])
            );
    }
    
    public IEnumerable<(TWeight, TNode)> GetNodeWeightedOutNeighbors<TWeight>(TNode node, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
            return [];

        return _indexToNodeOutNeighborIndices[nodeIndex]
            .Select(
                i => (edgeWeightFunc(node, _indexToNode[i]), _indexToNode[i])
            );
    }

    public IEnumerable<(TWeight, TNode)> GetNodeWeightedInNeighborsAt<TWeight>(int nodeIndex, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        var node = _indexToNode[nodeIndex];

        return _indexToNodeInNeighborIndices[nodeIndex]
            .Select(
                i => (edgeWeightFunc(node, _indexToNode[i]), _indexToNode[i])
            );
    }
    
    public IEnumerable<(TWeight, TNode)> GetNodeWeightedOutNeighborsAt<TWeight>(int nodeIndex, Func<TNode, TNode, TWeight> edgeWeightFunc)
    {
        var node = _indexToNode[nodeIndex];

        return _indexToNodeOutNeighborIndices[nodeIndex]
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
    
    public bool TryGetNodeOutNeighbors(TNode node, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeOutNeighborsAt(nodeIndex).ToArray();
        return true;
    }
    
    public bool TryGetNodeInNeighbors(TNode node, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (!_nodeToIndex.TryGetValue(node, out var nodeIndex))
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeInNeighborsAt(nodeIndex).ToArray();
        return true;
    }

    public bool TryGetNodeOutNeighborsAt(int nodeIndex, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (nodeIndex < 0 || nodeIndex >= _indexToNode.Count)
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeOutNeighborsAt(nodeIndex).ToArray();
        return true;
    }
    
    public bool TryGetNodeInNeighborsAt(int nodeIndex, out IReadOnlyList<TNode> nodeNeighbors)
    {
        if (nodeIndex < 0 || nodeIndex >= _indexToNode.Count)
        {
            nodeNeighbors = [];
            return false;
        }

        nodeNeighbors = GetNodeInNeighborsAt(nodeIndex).ToArray();
        return true;
    }


    public IReadOnlyList<List<TNode>> GetEdgeChains()
    {
        var chains = new List<List<TNode>>(EdgeCount);
        var visitedEdges = new HashSet<ulong>(EdgeCount);

        var n = _indexToNode.Count;

        // Local function to convert an index chain into a List<TNode>
        static List<TNode> ConvertToNodes(
            int[] buffer, int length, List<TNode> indexToNode)
        {
            var result = new List<TNode>(length);
            for (var k = 0; k < length; k++)
                result.Add(indexToNode[buffer[k]]);
            return result;
        }

        // ---- Step 1: Paths starting from end nodes ----
        for (var i = 0; i < n; i++)
        {
            var inDegree = _indexToNodeInNeighborIndices[i].Count;
            var outDegree = _indexToNodeOutNeighborIndices[i].Count;

            if (inDegree == 0 && outDegree == 0) continue; // isolated
            if (inDegree == 1 && outDegree == 1) continue; // internal node, skip

            foreach (var next in _indexToNodeOutNeighborIndices[i])
            {
                var edgeId = NodeIndicesToEdgeId(i, next);
                if (!visitedEdges.Add(edgeId)) continue;

                var buffer = ArrayPool<int>.Shared.Rent(_indexToNode.Count);
                var len = 0;

                buffer[len++] = i;
                buffer[len++] = next;

                var currIndex = next;

                while (_indexToNodeOutNeighborIndices[currIndex].Count == 1 &&
                       _indexToNodeInNeighborIndices[currIndex].Count == 1)
                {
                    var nextIndex = _indexToNodeOutNeighborIndices[currIndex][0];
                    var nextEdgeId = NodeIndicesToEdgeId(currIndex, nextIndex);
                    if (!visitedEdges.Add(nextEdgeId)) break;

                    buffer[len++] = nextIndex;
                    currIndex = nextIndex;
                }

                chains.Add(ConvertToNodes(buffer, len, _indexToNode));
                ArrayPool<int>.Shared.Return(buffer);
            }
        }

        // ---- Step 2: Remaining cycles ----
        for (var i = 0; i < n; i++)
        {
            foreach (var next in _indexToNodeOutNeighborIndices[i])
            {
                var edgeId = NodeIndicesToEdgeId(i, next);
                if (!visitedEdges.Add(edgeId)) continue;

                var buffer = ArrayPool<int>.Shared.Rent(_indexToNode.Count);
                var len = 0;

                buffer[len++] = i;
                buffer[len++] = next;

                var currIndex = next;

                while (true)
                {
                    if (_indexToNodeOutNeighborIndices[currIndex].Count != 1)
                        break;

                    var nextIndex = _indexToNodeOutNeighborIndices[currIndex][0];
                    var nextEdgeId = NodeIndicesToEdgeId(currIndex, nextIndex);
                    if (!visitedEdges.Add(nextEdgeId)) break;

                    if (nextIndex == i) // closed cycle
                    {
                        break;
                    }

                    buffer[len++] = nextIndex;
                    currIndex = nextIndex;
                }

                chains.Add(ConvertToNodes(buffer, len, _indexToNode));
                ArrayPool<int>.Shared.Return(buffer);
            }
        }

        return chains;
    }

    
    public DirectedAStarPathFinder<TNode> GetAStarPathFinder(Func<TNode, TNode, double> edgeWeightFunc, Func<TNode, TNode, double> heuristic)
    {
        return new DirectedAStarPathFinder<TNode>(
            this,
            edgeWeightFunc,
            heuristic
        );
    }

    public DirectedAStarWithLandmarksPathFinder<TNode> GetAStarWithLandmarksPathFinder(Func<TNode, TNode, double> edgeWeightFunc, int landmarkCount)
    {
        var pathFinder = new DirectedAStarWithLandmarksPathFinder<TNode>(
            this,
            edgeWeightFunc
        );

        pathFinder.Preprocess(landmarkCount);

        return pathFinder;
    }

    public DirectedBidirectionalAStarPathFinder<TNode> GetBidirectionalAStarPathFinder(Func<TNode, TNode, double> edgeWeightFunc, Func<TNode, TNode, double> heuristic)
    {
        return new DirectedBidirectionalAStarPathFinder<TNode>(
            this,
            edgeWeightFunc,
            heuristic
        );
    }

    public DirectedBidirectionalDijkstraPathFinder<TNode> GetBidirectionalDijkstraPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        return new DirectedBidirectionalDijkstraPathFinder<TNode>(
            this,
            edgeWeightFunc
        );
    }

    public DirectedBreadthFirstPathFinder<TNode> GetBreadthFirstPathFinder()
    {
        return new DirectedBreadthFirstPathFinder<TNode>(this);
    }

    public DirectedContractionHierarchiesPathFinder<TNode> GetContractionHierarchiesPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        var pathFinder = new DirectedContractionHierarchiesPathFinder<TNode>(
            this,
            edgeWeightFunc
        );

        pathFinder.Preprocess();

        return pathFinder;
    }

    public DirectedDijkstraPathFinder<TNode> GetDijkstraPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        return new DirectedDijkstraPathFinder<TNode>(
            this, 
            edgeWeightFunc
        );
    }

    public DirectedHubLabelingPathFinder<TNode> GetHubLabelingPathFinder(Func<TNode, TNode, double> edgeWeightFunc)
    {
        var pathFinder = new DirectedHubLabelingPathFinder<TNode>(
            this, 
            edgeWeightFunc
        );

        pathFinder.Preprocess();

        return pathFinder;
    }


    public IEnumerator<TNode> GetEnumerator() 
        => _indexToNode.GetEnumerator();
    
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
}
