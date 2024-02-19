namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label;

/// <summary>
/// This interface represents an HTML tag in the dot language
/// </summary>
public interface IDotHtmlTag
{
    /// <summary>
    /// The name of this tag
    /// </summary>
    string TagName { get; }

    /// <summary>
    /// A list of attribute names and values for this tag
    /// </summary>
    IEnumerable<KeyValuePair<string, string>> Attributes { get; }
}