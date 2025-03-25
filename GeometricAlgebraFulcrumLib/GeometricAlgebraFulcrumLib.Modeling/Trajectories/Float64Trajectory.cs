using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public abstract class Float64Trajectory
    : IFloat64Trajectory
{
    public bool IsPeriodic { get; }

    public bool IsFinite 
        => !IsPeriodic;

    public Float64ScalarRange TimeRange { get; }

    public double MinTime 
        => TimeRange.MinValue;
    
    public double MidTime
        => TimeRange.MidValue;

    public double MaxTime 
        => TimeRange.MaxValue;

    public double TimeRangeLength
        => TimeRange.Length;

    
    protected Float64Trajectory(Float64ScalarRange timeRange, bool isPeriodic)
    {
        if (!timeRange.IsValid() || !timeRange.IsFinite)
            throw new ArgumentException(nameof(timeRange));

        TimeRange = timeRange;
        IsPeriodic = isPeriodic;
    }


    public abstract bool IsValid();
    
    public abstract IFloat64Trajectory ToFinite();

    public abstract IFloat64Trajectory ToPeriodic();
}

public abstract class Float64Trajectory<T>(Float64ScalarRange timeRange, bool isPeriodic) :
    Float64Trajectory(timeRange, isPeriodic),
    IFloat64Trajectory<T>
{
    public T ValueAtMinTime
        => GetValue(MinTime);

    public T ValueAtMidTime
        => GetValue(MidTime);

    public T ValueAtMaxTime
        => GetValue(MaxTime);


    public abstract T GetValue(double t);
}