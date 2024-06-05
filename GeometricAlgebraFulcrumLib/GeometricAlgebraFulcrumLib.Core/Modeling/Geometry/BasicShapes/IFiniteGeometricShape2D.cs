using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space2D.Mutable;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;

public interface IFiniteGeometricShape2D 
    : IAlgebraicElement, IIntersectable
{
    BoundingBox2D GetBoundingBox();

    MutableBoundingBox2D GetMutableBoundingBox();
}