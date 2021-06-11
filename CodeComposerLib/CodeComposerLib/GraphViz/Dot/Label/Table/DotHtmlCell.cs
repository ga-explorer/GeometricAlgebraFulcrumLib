using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Label.Text;
using CodeComposerLib.GraphViz.Dot.Value;
using TextComposerLib;
using TextComposerLib.Text;

namespace CodeComposerLib.GraphViz.Dot.Label.Table
{
    /// <summary>
    /// This class represents a cell inside an HTML table tag in the dot language
    /// See http://www.graphviz.org/content/node-shapes#html for more details
    /// </summary>
    public sealed class DotHtmlCell : DotHtmlTag
    {
        internal readonly Dictionary<string, string> AttrValues = new Dictionary<string, string>();


        /// <summary>
        /// Get or set the state of the vertical rule for this cell
        /// </summary>
        public bool VerticalRule { get; set; }

        /// <summary>
        /// The contents of this cell
        /// </summary>
        public IDotHtmlCellContents Contents { get; private set; }

        public override IEnumerable<KeyValuePair<string, string>> Attributes => AttrValues;


        public string Align
        {
            get { return AttrValues.GetAttribute("ALIGN"); }
            set { AttrValues.SetAttribute("ALIGN", value); }
        }

        public string LineBreakAlign
        {
            get { return AttrValues.GetAttribute("BALIGN"); }
            set { AttrValues.SetAttribute("BALIGN", value); }
        }

        public string BackgroundColor
        {
            get { return AttrValues.GetAttribute("BGCOLOR"); }
            set { AttrValues.SetAttribute("BGCOLOR", value); }
        }

        public string Border
        {
            get { return AttrValues.GetAttribute("BORDER"); }
            set { AttrValues.SetAttribute("BORDER", value); }
        }

        public string CellPadding
        {
            get { return AttrValues.GetAttribute("CELLPADDING"); }
            set { AttrValues.SetAttribute("CELLPADDING", value); }
        }

        public string CellSpacing
        {
            get { return AttrValues.GetAttribute("CELLSPACING"); }
            set { AttrValues.SetAttribute("CELLSPACING", value); }
        }

        public string Color
        {
            get { return AttrValues.GetAttribute("COLOR"); }
            set { AttrValues.SetAttribute("COLOR", value); }
        }

        public string ColumnSpan
        {
            get { return AttrValues.GetAttribute("COLSPAN"); }
            set { AttrValues.SetAttribute("COLSPAN", value); }
        }

        public string FixedSize
        {
            get { return AttrValues.GetAttribute("FIXEDSIZE"); }
            set { AttrValues.SetAttribute("FIXEDSIZE", value); }
        }

        public string GradientAngle
        {
            get { return AttrValues.GetAttribute("GRADIENTANGLE"); }
            set { AttrValues.SetAttribute("GRADIENTANGLE", value); }
        }

        public string Height
        {
            get { return AttrValues.GetAttribute("HEIGHT"); }
            set { AttrValues.SetAttribute("HEIGHT", value); }
        }

        public string HRef
        {
            get { return AttrValues.GetAttribute("HREF"); }
            set { AttrValues.SetAttribute("HREF", value); }
        }

        public string Id
        {
            get { return AttrValues.GetAttribute("ID"); }
            set { AttrValues.SetAttribute("ID", value); }
        }

        public string Port
        {
            get { return AttrValues.GetAttribute("PORT"); }
            set { AttrValues.SetAttribute("PORT", value); }
        }

        public string RowSpan
        {
            get { return AttrValues.GetAttribute("ROWSPAN"); }
            set { AttrValues.SetAttribute("ROWSPAN", value); }
        }

        public string Sides
        {
            get { return AttrValues.GetAttribute("SIDES"); }
            set { AttrValues.SetAttribute("SIDES", value); }
        }

        public string Style
        {
            get { return AttrValues.GetAttribute("STYLE"); }
            set { AttrValues.SetAttribute("STYLE", value); }
        }

        public string Target
        {
            get { return AttrValues.GetAttribute("TARGET"); }
            set { AttrValues.SetAttribute("TARGET", value); }
        }

        public string Title
        {
            get { return AttrValues.GetAttribute("TITLE"); }
            set { AttrValues.SetAttribute("TITLE", value); }
        }

