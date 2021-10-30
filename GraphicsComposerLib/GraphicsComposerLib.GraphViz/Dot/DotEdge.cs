using System;
using System.Collections.Generic;
using System.Linq;
using GraphicsComposerLib.GraphViz.Dot.Color;
using GraphicsComposerLib.GraphViz.Dot.Label;
using GraphicsComposerLib.GraphViz.Dot.Label.Text;
using GraphicsComposerLib.GraphViz.Dot.Value;
using TextComposerLib;

namespace GraphicsComposerLib.GraphViz.Dot
{
    /// <summary>
    /// This class represents an edge or edges sequence of the graph in the AST
    /// See  http://www.graphviz.org/content/dot-language 
    /// and http://www.graphviz.org/content/attrs for more details
    /// </summary>
    public sealed class DotEdge : IDotStatement
    {
        internal List<IDotEdgeSide> SidesList = new List<IDotEdgeSide>();

        internal readonly Dictionary<string, string> AttrValues = 
            new Dictionary<string, string>();


        public IDotGraph ParentGraph { get; }

        public DotGraph MainGraph => ParentGraph.MainGraph;

        /// <summary>
        /// True if this edge has attributes
        /// </summary>
        public bool HasAttributes => AttrValues.Count > 0;

        /// <summary>
        /// The sides of this edge
        /// </summary>
        public IEnumerable<IDotEdgeSide> Sides => SidesList;

        /// <summary>
        /// True if this is an actual edge definition not an edge defaults statement
        /// </summary>
        public bool IsEdge => SidesList.Count > 0;

        /// <summary>
        /// True if this is an edge defaults statement not an actual edge
        /// </summary>
        public bool IsEdgeDefaults => SidesList.Count == 0;

        #region Attribute Accessors
        public string Url
        {
            get { return AttrValues.GetAttribute("URL"); }
            set { AttrValues.SetAttribute("URL", value); }
        }

        public string ArrowHead
        {
            get { return AttrValues.GetAttribute("arrowhead"); }
            set { AttrValues.SetAttribute("arrowhead", value); }
        }

        public string ArrowSize
        {
            get { return AttrValues.GetAttribute("arrowsize"); }
            set { AttrValues.SetAttribute("arrowsize", value); }
        }

        public string ArrowTail
        {
            get { return AttrValues.GetAttribute("arrowtail"); }
            set { AttrValues.SetAttribute("arrowtail", value); }
        }

        public string Color
        {
            get { return AttrValues.GetAttribute("color"); }
            set { AttrValues.SetAttribute("color", value); }
        }

        public string ColorScheme
        {
            get { return AttrValues.GetAttribute("colorscheme"); }
            set { AttrValues.SetAttribute("colorscheme", value); }
        }

        public string Comment
        {
            get { return AttrValues.GetAttribute("comment"); }
            set { AttrValues.SetAttribute("comment", value); }
        }

        public string Constraint
        {
            get { return AttrValues.GetAttribute("constraint"); }
            set { AttrValues.SetAttribute("constraint", value); }
        }

        public string Decorate
        {
            get { return AttrValues.GetAttribute("decorate"); }
            set { AttrValues.SetAttribute("decorate", value); }
        }

        public string Direction
        {
            get { return AttrValues.GetAttribute("dir"); }
            set { AttrValues.SetAttribute("dir", value); }
        }

        public string EdgeUrl
        {
            get { return AttrValues.GetAttribute("edgeURL"); }
            set { AttrValues.SetAttribute("edgeURL", value); }
        }

        public string EdgeHRef
        {
            get { return AttrValues.GetAttribute("edgehref"); }
            set { AttrValues.SetAttribute("edgehref", value); }
        }

        public string EdgeTarget
        {
            get { return AttrValues.GetAttribute("edgetarget"); }
            set { AttrValues.SetAttribute("edgetarget", value); }
        }

        public string EdgeToolTip
        {
            get { return AttrValues.GetAttribute("edgetooltip"); }
            set { AttrValues.SetAttribute("edgetooltip", value); }
        }

        public string FillColor
        {
            get { return AttrValues.GetAttribute("fillcolor"); }
            set { AttrValues.SetAttribute("fillcolor", value); }
        }

        public string FontColor
        {
            get { return AttrValues.GetAttribute("fontcolor"); }
            set { AttrValues.SetAttribute("fontcolor", value); }
        }

