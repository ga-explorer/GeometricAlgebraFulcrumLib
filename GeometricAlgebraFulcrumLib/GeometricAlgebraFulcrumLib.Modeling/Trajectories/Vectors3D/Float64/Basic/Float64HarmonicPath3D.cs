using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public sealed class Float64HarmonicPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64HarmonicPath3D Create(Float64ScalarHarmonicSignal xCurve, Float64ScalarHarmonicSignal yCurve, Float64ScalarHarmonicSignal zCurve)
    {
        return new Float64HarmonicPath3D(xCurve, yCurve, zCurve);
    }


    public Float64ScalarHarmonicSignal XCurve { get; }

    public Float64ScalarHarmonicSignal YCurve { get; }

    public Float64ScalarHarmonicSignal ZCurve { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64HarmonicPath3D(Float64ScalarHarmonicSignal xCurve, Float64ScalarHarmonicSignal yCurve, Float64ScalarHarmonicSignal zCurve)
        : base(xCurve.TimeRange, xCurve.IsPeriodic)
    {
        XCurve = xCurve;
        YCurve = yCurve;
        ZCurve = zCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return XCurve.IsValid() &&
               YCurve.IsValid() &&
               ZCurve.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetValue(parameterValue),
            YCurve.GetValue(parameterValue),
            ZCurve.GetValue(parameterValue)
        );
    }

    public override Float64Path3D ToFinitePath()
    {
        throw new NotImplementedException();
    }

    public override Float64Path3D ToPeriodicPath()
    {
        throw new NotImplementedException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetDerivative1Value(parameterValue),
            YCurve.GetDerivative1Value(parameterValue),
            ZCurve.GetDerivative1Value(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double parameterValue)
    {
        return LinFloat64Vector3D.Create(
            XCurve.GetDerivative2Value(parameterValue),
            YCurve.GetDerivative2Value(parameterValue),
            ZCurve.GetDerivative2Value(parameterValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}