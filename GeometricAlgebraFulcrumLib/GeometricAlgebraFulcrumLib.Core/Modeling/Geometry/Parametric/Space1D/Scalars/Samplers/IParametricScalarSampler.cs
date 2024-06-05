using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space1D.Scalars.Samplers;

public interface IParametricScalarSampler :
    IAlgebraicElement,
    IReadOnlyList<ParametricScalarLocalFrame>
{
    IFloat64ParametricScalar Curve { get; }

    Float64ScalarRange ParameterRange { get; }
        
    bool IsPeriodic { get; }

    IEnumerable<double> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<double> GetPoints();

    IEnumerable<double> GetTangents();

    IEnumerable<ParametricScalarLocalFrame> GetFrames();
}