namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Graphs.Undirected
{
    /// <summary>
    /// Abstract base class for shortest path search algorithms on an
    /// undirected adjacency graph. Stores the graph and edge weight
    /// function, and exposes a unified FindPath API.
    /// </summary>
    /// <typeparam name="TNode">The node type of the graph.</typeparam>
    public abstract class UndirectedPathFinder<TNode>(UndirectedAdjacencyGraph<TNode> graph, Func<TNode, TNode, double> edgeWeightFunc)
        where TNode : notnull
    {
        public UndirectedAdjacencyGraph<TNode> Graph { get; } 
            = graph ?? throw new ArgumentNullException(nameof(graph));

        public Func<TNode, TNode, double> EdgeWeightFunc { get; } 
            = edgeWeightFunc ?? throw new ArgumentNullException(nameof(edgeWeightFunc));


        /// <summary>
        /// Find the shortest path between two nodes.
        /// Returns the path as a list of nodes and the path cost.
        /// </summary>
        public abstract (List<TNode> Path, double Cost) FindPath(TNode start, TNode goal);
    }
}