using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;

public interface ITransform3D
{
    LinFloat64Vector3D MapPoint(LinFloat64Vector3D point);
}