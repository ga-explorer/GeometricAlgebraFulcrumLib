using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines;
using GeometricAlgebraFulcrumLib.Modeling.Geometry.BasicShapes.Lines.Space2D.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Statistics;

namespace GeometricAlgebraFulcrumLib.Modeling.Graphics.Computers.Reflections;

/// <summary>
/// This computer finds the reflection of geometric objects on a given
/// line specified as a line segment between two points in 2D
/// </summary>
public sealed class GcLineSegmentReflector2D
{
    public static SingleOutcomeEventSummary ComputePointReflectionCounter { get; }
        = SingleOutcomeEventSummary.Create(
            "GcLineSegmentReflector2D.ComputePointReflectionCounter",
            "Point on Line Reflection Computer in 2D"
        );


    /// <summary>
    /// The line of reflection
    /// </summary>
    public IFloat64LineSegment2D LineSegment { get; set; }


    public GcLineSegmentReflector2D()
    {
    }


    public LinFloat64Vector2D ReflectPointVa(ILinFloat64Vector2D point)
    {
        //https://math.stackexchange.com/questions/281503/reflecting-a-point-by-a-line-in-mathbb-r3

        var vx = LineSegment.Point2X - LineSegment.Point1X;
        var vy = LineSegment.Point2Y - LineSegment.Point1Y;

        var t1 = point.X * vx + point.Y * vy;
        var t2 = LineSegment.Point1X * vx + LineSegment.Point1Y * vy;
        var t3 = vx * vx + vy * vy;

        var t0 = (t1 - t2) / t3;

        var s = LineSegment.GetPointAt(t0);

        return LinFloat64Vector2D.Create(2 * s.X - point.X,
            2 * s.Y - point.Y);
    }
        
    public LinFloat64Vector2D ReflectPoint(ILinFloat64Vector2D point)
    {
        ComputePointReflectionCounter.Begin();

        //Begin GMac Macro Code Generation, 2018-10-05T10:15:06.0739361+02:00
        //Macro: cemsim.hga4d.ReflectPointOnLine2D
        //Input Variables: 6 used, 0 not used, 6 total.
        //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
        //Target Temp Variables: 8 total.
        //Output Variables: 2 total.
        //Computations: 1.07692307692308 average, 28 total.
        //Memory Reads: 1.80769230769231 average, 47 total.
        //Memory Writes: 26 total.
        //
        //Macro Binding Data: 
        //   result.#e1# = variable: var pointImageX
        //   result.#e2# = variable: var pointImageY
        //   point.#e1# = variable: point.ItemX
        //   point.#e2# = variable: point.ItemY
        //   v1.#e1# = variable: LineSegment.Point1X
        //   v1.#e2# = variable: LineSegment.Point1Y
        //   v2.#e1# = variable: LineSegment.Point2X
        //   v2.#e2# = variable: LineSegment.Point2Y

        double tmp0;
        double tmp1;
        double tmp2;
        double tmp3;
        double tmp4;
        double tmp5;
        double tmp6;
        double tmp7;

        //Sub-expression: LLDI0015 = Times[-1,LLDI0005]
        tmp0 = -LineSegment.Point1X;

        //Sub-expression: LLDI0016 = Plus[LLDI0015,LLDI0007]
        tmp1 = tmp0 + LineSegment.Point2X;

        //Sub-expression: LLDI0017 = Power[LLDI0016,2]
        tmp2 = tmp1 * tmp1;

        //Sub-expression: LLDI0018 = Times[-1,LLDI0006]
        tmp3 = -LineSegment.Point1Y;

        //Sub-expression: LLDI0019 = Plus[LLDI0018,LLDI0008]
        tmp4 = tmp3 + LineSegment.Point2Y;

        //Sub-expression: LLDI001A = Power[LLDI0019,2]
        tmp5 = tmp4 * tmp4;

        //Sub-expression: LLDI001B = Plus[LLDI0017,LLDI001A]
        tmp2 = tmp2 + tmp5;

        //Sub-expression: LLDI001C = Power[LLDI001B,-1]
        tmp2 = 1 / tmp2;

        //Sub-expression: LLDI001D = Times[LLDI0016,LLDI001C]
        tmp5 = tmp1 * tmp2;

        //Sub-expression: LLDI001E = Plus[LLDI0003,LLDI0015]
        tmp0 = point.X + tmp0;

        //Sub-expression: LLDI001F = Times[LLDI0016,LLDI001E]
        tmp6 = tmp1 * tmp0;

        //Sub-expression: LLDI0020 = Plus[LLDI0004,LLDI0018]
        tmp3 = point.Y + tmp3;

        //Sub-expression: LLDI0021 = Times[LLDI0019,LLDI0020]
        tmp7 = tmp4 * tmp3;

        //Sub-expression: LLDI0022 = Plus[LLDI001F,LLDI0021]
        tmp6 = tmp6 + tmp7;

        //Sub-expression: LLDI0023 = Times[LLDI001D,LLDI0022]
        tmp7 = tmp5 * tmp6;

        //Sub-expression: LLDI0024 = Times[LLDI0019,LLDI001C]
        tmp2 = tmp4 * tmp2;

        //Sub-expression: LLDI0025 = Times[-1,LLDI0019,LLDI001E]
        tmp0 = -1 * tmp4 * tmp0;

        //Sub-expression: LLDI0026 = Times[LLDI0016,LLDI0020]
        tmp1 = tmp1 * tmp3;

        //Sub-expression: LLDI0027 = Plus[LLDI0025,LLDI0026]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0028 = Times[LLDI0024,LLDI0027]
        tmp1 = tmp2 * tmp0;

        //Sub-expression: LLDI0029 = Plus[LLDI0023,LLDI0028]
        tmp1 = tmp7 + tmp1;

        //Output: LLDI0001 = Plus[LLDI0005,LLDI0029]
        var pointImageX = LineSegment.Point1X + tmp1;

        //Sub-expression: LLDI002A = Times[LLDI0024,LLDI0022]
        tmp1 = tmp2 * tmp6;

        //Sub-expression: LLDI002B = Times[-1,LLDI001D,LLDI0027]
        tmp0 = -1 * tmp5 * tmp0;

        //Sub-expression: LLDI002C = Plus[LLDI002A,LLDI002B]
        tmp0 = tmp1 + tmp0;

        //Output: LLDI0002 = Plus[LLDI0006,LLDI002C]
        var pointImageY = LineSegment.Point1Y + tmp0;


        //Finish GMac Macro Code Generation, 2018-10-05T10:15:06.0739361+02:00

        ComputePointReflectionCounter.End();

        return LinFloat64Vector2D.Create((Float64Scalar)pointImageX, (Float64Scalar)pointImageY);
    }
}