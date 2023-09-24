using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Lines;

namespace GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes
{
    public static class IntersectionUtils
    {
        public static Tuple<bool, double> NoIntersection { get; }
            = new Tuple<bool, double>(false, 0);

        public static Tuple<bool, double, double> NoIntersectionPair { get; }
            = new Tuple<bool, double, double>(false, 0, 0);


        #region Intersection Tests in 2D with Limited Lines
        public static bool TestIntersection(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return false;

            //Begin GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00
            //Macro: cemsim.hga4d.IntersectFiniteLineWithLineSegment2D
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 5 total.
            //Computations: 1.13333333333333 average, 51 total.
            //Memory Reads: 1.84444444444444 average, 83 total.
            //Memory Writes: 45 total.
            //
            //Macro Binding Data: 
            //   result.d120 = variable: var d120
            //   result.d121 = variable: var d121
            //   result.d210 = variable: var d210
            //   result.d211 = variable: var d211
            //   lineOrigin.#e1# = variable: Line.OriginX
            //   lineOrigin.#e2# = variable: Line.OriginY
            //   lineDirection.#e1# = variable: Line.DirectionX
            //   lineDirection.#e2# = variable: Line.DirectionY
            //   lineParamMinValue = variable: lineParamMinValue
            //   lineParamMaxValue = variable: lineParamMaxValue
            //   segmentPoint1.#e1# = variable: lineSegment.Point1X
            //   segmentPoint1.#e2# = variable: lineSegment.Point1Y
            //   segmentPoint2.#e1# = variable: lineSegment.Point2X
            //   segmentPoint2.#e2# = variable: lineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0089 = Times[LLDI0008,LLDI000A]
            tmp0 = line.DirectionX * line.ParameterMinValue;

            //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
            tmp0 = line.OriginX + tmp0;

            //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
            tmp1 = line.DirectionY * line.ParameterMinValue;

            //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
            tmp1 = line.OriginY + tmp1;

            //Sub-expression: LLDI009F = Times[-1,LLDI000C]
            tmp2 = -lineSegment.Point1X;

            //Sub-expression: LLDI00A0 = Plus[LLDI009F,LLDI000E]
            tmp2 = tmp2 + lineSegment.Point2X;

            //Sub-expression: LLDI00A2 = Times[-1,LLDI000D]
            tmp3 = -lineSegment.Point1Y;

            //Sub-expression: LLDI00A3 = Plus[LLDI00A2,LLDI000F]
            tmp3 = tmp3 + lineSegment.Point2Y;

            //Sub-expression: LLDI00A6 = Times[-1,LLDI000D,LLDI00A0]
            tmp4 = -1 * lineSegment.Point1Y * tmp2;

            //Sub-expression: LLDI00A7 = Times[LLDI000C,LLDI00A3]
            tmp5 = lineSegment.Point1X * tmp3;

            //Sub-expression: LLDI00A8 = Plus[LLDI00A6,LLDI00A7]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI00AA = Times[LLDI0091,LLDI00A0]
            tmp5 = tmp1 * tmp2;

            //Sub-expression: LLDI00AB = Times[-1,LLDI008A,LLDI00A3]
            tmp6 = -1 * tmp0 * tmp3;

            //Sub-expression: LLDI00AC = Plus[LLDI00AA,LLDI00AB]
            tmp5 = tmp5 + tmp6;

            //Output: LLDI0004 = Plus[LLDI00AC,LLDI00A8]
            var d211 = tmp5 + tmp4;

            //Sub-expression: LLDI008C = Times[LLDI0008,LLDI000B]
            tmp6 = line.DirectionX * line.ParameterMaxValue;

            //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
            tmp6 = line.OriginX + tmp6;

            //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
            tmp7 = line.DirectionY * line.ParameterMaxValue;

            //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
            tmp7 = line.OriginY + tmp7;

            //Sub-expression: LLDI00A1 = Times[-1,LLDI0094,LLDI00A0]
            tmp2 = -1 * tmp7 * tmp2;

            //Sub-expression: LLDI00A4 = Times[LLDI008D,LLDI00A3]
            tmp3 = tmp6 * tmp3;

            //Sub-expression: LLDI00A5 = Plus[LLDI00A1,LLDI00A4]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI00A9 = Times[-1,LLDI00A8]
            tmp3 = -tmp4;

            //Output: LLDI0003 = Plus[LLDI00A5,LLDI00A9]
            var d210 = tmp2 + tmp3;

            if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
                return false;

            //Sub-expression: LLDI008B = Times[-1,LLDI008A]
            tmp8 = -tmp0;

            //Sub-expression: LLDI008E = Plus[LLDI008B,LLDI008D]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0092 = Times[-1,LLDI0091]
            tmp8 = -tmp1;

            //Sub-expression: LLDI0095 = Plus[LLDI0092,LLDI0094]
            tmp7 = tmp8 + tmp7;

            //Sub-expression: LLDI0098 = Times[-1,LLDI0091,LLDI008E]
            tmp1 = -1 * tmp1 * tmp6;

            //Sub-expression: LLDI0099 = Times[LLDI008A,LLDI0095]
            tmp0 = tmp0 * tmp7;

            //Sub-expression: LLDI009A = Plus[LLDI0098,LLDI0099]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI009C = Times[LLDI000D,LLDI008E]
            tmp1 = lineSegment.Point1Y * tmp6;

            //Sub-expression: LLDI009D = Times[-1,LLDI000C,LLDI0095]
            tmp8 = -1 * lineSegment.Point1X * tmp7;

            //Sub-expression: LLDI009E = Plus[LLDI009C,LLDI009D]
            tmp1 = tmp1 + tmp8;

            //Output: LLDI0002 = Plus[LLDI009E,LLDI009A]
            var d121 = tmp1 + tmp0;

            //Sub-expression: LLDI008F = Times[-1,LLDI000F,LLDI008E]
            tmp1 = -1 * lineSegment.Point2Y * tmp6;

            //Sub-expression: LLDI0096 = Times[LLDI000E,LLDI0095]
            tmp6 = lineSegment.Point2X * tmp7;

            //Sub-expression: LLDI0097 = Plus[LLDI008F,LLDI0096]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI009B = Times[-1,LLDI009A]
            tmp0 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI0097,LLDI009B]
            var d120 = tmp1 + tmp0;

