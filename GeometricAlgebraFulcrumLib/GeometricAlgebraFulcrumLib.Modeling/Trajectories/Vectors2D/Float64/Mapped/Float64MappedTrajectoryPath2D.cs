using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Mapped;

public sealed class Float64MappedTrajectoryPath2D<T> :
    Float64Path2D
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Float64MappedTrajectoryPath2D<T> Create(Float64Trajectory<T> baseTrajectory, Func<T, LinFloat64Vector2D> scalarMap)
    {
        return new Float64MappedTrajectoryPath2D<T>(baseTrajectory, scalarMap);
    }


    public Float64Trajectory<T> BaseTrajectory { get; }

    public Func<T, LinFloat64Vector2D> ScalarMap { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private Float64MappedTrajectoryPath2D(Float64Trajectory<T> baseTrajectory, Func<T, LinFloat64Vector2D> scalarMap)
        : base(baseTrajectory.TimeRange, baseTrajectory.IsPeriodic)
    {
        BaseTrajectory = baseTrajectory;
        ScalarMap = scalarMap;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool IsValid()
    {
        return BaseTrajectory.IsValid();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override LinFloat64Vector2D GetValue(double t)
    {
        return ScalarMap(BaseTrajectory.GetValue(t));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToFinitePath()
    {
        return IsFinite
            ? this
            : new Float64MappedTrajectoryPath2D<T>(
                (Float64Trajectory<T>)BaseTrajectory.ToFinite(), 
                ScalarMap
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Path2D ToPeriodicPath()
    {
        return IsPeriodic
            ? this
            : new Float64MappedTrajectoryPath2D<T>(
                (Float64Trajectory<T>)BaseTrajectory.ToFinite(), 
                ScalarMap
            );
    }

}