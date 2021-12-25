using System.Collections.Generic;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space2D.Immutable;

namespace NumericalGeometryLib.Accelerators.Grids.Space2D
{
    public sealed class AccGridInfo2D
    {
        public static AccGridInfo2D Create(IAccGrid2D<IFiniteGeometricShape2D> grid)
        {
            return new AccGridInfo2D(grid);
        }


        public IAccGrid2D<IFiniteGeometricShape2D> Grid { get; }

        public int CellsCountX
        {
            get { return Grid.CellsCountX; }
        }

        public int CellsCountY
        {
            get { return Grid.CellsCountY; }
        }

        public int CellsCount
        {
            get { return CellsCountX * CellsCountY; }
        }

        public BoundingBox2D BoundingBox { get; }

        public AccGridCellInfo2D this[int indexX, int indexY]
        {
            get
            {
                var gridCell = Grid[indexX, indexY];

                var cellBoundingBox = Grid.GetCellBoundingBox(indexX, indexY);

                return ReferenceEquals(gridCell, null)
                    ? new AccGridCellInfo2D(indexX, indexY, cellBoundingBox)
                    : new AccGridCellInfo2D(indexX, indexY, cellBoundingBox, gridCell);
            }
        }

        public IEnumerable<AccGridCellInfo2D> GridCells
        {
            get
            {
                for (var ix = 0; ix < Grid.CellsCountX; ix++)
                for (var iy = 0; iy < Grid.CellsCountY; iy++)
                    yield return this[ix, iy];
            }
        }

        public IEnumerable<AccGridCellInfo2D> NonEmptyGridCells
        {
            get
            {
                for (var ix = 0; ix < Grid.CellsCountX; ix++)
                for (var iy = 0; iy < Grid.CellsCountY; iy++)
                {
                    var gridCell = Grid[ix, iy];

                    if (ReferenceEquals(gridCell, null)) continue;

                    var cellBoundingBox = Grid.GetCellBoundingBox(ix, iy);
                    yield return new AccGridCellInfo2D(ix, iy, cellBoundingBox, gridCell);
                }
            }
        }


        private AccGridInfo2D(IAccGrid2D<IFiniteGeometricShape2D> grid)
        {
            Grid = grid;
            BoundingBox = Grid.GetBoundingBox();
        }

    }
}