        public string ToolTip
        {
            get { return AttrValues.GetAttribute("TOOLTIP"); }
            set { AttrValues.SetAttribute("TOOLTIP", value); }
        }

        public string VerticalAlign
        {
            get { return AttrValues.GetAttribute("VALIGN"); }
            set { AttrValues.SetAttribute("VALIGN", value); }
        }

        public string Width
        {
            get { return AttrValues.GetAttribute("WIDTH"); }
            set { AttrValues.SetAttribute("WIDTH", value); }
        }

        
        internal DotHtmlCell()
            : base("TD")
        {
        }


        public DotHtmlCell SetAlign(DotAlign value)
        {
            AttrValues.SetAttribute("ALIGN", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetAlignText()
        {
            AttrValues.SetAttribute("ALIGN", "\"TEXT\"");

            return this;
        }

        public DotHtmlCell SetLineBreakAlign(DotAlign value)
        {
            AttrValues.SetAttribute("BALIGN", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetBackgroundColor(DotColor value)
        {
            AttrValues.SetAttribute("BGCOLOR", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetBackgroundColor(DotColor c1, DotColor c2)
        {
            AttrValues.SetAttribute("BGCOLOR", (c1.Value + ":" + c2.Value).DoubleQuote());

            return this;
        }

        public DotHtmlCell SetBorder(int value)
        {
            AttrValues.SetAttribute("BORDER", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetCellPadding(int value)
        {
            AttrValues.SetAttribute("CELLPADDING", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetCellSpacing(int value)
        {
            AttrValues.SetAttribute("CELLSPACING", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetColor(DotColor value)
        {
            AttrValues.SetAttribute("COLOR", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetColumnSpan(int value)
        {
            AttrValues.SetAttribute("COLSPAN", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetFixedSize(bool value)
        {
            AttrValues.SetAttribute("FIXEDSIZE", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetGradientAngle(float value)
        {
            AttrValues.SetAttribute("GRADIENTANGLE", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetHeight(int value)
        {
            AttrValues.SetAttribute("HEIGHT", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetHRef(DotHtmlString value)
        {
            AttrValues.SetAttribute("HREF", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetId(DotHtmlString value)
        {
            AttrValues.SetAttribute("ID", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetPort(string value)
        {
            AttrValues.SetAttribute("PORT", value.ValueToQuotedLiteral());

            return this;
        }

        public DotHtmlCell SetRowSpan(int value)
        {
            AttrValues.SetAttribute("ROWSPAN", value.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetSidesAll()
        {
            AttrValues.SetAttribute("SIDES", "\"LRTB\"");

            return this;
        }

        public DotHtmlCell SetSides(params DotSides[] value)
        {
            var sidesValue =
                value
                    .Select(v => v.Value)
                    .Distinct()
                    .Take(4)
                    .Concatenate();

            AttrValues.SetAttribute("SIDES", sidesValue.DoubleQuote());

            return this;
        }

        public DotHtmlCell SetStyleRadial()
        {
            AttrValues.SetAttribute("STYLE", "\"RADIAL\"");

            return this;
        }

        public DotHtmlCell SetTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("TARGET", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetTitle(DotHtmlString value)
        {
            AttrValues.SetAttribute("TITLE", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("TOOLTIP", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetVerticalAlign(DotAlign value)
        {
            AttrValues.SetAttribute("VALIGN", value.QuotedValue);

            return this;
        }

        public DotHtmlCell SetWidth(int value)
        {
            AttrValues.SetAttribute("WIDTH", value.DoubleQuote());

            return this;
        }


        public DotHtmlCell SetContents(string text)
        {
            Contents = text.ToHtmlString();

            return this;
        }

        public DotHtmlCell SetContents(IDotHtmlLabel label)
        {
            Contents = label;

            return this;
        }

        public DotHtmlCell SetContents(string imageFilePath, DotNodeImageScale imageScaleMethod)
        {
            Contents = 
                new DotHtmlCellImage()
                .SetImageSource(imageFilePath)
                .SetImageScale(imageScaleMethod);

            return this;
        }

        public DotHtmlCell SetContents(DotHtmlCellImage imageTag)
        {
            Contents = imageTag;

            return this;
        }


        public override string ToString()
        {
            return 
                new StringBuilder()
                .Append(VerticalRule ? @"<VR/>" : "")
                .Append(TagOpenText)
                .Append(Contents)
                .Append(TagCloseText)
                .ToString();
        }
    }
}
