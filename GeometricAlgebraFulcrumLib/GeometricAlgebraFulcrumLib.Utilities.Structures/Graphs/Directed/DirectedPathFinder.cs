namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Directed
{
    /// <summary>
    /// Abstract base class for shortest path search algorithms on a directed adjacency graph.
    /// Provides a unified API with algorithm-specific derived classes.
    /// </summary>
    /// <typeparam name="TNode">The node type of the graph.</typeparam>
    public abstract class DirectedPathFinder<TNode>(
        DirectedAdjacencyGraph<TNode> graph,
        Func<TNode, TNode, double> edgeWeightFunc)
        where TNode : notnull
    {
        protected readonly DirectedAdjacencyGraph<TNode> Graph = graph ?? throw new ArgumentNullException(nameof(graph));
        protected readonly Func<TNode, TNode, double> EdgeWeightFunc = edgeWeightFunc ?? throw new ArgumentNullException(nameof(edgeWeightFunc));

        /// <summary>
        /// Compute the shortest path between two nodes in the directed graph.
        /// Returns the path as a list of nodes and the total path cost.
        /// </summary>
        public abstract (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal);
    }
}