using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space3D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Triangles.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.Borders.Space3D.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.BIH.Space3D.Traversal;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids;
using GeometricAlgebraFulcrumLib.Modeling.Graphics.Accelerators.Grids.Space3D;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers.Intersections;

public class GcLimitedLineIntersector3D
{
    public static BooleanOutcomesEventSummary TestTriangleIntersectionCounter { get; }
        = BooleanOutcomesEventSummary.Create(
            "GcLimitedLineIntersector3D.TestTriangleIntersectionCounter",
            "Test Line Segment - Triangle Intersection in 3D"
        );

    public static BooleanOutcomesEventSummary ComputeTriangleIntersectionCounter { get; }
        = BooleanOutcomesEventSummary.Create(
            "GcLimitedLineIntersector3D.ComputeTriangleIntersectionCounter",
            "Compute Line Segment - Triangle Intersection in 3D"
        );


    public bool ExcludeEdges { get; set; } = true;

    private bool IsLineInsideTriangle(double d1, double d2, double d3)
    {
        //var dSumInv = 1 / (d1 + d2 + d3);

        //var w1 = d1 * dSumInv;
        //var w2 = d2 * dSumInv;
        //var w3 = d3 * dSumInv;

        //if (w1 > 0 && w2 > -1e-10 && w3 > -1e-10) return true;
        //if (w2 > 0 && w3 > -1e-10 && w1 > -1e-10) return true;
        //if (w3 > 0 && w1 > -1e-10 && w2 > -1e-10) return true;

        //return false;

        if (ExcludeEdges)
            return
                (d1 < 0 && d2 < 0 && d3 < 0) ||
                (d1 > 0 && d2 > 0 && d3 > 0);

        var dSum = d1 + d2 + d3;

        return
            (d1 <= 0 && d2 <= 0 && d3 <= 0 && dSum < 0) ||
            (d1 >= 0 && d2 >= 0 && d3 >= 0 && dSum > 0);
    }

    public static bool TestIntersection(LineTraversalData3D lineData, IFloat64BoundingBox3D boundingBox)
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
        var txMin = (corners[isXNegative].X - lineData.Origin.X) * lineData.DirectionInv.X;
        var txMax = (corners[isXPositive].X - lineData.Origin.X) * lineData.DirectionInv.X;
        var tyMin = (corners[isYNegative].Y - lineData.Origin.Y) * lineData.DirectionInv.Y;
        var tyMax = (corners[isYPositive].Y - lineData.Origin.Y) * lineData.DirectionInv.Y;

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

    public static Tuple<bool, double, double> ComputeIntersections(LineTraversalData3D lineData, IFloat64BoundingBox3D boundingBox)
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
        var txMin = (corners[isXNegative].X - lineData.Origin.X) * lineData.DirectionInv.X;
        var txMax = (corners[isXPositive].X - lineData.Origin.X) * lineData.DirectionInv.X;
        var tyMin = (corners[isYNegative].Y - lineData.Origin.Y) * lineData.DirectionInv.Y;
        var tyMax = (corners[isYPositive].Y - lineData.Origin.Y) * lineData.DirectionInv.Y;

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


    public IEnumerable<AccBihLineTraversalState3D> BihLineTraversalStates { get; private set; }
        = Enumerable.Empty<AccBihLineTraversalState3D>();


    public Float64Line3D Line { get; private set; }

    public Float64ScalarRange LineParameterLimits { get; private set; }


    /// <summary>
    /// Set the line and its limits from a given line segment. The line origin
    /// is the first end point of the line segment. The line direction is
    /// the difference between the line segment end points. The line limits
    /// are 0 and 1
    /// </summary>
    /// <param name="lineSegment"></param>
    /// <returns></returns>
    public GcLimitedLineIntersector3D SetLineAsLineSegment(IFloat64LineSegment3D lineSegment)
    {
        Line = new Float64Line3D(
            lineSegment.Point1X,
            lineSegment.Point1Y,
            lineSegment.Point1Z,
            lineSegment.Point2X - lineSegment.Point1X,
            lineSegment.Point2Y - lineSegment.Point1Y,
            lineSegment.Point2Z - lineSegment.Point1Z
        );

        LineParameterLimits = Float64ScalarRange.ZeroToOne;

        return this;
    }

    public GcLimitedLineIntersector3D SetLineAsLineSegment(ILinFloat64Vector3D point1, ILinFloat64Vector3D point2)
    {
        Line = new Float64Line3D(
            point1.X,
            point1.Y,
            point1.Z,
            point2.X - point1.X,
            point2.Y - point1.Y,
            point2.Z - point1.Z
        );

        LineParameterLimits = Float64ScalarRange.ZeroToOne;

        return this;
    }

    public GcLimitedLineIntersector3D SetLine(ILinFloat64Vector3D lineOrigin, ILinFloat64Vector3D lineDirection, Float64ScalarRange lineParamLimits)
    {
        Line = new Float64Line3D(
            lineOrigin.X,
            lineOrigin.Y,
            lineOrigin.Z,
            lineDirection.X,
            lineDirection.Y,
            lineDirection.Z
        );

        LineParameterLimits = lineParamLimits;

        return this;
    }

    public GcLimitedLineIntersector3D SetLine(IFloat64Line3D line, Float64ScalarRange lineParamLimits)
    {
        Line = line.ToLine();

        LineParameterLimits = lineParamLimits;

        return this;
    }


    #region Line-Triangle Intersection

