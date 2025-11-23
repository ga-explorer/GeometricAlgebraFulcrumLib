namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes shortest paths using the ALT algorithm (A*, Landmarks, and Triangle inequality).
    /// 
    /// Characteristics:
    /// - Preprocesses the graph by selecting landmarks and computing the shortest path distances from them.
    /// - Heuristic is derived from the triangle inequality using landmark distances.
    /// - Provides stronger heuristics than plain A*, reducing explored nodes significantly.
    /// - Preprocessing time: O(k · (V + E) log V), where k is the number of landmarks.
    /// - Query time: O((V + E) log V), often much faster in practice.
    /// - Space complexity: O(k · V).
    /// 
    /// Recommended for repeated shortest-path queries on large graphs.
    /// </summary>
    public class UndirectedAStarWithLandmarksPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private List<TNode>? _landmarks;
        private Dictionary<TNode, double[]>? _distFromLandmarks;

        public void Preprocess(int landmarkCount)
        {
            _landmarks = [];
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
                var dists = RunSingleSourceDijkstra(landmark);

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

        private Dictionary<TNode, double> RunSingleSourceDijkstra(TNode source)
        {
            var dist = new Dictionary<TNode, double> { [source] = 0 };
            var pq = new PriorityQueue<TNode, double>();
            pq.Enqueue(source, 0);

            while (pq.Count > 0)
            {
                var u = pq.Dequeue();
                foreach (var (w, v) in Graph.GetNodeWeightedNeighbors(u, EdgeWeightFunc))
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

            var dijkstra = new UndirectedAStarPathFinder<TNode>(Graph, EdgeWeightFunc, (n, g) => Heuristic(n));
            return dijkstra.FindPath(start, goal);

            double Heuristic(TNode n)
            {
                double h = 0;
                var startVec = _distFromLandmarks[start];
                var goalVec = _distFromLandmarks[goal];
                for (var i = 0; i < _landmarks.Count; i++)
                    h = Math.Max(h, Math.Abs(startVec[i] - goalVec[i]));
                return h;
            }
        }
    }
}
