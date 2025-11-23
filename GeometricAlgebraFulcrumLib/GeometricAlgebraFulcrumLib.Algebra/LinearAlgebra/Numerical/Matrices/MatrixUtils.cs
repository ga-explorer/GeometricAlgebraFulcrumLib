using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Numerical.Matrices;

public static class MatrixUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix2 Lerp(this double t, SquareMatrix2 v1, SquareMatrix2 v2)
    {
        var s = 1.0d - t;

        return new SquareMatrix2()
        {
            Scalar00 = s * v1.Scalar00 + t * v2.Scalar00,
            Scalar01 = s * v1.Scalar01 + t * v2.Scalar01,
            Scalar10 = s * v1.Scalar10 + t * v2.Scalar10,
            Scalar11 = s * v1.Scalar11 + t * v2.Scalar11
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static SquareMatrix3 Lerp(this double t, SquareMatrix3 v1, SquareMatrix3 v2)
    {
        var s = 1.0d - t;

        return new SquareMatrix3()
        {
            Scalar00 = s * v1.Scalar00 + t * v2.Scalar00,
            Scalar10 = s * v1.Scalar10 + t * v2.Scalar10,
            Scalar20 = s * v1.Scalar20 + t * v2.Scalar20,
            Scalar01 = s * v1.Scalar01 + t * v2.Scalar01,
            Scalar11 = s * v1.Scalar11 + t * v2.Scalar11,
            Scalar21 = s * v1.Scalar21 + t * v2.Scalar21,
            Scalar02 = s * v1.Scalar02 + t * v2.Scalar02,
            Scalar12 = s * v1.Scalar12 + t * v2.Scalar12,
            Scalar22 = s * v1.Scalar22 + t * v2.Scalar22
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<SquareMatrix2> Lerp(this IEnumerable<double> tList, SquareMatrix2 v1, SquareMatrix2 v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<SquareMatrix3> Lerp(this IEnumerable<double> tList, SquareMatrix3 v1, SquareMatrix3 v2)
    {
        return tList.Select(t => t.Lerp(v1, v2));
    }
}