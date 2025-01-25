using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;

public interface IAccelerator3D<out T>
    : IFloat64GeometricObjectsContainer3D<T>, IIntersectable
    where T : IFloat64FiniteGeometricShape3D
{
}