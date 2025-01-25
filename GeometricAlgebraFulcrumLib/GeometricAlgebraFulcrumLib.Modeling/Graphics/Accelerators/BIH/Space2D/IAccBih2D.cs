using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;

public interface IAccBih2D<out T>
    : IAccelerator2D<T> where T : IFloat64FiniteGeometricShape2D
{
    int BihDepth { get; }

    Float64BoundingBox2D BoundingBox { get; }

    IAccBihNode2D<T> RootNode { get; }
}