using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Mapped;

public sealed class Float64ScalarMappedTrajectorySignal<T> :
    Float64ScalarSignal
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Float64ScalarMappedTrajectorySignal<T> Create(Float64Trajectory<T> baseTrajectory, Func<T, double> valueMap)
    {
        return new Float64ScalarMappedTrajectorySignal<T>(baseTrajectory, valueMap);
    }


    public Float64Trajectory<T> BaseTrajectory { get; }

    public Func<T, double> ValueMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64ScalarMappedTrajectorySignal(Float64Trajectory<T> baseTrajectory, Func<T, double> valueMap)
        : base(baseTrajectory.TimeRange, baseTrajectory.IsPeriodic)
    {
        BaseTrajectory = baseTrajectory;
        ValueMap = valueMap;

        Debug.Assert(IsValid());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseTrajectory.IsValid();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToFiniteSignal()
    {
        return IsFinite 
            ? this 
            : new Float64ScalarMappedTrajectorySignal<T>(
                (Float64Trajectory<T>)BaseTrajectory.ToFinite(), 
                ValueMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64ScalarSignal ToPeriodicSignal()
    {
        return IsPeriodic 
            ? this 
            : new Float64ScalarMappedTrajectorySignal<T>(
                (Float64Trajectory<T>)BaseTrajectory.ToPeriodic(),
                ValueMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override double GetValue(double t)
    {
        t = this.ClampTime(t);

        var value = ValueMap(
            BaseTrajectory.GetValue(t)
        );

        Debug.Assert(value.IsFinite());

        return value;
    }

}