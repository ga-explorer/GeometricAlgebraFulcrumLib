﻿using System.Collections.Generic;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;
using NumericalGeometryLib.BasicShapes;
using NumericalGeometryLib.Borders.Space2D.Immutable;

namespace NumericalGeometryLib.Accelerators.Grids.Space2D
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