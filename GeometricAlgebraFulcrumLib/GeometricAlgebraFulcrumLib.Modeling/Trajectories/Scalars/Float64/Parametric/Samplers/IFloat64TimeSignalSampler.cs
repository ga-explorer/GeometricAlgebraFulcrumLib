using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Scalars.Float64.Parametric.Samplers;

public interface IFloat64ScalarSignalSampler :
    IAlgebraicElement,
    IReadOnlyList<ParametricScalarLocalFrame>
{
    Float64ScalarSignal Signal { get; }

    Float64ScalarRange TimeRange { get; }

    bool IsPeriodic { get; }

    IEnumerable<double> GetTimeValues();

    IEnumerable<Float64ScalarRange> GetTimeSections();

    IEnumerable<double> GetValues();

    IEnumerable<double> GetDerivative1Values();

    IEnumerable<ParametricScalarLocalFrame> GetFrames();
}