using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space2D.Curves.Samplers;

public interface IParametricCurveSampler2D :
    IGeometricElement,
    IReadOnlyList<ParametricCurveLocalFrame2D>
{
    IParametricCurve2D Curve { get; }

    Float64ScalarRange ParameterRange { get; }
        
    bool IsPeriodic { get; }

    IEnumerable<Float64Scalar> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<Float64Vector2D> GetPoints();

    IEnumerable<Float64Vector2D> GetTangents();

    IEnumerable<ParametricCurveLocalFrame2D> GetFrames();
}