    public bool TestIntersectionVa(IFloat64Triangle3D triangle)
    {
        if (!triangle.IntersectionTestsEnabled)
            return false;

        TestTriangleIntersectionCounter.Begin();

        var d1 = Line.GetSignedDistanceToLineVa(triangle.GetLine23());
        var d2 = Line.GetSignedDistanceToLineVa(triangle.GetLine31());
        var d3 = Line.GetSignedDistanceToLineVa(triangle.GetLine12());

        if (!IsLineInsideTriangle(d1, d2, d3))
        {
            TestTriangleIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

        var s1 = Line
            .GetPointAt(LineParameterLimits.MinValue)
            .GetSignedDistanceToPlaneVa(triangle);

        var s2 = Line
            .GetPointAt(LineParameterLimits.MaxValue)
            .GetSignedDistanceToPlaneVa(triangle);

        if (!(s1 < 0 && s2 > 0) && !(s1 > 0 && s2 < 0))
        {
            TestTriangleIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

        TestTriangleIntersectionCounter.EndWithTrueOutcome();

        return true;
    }

    public bool TestIntersection(IFloat64Triangle3D triangle)
    {
        if (!triangle.IntersectionTestsEnabled)
            return false;

        TestTriangleIntersectionCounter.Begin();

        var linePoint1X = Line.OriginX + LineParameterLimits.MinValue * Line.DirectionX;
        var linePoint1Y = Line.OriginY + LineParameterLimits.MinValue * Line.DirectionY;
        var linePoint1Z = Line.OriginZ + LineParameterLimits.MinValue * Line.DirectionZ;

        var linePoint2X = Line.OriginX + LineParameterLimits.MaxValue * Line.DirectionX;
        var linePoint2Y = Line.OriginY + LineParameterLimits.MaxValue * Line.DirectionY;
        var linePoint2Z = Line.OriginZ + LineParameterLimits.MaxValue * Line.DirectionZ;


        //Begin GMac Macro Code Generation, 2018-10-05T22:07:42.3237425+02:00
        //Macro: cemsim.hga4d.LinePlaneIntersect3D
        //Input Variables: 15 used, 0 not used, 15 total.
        //Temp Variables: 122 sub-expressions, 0 generated temps, 122 total.
        //Target Temp Variables: 9 total.
        //Output Variables: 4 total.
        //Computations: 1.1984126984127 average, 151 total.
        //Memory Reads: 1.88888888888889 average, 238 total.
        //Memory Writes: 126 total.
        //
        //Macro Binding Data: 
        //   result.d1 = variable: var d1
        //   result.d2 = variable: var d2
        //   result.d3 = variable: var d3
        //   result.t = variable: var t
        //   linePoint1.#e1# = variable: linePoint1X
        //   linePoint1.#e2# = variable: linePoint1Y
        //   linePoint1.#e3# = variable: linePoint1Z
        //   linePoint2.#e1# = variable: linePoint2X
        //   linePoint2.#e2# = variable: linePoint2Y
        //   linePoint2.#e3# = variable: linePoint2Z
        //   planePoint1.#e1# = variable: triangle.Point1X
        //   planePoint1.#e2# = variable: triangle.Point1Y
        //   planePoint1.#e3# = variable: triangle.Point1Z
        //   planePoint2.#e1# = variable: triangle.Point2X
        //   planePoint2.#e2# = variable: triangle.Point2Y
        //   planePoint2.#e3# = variable: triangle.Point2Z
        //   planePoint3.#e1# = variable: triangle.Point3X
        //   planePoint3.#e2# = variable: triangle.Point3Y
        //   planePoint3.#e3# = variable: triangle.Point3Z

        double tmp0;
        double tmp1;
        double tmp2;
        double tmp3;
        double tmp4;
        double tmp5;
        double tmp6;
        double tmp7;
        double tmp8;

        //Sub-expression: LLDI005A = Times[-1,LLDI000A]
        tmp0 = -linePoint2Z;

        //Sub-expression: LLDI005B = Plus[LLDI0007,LLDI005A]
        tmp0 = linePoint1Z + tmp0;

        //Sub-expression: LLDI005C = Times[-1,LLDI000F,LLDI0011]
        tmp1 = -1 * triangle.Point2Y * triangle.Point3X;

        //Sub-expression: LLDI005D = Times[LLDI000E,LLDI0012]
        tmp2 = triangle.Point2X * triangle.Point3Y;

        //Sub-expression: LLDI005E = Plus[LLDI005C,LLDI005D]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI005F = Times[LLDI005B,LLDI005E]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0060 = Times[-1,LLDI0009]
        tmp2 = -linePoint2Y;

        //Sub-expression: LLDI0061 = Plus[LLDI0006,LLDI0060]
        tmp2 = linePoint1Y + tmp2;

        //Sub-expression: LLDI0062 = Times[-1,LLDI0010,LLDI0011]
        tmp3 = -1 * triangle.Point2Z * triangle.Point3X;

        //Sub-expression: LLDI0063 = Times[LLDI000E,LLDI0013]
        tmp4 = triangle.Point2X * triangle.Point3Z;

        //Sub-expression: LLDI0064 = Plus[LLDI0062,LLDI0063]
        tmp3 = tmp3 + tmp4;

        //Sub-expression: LLDI0065 = Times[-1,LLDI0061,LLDI0064]
        tmp3 = -1 * tmp2 * tmp3;

        //Sub-expression: LLDI0066 = Plus[LLDI005F,LLDI0065]
        tmp1 = tmp1 + tmp3;

        //Sub-expression: LLDI0067 = Times[-1,LLDI0008]
        tmp3 = -linePoint2X;

        //Sub-expression: LLDI0068 = Plus[LLDI0005,LLDI0067]
        tmp3 = linePoint1X + tmp3;

        //Sub-expression: LLDI0069 = Times[-1,LLDI0010,LLDI0012]
        tmp4 = -1 * triangle.Point2Z * triangle.Point3Y;

        //Sub-expression: LLDI006A = Times[LLDI000F,LLDI0013]
        tmp5 = triangle.Point2Y * triangle.Point3Z;

        //Sub-expression: LLDI006B = Plus[LLDI0069,LLDI006A]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI006C = Times[LLDI0068,LLDI006B]
        tmp4 = tmp3 * tmp4;

        //Sub-expression: LLDI006D = Plus[LLDI0066,LLDI006C]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI006E = Times[-1,LLDI0007,LLDI0009]
        tmp4 = -1 * linePoint1Z * linePoint2Y;

        //Sub-expression: LLDI006F = Times[LLDI0006,LLDI000A]
        tmp5 = linePoint1Y * linePoint2Z;

        //Sub-expression: LLDI0070 = Plus[LLDI006E,LLDI006F]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI0071 = Times[-1,LLDI0011]
        tmp5 = -triangle.Point3X;

        //Sub-expression: LLDI0072 = Plus[LLDI000E,LLDI0071]
        tmp5 = triangle.Point2X + tmp5;

        //Sub-expression: LLDI0073 = Times[LLDI0070,LLDI0072]
        tmp5 = tmp4 * tmp5;

        //Sub-expression: LLDI0074 = Plus[LLDI006D,LLDI0073]
        tmp1 = tmp1 + tmp5;

        //Sub-expression: LLDI0075 = Times[-1,LLDI0007,LLDI0008]
        tmp5 = -1 * linePoint1Z * linePoint2X;

        //Sub-expression: LLDI0076 = Times[LLDI0005,LLDI000A]
        tmp6 = linePoint1X * linePoint2Z;

        //Sub-expression: LLDI0077 = Plus[LLDI0075,LLDI0076]
        tmp5 = tmp5 + tmp6;

        //Sub-expression: LLDI0078 = Times[-1,LLDI0012]
        tmp6 = -triangle.Point3Y;

        //Sub-expression: LLDI0079 = Plus[LLDI000F,LLDI0078]
        tmp6 = triangle.Point2Y + tmp6;

        //Sub-expression: LLDI007A = Times[-1,LLDI0077,LLDI0079]
        tmp6 = -1 * tmp5 * tmp6;

        //Sub-expression: LLDI007B = Plus[LLDI0074,LLDI007A]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI007C = Times[-1,LLDI0006,LLDI0008]
        tmp6 = -1 * linePoint1Y * linePoint2X;

        //Sub-expression: LLDI007D = Times[LLDI0005,LLDI0009]
        tmp7 = linePoint1X * linePoint2Y;

        //Sub-expression: LLDI007E = Plus[LLDI007C,LLDI007D]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI007F = Times[-1,LLDI0013]
        tmp7 = -triangle.Point3Z;

        //Sub-expression: LLDI0080 = Plus[LLDI0010,LLDI007F]
        tmp7 = triangle.Point2Z + tmp7;

        //Sub-expression: LLDI0081 = Times[LLDI007E,LLDI0080]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0001 = Plus[LLDI007B,LLDI0081]
        var d1 = tmp1 + tmp7;

        //Sub-expression: LLDI0082 = Times[LLDI000C,LLDI0011]
        tmp1 = triangle.Point1Y * triangle.Point3X;

        //Sub-expression: LLDI0083 = Times[-1,LLDI000B,LLDI0012]
        tmp7 = -1 * triangle.Point1X * triangle.Point3Y;

        //Sub-expression: LLDI0084 = Plus[LLDI0082,LLDI0083]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0085 = Times[LLDI005B,LLDI0084]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0086 = Times[LLDI000D,LLDI0011]
        tmp7 = triangle.Point1Z * triangle.Point3X;

        //Sub-expression: LLDI0087 = Times[-1,LLDI000B,LLDI0013]
        tmp8 = -1 * triangle.Point1X * triangle.Point3Z;

        //Sub-expression: LLDI0088 = Plus[LLDI0086,LLDI0087]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI0089 = Times[-1,LLDI0061,LLDI0088]
        tmp7 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI008A = Plus[LLDI0085,LLDI0089]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI008B = Times[LLDI000D,LLDI0012]
        tmp7 = triangle.Point1Z * triangle.Point3Y;

        //Sub-expression: LLDI008C = Times[-1,LLDI000C,LLDI0013]
        tmp8 = -1 * triangle.Point1Y * triangle.Point3Z;

        //Sub-expression: LLDI008D = Plus[LLDI008B,LLDI008C]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI008E = Times[LLDI0068,LLDI008D]
        tmp7 = tmp3 * tmp7;

        //Sub-expression: LLDI008F = Plus[LLDI008A,LLDI008E]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0090 = Times[-1,LLDI000B]
        tmp7 = -triangle.Point1X;

        //Sub-expression: LLDI0091 = Plus[LLDI0090,LLDI0011]
        tmp7 = tmp7 + triangle.Point3X;

        //Sub-expression: LLDI0092 = Times[LLDI0070,LLDI0091]
        tmp7 = tmp4 * tmp7;

        //Sub-expression: LLDI0093 = Plus[LLDI008F,LLDI0092]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0094 = Times[-1,LLDI000C]
        tmp7 = -triangle.Point1Y;

        //Sub-expression: LLDI0095 = Plus[LLDI0094,LLDI0012]
        tmp7 = tmp7 + triangle.Point3Y;

        //Sub-expression: LLDI0096 = Times[-1,LLDI0077,LLDI0095]
        tmp7 = -1 * tmp5 * tmp7;

        //Sub-expression: LLDI0097 = Plus[LLDI0093,LLDI0096]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0098 = Times[-1,LLDI000D]
        tmp7 = -triangle.Point1Z;

        //Sub-expression: LLDI0099 = Plus[LLDI0098,LLDI0013]
        tmp7 = tmp7 + triangle.Point3Z;

        //Sub-expression: LLDI009A = Times[LLDI007E,LLDI0099]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0002 = Plus[LLDI0097,LLDI009A]
        var d2 = tmp1 + tmp7;

        //Sub-expression: LLDI009B = Times[-1,LLDI000C,LLDI000E]
        tmp1 = -1 * triangle.Point1Y * triangle.Point2X;

        //Sub-expression: LLDI009C = Times[LLDI000B,LLDI000F]
        tmp7 = triangle.Point1X * triangle.Point2Y;

        //Sub-expression: LLDI009D = Plus[LLDI009B,LLDI009C]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI009E = Times[LLDI005B,LLDI009D]
        tmp0 = tmp0 * tmp1;

        //Sub-expression: LLDI009F = Times[-1,LLDI000D,LLDI000E]
        tmp7 = -1 * triangle.Point1Z * triangle.Point2X;

        //Sub-expression: LLDI00A0 = Times[LLDI000B,LLDI0010]
        tmp8 = triangle.Point1X * triangle.Point2Z;

        //Sub-expression: LLDI00A1 = Plus[LLDI009F,LLDI00A0]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI00A2 = Times[-1,LLDI0061,LLDI00A1]
        tmp2 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI00A3 = Plus[LLDI009E,LLDI00A2]
        tmp0 = tmp0 + tmp2;

        //Sub-expression: LLDI00A4 = Times[-1,LLDI000D,LLDI000F]
        tmp2 = -1 * triangle.Point1Z * triangle.Point2Y;

        //Sub-expression: LLDI00A5 = Times[LLDI000C,LLDI0010]
        tmp8 = triangle.Point1Y * triangle.Point2Z;

        //Sub-expression: LLDI00A6 = Plus[LLDI00A4,LLDI00A5]
        tmp2 = tmp2 + tmp8;

        //Sub-expression: LLDI00A7 = Times[LLDI0068,LLDI00A6]
        tmp3 = tmp3 * tmp2;

        //Sub-expression: LLDI00A8 = Plus[LLDI00A3,LLDI00A7]
        tmp0 = tmp0 + tmp3;

        //Sub-expression: LLDI00A9 = Times[-1,LLDI000E]
        tmp3 = -triangle.Point2X;

        //Sub-expression: LLDI00AA = Plus[LLDI000B,LLDI00A9]
        tmp3 = triangle.Point1X + tmp3;

        //Sub-expression: LLDI00AB = Times[LLDI0070,LLDI00AA]
        tmp4 = tmp4 * tmp3;

        //Sub-expression: LLDI00AC = Plus[LLDI00A8,LLDI00AB]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00AD = Times[-1,LLDI000F]
        tmp4 = -triangle.Point2Y;

        //Sub-expression: LLDI00AE = Plus[LLDI000C,LLDI00AD]
        tmp4 = triangle.Point1Y + tmp4;

        //Sub-expression: LLDI00AF = Times[-1,LLDI0077,LLDI00AE]
        tmp5 = -1 * tmp5 * tmp4;

        //Sub-expression: LLDI00B0 = Plus[LLDI00AC,LLDI00AF]
        tmp0 = tmp0 + tmp5;

        //Sub-expression: LLDI00B1 = Times[-1,LLDI0010]
        tmp5 = -triangle.Point2Z;

        //Sub-expression: LLDI00B2 = Plus[LLDI000D,LLDI00B1]
        tmp5 = triangle.Point1Z + tmp5;

        //Sub-expression: LLDI00B3 = Times[LLDI007E,LLDI00B2]
        tmp6 = tmp6 * tmp5;

        //Output: LLDI0003 = Plus[LLDI00B0,LLDI00B3]
        var d3 = tmp0 + tmp6;

        if (!IsLineInsideTriangle(d1, d2, d3))
        {
            TestTriangleIntersectionCounter.EndWithFalseOutcome();

            return false;
        }

        //Sub-expression: LLDI00B4 = Times[LLDI0013,LLDI009D]
        tmp0 = triangle.Point3Z * tmp1;

        //Sub-expression: LLDI00B5 = Times[-1,LLDI0012,LLDI00A1]
        tmp6 = -1 * triangle.Point3Y * tmp7;

        //Sub-expression: LLDI00B6 = Plus[LLDI00B4,LLDI00B5]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B7 = Times[LLDI0011,LLDI00A6]
        tmp6 = triangle.Point3X * tmp2;

        //Sub-expression: LLDI00B8 = Plus[LLDI00B6,LLDI00B7]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B9 = Times[-1,LLDI0012,LLDI00AA]
        tmp6 = -1 * triangle.Point3Y * tmp3;

        //Sub-expression: LLDI00BA = Plus[LLDI009D,LLDI00B9]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BB = Times[LLDI0011,LLDI00AE]
        tmp6 = triangle.Point3X * tmp4;

        //Sub-expression: LLDI00BC = Plus[LLDI00BA,LLDI00BB]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BD = Times[-1,LLDI0007,LLDI00BC]
        tmp6 = -1 * linePoint1Z * tmp1;

        //Sub-expression: LLDI00BE = Plus[LLDI00B8,LLDI00BD]
        tmp6 = tmp0 + tmp6;

        //Sub-expression: LLDI00BF = Times[-1,LLDI0013,LLDI00AA]
        tmp3 = -1 * triangle.Point3Z * tmp3;

        //Sub-expression: LLDI00C0 = Plus[LLDI00A1,LLDI00BF]
        tmp3 = tmp7 + tmp3;

        //Sub-expression: LLDI00C1 = Times[LLDI0011,LLDI00B2]
        tmp7 = triangle.Point3X * tmp5;

        //Sub-expression: LLDI00C2 = Plus[LLDI00C0,LLDI00C1]
        tmp3 = tmp3 + tmp7;

        //Sub-expression: LLDI00C3 = Times[LLDI0006,LLDI00C2]
        tmp7 = linePoint1Y * tmp3;

        //Sub-expression: LLDI00C4 = Plus[LLDI00BE,LLDI00C3]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI00C5 = Times[-1,LLDI0013,LLDI00AE]
        tmp4 = -1 * triangle.Point3Z * tmp4;

        //Sub-expression: LLDI00C6 = Plus[LLDI00A6,LLDI00C5]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C7 = Times[LLDI0012,LLDI00B2]
        tmp4 = triangle.Point3Y * tmp5;

        //Sub-expression: LLDI00C8 = Plus[LLDI00C6,LLDI00C7]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C9 = Times[-1,LLDI0005,LLDI00C8]
        tmp4 = -1 * linePoint1X * tmp2;

        //Sub-expression: LLDI00CA = Plus[LLDI00C4,LLDI00C9]
        tmp4 = tmp6 + tmp4;

        //Sub-expression: LLDI00CB = Times[-1,LLDI00B8]
        tmp0 = -tmp0;

        //Sub-expression: LLDI00CC = Times[LLDI000A,LLDI00BC]
        tmp1 = linePoint2Z * tmp1;

        //Sub-expression: LLDI00CD = Plus[LLDI00CB,LLDI00CC]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00CE = Times[-1,LLDI0009,LLDI00C2]
        tmp1 = -1 * linePoint2Y * tmp3;

        //Sub-expression: LLDI00CF = Plus[LLDI00CD,LLDI00CE]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D0 = Times[LLDI0008,LLDI00C8]
        tmp1 = linePoint2X * tmp2;

        //Sub-expression: LLDI00D1 = Plus[LLDI00CF,LLDI00D0]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D2 = Plus[LLDI00D1,LLDI00CA]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00D3 = Power[LLDI00D2,-1]
        tmp0 = 1 / tmp0;

        //Output: LLDI0004 = Times[LLDI00CA,LLDI00D3]
        var t = tmp4 * tmp0;


        //Finish GMac Macro Code Generation, 2018-10-05T22:07:42.3247418+02:00

        Debug.Assert(!double.IsNaN(t));

        var result = t >= 0 && t <= 1;

        TestTriangleIntersectionCounter.End(result);

        return result;
    }

    public Tuple<bool, double> ComputeIntersectionVa(IFloat64Triangle3D triangle)
    {
        if (!triangle.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        ComputeTriangleIntersectionCounter.Begin();

        var d1 = Line.GetSignedDistanceToLineVa(triangle.GetLine23());
        var d2 = Line.GetSignedDistanceToLineVa(triangle.GetLine31());
        var d3 = Line.GetSignedDistanceToLineVa(triangle.GetLine12());

        if (!IsLineInsideTriangle(d1, d2, d3))
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        var s1 = Line
            .GetPointAt(LineParameterLimits.MinValue)
            .GetSignedDistanceToPlaneVa(triangle);

        var s2 = Line
            .GetPointAt(LineParameterLimits.MaxValue)
            .GetSignedDistanceToPlaneVa(triangle);

        if (!(s1 < 0 && s2 > 0) && !(s1 > 0 && s2 < 0))
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        var t = s1 / (s1 - s2);

        Debug.Assert(!double.IsNaN(t));

        ComputeTriangleIntersectionCounter.EndWithTrueOutcome();

        return Tuple.Create(true, t);
    }

    public Tuple<bool, double> ComputeIntersection(IFloat64Triangle3D triangle)
    {
        if (!triangle.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        ComputeTriangleIntersectionCounter.Begin();

        var linePoint1X = Line.OriginX + LineParameterLimits.MinValue * Line.DirectionX;
        var linePoint1Y = Line.OriginY + LineParameterLimits.MinValue * Line.DirectionY;
        var linePoint1Z = Line.OriginZ + LineParameterLimits.MinValue * Line.DirectionZ;

        var linePoint2X = Line.OriginX + LineParameterLimits.MaxValue * Line.DirectionX;
        var linePoint2Y = Line.OriginY + LineParameterLimits.MaxValue * Line.DirectionY;
        var linePoint2Z = Line.OriginZ + LineParameterLimits.MaxValue * Line.DirectionZ;


        //Begin GMac Macro Code Generation, 2018-10-05T22:07:42.3237425+02:00
        //Macro: cemsim.hga4d.LinePlaneIntersect3D
        //Input Variables: 15 used, 0 not used, 15 total.
        //Temp Variables: 122 sub-expressions, 0 generated temps, 122 total.
        //Target Temp Variables: 9 total.
        //Output Variables: 4 total.
        //Computations: 1.1984126984127 average, 151 total.
        //Memory Reads: 1.88888888888889 average, 238 total.
        //Memory Writes: 126 total.
        //
        //Macro Binding Data: 
        //   result.d1 = variable: var d1
        //   result.d2 = variable: var d2
        //   result.d3 = variable: var d3
        //   result.t = variable: var t
        //   linePoint1.#e1# = variable: linePoint1X
        //   linePoint1.#e2# = variable: linePoint1Y
        //   linePoint1.#e3# = variable: linePoint1Z
        //   linePoint2.#e1# = variable: linePoint2X
        //   linePoint2.#e2# = variable: linePoint2Y
        //   linePoint2.#e3# = variable: linePoint2Z
        //   planePoint1.#e1# = variable: triangle.Point1X
        //   planePoint1.#e2# = variable: triangle.Point1Y
        //   planePoint1.#e3# = variable: triangle.Point1Z
        //   planePoint2.#e1# = variable: triangle.Point2X
        //   planePoint2.#e2# = variable: triangle.Point2Y
        //   planePoint2.#e3# = variable: triangle.Point2Z
        //   planePoint3.#e1# = variable: triangle.Point3X
        //   planePoint3.#e2# = variable: triangle.Point3Y
        //   planePoint3.#e3# = variable: triangle.Point3Z

        double tmp0;
        double tmp1;
        double tmp2;
        double tmp3;
        double tmp4;
        double tmp5;
        double tmp6;
        double tmp7;
        double tmp8;

        //Sub-expression: LLDI005A = Times[-1,LLDI000A]
        tmp0 = -linePoint2Z;

        //Sub-expression: LLDI005B = Plus[LLDI0007,LLDI005A]
        tmp0 = linePoint1Z + tmp0;

        //Sub-expression: LLDI005C = Times[-1,LLDI000F,LLDI0011]
        tmp1 = -1 * triangle.Point2Y * triangle.Point3X;

        //Sub-expression: LLDI005D = Times[LLDI000E,LLDI0012]
        tmp2 = triangle.Point2X * triangle.Point3Y;

        //Sub-expression: LLDI005E = Plus[LLDI005C,LLDI005D]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI005F = Times[LLDI005B,LLDI005E]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0060 = Times[-1,LLDI0009]
        tmp2 = -linePoint2Y;

        //Sub-expression: LLDI0061 = Plus[LLDI0006,LLDI0060]
        tmp2 = linePoint1Y + tmp2;

        //Sub-expression: LLDI0062 = Times[-1,LLDI0010,LLDI0011]
        tmp3 = -1 * triangle.Point2Z * triangle.Point3X;

        //Sub-expression: LLDI0063 = Times[LLDI000E,LLDI0013]
        tmp4 = triangle.Point2X * triangle.Point3Z;

        //Sub-expression: LLDI0064 = Plus[LLDI0062,LLDI0063]
        tmp3 = tmp3 + tmp4;

        //Sub-expression: LLDI0065 = Times[-1,LLDI0061,LLDI0064]
        tmp3 = -1 * tmp2 * tmp3;

        //Sub-expression: LLDI0066 = Plus[LLDI005F,LLDI0065]
        tmp1 = tmp1 + tmp3;

        //Sub-expression: LLDI0067 = Times[-1,LLDI0008]
        tmp3 = -linePoint2X;

        //Sub-expression: LLDI0068 = Plus[LLDI0005,LLDI0067]
        tmp3 = linePoint1X + tmp3;

        //Sub-expression: LLDI0069 = Times[-1,LLDI0010,LLDI0012]
        tmp4 = -1 * triangle.Point2Z * triangle.Point3Y;

        //Sub-expression: LLDI006A = Times[LLDI000F,LLDI0013]
        tmp5 = triangle.Point2Y * triangle.Point3Z;

        //Sub-expression: LLDI006B = Plus[LLDI0069,LLDI006A]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI006C = Times[LLDI0068,LLDI006B]
        tmp4 = tmp3 * tmp4;

        //Sub-expression: LLDI006D = Plus[LLDI0066,LLDI006C]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI006E = Times[-1,LLDI0007,LLDI0009]
        tmp4 = -1 * linePoint1Z * linePoint2Y;

        //Sub-expression: LLDI006F = Times[LLDI0006,LLDI000A]
        tmp5 = linePoint1Y * linePoint2Z;

        //Sub-expression: LLDI0070 = Plus[LLDI006E,LLDI006F]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI0071 = Times[-1,LLDI0011]
        tmp5 = -triangle.Point3X;

        //Sub-expression: LLDI0072 = Plus[LLDI000E,LLDI0071]
        tmp5 = triangle.Point2X + tmp5;

        //Sub-expression: LLDI0073 = Times[LLDI0070,LLDI0072]
        tmp5 = tmp4 * tmp5;

        //Sub-expression: LLDI0074 = Plus[LLDI006D,LLDI0073]
        tmp1 = tmp1 + tmp5;

        //Sub-expression: LLDI0075 = Times[-1,LLDI0007,LLDI0008]
        tmp5 = -1 * linePoint1Z * linePoint2X;

        //Sub-expression: LLDI0076 = Times[LLDI0005,LLDI000A]
        tmp6 = linePoint1X * linePoint2Z;

        //Sub-expression: LLDI0077 = Plus[LLDI0075,LLDI0076]
        tmp5 = tmp5 + tmp6;

        //Sub-expression: LLDI0078 = Times[-1,LLDI0012]
        tmp6 = -triangle.Point3Y;

        //Sub-expression: LLDI0079 = Plus[LLDI000F,LLDI0078]
        tmp6 = triangle.Point2Y + tmp6;

        //Sub-expression: LLDI007A = Times[-1,LLDI0077,LLDI0079]
        tmp6 = -1 * tmp5 * tmp6;

        //Sub-expression: LLDI007B = Plus[LLDI0074,LLDI007A]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI007C = Times[-1,LLDI0006,LLDI0008]
        tmp6 = -1 * linePoint1Y * linePoint2X;

        //Sub-expression: LLDI007D = Times[LLDI0005,LLDI0009]
        tmp7 = linePoint1X * linePoint2Y;

        //Sub-expression: LLDI007E = Plus[LLDI007C,LLDI007D]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI007F = Times[-1,LLDI0013]
        tmp7 = -triangle.Point3Z;

        //Sub-expression: LLDI0080 = Plus[LLDI0010,LLDI007F]
        tmp7 = triangle.Point2Z + tmp7;

        //Sub-expression: LLDI0081 = Times[LLDI007E,LLDI0080]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0001 = Plus[LLDI007B,LLDI0081]
        var d1 = tmp1 + tmp7;

        //Sub-expression: LLDI0082 = Times[LLDI000C,LLDI0011]
        tmp1 = triangle.Point1Y * triangle.Point3X;

        //Sub-expression: LLDI0083 = Times[-1,LLDI000B,LLDI0012]
        tmp7 = -1 * triangle.Point1X * triangle.Point3Y;

        //Sub-expression: LLDI0084 = Plus[LLDI0082,LLDI0083]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0085 = Times[LLDI005B,LLDI0084]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0086 = Times[LLDI000D,LLDI0011]
        tmp7 = triangle.Point1Z * triangle.Point3X;

        //Sub-expression: LLDI0087 = Times[-1,LLDI000B,LLDI0013]
        tmp8 = -1 * triangle.Point1X * triangle.Point3Z;

        //Sub-expression: LLDI0088 = Plus[LLDI0086,LLDI0087]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI0089 = Times[-1,LLDI0061,LLDI0088]
        tmp7 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI008A = Plus[LLDI0085,LLDI0089]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI008B = Times[LLDI000D,LLDI0012]
        tmp7 = triangle.Point1Z * triangle.Point3Y;

        //Sub-expression: LLDI008C = Times[-1,LLDI000C,LLDI0013]
        tmp8 = -1 * triangle.Point1Y * triangle.Point3Z;

        //Sub-expression: LLDI008D = Plus[LLDI008B,LLDI008C]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI008E = Times[LLDI0068,LLDI008D]
        tmp7 = tmp3 * tmp7;

        //Sub-expression: LLDI008F = Plus[LLDI008A,LLDI008E]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0090 = Times[-1,LLDI000B]
        tmp7 = -triangle.Point1X;

        //Sub-expression: LLDI0091 = Plus[LLDI0090,LLDI0011]
        tmp7 = tmp7 + triangle.Point3X;

        //Sub-expression: LLDI0092 = Times[LLDI0070,LLDI0091]
        tmp7 = tmp4 * tmp7;

        //Sub-expression: LLDI0093 = Plus[LLDI008F,LLDI0092]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0094 = Times[-1,LLDI000C]
        tmp7 = -triangle.Point1Y;

        //Sub-expression: LLDI0095 = Plus[LLDI0094,LLDI0012]
        tmp7 = tmp7 + triangle.Point3Y;

        //Sub-expression: LLDI0096 = Times[-1,LLDI0077,LLDI0095]
        tmp7 = -1 * tmp5 * tmp7;

        //Sub-expression: LLDI0097 = Plus[LLDI0093,LLDI0096]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0098 = Times[-1,LLDI000D]
        tmp7 = -triangle.Point1Z;

        //Sub-expression: LLDI0099 = Plus[LLDI0098,LLDI0013]
        tmp7 = tmp7 + triangle.Point3Z;

        //Sub-expression: LLDI009A = Times[LLDI007E,LLDI0099]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0002 = Plus[LLDI0097,LLDI009A]
        var d2 = tmp1 + tmp7;

        //Sub-expression: LLDI009B = Times[-1,LLDI000C,LLDI000E]
        tmp1 = -1 * triangle.Point1Y * triangle.Point2X;

        //Sub-expression: LLDI009C = Times[LLDI000B,LLDI000F]
        tmp7 = triangle.Point1X * triangle.Point2Y;

        //Sub-expression: LLDI009D = Plus[LLDI009B,LLDI009C]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI009E = Times[LLDI005B,LLDI009D]
        tmp0 = tmp0 * tmp1;

        //Sub-expression: LLDI009F = Times[-1,LLDI000D,LLDI000E]
        tmp7 = -1 * triangle.Point1Z * triangle.Point2X;

        //Sub-expression: LLDI00A0 = Times[LLDI000B,LLDI0010]
        tmp8 = triangle.Point1X * triangle.Point2Z;

        //Sub-expression: LLDI00A1 = Plus[LLDI009F,LLDI00A0]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI00A2 = Times[-1,LLDI0061,LLDI00A1]
        tmp2 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI00A3 = Plus[LLDI009E,LLDI00A2]
        tmp0 = tmp0 + tmp2;

        //Sub-expression: LLDI00A4 = Times[-1,LLDI000D,LLDI000F]
        tmp2 = -1 * triangle.Point1Z * triangle.Point2Y;

        //Sub-expression: LLDI00A5 = Times[LLDI000C,LLDI0010]
        tmp8 = triangle.Point1Y * triangle.Point2Z;

        //Sub-expression: LLDI00A6 = Plus[LLDI00A4,LLDI00A5]
        tmp2 = tmp2 + tmp8;

        //Sub-expression: LLDI00A7 = Times[LLDI0068,LLDI00A6]
        tmp3 = tmp3 * tmp2;

        //Sub-expression: LLDI00A8 = Plus[LLDI00A3,LLDI00A7]
        tmp0 = tmp0 + tmp3;

        //Sub-expression: LLDI00A9 = Times[-1,LLDI000E]
        tmp3 = -triangle.Point2X;

        //Sub-expression: LLDI00AA = Plus[LLDI000B,LLDI00A9]
        tmp3 = triangle.Point1X + tmp3;

        //Sub-expression: LLDI00AB = Times[LLDI0070,LLDI00AA]
        tmp4 = tmp4 * tmp3;

        //Sub-expression: LLDI00AC = Plus[LLDI00A8,LLDI00AB]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00AD = Times[-1,LLDI000F]
        tmp4 = -triangle.Point2Y;

        //Sub-expression: LLDI00AE = Plus[LLDI000C,LLDI00AD]
        tmp4 = triangle.Point1Y + tmp4;

        //Sub-expression: LLDI00AF = Times[-1,LLDI0077,LLDI00AE]
        tmp5 = -1 * tmp5 * tmp4;

        //Sub-expression: LLDI00B0 = Plus[LLDI00AC,LLDI00AF]
        tmp0 = tmp0 + tmp5;

        //Sub-expression: LLDI00B1 = Times[-1,LLDI0010]
        tmp5 = -triangle.Point2Z;

        //Sub-expression: LLDI00B2 = Plus[LLDI000D,LLDI00B1]
        tmp5 = triangle.Point1Z + tmp5;

        //Sub-expression: LLDI00B3 = Times[LLDI007E,LLDI00B2]
        tmp6 = tmp6 * tmp5;

        //Output: LLDI0003 = Plus[LLDI00B0,LLDI00B3]
        var d3 = tmp0 + tmp6;

        if (!IsLineInsideTriangle(d1, d2, d3))
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        //Sub-expression: LLDI00B4 = Times[LLDI0013,LLDI009D]
        tmp0 = triangle.Point3Z * tmp1;

        //Sub-expression: LLDI00B5 = Times[-1,LLDI0012,LLDI00A1]
        tmp6 = -1 * triangle.Point3Y * tmp7;

        //Sub-expression: LLDI00B6 = Plus[LLDI00B4,LLDI00B5]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B7 = Times[LLDI0011,LLDI00A6]
        tmp6 = triangle.Point3X * tmp2;

        //Sub-expression: LLDI00B8 = Plus[LLDI00B6,LLDI00B7]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B9 = Times[-1,LLDI0012,LLDI00AA]
        tmp6 = -1 * triangle.Point3Y * tmp3;

        //Sub-expression: LLDI00BA = Plus[LLDI009D,LLDI00B9]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BB = Times[LLDI0011,LLDI00AE]
        tmp6 = triangle.Point3X * tmp4;

        //Sub-expression: LLDI00BC = Plus[LLDI00BA,LLDI00BB]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BD = Times[-1,LLDI0007,LLDI00BC]
        tmp6 = -1 * linePoint1Z * tmp1;

        //Sub-expression: LLDI00BE = Plus[LLDI00B8,LLDI00BD]
        tmp6 = tmp0 + tmp6;

        //Sub-expression: LLDI00BF = Times[-1,LLDI0013,LLDI00AA]
        tmp3 = -1 * triangle.Point3Z * tmp3;

        //Sub-expression: LLDI00C0 = Plus[LLDI00A1,LLDI00BF]
        tmp3 = tmp7 + tmp3;

        //Sub-expression: LLDI00C1 = Times[LLDI0011,LLDI00B2]
        tmp7 = triangle.Point3X * tmp5;

        //Sub-expression: LLDI00C2 = Plus[LLDI00C0,LLDI00C1]
        tmp3 = tmp3 + tmp7;

        //Sub-expression: LLDI00C3 = Times[LLDI0006,LLDI00C2]
        tmp7 = linePoint1Y * tmp3;

        //Sub-expression: LLDI00C4 = Plus[LLDI00BE,LLDI00C3]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI00C5 = Times[-1,LLDI0013,LLDI00AE]
        tmp4 = -1 * triangle.Point3Z * tmp4;

        //Sub-expression: LLDI00C6 = Plus[LLDI00A6,LLDI00C5]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C7 = Times[LLDI0012,LLDI00B2]
        tmp4 = triangle.Point3Y * tmp5;

        //Sub-expression: LLDI00C8 = Plus[LLDI00C6,LLDI00C7]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C9 = Times[-1,LLDI0005,LLDI00C8]
        tmp4 = -1 * linePoint1X * tmp2;

        //Sub-expression: LLDI00CA = Plus[LLDI00C4,LLDI00C9]
        tmp4 = tmp6 + tmp4;

        //Sub-expression: LLDI00CB = Times[-1,LLDI00B8]
        tmp0 = -tmp0;

        //Sub-expression: LLDI00CC = Times[LLDI000A,LLDI00BC]
        tmp1 = linePoint2Z * tmp1;

        //Sub-expression: LLDI00CD = Plus[LLDI00CB,LLDI00CC]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00CE = Times[-1,LLDI0009,LLDI00C2]
        tmp1 = -1 * linePoint2Y * tmp3;

        //Sub-expression: LLDI00CF = Plus[LLDI00CD,LLDI00CE]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D0 = Times[LLDI0008,LLDI00C8]
        tmp1 = linePoint2X * tmp2;

        //Sub-expression: LLDI00D1 = Plus[LLDI00CF,LLDI00D0]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D2 = Plus[LLDI00D1,LLDI00CA]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00D3 = Power[LLDI00D2,-1]
        tmp0 = 1 / tmp0;

        //Output: LLDI0004 = Times[LLDI00CA,LLDI00D3]
        var t = tmp4 * tmp0;


        //Finish GMac Macro Code Generation, 2018-10-05T22:07:42.3247418+02:00

        if (t is < 0 or > 1)
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        //Correction to get line parameter t w.r.t. line origin and direction
        //because t is w.r.t. two end points given by lineParamRange
        t = (1 - t) * LineParameterLimits.MinValue + t * LineParameterLimits.MaxValue;

        Debug.Assert(!double.IsNaN(t));

        ComputeTriangleIntersectionCounter.EndWithTrueOutcome();

        return new Tuple<bool, double>(true, t);
    }

    public Tuple<bool, double> ComputeIntersection(IFloat64Triangle3D triangle, double lineParamMinValue, double lineParamMaxValue)
    {
        if (!triangle.IntersectionTestsEnabled)
            return IntersectionUtils.NoIntersection;

        ComputeTriangleIntersectionCounter.Begin();

        var linePoint1X = Line.OriginX + lineParamMinValue * Line.DirectionX;
        var linePoint1Y = Line.OriginY + lineParamMinValue * Line.DirectionY;
        var linePoint1Z = Line.OriginZ + lineParamMinValue * Line.DirectionZ;

        var linePoint2X = Line.OriginX + lineParamMaxValue * Line.DirectionX;
        var linePoint2Y = Line.OriginY + lineParamMaxValue * Line.DirectionY;
        var linePoint2Z = Line.OriginZ + lineParamMaxValue * Line.DirectionZ;


        //Begin GMac Macro Code Generation, 2018-10-05T22:07:42.3237425+02:00
        //Macro: cemsim.hga4d.LinePlaneIntersect3D
        //Input Variables: 15 used, 0 not used, 15 total.
        //Temp Variables: 122 sub-expressions, 0 generated temps, 122 total.
        //Target Temp Variables: 9 total.
        //Output Variables: 4 total.
        //Computations: 1.1984126984127 average, 151 total.
        //Memory Reads: 1.88888888888889 average, 238 total.
        //Memory Writes: 126 total.
        //
        //Macro Binding Data: 
        //   result.d1 = variable: var d1
        //   result.d2 = variable: var d2
        //   result.d3 = variable: var d3
        //   result.t = variable: var t
        //   linePoint1.#e1# = variable: linePoint1X
        //   linePoint1.#e2# = variable: linePoint1Y
        //   linePoint1.#e3# = variable: linePoint1Z
        //   linePoint2.#e1# = variable: linePoint2X
        //   linePoint2.#e2# = variable: linePoint2Y
        //   linePoint2.#e3# = variable: linePoint2Z
        //   planePoint1.#e1# = variable: triangle.Point1X
        //   planePoint1.#e2# = variable: triangle.Point1Y
        //   planePoint1.#e3# = variable: triangle.Point1Z
        //   planePoint2.#e1# = variable: triangle.Point2X
        //   planePoint2.#e2# = variable: triangle.Point2Y
        //   planePoint2.#e3# = variable: triangle.Point2Z
        //   planePoint3.#e1# = variable: triangle.Point3X
        //   planePoint3.#e2# = variable: triangle.Point3Y
        //   planePoint3.#e3# = variable: triangle.Point3Z

        double tmp0;
        double tmp1;
        double tmp2;
        double tmp3;
        double tmp4;
        double tmp5;
        double tmp6;
        double tmp7;
        double tmp8;

        //Sub-expression: LLDI005A = Times[-1,LLDI000A]
        tmp0 = -linePoint2Z;

        //Sub-expression: LLDI005B = Plus[LLDI0007,LLDI005A]
        tmp0 = linePoint1Z + tmp0;

        //Sub-expression: LLDI005C = Times[-1,LLDI000F,LLDI0011]
        tmp1 = -1 * triangle.Point2Y * triangle.Point3X;

        //Sub-expression: LLDI005D = Times[LLDI000E,LLDI0012]
        tmp2 = triangle.Point2X * triangle.Point3Y;

        //Sub-expression: LLDI005E = Plus[LLDI005C,LLDI005D]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI005F = Times[LLDI005B,LLDI005E]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0060 = Times[-1,LLDI0009]
        tmp2 = -linePoint2Y;

        //Sub-expression: LLDI0061 = Plus[LLDI0006,LLDI0060]
        tmp2 = linePoint1Y + tmp2;

        //Sub-expression: LLDI0062 = Times[-1,LLDI0010,LLDI0011]
        tmp3 = -1 * triangle.Point2Z * triangle.Point3X;

        //Sub-expression: LLDI0063 = Times[LLDI000E,LLDI0013]
        tmp4 = triangle.Point2X * triangle.Point3Z;

        //Sub-expression: LLDI0064 = Plus[LLDI0062,LLDI0063]
        tmp3 = tmp3 + tmp4;

        //Sub-expression: LLDI0065 = Times[-1,LLDI0061,LLDI0064]
        tmp3 = -1 * tmp2 * tmp3;

        //Sub-expression: LLDI0066 = Plus[LLDI005F,LLDI0065]
        tmp1 = tmp1 + tmp3;

        //Sub-expression: LLDI0067 = Times[-1,LLDI0008]
        tmp3 = -linePoint2X;

        //Sub-expression: LLDI0068 = Plus[LLDI0005,LLDI0067]
        tmp3 = linePoint1X + tmp3;

        //Sub-expression: LLDI0069 = Times[-1,LLDI0010,LLDI0012]
        tmp4 = -1 * triangle.Point2Z * triangle.Point3Y;

        //Sub-expression: LLDI006A = Times[LLDI000F,LLDI0013]
        tmp5 = triangle.Point2Y * triangle.Point3Z;

        //Sub-expression: LLDI006B = Plus[LLDI0069,LLDI006A]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI006C = Times[LLDI0068,LLDI006B]
        tmp4 = tmp3 * tmp4;

        //Sub-expression: LLDI006D = Plus[LLDI0066,LLDI006C]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI006E = Times[-1,LLDI0007,LLDI0009]
        tmp4 = -1 * linePoint1Z * linePoint2Y;

        //Sub-expression: LLDI006F = Times[LLDI0006,LLDI000A]
        tmp5 = linePoint1Y * linePoint2Z;

        //Sub-expression: LLDI0070 = Plus[LLDI006E,LLDI006F]
        tmp4 = tmp4 + tmp5;

        //Sub-expression: LLDI0071 = Times[-1,LLDI0011]
        tmp5 = -triangle.Point3X;

        //Sub-expression: LLDI0072 = Plus[LLDI000E,LLDI0071]
        tmp5 = triangle.Point2X + tmp5;

        //Sub-expression: LLDI0073 = Times[LLDI0070,LLDI0072]
        tmp5 = tmp4 * tmp5;

        //Sub-expression: LLDI0074 = Plus[LLDI006D,LLDI0073]
        tmp1 = tmp1 + tmp5;

        //Sub-expression: LLDI0075 = Times[-1,LLDI0007,LLDI0008]
        tmp5 = -1 * linePoint1Z * linePoint2X;

        //Sub-expression: LLDI0076 = Times[LLDI0005,LLDI000A]
        tmp6 = linePoint1X * linePoint2Z;

        //Sub-expression: LLDI0077 = Plus[LLDI0075,LLDI0076]
        tmp5 = tmp5 + tmp6;

        //Sub-expression: LLDI0078 = Times[-1,LLDI0012]
        tmp6 = -triangle.Point3Y;

        //Sub-expression: LLDI0079 = Plus[LLDI000F,LLDI0078]
        tmp6 = triangle.Point2Y + tmp6;

        //Sub-expression: LLDI007A = Times[-1,LLDI0077,LLDI0079]
        tmp6 = -1 * tmp5 * tmp6;

        //Sub-expression: LLDI007B = Plus[LLDI0074,LLDI007A]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI007C = Times[-1,LLDI0006,LLDI0008]
        tmp6 = -1 * linePoint1Y * linePoint2X;

        //Sub-expression: LLDI007D = Times[LLDI0005,LLDI0009]
        tmp7 = linePoint1X * linePoint2Y;

        //Sub-expression: LLDI007E = Plus[LLDI007C,LLDI007D]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI007F = Times[-1,LLDI0013]
        tmp7 = -triangle.Point3Z;

        //Sub-expression: LLDI0080 = Plus[LLDI0010,LLDI007F]
        tmp7 = triangle.Point2Z + tmp7;

        //Sub-expression: LLDI0081 = Times[LLDI007E,LLDI0080]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0001 = Plus[LLDI007B,LLDI0081]
        var d1 = tmp1 + tmp7;

        //Sub-expression: LLDI0082 = Times[LLDI000C,LLDI0011]
        tmp1 = triangle.Point1Y * triangle.Point3X;

        //Sub-expression: LLDI0083 = Times[-1,LLDI000B,LLDI0012]
        tmp7 = -1 * triangle.Point1X * triangle.Point3Y;

        //Sub-expression: LLDI0084 = Plus[LLDI0082,LLDI0083]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0085 = Times[LLDI005B,LLDI0084]
        tmp1 = tmp0 * tmp1;

        //Sub-expression: LLDI0086 = Times[LLDI000D,LLDI0011]
        tmp7 = triangle.Point1Z * triangle.Point3X;

        //Sub-expression: LLDI0087 = Times[-1,LLDI000B,LLDI0013]
        tmp8 = -1 * triangle.Point1X * triangle.Point3Z;

        //Sub-expression: LLDI0088 = Plus[LLDI0086,LLDI0087]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI0089 = Times[-1,LLDI0061,LLDI0088]
        tmp7 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI008A = Plus[LLDI0085,LLDI0089]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI008B = Times[LLDI000D,LLDI0012]
        tmp7 = triangle.Point1Z * triangle.Point3Y;

        //Sub-expression: LLDI008C = Times[-1,LLDI000C,LLDI0013]
        tmp8 = -1 * triangle.Point1Y * triangle.Point3Z;

        //Sub-expression: LLDI008D = Plus[LLDI008B,LLDI008C]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI008E = Times[LLDI0068,LLDI008D]
        tmp7 = tmp3 * tmp7;

        //Sub-expression: LLDI008F = Plus[LLDI008A,LLDI008E]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0090 = Times[-1,LLDI000B]
        tmp7 = -triangle.Point1X;

        //Sub-expression: LLDI0091 = Plus[LLDI0090,LLDI0011]
        tmp7 = tmp7 + triangle.Point3X;

        //Sub-expression: LLDI0092 = Times[LLDI0070,LLDI0091]
        tmp7 = tmp4 * tmp7;

        //Sub-expression: LLDI0093 = Plus[LLDI008F,LLDI0092]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0094 = Times[-1,LLDI000C]
        tmp7 = -triangle.Point1Y;

        //Sub-expression: LLDI0095 = Plus[LLDI0094,LLDI0012]
        tmp7 = tmp7 + triangle.Point3Y;

        //Sub-expression: LLDI0096 = Times[-1,LLDI0077,LLDI0095]
        tmp7 = -1 * tmp5 * tmp7;

        //Sub-expression: LLDI0097 = Plus[LLDI0093,LLDI0096]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI0098 = Times[-1,LLDI000D]
        tmp7 = -triangle.Point1Z;

        //Sub-expression: LLDI0099 = Plus[LLDI0098,LLDI0013]
        tmp7 = tmp7 + triangle.Point3Z;

        //Sub-expression: LLDI009A = Times[LLDI007E,LLDI0099]
        tmp7 = tmp6 * tmp7;

        //Output: LLDI0002 = Plus[LLDI0097,LLDI009A]
        var d2 = tmp1 + tmp7;

        //Sub-expression: LLDI009B = Times[-1,LLDI000C,LLDI000E]
        tmp1 = -1 * triangle.Point1Y * triangle.Point2X;

        //Sub-expression: LLDI009C = Times[LLDI000B,LLDI000F]
        tmp7 = triangle.Point1X * triangle.Point2Y;

        //Sub-expression: LLDI009D = Plus[LLDI009B,LLDI009C]
        tmp1 = tmp1 + tmp7;

        //Sub-expression: LLDI009E = Times[LLDI005B,LLDI009D]
        tmp0 = tmp0 * tmp1;

        //Sub-expression: LLDI009F = Times[-1,LLDI000D,LLDI000E]
        tmp7 = -1 * triangle.Point1Z * triangle.Point2X;

        //Sub-expression: LLDI00A0 = Times[LLDI000B,LLDI0010]
        tmp8 = triangle.Point1X * triangle.Point2Z;

        //Sub-expression: LLDI00A1 = Plus[LLDI009F,LLDI00A0]
        tmp7 = tmp7 + tmp8;

        //Sub-expression: LLDI00A2 = Times[-1,LLDI0061,LLDI00A1]
        tmp2 = -1 * tmp2 * tmp7;

        //Sub-expression: LLDI00A3 = Plus[LLDI009E,LLDI00A2]
        tmp0 = tmp0 + tmp2;

        //Sub-expression: LLDI00A4 = Times[-1,LLDI000D,LLDI000F]
        tmp2 = -1 * triangle.Point1Z * triangle.Point2Y;

        //Sub-expression: LLDI00A5 = Times[LLDI000C,LLDI0010]
        tmp8 = triangle.Point1Y * triangle.Point2Z;

        //Sub-expression: LLDI00A6 = Plus[LLDI00A4,LLDI00A5]
        tmp2 = tmp2 + tmp8;

        //Sub-expression: LLDI00A7 = Times[LLDI0068,LLDI00A6]
        tmp3 = tmp3 * tmp2;

        //Sub-expression: LLDI00A8 = Plus[LLDI00A3,LLDI00A7]
        tmp0 = tmp0 + tmp3;

        //Sub-expression: LLDI00A9 = Times[-1,LLDI000E]
        tmp3 = -triangle.Point2X;

        //Sub-expression: LLDI00AA = Plus[LLDI000B,LLDI00A9]
        tmp3 = triangle.Point1X + tmp3;

        //Sub-expression: LLDI00AB = Times[LLDI0070,LLDI00AA]
        tmp4 = tmp4 * tmp3;

        //Sub-expression: LLDI00AC = Plus[LLDI00A8,LLDI00AB]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00AD = Times[-1,LLDI000F]
        tmp4 = -triangle.Point2Y;

        //Sub-expression: LLDI00AE = Plus[LLDI000C,LLDI00AD]
        tmp4 = triangle.Point1Y + tmp4;

        //Sub-expression: LLDI00AF = Times[-1,LLDI0077,LLDI00AE]
        tmp5 = -1 * tmp5 * tmp4;

        //Sub-expression: LLDI00B0 = Plus[LLDI00AC,LLDI00AF]
        tmp0 = tmp0 + tmp5;

        //Sub-expression: LLDI00B1 = Times[-1,LLDI0010]
        tmp5 = -triangle.Point2Z;

        //Sub-expression: LLDI00B2 = Plus[LLDI000D,LLDI00B1]
        tmp5 = triangle.Point1Z + tmp5;

        //Sub-expression: LLDI00B3 = Times[LLDI007E,LLDI00B2]
        tmp6 = tmp6 * tmp5;

        //Output: LLDI0003 = Plus[LLDI00B0,LLDI00B3]
        var d3 = tmp0 + tmp6;

        if (!IsLineInsideTriangle(d1, d2, d3))
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        //Sub-expression: LLDI00B4 = Times[LLDI0013,LLDI009D]
        tmp0 = triangle.Point3Z * tmp1;

        //Sub-expression: LLDI00B5 = Times[-1,LLDI0012,LLDI00A1]
        tmp6 = -1 * triangle.Point3Y * tmp7;

        //Sub-expression: LLDI00B6 = Plus[LLDI00B4,LLDI00B5]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B7 = Times[LLDI0011,LLDI00A6]
        tmp6 = triangle.Point3X * tmp2;

        //Sub-expression: LLDI00B8 = Plus[LLDI00B6,LLDI00B7]
        tmp0 = tmp0 + tmp6;

        //Sub-expression: LLDI00B9 = Times[-1,LLDI0012,LLDI00AA]
        tmp6 = -1 * triangle.Point3Y * tmp3;

        //Sub-expression: LLDI00BA = Plus[LLDI009D,LLDI00B9]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BB = Times[LLDI0011,LLDI00AE]
        tmp6 = triangle.Point3X * tmp4;

        //Sub-expression: LLDI00BC = Plus[LLDI00BA,LLDI00BB]
        tmp1 = tmp1 + tmp6;

        //Sub-expression: LLDI00BD = Times[-1,LLDI0007,LLDI00BC]
        tmp6 = -1 * linePoint1Z * tmp1;

        //Sub-expression: LLDI00BE = Plus[LLDI00B8,LLDI00BD]
        tmp6 = tmp0 + tmp6;

        //Sub-expression: LLDI00BF = Times[-1,LLDI0013,LLDI00AA]
        tmp3 = -1 * triangle.Point3Z * tmp3;

        //Sub-expression: LLDI00C0 = Plus[LLDI00A1,LLDI00BF]
        tmp3 = tmp7 + tmp3;

        //Sub-expression: LLDI00C1 = Times[LLDI0011,LLDI00B2]
        tmp7 = triangle.Point3X * tmp5;

        //Sub-expression: LLDI00C2 = Plus[LLDI00C0,LLDI00C1]
        tmp3 = tmp3 + tmp7;

        //Sub-expression: LLDI00C3 = Times[LLDI0006,LLDI00C2]
        tmp7 = linePoint1Y * tmp3;

        //Sub-expression: LLDI00C4 = Plus[LLDI00BE,LLDI00C3]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI00C5 = Times[-1,LLDI0013,LLDI00AE]
        tmp4 = -1 * triangle.Point3Z * tmp4;

        //Sub-expression: LLDI00C6 = Plus[LLDI00A6,LLDI00C5]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C7 = Times[LLDI0012,LLDI00B2]
        tmp4 = triangle.Point3Y * tmp5;

        //Sub-expression: LLDI00C8 = Plus[LLDI00C6,LLDI00C7]
        tmp2 = tmp2 + tmp4;

        //Sub-expression: LLDI00C9 = Times[-1,LLDI0005,LLDI00C8]
        tmp4 = -1 * linePoint1X * tmp2;

        //Sub-expression: LLDI00CA = Plus[LLDI00C4,LLDI00C9]
        tmp4 = tmp6 + tmp4;

        //Sub-expression: LLDI00CB = Times[-1,LLDI00B8]
        tmp0 = -tmp0;

        //Sub-expression: LLDI00CC = Times[LLDI000A,LLDI00BC]
        tmp1 = linePoint2Z * tmp1;

        //Sub-expression: LLDI00CD = Plus[LLDI00CB,LLDI00CC]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00CE = Times[-1,LLDI0009,LLDI00C2]
        tmp1 = -1 * linePoint2Y * tmp3;

        //Sub-expression: LLDI00CF = Plus[LLDI00CD,LLDI00CE]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D0 = Times[LLDI0008,LLDI00C8]
        tmp1 = linePoint2X * tmp2;

        //Sub-expression: LLDI00D1 = Plus[LLDI00CF,LLDI00D0]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI00D2 = Plus[LLDI00D1,LLDI00CA]
        tmp0 = tmp0 + tmp4;

        //Sub-expression: LLDI00D3 = Power[LLDI00D2,-1]
        tmp0 = 1 / tmp0;

        //Output: LLDI0004 = Times[LLDI00CA,LLDI00D3]
        var t = tmp4 * tmp0;


        //Finish GMac Macro Code Generation, 2018-10-05T22:07:42.3247418+02:00

        if (t is < 0 or > 1)
        {
            ComputeTriangleIntersectionCounter.EndWithFalseOutcome();

            return IntersectionUtils.NoIntersection;
        }

        //Correction to get line parameter t w.r.t. line origin and direction
        //because t is w.r.t. two end points given by lineParamRange
        t = (1 - t) * lineParamMinValue + t * lineParamMaxValue;

        Debug.Assert(!double.IsNaN(t));

        ComputeTriangleIntersectionCounter.EndWithTrueOutcome();

        return new Tuple<bool, double>(true, t);
    }


    /// <summary>
    /// Test if the finite line (a line segment) intersects any of the given
    /// line segments
    /// </summary>
    /// <param name="trianglesList"></param>
    /// <returns></returns>
    public bool TestIntersection(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        return trianglesList.Any(TestIntersection);
    }

    public IEnumerable<Tuple<double, IFloat64Triangle3D>> ComputeIntersections(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        foreach (var triangle in trianglesList)
        {
            var result = ComputeIntersection(triangle);

            if (result.Item1)
                yield return new Tuple<double, IFloat64Triangle3D>(
                    result.Item2, triangle
                );
        }
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeFirstIntersection(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        var hasIntersection = false;
        var tValue = double.PositiveInfinity;
        IFloat64Triangle3D hitLineSegment = null;

        foreach (var triangle in trianglesList)
        {
            var result = ComputeIntersection(triangle);

            if (!result.Item1 || tValue <= result.Item2)
                continue;

            hasIntersection = true;
            tValue = result.Item2;
            hitLineSegment = triangle;
        }

        return new Tuple<bool, double, IFloat64Triangle3D>(
            hasIntersection,
            tValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeLastIntersection(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        var hasIntersection = false;
        var tValue = double.NegativeInfinity;
        IFloat64Triangle3D hitLineSegment = null;

        foreach (var triangle in trianglesList)
        {
            var result = ComputeIntersection(triangle);

            if (!result.Item1 || tValue > result.Item2)
                continue;

            hasIntersection = true;
            tValue = result.Item2;
            hitLineSegment = triangle;
        }

        return new Tuple<bool, double, IFloat64Triangle3D>(
            hasIntersection,
            tValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D> ComputeEdgeIntersections(IEnumerable<IFloat64Triangle3D> trianglesList)
    {
        var hasIntersection = false;
        var tValue1 = double.PositiveInfinity;
        var tValue2 = double.NegativeInfinity;
        IFloat64Triangle3D hitLineSegment1 = null;
        IFloat64Triangle3D hitLineSegment2 = null;

        foreach (var triangle in trianglesList)
        {
            var result = ComputeIntersection(triangle);

            if (!result.Item1)
                continue;

            hasIntersection = true;

            if (tValue1 > result.Item2)
            {
                tValue1 = result.Item2;
                hitLineSegment1 = triangle;
            }

            if (tValue2 < result.Item2)
            {
                tValue2 = result.Item2;
                hitLineSegment2 = triangle;
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
    public bool TestIntersection(IFloat64BoundingBox3D boundingBox)
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

    public Tuple<bool, double, double> ComputeIntersections(IFloat64BoundingBox3D boundingBox)
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

        return new Tuple<bool, double, double>(true, tMin, tMax);
    }


    public bool TestIntersection(IEnumerable<IFloat64BoundingBox3D> boundingBoxesList)
    {
        var lineData = Line.GetLineTraversalData(LineParameterLimits);

        return boundingBoxesList
            .Any(b => TestIntersection(lineData, b));
    }
    #endregion


    #region Line-Acceleration Grid Intersection
    public bool TestIntersection(IAccGrid3D<IFloat64Triangle3D> grid)
    {
        return grid
            .GetLineTraverser(Line, LineParameterLimits)
            .GetCells()
            .Where(cell => !ReferenceEquals(cell, null))
            .Select(cell => TestIntersection((IEnumerable<IFloat64Triangle3D>)cell))
            .Any(v => v);
    }

    public IEnumerable<Tuple<double, IFloat64Triangle3D>> ComputeIntersections(IAccGrid3D<IFloat64Triangle3D> grid)
    {
        var lineTraverser = AccGridLineTraverser3D.Create(grid, Line, LineParameterLimits);

        foreach (var cell in lineTraverser.GetActiveCells())
        {
            var tList =
                ComputeIntersections((IEnumerable<IFloat64Triangle3D>)cell);

            foreach (var t in tList.Where(t => t.Item1 < lineTraverser.TNext))
                yield return t;
        }
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeFirstIntersection(IAccGrid3D<IFloat64Triangle3D> grid)
    {
        var lineTraverser = AccGridLineTraverser3D.Create(grid, Line, LineParameterLimits);

        foreach (var cell in lineTraverser.GetActiveCells())
        {
            var t =
                ComputeFirstIntersection((IEnumerable<IFloat64Triangle3D>)cell);

            if (t.Item1 && t.Item2 < lineTraverser.TNext)
                return t;
        }

        return new Tuple<bool, double, IFloat64Triangle3D>(false, 0, null);
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeLastIntersection(IAccGrid3D<IFloat64Triangle3D> grid)
    {
        var oldLine = Line;
        var oldLineParameterLimits = LineParameterLimits;

        Line = new Float64Line3D(
            oldLine.OriginX + oldLine.DirectionX,
            oldLine.OriginY + oldLine.DirectionY,
            oldLine.OriginZ + oldLine.DirectionZ,
            -oldLine.DirectionX,
            -oldLine.DirectionY,
            -oldLine.DirectionZ
        );

        LineParameterLimits = 1d - oldLineParameterLimits;

        var result = ComputeFirstIntersection(grid);

        Line = oldLine;
        LineParameterLimits = oldLineParameterLimits;

        return result.Item1
            ? Tuple.Create(true, 1 - result.Item2, result.Item3)
            : new Tuple<bool, double, IFloat64Triangle3D>(false, 0, null);
    }

    public Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D> ComputeEdgeIntersections(IAccGrid3D<IFloat64Triangle3D> grid)
    {
        var first = ComputeFirstIntersection(grid);
        var last = ComputeLastIntersection(grid);

        if (first.Item1 && last.Item1)
            return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
                true,
                first.Item2,
                last.Item2,
                first.Item3,
                last.Item3
            );

        if (first.Item1)
            return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
                true,
                first.Item2,
                first.Item2,
                first.Item3,
                first.Item3
            );

        if (last.Item1)
            return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
                true,
                last.Item2,
                last.Item2,
                last.Item3,
                last.Item3
            );

        return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
            false,
            0,
            0,
            null,
            null
        );
    }
    #endregion


    #region Line-Acceleration BIH Intersection
    public bool TestIntersection(IAccBih3D<IFloat64Triangle3D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState3D>();

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
                    (IEnumerable<IFloat64Triangle3D>)state.BihNode
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
        //        .Any(state => TestIntersection((IAccBihNode3D<ITriangle3D>)state.BihNode));

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return hasIntersection;
    }

    public IEnumerable<Tuple<double, IFloat64Triangle3D>> ComputeIntersections(IAccBih3D<IFloat64Triangle3D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState3D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            yield break;

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        foreach (var state in lineTraverser.GetLeafTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode3D<IFloat64Triangle3D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            //For leaf nodes find all intersections within all its geometric
            //objects
            foreach (var triangle in node)
            {
                var result = ComputeIntersection(triangle, t0, t1);

                if (result.Item1)
                    yield return new Tuple<double, IFloat64Triangle3D>(
                        result.Item2,
                        triangle
                    );
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeFirstIntersection(IAccBih3D<IFloat64Triangle3D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState3D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, IFloat64Triangle3D>(false, 0, null);

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        var hasIntersection = false;
        IFloat64Triangle3D hitLineSegment = null;

        foreach (var state in lineTraverser.GetTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode3D<IFloat64Triangle3D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            if (!node.IsLeaf)
                continue;

            //For leaf nodes find first intersection within all its geometric
            //objects
            foreach (var triangle in node)
            {
                var result = ComputeIntersection(triangle, t0, t1);

                if (!result.Item1)
                    continue;

                hasIntersection = true;

                if (lineTraverser.LineParameterRange.MaxValue > result.Item2)
                {
                    hitLineSegment = triangle;

                    lineTraverser.ResetMaxParameterValue(result.Item2);
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, IFloat64Triangle3D>(
            hasIntersection,
            lineTraverser.LineParameterRange.MaxValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeLastIntersection(IAccBih3D<IFloat64Triangle3D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState3D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, IFloat64Triangle3D>(false, 0, null);

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        var hasIntersection = false;
        IFloat64Triangle3D hitLineSegment = null;

        foreach (var state in lineTraverser.GetTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode3D<IFloat64Triangle3D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            if (!node.IsLeaf)
                continue;

            //For leaf nodes find last intersection within all its geometric
            //objects
            foreach (var triangle in node)
            {
                var result = ComputeIntersection(triangle, t0, t1);

                if (!result.Item1)
                    continue;

                hasIntersection = true;

                if (lineTraverser.LineParameterRange.MinValue < result.Item2)
                {
                    hitLineSegment = triangle;

                    lineTraverser.ResetMinParameterValue(result.Item2);
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, IFloat64Triangle3D>(
            hasIntersection,
            lineTraverser.LineParameterRange.MinValue,
            hitLineSegment
        );
    }

    public Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D> ComputeEdgeIntersections(IAccBih3D<IFloat64Triangle3D> bih, bool storeTraversalStates = false)
    {
        if (storeTraversalStates)
            BihLineTraversalStates =
                Enumerable.Empty<AccBihLineTraversalState3D>();

        //Test line intersection with BIH bounding box
        var lineLimits = ComputeIntersections(bih.BoundingBox);

        if (!lineLimits.Item1)
            return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
                false,
                0, 0,
                null, null
            );

        var hasIntersection = false;
        var tValue1 = double.PositiveInfinity;
        var tValue2 = double.NegativeInfinity;
        IFloat64Triangle3D hitLineSegment1 = null;
        IFloat64Triangle3D hitLineSegment2 = null;

        //Traverse BIH nodes
        var lineTraverser =
            bih.GetLineTraverser(Line, lineLimits.Item2, lineLimits.Item3);

        foreach (var state in lineTraverser.GetLeafTraversalStates(storeTraversalStates))
        {
            var node = (IAccBihNode3D<IFloat64Triangle3D>)state.BihNode;
            var t0 = state.LineParameterMinValue;
            var t1 = state.LineParameterMaxValue;

            //For leaf nodes find all intersections within all its geometric
            //objects
            foreach (var triangle in node)
            {
                var result = ComputeIntersection(triangle, t0, t1);

                if (!result.Item1) continue;

                hasIntersection = true;

                if (tValue1 > result.Item2)
                {
                    tValue1 = result.Item2;
                    hitLineSegment1 = triangle;
                }

                if (tValue2 < result.Item2)
                {
                    tValue2 = result.Item2;
                    hitLineSegment2 = triangle;
                }
            }
        }

        if (storeTraversalStates)
            BihLineTraversalStates =
                lineTraverser.TraversalStates;

        return new Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D>(
            hasIntersection,
            tValue1,
            tValue2,
            hitLineSegment1,
            hitLineSegment2
        );
    }
    #endregion


    public bool TestIntersection(IFloat64GeometricObjectsContainer3D<IFloat64Triangle3D> trianglesList)
    {
        var grid = trianglesList as IAccGrid3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(grid, null))
            return TestIntersection(grid);

        var bih = trianglesList as IAccBih3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(bih, null))
            return TestIntersection(bih);

        return TestIntersection(
            (IEnumerable<IFloat64Triangle3D>)trianglesList
        );
    }

    public IEnumerable<Tuple<double, IFloat64Triangle3D>> ComputeIntersections(IFloat64GeometricObjectsContainer3D<IFloat64Triangle3D> trianglesList)
    {
        var grid = trianglesList as IAccGrid3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(grid, null))
            return ComputeIntersections(grid);

        var bih = trianglesList as IAccBih3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(bih, null))
            return ComputeIntersections(bih);

        return ComputeIntersections(
            (IEnumerable<IFloat64Triangle3D>)trianglesList
        );
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeFirstIntersection(IFloat64GeometricObjectsContainer3D<IFloat64Triangle3D> trianglesList)
    {
        var grid = trianglesList as IAccGrid3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(grid, null))
            return ComputeFirstIntersection(grid);

        var bih = trianglesList as IAccBih3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(bih, null))
            return ComputeFirstIntersection(bih);

        return ComputeFirstIntersection(
            (IEnumerable<IFloat64Triangle3D>)trianglesList
        );
    }

    public Tuple<bool, double, IFloat64Triangle3D> ComputeLastIntersection(IFloat64GeometricObjectsContainer3D<IFloat64Triangle3D> trianglesList)
    {
        var grid = trianglesList as IAccGrid3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(grid, null))
            return ComputeLastIntersection(grid);

        var bih = trianglesList as IAccBih3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(bih, null))
            return ComputeLastIntersection(bih);

        return ComputeLastIntersection(
            (IEnumerable<IFloat64Triangle3D>)trianglesList
        );
    }

    public Tuple<bool, double, double, IFloat64Triangle3D, IFloat64Triangle3D> ComputeEdgeIntersections(IFloat64GeometricObjectsContainer3D<IFloat64Triangle3D> trianglesList)
    {
        var grid = trianglesList as IAccGrid3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(grid, null))
            return ComputeEdgeIntersections(grid);

        var bih = trianglesList as IAccBih3D<IFloat64Triangle3D>;
        if (!ReferenceEquals(bih, null))
            return ComputeEdgeIntersections(bih);

        return ComputeEdgeIntersections(
            (IEnumerable<IFloat64Triangle3D>)trianglesList
        );
    }
}