using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Bounding.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIHold.Space3D;

public interface IAccBih3D<out T>
    : IAccelerator3D<T> where T : IFloat64BoundedShape3D
{
    int BihDepth { get; }

    Float64BoundingBox3D BoundingBox { get; }

    IAccBihNode3D<T> RootNode { get; }
}