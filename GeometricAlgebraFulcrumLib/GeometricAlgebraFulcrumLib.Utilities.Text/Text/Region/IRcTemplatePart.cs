namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Region;

public interface IRcTemplatePart
{
    /// <summary>
    /// The text lines inside the tag
    /// </summary>
    IEnumerable<string> TextLines { get; }

    /// <summary>
    /// The marked text lines inside the tag
    /// </summary>
    IEnumerable<string> TemplateTextLines { get; }

    /// <summary>
    /// The unmarked text lines inside the tag
    /// </summary>
    IEnumerable<string> GeneratedTextLines { get; }
}