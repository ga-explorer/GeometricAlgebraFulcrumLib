using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves.Samplers;

public interface IParametricCurveSampler2D :
    IAlgebraicElement,
    IReadOnlyList<ParametricCurveLocalFrame2D>
{
    IFloat64ParametricCurve2D Curve { get; }

    Float64ScalarRange ParameterRange { get; }
        
    bool IsPeriodic { get; }

    IEnumerable<Float64Scalar> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<LinFloat64Vector2D> GetPoints();

    IEnumerable<LinFloat64Vector2D> GetTangents();

    IEnumerable<ParametricCurveLocalFrame2D> GetFrames();
}