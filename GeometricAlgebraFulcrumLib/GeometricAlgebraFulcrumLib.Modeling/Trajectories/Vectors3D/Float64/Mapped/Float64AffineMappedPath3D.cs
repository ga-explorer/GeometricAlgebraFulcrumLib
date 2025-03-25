using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64AffineMappedPath3D :
    Float64Path3D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedPath3D Create(Float64Path3D baseCurve, IFloat64AffineMap3D map)
    {
        return new Float64AffineMappedPath3D(baseCurve, map);
    }


    public Float64Path3D BasePath { get; }

    public IFloat64AffineMap3D AffineMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMappedPath3D(Float64Path3D baseCurve, IFloat64AffineMap3D map)
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
    public override LinFloat64Vector3D GetValue(double t)
    {
        return AffineMap.MapPoint(BasePath.GetValue(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64AffineMappedPath3D(BasePath.ToFinitePath(), AffineMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64AffineMappedPath3D(BasePath.ToPeriodicPath(), AffineMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return AffineMap.MapPoint(
            BasePath.GetDerivative1Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return AffineMap.MapPoint(
            BasePath.GetDerivative2Value(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        var frame = BasePath.GetFrame(t);

        return Float64Path3DLocalFrame.Create(
            t,
            AffineMap.MapPoint(frame.Point),
            AffineMap.MapVector(frame.Tangent)
        );
    }
}