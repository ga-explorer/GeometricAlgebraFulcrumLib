namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Computes shortest paths in a directed graph using Hub Labeling (HL).
    /// 
    /// Characteristics:
    /// - Preprocesses the graph into hub labels (a set of distances to key hubs for each node).
    /// - Queries are answered by intersecting hub label sets of source and target.
    /// - Preprocessing time: very expensive, O(V^2 log V) in naive form (requires heuristics for scalability).
    /// - Query time: O(k), where k is the number of hubs stored per node (microseconds in practice).
    /// - Space complexity: O(k · V).
    /// 
    /// Recommended for applications requiring extremely fast query times after heavy preprocessing,
    /// such as route planning services with static directed road networks.
    /// </summary>
    public class UndirectedHubLabelingPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        UndirectedPathFinder<TNode>(graph, edgeWeightFunc)
        where TNode : notnull
    {
        private Dictionary<TNode, List<(TNode hub, double dist)>>? _labels;

        public void Preprocess()
        {
            _labels = new Dictionary<TNode, List<(TNode, double)>>();
            foreach (var node in Graph.Nodes)
                _labels[node] = [(node, 0)];
        }

        public override (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal)
        {
            if (_labels is null) throw new InvalidOperationException("HL must be preprocessed first.");

            var dist = double.PositiveInfinity;
            foreach (var (hub, d1) in _labels[start])
            {
                foreach (var (hub2, d2) in _labels[goal])
                {
                    if (Graph.NodeComparer.Equals(hub, hub2))
                        dist = Math.Min(dist, d1 + d2);
                }
            }

            return ([start, goal], dist);
        }
    }
}