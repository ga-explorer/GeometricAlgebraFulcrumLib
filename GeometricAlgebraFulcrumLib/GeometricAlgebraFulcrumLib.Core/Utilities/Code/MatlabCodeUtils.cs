using System.Text;

namespace GeometricAlgebraFulcrumLib.Core.Utilities.Code;

public static class MatlabCodeUtils
{
    public static string ToMatlabCode(this double[,] array)
    {
        var m = array.GetLength(0);
        var n = array.GetLength(1);

        var composer = new StringBuilder();

        composer.Append('[');

        for (var i = 0; i < m; i++)
        {
            for (var j = 0; j < n; j++)
            {
                composer.Append(array[i, j].ToString("G"));

                if (j < n - 1)
                    composer.Append(", ");
            }

            if (i < m - 1)
                composer.Append("; ");
        }

        composer.Append(']');

        return composer.ToString();
    }
}