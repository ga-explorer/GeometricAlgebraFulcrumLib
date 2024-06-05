using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps;

public interface ITransform3D
{
    LinFloat64Vector3D MapPoint(LinFloat64Vector3D point);
}