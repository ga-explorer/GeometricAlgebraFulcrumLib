using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors2D.Float64;

public class ConstantParametricBivector2D :
    IParametricBivector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(LinFloat64Bivector2D point)
    {
        return new ConstantParametricBivector2D(
            point,
            LinFloat64Bivector2D.Zero
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ConstantParametricBivector2D Create(LinFloat64Bivector2D point, LinFloat64Bivector2D tangent)
    {
        return new ConstantParametricBivector2D(
            point,
            tangent
        );
    }


    public LinFloat64Bivector2D Point { get; }

    public LinFloat64Bivector2D Tangent { get; }

    public Float64ScalarRange TimeRange
        => Float64ScalarRange.Infinite;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ConstantParametricBivector2D(LinFloat64Bivector2D point, LinFloat64Bivector2D tangent)
    {
        Point = point;
        Tangent = tangent;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return Point.IsValid() &&
               Tangent.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetValue(double parameterValue)
    {
        return Point;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue)
    {
        return Tangent;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSignal GetDualScalarCurve()
    {
        return Float64ScalarSignal.FiniteConstant(
            TimeRange,
            Point.Dual2D().Scalar.ScalarValue
        );
    }
}