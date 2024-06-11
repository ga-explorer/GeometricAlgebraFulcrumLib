using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;

public interface IAccelerator3D<out T>
    : IGeometricObjectsContainer3D<T>, IIntersectable
    where T : IFiniteGeometricShape3D
{
}