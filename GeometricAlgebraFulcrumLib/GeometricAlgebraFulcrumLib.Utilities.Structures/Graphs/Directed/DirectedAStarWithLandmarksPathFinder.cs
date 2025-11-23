namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed
{
    /// <summary>
    /// Computes shortest paths in a directed graph using the ALT algorithm (A*, Landmarks, Triangle inequality).
    /// 
    /// Characteristics:
    /// - Preprocesses the graph by selecting landmarks and computing the shortest path distances from them.
    /// - Heuristic is derived from the triangle inequality using landmark distances.
    /// - Provides stronger heuristics than plain A*, reducing explored nodes significantly.
    /// - Preprocessing time: O(k · (V + E) log V), where k is the number of landmarks.
    /// - Query time: O((V + E) log V), often much faster in practice.
    /// - Space complexity: O(k · V).
    /// 
    /// Recommended for repeated shortest-path queries on large directed graphs.
    /// </summary>
    public class DirectedAStarWithLandmarksPathFinder<TNode>(DirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        DirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private List<TNode>? _landmarks;
        private Dictionary<TNode, double[]>? _distFromLandmarks;

        public void Preprocess(int landmarkCount)
        {
            _landmarks = new List<TNode>();
            _distFromLandmarks = new Dictionary<TNode, double[]>();

            var count = 0;
            foreach (var node in Graph.Nodes)
            {
                _landmarks.Add(node);
                count++;
                if (count >= landmarkCount) break;
            }

            for (var li = 0; li < _landmarks.Count; li++)
            {
                var landmark = _landmarks[li];
                var dists = RunDijkstra(landmark);

                foreach (var (n, d) in dists)
                {
                    if (!_distFromLandmarks.TryGetValue(n, out var arr))
                    {
                        arr = new double[_landmarks.Count];
                        _distFromLandmarks[n] = arr;
                    }
                    arr[li] = d;
                }
            }
        }

        private Dictionary<TNode, double> RunDijkstra(TNode source)
        {
            var dist = new Dictionary<TNode, double> { [source] = 0 };
            var pq = new PriorityQueue<TNode, double>();
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                var u = pq.Dequeue();
                foreach (var (w, v) in Graph.GetNodeWeightedOutNeighbors(u, EdgeWeightFunc))
                {
                    var nd = dist[u] + w;
                    if (!dist.TryGetValue(v, out var old) || nd < old)
                    {
                        dist[v] = nd;
                        pq.Enqueue(v, nd);
                    }
                }
            }

            return dist;
        }

        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            if (_landmarks is null || _distFromLandmarks is null)
                throw new InvalidOperationException("ALT must be preprocessed first.");

            double Heuristic(TNode n)
            {
                double h = 0;
                var startVec = _distFromLandmarks[start];
                var goalVec = _distFromLandmarks[goal];
                for (var i = 0; i < _landmarks.Count; i++)
                    h = Math.Max(h, Math.Abs(startVec[i] - goalVec[i]));
                return h;
            }

            var astar = new DirectedAStarPathFinder<TNode>(Graph, EdgeWeightFunc, (n, g) => Heuristic(n));
            return astar.FindPath(start, goal);
        }
    }
}
