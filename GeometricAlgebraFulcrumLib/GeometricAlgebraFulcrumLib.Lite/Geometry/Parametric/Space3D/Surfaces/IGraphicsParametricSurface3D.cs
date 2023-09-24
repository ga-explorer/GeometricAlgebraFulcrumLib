using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Parametric.Space3D.Surfaces
{
    public interface IGraphicsParametricSurface3D : 
        IGeometricElement
    {
        Float64Vector3D GetPoint(double parameterValue1, double parameterValue2);

        Float64Vector3D GetNormal(double parameterValue1, double parameterValue2);

        Float64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2);

        GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2);
    }
}