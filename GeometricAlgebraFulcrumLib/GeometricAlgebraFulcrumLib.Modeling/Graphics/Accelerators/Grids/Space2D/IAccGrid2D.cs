using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Tuples;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space2D;

public interface IAccGrid2D<out T> : IAccelerator2D<T>
    where T : IFloat64FiniteGeometricShape2D
{
    IReadOnlyList<T> this[int ix, int iy] { get; }

    Float64BoundingBox2D BoundingBox { get; }

    int CellsCountX { get; }

    int CellsCountY { get; }

    IntTuple2D PointToCellIndex(double x, double y);

    int PointXToCellIndex(double x);

    int PointYToCellIndex(double y);
}