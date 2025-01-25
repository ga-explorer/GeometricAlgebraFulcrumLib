using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space3D;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids;

public static class GridsUtils
{
    public static AccGrid2D<T> ToGrid2D<T>(this IReadOnlyList<T> geometricObjectsList)
        where T : IFloat64FiniteGeometricShape2D
    {
        return new AccGrid2D<T>(geometricObjectsList);
    }

    public static AccGridInfo2D GetGridInfo(this IAccGrid2D<IFloat64FiniteGeometricShape2D> grid)
    {
        return AccGridInfo2D.Create(grid);
    }

    public static double GetCellLengthX<T>(this IAccGrid2D<T> grid)
        where T : IFloat64FiniteGeometricShape2D
    {
        return grid.BoundingBox.GetLengthX() / grid.CellsCountX;
    }

    public static double GetCellLengthY<T>(this IAccGrid2D<T> grid)
        where T : IFloat64FiniteGeometricShape2D
    {
        return grid.BoundingBox.GetLengthY() / grid.CellsCountY;
    }

    public static Float64BoundingBox2D GetCellBoundingBox(this IAccGrid2D<IFloat64FiniteGeometricShape2D> grid, int indexX, int indexY)
    {
        var cellLengthX = grid.BoundingBox.GetLengthX() / grid.CellsCountX;
        var cellLengthY = grid.BoundingBox.GetLengthY() / grid.CellsCountY;

        var minX = grid.BoundingBox.MinX + indexX * cellLengthX;
        var minY = grid.BoundingBox.MinY + indexY * cellLengthY;

        var maxX = grid.BoundingBox.MinX + (indexX + 1) * cellLengthX;
        var maxY = grid.BoundingBox.MinY + (indexY + 1) * cellLengthY;

        return new Float64BoundingBox2D(minX, minY, maxX, maxY);
    }

    public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFloat64FiniteGeometricShape2D> grid, IFloat64Line2D line)
    {
        return AccGridLineTraverser2D.Create(grid, line);
    }

    public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFloat64FiniteGeometricShape2D> grid, IFloat64Line2D line, double lineParamValue1, double lineParamValue2)
    {
        return AccGridLineTraverser2D.Create(
            grid,
            line,
            Float64ScalarRange.Create(lineParamValue1, lineParamValue2)
        );
    }

    public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFloat64FiniteGeometricShape2D> grid, IFloat64Line2D line, Float64ScalarRange lineParamRange)
    {
        return AccGridLineTraverser2D.Create(
            grid,
            line,
            lineParamRange
        );
    }


    public static AccGrid3D<T> ToGrid3D<T>(this IReadOnlyList<T> geometricObjectsList)
        where T : IFloat64FiniteGeometricShape3D
    {
        return new AccGrid3D<T>(geometricObjectsList);
    }

    public static AccGridInfo3D GetGridInfo(this IAccGrid3D<IFloat64FiniteGeometricShape3D> grid)
    {
        return AccGridInfo3D.Create(grid);
    }

    public static double GetCellLengthX<T>(this IAccGrid3D<T> grid)
        where T : IFloat64FiniteGeometricShape3D
    {
        return grid.BoundingBox.GetLengthX() / grid.CellsCountX;
    }

    public static double GetCellLengthY<T>(this IAccGrid3D<T> grid)
        where T : IFloat64FiniteGeometricShape3D
    {
        return grid.BoundingBox.GetLengthY() / grid.CellsCountY;
    }

    public static Float64BoundingBox3D GetCellBoundingBox(this IAccGrid3D<IFloat64FiniteGeometricShape3D> grid, int indexX, int indexY, int indexZ)
    {
        var cellLengthX = grid.BoundingBox.GetLengthX() / grid.CellsCountX;
        var cellLengthY = grid.BoundingBox.GetLengthY() / grid.CellsCountY;
        var cellLengthZ = grid.BoundingBox.GetLengthZ() / grid.CellsCountZ;

        var minX = grid.BoundingBox.MinX + indexX * cellLengthX;
        var minY = grid.BoundingBox.MinY + indexY * cellLengthY;
        var minZ = grid.BoundingBox.MinZ + indexZ * cellLengthZ;

        var maxX = grid.BoundingBox.MinX + (indexX + 1) * cellLengthX;
        var maxY = grid.BoundingBox.MinY + (indexY + 1) * cellLengthY;
        var maxZ = grid.BoundingBox.MinZ + (indexZ + 1) * cellLengthZ;

        return new Float64BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
    }

    public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFloat64FiniteGeometricShape3D> grid, IFloat64Line3D line)
    {
        return AccGridLineTraverser3D.Create(grid, line);
    }

    public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFloat64FiniteGeometricShape3D> grid, IFloat64Line3D line, double lineParamValue1, double lineParamValue2)
    {
        return AccGridLineTraverser3D.Create(
            grid,
            line,
            Float64ScalarRange.Create(lineParamValue1, lineParamValue2)
        );
    }

    public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFloat64FiniteGeometricShape3D> grid, IFloat64Line3D line, Float64ScalarRange lineParamRange)
    {
        return AccGridLineTraverser3D.Create(
            grid,
            line,
            lineParamRange
        );
    }
}