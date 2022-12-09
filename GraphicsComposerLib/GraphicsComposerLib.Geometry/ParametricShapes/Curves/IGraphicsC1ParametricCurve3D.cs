using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Curves
{
    /// <summary>
    /// A parametric 3D curve with continuous first derivative
    /// </summary>
    public interface IGraphicsC1ParametricCurve3D : 
        IGeometricElement
    {
        Float64Tuple3D GetPoint(double parameterValue);

        Float64Tuple3D GetTangent(double parameterValue);

        Float64Tuple3D GetUnitTangent(double parameterValue);

        GrParametricCurveLocalFrame3D GetFrame(double parameterValue);
    }
}