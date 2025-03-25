using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64RotatedNormalsPath3D :
    Float64Path3D
{
    public Float64Path3D BaseCurve { get; }

    public LinFloat64PolarAngleTimeSignal AngleFunction { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RotatedNormalsPath3D(Float64Path3D baseCurve, LinFloat64PolarAngleTimeSignal angleFunction)
        : base(baseCurve.TimeRange, baseCurve.IsPeriodic)
    {
        BaseCurve = baseCurve;
        AngleFunction = angleFunction;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseCurve.IsValid() &&
               AngleFunction.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64RotatedNormalsPath3D(
                BaseCurve.ToFinitePath(),
                AngleFunction
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64RotatedNormalsPath3D(
                BaseCurve.ToPeriodicPath(),
                AngleFunction
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return BaseCurve.GetValue(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return BaseCurve.GetDerivative1Value(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return BaseCurve.GetDerivative2Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        return BaseCurve
            .GetFrame(t)
            .RotateNormalsBy(AngleFunction.GetValue(t));
    }
}