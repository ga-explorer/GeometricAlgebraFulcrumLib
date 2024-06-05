using GeometricAlgebraFulcrumLib.Core.Algebra;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.Borders.Space3D.Mutable;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;

public interface IFiniteGeometricShape3D : 
    IAlgebraicElement, 
    IIntersectable
{
    BoundingBox3D GetBoundingBox();

    MutableBoundingBox3D GetMutableBoundingBox();
}