            //Finish GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00

            return (d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0);
        }

        public static bool TestIntersectionVa(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return false;

            var p1 = line.GetPointAt(line.ParameterMinValue);
            var p2 = line.GetPointAt(line.ParameterMaxValue);

            var p3 = lineSegment.GetPoint1();
            var p4 = lineSegment.GetPoint2();

            var d1 = p1.GetSignedDistanceToLineVa(p3, p4);
            var d2 = p2.GetSignedDistanceToLineVa(p3, p4);

            if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
                return false;

            d1 = p3.GetSignedDistanceToLineVa(p1, p2);
            d2 = p4.GetSignedDistanceToLineVa(p1, p2);

            if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
                return false;

            return true;
        }

        public static bool TestIntersectionVaOpt(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            //http://www.cs.swan.ac.uk/~cssimon/line_intersection.html

            if (!lineSegment.IntersectionTestsEnabled)
                return false;

            var p1 = line.GetPointAt(line.ParameterMinValue);
            var p2 = line.GetPointAt(line.ParameterMaxValue);

            var p3 = lineSegment.GetPoint1();
            var p4 = lineSegment.GetPoint2();


            var t2 =
                (p1.Y - p2.Y) * (p4.X - p3.X) -
                (p1.X - p2.X) * (p4.Y - p3.Y);

            if (t2.IsZero())
                return false;

            var ta1 =
                (p3.Y - p4.Y) * (p1.X - p3.X) -
                (p3.X - p4.X) * (p1.Y - p3.Y);

            var ta = ta1 / t2;

            if (ta.Value is < 0 or > 1)
                return false;

            var tb1 =
                (p1.Y - p2.Y) * (p1.X - p3.X) -
                (p1.X - p2.X) * (p1.Y - p3.Y);

            var tb = tb1 / t2;

            if (tb.Value is < 0 or > 1)
                return false;

            return true;
        }
        #endregion


