using EuclideanGeometryLib.BasicShapes;
using EuclideanGeometryLib.Borders.Space2D.Immutable;

namespace EuclideanGeometryLib.Accelerators.BIH.Space2D
{
    public interface IAccBih2D<out T>
        : IAccelerator2D<T> where T : IFiniteGeometricShape2D
    {
        int BihDepth { get; }

        BoundingBox2D BoundingBox { get; }

        IAccBihNode2D<T> RootNode { get; }
    }
}