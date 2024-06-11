using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Immutable;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space3D;

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