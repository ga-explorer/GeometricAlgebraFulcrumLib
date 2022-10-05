using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

/// <summary>
/// A parametric 3D curve with continuous first and second derivatives
/// </summary>
public interface IGraphicsC2ParametricCurve3D :
    IGraphicsC1ParametricCurve3D
{
    Tuple3D GetSecondDerivative(double parameterValue);
}