using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra.Float64.Matrices;

public static class Float64ScalarArrayUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetSum(this IEnumerable<Float64Scalar> vector)
    {
        return vector.Select(s => s.ScalarValue).GetSum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetMin(this IEnumerable<Float64Scalar> vector)
    {
        return vector.Select(s => s.ScalarValue).GetMin();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetMax(this IEnumerable<Float64Scalar> vector)
    {
        return vector.Select(s => s.ScalarValue).GetMax();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Tuple<Float64Scalar, Float64Scalar> GetMinMax(this IEnumerable<Float64Scalar> vector)
    {
        var (min, max) = 
            vector.Select(s => s.ScalarValue).GetMinMax();

        return new Tuple<Float64Scalar, Float64Scalar>(min, max);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange GetMinMaxRange(this IEnumerable<double> vector)
    {
        return Float64ScalarRange.Create(vector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64ScalarRange GetMinMaxRange(this IEnumerable<Float64Scalar> vector)
    {
        return Float64ScalarRange.Create(vector);
    }
}