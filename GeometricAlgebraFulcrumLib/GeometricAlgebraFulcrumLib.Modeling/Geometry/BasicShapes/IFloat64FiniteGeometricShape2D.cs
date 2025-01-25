using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFloat64FiniteGeometricShape2D : 
    IAlgebraicElement, 
    IIntersectable
{
    Float64BoundingBox2D GetBoundingBox();

    Float64BoundingBoxComposer2D GetBoundingBoxComposer();
}