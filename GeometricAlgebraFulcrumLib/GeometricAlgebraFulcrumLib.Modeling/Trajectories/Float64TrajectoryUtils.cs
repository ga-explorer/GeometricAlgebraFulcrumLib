using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public static class Float64TrajectoryUtils
{
    


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsTime(this IFloat64Trajectory value, double t)
    {
        return value.TimeRange.Contains(t);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ClampTime(this IFloat64Trajectory value, double t)
    {
        return value.IsPeriodic
            ? t.ClampPeriodic(value.MinTime, value.MaxTime)
            : t.Clamp(value.MinTime, value.MaxTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ClampTime(this IFloat64Trajectory value, double t, bool clampPeriodic)
    {
        return clampPeriodic
            ? t.ClampPeriodic(value.MinTime, value.MaxTime)
            : t.Clamp(value.MinTime, value.MaxTime);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ClampTime(this IFloat64Trajectory value, Float64Scalar t, bool clampPeriodic)
    {
        return value.ClampTime(t.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar ClampTime(this IFloat64Trajectory value, IFloat64Scalar t, bool clampPeriodic)
    {
        return value.ClampTime(t.ScalarValue, clampPeriodic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetRelativeTime(this IFloat64Trajectory value, double t, bool clampPeriodic)
    {
        t = value.ClampTime(t, clampPeriodic);

        return ((t - value.MinTime) / (value.MaxTime - value.MinTime)).ClampToUnit();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetRelativeTime(this IFloat64Trajectory value, Float64Scalar t, bool clampPeriodic)
    {
        return value.GetRelativeTime(t.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetRelativeTime(this IFloat64Trajectory value, IFloat64Scalar t, bool clampPeriodic)
    {
        return value.GetRelativeTime(t.ScalarValue, clampPeriodic);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double GetTime(this IFloat64Trajectory value, double tLerp, bool clampPeriodic)
    {
        var x = clampPeriodic 
            ? tLerp.ClampPeriodicToUnit() 
            : tLerp.ClampToUnit();

        var t = (1 - x) * value.MinTime + x * value.MaxTime;

        return value.ClampTime(t, false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetTime(this IFloat64Trajectory value, Float64Scalar tLerp, bool clampPeriodic)
    {
        return value.GetTime(tLerp.ScalarValue, clampPeriodic);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64Scalar GetTime(this IFloat64Trajectory value, IFloat64Scalar tLerp, bool clampPeriodic)
    {
        return value.GetTime(tLerp.ScalarValue, clampPeriodic);
    }


}