        public string FontName
        {
            get { return AttrValues.GetAttribute("fontname"); }
            set { AttrValues.SetAttribute("fontname", value); }
        }

        public string FontSize
        {
            get { return AttrValues.GetAttribute("fontsize"); }
            set { AttrValues.SetAttribute("fontsize", value); }
        }

        public string HeadUrl
        {
            get { return AttrValues.GetAttribute("headURL"); }
            set { AttrValues.SetAttribute("headURL", value); }
        }

        public string HeadLabelPosition
        {
            get { return AttrValues.GetAttribute("head_lp"); }
            set { AttrValues.SetAttribute("head_lp", value); }
        }

        public string HeadClip
        {
            get { return AttrValues.GetAttribute("headclip"); }
            set { AttrValues.SetAttribute("headclip", value); }
        }

        public string HeadHRef
        {
            get { return AttrValues.GetAttribute("headhref"); }
            set { AttrValues.SetAttribute("headhref", value); }
        }

        public string HeadLabel
        {
            get { return AttrValues.GetAttribute("headlabel"); }
            set { AttrValues.SetAttribute("headlabel", value); }
        }

        public string HeadPort
        {
            get { return AttrValues.GetAttribute("headport"); }
            set { AttrValues.SetAttribute("headport", value); }
        }

        public string HeadTarget
        {
            get { return AttrValues.GetAttribute("headtarget"); }
            set { AttrValues.SetAttribute("headtarget", value); }
        }

        public string HeadToolTip
        {
            get { return AttrValues.GetAttribute("headtooltip"); }
            set { AttrValues.SetAttribute("headtooltip", value); }
        }

        public string HRef
        {
            get { return AttrValues.GetAttribute("href"); }
            set { AttrValues.SetAttribute("href", value); }
        }

        public string Id
        {
            get { return AttrValues.GetAttribute("id"); }
            set { AttrValues.SetAttribute("id", value); }
        }

        public string TeXLabel
        {
            get { return AttrValues.GetAttribute("texlbl"); }
            set { AttrValues.SetAttribute("texlbl", value); }
        }

        public string Label
        {
            get { return AttrValues.GetAttribute("label"); }
            set { AttrValues.SetAttribute("label", value); }
        }

        public string LabelUrl
        {
            get { return AttrValues.GetAttribute("labelURL"); }
            set { AttrValues.SetAttribute("labelURL", value); }
        }

        public string LabelAngle
        {
            get { return AttrValues.GetAttribute("labelangle"); }
            set { AttrValues.SetAttribute("labelangle", value); }
        }

        public string LabelDistance
        {
            get { return AttrValues.GetAttribute("labeldistance"); }
            set { AttrValues.SetAttribute("labeldistance", value); }
        }

        public string LabelFloat
        {
            get { return AttrValues.GetAttribute("labelfloat"); }
            set { AttrValues.SetAttribute("labelfloat", value); }
        }

        public string LabelFontColor
        {
            get { return AttrValues.GetAttribute("labelfontcolor"); }
            set { AttrValues.SetAttribute("labelfontcolor", value); }
        }

        public string LabelFontName
        {
            get { return AttrValues.GetAttribute("labelfontname"); }
            set { AttrValues.SetAttribute("labelfontname", value); }
        }

        public string LabelFontSize
        {
            get { return AttrValues.GetAttribute("labelfontsize"); }
            set { AttrValues.SetAttribute("labelfontsize", value); }
        }

        public string LabelHRef
        {
            get { return AttrValues.GetAttribute("labelhref"); }
            set { AttrValues.SetAttribute("labelhref", value); }
        }

        public string LabelTarget
        {
            get { return AttrValues.GetAttribute("labeltarget"); }
            set { AttrValues.SetAttribute("labeltarget", value); }
        }

        public string LabelTooltip
        {
            get { return AttrValues.GetAttribute("labeltooltip"); }
            set { AttrValues.SetAttribute("labeltooltip", value); }
        }

        public string Layer
        {
            get { return AttrValues.GetAttribute("layer"); }
            set { AttrValues.SetAttribute("layer", value); }
        }

        public string EdgeLength
        {
            get { return AttrValues.GetAttribute("len"); }
            set { AttrValues.SetAttribute("len", value); }
        }

