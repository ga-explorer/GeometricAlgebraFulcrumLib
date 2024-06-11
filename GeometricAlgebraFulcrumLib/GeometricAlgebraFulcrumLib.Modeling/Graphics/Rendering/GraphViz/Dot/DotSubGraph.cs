using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Color;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Label;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Label.Text;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot.Value;
using GeometricAlgebraFulcrumLib.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Rendering.GraphViz.Dot;

/// <summary>
/// This class represents a subgraph definition in the dot AST. A subgraph can also be 
/// a cluster. The attributes of a subgraph are added as separate statements to its
/// body of code, not like nodes or edges.
/// See  http://www.graphviz.org/content/dot-language 
/// and http://www.graphviz.org/content/attrs for more details
/// </summary>
public sealed class DotSubGraph : IDotEdgeSide, IDotStatement, IDotGraph
{
    internal List<IDotStatement> StatementsList = new List<IDotStatement>();


    public IDotGraph ParentGraph { get; }

    public DotGraph ParentAsMainGraph => ParentGraph as DotGraph;

    public DotSubGraph ParentAsSubGraph => ParentGraph as DotSubGraph;

    public DotGraph MainGraph { get; }

    public string SubGraphName { get; }


    public IEnumerable<IDotStatement> Statements => StatementsList;

