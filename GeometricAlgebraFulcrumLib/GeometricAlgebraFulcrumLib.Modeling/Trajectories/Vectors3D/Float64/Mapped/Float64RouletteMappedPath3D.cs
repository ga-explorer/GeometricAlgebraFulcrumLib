using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public class Float64RouletteMappedPath3D :
    Float64ArcLengthPath3D
{
    public Float64ArcLengthPath3D BaseCurve { get; }

    public Float64RouletteAffineMap3D RouletteMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64RouletteMappedPath3D(Float64ArcLengthPath3D baseCurve, Float64RouletteAffineMap3D rouletteMap)
        : base(baseCurve.TimeRange, baseCurve.IsPeriodic)
    {
        BaseCurve = baseCurve;
        RouletteMap = rouletteMap;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseCurve.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return RouletteMap.MapPoint(BaseCurve.GetValue(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToFiniteArcLengthPath()
    {
        return IsFinite
            ? this
            : new Float64RouletteMappedPath3D(
                BaseCurve.ToFiniteArcLengthPath(), 
                RouletteMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ArcLengthPath3D ToPeriodicArcLengthPath()
    {
        return IsPeriodic
            ? this
            : new Float64RouletteMappedPath3D(
                BaseCurve.ToPeriodicArcLengthPath(), 
                RouletteMap
            );
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

    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        var frame = BaseCurve.GetFrame(t);

        var point = RouletteMap.MapPoint(frame.Point);
        var (tangent, normal1, normal2) =
            RouletteMap.RotationQuaternion.RotateVectors(
                frame.Tangent,
                frame.Normal1,
                frame.Normal2
            );

        return Float64Path3DLocalFrame.Create(
            t,
            point,
            tangent,
            normal1,
            normal2
        );
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