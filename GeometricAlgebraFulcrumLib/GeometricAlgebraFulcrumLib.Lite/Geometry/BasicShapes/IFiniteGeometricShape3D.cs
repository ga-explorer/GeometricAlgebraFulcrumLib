using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes
{
    public interface IFiniteGeometricShape3D : 
        IGeometricElement, 
        IIntersectable
    {
        BoundingBox3D GetBoundingBox();

        MutableBoundingBox3D GetMutableBoundingBox();
    }
}