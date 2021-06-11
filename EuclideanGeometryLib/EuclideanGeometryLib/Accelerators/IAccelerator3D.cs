using EuclideanGeometryLib.BasicShapes;

namespace EuclideanGeometryLib.Accelerators
{
    public interface IAccelerator3D<out T>
        : IGeometricObjectsContainer3D<T>, IIntersectable
        where T : IFiniteGeometricShape3D
    {
    }
}