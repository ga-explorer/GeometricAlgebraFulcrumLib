using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space1D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors3D.Float64.Mapped;

public sealed class Float64AffineMappedTimePath3D :
    Float64Path3D
{
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64AffineMappedTimePath3D Create(Float64Path3D baseCurve, Float64AffineMap1D timeMap)
    {
        return new Float64AffineMappedTimePath3D(baseCurve, timeMap);
    }


    public Float64Path3D BasePath { get; }

    /// <summary>
    /// This function takes a number in the range [0, 1] and returns a number
    /// in the range [BaseCurve.ParameterValueMin, BaseCurve.ParameterValueMax]
    /// </summary>
    public Float64AffineMap1D TimeMap { get; }

    public Float64AffineMap1D TimeMapInverse { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64AffineMappedTimePath3D(Float64Path3D baseCurve, Float64AffineMap1D timeMap)
        : base(
            timeMap.Scaling > 0
                ? Float64ScalarRange.Create(
                    timeMap[baseCurve.MinTime],
                    timeMap[baseCurve.MaxTime]
                )
                : Float64ScalarRange.Create(
                    timeMap[baseCurve.MaxTime],
                    timeMap[baseCurve.MinTime]
                ),
            baseCurve.IsPeriodic
        )
    {
        BasePath = baseCurve;

        TimeMap = timeMap;
        TimeMapInverse = (Float64AffineMap1D)timeMap.GetInverseAffineMap();

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BasePath.IsValid() &&
               TimeMap.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetValue(double t)
    {
        return BasePath.GetValue(
            TimeMapInverse.MapPoint(t)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToFinitePath()
    {
        return IsFinite
            ? this
            : Create(BasePath.ToFinitePath(),
                TimeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : Create(BasePath.ToPeriodicPath(),
                TimeMap);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative1Value(double t)
    {
        return TimeMapInverse.Scaling.ScalarValue *
               BasePath.GetDerivative1Value(TimeMapInverse.MapPoint(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector3D GetDerivative2Value(double t)
    {
        return TimeMapInverse.Scaling.ScalarValue.Square() *
               BasePath.GetDerivative2Value(TimeMapInverse.MapPoint(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path3DLocalFrame GetFrame(double t)
    {
        return BasePath.GetFrame(
            TimeMapInverse.MapPoint(t)
        );
    }
}