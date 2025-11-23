namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFloat64GeometricObjectsContainer2D<out T> : 
    IFloat64BoundedShape2D, 
    IReadOnlyList<T>
    where T : IFloat64BoundedShape2D
{

}