        public string LogicalHead
        {
            get { return AttrValues.GetAttribute("lhead"); }
            set { AttrValues.SetAttribute("lhead", value); }
        }

        public string LabelPosition
        {
            get { return AttrValues.GetAttribute("lp"); }
            set { AttrValues.SetAttribute("lp", value); }
        }

        public string LogicalTail
        {
            get { return AttrValues.GetAttribute("ltail"); }
            set { AttrValues.SetAttribute("ltail", value); }
        }

        public string MinLength
        {
            get { return AttrValues.GetAttribute("minlen"); }
            set { AttrValues.SetAttribute("minlen", value); }
        }

        public string NoJustify
        {
            get { return AttrValues.GetAttribute("nojustify"); }
            set { AttrValues.SetAttribute("nojustify", value); }
        }

        public string PenWidth
        {
            get { return AttrValues.GetAttribute("penwidth"); }
            set { AttrValues.SetAttribute("penwidth", value); }
        }

        public string Pos
        {
            get { return AttrValues.GetAttribute("pos"); }
            set { AttrValues.SetAttribute("pos", value); }
        }

        public string SameHead
        {
            get { return AttrValues.GetAttribute("samehead"); }
            set { AttrValues.SetAttribute("samehead", value); }
        }

        public string SameTail
        {
            get { return AttrValues.GetAttribute("sametail"); }
            set { AttrValues.SetAttribute("sametail", value); }
        }

        public string ShowBoxes
        {
            get { return AttrValues.GetAttribute("showboxes"); }
            set { AttrValues.SetAttribute("showboxes", value); }
        }

        public string Style
        {
            get { return AttrValues.GetAttribute("style"); }
            set { AttrValues.SetAttribute("style", value); }
        }

        public string TailUrl
        {
            get { return AttrValues.GetAttribute("tailURL"); }
            set { AttrValues.SetAttribute("tailURL", value); }
        }

        public string TailLabelPosition
        {
            get { return AttrValues.GetAttribute("tail_lp"); }
            set { AttrValues.SetAttribute("tail_lp", value); }
        }

        public string TailClip
        {
            get { return AttrValues.GetAttribute("tailclip"); }
            set { AttrValues.SetAttribute("tailclip", value); }
        }

        public string TailHRef
        {
            get { return AttrValues.GetAttribute("tailhref"); }
            set { AttrValues.SetAttribute("tailhref", value); }
        }

        public string TailLabel
        {
            get { return AttrValues.GetAttribute("taillabel"); }
            set { AttrValues.SetAttribute("taillabel", value); }
        }

        public string TailPort
        {
            get { return AttrValues.GetAttribute("tailport"); }
            set { AttrValues.SetAttribute("tailport", value); }
        }

        public string TailTarget
        {
            get { return AttrValues.GetAttribute("tailtarget"); }
            set { AttrValues.SetAttribute("tailtarget", value); }
        }

        public string TailToolTip
        {
            get { return AttrValues.GetAttribute("tailtooltip"); }
            set { AttrValues.SetAttribute("tailtooltip", value); }
        }

        public string Target
        {
            get { return AttrValues.GetAttribute("target"); }
            set { AttrValues.SetAttribute("target", value); }
        }

        public string ToolTip
        {
            get { return AttrValues.GetAttribute("tooltip"); }
            set { AttrValues.SetAttribute("tooltip", value); }
        }

        public string Weight
        {
            get { return AttrValues.GetAttribute("weight"); }
            set { AttrValues.SetAttribute("weight", value); }
        }

        public string ExternalLabel
        {
            get { return AttrValues.GetAttribute("xlabel"); }
            set { AttrValues.SetAttribute("xlabel", value); }
        }

        public string ExternalLabelPosition
        {
            get { return AttrValues.GetAttribute("xlp"); }
            set { AttrValues.SetAttribute("xlp", value); }
        }


        public DotEdge SetUrl(DotHtmlString value)
        {
            AttrValues.SetAttribute("URL", value.QuotedValue);

            return this;
        }

        public DotEdge SetArrowHead(DotArrowType value)
        {
            AttrValues.SetAttribute("arrowhead", value.Value);

            return this;
        }

        public DotEdge SetArrowHead(params DotArrowType[] values)
        {
            AttrValues.SetAttribute("arrowhead", DotArrowType.ConcatArrows(values.Take(4)).DoubleQuote());

            return this;
        }

