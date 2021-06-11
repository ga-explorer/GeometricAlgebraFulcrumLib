using EuclideanGeometryLib.BasicMath;
using EuclideanGeometryLib.Borders.Space3D.Immutable;
using EuclideanGeometryLib.Borders.Space3D.Mutable;

namespace EuclideanGeometryLib.BasicShapes
{
    public interface IFiniteGeometricShape3D : 
        IGeometricElement, 
        IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}