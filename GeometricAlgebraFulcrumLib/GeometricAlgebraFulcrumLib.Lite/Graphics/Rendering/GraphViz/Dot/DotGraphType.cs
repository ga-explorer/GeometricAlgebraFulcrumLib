namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot
{
    /// <summary>
    /// Types of graphs in GraphViz
    /// See http://www.graphviz.org/content/dot-language for more details
    /// </summary>
    public enum DotGraphType
    {
        /// <summary>
        /// Undirected graphs
        /// </summary>
        Undirected = 0,

        /// <summary>
        /// Directed graphs
        /// </summary>
        Directed = 1,

        /// <summary>
        /// Strictly directed graphs
        /// </summary>
        StrictDirected = 2
    }
}
