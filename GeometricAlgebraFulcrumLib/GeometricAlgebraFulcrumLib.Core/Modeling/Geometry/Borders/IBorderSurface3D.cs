using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Maps.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders;

/// <summary>
/// This interface represents a convex bounding surface in 3D
/// </summary>
public interface IBorderSurface3D : IFiniteGeometricShape3D
{
    IBorderSurface3D MapUsing(IAffineMap3D affineMap);
}