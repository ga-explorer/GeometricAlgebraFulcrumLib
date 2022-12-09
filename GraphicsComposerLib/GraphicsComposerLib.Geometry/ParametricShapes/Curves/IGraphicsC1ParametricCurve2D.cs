using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves;

/// <summary>
/// A parametric 2D curve with continuous first derivative
/// </summary>
public interface IGraphicsC1ParametricCurve2D : 
    IGeometricElement
{
    Float64Tuple2D GetPoint(double parameterValue);

    Float64Tuple2D GetTangent(double parameterValue);

    Float64Tuple2D GetUnitTangent(double parameterValue);

    GrParametricCurveLocalFrame2D GetFrame(double parameterValue);
}