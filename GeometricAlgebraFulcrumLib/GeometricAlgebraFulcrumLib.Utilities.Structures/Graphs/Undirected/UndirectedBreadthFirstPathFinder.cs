namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes the shortest path in an unweighted graph using Breadth-First Search (BFS).
    /// 
    /// Characteristics:
    /// - Assumes all edges have equal weight (treated as 1).
    /// - Returns the path with the minimum number of edges.
    /// - Time complexity: O(V + E).
    /// - Space complexity: O(V).
    /// 
    /// Recommended when all edges are unweighted or equally weighted.
    /// </summary>
    public class UndirectedBreadthFirstPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph) : 
        UndirectedPathFinder<TNode>(graph, (_, _) => 1.0)
        where TNode : notnull
    {
        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            var q = new Queue<TNode>();
            var visited = new HashSet<TNode>(Graph.NodeComparer);
            var cameFrom = new Dictionary<TNode, TNode>();

            q.Enqueue(start);
            visited.Add(start);

            while (q.Count > 0)
            {
                var current = q.Dequeue();
                if (Graph.NodeComparer.Equals(current, goal))
                {
                    var path = new List<TNode>();
                    var node = goal;
                    while (cameFrom.ContainsKey(node))
                    {
                        path.Add(node);
                        node = cameFrom[node];
                    }
                    path.Add(start);
                    path.Reverse();
                    return (path, path.Count - 1);
                }

                foreach (var neighbor in Graph.GetNodeNeighbors(current))
                {
                    if (visited.Add(neighbor))
                    {
                        cameFrom[neighbor] = current;
                        q.Enqueue(neighbor);
                    }
                }
            }

            return (new List<TNode>(), double.PositiveInfinity);
        }
    }
}