using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators;

public interface IAccelerator2D<out T> 
    : IGeometricObjectsContainer2D<T>, IIntersectable
    where T : IFiniteGeometricShape2D
{
}