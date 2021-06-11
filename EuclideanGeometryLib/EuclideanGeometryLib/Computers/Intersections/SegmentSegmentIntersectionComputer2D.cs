namespace EuclideanGeometryLib.Computers.Intersections
{
    //public sealed class SegmentSegmentIntersectionComputer2D
    //{
    //    public static int CallsCount { get; private set; }

    //    public static int CallsWithIntersectionCount { get; private set; }

    //    public static int CallsWithoutIntersectionCount
    //        => CallsCount - CallsWithIntersectionCount;

    //    public static int TestsCount { get; private set; }

    //    public static int TestsWithIntersectionCount { get; private set; }

    //    public static int TestsWithoutIntersectionCount
    //        => TestsCount - TestsWithIntersectionCount;


    //    public static void ResetCounters()
    //    {
    //        CallsCount = 0;
    //        CallsWithIntersectionCount = 0;

    //        TestsCount = 0;
    //        TestsWithIntersectionCount = 0;
    //    }


    //    #region Computer Inputs
    //    public MutableLineSegment2D Segment1 { get; } 
    //        = new MutableLineSegment2D();

    //    public ILineSegment2D Segment2 { get; set; }
    //    #endregion


    //    #region Computer Outputs
    //    public bool HasIntersection { get; private set; }

    //    public MutableTuple2D IntersectionPoint { get; } 
    //        = new MutableTuple2D();
    //    #endregion


    //    public SegmentSegmentIntersectionComputer2D ResetOutputs()
    //    {
    //        HasIntersection = false;

    //        return this;
    //    }

    //    public SegmentSegmentIntersectionComputer2D SetInputs(ITuple2D startPoint, ITuple2D imagePoint, ILineSegment2D surface)
    //    {
    //        Segment1.Point1X = startPoint.ItemX;
    //        Segment1.Point1Y = startPoint.ItemY;

    //        Segment1.Point2X = imagePoint.ItemX;
    //        Segment1.Point2Y = imagePoint.ItemY;

    //        Segment2 = surface;

    //        return this;
    //    }


    //    public SegmentSegmentIntersectionComputer2D Test()
    //    {
    //        TestsCount++;

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
    //        //   s1p1.#e1# = variable: Point1X
    //        //   s1p1.#e2# = variable: Point1Y
    //        //   s1p2.#e1# = variable: Point2X
    //        //   s1p2.#e2# = variable: Point2Y
    //        //   s2p1.#e1# = variable: Point3X
    //        //   s2p1.#e2# = variable: Point3Y
    //        //   s2p2.#e1# = variable: Point4X
    //        //   s2p2.#e2# = variable: Point4Y

    //        double tmp0;
    //        double tmp1;
    //        double tmp2;
    //        double tmp3;
    //        double tmp4;
    //        double tmp5;
    //        double tmp6;
    //        double tmp7;

    //        //Sub-expression: LLDI0064 = Times[-1,LLDI0005]
    //        tmp0 = -Segment1.Point1X;

    //        //Sub-expression: LLDI0065 = Plus[LLDI0064,LLDI0007]
    //        tmp0 = tmp0 + Segment1.Point2X;

    //        //Sub-expression: LLDI0067 = Times[-1,LLDI0006]
    //        tmp1 = -Segment1.Point1Y;

    //        //Sub-expression: LLDI0068 = Plus[LLDI0067,LLDI0008]
    //        tmp1 = tmp1 + Segment1.Point2Y;

    //        //Sub-expression: LLDI006B = Times[-1,LLDI0006,LLDI0065]
    //        tmp2 = -1 * Segment1.Point1Y * tmp0;

    //        //Sub-expression: LLDI006C = Times[LLDI0005,LLDI0068]
    //        tmp3 = Segment1.Point1X * tmp1;

    //        //Sub-expression: LLDI006D = Plus[LLDI006B,LLDI006C]
    //        tmp2 = tmp2 + tmp3;

    //        //Sub-expression: LLDI006F = Times[LLDI000A,LLDI0065]
    //        tmp3 = Segment2.Point1Y * tmp0;

    //        //Sub-expression: LLDI0070 = Times[-1,LLDI0009,LLDI0068]
    //        tmp4 = -1 * Segment2.Point1X * tmp1;

    //        //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
    //        tmp3 = tmp3 + tmp4;

    //        //Output: LLDI0002 = Plus[LLDI0071,LLDI006D]
    //        var d121 = tmp3 + tmp2;

    //        //Sub-expression: LLDI0072 = Times[-1,LLDI0009]
    //        tmp3 = -Segment2.Point1X;

    //        //Sub-expression: LLDI0073 = Plus[LLDI0072,LLDI000B]
    //        tmp3 = tmp3 + Segment2.Point2X;

    //        //Sub-expression: LLDI0075 = Times[-1,LLDI000A]
    //        tmp4 = -Segment2.Point1Y;

    //        //Sub-expression: LLDI0076 = Plus[LLDI0075,LLDI000C]
    //        tmp4 = tmp4 + Segment2.Point2Y;

    //        //Sub-expression: LLDI0079 = Times[-1,LLDI000A,LLDI0073]
    //        tmp5 = -1 * Segment2.Point1Y * tmp3;

    //        //Sub-expression: LLDI007A = Times[LLDI0009,LLDI0076]
    //        tmp6 = Segment2.Point1X * tmp4;

    //        //Sub-expression: LLDI007B = Plus[LLDI0079,LLDI007A]
    //        tmp5 = tmp5 + tmp6;

    //        //Sub-expression: LLDI007D = Times[LLDI0006,LLDI0073]
    //        tmp6 = Segment1.Point1Y * tmp3;

    //        //Sub-expression: LLDI007E = Times[-1,LLDI0005,LLDI0076]
    //        tmp7 = -1 * Segment1.Point1X * tmp4;

    //        //Sub-expression: LLDI007F = Plus[LLDI007D,LLDI007E]
    //        tmp6 = tmp6 + tmp7;

    //        //Output: LLDI0004 = Plus[LLDI007F,LLDI007B]
    //        var d211 = tmp6 + tmp5;

    //        //Sub-expression: LLDI0066 = Times[-1,LLDI000C,LLDI0065]
    //        tmp0 = -1 * Segment2.Point2Y * tmp0;

    //        //Sub-expression: LLDI0069 = Times[LLDI000B,LLDI0068]
    //        tmp1 = Segment2.Point2X * tmp1;

    //        //Sub-expression: LLDI006A = Plus[LLDI0066,LLDI0069]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI006E = Times[-1,LLDI006D]
    //        tmp1 = -tmp2;

    //        //Output: LLDI0001 = Plus[LLDI006A,LLDI006E]
    //        var d120 = tmp0 + tmp1;

    //        if (!((d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0)))
    //            return this;

    //        //Sub-expression: LLDI0074 = Times[-1,LLDI0008,LLDI0073]
    //        tmp0 = -1 * Segment1.Point2Y * tmp3;

    //        //Sub-expression: LLDI0077 = Times[LLDI0007,LLDI0076]
    //        tmp1 = Segment1.Point2X * tmp4;

    //        //Sub-expression: LLDI0078 = Plus[LLDI0074,LLDI0077]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI007C = Times[-1,LLDI007B]
    //        tmp1 = -tmp5;

    //        //Output: LLDI0003 = Plus[LLDI0078,LLDI007C]
    //        var d210 = tmp0 + tmp1;

    //        if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
    //            return this;

    //        //Finish GMac Macro Code Generation, 2018-07-26T22:48:23.1230043+02:00

    //        HasIntersection = true;

    //        TestsWithIntersectionCount++;

    //        return this;
    //    }

    //    public SegmentSegmentIntersectionComputer2D Compute()
    //    {
    //        CallsCount++;

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
    //        //   result.intersectionPoint.#e1# = variable: result.IntersectionPointX
    //        //   result.intersectionPoint.#e2# = variable: result.IntersectionPointY
    //        //   s1p1.#e1# = variable: Segment1EndPoint1X
    //        //   s1p1.#e2# = variable: Segment1EndPoint1Y
    //        //   s1p2.#e1# = variable: Segment1EndPoint2X
    //        //   s1p2.#e2# = variable: Segment1EndPoint2Y
    //        //   s2p1.#e1# = variable: Segment2EndPoint1X
    //        //   s2p1.#e2# = variable: Segment2EndPoint1Y
    //        //   s2p2.#e1# = variable: Segment2EndPoint2X
    //        //   s2p2.#e2# = variable: Segment2EndPoint2Y

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
    //        tmp0 = -Segment1.Point1X;

    //        //Sub-expression: LLDI0067 = Plus[LLDI0066,LLDI0009]
    //        tmp0 = tmp0 + Segment1.Point2X;

    //        //Sub-expression: LLDI0069 = Times[-1,LLDI0008]
    //        tmp1 = -Segment1.Point1Y;

    //        //Sub-expression: LLDI006A = Plus[LLDI0069,LLDI000A]
    //        tmp1 = tmp1 + Segment1.Point2Y;

    //        //Sub-expression: LLDI006D = Times[-1,LLDI0008,LLDI0067]
    //        tmp2 = -1 * Segment1.Point1Y * tmp0;

    //        //Sub-expression: LLDI006E = Times[LLDI0007,LLDI006A]
    //        tmp3 = Segment1.Point1X * tmp1;

    //        //Sub-expression: LLDI006F = Plus[LLDI006D,LLDI006E]
    //        tmp2 = tmp2 + tmp3;

    //        //Sub-expression: LLDI0071 = Times[LLDI000C,LLDI0067]
    //        tmp3 = Segment2.Point1Y * tmp0;

    //        //Sub-expression: LLDI0072 = Times[-1,LLDI000B,LLDI006A]
    //        tmp4 = -1 * Segment2.Point1X * tmp1;

    //        //Sub-expression: LLDI0073 = Plus[LLDI0071,LLDI0072]
    //        tmp3 = tmp3 + tmp4;

    //        //Output: LLDI0002 = Plus[LLDI0073,LLDI006F]
    //        var d121 = tmp3 + tmp2;

    //        //Sub-expression: LLDI0074 = Times[-1,LLDI000B]
    //        tmp4 = -Segment2.Point1X;

    //        //Sub-expression: LLDI0075 = Plus[LLDI0074,LLDI000D]
    //        tmp4 = tmp4 + Segment2.Point2X;

    //        //Sub-expression: LLDI0077 = Times[-1,LLDI000C]
    //        tmp5 = -Segment2.Point1Y;

    //        //Sub-expression: LLDI0078 = Plus[LLDI0077,LLDI000E]
    //        tmp5 = tmp5 + Segment2.Point2Y;

    //        //Sub-expression: LLDI007B = Times[-1,LLDI000C,LLDI0075]
    //        tmp6 = -1 * Segment2.Point1Y * tmp4;

    //        //Sub-expression: LLDI007C = Times[LLDI000B,LLDI0078]
    //        tmp7 = Segment2.Point1X * tmp5;

    //        //Sub-expression: LLDI007D = Plus[LLDI007B,LLDI007C]
    //        tmp6 = tmp6 + tmp7;

    //        //Sub-expression: LLDI007F = Times[LLDI0008,LLDI0075]
    //        tmp7 = Segment1.Point1Y * tmp4;

    //        //Sub-expression: LLDI0080 = Times[-1,LLDI0007,LLDI0078]
    //        tmp8 = -1 * Segment1.Point1X * tmp5;

    //        //Sub-expression: LLDI0081 = Plus[LLDI007F,LLDI0080]
    //        tmp7 = tmp7 + tmp8;

    //        //Output: LLDI0004 = Plus[LLDI0081,LLDI007D]
    //        var d211 = tmp7 + tmp6;

    //        //Sub-expression: LLDI0068 = Times[-1,LLDI000E,LLDI0067]
    //        tmp0 = -1 * Segment2.Point2Y * tmp0;

    //        //Sub-expression: LLDI006B = Times[LLDI000D,LLDI006A]
    //        tmp1 = Segment2.Point2X * tmp1;

    //        //Sub-expression: LLDI006C = Plus[LLDI0068,LLDI006B]
    //        tmp0 = tmp0 + tmp1;

    //        //Sub-expression: LLDI0070 = Times[-1,LLDI006F]
    //        tmp1 = -tmp2;

    //        //Output: LLDI0001 = Plus[LLDI006C,LLDI0070]
    //        var d120 = tmp0 + tmp1;

    //        if (!((d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0)))
    //            return this;

    //        //Sub-expression: LLDI0076 = Times[-1,LLDI000A,LLDI0075]
    //        tmp4 = -1 * Segment1.Point2Y * tmp4;

    //        //Sub-expression: LLDI0079 = Times[LLDI0009,LLDI0078]
    //        tmp5 = Segment1.Point2X * tmp5;

    //        //Sub-expression: LLDI007A = Plus[LLDI0076,LLDI0079]
    //        tmp4 = tmp4 + tmp5;

    //        //Sub-expression: LLDI007E = Times[-1,LLDI007D]
    //        tmp5 = -tmp6;

    //        //Output: LLDI0003 = Plus[LLDI007A,LLDI007E]
    //        var d210 = tmp4 + tmp5;

    //        if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
    //            return this;

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
    //        tmp3 = Segment2.Point1X * tmp0;

    //        //Sub-expression: LLDI0088 = Times[LLDI0083,LLDI0085]
    //        tmp1 = tmp1 * tmp2;

    //        //Sub-expression: LLDI0089 = Times[LLDI000D,LLDI0088]
    //        tmp2 = Segment2.Point2X * tmp1;

    //        //Output: LLDI0005 = Plus[LLDI0087,LLDI0089]
    //        IntersectionPoint.ItemX = tmp3 + tmp2;

    //        //Sub-expression: LLDI008A = Times[LLDI000C,LLDI0086]
    //        tmp0 = Segment2.Point1Y * tmp0;

    //        //Sub-expression: LLDI008B = Times[LLDI000E,LLDI0088]
    //        tmp1 = Segment2.Point2Y * tmp1;

    //        //Output: LLDI0006 = Plus[LLDI008A,LLDI008B]
    //        IntersectionPoint.ItemY = tmp0 + tmp1;


    //        //Finish GMac Macro Code Generation, 2018-07-26T22:42:23.4312072+02:00

    //        HasIntersection = true;

    //        CallsWithIntersectionCount++;

    //        return this;
    //    }
    //}
}