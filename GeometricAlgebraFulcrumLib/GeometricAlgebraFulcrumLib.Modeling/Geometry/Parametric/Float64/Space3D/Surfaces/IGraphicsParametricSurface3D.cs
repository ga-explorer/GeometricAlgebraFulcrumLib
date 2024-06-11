using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Parametric.Float64.Space3D.Surfaces;

public interface IGraphicsParametricSurface3D :
    IAlgebraicElement
{
    LinFloat64Vector3D GetPoint(double parameterValue1, double parameterValue2);

    LinFloat64Vector3D GetNormal(double parameterValue1, double parameterValue2);

    LinFloat64Vector3D GetUnitNormal(double parameterValue1, double parameterValue2);

    GrParametricSurfaceLocalFrame3D GetFrame(double parameterValue1, double parameterValue2);
}