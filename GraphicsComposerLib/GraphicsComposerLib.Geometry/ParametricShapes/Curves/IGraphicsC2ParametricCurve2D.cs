using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

/// <summary>
/// A parametric 2D curve with continuous first and second derivatives
/// </summary>
public interface IGraphicsC2ParametricCurve2D :
    IGraphicsC1ParametricCurve2D
{
    Tuple2D GetSecondDerivative(double parameterValue);
}