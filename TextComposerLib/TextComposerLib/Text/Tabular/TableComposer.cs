using System;
using System.Linq;
using TextComposerLib.Text.Linear;

namespace TextComposerLib.Text.Tabular
{
    //TODO: Complete this
    public sealed class TableComposer
    {

        public TableComposerRow[] RowsInfo { get; }

        public TableComposerColumn[] ColumnsInfo { get; }

        public string[,] Items { get; }

        public bool RenderByRows { get; set; }


        public int Rows => RowsInfo.Length;

        public int Columns => ColumnsInfo.Length;

        public bool RenderByColumns 
        {
            get { return !RenderByRows; }
            set { RenderByRows = !value; } 
        }

        public string ColumnSeparator { get; set; }

        public string RowSeparator { get; set; }

        public bool EqualColumnWidths { get; set; }

        //public int TableHeight { get; private set; }

        //public int TableWidth { get; private set; }


        public TableComposer(int rows, int columns)
        {
            Items = new string[rows, columns];

            RowsInfo = new TableComposerRow[rows];
            for (var i = 0; i < rows; i++)
            {
                RowsInfo[i] = new TableComposerRow();
            }

            ColumnsInfo = new TableComposerColumn[columns];
            for (var i = 0; i < columns; i++)
            {
                ColumnsInfo[i] = new TableComposerColumn();
            }
        }


        private string FormatItem(string itemText, int itemWidth)
        {
            return (itemText ?? "").PadRight(itemWidth + 1);
        }

        //private void ComputeTableSize()
        //{


        //}

        public override string ToString()
        {
            var rowHeadersWidth = RowsInfo.Max(r => r.Header.Length);
            var colHeadersWidth = ColumnsInfo.Max(c => c.Header.Length);

            var maxItemWidth = Math.Max(rowHeadersWidth, colHeadersWidth);
            for (var c = 0; c < Columns; c++)
                for (var r = 0; r < Rows; r++)
                {
                    var item = Items[r, c];
                    if (!string.IsNullOrEmpty(item) && maxItemWidth < item.Length)
                        maxItemWidth = item.Length;
                }

            var composer = new LinearTextComposer();

            //Add column headers
            composer.Append(FormatItem("", maxItemWidth));

            for (var c = 0; c < Columns; c++)
                composer.Append(FormatItem(ColumnsInfo[c].Header, maxItemWidth));

            composer.AppendLine();

            //Add rows
            for (var r = 0; r < Rows; r++)
            {
                composer.Append(FormatItem(RowsInfo[r].Header, maxItemWidth));

                for (var c = 0; c < Columns; c++)
                    composer.Append(FormatItem(Items[r, c], maxItemWidth));

                composer.AppendLine();
            }

            return composer.ToString();
        }
    }
}
