namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot
{
    /// <summary>
    /// This interface represents a graph or subgraph in the constructed dot AST
    /// See http://www.graphviz.org/content/dot-language for more details
    /// </summary>
    public interface IDotGraph
    {
        /// <summary>
        /// The list of statememts of this graph
        /// </summary>
        IEnumerable<IDotStatement> Statements { get; }

        /// <summary>
        /// The list of all node statements of this graph
        /// </summary>
        IEnumerable<DotNode> NodeStatements { get; }

        /// <summary>
        /// The first node stement of this graph
        /// </summary>
        DotNode FirstNodeStatement { get; }

        /// <summary>
        /// The last node stement of this graph
        /// </summary>
        DotNode LastNodeStatement { get; }

        /// <summary>
        /// A list of node defaults stements of this graph
        /// </summary>
        IEnumerable<DotNode> NodeDefaultsStatements { get; }

        /// <summary>
        /// The first node defaults statement of this graph
        /// </summary>
        DotNode FirstNodeDefaultsStatement { get; }

        /// <summary>
        /// The first node defaults statement of this graph
        /// </summary>
        DotNode LastNodeDefaultsStatement { get; }

        /// <summary>
        /// A list of edge stements of this graph
        /// </summary>
        IEnumerable<DotEdge> EdgeStatements { get; }

        /// <summary>
        /// The first edge statement of this graph
        /// </summary>
        DotEdge FirstEdgeStatement { get; }

        /// <summary>
        /// The last edge statement of this graph
        /// </summary>
        DotEdge LastEdgeStatement { get; }

        /// <summary>
        /// A list of edge defaults statements of this graph
        /// </summary>
        IEnumerable<DotEdge> EdgeDefaultsStatements { get; }

        /// <summary>
        /// The first edge defaults statement of this graph
        /// </summary>
        DotEdge FirstEdgeDefaultsStatement { get; }

        /// <summary>
        /// The last edge defaults statement of this graph
        /// </summary>
        DotEdge LastEdgeDefaultsStatement { get; }

        /// <summary>
        /// A list of subgraph definition statements of this graph
        /// </summary>
        IEnumerable<DotSubGraph> SubGraphStatements { get; }

        /// <summary>
        /// The first subgraph definition statement of this graph
        /// </summary>
        DotSubGraph FirstSubGraphStatement { get; }

        /// <summary>
        /// The last subgraph definition statement of this graph
        /// </summary>
        DotSubGraph LastSubGraphStatement { get; }

        /// <summary>
        /// A list of cluster definition statements of this graph
        /// </summary>
        IEnumerable<DotSubGraph> ClusterStatements { get; }

        /// <summary>
        /// The first cluster definition statement of this graph
        /// </summary>
        DotSubGraph FirstClusterStatement { get; }

        /// <summary>
        /// The last cluster definition statement of this graph
        /// </summary>
        DotSubGraph LastClusterStatement { get; }

        /// <summary>
        /// A list of non-cluster subgraph definition statements of this graph
        /// </summary>
        IEnumerable<DotSubGraph> NonClusterStatements { get; }

        /// <summary>
        /// The first non-cluster subgraph definition statement of this graph
        /// </summary>
        DotSubGraph FirstNonClusterStatement { get; }

        /// <summary>
        /// The last non-cluster subgraph definition statement of this graph
        /// </summary>
        DotSubGraph LastNonClusterStatement { get; }

        /// <summary>
        /// A list of subgraph defaults statements of this graph
        /// </summary>
        IEnumerable<DotSubGraphDefaults> SubGraphDefaultsStatements { get; }

        /// <summary>
        /// The first subgraph defaults statement of this graph
        /// </summary>
        DotSubGraphDefaults FirstSubGraphDefaultsStatement { get; }

        /// <summary>
        /// The last subgraph defaults statement of this graph
        /// </summary>
        DotSubGraphDefaults LastSubGraphDefaultsStatement { get; }

        /// <summary>
        /// A list of fixed code statements of this graph
        /// </summary>
        IEnumerable<DotFixedCode> FixedCodeStatements { get; }

        /// <summary>
        /// The first fixed code statement of this graph
        /// </summary>
        DotFixedCode FirstFixedCodeStatement { get; }

        /// <summary>
        /// The last fixed code statement of this graph
        /// </summary>
        DotFixedCode LastFixedCodeStatement { get; }

        /// <summary>
        /// The parent main or sub-graph. If this the a main graph it returns null.
        /// </summary>
        IDotGraph ParentGraph { get; }

        /// <summary>
        /// The parent graph as a top-level main graph. If the parent is a sub-graph this 
        /// returns null. If this the a main graph it returns null.
        /// </summary>
        DotGraph ParentAsMainGraph { get; }

        /// <summary>
        /// The parent graph as a sub-graph. If the parent is a main graph this returns null.
        /// If this the a main graph it returns null.
        /// </summary>
        DotSubGraph ParentAsSubGraph { get; }

        /// <summary>
        /// The main graph of this graph. If this is a main graph it returns itself else it searches 
        /// the parent graphs upward until the main graph is found.
        /// </summary>
        DotGraph MainGraph { get; }

        /// <summary>
        /// True if this is a sub-graph
        /// </summary>
        bool IsSubGraph { get; }

        /// <summary>
        /// True if this is a main graph
        /// </summary>
        bool IsMainGraph { get; }

        /// <summary>
        /// True if this is a cluster sub-graph
        /// </summary>
        bool IsCluster { get; }

        /// <summary>
        /// True if this is a sub-graph but not a cluster
        /// </summary>
        bool IsNonClusterSubGraph { get; }
    }
}
