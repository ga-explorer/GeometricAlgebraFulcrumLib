using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.SpaceND;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics.LinearAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;

public static class MathNetNumericsUtils
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<double> ToMathNetVector(this double[] array)
    {
        return Vector<double>.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<double> ToMathNetVector(this IEnumerable<double> scalarList)
    {
        return Vector<double>.Build.DenseOfEnumerable(scalarList);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector<double> ToMathNetVector(this IReadOnlyDictionary<int, double> scalarList, int size)
    {
        var array = new double[size];

        foreach (var (i, s) in scalarList)
            array[i] = s;

        return Vector<double>.Build.DenseOfArray(array);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Vector ToVector(this Vector<double> vector)
    {
        return vector.ToArray().CreateVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNorm(this Vector<double> vector)
    {
        return vector.DotProduct(vector).Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetVectorNormSquared(this Vector<double> vector)
    {
        return vector.DotProduct(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double VectorDot(this Vector<double> vector1, Vector<double> vector2)
    {
        return vector1.DotProduct(vector2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Matrix<double> GetMathNetOrthogonalMatrix(this System.Random random, int size)
    {
        return random
            .GetFloat64Array2D(size, size)
            .ToMatrix()
            .QR()
            .Q;
    }

    public static IReadOnlyList<Vector<double>> GetMathNetOrthonormalVectors(this System.Random random, int size, int count)
    {
        count = Math.Min(count, size);

        var matrix = random.GetMathNetOrthogonalMatrix(size);

        var vectorArray = new Vector<double>[count];

        for (var i = 0; i < count; i++)
            vectorArray[i] = matrix.Column(i);

        return vectorArray;
    }
}