using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    public interface IGraphicsParametricCurve3D : 
        IGeometricElement
    {
        Tuple3D GetPoint(double parameterValue);

        Tuple3D GetTangent(double parameterValue);

        Tuple3D GetUnitTangent(double parameterValue);

        GrParametricCurveLocalFrame3D GetFrame(double parameterValue);
    }
}