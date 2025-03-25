using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

public sealed class Float64AffineMappedPath2D :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedPath2D Create(Float64Path2D baseCurve, IFloat64AffineMap2D map)
    {
        return new Float64AffineMappedPath2D(baseCurve, map);
    }


    public Float64Path2D BasePath { get; }

    public IFloat64AffineMap2D AffineMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMappedPath2D(Float64Path2D baseCurve, IFloat64AffineMap2D map)
        : base(baseCurve.TimeRange, baseCurve.IsPeriodic)
    {
        BasePath = baseCurve;
        AffineMap = map;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BasePath.IsValid() &&
               AffineMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return AffineMap.MapPoint(BasePath.GetValue(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64AffineMappedPath2D(BasePath.ToFinitePath(), AffineMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64AffineMappedPath2D(BasePath.ToPeriodicPath(), AffineMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative1Value(double t)
    {
        return AffineMap.MapPoint(
            BasePath.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetDerivative2Value(double t)
    {
        return AffineMap.MapPoint(
            BasePath.GetDerivative2Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2DLocalFrame GetFrame(double t)
    {
        var frame = BasePath.GetFrame(t);

        return Float64Path2DLocalFrame.Create(
            t,
            AffineMap.MapPoint(frame.Point),
            AffineMap.MapVector(frame.Tangent)
        );
    }
}