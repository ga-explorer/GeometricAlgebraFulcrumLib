namespace CodeComposerLib.MathML.Elements.Layout.Tabular
{
    public sealed class MathMlTable
        : MathMlLayoutListElement<MathMlTableRow>
    {
        public static MathMlTable Create()
        {
            return new MathMlTable();
        }

        public static MathMlTable Create(int rowsCount)
        {
            var table = new MathMlTable();

            while (rowsCount > 0)
            {
                table.ContentsList.Add(
                    new MathMlTableRow()
                );

                rowsCount--;
            }

            return table;
        }

        public static MathMlTable Create(int rowsCount, int columnsCount)
        {
            var table = new MathMlTable();

            while (rowsCount > 0)
            {
                table.ContentsList.Add(
                    MathMlTableRow.Create(columnsCount)
                );

                rowsCount--;
            }

            return table;
        }


        public override string XmlTagName 
            => "mtable";

        public MathMlTableCell this[int rowIndex, int colIndex]
        {
            get => ContentsList[rowIndex][colIndex];
            set => ContentsList[rowIndex][colIndex] = value;
        }

        internal MathMlTable()
        {
        }
    }
}