using System;
using System.Text;

namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Combibnations;

public sealed class PascalTriangleUInt32
{
    private readonly uint[][] _rows;


    public int RowsCount 
        => _rows.Length;

    public uint this[int n, int k]
    {
        get
        {
            if (n < 0 || k < 0)
                throw new IndexOutOfRangeException();

            if (k > n)
                return 0U;

            if (k > n / 2) 
                k = n - k;

            return _rows[n][k];
        }
    }

    public PascalTriangleUInt32(int maxRowIndex)
    {
        if (maxRowIndex < 0 || maxRowIndex > 34)
            throw new ArgumentOutOfRangeException($"{nameof(maxRowIndex)} must be between 0 and 34");

        _rows = new uint[maxRowIndex + 1][];

        ComputePascalTriangleRows(maxRowIndex);
    }

        
    private void ComputePascalTriangleRows(int maxRowIndex)
    {
        //First row of Pascal triangle
        _rows[0] = [1U];
            
        //Second row of Pascal triangle
        _rows[1] = [1U];

        //Remaining rows of Pascal triangle
        var prevRow = _rows[1];
        for (var i = 2; i <= maxRowIndex; i++)
        {
            var rowSize = i / 2 + 1;

            var row = new uint[rowSize];

            row[0] = 1U;
            row[1] = (uint)i;

            var value1 = (uint)(i - 1);
            for (var j = 2; j < rowSize; j++)
            {
                var value2 = j < prevRow.Length 
                    ? prevRow[j] 
                    : prevRow[prevRow.Length - 1];

                checked { row[j] = value1 + value2; }

                value1 = value2;
            }

            _rows[i] = row;

            prevRow = row;
        }
    }

    public override string ToString()
    {
        var composer = new StringBuilder();

        composer.AppendLine("{");

        for (var n = 0; n < RowsCount; n++)
        {
            composer.Append("   {");

            for (var k = 0; k <= n; k++)
            {
                composer.Append(this[n, k]);

                if (k < n)
                    composer.Append(", ");
            }

            composer.AppendLine("}");
        }

        composer.AppendLine("}");

        return composer.ToString();
    }
}