using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

/// <summary>
/// A parametric 2D curve with continuous first derivative
/// </summary>
public interface IGraphicsC1ParametricCurve2D : 
    IGeometricElement
{
    Tuple2D GetPoint(double parameterValue);

    Tuple2D GetTangent(double parameterValue);

    Tuple2D GetUnitTangent(double parameterValue);

    GrParametricCurveLocalFrame2D GetFrame(double parameterValue);
}