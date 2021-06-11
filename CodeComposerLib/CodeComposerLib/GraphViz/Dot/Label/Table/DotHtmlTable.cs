using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeComposerLib.GraphViz.Dot.Color;
using CodeComposerLib.GraphViz.Dot.Label.Text;
using CodeComposerLib.GraphViz.Dot.Value;
using TextComposerLib;
using TextComposerLib.Text;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.GraphViz.Dot.Label.Table
{
    /// <summary>
    /// This class represents an HTML table tag in the dot language
    /// See http://www.graphviz.org/content/node-shapes#html for more details
    /// </summary>
    public sealed class DotHtmlTable : DotHtmlTag, IDotHtmlLabel, IList<DotHtmlRow>
    {
        private readonly List<DotHtmlRow> _rows = new List<DotHtmlRow>();

        internal readonly Dictionary<string, string> AttrValues = new Dictionary<string, string>();

        /// <summary>
        /// The font tage of this HTML table
        /// </summary>
        public DotHtmlTagFont FontTag { get; set; }

        public override IEnumerable<KeyValuePair<string, string>> Attributes => AttrValues;

        public string Value
        {
            get
            {
                var composer = new LinearTextComposer();

                if (ReferenceEquals(FontTag, null) == false)
                    composer
                        .AppendAtNewLine("<")
                        .IncreaseIndentation()
                        .AppendLineAtNewLine(FontTag.TagOpenText)
                        .IncreaseIndentation();

                composer
                    .AppendLineAtNewLine(TagOpenText)
                    .IncreaseIndentation();

                foreach (var row in _rows)
                    composer.AppendAtNewLine(row.ToString());

                composer
                    .DecreaseIndentation()
                    .AppendLineAtNewLine(TagCloseText);

                if (ReferenceEquals(FontTag, null) == false)
                    composer
                        .DecreaseIndentation()
                        .AppendLineAtNewLine(FontTag.TagCloseText);

                return composer.ToString();
            }
        }

        public string QuotedValue => Value.DoubleQuote();

        public string TaggedValue => new StringBuilder()
            .Append('<')
            .Append(Value)
            .Append('>')
            .ToString();

        public string LiteralValue => Value.ValueToQuotedLiteral();

        /// <summary>
        /// The current minimun size of all rows in this HTML table
        /// </summary>
        public int MinRowSize
        {
            get { return _rows.Select(row => row.Count).Min(); }
        }

        /// <summary>
        /// The current maximum size of all rows in this HTML table
        /// </summary>
        public int MaxRowSize
        {
            get { return _rows.Select(row => row.Count).Max(); }
        }

        /// <summary>
        /// The first row of this HTML table
        /// </summary>
        public DotHtmlRow FirstRow => _rows.Count > 0 ? _rows[0] : null;

        /// <summary>
        /// The second row of this HTML table
        /// </summary>
        public DotHtmlRow SecondRow => _rows.Count > 1 ? _rows[1] : null;

        /// <summary>
        /// The third row of this HTML table
        /// </summary>
        public DotHtmlRow ThirdRow => _rows.Count > 2 ? _rows[2] : null;

        /// <summary>
        /// The last row of this HTML table
        /// </summary>
        public DotHtmlRow LastRow => _rows.Count > 0 ? _rows[_rows.Count - 1] : null;

        /// <summary>
        /// True if this table has the same number of cells for all its rows
        /// </summary>
        public bool IsUniformTable
        {
            get
            {
                if (_rows.Count < 2) return true;

                var size = _rows[0].Count;

                return _rows.Skip(1).Any(row => row.Count != size);
            }
        }

        /// <summary>
        /// Get all cells in this table row by row
        /// </summary>
        public IEnumerable<DotHtmlCell> Cells
        {
            get { return this.SelectMany(row => row); }
        }


        internal DotHtmlTable()
            : base("TABLE")
        {
        }


        #region Attribute Accesors
        //These are all the attributes of this object. You can assign or read a string value but is has to
        //be in its final form that will be written as-is in the dot code. 
        //If any value is null or "" (not "\"\"", this is accepted) it's removed from the final attributes
        //written to the code for this object
        // You can use the Set methods for simpler and more controlled attribute assignments.

        public string Align
        {
            get { return AttrValues.GetAttribute("ALIGN"); }
            set { AttrValues.SetAttribute("ALIGN", value); }
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

        public string CellBorder
        {
            get { return AttrValues.GetAttribute("CELLBORDER"); }
            set { AttrValues.SetAttribute("CELLBORDER", value); }
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

        public string Columns
        {
            get { return AttrValues.GetAttribute("COLUMNS"); }
            set { AttrValues.SetAttribute("COLUMNS", value); }
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

        public string Rows
        {
            get { return AttrValues.GetAttribute("ROWS"); }
            set { AttrValues.SetAttribute("ROWS", value); }
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


        public DotHtmlTable SetAlign(DotAlign value)
        {
            AttrValues.SetAttribute("ALIGN", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetBackgroundColor(DotColor c)
        {
            AttrValues.SetAttribute("BGCOLOR", c.QuotedValue);

            return this;
        }

        public DotHtmlTable SetBackgroundColor(DotColor c1, DotColor c2)
        {
            AttrValues.SetAttribute("BGCOLOR", (c1.Value + ":" + c2.Value).DoubleQuote());

            return this;
        }

        public DotHtmlTable SetBorder(int value)
        {
            AttrValues.SetAttribute("BORDER", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetCellBorder(int value)
        {
            AttrValues.SetAttribute("CELLBORDER", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetCellPadding(int value)
        {
            AttrValues.SetAttribute("CELLPADDING", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetCellSpacing(int value)
        {
            AttrValues.SetAttribute("CELLSPACING", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetColor(DotColor value)
        {
            AttrValues.SetAttribute("COLOR", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetColumnsStar()
        {
            AttrValues.SetAttribute("COLUMNS", "\"*\"");

            return this;
        }

        public DotHtmlTable SetFixedSize(bool value)
        {
            AttrValues.SetAttribute("FIXEDSIZE", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetGradientAngle(float value)
        {
            AttrValues.SetAttribute("GRADIENTANGLE", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetHeight(int value)
        {
            AttrValues.SetAttribute("HEIGHT", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetHRef(IDotValue value)
        {
            AttrValues.SetAttribute("HREF", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetId(DotHtmlString value)
        {
            AttrValues.SetAttribute("ID", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetPort(string value)
        {
            AttrValues.SetAttribute("PORT", value.DoubleQuote());

            return this;
        }

        public DotHtmlTable SetRowsStar()
        {
            AttrValues.SetAttribute("ROWS", "\"*\"");

            return this;
        }

        public DotHtmlTable SetSidesNone()
        {
            AttrValues.SetAttribute("SIDES", "\"\"");

            return this;
        }

        public DotHtmlTable SetSides(params DotSides[] value)
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

        public DotHtmlTable SetStyleRadial()
        {
            AttrValues.SetAttribute("STYLE", "\"RADIAL\"");

            return this;
        }

        public DotHtmlTable SetStyleRounded()
        {
            AttrValues.SetAttribute("STYLE", "\"ROUNDED\"");

            return this;
        }

        public DotHtmlTable SetStyleRoundedRadial()
        {
            AttrValues.SetAttribute("STYLE", "\"ROUNDED,RADIAL\"");

            return this;
        }

        public DotHtmlTable SetTarget(DotHtmlString value)
        {
            AttrValues.SetAttribute("TARGET", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetTitle(DotHtmlString value)
        {
            AttrValues.SetAttribute("TITLE", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetToolTip(DotHtmlString value)
        {
            AttrValues.SetAttribute("TOOLTIP", value.QuotedValue);

            return this;
        }

        public DotHtmlTable SetVerticalAlign(DotVerticalAlign value)
        {
            AttrValues.SetAttribute("VALIGN", value.QuotedValue.ToUpper());

            return this;
        }

        public DotHtmlTable SetWidth(int value)
        {
            AttrValues.SetAttribute("WIDTH", value.DoubleQuote());

            return this;
        }
        #endregion


        public DotHtmlTable ClearAttributes()
        {
            AttrValues.Clear();

            return this;
        }


        public DotHtmlRow AddRow()
        {
            var row = new DotHtmlRow();

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(int cellsCount)
        {
            var row = new DotHtmlRow().AddCells(cellsCount);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(DotHtmlRow row)
        {
            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(string text)
        {
            var row = new DotHtmlRow();

            row.AddCell(text);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(IDotHtmlLabel label)
        {
            var row = new DotHtmlRow();

            row.AddCell(label);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(string imageFilePath, DotNodeImageScale imageScaleMethod)
        {
            var row = new DotHtmlRow();

            row.AddCell(imageFilePath, imageScaleMethod);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(DotHtmlCellImage imageTag)
        {
            var row = new DotHtmlRow();

            row.AddCell(imageTag);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(DotHtmlCell cell)
        {
            var row = new DotHtmlRow();

            row.AddCell(cell);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(params string[] text)
        {
            var row = new DotHtmlRow().AddCells(text);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(IEnumerable<string> text)
        {
            var row = new DotHtmlRow().AddCells(text);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(params IDotHtmlLabel[] labels)
        {
            var row = new DotHtmlRow().AddCells(labels);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(IEnumerable<IDotHtmlLabel> labels)
        {
            var row = new DotHtmlRow().AddCells(labels);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(params DotHtmlCellImage[] imageTags)
        {
            var row = new DotHtmlRow().AddCells(imageTags);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(IEnumerable<DotHtmlCellImage> imageTags)
        {
            var row = new DotHtmlRow().AddCells(imageTags);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(params DotHtmlCell[] cells)
        {
            var row = new DotHtmlRow().AddCells(cells);

            Add(row);

            return row;
        }

        public DotHtmlRow AddRow(IEnumerable<DotHtmlCell> cells)
        {
            var row = new DotHtmlRow().AddCells(cells);

            Add(row);

            return row;
        }


        public DotHtmlTable AddRows(int rowsCount)
        {
            for (var i = 0; i < rowsCount; i++)
                Add(new DotHtmlRow());

            return this;
        }

        public DotHtmlTable AddRows(int rowsCount, int cellsCount)
        {
            for (var i = 0; i < rowsCount; i++)
                Add(new DotHtmlRow().AddCells(cellsCount));

            return this;
        }

        public DotHtmlTable AddRows(params DotHtmlRow[] rows)
        {
            foreach (var row in rows)
                Add(row);

            return this;
        }

        public DotHtmlTable AddRows(IEnumerable<DotHtmlRow> rows)
        {
            foreach (var row in rows)
                Add(row);

            return this;
        }

        public DotHtmlTable AddRows(params string[] text)
        {
            foreach (var s in text)
                AddRow(s);

            return this;
        }

        public DotHtmlTable AddRows(IEnumerable<string> text)
        {
            foreach (var s in text)
                AddRow(s);

            return this;
        }

        public DotHtmlTable AddRows(params IDotHtmlLabel[] labels)
        {
            foreach (var label in labels)
                AddRow(label);

            return this;
        }

        public DotHtmlTable AddRows(IEnumerable<IDotHtmlLabel> labels)
        {
            foreach (var label in labels)
                AddRow(label);

            return this;
        }

        public DotHtmlTable AddRows(params DotHtmlCellImage[] imageTags)
        {
            foreach (var imageTag in imageTags)
                AddRow(imageTag);

            return this;
        }

        public DotHtmlTable AddRows(IEnumerable<DotHtmlCellImage> imageTags)
        {
            foreach (var imageTag in imageTags)
                AddRow(imageTag);

            return this;
        }

        public DotHtmlTable AddRows(params DotHtmlCell[] cells)
        {
            foreach (var cell in cells)
                AddRow(cell);

            return this;
        }

        public DotHtmlTable AddRows(IEnumerable<DotHtmlCell> cells)
        {
            foreach (var cell in cells)
                AddRow(cell);

            return this;
        }



        public int IndexOf(DotHtmlRow item)
        {
            return _rows.IndexOf(item);
        }

        public void Insert(int index, DotHtmlRow item)
        {
            _rows.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _rows.RemoveAt(index);
        }

        public DotHtmlRow this[int index]
        {
            get
            {
                return _rows[index];
            }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException(nameof(value));

                _rows[index] = value;
            }
        }

        public void Add(DotHtmlRow item)
        {
            _rows.Add(item);
        }

        public void Clear()
        {
            _rows.Clear();
        }

        public bool Contains(DotHtmlRow item)
        {
            return _rows.Contains(item);
        }

        public void CopyTo(DotHtmlRow[] array, int arrayIndex)
        {
            _rows.CopyTo(array, arrayIndex);
        }

        public int Count => _rows.Count;

        public bool IsReadOnly => false;

        public bool Remove(DotHtmlRow item)
        {
            return _rows.Remove(item);
        }

        public IEnumerator<DotHtmlRow> GetEnumerator()
        {
            return _rows.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _rows.GetEnumerator();
        }


        public override string ToString()
        {
            return Value;
        }
    }
}
