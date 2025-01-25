using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space1D.Scalars;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Trivectors;

public class ComputedParametricTrivector3D :
    IParametricTrivector3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Func<double, LinFloat64Trivector3D> getTrivectorFunc)
    {
        return new ComputedParametricTrivector3D(
            Float64ScalarRange.Infinite,
            getTrivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc)
    {
        return new ComputedParametricTrivector3D(
            parameterRange,
            getTrivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Func<double, LinFloat64Trivector3D> getTrivectorFunc, Func<double, LinFloat64Trivector3D> getTangentFunc)
    {
        return new ComputedParametricTrivector3D(
            Float64ScalarRange.Infinite,
            getTrivectorFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc, Func<double, LinFloat64Trivector3D> getTangentFunc)
    {
        return new ComputedParametricTrivector3D(
            parameterRange,
            getTrivectorFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(DifferentialFunction xyzFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyzFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, DifferentialFunction xyzFunc)
    {
        var xyzDtFunc = xyzFunc.GetDerivative1();

        return new ComputedParametricTrivector3D(
            parameterRange,
            t =>
                LinFloat64Trivector3D.Create(
                    xyzFunc.GetValue(t)
                ),
            t =>
                LinFloat64Trivector3D.Create(
                    xyzDtFunc.GetValue(t)
                )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Func<double, double> xyzFunc)
    {
        return Create(
            Float64ScalarRange.Infinite,
            xyzFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ComputedParametricTrivector3D Create(Float64ScalarRange parameterRange, Func<double, double> xyzFunc)
    {
        return new ComputedParametricTrivector3D(
            parameterRange,
            t =>
                LinFloat64Trivector3D.Create(xyzFunc(t)),
            t =>
                LinFloat64Trivector3D.Create(
                    Differentiate.FirstDerivative(xyzFunc, t)
                )
        );
    }


    public Float64ScalarRange ParameterRange { get; }

    public Func<double, LinFloat64Trivector3D> GetTrivectorFunc { get; }

    public Func<double, LinFloat64Trivector3D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricTrivector3D(Float64ScalarRange parameterRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc)
    {
        ParameterRange = parameterRange;
        GetTrivectorFunc = getTrivectorFunc;
        GetTangentFunc =
            t =>
            {
                const double zeroEpsilon = 1e-7;

                var p1 = getTrivectorFunc(t - zeroEpsilon);
                var p2 = getTrivectorFunc(t + zeroEpsilon);

                return (p2 - p1) / (2 * zeroEpsilon);
            };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private ComputedParametricTrivector3D(Float64ScalarRange parameterRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc, Func<double, LinFloat64Trivector3D> getTangentFunc)
    {
        ParameterRange = parameterRange;
        GetTrivectorFunc = getTrivectorFunc;
        GetTangentFunc = getTangentFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D GetTrivector(double parameterValue)
    {
        return GetTrivectorFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64Trivector3D GetDerivative1Trivector(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IFloat64ParametricScalar GetDualScalarCurve()
    {
        return ComputedParametricScalar.Create(
            ParameterRange,
            t => GetTrivector(t).Dual3D().Scalar.ScalarValue
        );
    }
}