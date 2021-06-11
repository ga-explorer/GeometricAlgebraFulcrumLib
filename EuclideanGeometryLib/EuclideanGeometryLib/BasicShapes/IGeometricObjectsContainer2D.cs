using System.Collections.Generic;

namespace EuclideanGeometryLib.BasicShapes
{
    public interface IGeometricObjectsContainer2D<out T> 
        : IFiniteGeometricShape2D, IReadOnlyList<T>
        where T : IFiniteGeometricShape2D
    {

    }
}