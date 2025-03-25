using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Basic;

public class Float64RoulettePath3D :
    Float64Path3D
{
    public Float64ArcLengthPath3D FixedCurve { get; }

    public Float64ArcLengthPath3D MovingCurve { get; }

    public LinFloat64Vector3D GeneratorPoint { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RoulettePath3D(bool isPeriodic, Float64ArcLengthPath3D fixedCurve, Float64ArcLengthPath3D movingCurve, double parameterValueMax)
        : base(Float64ScalarRange.Create(0, parameterValueMax), isPeriodic)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = movingCurve.GetValue(movingCurve.TimeRange.MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RoulettePath3D(bool isPeriodic, Float64ArcLengthPath3D fixedCurve, Float64ArcLengthPath3D movingCurve, ILinFloat64Vector3D generatorPoint, double parameterValueMax)
        : base(Float64ScalarRange.Create(0, parameterValueMax), isPeriodic)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = generatorPoint.ToLinVector3D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return FixedCurve.IsValid() &&
               MovingCurve.IsValid();
    }

    public Float64RouletteAffineMap3D GetRouletteMap(double parameterValue)
    {
        var t1 = MovingCurve.LengthToTime(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToTime(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var quaternion =
            movingFrame.FrameToFrameRotationQuaternion(fixedFrame);

        return new Float64RouletteAffineMap3D(
            fixedFrame.Point,
            movingFrame.Point,
            quaternion
        );
    }

    public override LinFloat64Vector3D GetValue(double parameterValue)
    {
        var t1 = MovingCurve.LengthToTime(parameterValue);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToTime(parameterValue);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var quaternion =
            movingFrame.FrameToFrameRotationQuaternion(fixedFrame);

        return fixedFrame.Point +
               quaternion.RotateVector(GeneratorPoint - movingFrame.Point);
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
    public override Float64Path3DLocalFrame GetFrame(double parameterValue)
    {
        return this.GetFrenetSerretFrame(parameterValue);
    }
}