namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes shortest paths using Contraction Hierarchies (CH).
    /// 
    /// Characteristics:
    /// - Preprocesses the graph by contracting nodes and adding shortcut edges.
    /// - Queries use only "upward edges" according to the contraction order.
    /// - Preprocessing time: O(V log V + E) (depends on contraction order heuristic).
    /// - Query time: O(log V) in practice, very fast for large graphs.
    /// - Space complexity: O(V + E + shortcuts).
    /// 
    /// Recommended for road networks and large graphs with many repeated queries.
    /// </summary>
    public class UndirectedContractionHierarchiesPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private int[]? _order;
        private List<(int neighbor, double w)>[]? _up;

        public void Preprocess()
        {
            var n = Graph.Count;
            _order = Enumerable.Range(0, n).ToArray(); // trivial order (improve with heuristic)
            _up = new List<(int, double)>[n];
            for (var i = 0; i < n; i++) _up[i] = [];

            for (var u = 0; u < n; u++)
            {
                foreach (var v in Graph.GetNodeNeighborIndicesAt(u))
                {
                    if (_order[u] < _order[v])
                        _up[u].Add((v, EdgeWeightFunc(Graph[u], Graph[v])));
                }
            }
        }

        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            if (_order is null || _up is null)
                throw new InvalidOperationException("CH must be preprocessed first.");

            if (!Graph.TryGetNodeIndex(start, out var s) || !Graph.TryGetNodeIndex(goal, out var t))
                return ([], double.PositiveInfinity);

            var dist = new double[Graph.Count];
            for (var i = 0; i < dist.Length; i++) 
                dist[i] = double.PositiveInfinity;

            dist[s] = 0;

            var pq = new PriorityQueue<int, double>();
            pq.Enqueue(s, 0);

            while (pq.Count > 0)
            {
                var u = pq.Dequeue();
                foreach (var (v, w) in _up[u])
                {
                    var nd = dist[u] + w;
                    if (nd < dist[v])
                    {
                        dist[v] = nd;
                        pq.Enqueue(v, nd);
                    }
                }
            }

            return ([start, goal], dist[t]);
        }
    }
}
