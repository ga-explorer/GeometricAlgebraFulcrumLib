using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFiniteGeometricShape2D
    : IAlgebraicElement, IIntersectable
{
    BoundingBox2D GetBoundingBox();

    MutableBoundingBox2D GetMutableBoundingBox();
}