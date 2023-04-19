using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometricElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}