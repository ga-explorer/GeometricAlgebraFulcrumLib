namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed
{
    /// <summary>
    /// Computes shortest paths in a directed graph using Contraction Hierarchies (CH).
    /// 
    /// Characteristics:
    /// - Preprocesses the graph by contracting nodes and adding shortcut edges.
    /// - Queries use only "upward edges" according to the contraction order.
    /// - Preprocessing time: O(V log V + E) (depends on contraction order heuristic).
    /// - Query time: O(log V) in practice, very fast for large graphs.
    /// - Space complexity: O(V + E + shortcuts).
    /// 
    /// Recommended for road networks and large directed graphs with many repeated queries.
    /// </summary>
    public class DirectedContractionHierarchiesPathFinder<TNode>(DirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        DirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private int[]? _order;
        private List<(int, double)>[]? _up;

        public void Preprocess()
        {
            var n = Graph.Count;
            _order = Enumerable.Range(0, n).ToArray();
            _up = new List<(int, double)>[n];
            for (var i = 0; i < n; i++) _up[i] = [];

            for (var u = 0; u < n; u++)
            {
                foreach (var v in Graph.GetNodeOutNeighborIndicesAt(u))
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
                    if (!(nd < dist[v])) continue;

                    dist[v] = nd;
                    pq.Enqueue(v, nd);
                }
            }

            return ([start, goal], dist[t]);
        }
    }
}
