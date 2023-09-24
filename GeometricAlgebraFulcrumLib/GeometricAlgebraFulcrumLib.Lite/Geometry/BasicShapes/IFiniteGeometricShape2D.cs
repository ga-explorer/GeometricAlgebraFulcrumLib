using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometricElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}