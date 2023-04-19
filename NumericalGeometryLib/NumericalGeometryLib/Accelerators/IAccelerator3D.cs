using GeometricAlgebraFulcrumLib.MathBase.BasicShapes;

namespace NumericalGeometryLib.Accelerators
{
    public interface IAccelerator3D<out T>
        : IGeometricObjectsContainer3D<T>, IIntersectable
        where T : IFiniteGeometricShape3D
    {
    }
}