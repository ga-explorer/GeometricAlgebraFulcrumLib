using System;
using System.Collections;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.BasicShapes;
using GeometricAlgebraFulcrumLib.MathBase.Borders;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Immutable;
using GeometricAlgebraFulcrumLib.MathBase.Borders.Space2D.Mutable;

namespace NumericalGeometryLib.Accelerators.Grids.Space2D
{
    public class AccGrid2D<T> : IAccGrid2D<T>
        where T : IFiniteGeometricShape2D
    {
        private readonly IReadOnlyList<T> _geometricObjectsList;
        private readonly List<T>[,] _gridCells;
        private readonly double _invCellLengthX;
        private readonly double _invCellLengthY;


        public bool IntersectionTestsEnabled { get; set; } = true;

        public bool IsValid()
        {
            return true;
        }

        public int Count
        {
            get { return _geometricObjectsList.Count; }
        }

        public T this[int index]
        {
            get { return _geometricObjectsList[index]; }
        }

        public IReadOnlyList<T> this[int ix, int iy]
        {
            get { return _gridCells[ix, iy]; }
        }

        public BoundingBox2D BoundingBox { get; }

        public int CellsCountX { get; }

        public int CellsCountY { get; }

        public double CellArea
        {
            get { return BoundingBox.GetArea() / CellsCount; }
        }

        public int CellsCount
        {
            get { return CellsCountX * CellsCountY; }
        }

        public int NonEmptyCellsCount
        {
            get
            {
                var n = 0;

                for (var ix = 0; ix < CellsCountX; ix++)
                for (var iy = 0; iy < CellsCountY; iy++)
                {
                    if (_gridCells[ix, iy] != null)
                        n++;
                }

                return n;
            }
        }

        public int EmptyCellsCount
        {
            get { return CellsCount - NonEmptyCellsCount; }
        }

        public double AverageSurfacesPerCell
        {
            get
            {
                var n = 0;
                double k = 0;

                for (var ix = 0; ix < CellsCountX; ix++)
                    for (var iy = 0; iy < CellsCountY; iy++)
                    {
                        if (_gridCells[ix, iy] == null) continue;

                        n++;
                        k += _gridCells[ix, iy].Count;
                    }

                return k / n;
            }
        }


        public AccGrid2D(IReadOnlyList<T> geometricObjectsList)
        {
            _geometricObjectsList = geometricObjectsList;

            if (_geometricObjectsList == null || _geometricObjectsList.Count == 0)
                return;

            //Compute bounding box for entire grid
            BoundingBox = BoundingBox2D.Create(
                (IEnumerable<IFiniteGeometricShape2D>)_geometricObjectsList
            );

            var minX = BoundingBox.MinX;
            var minY = BoundingBox.MinY;
            var maxX = BoundingBox.MaxX;
            var maxY = BoundingBox.MaxY;
            var lengthX = maxX - minX;
            var lengthY = maxY - minY;

            //Setup grid cells
            var s = Math.Pow(BoundingBox.GetArea() / _geometricObjectsList.Count, -0.5d);

            CellsCountX = (int)(2.0 * lengthX * s) + 1;
            CellsCountY = (int)(2.0 * lengthY * s) + 1;

            _invCellLengthX = CellsCountX / lengthX;
            _invCellLengthY = CellsCountY / lengthY;

            _gridCells = new List<T>[CellsCountX, CellsCountY];

            //For each geometric object find all cells it occupies and put it
            //in all of them
            foreach (var geometricObject in _geometricObjectsList)
            {
                var boundingBox = geometricObject.GetBoundingBox();

                var minCorner = 
                    PointToCellIndex(boundingBox.MinX, boundingBox.MinY);

                var maxCorner =
                    PointToCellIndex(boundingBox.MaxX, boundingBox.MaxY);

                for (var ix = minCorner.X; ix <= maxCorner.X; ix++)
                for (var iy = minCorner.Y; iy <= maxCorner.Y; iy++)
                {
                    if (_gridCells[ix, iy] == null)
                        _gridCells[ix, iy] = new List<T>();

                    _gridCells[ix, iy].Add(geometricObject);
                }
            }
        }


        public IntTuple2D PointToCellIndex(double x, double y)
        {
            var ix = ((x - BoundingBox.MinX) * _invCellLengthX).ClampToInt(CellsCountX - 1);
            var iy = ((y - BoundingBox.MinY) * _invCellLengthY).ClampToInt(CellsCountY - 1);

            return new IntTuple2D(ix, iy);
        }

        public int PointXToCellIndex(double x)
        {
            return ((x - BoundingBox.MinX) * _invCellLengthX).ClampToInt(CellsCountX - 1);
        }

        public int PointYToCellIndex(double y)
        {
            return ((y - BoundingBox.MinY) * _invCellLengthY).ClampToInt(CellsCountY - 1);
        }

        public BoundingBox2D GetBoundingBox()
        {
            return BoundingBox;
        }

        public MutableBoundingBox2D GetMutableBoundingBox()
        {
            return MutableBoundingBox2D.Create(BoundingBox);
        }

        //public bool TestLineSegmentIntersection(ITuple2D point1, ITuple2D point2)
        //{
        //    var point1X = point1.X;
        //    var point1Y = point1.Y;

        //    var point2X = point2.X;
        //    var point2Y = point2.Y;

        //    var lineDirectionX = point2X - point1X;
        //    var lineDirectionY = point2Y - point1Y;

        //    var gridBoxMinX = BoundingBox.MinX;
        //    var gridBoxMinY = BoundingBox.MinY;

        //    var gridBoxMaxX = BoundingBox.MaxX;
        //    var gridBoxMaxY = BoundingBox.MaxY;

        //    double txMin, tyMin;
        //    double txMax, tyMax;


        //    //Test if line segment hits grid bounding box
        //    var a = 1.0 / lineDirectionX;
        //    if (a >= 0)
        //    {
        //        txMin = (gridBoxMinX - point1X) * a;
        //        txMax = (gridBoxMaxX - point1X) * a;
        //    }
        //    else
        //    {
        //        txMin = (gridBoxMaxX - point1X) * a;
        //        txMax = (gridBoxMinX - point1X) * a;
        //    }

        //    var b = 1.0 / lineDirectionY;
        //    if (b >= 0)
        //    {
        //        tyMin = (gridBoxMinY - point1Y) * b;
        //        tyMax = (gridBoxMaxY - point1Y) * b;
        //    }
        //    else
        //    {
        //        tyMin = (gridBoxMaxY - point1Y) * b;
        //        tyMax = (gridBoxMinY - point1Y) * b;
        //    }

        //    var t0 = txMin > tyMin ? txMin : tyMin;
        //    var t1 = txMax < tyMax ? txMax : tyMax;

        //    if (t0 > t1 || t1 < 0 || t0 > 1)
        //        return false;

        //    if (t0 > 0)
        //    {
        //        //The line segment's first point is outside the grid's
        //        //bounding box; adjust the first point to be inside it
        //        point1X = point1X + t0 * lineDirectionX;
        //        point1Y = point1Y + t0 * lineDirectionY;
        //        t0 = 0;
        //    }

        //    if (t1 < 1)
        //    {
        //        //The line segment's last point is outside the grid's
        //        //bounding box; adjust the last point to be inside it
        //        point2X = point1X + t1 * lineDirectionX;
        //        point2Y = point1Y + t1 * lineDirectionY;
        //        t1 = 1;
        //    }

        //    //Compute indices of cell containing line segment first point
        //    var ix = PointXToCellIndex(point1X);
        //    var iy = PointYToCellIndex(point1Y);

        //    //Line segment parameter increments per cell in the x and y directions
        //    var dtx = (txMax - txMin) / CellsCountX;
        //    var dty = (tyMax - tyMin) / CellsCountY;

        //    double txNext, tyNext;
        //    int ixStep, iyStep;
        //    int ixStop, iyStop;

        //    if (lineDirectionX > 0)
        //    {
        //        txNext = txMin + (ix + 1) * dtx;
        //        ixStep = +1;
        //        ixStop = PointXToCellIndex(point2X) + 1;
        //    }
        //    else if (lineDirectionX < 0)
        //    {
        //        txNext = txMin + (CellsCountX - ix) * dtx;
        //        ixStep = -1;
        //        ixStop = -1;
        //    }
        //    else
        //    {
        //        txNext = double.MaxValue;
        //        ixStep = -1;
        //        ixStop = -1;
        //    }

        //    if (lineDirectionY > 0)
        //    {
        //        tyNext = tyMin + (iy + 1) * dty;
        //        iyStep = +1;
        //        iyStop = PointYToCellIndex(point2Y) + 1;
        //    }
        //    else if (lineDirectionY < 0)
        //    {
        //        tyNext = tyMin + (CellsCountY - iy) * dty;
        //        iyStep = -1;
        //        iyStop = -1;
        //    }
        //    else
        //    {
        //        tyNext = double.MaxValue;
        //        iyStep = -1;
        //        iyStop = -1;
        //    }
            
        //    //Traverse the grid
        //    while (true)
        //    {
        //        var gridCell = _gridCells[ix, iy];

        //        if (txNext < tyNext)
        //        {
        //            if (
        //                !ReferenceEquals(gridCell, null) &&
        //                TestLineSegmentIntersection(gridCell, point1, point2)
        //                )
        //                    return true;

        //            txNext += dtx;
        //            ix += ixStep;

        //            if (ix == ixStop)
        //                return false;
        //        }
        //        else
        //        {
        //            if (
        //                !ReferenceEquals(gridCell, null) &&
        //                TestLineSegmentIntersection(gridCell, point1, point2)
        //            )
        //                return true;

        //            tyNext += dty;
        //            iy += iyStep;

        //            if (iy == iyStop)
        //                return false;
        //        }
        //    }
        //}
        public IEnumerator<T> GetEnumerator()
        {
            return _geometricObjectsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _geometricObjectsList.GetEnumerator();
        }
    }
}
