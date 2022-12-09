using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public interface IGraphicsParametricCurve2D : 
        IGeometricElement
    {
        Float64Tuple2D GetPoint(double t);

        Float64Tuple2D GetTangent(double parameterValue);

        Float64Tuple2D GetUnitTangent(double parameterValue);

        GrParametricCurveLocalFrame2D GetFrame(double parameterValue);
    }
}