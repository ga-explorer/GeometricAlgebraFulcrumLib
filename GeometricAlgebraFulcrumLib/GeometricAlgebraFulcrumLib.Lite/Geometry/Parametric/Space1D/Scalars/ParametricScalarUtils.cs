using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars;

public static class ParametricScalarUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Times(this IParametricScalar scalar, double value)
    {
        Debug.Assert(value.IsValid() && !value.IsZero());

        return ComputedParametricScalar.Create(
            t => scalar.GetValue(t) * value,
            t => scalar.GetDerivative1Value(t) * value
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricScalar Divide(this IParametricScalar scalar, double value)
    {
        return scalar.Times(1d / value);
    }
}