using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Immutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;

public interface IAccBih3D<out T>
    : IAccelerator3D<T> where T : IFiniteGeometricShape3D
{
    int BihDepth { get; }

    BoundingBox3D BoundingBox { get; }

    IAccBihNode3D<T> RootNode { get; }
}