using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.BIH.Space3D
{
    public interface IAccBih3D<out T>
        : IAccelerator3D<T> where T : IFiniteGeometricShape3D
    {
        int BihDepth { get; }

        BoundingBox3D BoundingBox { get; }

        IAccBihNode3D<T> RootNode { get; }
    }
}