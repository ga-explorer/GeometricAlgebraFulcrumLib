using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.Borders.Space2D.Immutable;
using NumericalGeometryLib.Borders.Space2D.Mutable;

namespace NumericalGeometryLib.BasicShapes
{
    public interface IFiniteGeometricShape2D 
        : IGeometricElement, IIntersectable
    {
        BoundingBox2D GetBoundingBox();

        MutableBoundingBox2D GetMutableBoundingBox();
    }
}