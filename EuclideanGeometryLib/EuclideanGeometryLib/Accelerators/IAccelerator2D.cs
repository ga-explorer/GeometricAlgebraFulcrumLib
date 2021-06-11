using EuclideanGeometryLib.BasicShapes;

namespace EuclideanGeometryLib.Accelerators
{
    public interface IAccelerator2D<out T> 
        : IGeometricObjectsContainer2D<T>, IIntersectable
        where T : IFiniteGeometricShape2D
    {
    }
}