using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space4D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

/// <summary>
/// This interface represents a convex bounding surface in 4D
/// </summary>
public interface IFloat64BoundingSurface4D :
    IFloat64BoundedShape4D
{
    IFloat64BoundingSurface4D MapBoundingSurfaceUsing(IFloat64AffineMap4D affineMap);
}