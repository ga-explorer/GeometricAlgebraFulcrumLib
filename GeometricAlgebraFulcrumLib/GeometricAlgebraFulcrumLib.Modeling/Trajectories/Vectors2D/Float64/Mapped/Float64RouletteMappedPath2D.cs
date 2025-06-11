using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

public class Float64RouletteMappedPath2D :
    Float64ArcLengthPath2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64RouletteMappedPath2D Create(Float64ArcLengthPath2D baseCurve, Float64RouletteAffineMap2D rouletteMap)
    {
        return new Float64RouletteMappedPath2D(baseCurve, rouletteMap);
    }


    public Float64ArcLengthPath2D BasePath { get; }

    public Float64RouletteAffineMap2D RouletteMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64RouletteMappedPath2D(Float64ArcLengthPath2D baseCurve, Float64RouletteAffineMap2D rouletteMap)
        : base(baseCurve.TimeRange, baseCurve.IsPeriodic)
    {
        BasePath = baseCurve;
        RouletteMap = rouletteMap;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BasePath.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return RouletteMap.MapPoint(BasePath.GetValue(t));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64RouletteMappedPath2D(
                (Float64ArcLengthPath2D)BasePath.ToFinitePath(),
                RouletteMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath2D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64RouletteMappedPath2D(
                (Float64ArcLengthPath2D)BasePath.ToPeriodicPath(),
                RouletteMap
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return BasePath.GetDerivative1Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return BasePath.GetDerivative2Value(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        var frame = BasePath.GetFrame(t);

        var point = RouletteMap.MapPoint(frame.Point);
        var tangent = RouletteMap.RotationAngle.Rotate(frame.Tangent);

        return Float64Path2DLocalFrame.Create(
            t,
            point,
            tangent
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar GetLength()
    {
        return BasePath.GetLength();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar TimeToLength(double t)
    {
        return BasePath.TimeToLength(t);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar LengthToTime(double length)
    {
        return BasePath.LengthToTime(length);
    }
}