namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot;

/// <summary>
/// This interface represents a full dot-statement such as a node definition, link
/// definition, graph attribute, a subgraph definition, etc.
/// See http://www.graphviz.org/content/dot-language for more details
/// </summary>
public interface IDotStatement
{
    /// <summary>
    /// The main top-level graph of this statement
    /// </summary>
    DotGraph MainGraph { get; }

    /// <summary>
    /// The direct parent graph of this statement
    /// </summary>
    IDotGraph ParentGraph { get; }
}