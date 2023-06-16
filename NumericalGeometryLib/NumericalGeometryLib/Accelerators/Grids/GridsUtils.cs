using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders.Space3D.Immutable;
using NumericalGeometryLib.Accelerators.Grids.Space2D;
using NumericalGeometryLib.Accelerators.Grids.Space3D;

namespace NumericalGeometryLib.Accelerators.Grids
{
    public static class GridsUtils
    {
        public static AccGrid2D<T> ToGrid2D<T>(this IReadOnlyList<T> geometricObjectsList)
            where T : IFiniteGeometricShape2D
        {
            return new AccGrid2D<T>(geometricObjectsList);
        }

        public static AccGridInfo2D GetGridInfo(this IAccGrid2D<IFiniteGeometricShape2D> grid)
        {
            return AccGridInfo2D.Create(grid);
        }

        public static double GetCellLengthX<T>(this IAccGrid2D<T> grid)
            where T : IFiniteGeometricShape2D
        {
            return grid.BoundingBox.GetLengthX() / grid.CellsCountX;
        }

        public static double GetCellLengthY<T>(this IAccGrid2D<T> grid)
            where T : IFiniteGeometricShape2D
        {
            return grid.BoundingBox.GetLengthY() / grid.CellsCountY;
        }

        public static BoundingBox2D GetCellBoundingBox(this IAccGrid2D<IFiniteGeometricShape2D> grid, int indexX, int indexY)
        {
            var cellLengthX = grid.BoundingBox.GetLengthX() / grid.CellsCountX;
            var cellLengthY = grid.BoundingBox.GetLengthY() / grid.CellsCountY;

            var minX = grid.BoundingBox.MinX + indexX * cellLengthX;
            var minY = grid.BoundingBox.MinY + indexY * cellLengthY;

            var maxX = grid.BoundingBox.MinX + (indexX + 1) * cellLengthX;
            var maxY = grid.BoundingBox.MinY + (indexY + 1) * cellLengthY;

            return new BoundingBox2D(minX, minY, maxX, maxY);
        }

        public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line)
        {
            return AccGridLineTraverser2D.Create(grid, line);
        }

        public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line, double lineParamValue1, double lineParamValue2)
        {
            return AccGridLineTraverser2D.Create(
                grid,
                line,
                Float64Range1D.Create(lineParamValue1, lineParamValue2)
            );
        }

        public static AccGridLineTraverser2D GetLineTraverser(this IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line, Float64Range1D lineParamRange)
        {
            return AccGridLineTraverser2D.Create(
                grid,
                line,
                lineParamRange
            );
        }


        public static AccGrid3D<T> ToGrid3D<T>(this IReadOnlyList<T> geometricObjectsList)
            where T : IFiniteGeometricShape3D
        {
            return new AccGrid3D<T>(geometricObjectsList);
        }

        public static AccGridInfo3D GetGridInfo(this IAccGrid3D<IFiniteGeometricShape3D> grid)
        {
            return AccGridInfo3D.Create(grid);
        }

        public static double GetCellLengthX<T>(this IAccGrid3D<T> grid)
            where T : IFiniteGeometricShape3D
        {
            return grid.BoundingBox.GetLengthX() / grid.CellsCountX;
        }

        public static double GetCellLengthY<T>(this IAccGrid3D<T> grid)
            where T : IFiniteGeometricShape3D
        {
            return grid.BoundingBox.GetLengthY() / grid.CellsCountY;
        }

        public static BoundingBox3D GetCellBoundingBox(this IAccGrid3D<IFiniteGeometricShape3D> grid, int indexX, int indexY, int indexZ)
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

            return new BoundingBox3D(minX, minY, minZ, maxX, maxY, maxZ);
        }

        public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line)
        {
            return AccGridLineTraverser3D.Create(grid, line);
        }

        public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line, double lineParamValue1, double lineParamValue2)
        {
            return AccGridLineTraverser3D.Create(
                grid,
                line,
                Float64Range1D.Create(lineParamValue1, lineParamValue2)
            );
        }

        public static AccGridLineTraverser3D GetLineTraverser(this IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line, Float64Range1D lineParamRange)
        {
            return AccGridLineTraverser3D.Create(
                grid,
                line,
                lineParamRange
            );
        }
    }
}