        public DotEdge SetArrowHead(IEnumerable<DotArrowType> values)
        {
            AttrValues.SetAttribute("arrowhead", DotArrowType.ConcatArrows(values.Take(4)).DoubleQuote());

            return this;
        }

        public DotEdge SetArrowSize(float value)
        {
            AttrValues.SetAttribute("arrowsize", value);

            return this;
        }

        public DotEdge SetArrowTail(DotArrowType value)
        {
            AttrValues.SetAttribute("arrowtail", value.Value);

            return this;
        }

        public DotEdge SetArrowTail(params DotArrowType[] values)
        {
            AttrValues.SetAttribute("arrowtail", DotArrowType.ConcatArrows(values.Take(4)).DoubleQuote());

            return this;
        }

        public DotEdge SetArrowTail(IEnumerable<DotArrowType> values)
        {
            AttrValues.SetAttribute("arrowtail", DotArrowType.ConcatArrows(values.Take(4)).DoubleQuote());

            return this;
        }

        public DotEdge SetColor(DotColor value)
        {
            AttrValues.SetAttribute("color", value.QuotedValue);

            return this;
        }

        public DotEdge SetColor(DotColorList value)
        {
            AttrValues.SetAttribute("color", value.QuotedValue);

            return this;
        }

        public DotEdge SetColorScheme(DotColorScheme value)
        {
            AttrValues.SetAttribute("colorscheme", value.Value);

            return this;
        }

        public DotEdge SetComment(string value)
        {
            AttrValues.SetAttribute("comment", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetConstraint(bool value)
        {
            AttrValues.SetAttribute("constraint", value);

            return this;
        }

        public DotEdge SetDecorate(bool value)
        {
            AttrValues.SetAttribute("decorate", value);

            return this;
        }

        public DotEdge SetDirection(DotEdgeDirection value)
        {
            AttrValues.SetAttribute("dir", value.Value);

            return this;
        }

        public DotEdge SetEdgeUrl(DotHtmlString value)
        {
            AttrValues.SetAttribute("edgeURL", value.QuotedValue);

            return this;
        }

        public DotEdge SetEdgeHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("edgehref", value.QuotedValue);

            return this;
        }

        public DotEdge SetEdgeTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("edgetarget", value.QuotedValue);

            return this;
        }

        public DotEdge SetEdgeToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("edgetooltip", value.QuotedValue);

            return this;
        }

        public DotEdge SetFillColor(DotColor value)
        {
            AttrValues.SetAttribute("fillcolor", value.QuotedValue);

            return this;
        }

        public DotEdge SetFillColor(DotColorList value)
        {
            AttrValues.SetAttribute("fillcolor", value.QuotedValue);

            return this;
        }

        public DotEdge SetFontColor(DotColor value)
        {
            AttrValues.SetAttribute("fontcolor", value.QuotedValue);

            return this;
        }

        public DotEdge SetFontColor(int value)
        {
            AttrValues.SetAttribute("fontcolor", value);

            return this;
        }

        public DotEdge SetFontName(string value)
        {
            AttrValues.SetAttribute("fontname", value.DoubleQuote());

            return this;
        }

        public DotEdge SetFontSize(float value)
        {
            AttrValues.SetAttribute("fontsize", value);

            return this;
        }

        public DotEdge SetHeadUrl(DotHtmlString value)
        {
            AttrValues.SetAttribute("headURL", value.QuotedValue);

            return this;
        }

        public DotEdge SetHeadClip(bool value)
        {
            AttrValues.SetAttribute("headclip", value);

            return this;
        }

        public DotEdge SetHeadHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("headhref", value.QuotedValue);

