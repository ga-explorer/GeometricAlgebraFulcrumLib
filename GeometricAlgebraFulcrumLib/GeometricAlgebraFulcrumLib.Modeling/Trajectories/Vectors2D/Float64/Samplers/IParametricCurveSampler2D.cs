using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Trajectories.Vectors2D.Float64.Samplers;

public interface IParametricCurveSampler2D :
    IAlgebraicElement,
    IReadOnlyList<Float64Path2DLocalFrame>
{
    Float64Path2D Curve { get; }

    Float64ScalarRange ParameterRange { get; }

    bool IsPeriodic { get; }

    IEnumerable<double> GetParameterValues();

    IEnumerable<Float64ScalarRange> GetParameterSections();

    IEnumerable<LinFloat64Vector2D> GetPoints();

    IEnumerable<LinFloat64Vector2D> GetTangents();

    IEnumerable<Float64Path2DLocalFrame> GetFrames();
}