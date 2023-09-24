using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Maps.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.Borders
{
    /// <summary>
    /// This interface represents a convex bounding surface in 3D
    /// </summary>
    public interface IBorderSurface3D : IFiniteGeometricShape3D
    {
        IBorderSurface3D MapUsing(IAffineMap3D affineMap);
    }
}