namespace GeometricAlgebraFulcrumLib.Algebra.Polynomials.Float64.Chebyshev;

// Extension helper: extract row of 2D array
public static class ArrayExtensions
{
    public static double[] GetRow(this double[,] array, int row)
    {
        var cols = array.GetLength(1);
        var result = new double[cols];
        for (var j = 0; j < cols; j++) result[j] = array[row, j];
        return result;
    }
}