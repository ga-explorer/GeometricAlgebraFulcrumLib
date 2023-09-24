using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Accelerators.Grids.Space3D
{
    public sealed class AccGridLineTraverser3D
    {
        public static AccGridLineTraverser3D Create(IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line)
        {
            return new AccGridLineTraverser3D(
                grid,
                line,
                Float64ScalarRange.Infinite
            );
        }

        public static AccGridLineTraverser3D Create(IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line, Float64ScalarRange lineParamLimits)
        {
            return new AccGridLineTraverser3D(grid, line, lineParamLimits);
        }


        public IAccGrid3D<IFiniteGeometricShape3D> Grid { get; }

        public ILine3D Line { get; }

        public Float64ScalarRange LineParameterLimits { get; }

        /// <summary>
        /// Indicates if there are no cells to traverse in the grid
        /// </summary>
        public bool IsEmpty { get; }

        /// <summary>
        /// The limits of the line parameter intersecting the grid boundary
        /// </summary>
        public Float64ScalarRange TLimits { get; }

        /// <summary>
        /// Delta value of line parameter in x and y directions
        /// </summary>
        public Float64Vector3D TDelta { get; }

        /// <summary>
        /// Initial values of line parameter in x and y directions 
        /// </summary>
        public Float64Vector3D TFirst { get; }

        public double TNext { get; private set; }

        /// <summary>
        /// Cell index of first cell the line intersects in the grid
        /// </summary>
        public IntTuple3D CellIndexStart { get; }


        public IntTuple3D CellIndexStep { get; }

        /// <summary>
        /// Cell index to stop line traversal
        /// </summary>
        public IntTuple3D CellIndexStop { get; }


        private AccGridLineTraverser3D(IAccGrid3D<IFiniteGeometricShape3D> grid, ILine3D line, Float64ScalarRange lineParamLimits)
        {
            Grid = grid;
            Line = line;
            LineParameterLimits = lineParamLimits;

            var gridBoxMinX = grid.BoundingBox.MinX;
            var gridBoxMinY = grid.BoundingBox.MinY;
            var gridBoxMinZ = grid.BoundingBox.MinZ;

            var gridBoxMaxX = grid.BoundingBox.MaxX;
            var gridBoxMaxY = grid.BoundingBox.MaxY;
            var gridBoxMaxZ = grid.BoundingBox.MaxZ;

            double txMin, tyMin, tzMin;
            double txMax, tyMax, tzMax;

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

            var c = 1.0 / line.DirectionZ;
            if (c >= 0)
            {
                tzMin = (gridBoxMinZ - line.OriginZ) * c;
                tzMax = (gridBoxMaxZ - line.OriginZ) * c;
            }
            else
            {
                tzMin = (gridBoxMaxZ - line.OriginZ) * c;
                tzMax = (gridBoxMinZ - line.OriginZ) * c;
            }

            var t0 = txMin > tyMin 
                ? txMin > tzMin ? txMin : tzMin 
                : tyMin > tzMin ? tyMin : tzMin;

            var t1 = txMax < tyMax 
                ? txMax < tzMax ? txMax : tzMax 
                : tyMax < tzMax ? tyMax : tzMax;

            if (t0 > t1)
            {
                IsEmpty = true;
                TLimits = new Float64ScalarRange();
                TDelta = Float64Vector3D.Zero;

                return;
            }

            if (t0 < lineParamLimits.MinValue) t0 = lineParamLimits.MinValue;
            if (t1 > lineParamLimits.MaxValue) t1 = lineParamLimits.MaxValue;

            var point1X = Line.OriginX + t0 * Line.DirectionX;
            var point1Y = Line.OriginY + t0 * Line.DirectionY;
            var point1Z = Line.OriginZ + t0 * Line.DirectionZ;

            var point2X = Line.OriginX + t1 * Line.DirectionX;
            var point2Y = Line.OriginY + t1 * Line.DirectionY;
            var point2Z = Line.OriginZ + t1 * Line.DirectionZ;

            IsEmpty = false;
            TLimits = Float64ScalarRange.Create(t0, t1);

            //Compute indices of cell containing line segment first point
            CellIndexStart = grid.PointToCellIndex(point1X, point1Y, point1Z);

            //Line segment parameter increments per cell in the x and y directions
            TDelta = Float64Vector3D.Create((txMax - txMin) / grid.CellsCountX,
                (tyMax - tyMin) / grid.CellsCountY,
                (tzMax - tzMin) / grid.CellsCountZ);

            double txNext, tyNext, tzNext;
            int ixStep, iyStep, izStep;
            int ixStop, iyStop, izStop;

            if (line.DirectionX > 0)
            {
                txNext = txMin + (CellIndexStart.ItemX + 1) * TDelta.X;
                ixStep = +1;
                ixStop = grid.PointXToCellIndex(point2X) + 1;
            }
            else if (line.DirectionX < 0)
            {
                txNext = txMin + (grid.CellsCountX - CellIndexStart.ItemX) * TDelta.X;
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
                tyNext = tyMin + (CellIndexStart.ItemY + 1) * TDelta.Y;
                iyStep = +1;
                iyStop = grid.PointYToCellIndex(point2Y) + 1;
            }
            else if (Line.DirectionY < 0)
            {
                tyNext = tyMin + (grid.CellsCountY - CellIndexStart.ItemY) * TDelta.Y;
                iyStep = -1;
                iyStop = -1;
            }
            else
            {
                tyNext = double.PositiveInfinity;
                iyStep = -1;
                iyStop = -1;
            }

            if (Line.DirectionZ > 0)
            {
                tzNext = tzMin + (CellIndexStart.ItemZ + 1) * TDelta.Z;
                izStep = +1;
                izStop = grid.PointZToCellIndex(point2Z) + 1;
            }
            else if (Line.DirectionZ < 0)
            {
                tzNext = tzMin + (grid.CellsCountZ - CellIndexStart.ItemZ) * TDelta.Z;
                izStep = -1;
                izStop = -1;
            }
            else
            {
                tzNext = double.PositiveInfinity;
                izStep = -1;
                izStop = -1;
            }

            TFirst = Float64Vector3D.Create(txNext, tyNext, tzNext);
            CellIndexStep = new IntTuple3D(ixStep, iyStep, izStep);
            CellIndexStop = new IntTuple3D(ixStop, iyStop, izStop);
        }


        public IEnumerable<IntTuple3D> GetCellIndices()
        {
            var ix = CellIndexStart.ItemX;
            var iy = CellIndexStart.ItemY;
            var iz = CellIndexStart.ItemZ;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;
            var tNextZ = TFirst.Z;

            //Traverse the grid
            while (true)
            {
                if (tNextX < tNextY && tNextX < tNextZ)
                {
                    TNext = tNextX;

                    yield return new IntTuple3D(ix, iy, iz);

                    tNextX += TDelta.X;
                    ix += CellIndexStep.ItemX;

                    if (ix == CellIndexStop.ItemX)
                        yield break;
                }
                else if (tNextY < tNextZ)
                {
                    TNext = tNextY;

                    yield return new IntTuple3D(ix, iy, iz);

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.ItemY;

                    if (iy == CellIndexStop.ItemY)
                        yield break;
                }
                else
                {
                    TNext = tNextZ;

                    yield return new IntTuple3D(ix, iy, iz);

                    tNextZ += TDelta.Z;
                    iz += CellIndexStep.ItemZ;

                    if (iz == CellIndexStop.ItemZ)
                        yield break;
                }
            }
        }

        public IEnumerable<IReadOnlyList<IFiniteGeometricShape3D>> GetCells()
        {
            var ix = CellIndexStart.ItemX;
            var iy = CellIndexStart.ItemY;
            var iz = CellIndexStart.ItemZ;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;
            var tNextZ = TFirst.Z;

            //Traverse the grid
            while (true)
            {
                var gridCell = Grid[ix, iy, iz];

                if (tNextX < tNextY && tNextX < tNextZ)
                {
                    TNext = tNextX;

                    yield return gridCell;

                    tNextX += TDelta.X;
                    ix += CellIndexStep.ItemX;

                    if (ix == CellIndexStop.ItemX)
                        yield break;
                }
                else if (tNextY < tNextZ)
                {
                    TNext = tNextY;

                    yield return gridCell;

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.ItemY;

                    if (iy == CellIndexStop.ItemY)
                        yield break;
                }
                else
                {
                    TNext = tNextZ;

                    yield return gridCell;

                    tNextZ += TDelta.Z;
                    iz += CellIndexStep.ItemZ;

                    if (iz == CellIndexStop.ItemZ)
                        yield break;
                }
            }
        }

        public IEnumerable<IReadOnlyList<IFiniteGeometricShape3D>> GetActiveCells()
        {
            var ix = CellIndexStart.ItemX;
            var iy = CellIndexStart.ItemY;
            var iz = CellIndexStart.ItemZ;

            var tNextX = TFirst.X;
            var tNextY = TFirst.Y;
            var tNextZ = TFirst.Z;

            //Traverse the grid
            while (true)
            {
                var gridCell = Grid[ix, iy, iz];

                if (tNextX < tNextY && tNextX < tNextZ)
                {
                    TNext = tNextX;

                    if (!ReferenceEquals(gridCell, null))
                        yield return gridCell;

                    tNextX += TDelta.X;
                    ix += CellIndexStep.ItemX;

                    if (ix == CellIndexStop.ItemX)
                        yield break;
                }
                else if (tNextY < tNextZ)
                {
                    TNext = tNextY;

                    if (!ReferenceEquals(gridCell, null))
                        yield return gridCell;

                    tNextY += TDelta.Y;
                    iy += CellIndexStep.ItemY;

                    if (iy == CellIndexStop.ItemY)
                        yield break;
                }
                else
                {
                    TNext = tNextZ;

                    if (!ReferenceEquals(gridCell, null))
                        yield return gridCell;

                    tNextZ += TDelta.Z;
                    iz += CellIndexStep.ItemZ;

                    if (iz == CellIndexStop.ItemZ)
                        yield break;
                }
            }
        }
    }
}