using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;

public sealed class LinFloat64DirectedAngleTimeSignal :
    Float64TrajectoryConverter<double, LinFloat64DirectedAngle>
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64DirectedAngleTimeSignal Create(Func<double, LinFloat64DirectedAngle> getPointFunc)
    //{
    //    return new LinFloat64DirectedAngleTimeSignal(Float64ScalarRange.Infinite, getPointFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64DirectedAngleTimeSignal Create(Float64ScalarRange timeRange, Func<double, LinFloat64DirectedAngle> getPointFunc)
    //{
    //    return new LinFloat64DirectedAngleTimeSignal(timeRange, getPointFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64DirectedAngleTimeSignal Create(Func<double, LinFloat64DirectedAngle> getPointFunc, Func<double, LinFloat64DirectedAngle> getTangentFunc)
    //{
    //    return new LinFloat64DirectedAngleTimeSignal(Float64ScalarRange.Infinite, getPointFunc, getTangentFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64DirectedAngleTimeSignal Create(Float64ScalarRange timeRange, Func<double, LinFloat64DirectedAngle> getPointFunc, Func<double, LinFloat64DirectedAngle> getTangentFunc)
    //{
    //    return new LinFloat64DirectedAngleTimeSignal(timeRange, getPointFunc, getTangentFunc);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal CreateLinearCycles(double cycleTime, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteRamp()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(0, Math.Tau)
                .Repeat(cycleCount);

        return new LinFloat64DirectedAngleTimeSignal(
            baseSignal,
            r => r.RadiansToDirectedAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal CreateCosWaveCycles(double cycleTime, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteCos()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(0, Math.Tau)
                .Repeat(cycleCount);

        return new LinFloat64DirectedAngleTimeSignal(
            baseSignal,
            r => r.RadiansToDirectedAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal CreateCosWaveCycles(double cycleTime, LinFloat64DirectedAngle angle1, LinFloat64DirectedAngle angle2, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteCos()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(angle1.RadiansValue, angle2.RadiansValue)
                .Repeat(cycleCount);

        return new LinFloat64DirectedAngleTimeSignal(
            baseSignal,
            r => r.RadiansToDirectedAngle()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal CreateFromRadians(Float64ScalarSignal baseSignal)
    {
        return new LinFloat64DirectedAngleTimeSignal(
            baseSignal,
            r => r.RadiansToDirectedAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal CreateFromDegrees(Float64ScalarSignal baseSignal)
    {
        return new LinFloat64DirectedAngleTimeSignal(
            baseSignal,
            r => r.DegreesToDirectedAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64DirectedAngleTimeSignal Create(Float64Trajectory<double> baseSignal, Func<double, LinFloat64DirectedAngle> valueMap)
    {
        return new LinFloat64DirectedAngleTimeSignal(baseSignal, valueMap);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64DirectedAngleTimeSignal(Float64Trajectory<double> baseSignal, Func<double, LinFloat64DirectedAngle> valueMap)
        : base(baseSignal, valueMap)
    {
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseSignal.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToFinite()
    {
        return ToFiniteDirectedAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToPeriodic()
    {
        return ToPeriodicDirectedAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal ToFiniteDirectedAngle()
    {
        return IsFinite
            ? this
            : new LinFloat64DirectedAngleTimeSignal(
                (Float64Trajectory<double>) BaseSignal.ToFinite(),
                ValueMap
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal ToPeriodicDirectedAngle()
    {
        return IsPeriodic
            ? this
            : new LinFloat64DirectedAngleTimeSignal(
                (Float64Trajectory<double>) BaseSignal.ToPeriodic(),
                ValueMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngle GetAngle(double t)
    {
        return GetValue(t);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSignal RadiansToTimeSignal()
    {
        return Float64ScalarSignal.FiniteComputed(
            TimeRange, 
            t => GetValue(t).RadiansValue
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Float64ScalarSignal DegreesToTimeSignal()
    {
        return Float64ScalarSignal.FiniteComputed(
            TimeRange, 
            t => GetValue(t).RadiansValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal MapAngles(Func<LinFloat64DirectedAngle, LinFloat64DirectedAngle> angleMapping)
    {
        return new LinFloat64DirectedAngleTimeSignal(
            BaseSignal,
            r => angleMapping(ValueMap(r))
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal HalfPolarAngles()
    {
        return LinFloat64PolarAngleTimeSignal.Create(
            BaseSignal,
            r => ValueMap(r).HalfPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal HalfDirectedAngles()
    {
        return new LinFloat64DirectedAngleTimeSignal(
            BaseSignal,
            r => ValueMap(r).HalfDirectedAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal DoublePolarAngles()
    {
        return LinFloat64PolarAngleTimeSignal.Create(
            BaseSignal,
            r => ValueMap(r).DoublePolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal DoubleDirectedAngles()
    {
        return new LinFloat64DirectedAngleTimeSignal(
            BaseSignal,
            r => ValueMap(r).DoubleDirectedAngle()
        );
    }
}