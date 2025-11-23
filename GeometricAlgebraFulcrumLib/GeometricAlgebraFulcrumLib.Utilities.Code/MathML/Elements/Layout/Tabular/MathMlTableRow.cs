namespace GeometricAlgebraFulcrumLib.Utilities.Code.MathML.Elements.Layout.Tabular;

public sealed class MathMlTableRow
    : MathMlLayoutListElement<MathMlTableCell>
{
    public static MathMlTableRow Create()
    {
        return new MathMlTableRow();
    }

    public static MathMlTableRow Create(int cellsCount)
    {
        var row = new MathMlTableRow();

        while (cellsCount > 0)
        {
            row.ContentsList.Add(
                new MathMlTableCell()
            );

            cellsCount--;
        }

        return row;
    }

    public static IEnumerable<MathMlTableRow> CreateRows(int rowsCount)
    {
        while (rowsCount > 0)
        {
            yield return new MathMlTableRow();

            rowsCount--;
        }
    }

    public static IEnumerable<MathMlTableRow> CreateRows(int rowsCount, int cellsCount)
    {
        while (rowsCount > 0)
        {
            yield return Create(cellsCount);

            rowsCount--;
        }
    }


    public override string XmlTagName 
        => "mtr";


    internal MathMlTableRow()
    {
    }
}