using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Calculus.Functions.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Trivectors3D.Float64;

public class LinFloat64Trivector3DComputedTrajectory :
    LinFloat64Trivector3DTrajectory
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3DComputedTrajectory Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc)
    {
        return new LinFloat64Trivector3DComputedTrajectory(
            timeRange,
            false,
            getTrivectorFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3DComputedTrajectory Finite(Float64ScalarRange timeRange, Func<double, LinFloat64Trivector3D> getTrivectorFunc, Func<double, LinFloat64Trivector3D> getTangentFunc)
    {
        return new LinFloat64Trivector3DComputedTrajectory(
            timeRange,
            false,
            getTrivectorFunc,
            getTangentFunc
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64Trivector3DComputedTrajectory Finite(Float64ScalarRange timeRange, DifferentialFunction xyzFunc)
    {
        var xyzDtFunc = xyzFunc.GetDerivative1();

        return new LinFloat64Trivector3DComputedTrajectory(
            timeRange,
            false,
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
    public static LinFloat64Trivector3DComputedTrajectory Finite(Float64ScalarRange timeRange, Func<double, double> xyzFunc)
    {
        return new LinFloat64Trivector3DComputedTrajectory(
            timeRange,
            false,
            t =>
                LinFloat64Trivector3D.Create(xyzFunc(t)),
            t =>
                LinFloat64Trivector3D.Create(
                    Differentiate.FirstDerivative(xyzFunc, t)
                )
        );
    }


    public Func<double, LinFloat64Trivector3D> GetTrivectorFunc { get; }

    public Func<double, LinFloat64Trivector3D> GetTangentFunc { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64Trivector3DComputedTrajectory(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Trivector3D> getTrivectorFunc)
        : base(timeRange, isPeriodic)
    {
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
    private LinFloat64Trivector3DComputedTrajectory(Float64ScalarRange timeRange, bool isPeriodic, Func<double, LinFloat64Trivector3D> getTrivectorFunc, Func<double, LinFloat64Trivector3D> getTangentFunc)
        : base(timeRange, isPeriodic)
    {
        GetTrivectorFunc = getTrivectorFunc;
        GetTangentFunc = getTangentFunc;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetValue(double parameterValue)
    {
        return GetTrivectorFunc(parameterValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetDerivative1Value(double parameterValue)
    {
        return GetTangentFunc(parameterValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Trivector3D GetDerivative2Value(double parameterValue)
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal GetDualScalarCurve()
    {
        return Float64ScalarComputedSignal.Finite(
            TimeRange,
            t => GetValue(t).Dual3D().Scalar.ScalarValue
        );
    }
}