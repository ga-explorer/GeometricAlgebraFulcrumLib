using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Basic;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public sealed class Float64HarmonicPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64HarmonicPath2D Create(Float64ScalarHarmonicSignal xCurve, Float64ScalarHarmonicSignal yCurve)
    {
        return new Float64HarmonicPath2D(xCurve, yCurve);
    }


    public Float64ScalarHarmonicSignal XCurve { get; }

    public Float64ScalarHarmonicSignal YCurve { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64HarmonicPath2D(Float64ScalarHarmonicSignal xCurve, Float64ScalarHarmonicSignal yCurve)
        : base(xCurve.TimeRange, xCurve.IsPeriodic)
    {
        XCurve = xCurve;
        YCurve = yCurve;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return XCurve.IsValid() &&
               YCurve.IsValid();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetValue(t),
            YCurve.GetValue(t)
        );
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
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetDerivative1Value(t),
            YCurve.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return LinFloat64Vector2D.Create(
            XCurve.GetDerivative2Value(t),
            YCurve.GetDerivative2Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return this.GetFrenetSerretFrame(t);
    }
}