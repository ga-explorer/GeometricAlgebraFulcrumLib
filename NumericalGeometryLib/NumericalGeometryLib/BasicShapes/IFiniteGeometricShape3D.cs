using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.Borders.Space3D.Immutable;
using NumericalGeometryLib.Borders.Space3D.Mutable;

namespace NumericalGeometryLib.BasicShapes
{
    public interface IFiniteGeometricShape3D : 
        IGeometricElement, 
        IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}