            return this;
        }

        public DotEdge SetHeadLabelPosition(float x, float y)
        {
            AttrValues.SetAttribute("head_lp", DotUtils.ToDotPoint(x, y));

            return this;
        }

        public DotEdge SetHeadLabelPosition(float x, float y, float z)
        {
            AttrValues.SetAttribute("head_lp", DotUtils.ToDotPoint(x, y, z));

            return this;
        }

        public DotEdge SetHeadLabel(string value)
        {
            AttrValues.SetAttribute("headlabel", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetHeadLabel(IDotHtmlLabel value)
        {
            AttrValues.SetAttribute("headlabel", value.TaggedValue);

            return this;
        }

        public DotEdge SetHeadPort(string port)
        {
            AttrValues.SetAttribute("headport", port.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetHeadPort(DotCompass compass)
        {
            AttrValues.SetAttribute("headport", compass.Value);

            return this;
        }

        public DotEdge SetHeadPort(string port, DotCompass compass)
        {
            if (String.IsNullOrEmpty(port) && ReferenceEquals(compass, null))
            {
                AttrValues.ClearAttribute("headport");

                return this;
            }

            if (String.IsNullOrEmpty(port))
            {
                AttrValues.SetAttribute("headport", compass.Value);

                return this;
            }

            if (ReferenceEquals(compass, null))
            {
                AttrValues.SetAttribute("headport", port.ValueToQuotedLiteral());

                return this;
            }

            AttrValues.SetAttribute("headport", (port + ":" + compass.Value).ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetHeadTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("headtarget", value.QuotedValue);

            return this;
        }

        public DotEdge SetHeadToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("headtooltip", value.QuotedValue);

            return this;
        }

        public DotEdge SetHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("href", value.QuotedValue);

            return this;
        }

        public DotEdge SetId(DotHtmlString value)
        {
            AttrValues.SetAttribute("id", value.QuotedValue);

            return this;
        }

        public DotEdge SetTeXLabel(string value)
        {
            AttrValues.SetAttribute("texlbl", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLabel(string value)
        {
            AttrValues.SetAttribute("label", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLabel(IDotHtmlLabel value)
        {
            AttrValues.SetAttribute("label", value.TaggedValue);

            return this;
        }

        public DotEdge SetLabelUrl(DotHtmlString value)
        {
            AttrValues.SetAttribute("labelURL", value.QuotedValue);

            return this;
        }

        public DotEdge SetLabelAngle(float value)
        {
            AttrValues.SetAttribute("labelangle", value);

            return this;
        }

        public DotEdge SetLabelDistance(float value)
        {
            AttrValues.SetAttribute("labeldistance", value);

            return this;
        }

        public DotEdge SetLabelFloat(bool value)
        {
            AttrValues.SetAttribute("labelfloat", value);

            return this;
        }

        public DotEdge SetLabelFontColor(DotColor value)
        {
            AttrValues.SetAttribute("labelfontcolor", value.QuotedValue);

            return this;
        }

        public DotEdge SetLabelFontName(string value)
        {
            AttrValues.SetAttribute("labelfontname", value.DoubleQuote());

            return this;
        }

        public DotEdge SetLabelFontSize(float value)
        {
            AttrValues.SetAttribute("labelfontsize", value);

            return this;
        }

        public DotEdge SetLabelHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("labelhref", value.QuotedValue);

            return this;
        }

        public DotEdge SetLabelTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("labeltarget", value.QuotedValue);

            return this;
        }

        public DotEdge SetLabelToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("labeltooltip", value.QuotedValue);

            return this;
        }

        public DotEdge SetLayer(string value)
        {
            AttrValues.SetAttribute("layer", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLength(float value)
        {
            AttrValues.SetAttribute("len", value);

            return this;
        }

        public DotEdge SetLogicalHead(string value)
        {
            AttrValues.SetAttribute("lhead", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLogicalHead(DotSubGraph value)
        {
            if (value.IsCluster == false)
                throw new InvalidOperationException("This can only be a cluster");

            AttrValues.SetAttribute("lhead", value.SubGraphName.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLogicalTail(string value)
        {
            AttrValues.SetAttribute("ltail", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLogicalTail(DotSubGraph value)
        {
            if (value.IsCluster == false)
                throw new InvalidOperationException("This can only be a cluster");

            AttrValues.SetAttribute("ltail", value.SubGraphName.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetLabelPosition(float x, float y)
        {
            AttrValues.SetAttribute("lp", DotUtils.ToDotPoint(x, y));

            return this;
        }

        public DotEdge SetLabelPosition(float x, float y, float z)
        {
            AttrValues.SetAttribute("lp", DotUtils.ToDotPoint(x, y, z));

            return this;
        }

        public DotEdge SetMinLength(int value)
        {
            AttrValues.SetAttribute("minlen", value);

            return this;
        }

        public DotEdge SetNoJustify(bool value)
        {
            AttrValues.SetAttribute("nojustify", value);

            return this;
        }

        public DotEdge SetPenWidth(float value)
        {
            AttrValues.SetAttribute("penwidth", value);

            return this;
        }

        public DotEdge SetPosition(float x, float y)
        {
            AttrValues.SetAttribute("pos", DotUtils.ToDotPoint(x, y));

            return this;
        }

        public DotEdge SetPosition(float x, float y, float z)
        {
            AttrValues.SetAttribute("pos", DotUtils.ToDotPoint(x, y, z));

            return this;
        }

        public DotEdge SetPosition(string value)
        {
            AttrValues.SetAttribute("pos", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetSameHead(string value)
        {
            AttrValues.SetAttribute("samehead", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetSameTail(string value)
        {
            AttrValues.SetAttribute("sametail", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetShowBoxes(int value)
        {
            AttrValues.SetAttribute("showboxes", value);

            return this;
        }

        public DotEdge SetStyle(DotEdgeStyle value)
        {
            AttrValues.SetAttribute("style", value.Value);

            return this;
        }

        public DotEdge SetTailUrl(DotHtmlString value)
        {
            AttrValues.SetAttribute("tailURL", value.QuotedValue);

            return this;
        }

        public DotEdge SetTailClip(bool value)
        {
            AttrValues.SetAttribute("tailclip", value);

            return this;
        }

        public DotEdge SetTailLabelPosition(float x, float y)
        {
            AttrValues.SetAttribute("tail_lp", DotUtils.ToDotPoint(x, y));

            return this;
        }

        public DotEdge SetTailLabelPosition(float x, float y, float z)
        {
            AttrValues.SetAttribute("tail_lp", DotUtils.ToDotPoint(x, y, z));

            return this;
        }

        public DotEdge SetTailHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("tailhref", value.QuotedValue);

            return this;
        }

        public DotEdge SetTailLabel(string value)
        {
            AttrValues.SetAttribute("taillabel", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetTailLabel(IDotHtmlLabel value)
        {
            AttrValues.SetAttribute("taillabel", value.TaggedValue);

            return this;
        }

        public DotEdge SetTailPort(string port)
        {
            AttrValues.SetAttribute("tailport", port.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetTailPort(DotCompass compass)
        {
            AttrValues.SetAttribute("tailport", compass.Value);

            return this;
        }

        public DotEdge SetTailPort(string port, DotCompass compass)
        {
            if (String.IsNullOrEmpty(port) && ReferenceEquals(compass, null))
            {
                AttrValues.ClearAttribute("tailport");

                return this;
            }

            if (String.IsNullOrEmpty(port))
            {
                AttrValues.SetAttribute("tailport", compass.Value);

                return this;
            }

            if (ReferenceEquals(compass, null))
            {
                AttrValues.SetAttribute("tailport", port.ValueToQuotedLiteral());

                return this;
            }

            AttrValues.SetAttribute("tailport", (port + ":" + compass.Value).ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetTailTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("tailtarget", value.QuotedValue);

            return this;
        }

        public DotEdge SetTailToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("tailtooltip", value.QuotedValue);

            return this;
        }

        public DotEdge SetTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("target", value.QuotedValue);

            return this;
        }

        public DotEdge SetToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("tooltip", value.QuotedValue);

            return this;
        }

        public DotEdge SetWeight(int value)
        {
            AttrValues.SetAttribute("weight", value);

            return this;
        }

        public DotEdge SetWeight(float value)
        {
            AttrValues.SetAttribute("weight", value);

            return this;
        }

        public DotEdge SetExternalLabel(string value)
        {
            AttrValues.SetAttribute("xlabel", value.ValueToQuotedLiteral());

            return this;
        }

        public DotEdge SetExternalLabel(IDotHtmlLabel value)
        {
            AttrValues.SetAttribute("xlabel", value.TaggedValue);

            return this;
        }

        public DotEdge SetExternalLabelPosition(float x, float y)
        {
            AttrValues.SetAttribute("xlp", DotUtils.ToDotPoint(x, y));

            return this;
        }

        public DotEdge SetExternalLabelPosition(float x, float y, float z)
        {
            AttrValues.SetAttribute("xlp", DotUtils.ToDotPoint(x, y, z));

            return this;
        }
        #endregion


        internal DotEdge(IDotGraph parentGraph)
        {
            ParentGraph = parentGraph;
        }
    }
}