    public IEnumerable<DotNode> NodeStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsNode);
        }
    }

    public DotNode FirstNodeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsNode);
        }
    }

    public DotNode LastNodeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsNode);
        }
    }

    public IEnumerable<DotNode> NodeDefaultsStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
        }
    }

    public DotNode FirstNodeDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
        }
    }

    public DotNode LastNodeDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotNode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsNodeDefaults);
        }
    }

    public IEnumerable<DotEdge> EdgeStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsEdge);
        }
    }

    public DotEdge FirstEdgeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdge);
        }
    }

    public DotEdge LastEdgeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdge);
        }
    }

    public IEnumerable<DotEdge> EdgeDefaultsStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
        }
    }

    public DotEdge FirstEdgeDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
        }
    }

    public DotEdge LastEdgeDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotEdge)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsEdgeDefaults);
        }
    }

    public IEnumerable<DotSubGraph> SubGraphStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotSubGraph FirstSubGraphStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotSubGraph LastSubGraphStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }

    public IEnumerable<DotSubGraph> ClusterStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsCluster);
        }
    }

    public DotSubGraph FirstClusterStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster);
        }
    }

    public DotSubGraph LastClusterStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster);
        }
    }

    public IEnumerable<DotSubGraph> NonClusterStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .Where(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
        }
    }

    public DotSubGraph FirstNonClusterStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
        }
    }

    public DotSubGraph LastNonClusterStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraph)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false && s.IsCluster == false);
        }
    }

    public IEnumerable<DotSubGraphDefaults> SubGraphDefaultsStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .Where(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotSubGraphDefaults FirstSubGraphDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotSubGraphDefaults LastSubGraphDefaultsStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotSubGraphDefaults)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }

    public IEnumerable<DotFixedCode> FixedCodeStatements
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotFixedCode)
                    .Where(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotFixedCode FirstFixedCodeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotFixedCode)
                    .FirstOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }

    public DotFixedCode LastFixedCodeStatement
    {
        get
        {
            return
                StatementsList
                    .Select(s => s as DotFixedCode)
                    .LastOrDefault(s => ReferenceEquals(s, null) == false);
        }
    }


    public bool IsSubGraph => true;

    public bool IsMainGraph => false;

    /// <summary>
    /// Returns this subgraph as it is
    /// </summary>
    public DotSubGraph AsSubGraph => this;

    /// <summary>
    /// Returns null
    /// </summary>
    public DotSubGraphDefaults AsDefaults => null;

    public bool IsCluster
    {
        get
        {
            if (SubGraphName[0] == '"')
                return
                    SubGraphName[1] == 'c' &&
                    SubGraphName[2] == 'l' &&
                    SubGraphName[3] == 'u' &&
                    SubGraphName[4] == 's' &&
                    SubGraphName[5] == 't' &&
                    SubGraphName[6] == 'e' &&
                    SubGraphName[7] == 'r';

            return
                SubGraphName[0] == 'c' &&
                SubGraphName[1] == 'l' &&
                SubGraphName[2] == 'u' &&
                SubGraphName[3] == 's' &&
                SubGraphName[4] == 't' &&
                SubGraphName[5] == 'e' &&
                SubGraphName[6] == 'r';
        }
    }

    public bool IsNonClusterSubGraph => !IsCluster;


    internal void AssertIsCluster()
    {
        if (IsCluster)
            return;

        throw new InvalidOperationException("This attribute can only be used for clusters");
    }

    internal DotSubGraph SetAttribute(string attrName, float attrValue)
    {
        StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue.ToDotDouble()));

        return this;
    }

    internal DotSubGraph SetAttribute(string attrName, string attrValue)
    {
        StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue));

        return this;
    }

    internal DotSubGraph SetAttribute(string attrName, bool attrValue)
    {
        StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue.ToString()));

        return this;
    }

    internal DotSubGraph SetAttribute(string attrName, int attrValue)
    {
        StatementsList.Add(new DotGraphAttribute(this, attrName, attrValue.ToString()));

        return this;
    }


    #region Attributes Accessors
    public DotSubGraph SetK(float value)
    {
        AssertIsCluster();

        SetAttribute("K", value);

        return this;
    }

    public DotSubGraph SetUrl(DotHtmlString value)
    {
        AssertIsCluster();

        SetAttribute("URL", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetArea(float value)
    {
        AssertIsCluster();

        SetAttribute("area", value);

        return this;
    }

    public DotSubGraph SetBackgroundColor(DotColor value)
    {
        AssertIsCluster();

        SetAttribute("bgcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetBackgroundColor(DotColorList value)
    {
        AssertIsCluster();

        SetAttribute("bgcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetColor(DotColor value)
    {
        AssertIsCluster();

        SetAttribute("color", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetColor(DotColorList value)
    {
        AssertIsCluster();

        SetAttribute("color", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetColorScheme(DotColorScheme value)
    {
        AssertIsCluster();

        SetAttribute("colorscheme", value.Value);

        return this;
    }

    public DotSubGraph SetFillColor(DotColor value)
    {
        AssertIsCluster();

        SetAttribute("fillcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetFillColor(DotColorList value)
    {
        AssertIsCluster();

        SetAttribute("fillcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetFontColor(DotColor value)
    {
        AssertIsCluster();

        SetAttribute("fontcolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetFontName(string value)
    {
        AssertIsCluster();

        SetAttribute("fontname", value.DoubleQuote());

        return this;
    }

    public DotSubGraph SetFontSize(float value)
    {
        AssertIsCluster();

        SetAttribute("fontsize", value);

        return this;
    }

    public DotSubGraph SetGradientAngle(int value)
    {
        AssertIsCluster();

        SetAttribute("gradientangle", value);

        return this;
    }

    public DotSubGraph SetHRef(string value)
    {
        SetAttribute("href", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraph SetId(DotHtmlString value)
    {
        AssertIsCluster();

        SetAttribute("id", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetTeXLabel(string value)
    {
        return SetAttribute("texlbl", value.ValueToQuotedLiteral());
    }

    public DotSubGraph SetLabel(string value)
    {
        AssertIsCluster();

        SetAttribute("label", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraph SetLabel(IDotHtmlLabel value)
    {
        AssertIsCluster();

        SetAttribute("label", value.TaggedValue);

        return this;
    }

    public DotSubGraph SetLabelJustification(DotJustification value)
    {
        AssertIsCluster();

        SetAttribute("labeljust", value.Value);

        return this;
    }

    public DotSubGraph SetLabelVerticalLocation(DotVerticalLocation value)
    {
        AssertIsCluster();

        SetAttribute("labelloc", value.Value);

        return this;
    }

    public DotSubGraph SetLayer(string value)
    {
        AssertIsCluster();

        SetAttribute("layer", value.ValueToQuotedLiteral());

        return this;
    }

    public DotSubGraph SetLabelHeight(float value)
    {
        AssertIsCluster();

        SetAttribute("lheight", value);

        return this;
    }

    public DotSubGraph SetLabelPosition(float x, float y)
    {
        AssertIsCluster();

        SetAttribute("lp", DotUtils.ToDotPoint(x, y));

        return this;
    }

    public DotSubGraph SetLabelPosition(float x, float y, float z)
    {
        AssertIsCluster();

        SetAttribute("lp", DotUtils.ToDotPoint(x, y, z));

        return this;
    }

    public DotSubGraph SetLabelWidth(float value)
    {
        AssertIsCluster();

        SetAttribute("lwidth", value);

        return this;
    }

    public DotSubGraph SetMargin(float value)
    {
        AssertIsCluster();

        SetAttribute("margin", value);

        return this;
    }

    public DotSubGraph SetMargin(float x, float y)
    {
        AssertIsCluster();

        SetAttribute("margin", DotUtils.ToDotPoint(x, y));

        return this;
    }

    public DotSubGraph SetNoJustify(bool value)
    {
        AssertIsCluster();

        SetAttribute("nojustify", value);

        return this;
    }

    public DotSubGraph SetOrdering(DotOrdering value)
    {
        SetAttribute("ordering", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetPenColor(DotColor value)
    {
        AssertIsCluster();

        SetAttribute("pencolor", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetPenColor(int value)
    {
        AssertIsCluster();

        SetAttribute("pencolor", value);

        return this;
    }

    public DotSubGraph SetPenWidth(float value)
    {
        AssertIsCluster();

        SetAttribute("penwidth", value);

        return this;
    }

    public DotSubGraph SetPeripheries(bool value)
    {
        AssertIsCluster();

        SetAttribute("peripheries", value ? "1" : "0");

        return this;
    }

    public DotSubGraph SetRank(DotRankType value)
    {
        SetAttribute("rank", value.Value);

        return this;
    }

    public DotSubGraph SetSortValue(int value)
    {
        AssertIsCluster();

        SetAttribute("sortv", value);

        return this;
    }

    public DotSubGraph SetStyle(DotClusterStyle value)
    {
        AssertIsCluster();

        SetAttribute("style", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetStyle(params DotClusterStyle[] values)
    {
        AssertIsCluster();

        SetAttribute("style", values.ToDotStyle());

        return this;
    }

    public DotSubGraph SetTarget(DotHtmlString value)
    {
        AssertIsCluster();

        SetAttribute("target", value.QuotedValue);

        return this;
    }

    public DotSubGraph SetToolTip(DotHtmlString value)
    {
        AssertIsCluster();

        SetAttribute("tooltip", value.QuotedValue);

        return this;
    }
    #endregion


    internal DotSubGraph(IDotGraph parentGraph, string name)
    {
        SubGraphName = name;

        ParentGraph = parentGraph;

        var mainGraph = ParentGraph as DotGraph;

        if (mainGraph != null)
        {
            MainGraph = mainGraph;

            return;
        }

        var subGraph = ParentGraph as DotSubGraph;

        if (subGraph != null)
            MainGraph = subGraph.MainGraph;

        else
            throw new NullReferenceException("Parent graph is null!");
    }


    internal DotNode FindOrDefineNode(string nodeName)
    {
        if (MainGraph.NodesCache.TryGetValue(nodeName, out var node))
        {
            StatementsList.Add(new DotNode(this, nodeName));

            return node;
        }

        node = new DotNode(this, nodeName);

        MainGraph.NodesCache.Add(nodeName, node);

        StatementsList.Add(node);

        return node;
    }
}