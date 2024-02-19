using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators;

public interface IAccelerator3D<out T>
    : IGeometricObjectsContainer3D<T>, IIntersectable
    where T : IFiniteGeometricShape3D
{
}