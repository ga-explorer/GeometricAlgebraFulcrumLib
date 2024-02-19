using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space3D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.Grids.Space3D;

public interface IAccGrid3D<out T> : IAccelerator3D<T>
    where T : IFiniteGeometricShape3D
{
    IReadOnlyList<T> this[int ix, int iy, int iz] { get; }

    BoundingBox3D BoundingBox { get; }

    int CellsCountX { get; }

    int CellsCountY { get; }

    int CellsCountZ { get; }

    IntTuple3D PointToCellIndex(double x, double y, double z);

    int PointXToCellIndex(double x);

    int PointYToCellIndex(double y);

    int PointZToCellIndex(double z);
}