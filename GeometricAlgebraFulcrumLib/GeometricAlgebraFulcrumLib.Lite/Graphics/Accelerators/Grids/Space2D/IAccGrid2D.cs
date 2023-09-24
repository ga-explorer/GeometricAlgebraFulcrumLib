using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.Grids.Space2D
{
    public interface IAccGrid2D<out T> : IAccelerator2D<T>
        where T : IFiniteGeometricShape2D
    {
        IReadOnlyList<T> this[int ix, int iy] { get; }

        BoundingBox2D BoundingBox { get; }

        int CellsCountX { get; }

        int CellsCountY { get; }

        IntTuple2D PointToCellIndex(double x, double y);

        int PointXToCellIndex(double x);

        int PointYToCellIndex(double y);
    }
}