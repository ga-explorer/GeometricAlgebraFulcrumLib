using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeComposerLib.GraphViz.Dot.Value;
using TextComposerLib.Text.Linear;

namespace CodeComposerLib.GraphViz.Dot.Label.Table
{
    /// <summary>
    /// This class represents a row inside an HTML table tag in the dot language
    /// See http://www.graphviz.org/content/node-shapes#html for more details
    /// </summary>
    public sealed class DotHtmlRow : DotHtmlTag, IList<DotHtmlCell>
    {
        private readonly List<DotHtmlCell> _cells = new List<DotHtmlCell>();


        /// <summary>
        /// Get or set the state of the horizontal rule for this row
        /// </summary>
        public bool HorizontalRule { get; set; }

        public override IEnumerable<KeyValuePair<string, string>> Attributes => Enumerable.Empty<KeyValuePair<string, string>>();

        public DotHtmlCell FirstCell => _cells.Count > 0 ? _cells[0] : null;

        public DotHtmlCell SecondCell => _cells.Count > 1 ? _cells[1] : null;

        public DotHtmlCell ThirdCell => _cells.Count > 2 ? _cells[2] : null;

        public DotHtmlCell LastCell => _cells.Count > 0 ? _cells[_cells.Count - 1] : null;


        internal DotHtmlRow()
            : base("TR")
        {
        }


        public DotHtmlCell AddCell()
        {
            var cell = new DotHtmlCell();

            Add(cell);

            return cell;
        }

        public DotHtmlCell AddCell(string text)
        {
            var cell = new DotHtmlCell().SetContents(text);

            Add(cell);

            return cell;
        }

        public DotHtmlCell AddCell(IDotHtmlLabel label)
        {
            var cell = new DotHtmlCell().SetContents(label);

            Add(cell);

            return cell;
        }

        public DotHtmlCell AddCell(string imageFilePath, DotNodeImageScale imageScaleMethod)
        {
            var cell = new DotHtmlCell().SetContents(imageFilePath, imageScaleMethod);

            Add(cell);

            return cell;
        }

        public DotHtmlCell AddCell(DotHtmlCellImage imageTag)
        {
            var cell = new DotHtmlCell().SetContents(imageTag);

            Add(cell);

            return cell;
        }

        public DotHtmlCell AddCell(DotHtmlCell cell)
        {
            Add(cell);

            return cell;
        }


        public DotHtmlRow AddCells(int cellsCount)
        {
            for (var i = 0; i < cellsCount; i++)
                Add(new DotHtmlCell());

            return this;
        }

        public DotHtmlRow AddCells(params string[] text)
        {
            foreach (var cellText in text)
                AddCell(cellText);

            return this;
        }

        public DotHtmlRow AddCells(IEnumerable<string> text)
        {
            foreach (var cellText in text)
                AddCell(cellText);

            return this;
        }

        public DotHtmlRow AddCells(params IDotHtmlLabel[] labels)
        {
            foreach (var label in labels)
                AddCell(label);

            return this;
        }

        public DotHtmlRow AddCells(IEnumerable<IDotHtmlLabel> labels)
        {
            foreach (var label in labels)
                AddCell(label);

            return this;
        }

        public DotHtmlRow AddCells(params DotHtmlCellImage[] imageTags)
        {
            foreach (var imageTag in imageTags)
                AddCell(imageTag);

            return this;
        }

        public DotHtmlRow AddCells(IEnumerable<DotHtmlCellImage> imageTags)
        {
            foreach (var imageTag in imageTags)
                AddCell(imageTag);

            return this;
        }

        public DotHtmlRow AddCells(params DotHtmlCell[] cells)
        {
            foreach (var cell in cells)
                Add(cell);

            return this;
        }

        public DotHtmlRow AddCells(IEnumerable<DotHtmlCell> cells)
        {
            foreach (var cell in cells)
                Add(cell);

            return this;
        }


        public int IndexOf(DotHtmlCell item)
        {
            return _cells.IndexOf(item);
        }

        public void Insert(int index, DotHtmlCell item)
        {
            _cells.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _cells.RemoveAt(index);
        }

        public DotHtmlCell this[int index]
        {
            get
            {
                return _cells[index];
            }
            set
            {
                if (ReferenceEquals(value, null))
                    throw new ArgumentNullException(nameof(value));

                _cells[index] = value;
            }
        }

        public void Add(DotHtmlCell item)
        {
            _cells.Add(item);
        }

        public void Clear()
        {
            _cells.Clear();
        }

        public bool Contains(DotHtmlCell item)
        {
            return _cells.Contains(item);
        }

        public void CopyTo(DotHtmlCell[] array, int arrayIndex)
        {
            _cells.CopyTo(array, arrayIndex);
        }

        public int Count => _cells.Count;

        public bool IsReadOnly => false;

        public bool Remove(DotHtmlCell item)
        {
            return _cells.Remove(item);
        }

        public IEnumerator<DotHtmlCell> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }


        public override string ToString()
        {
            var composer = new LinearTextComposer();

            composer
                .AppendLineAtNewLine(@"<TR>")
                .IncreaseIndentation();

            foreach (var cell in _cells)
                composer.AppendLineAtNewLine(cell.ToString());

            composer
                .DecreaseIndentation()
                .AppendAtNewLine(@"</TR>");

            return composer.ToString();
        }
    }
}
