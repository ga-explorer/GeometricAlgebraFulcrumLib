using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Color;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Label.Text;
using GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot.Value;
using TextComposerLib;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Rendering.GraphViz.Dot;

/// <summary>
/// This class represents an AST node corresponding to a subgraph defaults statement
/// in the dot code.
/// See http://www.graphviz.org/content/dot-language 
/// and http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotSubGraphDefaults : IDotStatement
{
    internal readonly Dictionary<string, string> AttrValues = new Dictionary<string, string>();


    public IDotGraph ParentGraph { get; }

    public DotGraph MainGraph => ParentGraph.MainGraph;

    /// <summary>
    /// True if this statement has attributes
    /// </summary>
    public bool HasAttributes => AttrValues.Count > 0;

    /// <summary>
    /// Returns null
    /// </summary>
    public DotSubGraph AsSubGraph => null;

    /// <summary>
    /// Returns this statement as it is
    /// </summary>
    public DotSubGraphDefaults AsDefaults => this;

    #region Attributes Accessors
    public string K
    {
        get => AttrValues.GetAttribute("K");
        set => AttrValues.SetAttribute("K", value);
    }

    public string Url
    {
        get => AttrValues.GetAttribute("URL");
        set => AttrValues.SetAttribute("URL", value);
    }

    public string Area
    {
        get => AttrValues.GetAttribute("area");
        set => AttrValues.SetAttribute("area", value);
    }

    public string BackgroundColor
    {
        get => AttrValues.GetAttribute("bgcolor");
        set => AttrValues.SetAttribute("bgcolor", value);
    }

    public string Color
    {
        get => AttrValues.GetAttribute("color");
        set => AttrValues.SetAttribute("color", value);
    }

    public string ColorScheme
    {
        get => AttrValues.GetAttribute("colorscheme");
        set => AttrValues.SetAttribute("colorscheme", value);
    }

    public string FillColor
    {
        get => AttrValues.GetAttribute("fillcolor");
        set => AttrValues.SetAttribute("fillcolor", value);
    }

    public string FontColor
    {
        get => AttrValues.GetAttribute("fontcolor");
        set => AttrValues.SetAttribute("fontcolor", value);
    }

    public string FontName
    {
        get => AttrValues.GetAttribute("fontname");
        set => AttrValues.SetAttribute("fontname", value);
    }

    public string FontSize
    {
        get => AttrValues.GetAttribute("fontsize");
        set => AttrValues.SetAttribute("fontsize", value);
    }

    public string GradientAngle
    {
        get => AttrValues.GetAttribute("gradientangle");
        set => AttrValues.SetAttribute("gradientangle", value);
    }

    public string HRef
    {
        get => AttrValues.GetAttribute("href");
        set => AttrValues.SetAttribute("href", value);
    }

    public string Id
    {
        get => AttrValues.GetAttribute("id");
        set => AttrValues.SetAttribute("id", value);
    }

    public string TeXLabel
    {
        get => AttrValues.GetAttribute("texlbl");
        set => AttrValues.SetAttribute("texlbl", value);
    }

    public string Label
    {
        get => AttrValues.GetAttribute("label");
        set => AttrValues.SetAttribute("label", value);
    }

    public string LabelJustify
    {
        get => AttrValues.GetAttribute("labeljust");
        set => AttrValues.SetAttribute("labeljust", value);
    }

    public string LabelLocation
    {
        get => AttrValues.GetAttribute("labelloc");
        set => AttrValues.SetAttribute("labelloc", value);
    }

    public string Layer
    {
        get => AttrValues.GetAttribute("layer");
        set => AttrValues.SetAttribute("layer", value);
    }

    public string LabelHeight
    {
        get => AttrValues.GetAttribute("lheight");
        set => AttrValues.SetAttribute("lheight", value);
    }

    public string LabelPosition
    {
        get => AttrValues.GetAttribute("lp");
        set => AttrValues.SetAttribute("lp", value);
    }

    public string LabelWidth
    {
        get => AttrValues.GetAttribute("lwidth");
        set => AttrValues.SetAttribute("lwidth", value);
    }

    public string Margin
    {
        get => AttrValues.GetAttribute("margin");
        set => AttrValues.SetAttribute("margin", value);
    }

    public string NoJustify
    {
        get => AttrValues.GetAttribute("nojustify");
        set => AttrValues.SetAttribute("nojustify", value);
    }

    public string PenColor
    {
        get => AttrValues.GetAttribute("pencolor");
        set => AttrValues.SetAttribute("pencolor", value);
    }

    public string PenWidth
    {
        get => AttrValues.GetAttribute("penwidth");
        set => AttrValues.SetAttribute("penwidth", value);
    }

    public string Peripheries
    {
        get => AttrValues.GetAttribute("peripheries");
        set => AttrValues.SetAttribute("peripheries", value);
    }

    public string Rank
    {
        get => AttrValues.GetAttribute("rank");
        set => AttrValues.SetAttribute("rank", value);
    }

    public string SortVertical
    {
        get => AttrValues.GetAttribute("sortv");
        set => AttrValues.SetAttribute("sortv", value);
    }

    public string Style
    {
        get => AttrValues.GetAttribute("style");
        set => AttrValues.SetAttribute("style", value);
    }

    public string Target
    {
        get => AttrValues.GetAttribute("target");
        set => AttrValues.SetAttribute("target", value);
    }

    public string ToolTip
    {
        get => AttrValues.GetAttribute("tooltip");
        set => AttrValues.SetAttribute("tooltip", value);
    }


    public DotSubGraphDefaults SetK(float value)
    {
        AttrValues.SetAttribute("K", value);

        return this;
    }

    public DotSubGraphDefaults SetUrl(DotHtmlString value)
    {
        AttrValues.SetAttribute("URL", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetArea(float value)
    {
        AttrValues.SetAttribute("area", value);

        return this;
    }

    public DotSubGraphDefaults SetBackgroundColor(DotColor value)
    {
        AttrValues.SetAttribute("bgcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetBackgroundColor(DotColorList value)
    {
        AttrValues.SetAttribute("bgcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetColor(DotColor value)
    {
        AttrValues.SetAttribute("color", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetColor(DotColorList value)
    {
        AttrValues.SetAttribute("color", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetColorScheme(DotColorScheme value)
    {
        AttrValues.SetAttribute("colorscheme", value.Value);

        return this;
    }

    public DotSubGraphDefaults SetFillColor(DotColor value)
    {
        AttrValues.SetAttribute("fillcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetFillColor(DotColorList value)
    {
        AttrValues.SetAttribute("fillcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetFontColor(DotColor value)
    {
        AttrValues.SetAttribute("fontcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetFontName(string value)
    {
        AttrValues.SetAttribute("fontname", value.DoubleQuote());

        return this;
    }

    public DotSubGraphDefaults SetFontSize(float value)
    {
        AttrValues.SetAttribute("fontsize", value);

        return this;
    }

    public DotSubGraphDefaults SetGradientAngle(int value)
    {
        AttrValues.SetAttribute("gradientangle", value);

        return this;
    }

    public DotSubGraphDefaults SetHRef(string value)
    {
        AttrValues.SetAttribute("href", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraphDefaults SetId(DotHtmlString value)
    {
        AttrValues.SetAttribute("id", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetTeXLabel(string value)
    {
        AttrValues.SetAttribute("texlbl", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraphDefaults SetLabel(string value)
    {
        AttrValues.SetAttribute("label", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraphDefaults SetLabel(IDotHtmlLabel value)
    {
        AttrValues.SetAttribute("label", value.TaggedValue);

        return this;
    }

    public DotSubGraphDefaults SetLabelJustification(DotJustification value)
    {
        AttrValues.SetAttribute("labeljust", value.Value);

        return this;
    }

    public DotSubGraphDefaults SetLabelVerticalLocation(DotVerticalLocation value)
    {
        AttrValues.SetAttribute("labelloc", value.Value);

        return this;
    }

    public DotSubGraphDefaults SetLayer(string value)
    {
        AttrValues.SetAttribute("layer", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraphDefaults SetLabelHeight(float value)
    {
        AttrValues.SetAttribute("lheight", value);

        return this;
    }

    public DotSubGraphDefaults SetLabelPosition(float x, float y)
    {
        AttrValues.SetAttribute("lp", DotUtils.ToDotPoint(x, y));

        return this;
    }

    public DotSubGraphDefaults SetLabelPosition(float x, float y, float z)
    {
        AttrValues.SetAttribute("lp", DotUtils.ToDotPoint(x, y, z));

        return this;
    }

    public DotSubGraphDefaults SetLabelWidth(float value)
    {
        AttrValues.SetAttribute("lwidth", value);

        return this;
    }

    public DotSubGraphDefaults SetMargin(float value)
    {
        AttrValues.SetAttribute("margin", value);

        return this;
    }

    public DotSubGraphDefaults SetMargin(float x, float y)
    {
        AttrValues.SetAttribute("margin", DotUtils.ToDotPoint(x, y));

        return this;
    }

    public DotSubGraphDefaults SetNoJustify(bool value)
    {
        AttrValues.SetAttribute("nojustify", value);

        return this;
    }

    public DotSubGraphDefaults SetOrdering(DotOrdering value)
    {
        AttrValues.SetAttribute("ordering", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetPenColor(DotColor value)
    {
        AttrValues.SetAttribute("pencolor", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetPenColor(int value)
    {
        AttrValues.SetAttribute("pencolor", value);

        return this;
    }

    public DotSubGraphDefaults SetPenWidth(float value)
    {
        AttrValues.SetAttribute("penwidth", value);

        return this;
    }

    public DotSubGraphDefaults SetPeripheries(bool value)
    {
        AttrValues.SetAttribute("peripheries", value ? "1" : "0");

        return this;
    }

    public DotSubGraphDefaults SetRank(DotRankType value)
    {
        AttrValues.SetAttribute("rank", value.Value);

        return this;
    }

    public DotSubGraphDefaults SetSortValue(int value)
    {
        AttrValues.SetAttribute("sortv", value);

        return this;
    }

    public DotSubGraphDefaults SetStyle(DotClusterStyle value)
    {
        AttrValues.SetAttribute("style", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetStyle(params DotClusterStyle[] values)
    {
        AttrValues.SetAttribute("style", values.ToDotStyle());

        return this;
    }

    public DotSubGraphDefaults SetTarget(DotHtmlString value)
    {
        AttrValues.SetAttribute("target", value.QuotedValue);

        return this;
    }

    public DotSubGraphDefaults SetToolTip(DotHtmlString value)
    {
        AttrValues.SetAttribute("tooltip", value.QuotedValue);

        return this;
    }
    #endregion


    internal DotSubGraphDefaults(IDotGraph parentGraph)
    {
        ParentGraph = parentGraph;
    }
}