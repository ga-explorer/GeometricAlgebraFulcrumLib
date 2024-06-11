using GeometricAlgebraFulcrumLib.Modeling.Geometry.AffineMaps.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;

/// <summary>
/// This interface represents a convex bounding surface in 3D
/// </summary>
public interface IBorderSurface3D : IFiniteGeometricShape3D
{
    IBorderSurface3D MapUsing(IAffineMap3D affineMap);
}