using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

/// <summary>
/// This is a parametric expressed as parametric polar coordinates
/// (r(t), theta(t)) 
/// </summary>
public sealed class Float64PolarPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarPath2D Finite(Float64ScalarRange timeRange, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve)
    {
        return new Float64PolarPath2D(
            timeRange,
            false,
            rCurve,
            thetaCurve
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64PolarPath2D Periodic(Float64ScalarRange timeRange, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve)
    {
        return new Float64PolarPath2D(
            timeRange,
            true,
            rCurve,
            thetaCurve
        );
    }


    public Float64ScalarSignal RPath { get; }

    public Float64ScalarSignal ThetaPath { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64PolarPath2D(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve)
        : base(timeRange, isPeriodic)
    {
        RPath = rCurve;
        ThetaPath = thetaCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return RPath.IsValid() &&
               ThetaPath.IsValid();
    }

    public override Float64Path2D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path2D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        var r = RPath.GetValue(t);
        var theta = ThetaPath.GetValue(t);

        return new LinFloat64PolarVector2D(r, theta.RadiansToPolarAngle()).ToLinVector2D();
    }

    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        var r = RPath.GetValue(t);
        var theta = ThetaPath.GetValue(t);

        var thetaCos = theta.Cos();
        var thetaSin = theta.Sin();

        var rDt1 = RPath.GetDerivative1Value(t);
        var thetaDt1 = ThetaPath.GetDerivative1Value(t);

        // x = r * thetaCos;
        var xDt1 =
            rDt1 * thetaCos - r * thetaSin * thetaDt1;

        // y = r * thetaSin;
        var yDt1 =
            rDt1 * thetaSin + r * thetaCos * thetaDt1;

        return LinFloat64Vector2D.Create(xDt1, yDt1);
    }

    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        var r = RPath.GetValue(t);
        var theta = ThetaPath.GetValue(t);

        var thetaCos = theta.Cos();
        var thetaSin = theta.Sin();

        var rDt1 = RPath.GetDerivative1Value(t);
        var thetaDt1 = ThetaPath.GetDerivative1Value(t);

        var rDt2 = RPath.GetDerivative2Value(t);
        var thetaDt2 = ThetaPath.GetDerivative2Value(t);

        // xDt1 = rDt1 * thetaCos - r * thetaSin * thetaDt1
        var xDt2 =
            rDt2 * thetaCos -
            rDt1 * thetaSin * thetaDt1 -
            rDt1 * thetaSin * thetaDt1 -
            r * thetaCos * thetaDt1 * thetaDt1 -
            r * thetaSin * thetaDt2;

        // yDt1 = rDt1 * thetaSin + r * thetaCos * thetaDt1
        var yDt2 =
            rDt2 * thetaSin +
            rDt1 * thetaCos * thetaDt1 +
            rDt1 * thetaCos * thetaDt1 -
            r * thetaSin * thetaDt1 * thetaDt1 +
            r * thetaCos * thetaDt2;

        return LinFloat64Vector2D.Create(xDt2, yDt2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return this.GetFrenetSerretFrame(t);
    }
}