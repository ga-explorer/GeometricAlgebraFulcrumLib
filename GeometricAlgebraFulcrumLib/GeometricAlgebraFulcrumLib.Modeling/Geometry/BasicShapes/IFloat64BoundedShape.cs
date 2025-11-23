using GeometricAlgebraFulcrumLib.Algebra;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFloat64BoundedShape<out TBox> :
    IAlgebraicElement
{
    TBox GetBoundingBox();
}

public interface IFloat64BoundedShape2D : 
    IFloat64BoundedShape<Float64BoundingBox2D>, 
    IIntersectable
{
    Float64BoundingBoxComposer2D GetBoundingBoxComposer();
}

public interface IFloat64BoundedShape3D :
    IFloat64BoundedShape<Float64BoundingBox3D>,
    IIntersectable
{
    Float64BoundingBoxComposer3D GetBoundingBoxComposer();
}

public interface IFloat64BoundedShape4D : 
    IFloat64BoundedShape<Float64BoundingBox4D>, 
    IIntersectable
{
    Float64BoundingBoxComposer4D GetBoundingBoxComposer();
}

public interface IFloat64BoundedShape :
    IFloat64BoundedShape<Float64BoundingBox>,
    IIntersectable
{
    Float64BoundingBoxComposer GetBoundingBoxComposer();
}