        #region Intersection Computations in 2D with Limited Lines
        public static Tuple<bool, double> ComputeIntersection(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return NoIntersection;

            //Begin GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00
            //Macro: cemsim.hga4d.IntersectFiniteLineWithLineSegment2D
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 5 total.
            //Computations: 1.13333333333333 average, 51 total.
            //Memory Reads: 1.84444444444444 average, 83 total.
            //Memory Writes: 45 total.
            //
            //Macro Binding Data: 
            //   result.d120 = variable: var d120
            //   result.d121 = variable: var d121
            //   result.d210 = variable: var d210
            //   result.d211 = variable: var d211
            //   result.t1 = variable: var t1
            //   lineOrigin.#e1# = variable: Line.OriginX
            //   lineOrigin.#e2# = variable: Line.OriginY
            //   lineDirection.#e1# = variable: Line.DirectionX
            //   lineDirection.#e2# = variable: Line.DirectionY
            //   lineParamMinValue = variable: lineParamMinValue
            //   lineParamMaxValue = variable: lineParamMaxValue
            //   segmentPoint1.#e1# = variable: lineSegment.Point1X
            //   segmentPoint1.#e2# = variable: lineSegment.Point1Y
            //   segmentPoint2.#e1# = variable: lineSegment.Point2X
            //   segmentPoint2.#e2# = variable: lineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0089 = Times[LLDI0008,LLDI000A]
            tmp0 = line.DirectionX * line.ParameterMinValue;

            //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
            tmp0 = line.OriginX + tmp0;

            //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
            tmp1 = line.DirectionY * line.ParameterMinValue;

            //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
            tmp1 = line.OriginY + tmp1;

            //Sub-expression: LLDI009F = Times[-1,LLDI000C]
            tmp2 = -lineSegment.Point1X;

            //Sub-expression: LLDI00A0 = Plus[LLDI009F,LLDI000E]
            tmp2 = tmp2 + lineSegment.Point2X;

            //Sub-expression: LLDI00A2 = Times[-1,LLDI000D]
            tmp3 = -lineSegment.Point1Y;

            //Sub-expression: LLDI00A3 = Plus[LLDI00A2,LLDI000F]
            tmp3 = tmp3 + lineSegment.Point2Y;

            //Sub-expression: LLDI00A6 = Times[-1,LLDI000D,LLDI00A0]
            tmp4 = -1 * lineSegment.Point1Y * tmp2;

            //Sub-expression: LLDI00A7 = Times[LLDI000C,LLDI00A3]
            tmp5 = lineSegment.Point1X * tmp3;

            //Sub-expression: LLDI00A8 = Plus[LLDI00A6,LLDI00A7]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI00AA = Times[LLDI0091,LLDI00A0]
            tmp5 = tmp1 * tmp2;

            //Sub-expression: LLDI00AB = Times[-1,LLDI008A,LLDI00A3]
            tmp6 = -1 * tmp0 * tmp3;

            //Sub-expression: LLDI00AC = Plus[LLDI00AA,LLDI00AB]
            tmp5 = tmp5 + tmp6;

            //Output: LLDI0004 = Plus[LLDI00AC,LLDI00A8]
            var d211 = tmp5 + tmp4;

            //Sub-expression: LLDI008C = Times[LLDI0008,LLDI000B]
            tmp6 = line.DirectionX * line.ParameterMaxValue;

            //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
            tmp6 = line.OriginX + tmp6;

            //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
            tmp7 = line.DirectionY * line.ParameterMaxValue;

            //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
            tmp7 = line.OriginY + tmp7;

            //Sub-expression: LLDI00A1 = Times[-1,LLDI0094,LLDI00A0]
            tmp2 = -1 * tmp7 * tmp2;

            //Sub-expression: LLDI00A4 = Times[LLDI008D,LLDI00A3]
            tmp3 = tmp6 * tmp3;

            //Sub-expression: LLDI00A5 = Plus[LLDI00A1,LLDI00A4]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI00A9 = Times[-1,LLDI00A8]
            tmp3 = -tmp4;

            //Output: LLDI0003 = Plus[LLDI00A5,LLDI00A9]
            var d210 = tmp2 + tmp3;

            if (!((d210 < 0 && d211 < 0) || (d210 > 0 && d211 > 0)))
                return NoIntersection;

            //Sub-expression: LLDI008B = Times[-1,LLDI008A]
            tmp8 = -tmp0;

            //Sub-expression: LLDI008E = Plus[LLDI008B,LLDI008D]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0092 = Times[-1,LLDI0091]
            tmp8 = -tmp1;

            //Sub-expression: LLDI0095 = Plus[LLDI0092,LLDI0094]
            tmp7 = tmp8 + tmp7;

            //Sub-expression: LLDI0098 = Times[-1,LLDI0091,LLDI008E]
            tmp1 = -1 * tmp1 * tmp6;

            //Sub-expression: LLDI0099 = Times[LLDI008A,LLDI0095]
            tmp0 = tmp0 * tmp7;

            //Sub-expression: LLDI009A = Plus[LLDI0098,LLDI0099]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI009C = Times[LLDI000D,LLDI008E]
            tmp1 = lineSegment.Point1Y * tmp6;

            //Sub-expression: LLDI009D = Times[-1,LLDI000C,LLDI0095]
            tmp8 = -1 * lineSegment.Point1X * tmp7;

            //Sub-expression: LLDI009E = Plus[LLDI009C,LLDI009D]
            tmp1 = tmp1 + tmp8;

            //Output: LLDI0002 = Plus[LLDI009E,LLDI009A]
            var d121 = tmp1 + tmp0;

            //Sub-expression: LLDI008F = Times[-1,LLDI000F,LLDI008E]
            tmp1 = -1 * lineSegment.Point2Y * tmp6;

            //Sub-expression: LLDI0096 = Times[LLDI000E,LLDI0095]
            tmp6 = lineSegment.Point2X * tmp7;

            //Sub-expression: LLDI0097 = Plus[LLDI008F,LLDI0096]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI009B = Times[-1,LLDI009A]
            tmp0 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI0097,LLDI009B]
            var d120 = tmp1 + tmp0;

