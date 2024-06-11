using System.Text;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;

/// <summary>
/// This class is the base for HTML text in the dot language. HTML Text can be
/// a normal string, a line break, or a list of text items
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public abstract class DotHtmlText : IDotHtmlLabel
{
    public static readonly DotHtmlLineBreak LineBreak = new DotHtmlLineBreak();

    public static readonly DotHtmlLineBreak LineBreakAlignCenter = new DotHtmlLineBreak("CENTER");

    public static readonly DotHtmlLineBreak LineBreakAlignLeft = new DotHtmlLineBreak("LEFT");

    public static readonly DotHtmlLineBreak LineBreakAlignRight = new DotHtmlLineBreak("RIGHT");

    /// <summary>
    /// Create an HTML string
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public static DotHtmlString HtmlString(string text)
    {
        return new DotHtmlString(text);
    }

    /// <summary>
    /// Create a list of text items
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTextItemsList ItemsList()
    {
        return new DotHtmlTextItemsList();
    }

    /// <summary>
    /// Create a list of text items
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static DotHtmlTextItemsList ItemsList(IEnumerable<DotHtmlTextItem> items)
    {
        return new DotHtmlTextItemsList().AddRange(items);
    }

    /// <summary>
    /// Create a list of text items
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    public static DotHtmlTextItemsList ItemsList(params DotHtmlTextItem[] items)
    {
        return new DotHtmlTextItemsList().AddRange(items);
    }


    public abstract string Value { get; }

    public string QuotedValue => Value.DoubleQuote();

    public string TaggedValue => new StringBuilder()
        .Append('<')
        .Append(Value)
        .Append('>')
        .ToString();

    public string LiteralValue => Value.ValueToQuotedLiteral();


    public override string ToString()
    {
        return Value;
    }
}