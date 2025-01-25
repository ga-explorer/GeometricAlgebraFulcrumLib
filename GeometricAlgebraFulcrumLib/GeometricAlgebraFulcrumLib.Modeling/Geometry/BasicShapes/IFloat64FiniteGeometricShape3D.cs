using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFloat64FiniteGeometricShape3D :
    IAlgebraicElement,
    IIntersectable
{
    Float64BoundingBox3D GetBoundingBox();

    Float64BoundingBoxComposer3D GetBoundingBoxComposer();
}