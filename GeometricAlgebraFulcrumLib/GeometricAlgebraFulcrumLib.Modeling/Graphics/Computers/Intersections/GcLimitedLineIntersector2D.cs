using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Immutable;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space2D.Traversal;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space2D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers.Intersections;

public class GcLimitedLineIntersector2D
{
    public static BooleanOutcomesEventSummary TestLineSegmentIntersectionCounter { get; }
        = BooleanOutcomesEventSummary.Create(
            "GcLimitedLineIntersector2D.TestLineSegmentIntersectionCounter",
            "Test Line Segment - Line Segment Intersection in 2D"
        );

    public static BooleanOutcomesEventSummary ComputeLineSegmentIntersectionCounter { get; }
        = BooleanOutcomesEventSummary.Create(
            "GcLimitedLineIntersector2D.ComputeLineSegmentIntersectionCounter",
            "Compute Line Segment - Line Segment Intersection in 2D"
        );


    public static bool TestIntersection(LineTraversalData2D lineData, IBoundingBox2D boundingBox)
    {
        var corners = new[]
        {
            boundingBox.GetMinCorner(),
            boundingBox.GetMaxCorner()
        };

        var isXNegative = lineData.DirectionSign[0];
        var isXPositive = 1 - isXNegative;
        var isYNegative = lineData.DirectionSign[1];
        var isYPositive = 1 - isYNegative;

        // Check for ray intersection against x and y slabs
        var txMin = (corners[isXNegative].X - lineData.Origin[0]) * lineData.DirectionInv[0];
        var txMax = (corners[isXPositive].X - lineData.Origin[0]) * lineData.DirectionInv[0];
        var tyMin = (corners[isYNegative].Y - lineData.Origin[1]) * lineData.DirectionInv[1];
        var tyMax = (corners[isYPositive].Y - lineData.Origin[1]) * lineData.DirectionInv[1];

        // Update txMax and tyMax to ensure robust bounds intersection
        txMax *= 1 + 2 * Float64Utils.Geomma3;
        tyMax *= 1 + 2 * Float64Utils.Geomma3;
        if (txMin > tyMax || tyMin > txMax)
            return false;

        if (tyMin > txMin) txMin = tyMin;
        if (tyMax < txMax) txMax = tyMax;

        return txMin < lineData.ParameterMaxValue &&
               txMax > lineData.ParameterMinValue;
    }

    public static Tuple<bool, double, double> ComputeIntersections(LineTraversalData2D lineData, IBoundingBox2D boundingBox)
    {
        var corners = new[]
        {
            boundingBox.GetMinCorner(),
            boundingBox.GetMaxCorner()
        };

        var isXNegative = lineData.DirectionSign[0];
        var isXPositive = 1 - isXNegative;
        var isYNegative = lineData.DirectionSign[1];
        var isYPositive = 1 - isYNegative;

        // Check for ray intersection against x and y slabs
        var txMin = (corners[isXNegative].X - lineData.Origin[0]) * lineData.DirectionInv[0];
        var txMax = (corners[isXPositive].X - lineData.Origin[0]) * lineData.DirectionInv[0];
        var tyMin = (corners[isYNegative].Y - lineData.Origin[1]) * lineData.DirectionInv[1];
        var tyMax = (corners[isYPositive].Y - lineData.Origin[1]) * lineData.DirectionInv[1];

        // Update txMax and tyMax to ensure robust bounds intersection
        txMax *= 1 + 2 * Float64Utils.Geomma3;
        tyMax *= 1 + 2 * Float64Utils.Geomma3;
        if (txMin > tyMax || tyMin > txMax)
            return IntersectionUtils.NoIntersectionPair;

        if (tyMin > txMin) txMin = tyMin;
        if (tyMax < txMax) txMax = tyMax;

        return txMin < lineData.ParameterMaxValue &&
               txMax > lineData.ParameterMinValue
            ? Tuple.Create(
                true, 
                Math.Max(txMin, lineData.ParameterMinValue), 
                Math.Min(txMax, lineData.ParameterMaxValue)
            )
            : IntersectionUtils.NoIntersectionPair;
    }


    public IEnumerable<AccBihLineTraversalState2D> BihLineTraversalStates { get; private set; }
        = Enumerable.Empty<AccBihLineTraversalState2D>();



