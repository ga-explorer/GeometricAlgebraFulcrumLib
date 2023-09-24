using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators
{
    public interface IAccelerator2D<out T> 
        : IGeometricObjectsContainer2D<T>, IIntersectable
        where T : IFiniteGeometricShape2D
    {
    }
}