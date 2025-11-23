namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed
{
    /// <summary>
    /// Computes shortest paths in a directed graph using Hub Labeling (HL).
    /// Requires preprocessing into hub label sets.
    /// </summary>
    public class DirectedHubLabelingPathFinder<TNode>(DirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc) : 
        DirectedPathFinder<TNode>(graph, edgeWeightFunc)
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
            if (_labels is null)
                throw new InvalidOperationException("HL must be preprocessed first.");

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
