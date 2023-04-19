namespace GeometricAlgebraFulcrumLib.MathBase.BasicShapes
{
    public interface IGeometricObjectsContainer3D<out T>
        : IFiniteGeometricShape3D, IReadOnlyList<T>
        where T : IFiniteGeometricShape3D
    {

    }
}