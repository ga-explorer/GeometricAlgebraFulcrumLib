using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64RotatedNormalsArcLengthPath3D :
    Float64ArcLengthPath3D
{
    public Float64ArcLengthPath3D BaseCurve { get; }

    public LinFloat64PolarAngleTimeSignal AngleFunction { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RotatedNormalsArcLengthPath3D(Float64ArcLengthPath3D baseCurve, LinFloat64PolarAngleTimeSignal angleFunction)
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
    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64RotatedNormalsArcLengthPath3D(
                BaseCurve.ToFiniteArcLengthPath(),
                AngleFunction
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64RotatedNormalsArcLengthPath3D(
                BaseCurve.ToPeriodicArcLengthPath(),
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return BaseCurve.GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return BaseCurve.TimeToLength(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        return BaseCurve.LengthToTime(length);
    }
}