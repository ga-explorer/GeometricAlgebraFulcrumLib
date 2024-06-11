using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFiniteGeometricShape3D :
    IAlgebraicElement,
    IIntersectable
{
    BoundingBox3D GetBoundingBox();

    MutableBoundingBox3D GetMutableBoundingBox();
}