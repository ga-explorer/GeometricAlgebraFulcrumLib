using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TextComposerLib.Text.Columns
{
    public sealed class TextColumnsComposer : IEnumerable<TextColumn>
    {
        internal class TextColumnsDataSpecs
        {
            private static string[][] ColumnDataToRowsOfLines(IEnumerable<string> columnData)
            {
                return new List<string[]>(columnData.Select(rowData => rowData.SplitLines())).ToArray();
            }

            private static string[][][] ColumnsDataToRowsOfLines(IEnumerable<List<string>> columnsData)
            {
                return new List<string[][]>(columnsData.Select(ColumnDataToRowsOfLines)).ToArray();
            }


            public int ColumnsCount { get; }

            public int RowsCount { get; }

            public int[] ColumnWidths { get; }

            public int[] RowHeights { get; }

            public string[][][] LinesMatrix { get; }


            public TextColumnsDataSpecs(TextColumn[] columnsData)
            {
                LinesMatrix = ColumnsDataToRowsOfLines(columnsData);

                ColumnsCount = columnsData.Length;
                RowsCount = columnsData.Max(c => c.Count);
                ColumnWidths = new int[ColumnsCount];
                RowHeights = new int[RowsCount];

                FillSizeArrays();
            }


            private void FillSizeArrays()
            {
                for (var colIndex = 0; colIndex < ColumnWidths.Length; colIndex++)
                {
                    var curColumn = LinesMatrix[colIndex];

                    ColumnWidths[colIndex] =
                        curColumn
                            .SelectMany(t => t.AsEnumerable())
                            .Max(t => t.Length);

                    for (var rowIndex = 0; rowIndex < curColumn.Length; rowIndex++)
                    {
                        var curRowHeight = curColumn[rowIndex].Length;

                        if (curRowHeight > RowHeights[rowIndex])
                            RowHeights[rowIndex] = curRowHeight;
                    }
                }
            }
        }



        private TextColumn[] _columnsData;

        private readonly TextColumnSpecs _defaultColumnSpecs;

        private readonly TextRowSpecs _defaultRowSpecs;

        private readonly Dictionary<int, TextColumnSpecs> _columnSpecsDictionary =
            new Dictionary<int, TextColumnSpecs>();

        private readonly Dictionary<int, TextRowSpecs> _rowSpecsDictionary =
            new Dictionary<int, TextRowSpecs>();


        public string ColumnSeparator { get; set; }

        public TextColumnAlignment DefaultColumnAlignment
        {
            get { return _defaultColumnSpecs.Alignment; }
            set { _defaultColumnSpecs.Alignment = value; }
        }

        public TextRowAlignment DefaultRowAlignment
        {
            get { return _defaultRowSpecs.Alignment; }
            set { _defaultRowSpecs.Alignment = value; }
        }

        public int ColumnsCount => _columnsData.Length;

        public int RowsCount
        {
            get { return _columnsData.Max(c => c.Count); }
        }

        public string this[int rowIndex, int columnIndex]
        {
            get
            {
                var column = _columnsData[columnIndex];

                if (rowIndex < 0 || rowIndex > column.Count)
                    return string.Empty;

                return column[rowIndex];
            }
            set
            {
                var column = _columnsData[columnIndex];

                if (rowIndex < 0)
                    throw new IndexOutOfRangeException();

                if (rowIndex < column.Count)
                    column[rowIndex] = value ?? string.Empty;

                else if (rowIndex == column.Count)
                    column.Add(value ?? string.Empty);

                else
                {
                    column.AddRange(
                        Enumerable.Repeat(string.Empty, rowIndex - column.Count)
                        );

                    column.Add(value ?? string.Empty);
                }
            }
        }


        public TextColumnsComposer(int columnsCount)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsCount);
        }

        public TextColumnsComposer(string[,] columnsData)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsData);
        }

        public TextColumnsComposer(string[][] columnsData)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsData);
        }

        public TextColumnsComposer(List<string[]> columnsData)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsData);
        }

        public TextColumnsComposer(List<string>[] columnsData)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsData);
        }

        public TextColumnsComposer(IEnumerable<string> columnsData, int columnsCount)
        {
            _defaultColumnSpecs = new TextColumnSpecs();
            _defaultRowSpecs = new TextRowSpecs();

            Reset(columnsData, columnsCount);
        }


        /// <summary>
        /// Clear the data in this composer and keep the current number of columns
        /// </summary>
        /// <returns></returns>
        public TextColumnsComposer Reset()
        {
            return Reset(_columnsData.Length);
        }

        /// <summary>
        /// Clear the data in this composer and reset its columns to the given number
        /// </summary>
        /// <param name="columnsCount"></param>
        /// <returns></returns>
        public TextColumnsComposer Reset(int columnsCount)
        {
            _columnsData = new TextColumn[columnsCount];

            for (var i = 0; i < columnsCount; i++)
                _columnsData[i] = new TextColumn();

            return this;
        }

        /// <summary>
        /// Reset the data in this composer using a normal 2D array of strings.
        /// The first index of the input array is for the row and the second for the column
        /// </summary>
        /// <param name="columnsData"></param>
        /// <returns></returns>
        public TextColumnsComposer Reset(string[,] columnsData)
        {
            var rowsCount = columnsData.GetUpperBound(0) + 1;
            var columnsCount = columnsData.GetUpperBound(1) + 1;

            Reset(columnsCount);

            for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
                for (var rowIndex = 0; rowIndex < rowsCount; rowIndex++)
                    _columnsData[columnIndex].Add(columnsData[rowIndex, columnIndex] ?? string.Empty);

            return this;
        }

        /// <summary>
        /// Reset the data of this composer using an array of columns. Each column is an array of strings
        /// in the rows of the column.
        /// </summary>
        /// <param name="columnsData"></param>
        /// <returns></returns>
        public TextColumnsComposer Reset(string[][] columnsData)
        {
            var columnsCount = columnsData.Length;

            Reset(columnsCount);

            for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
            {
                var columnData = columnsData[columnIndex];

                if (columnData != null && columnData.Length > 0)
                    _columnsData[columnIndex].AddRange(columnData);
            }

            return this;
        }

        /// <summary>
        /// Reset the data of this composer using a list of columns. Each column is an array of strings
        /// in the rows of the column.
        /// </summary>
        /// <param name="columnsData"></param>
        /// <returns></returns>
        public TextColumnsComposer Reset(List<string[]> columnsData)
        {
            var columnsCount = columnsData.Count;

            Reset(columnsCount);

            for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
            {
                var columnData = columnsData[columnIndex];

                if (columnData != null && columnData.Length > 0)
                    _columnsData[columnIndex].AddRange(columnData);
            }

            return this;
        }

        /// <summary>
        /// Reset the data of this composer using a list of columns. Each column is an array of strings
        /// in the rows of the column.
        /// </summary>
        /// <param name="columnsData"></param>
        /// <returns></returns>
        public TextColumnsComposer Reset(params List<string>[] columnsData)
        {
            var columnsCount = columnsData.Length;

            Reset(columnsCount);

            for (var columnIndex = 0; columnIndex < columnsCount; columnIndex++)
            {
                var columnData = columnsData[columnIndex];

                if (columnData != null && columnData.Count > 0)
                    _columnsData[columnIndex].AddRange(columnData);
            }

            return this;
        }

        public TextColumnsComposer Reset(IEnumerable<string> columnsData, int columnsCount)
        {
            var textArray = new List<string>[columnsCount];

            for (var i = 0; i < columnsCount; i++)
                textArray[i] = new List<string>();

            var col = 0;
            foreach (var value in columnsData)
            {
                textArray[col].Add(value);

                col++;
                if (col >= columnsCount) col = 0;
            }

            return Reset(textArray);
        }


        public TextColumn GetTextColumn(int columnIndex)
        {
            return _columnsData[columnIndex];
        }

        /// <summary>
        /// Add the given strings to this composer as the last item in each column
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        public TextColumnsComposer AppendToColumns(params string[] rowData)
        {
            var columnIndex = 0;

            foreach (var text in rowData)
            {
                if (columnIndex >= ColumnsCount) break;

                _columnsData[columnIndex++].Add(text);
            }

            return this;
        }

        /// <summary>
        /// Add the given strings to this composer as the last item in each column
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        public TextColumnsComposer AppendToColumns(IEnumerable<string> rowData)
        {
            var columnIndex = 0;

            foreach (var text in rowData)
            {
                if (columnIndex >= ColumnsCount) break;

                _columnsData[columnIndex++].Add(text);
            }

            return this;
        }

        public TextColumnsComposer AppendEmptyStringsToColumns(int count = 1)
        {
            if (count == 1)
            {
                foreach (var column in _columnsData)
                    column.Add(string.Empty);

                return this;
            }

            foreach (var column in _columnsData)
                column.AddRange(Enumerable.Repeat(string.Empty, count));

            return this;
        }

        public TextColumnsComposer AddAsLastRow(params string[] rowData)
        {
            var rowIndex = RowsCount;
            var columnIndex = 0;

            foreach (var text in rowData)
            {
                if (columnIndex >= ColumnsCount) break;

                this[rowIndex, columnIndex] = text;

                columnIndex++;
            }

            return this;
        }

        public TextColumnsComposer AddAsLastRow(IEnumerable<string> rowData)
        {
            var rowIndex = RowsCount;
            var columnIndex = 0;

            foreach (var text in rowData)
            {
                if (columnIndex >= ColumnsCount) break;

                this[rowIndex, columnIndex] = text;

                columnIndex++;
            }

            return this;
        }


        public TextColumnAlignment GetColumnAlignment(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _columnsData.Length)
                throw new IndexOutOfRangeException();

            return 
                _columnSpecsDictionary.TryGetValue(columnIndex, out var columnSpecs) 
                ? columnSpecs.Alignment 
                : _defaultColumnSpecs.Alignment;
        }

        public TextRowAlignment GetRowAlignment(int rowIndex)
        {
            if (rowIndex < 0)
                throw new IndexOutOfRangeException();

            return
                _rowSpecsDictionary.TryGetValue(rowIndex, out var rowSpecs)
                ? rowSpecs.Alignment
                : _defaultRowSpecs.Alignment;
        }


        public TextColumnsComposer SetColumnAlignment(TextColumnAlignment alignment, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= _columnsData.Length)
                throw new IndexOutOfRangeException();

            if (_columnSpecsDictionary.TryGetValue(columnIndex, out var columnSpecs) == false)
            {
                columnSpecs = new TextColumnSpecs();
                _columnSpecsDictionary.Add(columnIndex, columnSpecs);
            }

            columnSpecs.Alignment = alignment;

            return this;
        }

        public TextColumnsComposer SetColumnAlignment(TextColumnAlignment alignment, params int[] columnIndexList)
        {
            foreach (var columnIndex in columnIndexList)
                SetColumnAlignment(alignment, columnIndex);

            return this;
        }

        public TextColumnsComposer SetRowAlignment(TextRowAlignment alignment, int rowIndex)
        {
            if (rowIndex < 0)
                throw new IndexOutOfRangeException();

            if (_rowSpecsDictionary.TryGetValue(rowIndex, out var rowSpecs) == false)
            {
                rowSpecs = new TextRowSpecs();
                _rowSpecsDictionary.Add(rowIndex, rowSpecs);
            }

            rowSpecs.Alignment = alignment;

            return this;
        }

        public TextColumnsComposer SetRowAlignment(TextRowAlignment alignment, params int[] rowIndexList)
        {
            foreach (var rowIndex in rowIndexList)
                SetRowAlignment(alignment, rowIndex);

            return this;
        }


        private string[] GetTextLineParts(TextColumnsDataSpecs specs, int rowIndex, int lineIndex)
        {
            var rowTextArray = new string[ColumnsCount];

            for (var columnIndex = 0; columnIndex < ColumnsCount; columnIndex++)
            {
                var columnData = specs.LinesMatrix[columnIndex];
                var columnAlignment = GetColumnAlignment(columnIndex);
                var columnWidth = specs.ColumnWidths[columnIndex];

                if (rowIndex >= columnData.Length)
                {
                    rowTextArray[columnIndex] = new string(' ', columnWidth);
                    continue;
                }

                var rowData = columnData[rowIndex];
                var rowAlignment = GetRowAlignment(rowIndex);

                if (
                    (rowAlignment == TextRowAlignment.Top && lineIndex >= rowData.Length) ||
                    (rowAlignment == TextRowAlignment.Bottom && lineIndex < specs.RowHeights[rowIndex] - rowData.Length)
                    )
                {
                    rowTextArray[columnIndex] = new string(' ', columnWidth);

                    continue;
                }

                var lineText =
                    rowAlignment == TextRowAlignment.Top
                    ? rowData[lineIndex]
                    : rowData[lineIndex - (specs.RowHeights[rowIndex] - rowData.Length)];

                rowTextArray[columnIndex] =
                    columnAlignment == TextColumnAlignment.Right
                        ? lineText.PadLeft(columnWidth)
                        : lineText.PadRight(columnWidth);
            }

            return rowTextArray;
        }

        public string GenerateText()
        {
            var tableSpecs = new TextColumnsDataSpecs(_columnsData);

            var s = new StringBuilder();

            for (var rowIndex = 0; rowIndex < tableSpecs.RowsCount; rowIndex++)
            {
                var rowHeight = tableSpecs.RowHeights[rowIndex];

                for (var lineIndex = 0; lineIndex < rowHeight; lineIndex++)
                    s.AppendLine(
                        GetTextLineParts(tableSpecs, rowIndex, lineIndex)
                        .Concatenate(ColumnSeparator ?? string.Empty)
                        );
            }

            s.Length -= Environment.NewLine.Length;

            return s.ToString();
        }

        public IEnumerator<TextColumn> GetEnumerator()
        {
            return _columnsData.AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _columnsData.GetEnumerator();
        }


        public override string ToString()
        {
            return GenerateText();
        }
    }
}
