using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.MathBase.Geometry.Borders;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace NumericalGeometryLib.Accelerators.Grids.Space2D
{
    public sealed class AccGridLineTraverser2D
    {
        public static AccGridLineTraverser2D Create(IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line)
        {
            return new AccGridLineTraverser2D(
                grid, 
                line, 
                Float64Range1D.Infinite
            );
        }

        public static AccGridLineTraverser2D Create(IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line, Float64Range1D lineParamLimits)
        {
            return new AccGridLineTraverser2D(grid, line, lineParamLimits);
        }


        public IAccGrid2D<IFiniteGeometricShape2D> Grid { get; }

        public ILine2D Line { get; }

        public Float64Range1D LineParameterLimits { get; }

        /// <summary>
        /// Indicates if there are no cells to traverse in the grid
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// The limits of the line parameter intersecting the grid boundary
        /// </summary>
        public Float64Range1D TLimits { get; }

        /// <summary>
        /// Delta value of line parameter in x and y directions
        /// </summary>
        public Float64Vector2D TDelta { get; }

        /// <summary>
        /// Initial values of line parameter in x and y directions 
        /// </summary>
        public Float64Vector2D TFirst { get; }

        public double TNext { get; private set; }

        /// <summary>
        /// Cell index of first cell the line intersects in the grid
        /// </summary>
        public IntTuple2D CellIndexStart { get; }


        public IntTuple2D CellIndexStep { get; }

        /// <summary>
        /// Cell index to stop line traversal
        /// </summary>
        public IntTuple2D CellIndexStop { get; }


        private AccGridLineTraverser2D(IAccGrid2D<IFiniteGeometricShape2D> grid, ILine2D line, Float64Range1D lineParamLimits)
        {
            Grid = grid;
            Line = line;
            LineParameterLimits = lineParamLimits;

            var gridBoxMinX = grid.BoundingBox.MinX;
            var gridBoxMinY = grid.BoundingBox.MinY;

            var gridBoxMaxX = grid.BoundingBox.MaxX;
            var gridBoxMaxY = grid.BoundingBox.MaxY;

            double txMin, tyMin;
            double txMax, tyMax;

            //Test if line segment hits grid bounding box
            var a = 1.0 / line.DirectionX;
            if (a >= 0)
            {
                txMin = (gridBoxMinX - line.OriginX) * a;
                txMax = (gridBoxMaxX - line.OriginX) * a;
            }
            else
            {
                txMin = (gridBoxMaxX - line.OriginX) * a;
                txMax = (gridBoxMinX - line.OriginX) * a;
            }

            var b = 1.0 / line.DirectionY;
            if (b >= 0)
            {
                tyMin = (gridBoxMinY - line.OriginY) * b;
                tyMax = (gridBoxMaxY - line.OriginY) * b;
            }
            else
            {
                tyMin = (gridBoxMaxY - line.OriginY) * b;
                tyMax = (gridBoxMinY - line.OriginY) * b;
            }

            var t0 = txMin > tyMin ? txMin : tyMin;
            var t1 = txMax < tyMax ? txMax : tyMax;

            if (t0 > t1)
            {
                IsEmpty = true;
                TLimits = new Float64Range1D();
                TDelta = Float64Vector2D.Zero;

                return;
            }

            if (t0 < lineParamLimits.MinValue) t0 = lineParamLimits.MinValue;
            if (t1 > lineParamLimits.MaxValue) t1 = lineParamLimits.MaxValue;

            var point1X = Line.OriginX + t0 * Line.DirectionX;
            var point1Y = Line.OriginY + t0 * Line.DirectionY;

            var point2X = Line.OriginX + t1 * Line.DirectionX;
            var point2Y = Line.OriginY + t1 * Line.DirectionY;

            //double point1X, point1Y;
            //double point2X, point2Y;

            //if (t0 != 0)
            //{
            //    //The line segment's first point is outside the grid's
            //    //bounding box; adjust the first point to be inside it
            //    point1X = Line.OriginX + t0 * Line.ItemX;
            //    point1Y = Line.OriginY + t0 * Line.ItemY;
            //    t0 = 0;
            //}
            //else
            //{
            //    point1X = Line.OriginX;
            //    point1Y = Line.OriginY;
            //}

            //if (t1 != 1)
            //{
            //    //The line segment's last point is outside the grid's
            //    //bounding box; adjust the last point to be inside it
            //    point2X = Line.OriginX + t1 * Line.ItemX;
            //    point2Y = Line.OriginY + t1 * Line.ItemY;
            //    t1 = 1;
            //}
            //else
            //{
            //    point2X = Line.OriginX + Line.ItemX;
            //    point2Y = Line.OriginY + Line.ItemY;
            //}

            IsEmpty = false;
            TLimits = Float64Range1D.Create(t0, t1);

            //Compute indices of cell containing line segment first point
            CellIndexStart = grid.PointToCellIndex(point1X, point1Y);

            //Line segment parameter increments per cell in the x and y directions
            TDelta = Float64Vector2D.Create((Float64Scalar)((txMax - txMin) / grid.CellsCountX),
                (Float64Scalar)((tyMax - tyMin) / grid.CellsCountY));

            double txNext, tyNext;
            int ixStep, iyStep;
            int ixStop, iyStop;

            if (line.DirectionX > 0)
            {
                txNext = txMin + (CellIndexStart.X + 1) * TDelta.X;
                ixStep = +1;
                ixStop = grid.PointXToCellIndex(point2X) + 1;
            }
            else if (line.DirectionX < 0)
            {
                txNext = txMin + (grid.CellsCountX - CellIndexStart.X) * TDelta.X;
                ixStep = -1;
                ixStop = -1;
            }
            else
            {
                txNext = double.PositiveInfinity;
                ixStep = -1;
                ixStop = -1;
            }

            if (Line.DirectionY > 0)
            {
                tyNext = tyMin + (CellIndexStart.Y + 1) * TDelta.Y;
                iyStep = +1;
                iyStop = grid.PointYToCellIndex(point2Y) + 1;
            }
            else if (Line.DirectionY < 0)
            {
                tyNext = tyMin + (grid.CellsCountY - CellIndexStart.Y) * TDelta.Y;
                iyStep = -1;
                iyStop = -1;
            }
            else
            {
                tyNext = double.PositiveInfinity;
                iyStep = -1;
                iyStop = -1;
            }

            TFirst = Float64Vector2D.Create((Float64Scalar)txNext, (Float64Scalar)tyNext);
            CellIndexStep = new IntTuple2D(ixStep, iyStep);
            CellIndexStop = new IntTuple2D(ixStop, iyStop);
        }


        public IEnumerable<IntTuple2D> GetCellIndices()
        {
            var ix = CellIndexStart.X;
            var iy = CellIndexStart.Y;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;

            //Traverse the grid
            while (true)
            {
                if (tNextX < tNextY)
                {
                    TNext = tNextX;

                    yield return new IntTuple2D(ix, iy);

                    tNextX += TDelta.X;
                    ix += CellIndexStep.X;

                    if (ix == CellIndexStop.X)
                        yield break;
                }
                else
                {
                    TNext = tNextY;

                    yield return new IntTuple2D(ix, iy);

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.Y;

                    if (iy == CellIndexStop.Y)
                        yield break;
                }
            }
        }

        public IEnumerable<IReadOnlyList<IFiniteGeometricShape2D>> GetCells()
        {
            var ix = CellIndexStart.X;
            var iy = CellIndexStart.Y;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;

            //Traverse the grid
            while (true)
            {
                var gridCell = Grid[ix, iy];

                if (tNextX < tNextY)
                {
                    TNext = tNextX;

                    yield return gridCell;

                    tNextX += TDelta.X;
                    ix += CellIndexStep.X;

                    if (ix == CellIndexStop.X)
                        yield break;
                }
                else
                {
                    TNext = tNextY;

                    yield return gridCell;

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.Y;

                    if (iy == CellIndexStop.Y)
                        yield break;
                }
            }
        }

        public IEnumerable<IReadOnlyList<IFiniteGeometricShape2D>> GetActiveCells()
        {
            var ix = CellIndexStart.X;
            var iy = CellIndexStart.Y;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;

            //Traverse the grid
            while (true)
            {
                var gridCell = Grid[ix, iy];

                if (tNextX < tNextY)
                {
                    TNext = tNextX;

                    if (!ReferenceEquals(gridCell, null))
                        yield return gridCell;

                    tNextX += TDelta.X;
                    ix += CellIndexStep.X;

                    if (ix == CellIndexStop.X)
                        yield break;
                }
                else
                {
                    TNext = tNextY;

                    if (!ReferenceEquals(gridCell, null))
                        yield return gridCell;

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.Y;

                    if (iy == CellIndexStop.Y)
                        yield break;
                }
            }
        }
    }
}