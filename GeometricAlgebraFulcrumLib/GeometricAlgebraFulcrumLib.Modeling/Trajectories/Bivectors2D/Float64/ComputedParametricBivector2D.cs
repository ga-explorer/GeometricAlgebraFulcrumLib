using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Bivectors2D.Float64;

public class ComputedParametricBivector2D :
    IParametricBivector2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        return new ComputedParametricBivector2D(
            Float64ScalarRange.Infinite,
            getBivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange,
            getBivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        return new ComputedParametricBivector2D(
            Float64ScalarRange.Infinite,
            getBivectorFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange,
            getBivectorFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(DifferentialFunction xyFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, DifferentialFunction xyFunc)
    {
        var xyDtFunc = xyFunc.GetDerivative1();

        return new ComputedParametricBivector2D(
            parameterRange,
            t =>
                LinFloat64Bivector2D.Create(
                    xyFunc.GetValue(t)
                ),
            t =>
                LinFloat64Bivector2D.Create(
                    xyDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Func<double, double> xyFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricBivector2D Create(Float64ScalarRange parameterRange, Func<double, double> xyFunc)
    {
        return new ComputedParametricBivector2D(
            parameterRange,
            t =>
                LinFloat64Bivector2D.Create(
                    xyFunc(t)
                ),
            t =>
                LinFloat64Bivector2D.Create(
                    Differentiate.FirstDerivative(xyFunc, t)
                )
        );
    }


    public Float64ScalarRange TimeRange { get; }

    public Func<double, LinFloat64Bivector2D> GetBivectorFunc { get; }

    public Func<double, LinFloat64Bivector2D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricBivector2D(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc)
    {
        TimeRange = parameterRange;
        GetBivectorFunc = getBivectorFunc;
        GetTangentFunc =
            t =>
            {
                const double zeroEpsilon = 1e-7;

                var p1 = getBivectorFunc(t - zeroEpsilon);
                var p2 = getBivectorFunc(t + zeroEpsilon);

                return (p2 - p1) / (2 * zeroEpsilon);
            };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricBivector2D(Float64ScalarRange parameterRange, Func<double, LinFloat64Bivector2D> getBivectorFunc, Func<double, LinFloat64Bivector2D> getTangentFunc)
    {
        TimeRange = parameterRange;
        GetBivectorFunc = getBivectorFunc;
        GetTangentFunc = getTangentFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetValue(double parameterValue)
    {
        return GetBivectorFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Bivector2D GetDerivative1Bivector(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSignal GetDualScalarCurve()
    {
        return Float64ScalarComputedSignal.Finite(
            TimeRange,
            t => GetValue(t).Dual2D().Scalar.ScalarValue
        );
    }
}