    public Line2D Line { get; private set; }

    public Float64ScalarRange LineParameterLimits { get; private set; }


    /// <summary>
    /// Set the line and its limits from a given line segment. The line origin
    /// is the first end point of the line segment. The line direction is
    /// the difference between the line segment end points. The line limits
    /// are 0 and 1
    /// </summary>
    /// <param name="lineSegment"></param>
    /// <returns></returns>
    public GcLimitedLineIntersector2D SetLineAsLineSegment(ILineSegment2D lineSegment)
    {
        Line = new Line2D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y
        );

        LineParameterLimits = Float64ScalarRange.ZeroToOne;

        return this;
    }

    public GcLimitedLineIntersector2D SetLineAsLineSegment(ILinFloat64Vector2D point1, ILinFloat64Vector2D point2)
    {
        Line = new Line2D(
            point1.X,
            point1.Y,
            point2.X - point1.X,
            point2.Y - point1.Y
        );

        LineParameterLimits = Float64ScalarRange.ZeroToOne;

        return this;
    }

    public GcLimitedLineIntersector2D SetLine(ILinFloat64Vector2D lineOrigin, ILinFloat64Vector2D lineDirection, Float64ScalarRange lineParamLimits)
    {
        Line = new Line2D(
            lineOrigin.X,
            lineOrigin.Y,
            lineDirection.X,
            lineDirection.Y
        );

        LineParameterLimits = lineParamLimits;

        return this;
    }

    public GcLimitedLineIntersector2D SetLine(ILine2D line, Float64ScalarRange lineParamLimits)
    {
        Line = line.ToLine();

        LineParameterLimits = lineParamLimits;

        return this;
    }


    #region Line-Line Segment Intersection
    public bool TestIntersectionVa(ILineSegment2D lineSegment)
    {
        if (!lineSegment.IntersectionTestsEnabled)
            return false;

        TestLineSegmentIntersectionCounter.Begin();

        var p1 = Line.GetPointAt(LineParameterLimits.MinValue);
        var p2 = Line.GetPointAt(LineParameterLimits.MaxValue);

        var p3 = lineSegment.GetPoint1();
        var p4 = lineSegment.GetPoint2();

        var d1 = p1.GetSignedDistanceToLineVa(p3, p4);
        var d2 = p2.GetSignedDistanceToLineVa(p3, p4);

        if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
        {
            TestLineSegmentIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

        d1 = p3.GetSignedDistanceToLineVa(p1, p2);
        d2 = p4.GetSignedDistanceToLineVa(p1, p2);

        if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
        {
            TestLineSegmentIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

        TestLineSegmentIntersectionCounter.EndWithTrueOutcome();

        return true;
    }

    public bool TestIntersectionVaOptimized(ILineSegment2D lineSegment)
    {
        //http://www.cs.swan.ac.uk/~cssimon/line_intersection.html

        if (!lineSegment.IntersectionTestsEnabled)
            return false;

        var p1 = Line.GetPointAt(LineParameterLimits.MinValue);
        var p2 = Line.GetPointAt(LineParameterLimits.MaxValue);

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

        if (ta.ScalarValue is < 0 or > 1)
            return false;

        var tb1 =
            (p1.Y - p2.Y) * (p1.X - p3.X) -
            (p1.X - p2.X) * (p1.Y - p3.Y);

        var tb = tb1 / t2;

        if (tb.ScalarValue is < 0 or > 1)
            return false;

        return true;
    }

    public bool TestIntersection(ILineSegment2D lineSegment)
    {
        if (!lineSegment.IntersectionTestsEnabled)
            return false;

        TestLineSegmentIntersectionCounter.Begin();

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
        tmp0 = Line.DirectionX * LineParameterLimits.MinValue;

        //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
        tmp0 = Line.OriginX + tmp0;

        //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
        tmp1 = Line.DirectionY * LineParameterLimits.MinValue;

        //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
        tmp1 = Line.OriginY + tmp1;

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
        tmp6 = Line.DirectionX * LineParameterLimits.MaxValue;

        //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
        tmp6 = Line.OriginX + tmp6;

        //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
        tmp7 = Line.DirectionY * LineParameterLimits.MaxValue;

        //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
        tmp7 = Line.OriginY + tmp7;

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
        {
            TestLineSegmentIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

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

        var result = (d120 < 0 && d121 < 0) || (d120 > 0 && d121 > 0);

        TestLineSegmentIntersectionCounter.End(result);

        return result;
    }


    public Tuple<bool, double> ComputeIntersectionVa(ILineSegment2D lineSegment)
    {
        if (!lineSegment.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        var p1 = Line.GetPointAt(LineParameterLimits.MinValue);
        var p2 = Line.GetPointAt(LineParameterLimits.MaxValue);

        var p3 = lineSegment.GetPoint1();
        var p4 = lineSegment.GetPoint2();

        var d1 = p1.GetSignedDistanceToLineVa(p3, p4);
        var d2 = p2.GetSignedDistanceToLineVa(p3, p4);

        if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
            return IntersectionUtils.NoIntersection;

        d1 = p1.GetSignedDistanceToLineVa(p3, p4);
        d2 = p2.GetSignedDistanceToLineVa(p3, p4);

        if (!(d1 < 0 && d2 > 0) && !(d1 > 0 && d2 < 0))
            return IntersectionUtils.NoIntersection;

        var t = d1 / (d1 - d2);

        Debug.Assert(!double.IsNaN(t));

        return Tuple.Create(true, t);
    }

    public Tuple<bool, double> ComputeIntersectionVaOptimized(ILineSegment2D lineSegment)
    {
        //http://www.cs.swan.ac.uk/~cssimon/line_intersection.html

        if (!lineSegment.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        var p1 = Line.GetPointAt(LineParameterLimits.MinValue);
        var p2 = Line.GetPointAt(LineParameterLimits.MaxValue);

        var p3 = lineSegment.GetPoint1();
        var p4 = lineSegment.GetPoint2();

        var t2 =
            (p1.Y - p2.Y) * (p4.X - p3.X) -
            (p1.X - p2.X) * (p4.Y - p3.Y);

        if (t2.IsZero())
            return IntersectionUtils.NoIntersection;

        var ta1 =
            (p3.Y - p4.Y) * (p1.X - p3.X) -
            (p3.X - p4.X) * (p1.Y - p3.Y);

        var ta = ta1 / t2;

        if (ta.ScalarValue is < 0 or > 1)
            return IntersectionUtils.NoIntersection;

        var tb1 =
            (p1.Y - p2.Y) * (p1.X - p3.X) -
            (p1.X - p2.X) * (p1.Y - p3.Y);

        var tb = tb1 / t2;

        if (tb.ScalarValue is < 0 or > 1)
            return IntersectionUtils.NoIntersection;

        return new Tuple<bool, double>(true, ta);
    }

    public Tuple<bool, double> ComputeIntersection(ILineSegment2D lineSegment)
    {
        if (!lineSegment.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        ComputeLineSegmentIntersectionCounter.Begin();

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
        tmp0 = Line.DirectionX * LineParameterLimits.MinValue;

        //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
        tmp0 = Line.OriginX + tmp0;

        //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
        tmp1 = Line.DirectionY * LineParameterLimits.MinValue;

        //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
        tmp1 = Line.OriginY + tmp1;

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
        tmp6 = Line.DirectionX * LineParameterLimits.MaxValue;

        //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
        tmp6 = Line.OriginX + tmp6;

        //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
        tmp7 = Line.DirectionY * LineParameterLimits.MaxValue;

        //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
        tmp7 = Line.OriginY + tmp7;

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
        {
            ComputeLineSegmentIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

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
        {
            ComputeLineSegmentIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

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
        var t1 = (1 - t2) * LineParameterLimits.MinValue + t2 * LineParameterLimits.MaxValue;

        Debug.Assert(!double.IsNaN(t1));

        ComputeLineSegmentIntersectionCounter.EndWithTrueOutcome();

        return new Tuple<bool, double>(true, t1);
    }

    public Tuple<bool, double> ComputeIntersection(ILineSegment2D lineSegment, double lineParamMinValue, double lineParamMaxValue)
    {
        if (!lineSegment.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

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
        tmp0 = Line.DirectionX * lineParamMinValue;

        //Sub-expression: LLDI008A = Plus[LLDI0006,LLDI0089]
        tmp0 = Line.OriginX + tmp0;

        //Sub-expression: LLDI0090 = Times[LLDI0009,LLDI000A]
        tmp1 = Line.DirectionY * lineParamMinValue;

        //Sub-expression: LLDI0091 = Plus[LLDI0007,LLDI0090]
        tmp1 = Line.OriginY + tmp1;

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
        tmp6 = Line.DirectionX * lineParamMaxValue;

        //Sub-expression: LLDI008D = Plus[LLDI0006,LLDI008C]
        tmp6 = Line.OriginX + tmp6;

        //Sub-expression: LLDI0093 = Times[LLDI0009,LLDI000B]
        tmp7 = Line.DirectionY * lineParamMaxValue;

        //Sub-expression: LLDI0094 = Plus[LLDI0007,LLDI0093]
        tmp7 = Line.OriginY + tmp7;

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
            return IntersectionUtils.NoIntersection;

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
            return IntersectionUtils.NoIntersection;

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

        Debug.Assert(!double.IsNaN(t1));

        return new Tuple<bool, double>(true, t1);
    }


    /// <summary>
    /// Test if the finite line (a line segment) intersects any of the given
    /// line segments
    /// </summary>
    /// <param name="lineSegmentsList"></param>
    /// <returns></returns>
    public bool TestIntersection(IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        return lineSegmentsList.Any(TestIntersection);
    }

    public IEnumerable<Tuple<double, ILineSegment2D>> ComputeIntersections(IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        foreach (var lineSegment in lineSegmentsList)
        {
            var result = ComputeIntersection(lineSegment);

            if (result.Item1)
                yield return new Tuple<double, ILineSegment2D>(
                    result.Item2, lineSegment
                );
        }
    }

    public Tuple<bool, double, ILineSegment2D> ComputeFirstIntersection(IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        var hasIntersection = false;
        var tValue = double.PositiveInfinity;
        ILineSegment2D hitLineSegment = null;

        foreach (var lineSegment in lineSegmentsList)
        {
            var result = ComputeIntersection(lineSegment);

            if (!result.Item1 || tValue <= result.Item2)
                continue;

            hasIntersection = true;
            tValue = result.Item2;
            hitLineSegment = lineSegment;
        }

        return new Tuple<bool, double, ILineSegment2D>(
            hasIntersection,
            tValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, ILineSegment2D> ComputeLastIntersection(IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        var hasIntersection = false;
        var tValue = double.NegativeInfinity;
        ILineSegment2D hitLineSegment = null;

        foreach (var lineSegment in lineSegmentsList)
        {
            var result = ComputeIntersection(lineSegment);

            if (!result.Item1 || tValue > result.Item2)
                continue;

            hasIntersection = true;
            tValue = result.Item2;
            hitLineSegment = lineSegment;
        }

        return new Tuple<bool, double, ILineSegment2D>(
            hasIntersection,
            tValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, double, ILineSegment2D, ILineSegment2D> ComputeEdgeIntersections(IEnumerable<ILineSegment2D> lineSegmentsList)
    {
        var hasIntersection = false;
        var tValue1 = double.PositiveInfinity;
        var tValue2 = double.NegativeInfinity;
        ILineSegment2D hitLineSegment1 = null;
        ILineSegment2D hitLineSegment2 = null;

        foreach (var lineSegment in lineSegmentsList)
        {
            var result = ComputeIntersection(lineSegment);

            if (!result.Item1)
                continue;

            hasIntersection = true;

            if (tValue1 > result.Item2)
            {
                tValue1 = result.Item2;
                hitLineSegment1 = lineSegment;
            }

            if (tValue2 < result.Item2)
            {
                tValue2 = result.Item2;
                hitLineSegment2 = lineSegment;
            }
        }

        return Tuple.Create(
            hasIntersection,
            tValue1,
            tValue2,
            hitLineSegment1,
            hitLineSegment2
        );
    }
    #endregion


    #region Line-Bounding Box Intersection
    public bool TestIntersection(IBoundingBox2D boundingBox)
    {
        var tMin = LineParameterLimits.MinValue;
        var tMax = LineParameterLimits.MaxValue;

        //Compute intersection parameters of ray with Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / Line.DirectionX;
            var tSlap1 = (boundingBox.MinX - Line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - Line.OriginX) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return false;
        }

        //Compute intersection parameters of ray with X slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / Line.DirectionY;
            var tSlap1 = (boundingBox.MinY - Line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - Line.OriginY) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return false;
        }

        return true;
    }

    public Tuple<bool, double, double> ComputeIntersections(IBoundingBox2D boundingBox)
    {
        var tMin = LineParameterLimits.MinValue;
        var tMax = LineParameterLimits.MaxValue;

        //Compute intersection parameters of ray with Y slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / Line.DirectionX;
            var tSlap1 = (boundingBox.MinX - Line.OriginX) * invRayDir;
            var tSlap2 = (boundingBox.MaxX - Line.OriginX) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return IntersectionUtils.NoIntersectionPair;
        }

        //Compute intersection parameters of ray with X slaps
        {
            // Update interval for _i_th bounding box slab
            var invRayDir = 1.0d / Line.DirectionY;
            var tSlap1 = (boundingBox.MinY - Line.OriginY) * invRayDir;
            var tSlap2 = (boundingBox.MaxY - Line.OriginY) * invRayDir;

            // Update parametric interval from slab intersection t values
            if (tSlap1 > tSlap2)
            {
                (tSlap1, tSlap2) = (tSlap2, tSlap1);
            }

            // Update tFar to ensure robust ray-bounds intersection
            tSlap2 *= 1 + 2 * Float64Utils.Geomma3;
            tMin = tSlap1 > tMin ? tSlap1 : tMin;
            tMax = tSlap2 < tMax ? tSlap2 : tMax;

            if (tMin > tMax)
                return IntersectionUtils.NoIntersectionPair;
        }

        return new Tuple<bool, double, double>(true, tMin.ScalarValue, tMax.ScalarValue);
    }


    public bool TestIntersection(IEnumerable<IBoundingBox2D> boundingBoxesList)
    {
        var lineData = Line.GetLineTraversalData(LineParameterLimits);

        return boundingBoxesList
            .Any(b => TestIntersection(lineData, b));
    }
    #endregion


    #region Line-Acceleration Grid Intersection
    public bool TestIntersection(IAccGrid2D<ILineSegment2D> grid)
    {
        return grid
            .GetLineTraverser(Line, LineParameterLimits)
            .GetCells()
            .Where(cell => !ReferenceEquals(cell, null))
            .Select(cell => TestIntersection((IEnumerable<ILineSegment2D>)cell))
            .Any(v => v);
    }

    public IEnumerable<Tuple<double, ILineSegment2D>> ComputeIntersections(IAccGrid2D<ILineSegment2D> grid)
    {
        var lineTraverser = AccGridLineTraverser2D.Create(grid, Line, LineParameterLimits);

        foreach (var cell in lineTraverser.GetActiveCells())
        {
            var tList =
                ComputeIntersections((IEnumerable<ILineSegment2D>)cell);

            foreach (var t in tList.Where(t => t.Item1 < lineTraverser.TNext))
                yield return t;
        }
    }

    public Tuple<bool, double, ILineSegment2D> ComputeFirstIntersection(IAccGrid2D<ILineSegment2D> grid)
    {
        var lineTraverser = AccGridLineTraverser2D.Create(grid, Line, LineParameterLimits);

        foreach (var cell in lineTraverser.GetActiveCells())
        {
            var t =
                ComputeFirstIntersection((IEnumerable<ILineSegment2D>)cell);

            if (t.Item1 && t.Item2 < lineTraverser.TNext)
                return t;
        }

        return new Tuple<bool, double, ILineSegment2D>(false, 0, null);
    }

    public Tuple<bool, double, ILineSegment2D> ComputeLastIntersection(IAccGrid2D<ILineSegment2D> grid)
    {
        var oldLine = Line;
        var oldLineParameterLimits = LineParameterLimits;

        Line = new Line2D(
            oldLine.OriginX + oldLine.DirectionX,
            oldLine.OriginY + oldLine.DirectionY,
            -oldLine.DirectionX,
            -oldLine.DirectionY
        );

        LineParameterLimits = 1d - oldLineParameterLimits;

        var result = ComputeFirstIntersection(grid);

        Line = oldLine;
        LineParameterLimits = oldLineParameterLimits;

        return result.Item1
            ? Tuple.Create(true, 1 - result.Item2, result.Item3)
            : new Tuple<bool, double, ILineSegment2D>(false, 0, null);
    }

    public Tuple<bool, double, double, ILineSegment2D, ILineSegment2D> ComputeEdgeIntersections(IAccGrid2D<ILineSegment2D> grid)
    {
        var first = ComputeFirstIntersection(grid);
        var last = ComputeLastIntersection(grid);

        if (first.Item1 && last.Item1) 
            return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
                true,
                first.Item2,
                last.Item2,
                first.Item3,
                last.Item3
            );

        if (first.Item1)
            return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
                true,
                first.Item2,
                first.Item2,
                first.Item3,
                first.Item3
            );

        if (last.Item1)
            return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
                true,
                last.Item2,
                last.Item2,
                last.Item3,
                last.Item3
            );

        return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
            false,
            0,
            0,
            null,
            null
        );
    }
    #endregion


    #region Line-Acceleration BIH Intersection
    public bool TestIntersection(IAccBih2D<ILineSegment2D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState2D>();

        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return false;

        var lineTraverser = bih.GetLineTraverser(
            Line, lineLimits.Item2, lineLimits.Item3
        );

        var traversalStates =
            lineTraverser
                .GetTraversalStates(storeTraversalStates);

        var hasIntersection = false;
        foreach (var state in traversalStates)
        {
            if (state.BihNode.IsLeaf)
            {
                var flag = TestIntersection(
                    (IEnumerable<ILineSegment2D>) state.BihNode
                );

                if (flag)
                {
                    hasIntersection = true;
                    break;
                }
            }
        }

        //var hasIntersection =
        //    lineTraverser
        //        .GetLeafTraversalStates(storeTraversalStates)
        //        .Any(state => TestIntersection((IAccBihNode2D<ILineSegment2D>)state.BihNode));

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return hasIntersection;
    }

    public IEnumerable<Tuple<double, ILineSegment2D>> ComputeIntersections(IAccBih2D<ILineSegment2D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState2D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            yield break;

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        foreach (var state in lineTraverser.GetLeafTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode2D<ILineSegment2D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            //For leaf nodes find all intersections within all its geometric
            //objects
            foreach (var lineSegment in node)
            {
                var result = ComputeIntersection(lineSegment, t0, t1);

                if (result.Item1)
                    yield return new Tuple<double, ILineSegment2D>(
                        result.Item2,
                        lineSegment
                    );
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;
    }

    public Tuple<bool, double, ILineSegment2D> ComputeFirstIntersection(IAccBih2D<ILineSegment2D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState2D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, ILineSegment2D>(false, 0, null);

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        var hasIntersection = false;
        ILineSegment2D hitLineSegment = null;

        foreach (var state in lineTraverser.GetTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode2D<ILineSegment2D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            if (!node.IsLeaf)
                continue;

            //For leaf nodes find first intersection within all its geometric
            //objects
            foreach (var lineSegment in node)
            {
                var result = ComputeIntersection(lineSegment, t0, t1);

                if (!result.Item1)
                    continue;

                hasIntersection = true;

                if (lineTraverser.LineParameterRange.MaxValue > result.Item2)
                {
                    hitLineSegment = lineSegment;

                    lineTraverser.ResetMaxParameterValue(result.Item2);
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, ILineSegment2D>(
            hasIntersection,
            lineTraverser.LineParameterRange.MaxValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, ILineSegment2D> ComputeLastIntersection(IAccBih2D<ILineSegment2D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState2D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, ILineSegment2D>(false, 0, null);

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        var hasIntersection = false;
        ILineSegment2D hitLineSegment = null;

        foreach (var state in lineTraverser.GetTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode2D<ILineSegment2D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            if (!node.IsLeaf)
                continue;

            //For leaf nodes find last intersection within all its geometric
            //objects
            foreach (var lineSegment in node)
            {
                var result = ComputeIntersection(lineSegment, t0, t1);

                if (!result.Item1)
                    continue;

                hasIntersection = true;

                if (lineTraverser.LineParameterRange.MinValue < result.Item2)
                {
                    hitLineSegment = lineSegment;

                    lineTraverser.ResetMinParameterValue(result.Item2);
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, ILineSegment2D>(
            hasIntersection,
            lineTraverser.LineParameterRange.MinValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, double, ILineSegment2D, ILineSegment2D> ComputeEdgeIntersections(IAccBih2D<ILineSegment2D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState2D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
                false,
                0, 0,
                null, null
            );

        var hasIntersection = false;
        var tValue1 = double.PositiveInfinity;
        var tValue2 = double.NegativeInfinity;
        ILineSegment2D hitLineSegment1 = null;
        ILineSegment2D hitLineSegment2 = null;

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        foreach (var state in lineTraverser.GetLeafTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode2D<ILineSegment2D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            //For leaf nodes find all intersections within all its geometric
            //objects
            foreach (var lineSegment in node)
            {
                var result = ComputeIntersection(lineSegment, t0, t1);

                if (!result.Item1) continue;

                hasIntersection = true;

                if (tValue1 > result.Item2)
                {
                    tValue1 = result.Item2;
                    hitLineSegment1 = lineSegment;
                }

                if (tValue2 < result.Item2)
                {
                    tValue2 = result.Item2;
                    hitLineSegment2 = lineSegment;
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, double, ILineSegment2D, ILineSegment2D>(
            hasIntersection,
            tValue1,
            tValue2,
            hitLineSegment1,
            hitLineSegment2
        );
    }
    #endregion


    public bool TestIntersection(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    {
        var grid = lineSegmentsList as IAccGrid2D<ILineSegment2D>;
        if (!ReferenceEquals(grid, null))
            return TestIntersection(grid);

        var bih = lineSegmentsList as IAccBih2D<ILineSegment2D>;
        if (!ReferenceEquals(bih, null))
            return TestIntersection(bih);

        return TestIntersection(
            (IEnumerable<ILineSegment2D>)lineSegmentsList
        );
    }

    public IEnumerable<Tuple<double, ILineSegment2D>> ComputeIntersections(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    {
        var grid = lineSegmentsList as IAccGrid2D<ILineSegment2D>;
        if (!ReferenceEquals(grid, null))
            return ComputeIntersections(grid);

        var bih = lineSegmentsList as IAccBih2D<ILineSegment2D>;
        if (!ReferenceEquals(bih, null))
            return ComputeIntersections(bih);

        return ComputeIntersections(
            (IEnumerable<ILineSegment2D>)lineSegmentsList
        );
    }

    public Tuple<bool, double, ILineSegment2D> ComputeFirstIntersection(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    {
        var grid = lineSegmentsList as IAccGrid2D<ILineSegment2D>;
        if (!ReferenceEquals(grid, null))
            return ComputeFirstIntersection(grid);

        var bih = lineSegmentsList as IAccBih2D<ILineSegment2D>;
        if (!ReferenceEquals(bih, null))
            return ComputeFirstIntersection(bih);

        return ComputeFirstIntersection(
            (IEnumerable<ILineSegment2D>)lineSegmentsList
        );
    }

    public Tuple<bool, double, ILineSegment2D> ComputeLastIntersection(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    {
        var grid = lineSegmentsList as IAccGrid2D<ILineSegment2D>;
        if (!ReferenceEquals(grid, null))
            return ComputeLastIntersection(grid);

        var bih = lineSegmentsList as IAccBih2D<ILineSegment2D>;
        if (!ReferenceEquals(bih, null))
            return ComputeLastIntersection(bih);

        return ComputeLastIntersection(
            (IEnumerable<ILineSegment2D>)lineSegmentsList
        );
    }

    public Tuple<bool, double, double, ILineSegment2D, ILineSegment2D> ComputeEdgeIntersections(IGeometricObjectsContainer2D<ILineSegment2D> lineSegmentsList)
    {
        var grid = lineSegmentsList as IAccGrid2D<ILineSegment2D>;
        if (!ReferenceEquals(grid, null))
            return ComputeEdgeIntersections(grid);

        var bih = lineSegmentsList as IAccBih2D<ILineSegment2D>;
        if (!ReferenceEquals(bih, null))
            return ComputeEdgeIntersections(bih);

        return ComputeEdgeIntersections(
            (IEnumerable<ILineSegment2D>)lineSegmentsList
        );
    }
}