using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Angles;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Angles;

public sealed class LinFloat64PolarAngleTimeSignal :
    Float64TrajectoryConverter<double, LinFloat64PolarAngle>
{
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngleTimeSignal Create(Func<double, LinFloat64PolarAngle> getPointFunc)
    //{
    //    return new LinFloat64PolarAngleTimeSignal(Float64ScalarRange.Infinite, getPointFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngleTimeSignal Create(Float64ScalarRange timeRange, Func<double, LinFloat64PolarAngle> getPointFunc)
    //{
    //    return new LinFloat64PolarAngleTimeSignal(timeRange, getPointFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngleTimeSignal Create(Func<double, LinFloat64PolarAngle> getPointFunc, Func<double, LinFloat64PolarAngle> getTangentFunc)
    //{
    //    return new LinFloat64PolarAngleTimeSignal(Float64ScalarRange.Infinite, getPointFunc, getTangentFunc);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static LinFloat64PolarAngleTimeSignal Create(Float64ScalarRange timeRange, Func<double, LinFloat64PolarAngle> getPointFunc, Func<double, LinFloat64PolarAngle> getTangentFunc)
    //{
    //    return new LinFloat64PolarAngleTimeSignal(timeRange, getPointFunc, getTangentFunc);
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateLinearCycles(double cycleTime, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteRamp()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(0, Math.Tau)
                .Repeat(cycleCount);

        return new LinFloat64PolarAngleTimeSignal(
            baseSignal, 
            r => r.RadiansToPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateCosWaveCycles(double cycleTime, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteCos()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(0, Math.Tau)
                .Repeat(cycleCount);

        return new LinFloat64PolarAngleTimeSignal(
            baseSignal, 
            r => r.RadiansToPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateCosWaveCycles(double cycleTime, LinFloat64PolarAngle angle1, LinFloat64PolarAngle angle2, int cycleCount = 1)
    {
        var baseSignal = 
            Float64ScalarSignal
                .FiniteCos()
                .MapTimeRangeTo(0, cycleTime)
                .MapValueRangeTo(angle1.RadiansValue, angle2.RadiansValue)
                .Repeat(cycleCount);

        return new LinFloat64PolarAngleTimeSignal(
            baseSignal, 
            r => r.RadiansToPolarAngle()
        );
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal FiniteConstant(Float64ScalarRange timeRange, LinFloat64PolarAngle angle)
    {
        return new LinFloat64PolarAngleTimeSignal(
            Float64ScalarSignal.FiniteConstant(timeRange, angle.RadiansValue),
            r => r.RadiansToPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal PeriodicConstant(Float64ScalarRange timeRange, LinFloat64PolarAngle angle)
    {
        return new LinFloat64PolarAngleTimeSignal(
            Float64ScalarSignal.PeriodicConstant(timeRange, angle.RadiansValue),
            r => r.RadiansToPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateConstant(Float64ScalarRange timeRange, bool isPeriodic, LinFloat64PolarAngle angle)
    {
        return new LinFloat64PolarAngleTimeSignal(
            Float64ScalarSignal.CreateConstant(timeRange, isPeriodic, angle.RadiansValue),
            r => r.RadiansToPolarAngle()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateFromRadians(Float64ScalarSignal baseSignal)
    {
        return new LinFloat64PolarAngleTimeSignal(
            baseSignal, 
            r => r.RadiansToPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal CreateFromDegrees(Float64ScalarSignal baseSignal)
    {
        return new LinFloat64PolarAngleTimeSignal(
            baseSignal, 
            r => r.DegreesToPolarAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinFloat64PolarAngleTimeSignal Create(Float64Trajectory<double> baseSignal, Func<double, LinFloat64PolarAngle> valueMap)
    {
        return new LinFloat64PolarAngleTimeSignal(baseSignal, valueMap);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private LinFloat64PolarAngleTimeSignal(Float64Trajectory<double> baseSignal, Func<double, LinFloat64PolarAngle> valueMap)
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
        return ToFinitePolarAngle();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IFloat64Trajectory ToPeriodic()
    {
        return ToPeriodicPolarAngle();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal ToFinitePolarAngle()
    {
        return IsFinite
            ? this
            : new LinFloat64PolarAngleTimeSignal(
                (Float64Trajectory<double>) BaseSignal.ToFinite(),
                ValueMap
            );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal ToPeriodicPolarAngle()
    {
        return IsPeriodic
            ? this
            : new LinFloat64PolarAngleTimeSignal(
                (Float64Trajectory<double>) BaseSignal.ToPeriodic(),
                ValueMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngle GetAngle(double t)
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
            t => GetValue(t).DegreesValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal MapAngles(Func<LinFloat64PolarAngle, LinFloat64PolarAngle> angleMapping)
    {
        return new LinFloat64PolarAngleTimeSignal(
            BaseSignal,
            r => angleMapping(ValueMap(r))
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal HalfPolarAngles()
    {
        return new LinFloat64PolarAngleTimeSignal(
            BaseSignal,
            r => ValueMap(r).HalfPolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal HalfDirectedAngles()
    {
        return LinFloat64DirectedAngleTimeSignal.Create(
            BaseSignal,
            r => ValueMap(r).HalfDirectedAngle()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64PolarAngleTimeSignal DoublePolarAngles()
    {
        return new LinFloat64PolarAngleTimeSignal(
            BaseSignal,
            r => ValueMap(r).DoublePolarAngle()
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinFloat64DirectedAngleTimeSignal DoubleDirectedAngles()
    {
        return LinFloat64DirectedAngleTimeSignal.Create(
            BaseSignal,
            r => ValueMap(r).DoubleDirectedAngle()
        );
    }

    
}