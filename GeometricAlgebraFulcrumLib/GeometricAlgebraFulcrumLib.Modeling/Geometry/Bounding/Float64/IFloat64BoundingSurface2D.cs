using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

/// <summary>
/// This interface represents a convex bounding surface (i.e. a closed convex curve) in 2D
/// </summary>
public interface IFloat64BoundingSurface2D :
    IFloat64BoundedShape2D,
    IIntersectable
{
    IFloat64BoundingSurface2D MapUsing(IFloat64AffineMap2D affineMap);
}