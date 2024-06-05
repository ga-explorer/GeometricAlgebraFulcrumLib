using GeometricAlgebraFulcrumLib.Core.Modeling.Geometry.BasicShapes;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Accelerators;

public interface IAccelerator2D<out T> 
    : IGeometricObjectsContainer2D<T>, IIntersectable
    where T : IFiniteGeometricShape2D
{
}