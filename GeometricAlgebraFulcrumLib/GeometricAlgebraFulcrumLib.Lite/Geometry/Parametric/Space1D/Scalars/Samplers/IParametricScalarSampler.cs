using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space1D.Scalars.Samplers;

public interface IParametricScalarSampler :
    IGeometricElement,
    IReadOnlyList<ParametricScalarLocalFrame>
{
    IParametricScalar Curve { get; }

    Float64ScalarRange ParameterRange { get; }
        
    bool IsPeriodic { get; }

    IEnumerable<double> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<double> GetPoints();

    IEnumerable<double> GetTangents();

    IEnumerable<ParametricScalarLocalFrame> GetFrames();
}