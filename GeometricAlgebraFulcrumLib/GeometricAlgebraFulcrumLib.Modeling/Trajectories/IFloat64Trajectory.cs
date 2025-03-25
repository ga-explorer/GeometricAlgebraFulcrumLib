using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories;

public interface IFloat64Trajectory :
    IAlgebraicElement
{
    bool IsPeriodic { get; }

    Float64ScalarRange TimeRange { get; }

    double MinTime { get; }

    double MaxTime { get; }

    double MidTime { get; }

    double TimeRangeLength { get; }

    IFloat64Trajectory ToFinite();

    IFloat64Trajectory ToPeriodic();
}

public interface IFloat64Trajectory<out T> :
    IFloat64Trajectory
{
    T ValueAtMinTime { get; }

    T ValueAtMidTime { get; }

    T ValueAtMaxTime { get; }

    T GetValue(double t);
}