using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Temporal;

public interface ITemporalValue :
    IAlgebraicElement
{
    Float64ScalarRange TimeRange { get; }

    double MinTime { get; }

    double MaxTime { get; }

    double MidTime { get; }

    double TimeRangeLength { get; }
}

public interface ITemporalValue<out T> :
    ITemporalValue
{
    T ValueAtMinTime { get; }

    T ValueAtMidTime { get; }

    T ValueAtMaxTime { get; }

    T GetValue(double t);
}