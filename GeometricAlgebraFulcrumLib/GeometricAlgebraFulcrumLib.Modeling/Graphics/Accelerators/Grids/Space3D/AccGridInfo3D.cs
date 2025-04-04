﻿using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space3D;

public sealed class AccGridInfo3D
{
    public static AccGridInfo3D Create(IAccGrid3D<IFloat64FiniteGeometricShape3D> grid)
    {
        return new AccGridInfo3D(grid);
    }


    public IAccGrid3D<IFloat64FiniteGeometricShape3D> Grid { get; }

    public int CellsCountX => Grid.CellsCountX;

    public int CellsCountY => Grid.CellsCountY;

    public int CellsCountZ => Grid.CellsCountZ;

    public int CellsCount => CellsCountX * CellsCountY * CellsCountZ;

    public Float64BoundingBox3D BoundingBox { get; }

    public AccGridCellInfo3D this[int indexX, int indexY, int indexZ]
    {
        get
        {
            var gridCell = Grid[indexX, indexY, indexZ];

            var cellBoundingBox = Grid.GetCellBoundingBox(indexX, indexY, indexZ);

            return ReferenceEquals(gridCell, null)
                ? new AccGridCellInfo3D(indexX, indexY, indexZ, cellBoundingBox)
                : new AccGridCellInfo3D(indexX, indexY, indexZ, cellBoundingBox, gridCell);
        }
    }

    public IEnumerable<AccGridCellInfo3D> GridCells
    {
        get
        {
            for (var ix = 0; ix < Grid.CellsCountX; ix++)
            for (var iy = 0; iy < Grid.CellsCountY; iy++)
            for (var iz = 0; iz < Grid.CellsCountZ; iz++)
                yield return this[ix, iy, iz];
        }
    }

    public IEnumerable<AccGridCellInfo3D> NonEmptyGridCells
    {
        get
        {
            for (var ix = 0; ix < Grid.CellsCountX; ix++)
            for (var iy = 0; iy < Grid.CellsCountY; iy++)
            for (var iz = 0; iz < Grid.CellsCountZ; iz++)
            {
                var gridCell = Grid[ix, iy, iz];

                if (ReferenceEquals(gridCell, null)) continue;

                var cellBoundingBox = Grid.GetCellBoundingBox(ix, iy, iz);
                yield return new AccGridCellInfo3D(ix, iy, iz, cellBoundingBox, gridCell);
            }
        }
    }


    private AccGridInfo3D(IAccGrid3D<IFloat64FiniteGeometricShape3D> grid)
    {
        Grid = grid;
        BoundingBox = Grid.GetBoundingBox();
    }

}