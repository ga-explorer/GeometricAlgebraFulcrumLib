using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;

public interface IAccBih3D<out T>
    : IAccelerator3D<T> where T : IFloat64FiniteGeometricShape3D
{
    int BihDepth { get; }

    Float64BoundingBox3D BoundingBox { get; }

    IAccBihNode3D<T> RootNode { get; }
}