using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometricElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}