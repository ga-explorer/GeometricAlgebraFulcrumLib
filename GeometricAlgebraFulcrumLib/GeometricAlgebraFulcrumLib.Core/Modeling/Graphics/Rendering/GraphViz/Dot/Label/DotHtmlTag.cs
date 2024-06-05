using System.Text;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Color;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.GraphViz.Dot.Label;

/// <summary>
/// This is the base class for HTML tags in the dot language
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public abstract class DotHtmlTag : IDotHtmlTag
{
    /// <summary>
    /// Create an italic formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Italic()
    {
        return new DotHtmlTagFormatting("I");
    }

    /// <summary>
    /// Create a bold formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Bold()
    {
        return new DotHtmlTagFormatting("B");
    }

    /// <summary>
    /// Create an underline formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Underline()
    {
        return new DotHtmlTagFormatting("U");
    }

    /// <summary>
    /// Create an overline formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Overline()
    {
        return new DotHtmlTagFormatting("O");
    }

    /// <summary>
    /// Create a subscript formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Subscript()
    {
        return new DotHtmlTagFormatting("SUB");
    }

    /// <summary>
    /// Create a superscript formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting Superscript()
    {
        return new DotHtmlTagFormatting("SUP");
    }

    /// <summary>
    /// Create a strikethrough formatting tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFormatting StrikeThrough()
    {
        return new DotHtmlTagFormatting("S");
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <returns></returns>
    public static DotHtmlTagFont Font()
    {
        return new DotHtmlTagFont();
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="color"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(DotColor color)
    {
        return
            new DotHtmlTagFont()
                .SetColor(color);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(float size)
    {
        return
            new DotHtmlTagFont()
                .SetPointSize(size);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="fontName"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(string fontName)
    {
        return
            new DotHtmlTagFont()
                .SetFontName(fontName);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="size"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(float size, DotColor color)
    {
        return
            new DotHtmlTagFont()
                .SetPointSize(size)
                .SetColor(color);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="fontName"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(string fontName, DotColor color)
    {
        return
            new DotHtmlTagFont()
                .SetFontName(fontName)
                .SetColor(color);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="fontName"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(string fontName, float size)
    {
        return
            new DotHtmlTagFont()
                .SetFontName(fontName)
                .SetPointSize(size);
    }

    /// <summary>
    /// Create a font tag
    /// </summary>
    /// <param name="fontName"></param>
    /// <param name="size"></param>
    /// <param name="color"></param>
    /// <returns></returns>
    public static DotHtmlTagFont Font(string fontName, float size, DotColor color)
    {
        return 
            new DotHtmlTagFont()
                .SetFontName(fontName)
                .SetPointSize(size)
                .SetColor(color);
    }


    public string TagName { get; }

    public abstract IEnumerable<KeyValuePair<string, string>> Attributes { get; }

    /// <summary>
    /// The open-tag text of this tag
    /// </summary>
    public string TagOpenText
    {
        get
        {
            var s = new StringBuilder();

            s.Append("<")
                .Append(TagName);

            foreach (var pair in Attributes)
                s.Append(" ").Append(pair.Key).Append("=").Append(pair.Value);

            return
                s.Append(">")
                    .ToString();
        }
    }

    /// <summary>
    /// The close-tag text of this tag
    /// </summary>
    public string TagCloseText => new StringBuilder()
        .Append("</")
        .Append(TagName)
        .Append(">")
        .ToString();

    /// <summary>
    /// The full tag without contents text
    /// </summary>
    public string TagNoContentsText
    {
        get
        {
            var s = new StringBuilder();

            s.Append("<")
                .Append(TagName);

            foreach (var pair in Attributes)
                s.Append(" ").Append(pair.Key).Append("=").Append(pair.Value);

            return
                s.Append("/>")
                    .ToString();
        }
    }


    protected DotHtmlTag(string tagName)
    {
        TagName = tagName;
    }
}