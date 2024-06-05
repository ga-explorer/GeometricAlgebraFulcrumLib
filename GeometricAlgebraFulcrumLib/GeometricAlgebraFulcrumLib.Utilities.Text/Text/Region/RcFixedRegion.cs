using System.Text;

namespace GeometricAlgebraFulcrumLib.Utilities.Text.Text.Region;

/// <summary>
/// A fixed region of text that will be preserved in the final generated text
/// </summary>
public sealed class RcFixedRegion : IRcRegion
{
    /// <summary>
    /// The text lines of this fixed region
    /// </summary>
    private readonly List<string> _fixedLinesList;


    public IEnumerable<string> TextLines => _fixedLinesList;

    public IEnumerable<string> TemplateTextLines => _fixedLinesList;

    public IEnumerable<string> GeneratedTextLines => _fixedLinesList;

    public bool IsFixed => true;

    public bool IsSlot => false;


    internal RcFixedRegion()
    {
        _fixedLinesList = new List<string>();
    }


    /// <summary>
    /// Create an exact copy of this region
    /// </summary>
    /// <returns></returns>
    public RcFixedRegion CreateCopy()
    {
        var newRegion = new RcFixedRegion();

        newRegion._fixedLinesList.AddRange(_fixedLinesList);

        return newRegion;
    }

    /// <summary>
    /// Clear all text in this region
    /// </summary>
    /// <returns></returns>
    public RcFixedRegion ClearText()
    {
        _fixedLinesList.Clear();

        return this;
    }

    /// <summary>
    /// Add text to this region
    /// </summary>
    /// <param name="text"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcFixedRegion AddText(string text, string linePrefix = null)
    {
        if (string.IsNullOrEmpty(text)) return this;

        var textLines = text.SplitLines();

        _fixedLinesList.AddRange(
            string.IsNullOrEmpty(linePrefix)
                ? textLines
                : textLines.Select(t => linePrefix + t)
        );

        return this;
    }

    /// <summary>
    /// Add text lines to this region
    /// </summary>
    /// <param name="textLines"></param>
    /// <returns></returns>
    public RcFixedRegion AddTextLines(IEnumerable<string> textLines)
    {
        _fixedLinesList.AddRange(textLines);

        return this;
    }

    /// <summary>
    /// Add text lines to this region
    /// </summary>
    /// <param name="textLines"></param>
    /// <returns></returns>
    public RcFixedRegion AddTextLines(params string[] textLines)
    {
        _fixedLinesList.AddRange(textLines);

        return this;
    }

    /// <summary>
    /// Set text of this region
    /// </summary>
    /// <param name="text"></param>
    /// <param name="linePrefix"></param>
    /// <returns></returns>
    public RcFixedRegion SetText(string text, string linePrefix = null)
    {
        ClearText();
        return AddText(text, linePrefix);
    }

    public override string ToString()
    {
        var s = new StringBuilder();

        foreach (var codeLine in _fixedLinesList)
            s.AppendLine(codeLine);

        return s.ToString();
    }
}