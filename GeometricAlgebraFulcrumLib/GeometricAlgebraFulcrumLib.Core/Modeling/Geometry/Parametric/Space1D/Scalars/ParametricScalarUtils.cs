using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars;

public static class ParametricScalarUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Times(this IFloat64ParametricScalar scalar, double value)
    {
        Debug.Assert(value.IsValid() && !value.IsZero());

        return ComputedParametricScalar.Create(
            t => scalar.GetValue(t) * value,
            t => scalar.GetDerivative1Value(t) * value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Divide(this IFloat64ParametricScalar scalar, double value)
    {
        return scalar.Times(1d / value);
    }
}