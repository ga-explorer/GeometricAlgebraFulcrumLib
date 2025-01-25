using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;

public interface IAccelerator2D<out T> 
    : IFloat64GeometricObjectsContainer2D<T>, IIntersectable
    where T : IFloat64FiniteGeometricShape2D
{
}