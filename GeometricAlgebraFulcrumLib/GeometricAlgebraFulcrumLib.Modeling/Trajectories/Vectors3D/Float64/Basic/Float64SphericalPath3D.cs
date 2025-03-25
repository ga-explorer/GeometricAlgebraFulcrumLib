using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

/// <summary>
/// This is a parametric expressed as parametric spherical coordinates
/// (r(t), theta(t), phi(t)) 
/// </summary>
public sealed class Float64SphericalPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalPath3D Finite(Float64ScalarRange timeRange, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve, Float64ScalarSignal phiCurve)
    {
        return new Float64SphericalPath3D(
            timeRange,
            false,
            rCurve,
            thetaCurve,
            phiCurve
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64SphericalPath3D Periodic(Float64ScalarRange timeRange, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve, Float64ScalarSignal phiCurve)
    {
        return new Float64SphericalPath3D(
            timeRange,
            true,
            rCurve,
            thetaCurve,
            phiCurve
        );
    }


    public Float64ScalarSignal RCurve { get; }

    public Float64ScalarSignal ThetaCurve { get; }

    public Float64ScalarSignal PhiCurve { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64SphericalPath3D(Float64ScalarRange timeRange, bool isPeriodic, Float64ScalarSignal rCurve, Float64ScalarSignal thetaCurve, Float64ScalarSignal phiCurve)
        : base(timeRange, isPeriodic)
    {
        RCurve = rCurve;
        ThetaCurve = thetaCurve;
        PhiCurve = phiCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return TimeRange.IsValid() &&
               RCurve.IsValid() &&
               ThetaCurve.IsValid() &&
               PhiCurve.IsValid() &&
               RCurve.TimeRange.Contains(TimeRange) &&
               ThetaCurve.TimeRange.Contains(TimeRange) &&
               PhiCurve.TimeRange.Contains(TimeRange);
    }

    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = theta.Cos();
        var thetaSin = theta.Sin();

        var phiCos = phi.Cos();
        var phiSin = phi.Sin();

        var x = r * thetaCos * phiCos;
        var y = r * thetaCos * phiSin;
        var z = r * thetaSin;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public override Float64Path3D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path3D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = theta.Cos();
        var thetaSin = theta.Sin();

        var phiCos = phi.Cos();
        var phiSin = phi.Sin();

        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
        var phiDt1 = PhiCurve.GetDerivative1Value(parameterValue);

        // x = r * thetaCos * phiCos;
        var x =
            rDt1 * thetaCos * phiCos -
            r * thetaSin * thetaDt1 * phiCos -
            r * thetaCos * phiSin * phiDt1;

        // y = r * thetaCos * phiSin;
        var y =
            rDt1 * thetaCos * phiSin -
            r * thetaSin * thetaDt1 * phiSin +
            r * thetaCos * phiCos * phiDt1;

        // z = r * thetaSin;
        var z =
            rDt1 * thetaSin +
            r * thetaCos * thetaDt1;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        var r = RCurve.GetValue(parameterValue);
        var theta = ThetaCurve.GetValue(parameterValue);
        var phi = PhiCurve.GetValue(parameterValue);

        var thetaCos = theta.Cos();
        var thetaSin = theta.Sin();

        var phiCos = phi.Cos();
        var phiSin = phi.Sin();

        var rDt1 = RCurve.GetDerivative1Value(parameterValue);
        var thetaDt1 = ThetaCurve.GetDerivative1Value(parameterValue);
        var phiDt1 = PhiCurve.GetDerivative1Value(parameterValue);

        var rDt2 = RCurve.GetDerivative2Value(parameterValue);
        var thetaDt2 = ThetaCurve.GetDerivative2Value(parameterValue);
        var phiDt2 = PhiCurve.GetDerivative2Value(parameterValue);

        var x =
            -phiCos * thetaCos * r * phiDt1 * phiDt1 -
            2 * thetaCos * phiSin * phiDt1 * rDt1 +
            2 * r * phiSin * thetaSin * phiDt1 * thetaDt1 -
            2 * phiCos * thetaSin * rDt1 * thetaDt1 -
            phiCos * thetaCos * r * thetaDt1 * thetaDt1 -
            thetaCos * r * phiSin * phiDt2 +
            phiCos * thetaCos * rDt2 -
            phiCos * r * thetaSin * thetaDt2;

        var y =
            -thetaCos * r * phiSin * phiDt1 * phiDt1 +
            2 * phiCos * thetaCos * phiDt1 * rDt1 -
            2 * phiCos * r * thetaSin * phiDt1 * thetaDt1 -
            2 * phiSin * thetaSin * rDt1 * thetaDt1 -
            thetaCos * r * phiSin * thetaDt1 * thetaDt1 +
            phiCos * thetaCos * r * phiDt2 +
            thetaCos * phiSin * rDt2 -
            r * phiSin * thetaSin * thetaDt2;

        var z =
            2 * thetaCos * rDt1 * thetaDt1 -
            r * thetaSin * thetaDt1 * thetaDt1 +
            thetaSin * rDt2 +
            thetaCos * r * thetaDt2;

        return LinFloat64Vector3D.Create(x, y, z);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}