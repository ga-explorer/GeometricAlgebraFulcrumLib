using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;
using SixLabors.ImageSharp;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Parametric.Space2D.Curves;

public interface IParametricCurveLocalFrame2D :
    ILinFloat64Vector2D
{
    int Index { get; }

    LinFloat64Vector2D Point { get; }

    Color Color { get; set; }

    Float64Scalar ParameterValue { get; }

    LinFloat64Vector2D Tangent { get; }

    LinFloat64Vector2D Normal { get; }
}