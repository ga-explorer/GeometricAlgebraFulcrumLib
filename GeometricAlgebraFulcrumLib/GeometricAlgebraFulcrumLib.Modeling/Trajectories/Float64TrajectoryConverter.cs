using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public abstract class Float64TrajectoryConverter<T1, T>(Float64Trajectory<T1> baseSignal, Func<T1, T> valueMap) :
    Float64Trajectory<T>(baseSignal.TimeRange, baseSignal.IsPeriodic)
{
    public Float64Trajectory<T1> BaseSignal { get; } = baseSignal;

    public Func<T1, T> ValueMap { get; } = valueMap;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetValue(double t)
    {
        return ValueMap( 
            BaseSignal.GetValue(t)
        );
    }

}