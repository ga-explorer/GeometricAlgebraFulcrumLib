using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public interface IGraphicsParametricCurve2D : 
        IGeometricElement
    {
        Tuple2D GetPoint(double t);

        Tuple2D GetTangent(double parameterValue);

        Tuple2D GetUnitTangent(double parameterValue);

        GrParametricCurveLocalFrame2D GetFrame(double parameterValue);
    }
}