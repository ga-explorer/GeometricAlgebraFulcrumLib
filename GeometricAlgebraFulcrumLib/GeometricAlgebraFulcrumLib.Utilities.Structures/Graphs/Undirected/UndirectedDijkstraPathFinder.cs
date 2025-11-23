namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes the shortest path between two nodes using Dijkstra's algorithm.
    /// 
    /// Characteristics:
    /// - Works on graphs with non-negative edge weights.
    /// - Guarantees the optimal shortest path.
    /// - Time complexity: O((V + E) log V) with a priority queue.
    /// - Space complexity: O(V).
    /// 
    /// Recommended when no heuristic is available and edge weights are positive.
    /// </summary>
    public class UndirectedDijkstraPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            var open = new PriorityQueue<TNode, double>();
            var cameFrom = new Dictionary<TNode, TNode>();
            var gScore = new Dictionary<TNode, double> { [start] = 0.0 };

            open.Enqueue(start, 0.0);

            while (open.Count > 0)
            {
                var current = open.Dequeue();
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
                    return (path, gScore[goal]);
                }

                foreach (var (w, neighbor) in Graph.GetNodeWeightedNeighbors(current, EdgeWeightFunc))
                {
                    var tentative = gScore[current] + w;
                    if (!gScore.TryGetValue(neighbor, out var prev) || tentative < prev)
                    {
                        cameFrom[neighbor] = current;
                        gScore[neighbor] = tentative;
                        open.Enqueue(neighbor, tentative);
                    }
                }
            }

            return ([], double.PositiveInfinity);
        }
    }
}