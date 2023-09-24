namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Intersections
{
    ///// <summary>
    ///// This class can be used to efficiently compute the intersection of a single
    ///// line segment given by its two end points with a collection of geometric
    ///// objects in 2D space
    ///// </summary>
    //public class GcLineSegmentIntersector2D
    //{
    //    public MutableLineSegment2D LineSegment { get; }
    //        = new MutableLineSegment2D();


    //    public bool HasIntersection { get; private set; }

    //    public MutableTuple2D IntersectionPoint { get; }
    //        = new MutableTuple2D();


    //    public GcLineSegmentIntersector2D()
    //    {
    //    }

    //    public GcLineSegmentIntersector2D(ITuple2D endPoint1, ITuple2D endPoint2)
    //    {
    //        LineSegment.Point1X = endPoint1.ItemX;
    //        LineSegment.Point1Y = endPoint1.ItemY;

    //        LineSegment.Point2X = endPoint2.ItemX;
    //        LineSegment.Point2Y = endPoint2.ItemY;
    //    }

    //    public GcLineSegmentIntersector2D(ILineSegment2D lineSegment)
    //    {
    //        LineSegment.Point1X = lineSegment.Point1X;
    //        LineSegment.Point1Y = lineSegment.Point1Y;

    //        LineSegment.Point2X = lineSegment.Point2X;
    //        LineSegment.Point2Y = lineSegment.Point2Y;
    //    }


    //    public GcLineSegmentIntersector2D ResetOutputs()
    //    {
    //        HasIntersection = false;
    //        IntersectionPoint.SetTuple(0, 0);

    //        return this;
    //    }

    //    public GcLineSegmentIntersector2D SetLineSegment(ITuple2D startPoint, ITuple2D imagePoint)
    //    {
    //        LineSegment.Point1X = startPoint.ItemX;
    //        LineSegment.Point1Y = startPoint.ItemY;

    //        LineSegment.Point2X = imagePoint.ItemX;
    //        LineSegment.Point2Y = imagePoint.ItemY;

    //        return this;
    //    }

    //    public GcLineSegmentIntersector2D SetLineSegment(ILineSegment2D lineSegment)
    //    {
    //        LineSegment.Point1X = lineSegment.Point1X;
    //        LineSegment.Point1Y = lineSegment.Point1Y;

    //        LineSegment.Point2X = lineSegment.Point2X;
    //        LineSegment.Point2Y = lineSegment.Point2Y;

    //        return this;
    //    }


    //    public bool TestIntersection(IFiniteGeometricShape2D geometricObject)
    //    {
    //        var lineSegment = geometricObject as ILineSegment2D;
    //        if (!ReferenceEquals(lineSegment, null))
    //            return TestIntersection(lineSegment);

    //        //TODO: Add More Kinds of Geometric Objects Here

    //        return false;
    //    }

    //    public bool TestIntersection(IEnumerable<IFiniteGeometricShape2D> geometricObjectsList)
    //    {
    //        foreach (var geometricObject in geometricObjectsList)
    //        {
    //            var lineSegment = geometricObject as ILineSegment2D;
    //            if (!ReferenceEquals(lineSegment, null))
    //                return TestIntersection(lineSegment);

    //            //TODO: Add More Kinds of Geometric Objects Here
    //        }

    //        return false;
    //    }

    //    public bool TestIntersection(ILine2D line)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TestIntersection(ILineSegment2D lineSegment)
    //    {
    //        HasIntersection = false;

    //        //Begin GMac Macro Code Generation, 2018-07-26T22:48:23.1230043+02:00
    //        //Macro: cemsim.hga4d.IntersectLineSegments2D
    //        //Input Variables: 8 used, 0 not used, 8 total.
    //        //Temp Variables: 28 sub-expressions, 0 generated temps, 28 total.
    //        //Target Temp Variables: 8 total.
    //        //Output Variables: 4 total.
    //        //Computations: 1.1875 average, 38 total.
    //        //Memory Reads: 1.8125 average, 58 total.
    //        //Memory Writes: 32 total.
    //        //
    //        //Macro Binding Data: 
    //        //   result.d120 = variable: var d120
    //        //   result.d121 = variable: var d121
    //        //   result.d210 = variable: var d210
    //        //   result.d211 = variable: var d211
    //        //   s1p1.#e1# = variable: LineSegment.Point1X
    //        //   s1p1.#e2# = variable: LineSegment.Point1Y
    //        //   s1p2.#e1# = variable: LineSegment.Point2X
    //        //   s1p2.#e2# = variable: LineSegment.Point2Y
    //        //   s2p1.#e1# = variable: lineSegment2.Point3X
    //        //   s2p1.#e2# = variable: lineSegment2.Point3Y
    //        //   s2p2.#e1# = variable: lineSegment2.Point4X
    //        //   s2p2.#e2# = variable: lineSegment2.Point4Y

    //        double tmp0;
    //        double tmp1;
    //        double tmp2;
    //        double tmp3;
    //        double tmp4;
    //        double tmp5;
    //        double tmp6;
    //        double tmp7;

    //        //Sub-expression: LLDI0064 = Times[-1,LLDI0005]
    //        tmp0 = -LineSegment.Point1X;

    //        //Sub-expression: LLDI0065 = Plus[LLDI0064,LLDI0007]
    //        tmp0 = tmp0 + LineSegment.Point2X;

    //        //Sub-expression: LLDI0067 = Times[-1,LLDI0006]
    //        tmp1 = -LineSegment.Point1Y;

    //        //Sub-expression: LLDI0068 = Plus[LLDI0067,LLDI0008]
    //        tmp1 = tmp1 + LineSegment.Point2Y;

    //        //Sub-expression: LLDI006B = Times[-1,LLDI0006,LLDI0065]
    //        tmp2 = -1 * LineSegment.Point1Y * tmp0;

    //        //Sub-expression: LLDI006C = Times[LLDI0005,LLDI0068]
    //        tmp3 = LineSegment.Point1X * tmp1;

    //        //Sub-expression: LLDI006D = Plus[LLDI006B,LLDI006C]
    //        tmp2 = tmp2 + tmp3;

    //        //Sub-expression: LLDI006F = Times[LLDI000A,LLDI0065]
    //        tmp3 = lineSegment.Point1Y * tmp0;

    //        //Sub-expression: LLDI0070 = Times[-1,LLDI0009,LLDI0068]
    //        tmp4 = -1 * lineSegment.Point1X * tmp1;

    //        //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
    //        tmp3 = tmp3 + tmp4;

    //        //Output: LLDI0002 = Plus[LLDI0071,LLDI006D]
    //        var d121 = tmp3 + tmp2;

    //        //Sub-expression: LLDI0072 = Times[-1,LLDI0009]
    //        tmp3 = -lineSegment.Point1X;

    //        //Sub-expression: LLDI0073 = Plus[LLDI0072,LLDI000B]
    //        tmp3 = tmp3 + lineSegment.Point2X;

    //        //Sub-expression: LLDI0075 = Times[-1,LLDI000A]
    //        tmp4 = -lineSegment.Point1Y;

    //        //Sub-expression: LLDI0076 = Plus[LLDI0075,LLDI000C]
    //        tmp4 = tmp4 + lineSegment.Point2Y;

    //        //Sub-expression: LLDI0079 = Times[-1,LLDI000A,LLDI0073]
    //        tmp5 = -1 * lineSegment.Point1Y * tmp3;

    //        //Sub-expression: LLDI007A = Times[LLDI0009,LLDI0076]
    //        tmp6 = lineSegment.Point1X * tmp4;

    //        //Sub-expression: LLDI007B = Plus[LLDI0079,LLDI007A]
    //        tmp5 = tmp5 + tmp6;

    //        //Sub-expression: LLDI007D = Times[LLDI0006,LLDI0073]
    //        tmp6 = LineSegment.Point1Y * tmp3;

    //        //Sub-expression: LLDI007E = Times[-1,LLDI0005,LLDI0076]
    //        tmp7 = -1 * LineSegment.Point1X * tmp4;

    //        //Sub-expression: LLDI007F = Plus[LLDI007D,LLDI007E]
    //        tmp6 = tmp6 + tmp7;

    //        //Output: LLDI0004 = Plus[LLDI007F,LLDI007B]
    //        var d211 = tmp6 + tmp5;

    //        //Sub-expression: LLDI0066 = Times[-1,LLDI000C,LLDI0065]
    //        tmp0 = -1 * lineSegment.Point2Y * tmp0;

    //        //Sub-expression: LLDI0069 = Times[LLDI000B,LLDI0068]
    //        tmp1 = lineSegment.Point2X * tmp1;

    //        //Sub-expression: LLDI006A = Plus[LLDI0066,LLDI0069]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI006E = Times[-1,LLDI006D]
    //        tmp1 = -tmp2;

    //        //Output: LLDI0001 = Plus[LLDI006A,LLDI006E]
    //        var d120 = tmp0 + tmp1;

    //        if (!((d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0)))
    //            return false; 

    //        //Sub-expression: LLDI0074 = Times[-1,LLDI0008,LLDI0073]
    //        tmp0 = -1 * LineSegment.Point2Y * tmp3;

    //        //Sub-expression: LLDI0077 = Times[LLDI0007,LLDI0076]
    //        tmp1 = LineSegment.Point2X * tmp4;

    //        //Sub-expression: LLDI0078 = Plus[LLDI0074,LLDI0077]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI007C = Times[-1,LLDI007B]
    //        tmp1 = -tmp5;

    //        //Output: LLDI0003 = Plus[LLDI0078,LLDI007C]
    //        var d210 = tmp0 + tmp1;

    //        if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
    //            return false;

    //        //Finish GMac Macro Code Generation, 2018-07-26T22:48:23.1230043+02:00

    //        HasIntersection = true;

    //        return true;
    //    }

    //    public bool TestIntersection(IEnumerable<ILineSegment2D> lineSegmentsList) 
    //        => lineSegmentsList.Any(TestIntersection);

    //    public bool TestIntersection(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    //    {
    //        var bih = lineSegmentsList as AccBih2D<ILineSegment2D>;
    //        if (!ReferenceEquals(bih, null))
    //            return TestIntersection(bih);

    //        var grid = lineSegmentsList as AccGrid2D<ILineSegment2D>;
    //        if (!ReferenceEquals(grid, null))
    //            return TestIntersection(grid);

    //        return lineSegmentsList.Any(TestIntersection);
    //    }

    //    public bool TestIntersection(IAccGrid2D<ILineSegment2D> grid)
    //    {
    //        var point1X = LineSegment.Point1X;
    //        var point1Y = LineSegment.Point1Y;

    //        var point2X = LineSegment.Point2X;
    //        var point2Y = LineSegment.Point2Y;

    //        var lineDirectionX = point2X - point1X;
    //        var lineDirectionY = point2Y - point1Y;

    //        var gridBoxMinX = grid.BoundingBox.MinX;
    //        var gridBoxMinY = grid.BoundingBox.MinY;

    //        var gridBoxMaxX = grid.BoundingBox.MaxX;
    //        var gridBoxMaxY = grid.BoundingBox.MaxY;

    //        double txMin, tyMin;
    //        double txMax, tyMax;


    //        //Test if line segment hits grid bounding box
    //        var a = 1.0 / lineDirectionX;
    //        if (a >= 0)
    //        {
    //            txMin = (gridBoxMinX - point1X) * a;
    //            txMax = (gridBoxMaxX - point1X) * a;
    //        }
    //        else
    //        {
    //            txMin = (gridBoxMaxX - point1X) * a;
    //            txMax = (gridBoxMinX - point1X) * a;
    //        }

    //        var b = 1.0 / lineDirectionY;
    //        if (b >= 0)
    //        {
    //            tyMin = (gridBoxMinY - point1Y) * b;
    //            tyMax = (gridBoxMaxY - point1Y) * b;
    //        }
    //        else
    //        {
    //            tyMin = (gridBoxMaxY - point1Y) * b;
    //            tyMax = (gridBoxMinY - point1Y) * b;
    //        }

    //        var t0 = txMin > tyMin ? txMin : tyMin;
    //        var t1 = txMax < tyMax ? txMax : tyMax;

    //        if (t0 > t1 || t1 < 0 || t0 > 1)
    //            return false;

    //        if (t0 > 0)
    //        {
    //            //The line segment's first point is outside the grid's
    //            //bounding box; adjust the first point to be inside it
    //            point1X = point1X + t0 * lineDirectionX;
    //            point1Y = point1Y + t0 * lineDirectionY;
    //            t0 = 0;
    //        }

    //        if (t1 < 1)
    //        {
    //            //The line segment's last point is outside the grid's
    //            //bounding box; adjust the last point to be inside it
    //            point2X = point1X + t1 * lineDirectionX;
    //            point2Y = point1Y + t1 * lineDirectionY;
    //            t1 = 1;
    //        }

    //        //Compute indices of cell containing line segment first point
    //        var ix = grid.PointXToCellIndex(point1X);
    //        var iy = grid.PointYToCellIndex(point1Y);

    //        //Line segment parameter increments per cell in the x and y directions
    //        var dtx = (txMax - txMin) / grid.CellsCountX;
    //        var dty = (tyMax - tyMin) / grid.CellsCountY;

    //        double txNext, tyNext;
    //        int ixStep, iyStep;
    //        int ixStop, iyStop;

    //        if (lineDirectionX > 0)
    //        {
    //            txNext = txMin + (ix + 1) * dtx;
    //            ixStep = +1;
    //            ixStop = grid.PointXToCellIndex(point2X) + 1;
    //        }
    //        else if (lineDirectionX < 0)
    //        {
    //            txNext = txMin + (grid.CellsCountX - ix) * dtx;
    //            ixStep = -1;
    //            ixStop = -1;
    //        }
    //        else
    //        {
    //            txNext = double.MaxValue;
    //            ixStep = -1;
    //            ixStop = -1;
    //        }

    //        if (lineDirectionY > 0)
    //        {
    //            tyNext = tyMin + (iy + 1) * dty;
    //            iyStep = +1;
    //            iyStop = grid.PointYToCellIndex(point2Y) + 1;
    //        }
    //        else if (lineDirectionY < 0)
    //        {
    //            tyNext = tyMin + (grid.CellsCountY - iy) * dty;
    //            iyStep = -1;
    //            iyStop = -1;
    //        }
    //        else
    //        {
    //            tyNext = double.MaxValue;
    //            iyStep = -1;
    //            iyStop = -1;
    //        }

    //        //Traverse the grid
    //        while (true)
    //        {
    //            var gridCell = grid[ix, iy];

    //            if (txNext < tyNext)
    //            {
    //                if (
    //                    !ReferenceEquals(gridCell, null) &&
    //                    TestIntersection(gridCell)
    //                    )
    //                    return true;

    //                txNext += dtx;
    //                ix += ixStep;

    //                if (ix == ixStop)
    //                    return false;
    //            }
    //            else
    //            {
    //                if (
    //                    !ReferenceEquals(gridCell, null) &&
    //                    TestIntersection(gridCell)
    //                )
    //                    return true;

    //                tyNext += dty;
    //                iy += iyStep;

    //                if (iy == iyStop)
    //                    return false;
    //            }
    //        }
    //    }

    //    public bool TestIntersection(AccBih2D<ILineSegment2D> bih)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public bool TestIntersection(BoundingSphere2D boundingSphere)
    //    {
    //        var d1 = (LineSegment.Point1X - boundingSphere.CenterX) * (LineSegment.Point1X - boundingSphere.CenterX) +
    //                 (LineSegment.Point1Y - boundingSphere.CenterY) * (LineSegment.Point1Y - boundingSphere.CenterY);

    //        var d2 = (LineSegment.Point2X - boundingSphere.CenterX) * (LineSegment.Point2X - boundingSphere.CenterX) +
    //                 (LineSegment.Point2Y - boundingSphere.CenterY) * (LineSegment.Point2Y - boundingSphere.CenterY);

    //        var r = boundingSphere.Radius * boundingSphere.Radius;

    //        return (d1 <= r && d2 >= r) || (d2 <= r && d1 >= r);
    //    }

    //    public void ComputeIntersection(ILineSegment2D lineSegment)
    //    {
    //        HasIntersection = false;

    //        //Begin GMac Macro Code Generation, 2018-07-26T22:42:23.4312072+02:00
    //        //Macro: cemsim.hga4d.IntersectLineSegments2D
    //        //Input Variables: 8 used, 0 not used, 8 total.
    //        //Temp Variables: 38 sub-expressions, 0 generated temps, 38 total.
    //        //Target Temp Variables: 9 total.
    //        //Output Variables: 6 total.
    //        //Computations: 1.13636363636364 average, 50 total.
    //        //Memory Reads: 1.84090909090909 average, 81 total.
    //        //Memory Writes: 44 total.
    //        //
    //        //Macro Binding Data: 
    //        //   result.d120 = variable: var d120
    //        //   result.d121 = variable: var d121
    //        //   result.d210 = variable: var d210
    //        //   result.d211 = variable: var d211
    //        //   result.intersectionPoint.#e1# = variable: IntersectionPoint.X
    //        //   result.intersectionPoint.#e2# = variable: IntersectionPoint.Y
    //        //   s1p1.#e1# = variable: LineSegment.Point1X
    //        //   s1p1.#e2# = variable: LineSegment.Point1Y
    //        //   s1p2.#e1# = variable: LineSegment.Point2X
    //        //   s1p2.#e2# = variable: LineSegment.Point2Y
    //        //   s2p1.#e1# = variable: lineSegment2.Point1X
    //        //   s2p1.#e2# = variable: lineSegment2.Point1Y
    //        //   s2p2.#e1# = variable: lineSegment2.Point2X
    //        //   s2p2.#e2# = variable: lineSegment2.Point2Y

    //        double tmp0;
    //        double tmp1;
    //        double tmp2;
    //        double tmp3;
    //        double tmp4;
    //        double tmp5;
    //        double tmp6;
    //        double tmp7;
    //        double tmp8;

    //        //Sub-expression: LLDI0066 = Times[-1,LLDI0007]
    //        tmp0 = -LineSegment.Point1X;

    //        //Sub-expression: LLDI0067 = Plus[LLDI0066,LLDI0009]
    //        tmp0 = tmp0 + LineSegment.Point2X;

    //        //Sub-expression: LLDI0069 = Times[-1,LLDI0008]
    //        tmp1 = -LineSegment.Point1Y;

    //        //Sub-expression: LLDI006A = Plus[LLDI0069,LLDI000A]
    //        tmp1 = tmp1 + LineSegment.Point2Y;

    //        //Sub-expression: LLDI006D = Times[-1,LLDI0008,LLDI0067]
    //        tmp2 = -1 * LineSegment.Point1Y * tmp0;

    //        //Sub-expression: LLDI006E = Times[LLDI0007,LLDI006A]
    //        tmp3 = LineSegment.Point1X * tmp1;

    //        //Sub-expression: LLDI006F = Plus[LLDI006D,LLDI006E]
    //        tmp2 = tmp2 + tmp3;

    //        //Sub-expression: LLDI0071 = Times[LLDI000C,LLDI0067]
    //        tmp3 = lineSegment.Point1Y * tmp0;

    //        //Sub-expression: LLDI0072 = Times[-1,LLDI000B,LLDI006A]
    //        tmp4 = -1 * lineSegment.Point1X * tmp1;

    //        //Sub-expression: LLDI0073 = Plus[LLDI0071,LLDI0072]
    //        tmp3 = tmp3 + tmp4;

    //        //Output: LLDI0002 = Plus[LLDI0073,LLDI006F]
    //        var d121 = tmp3 + tmp2;

    //        //Sub-expression: LLDI0074 = Times[-1,LLDI000B]
    //        tmp4 = -lineSegment.Point1X;

    //        //Sub-expression: LLDI0075 = Plus[LLDI0074,LLDI000D]
    //        tmp4 = tmp4 + lineSegment.Point2X;

    //        //Sub-expression: LLDI0077 = Times[-1,LLDI000C]
    //        tmp5 = -lineSegment.Point1Y;

    //        //Sub-expression: LLDI0078 = Plus[LLDI0077,LLDI000E]
    //        tmp5 = tmp5 + lineSegment.Point2Y;

    //        //Sub-expression: LLDI007B = Times[-1,LLDI000C,LLDI0075]
    //        tmp6 = -1 * lineSegment.Point1Y * tmp4;

    //        //Sub-expression: LLDI007C = Times[LLDI000B,LLDI0078]
    //        tmp7 = lineSegment.Point1X * tmp5;

    //        //Sub-expression: LLDI007D = Plus[LLDI007B,LLDI007C]
    //        tmp6 = tmp6 + tmp7;

    //        //Sub-expression: LLDI007F = Times[LLDI0008,LLDI0075]
    //        tmp7 = LineSegment.Point1Y * tmp4;

    //        //Sub-expression: LLDI0080 = Times[-1,LLDI0007,LLDI0078]
    //        tmp8 = -1 * LineSegment.Point1X * tmp5;

    //        //Sub-expression: LLDI0081 = Plus[LLDI007F,LLDI0080]
    //        tmp7 = tmp7 + tmp8;

    //        //Output: LLDI0004 = Plus[LLDI0081,LLDI007D]
    //        var d211 = tmp7 + tmp6;

    //        //Sub-expression: LLDI0068 = Times[-1,LLDI000E,LLDI0067]
    //        tmp0 = -1 * lineSegment.Point2Y * tmp0;

    //        //Sub-expression: LLDI006B = Times[LLDI000D,LLDI006A]
    //        tmp1 = lineSegment.Point2X * tmp1;

    //        //Sub-expression: LLDI006C = Plus[LLDI0068,LLDI006B]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI0070 = Times[-1,LLDI006F]
    //        tmp1 = -tmp2;

    //        //Output: LLDI0001 = Plus[LLDI006C,LLDI0070]
    //        var d120 = tmp0 + tmp1;

    //        if (!((d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0)))
    //            return;

    //        //Sub-expression: LLDI0076 = Times[-1,LLDI000A,LLDI0075]
    //        tmp4 = -1 * LineSegment.Point2Y * tmp4;

    //        //Sub-expression: LLDI0079 = Times[LLDI0009,LLDI0078]
    //        tmp5 = LineSegment.Point2X * tmp5;

    //        //Sub-expression: LLDI007A = Plus[LLDI0076,LLDI0079]
    //        tmp4 = tmp4 + tmp5;

    //        //Sub-expression: LLDI007E = Times[-1,LLDI007D]
    //        tmp5 = -tmp6;

    //        //Output: LLDI0003 = Plus[LLDI007A,LLDI007E]
    //        var d210 = tmp4 + tmp5;

    //        if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
    //            return;

    //        //Sub-expression: LLDI0082 = Plus[LLDI006C,LLDI0070]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI0083 = Plus[LLDI0073,LLDI006F]
    //        tmp1 = tmp3 + tmp2;

    //        //Sub-expression: LLDI0084 = Plus[LLDI0082,LLDI0083]
    //        tmp2 = tmp0 + tmp1;

    //        //Sub-expression: LLDI0085 = Power[LLDI0084,-1]
    //        tmp2 = 1 / tmp2;

    //        //Sub-expression: LLDI0086 = Times[LLDI0082,LLDI0085]
    //        tmp0 = tmp0 * tmp2;

    //        //Sub-expression: LLDI0087 = Times[LLDI000B,LLDI0086]
    //        tmp3 = lineSegment.Point1X * tmp0;

    //        //Sub-expression: LLDI0088 = Times[LLDI0083,LLDI0085]
    //        tmp1 = tmp1 * tmp2;

    //        //Sub-expression: LLDI0089 = Times[LLDI000D,LLDI0088]
    //        tmp2 = lineSegment.Point2X * tmp1;

    //        //Output: LLDI0005 = Plus[LLDI0087,LLDI0089]
    //        IntersectionPoint.ItemX = tmp3 + tmp2;

    //        //Sub-expression: LLDI008A = Times[LLDI000C,LLDI0086]
    //        tmp0 = lineSegment.Point1Y * tmp0;

    //        //Sub-expression: LLDI008B = Times[LLDI000E,LLDI0088]
    //        tmp1 = lineSegment.Point2Y * tmp1;

    //        //Output: LLDI0006 = Plus[LLDI008A,LLDI008B]
    //        IntersectionPoint.ItemY = tmp0 + tmp1;


    //        //Finish GMac Macro Code Generation, 2018-07-26T22:42:23.4312072+02:00

    //        HasIntersection = true;
    //    }
    //}
}
