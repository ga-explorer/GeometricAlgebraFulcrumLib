using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.ParametricShapes.Surfaces
{
    public interface IGraphicsParametricSurface3D : 
        IGeometricElement
    {
        Float64Tuple3D GetPoint(double parameterValue1, double parameterValue2);

        Float64Tuple3D GetNormal(double parameterValue1, double parameterValue2);

        Float64Tuple3D GetUnitNormal(double parameterValue1, double parameterValue2);

        GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2);
    }
}