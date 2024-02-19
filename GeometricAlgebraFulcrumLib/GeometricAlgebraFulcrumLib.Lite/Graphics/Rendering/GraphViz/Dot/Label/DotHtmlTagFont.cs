using System.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label.Text;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label;

/// <summary>
/// This class represents an HTML font tag in the dot language
/// See http://www.graphviz.org/content/node-shapes#html for more details
/// </summary>
public sealed class DotHtmlTagFont : DotHtmlTag, IDotHtmlTagFormatting
{
    internal readonly Dictionary<string, string> AttrValues = new Dictionary<string, string>();


    public override IEnumerable<KeyValuePair<string, string>> Attributes => AttrValues;

    public string Color => AttrValues.GetAttribute("COLOR");

    public string FontName => AttrValues.GetAttribute("FACE");

    public string PointSize => AttrValues.GetAttribute("POINT-SIZE");


    internal DotHtmlTagFont()
        : base("FONT")
    {
    }


    public DotHtmlTagFont ClearAttributes()
    {
        AttrValues.Clear();

        return this;
    }


    public DotHtmlTagFont ClearColor()
    {
        AttrValues.ClearAttribute("COLOR");

        return this;
    }

    public DotHtmlTagFont ClearFontName()
    {
        AttrValues.ClearAttribute("FACE");

        return this;
    }

    public DotHtmlTagFont ClearPointSize()
    {
        AttrValues.ClearAttribute("POINT-SIZE");

        return this;
    }


    public DotHtmlTagFont SetColor(DotColor color)
    {
        AttrValues.SetAttribute("COLOR", color.Value);

        return this;
    }

    public DotHtmlTagFont SetFontName(string fontName)
    {
        AttrValues.SetAttribute("FACE", fontName);

        return this;
    }

    public DotHtmlTagFont SetPointSize(float size)
    {
        AttrValues.SetAttribute("POINT-SIZE", size);

        return this;
    }

    public string FormatText(DotHtmlText text)
    {
        var s = new StringBuilder();

        s.Append("<")
            .Append(TagName);

        foreach (var pair in Attributes)
            s.Append(" ").Append(pair.Key).Append("=").Append(pair.Value.ValueToQuotedLiteral());

        s.Append(">");

        s.Append(ReferenceEquals(text, null) ? "" : text.ToString());

        return s
            .Append("</")
            .Append(TagName)
            .Append(">")
            .ToString();
    }

    public override string ToString()
    {
        return FormatText(null);
    }
}