            if (!((d120 < 0 && d121 < 0) || (d120 > 0 && d121 > 0)))
                return NoIntersection;

            //Sub-expression: LLDI00AD = Plus[LLDI00AC,LLDI00A8]
            tmp0 = tmp5 + tmp4;

            //Sub-expression: LLDI00AE = Plus[LLDI00A5,LLDI00A9]
            tmp1 = tmp2 + tmp3;

            //Sub-expression: LLDI00AF = Plus[LLDI00AE,LLDI00AD]
            tmp1 = tmp1 + tmp0;

            //Sub-expression: LLDI00B0 = Power[LLDI00AF,-1]
            tmp1 = 1 / tmp1;

            //Output: LLDI0005 = Times[LLDI00AD,LLDI00B0]
            var t2 = tmp0 * tmp1;

            //Finish GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00

            //Correction to get line parameter t1 w.r.t. line origin and direction
            //because t2 is w.r.t. two end points given by LineParameterLimits
            var t1 = (1 - t2) * line.ParameterMinValue + t2 * line.ParameterMaxValue;

            Debug.Assert(!double.IsNaN(t1));

            return new Tuple<bool, double>(true, t1);
        }

        public static Tuple<bool, double> ComputeIntersectionVa(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return NoIntersection;

            var p1 = line.GetPointAt(line.ParameterMinValue);
            var p2 = line.GetPointAt(line.ParameterMaxValue);

            var p3 = lineSegment.GetPoint1();
            var p4 = lineSegment.GetPoint2();

            var d1 = p1.GetSignedDistanceToLineVa(p3, p4);
            var d2 = p2.GetSignedDistanceToLineVa(p3, p4);

            if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
                return NoIntersection;

            d1 = p1.GetSignedDistanceToLineVa(p3, p4);
            d2 = p2.GetSignedDistanceToLineVa(p3, p4);

            if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
                return NoIntersection;

            var t = d1 / (d1 - d2);

            Debug.Assert(!double.IsNaN(t));

            return Tuple.Create(true, t);
        }

        public static Tuple<bool, double> ComputeIntersectionVaOpt(this ILimitedLine2D line, ILineSegment2D lineSegment)
        {
            //http://www.cs.swan.ac.uk/~cssimon/line_intersection.html

            if (!lineSegment.IntersectionTestsEnabled)
                return NoIntersection;

            var p1 = line.GetPointAt(line.ParameterMinValue);
            var p2 = line.GetPointAt(line.ParameterMaxValue);

            var p3 = lineSegment.GetPoint1();
            var p4 = lineSegment.GetPoint2();

            var t2 =
                (p1.Y - p2.Y) * (p4.X - p3.X) -
                (p1.X - p2.X) * (p4.Y - p3.Y);

            if (t2.IsZero())
                return NoIntersection;

            var ta1 =
                (p3.Y - p4.Y) * (p1.X - p3.X) -
                (p3.X - p4.X) * (p1.Y - p3.Y);

            var ta = ta1 / t2;

            if (ta.Value is < 0 or > 1)
                return NoIntersection;

            var tb1 =
                (p1.Y - p2.Y) * (p1.X - p3.X) -
                (p1.X - p2.X) * (p1.Y - p3.Y);

            var tb = tb1 / t2;

            if (tb.Value is < 0 or > 1)
                return NoIntersection;

            return new Tuple<bool, double>(true, ta);
        }
        #endregion


        #region Intersection Tests in 2D with Infinite Lines
        public static bool TestIntersection(this ILine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return false;

            //Begin GMac Macro Code Generation, 2018-08-23T06:50:38.5618483+02:00
            //Macro: cemsim.hga4d.LineSegmentIntersect2D
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 2 total.
            //Computations: 1.25 average, 15 total.
            //Memory Reads: 1.91666666666667 average, 23 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //   result.d0 = variable: var d0
            //   result.d1 = variable: var d1
            //   rayOrigin.#e1# = variable: Line.OriginX
            //   rayOrigin.#e2# = variable: Line.OriginY
            //   rayDirection.#e1# = variable: Line.DirectionX
            //   rayDirection.#e2# = variable: Line.DirectionY
            //   v0.#e1# = variable: lineSegment.Point1X
            //   v0.#e2# = variable: lineSegment.Point1Y
            //   v1.#e1# = variable: lineSegment.Point2X
            //   v1.#e2# = variable: lineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;

            //Sub-expression: LLDI001A = Times[-1,LLDI0004,LLDI0005]
            tmp0 = -1 * line.OriginY * line.DirectionX;

            //Sub-expression: LLDI001B = Times[LLDI0003,LLDI0006]
            tmp1 = line.OriginX * line.DirectionY;

            //Sub-expression: LLDI001C = Plus[LLDI001A,LLDI001B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001E = Times[-1,LLDI0006,LLDI0007]
            tmp1 = -1 * line.DirectionY * lineSegment.Point1X;

            //Sub-expression: LLDI001F = Times[LLDI0005,LLDI0008]
            tmp2 = line.DirectionX * lineSegment.Point1Y;

            //Sub-expression: LLDI0020 = Plus[LLDI001E,LLDI001F]
            tmp1 = tmp1 + tmp2;

            //Output: LLDI0002 = Plus[LLDI0020,LLDI001C]
            var d1 = tmp1 + tmp0;

            //Sub-expression: LLDI0017 = Times[LLDI0006,LLDI0009]
            tmp1 = line.DirectionY * lineSegment.Point2X;

            //Sub-expression: LLDI0018 = Times[-1,LLDI0005,LLDI000A]
            tmp2 = -1 * line.DirectionX * lineSegment.Point2Y;

            //Sub-expression: LLDI0019 = Plus[LLDI0017,LLDI0018]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI001D = Times[-1,LLDI001C]
            tmp0 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI0019,LLDI001D]
            var d0 = tmp1 + tmp0;


            //Finish GMac Macro Code Generation, 2018-08-23T06:50:38.5618483+02:00

            return (d0 <= 0 && d1 <= 0) || (d0 >= 0 && d1 >= 0);
        }
        #endregion

        #region Intersection Computations in 2D with Infinite Lines
        public static Tuple<bool, double> ComputeIntersection(this ILine2D line, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return NoIntersection;

            //Begin GMac Macro Code Generation, 2018-08-23T06:48:29.9080555+02:00
            //Macro: cemsim.hga4d.LineSegmentIntersect2D
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 34 sub-expressions, 0 generated temps, 34 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 3 total.
            //Computations: 1.08108108108108 average, 40 total.
            //Memory Reads: 1.81081081081081 average, 67 total.
            //Memory Writes: 37 total.
            //
            //Macro Binding Data: 
            //   result.d0 = variable: var d0
            //   result.d1 = variable: var d1
            //   result.t = variable: var t
            //   rayOrigin.#e1# = variable: Ray.OriginX
            //   rayOrigin.#e2# = variable: Ray.OriginY
            //   rayDirection.#e1# = variable: Ray.ItemX
            //   rayDirection.#e2# = variable: Ray.ItemY
            //   v0.#e1# = variable: lineSegment.Point1X
            //   v0.#e2# = variable: lineSegment.Point1Y
            //   v1.#e1# = variable: lineSegment.Point2X
            //   v1.#e2# = variable: lineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            //Sub-expression: LLDI004C = Times[-1,LLDI0005,LLDI0006]
            tmp0 = -1 * line.OriginY * line.DirectionX;

            //Sub-expression: LLDI004D = Times[LLDI0004,LLDI0007]
            tmp1 = line.OriginX * line.DirectionY;

            //Sub-expression: LLDI004E = Plus[LLDI004C,LLDI004D]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0050 = Times[-1,LLDI0007,LLDI0008]
            tmp1 = -1 * line.DirectionY * lineSegment.Point1X;

            //Sub-expression: LLDI0051 = Times[LLDI0006,LLDI0009]
            tmp2 = line.DirectionX * lineSegment.Point1Y;

            //Sub-expression: LLDI0052 = Plus[LLDI0050,LLDI0051]
            tmp1 = tmp1 + tmp2;

            //Output: LLDI0002 = Plus[LLDI0052,LLDI004E]
            var d1 = tmp1 + tmp0;

            //Sub-expression: LLDI0049 = Times[LLDI0007,LLDI000A]
            tmp2 = line.DirectionY * lineSegment.Point2X;

            //Sub-expression: LLDI004A = Times[-1,LLDI0006,LLDI000B]
            tmp3 = -1 * line.DirectionX * lineSegment.Point2Y;

            //Sub-expression: LLDI004B = Plus[LLDI0049,LLDI004A]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI004F = Times[-1,LLDI004E]
            tmp3 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI004B,LLDI004F]
            var d0 = tmp2 + tmp3;

            if (!((d0 <= 0 && d1 <= 0) || (d0 >= 0 && d1 >= 0)))
                return NoIntersection;

            //Sub-expression: LLDI0053 = Times[-1,LLDI0004]
            tmp4 = -line.OriginX;

            //Sub-expression: LLDI0054 = Plus[LLDI004B,LLDI004F]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0055 = Plus[LLDI0052,LLDI004E]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp1 = tmp2 + tmp0;

            //Sub-expression: LLDI0057 = Power[LLDI0056,-1]
            tmp1 = 1 / tmp1;

            //Sub-expression: LLDI0058 = Times[LLDI0054,LLDI0057]
            tmp2 = tmp2 * tmp1;

            //Sub-expression: LLDI0059 = Times[LLDI0008,LLDI0058]
            tmp3 = lineSegment.Point1X * tmp2;

            //Sub-expression: LLDI005A = Times[LLDI0055,LLDI0057]
            tmp0 = tmp0 * tmp1;

            //Sub-expression: LLDI005B = Times[LLDI000A,LLDI005A]
            tmp1 = lineSegment.Point2X * tmp0;

            //Sub-expression: LLDI005C = Plus[LLDI0059,LLDI005B]
            tmp1 = tmp3 + tmp1;

            //Sub-expression: LLDI005D = Plus[LLDI0053,LLDI005C]
            tmp1 = tmp4 + tmp1;

            //Sub-expression: LLDI005E = Power[LLDI0006,2]
            tmp3 = line.DirectionX * line.DirectionX;

            //Sub-expression: LLDI005F = Power[LLDI0007,2]
            tmp4 = line.DirectionY * line.DirectionY;

            //Sub-expression: LLDI0060 = Plus[LLDI005E,LLDI005F]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0061 = Power[LLDI0060,-1]
            tmp3 = 1 / tmp3;

            //Sub-expression: LLDI0062 = Times[LLDI0006,LLDI0061]
            tmp4 = line.DirectionX * tmp3;

            //Sub-expression: LLDI0063 = Times[LLDI005D,LLDI0062]
            tmp1 = tmp1 * tmp4;

            //Sub-expression: LLDI0064 = Times[-1,LLDI0005]
            tmp4 = -line.OriginY;

            //Sub-expression: LLDI0065 = Times[LLDI0009,LLDI0058]
            tmp2 = lineSegment.Point1Y * tmp2;

            //Sub-expression: LLDI0066 = Times[LLDI000B,LLDI005A]
            tmp0 = lineSegment.Point2Y * tmp0;

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp0 = tmp2 + tmp0;

            //Sub-expression: LLDI0068 = Plus[LLDI0064,LLDI0067]
            tmp0 = tmp4 + tmp0;

            //Sub-expression: LLDI0069 = Times[LLDI0007,LLDI0061]
            tmp2 = line.DirectionY * tmp3;

            //Sub-expression: LLDI006A = Times[LLDI0068,LLDI0069]
            tmp0 = tmp0 * tmp2;

            //Output: LLDI0003 = Plus[LLDI0063,LLDI006A]
            var t = tmp1 + tmp0;


            //Finish GMac Macro Code Generation, 2018-08-23T06:48:29.9080555+02:00

            return new Tuple<bool, double>(true, t);
        }

        public static Tuple<bool, double> ComputeIntersection(this ILine2D line, double lineParamMinValue, double lineParamMaxValue, ILineSegment2D lineSegment)
        {
            if (!lineSegment.IntersectionTestsEnabled)
                return NoIntersection;

            //Begin GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00
            //Macro: cemsim.hga4d.IntersectFiniteLineWithLineSegment2D
            //Input Variables: 10 used, 0 not used, 10 total.
            //Temp Variables: 40 sub-expressions, 0 generated temps, 40 total.
            //Target Temp Variables: 9 total.
            //Output Variables: 5 total.
            //Computations: 1.13333333333333 average, 51 total.
            //Memory Reads: 1.84444444444444 average, 83 total.
            //Memory Writes: 45 total.
            //
            //Macro Binding Data: 
            //   result.d120 = variable: var d120
            //   result.d121 = variable: var d121
            //   result.d210 = variable: var d210
            //   result.d211 = variable: var d211
            //   result.t1 = variable: var t1
            //   lineOrigin.#e1# = variable: Line.OriginX
            //   lineOrigin.#e2# = variable: Line.OriginY
            //   lineDirection.#e1# = variable: Line.DirectionX
            //   lineDirection.#e2# = variable: Line.DirectionY
            //   lineParamMinValue = variable: lineParamMinValue
            //   lineParamMaxValue = variable: lineParamMaxValue
            //   segmentPoint1.#e1# = variable: lineSegment.Point1X
            //   segmentPoint1.#e2# = variable: lineSegment.Point1Y
            //   segmentPoint2.#e1# = variable: lineSegment.Point2X
            //   segmentPoint2.#e2# = variable: lineSegment.Point2Y

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;

            //Sub-expression: LLDI0089 = Times[LLDI0008,LLDI000A]
            tmp0 = line.DirectionX * lineParamMinValue;

            //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
            tmp0 = line.OriginX + tmp0;

            //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
            tmp1 = line.DirectionY * lineParamMinValue;

            //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
            tmp1 = line.OriginY + tmp1;

            //Sub-expression: LLDI009F = Times[-1,LLDI000C]
            tmp2 = -lineSegment.Point1X;

            //Sub-expression: LLDI00A0 = Plus[LLDI009F,LLDI000E]
            tmp2 = tmp2 + lineSegment.Point2X;

            //Sub-expression: LLDI00A2 = Times[-1,LLDI000D]
            tmp3 = -lineSegment.Point1Y;

            //Sub-expression: LLDI00A3 = Plus[LLDI00A2,LLDI000F]
            tmp3 = tmp3 + lineSegment.Point2Y;

            //Sub-expression: LLDI00A6 = Times[-1,LLDI000D,LLDI00A0]
            tmp4 = -1 * lineSegment.Point1Y * tmp2;

            //Sub-expression: LLDI00A7 = Times[LLDI000C,LLDI00A3]
            tmp5 = lineSegment.Point1X * tmp3;

            //Sub-expression: LLDI00A8 = Plus[LLDI00A6,LLDI00A7]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI00AA = Times[LLDI0091,LLDI00A0]
            tmp5 = tmp1 * tmp2;

            //Sub-expression: LLDI00AB = Times[-1,LLDI008A,LLDI00A3]
            tmp6 = -1 * tmp0 * tmp3;

            //Sub-expression: LLDI00AC = Plus[LLDI00AA,LLDI00AB]
            tmp5 = tmp5 + tmp6;

            //Output: LLDI0004 = Plus[LLDI00AC,LLDI00A8]
            var d211 = tmp5 + tmp4;

            //Sub-expression: LLDI008C = Times[LLDI0008,LLDI000B]
            tmp6 = line.DirectionX * lineParamMaxValue;

            //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
            tmp6 = line.OriginX + tmp6;

            //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
            tmp7 = line.DirectionY * lineParamMaxValue;

            //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
            tmp7 = line.OriginY + tmp7;

            //Sub-expression: LLDI00A1 = Times[-1,LLDI0094,LLDI00A0]
            tmp2 = -1 * tmp7 * tmp2;

            //Sub-expression: LLDI00A4 = Times[LLDI008D,LLDI00A3]
            tmp3 = tmp6 * tmp3;

            //Sub-expression: LLDI00A5 = Plus[LLDI00A1,LLDI00A4]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI00A9 = Times[-1,LLDI00A8]
            tmp3 = -tmp4;

            //Output: LLDI0003 = Plus[LLDI00A5,LLDI00A9]
            var d210 = tmp2 + tmp3;

            if (!((d210 <= 0 && d211 <= 0) || (d210 >= 0 && d211 >= 0)))
                return NoIntersection;

            //Sub-expression: LLDI008B = Times[-1,LLDI008A]
            tmp8 = -tmp0;

            //Sub-expression: LLDI008E = Plus[LLDI008B,LLDI008D]
            tmp6 = tmp8 + tmp6;

            //Sub-expression: LLDI0092 = Times[-1,LLDI0091]
            tmp8 = -tmp1;

            //Sub-expression: LLDI0095 = Plus[LLDI0092,LLDI0094]
            tmp7 = tmp8 + tmp7;

            //Sub-expression: LLDI0098 = Times[-1,LLDI0091,LLDI008E]
            tmp1 = -1 * tmp1 * tmp6;

            //Sub-expression: LLDI0099 = Times[LLDI008A,LLDI0095]
            tmp0 = tmp0 * tmp7;

            //Sub-expression: LLDI009A = Plus[LLDI0098,LLDI0099]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI009C = Times[LLDI000D,LLDI008E]
            tmp1 = lineSegment.Point1Y * tmp6;

            //Sub-expression: LLDI009D = Times[-1,LLDI000C,LLDI0095]
            tmp8 = -1 * lineSegment.Point1X * tmp7;

            //Sub-expression: LLDI009E = Plus[LLDI009C,LLDI009D]
            tmp1 = tmp1 + tmp8;

            //Output: LLDI0002 = Plus[LLDI009E,LLDI009A]
            var d121 = tmp1 + tmp0;

            //Sub-expression: LLDI008F = Times[-1,LLDI000F,LLDI008E]
            tmp1 = -1 * lineSegment.Point2Y * tmp6;

            //Sub-expression: LLDI0096 = Times[LLDI000E,LLDI0095]
            tmp6 = lineSegment.Point2X * tmp7;

            //Sub-expression: LLDI0097 = Plus[LLDI008F,LLDI0096]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI009B = Times[-1,LLDI009A]
            tmp0 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI0097,LLDI009B]
            var d120 = tmp1 + tmp0;

            if (!((d120 <= 0 && d121 <= 0) || (d120 >= 0 && d121 >= 0)))
                return NoIntersection;

            //Sub-expression: LLDI00AD = Plus[LLDI00AC,LLDI00A8]
            tmp0 = tmp5 + tmp4;

            //Sub-expression: LLDI00AE = Plus[LLDI00A5,LLDI00A9]
            tmp1 = tmp2 + tmp3;

            //Sub-expression: LLDI00AF = Plus[LLDI00AE,LLDI00AD]
            tmp1 = tmp1 + tmp0;

            //Sub-expression: LLDI00B0 = Power[LLDI00AF,-1]
            tmp1 = 1 / tmp1;

            //Output: LLDI0005 = Times[LLDI00AD,LLDI00B0]
            var t2 = tmp0 * tmp1;

            //Finish GMac Macro Code Generation, 2018-08-24T19:21:50.4004634+02:00

            //Correction to get line parameter t1 w.r.t. line origin and direction
            //because t2 is w.r.t. two end points given by lineParamRange
            var t1 = (1 - t2) * lineParamMinValue + t2 * lineParamMaxValue;

            return new Tuple<bool, double>(true, t1);
        }
        #endregion
    }
}
