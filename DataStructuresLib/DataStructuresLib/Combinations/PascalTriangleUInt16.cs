using System;
using System.Text;

namespace DataStructuresLib.Combinations
{
    public sealed class PascalTriangleUInt16
    {
        private readonly ushort[][] _rows;


        public int RowsCount 
            => _rows.Length;

        public ushort this[int n, int k]
        {
            get
            {
                if (n < 0 || k < 0)
                    throw new IndexOutOfRangeException();

                if (k > n)
                    return 0;

                if (k > n / 2) 
                    k = n - k;

                return _rows[n][k];
            }
        }

        public PascalTriangleUInt16(int maxRowIndex)
        {
            if (maxRowIndex < 0 || maxRowIndex > 18)
                throw new ArgumentOutOfRangeException($"{nameof(maxRowIndex)} must be between 0 and 18");

            _rows = new ushort[maxRowIndex + 1][];

            ComputePascalTriangleRows(maxRowIndex);
        }

        
        private void ComputePascalTriangleRows(int maxRowIndex)
        {
            //First row of Pascal triangle
            _rows[0] = new[] {(ushort)1};
            
            //Second row of Pascal triangle
            _rows[1] = new[] {(ushort)1};

            //Remaining rows of Pascal triangle
            var prevRow = _rows[1];
            for (var i = 2; i <= maxRowIndex; i++)
            {
                var rowSize = i / 2 + 1;

                var row = new ushort[rowSize];

                row[0] = (ushort)1;
                row[1] = (ushort)i;

                var value1 = (ushort)(i - 1);
                for (var j = 2; j < rowSize; j++)
                {
                    var value2 = j < prevRow.Length 
                        ? prevRow[j] 
                        : prevRow[prevRow.Length - 1];

                    checked { row[j] = (ushort)(value1 + value2); }

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
}