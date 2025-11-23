namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes the shortest path using the A* search algorithm.
    /// 
    /// Characteristics:
    /// - Works on graphs with non-negative edge weights.
    /// - Uses a heuristic to guide the search towards the goal.
    /// - Can significantly reduce explored nodes if heuristic is admissible (never overestimates).
    /// - Falls back to Dijkstra's behavior if the heuristic always returns 0.
    /// - Time complexity: O((V + E) log V) depending on heuristic accuracy.
    /// 
    /// Recommended for pathfinding when a good heuristic function is available (e.g., Euclidean distance in spatial graphs).
    /// </summary>
    public class UndirectedAStarPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc, Func<TNode, TNode, double> heuristic) :
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private readonly Func<TNode, TNode, double> _heuristic 
            = heuristic ?? throw new ArgumentNullException(nameof(heuristic));

        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            var open = new PriorityQueue<TNode, double>();
            var cameFrom = new Dictionary<TNode, TNode>();
            var gScore = new Dictionary<TNode, double> { [start] = 0.0 };

            open.Enqueue(start, _heuristic(start, goal));

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
                    if (gScore.TryGetValue(neighbor, out var prev) && !(tentative < prev)) 
                        continue;
                    
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentative;
                    open.Enqueue(neighbor, tentative + _heuristic(neighbor, goal));
                }
            }

            return ([], double.PositiveInfinity);
        }
    }
}
