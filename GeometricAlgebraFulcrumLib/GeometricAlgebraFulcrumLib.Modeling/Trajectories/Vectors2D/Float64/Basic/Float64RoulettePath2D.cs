using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Basic;

public class Float64RoulettePath2D :
    Float64Path2D
{
    public Float64ArcLengthPath2D FixedCurve { get; }

    public Float64ArcLengthPath2D MovingCurve { get; }

    public LinFloat64Vector2D GeneratorPoint { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RoulettePath2D(bool isPeriodic, Float64ArcLengthPath2D fixedCurve, Float64ArcLengthPath2D movingCurve, double parameterValueMax)
        : base(Float64ScalarRange.Create(0, parameterValueMax), isPeriodic)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = movingCurve.GetValue(movingCurve.TimeRange.MinValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RoulettePath2D(bool isPeriodic, Float64ArcLengthPath2D fixedCurve, Float64ArcLengthPath2D movingCurve, ILinFloat64Vector2D generatorPoint, double parameterValueMax)
        : base(Float64ScalarRange.Create(0, parameterValueMax), isPeriodic)
    {
        FixedCurve = fixedCurve;
        MovingCurve = movingCurve;
        GeneratorPoint = generatorPoint.ToLinVector2D();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return FixedCurve.IsValid() &&
               MovingCurve.IsValid();
    }

    public Float64RouletteAffineMap2D GetRouletteMap(double t)
    {
        var t1 = MovingCurve.LengthToTime(t);
        var movingFrame = MovingCurve.GetTangent(t1);

        var t2 = FixedCurve.LengthToTime(t);
        var fixedFrame = FixedCurve.GetTangent(t2);

        return new Float64RouletteAffineMap2D(
            FixedCurve.GetValue(t),
            MovingCurve.GetValue(t),
            movingFrame.GetAngle(fixedFrame).ToDirectedAngle()
        );
    }

    public override LinFloat64Vector2D GetValue(double t)
    {
        var t1 = MovingCurve.LengthToTime(t);
        var movingFrame = MovingCurve.GetFrame(t1);

        var t2 = FixedCurve.LengthToTime(t);
        var fixedFrame = FixedCurve.GetFrame(t2);

        var angle =
            movingFrame.GetAngle(fixedFrame);

        return fixedFrame.Point +
               angle.Rotate(GeneratorPoint - movingFrame.Point);
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
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        return this.GetFrenetSerretFrame(t);
    }
}