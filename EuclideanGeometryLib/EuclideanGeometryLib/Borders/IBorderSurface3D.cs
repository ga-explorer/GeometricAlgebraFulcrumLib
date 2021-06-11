using EuclideanGeometryLib.BasicMath.Maps.Space3D;
using EuclideanGeometryLib.BasicShapes;

namespace EuclideanGeometryLib.Borders
{
    /// <summary>
    /// This interface represents a convex bounding surface in 3D
    /// </summary>
    public interface IBorderSurface3D : IFiniteGeometricShape3D
    {
        IBorderSurface3D MapUsing(IAffineMap3D affineMap);
    }
}