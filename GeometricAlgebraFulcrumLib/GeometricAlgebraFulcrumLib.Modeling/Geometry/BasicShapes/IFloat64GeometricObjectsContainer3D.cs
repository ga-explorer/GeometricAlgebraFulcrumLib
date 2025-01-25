namespace GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

public interface IFloat64GeometricObjectsContainer3D<out T> : 
    IFloat64FiniteGeometricShape3D, 
    IReadOnlyList<T>
    where T : IFloat64FiniteGeometricShape3D
{

}