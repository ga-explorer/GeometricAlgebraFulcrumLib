using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

/// <summary>
/// This interface represents a convex bounding surface in 3D
/// </summary>
public interface IFloat64BoundingSurface3D :
    IFloat64BoundedShape3D
{
    IFloat64BoundingSurface3D MapBoundingSurfaceUsing(IFloat64AffineMap3D affineMap);
}