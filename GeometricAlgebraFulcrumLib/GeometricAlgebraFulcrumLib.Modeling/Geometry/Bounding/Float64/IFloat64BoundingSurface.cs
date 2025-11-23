using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

/// <summary>
/// This interface represents a convex bounding surface in 
/// </summary>
public interface IFloat64BoundingSurface :
    IFloat64BoundedShape
{
    IFloat64BoundingSurface MapBoundingSurfaceUsing(IFloat64AffineMap affineMap);
}