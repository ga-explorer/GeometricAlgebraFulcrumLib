using DataStructuresLib.Statistics;
using GeometricAlgebraFulcrumLib.Lite.Geometry.BasicShapes.Triangles;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.Computers.Reflections
{
    /// <summary>
    /// This computer finds the reflection of geometric objects on a given
    /// plane specified as a triangle in 3D
    /// </summary>
    public sealed class GcTriangleReflector3D
    {
        public static SingleOutcomeEventSummary ComputePointReflectionCounter { get; }
            = SingleOutcomeEventSummary.Create(
                "GcTriangleReflector3D.ComputePointReflectionCounter",
                "Point on Plane Reflection Computer in 3D"
            );


        /// <summary>
        /// The line of reflection
        /// </summary>
        public ITriangle3D Triangle { get; set; }


        public Float64Vector3D ReflectPointVa(IFloat64Vector3D point)
        {
            //http://mathworld.wolfram.com/Reflection.html
            //https://en.wikipedia.org/wiki/Reflection_(mathematics)

            ComputePointReflectionCounter.Begin();

            var n = Triangle.GetNormal();
            var n2 = n.ENormSquared();
            var d = -n.ESp(Triangle.GetPoint1());
            var p = point.ToVector3D();

            var result = p - 2 * (n.ESp(p) + d) / n2 * n;

            ComputePointReflectionCounter.End();

            return result;
        }

        public Float64Vector3D ReflectPoint(IFloat64Vector3D point)
        {
            ComputePointReflectionCounter.Begin();

            //Begin GMac Macro Code Generation, 2018-10-05T10:56:14.0162855+02:00
            //Macro: cemsim.hga4d.ReflectPointOnPlane3D
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 65 sub-expressions, 0 generated temps, 65 total.
            //Target Temp Variables: 13 total.
            //Output Variables: 3 total.
            //Computations: 1.17647058823529 average, 80 total.
            //Memory Reads: 1.80882352941176 average, 123 total.
            //Memory Writes: 68 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: var pointImageX
            //   result.#e2# = variable: var pointImageY
            //   result.#e3# = variable: var pointImageZ
            //   point.#e1# = variable: point.ItemX
            //   point.#e2# = variable: point.ItemY
            //   point.#e3# = variable: point.ItemZ
            //   v1.#e1# = variable: Triangle.Point1X
            //   v1.#e2# = variable: Triangle.Point1Y
            //   v1.#e3# = variable: Triangle.Point1Z
            //   v2.#e1# = variable: Triangle.Point2X
            //   v2.#e2# = variable: Triangle.Point2Y
            //   v2.#e3# = variable: Triangle.Point2Z
            //   v3.#e1# = variable: Triangle.Point3X
            //   v3.#e2# = variable: Triangle.Point3Y
            //   v3.#e3# = variable: Triangle.Point3Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;
            double tmp9;
            double tmp10;
            double tmp11;
            double tmp12;

            //Sub-expression: LLDI0028 = Times[-1,LLDI0008]
            tmp0 = -Triangle.Point1Y;

            //Sub-expression: LLDI0029 = Plus[LLDI0028,LLDI000B]
            tmp1 = tmp0 + Triangle.Point2Y;

            //Sub-expression: LLDI002A = Times[-1,LLDI0007]
            tmp2 = -Triangle.Point1X;

            //Sub-expression: LLDI002B = Plus[LLDI002A,LLDI000D]
            tmp3 = tmp2 + Triangle.Point3X;

            //Sub-expression: LLDI002C = Times[-1,LLDI0029,LLDI002B]
            tmp4 = -1 * tmp1 * tmp3;

            //Sub-expression: LLDI002D = Plus[LLDI002A,LLDI000A]
            tmp5 = tmp2 + Triangle.Point2X;

            //Sub-expression: LLDI002E = Plus[LLDI0028,LLDI000E]
            tmp6 = tmp0 + Triangle.Point3Y;

            //Sub-expression: LLDI002F = Times[LLDI002D,LLDI002E]
            tmp7 = tmp5 * tmp6;

            //Sub-expression: LLDI0030 = Plus[LLDI002C,LLDI002F]
            tmp4 = tmp4 + tmp7;

            //Sub-expression: LLDI0031 = Power[LLDI0030,2]
            tmp7 = tmp4 * tmp4;

            //Sub-expression: LLDI0032 = Times[-1,LLDI0031]
            tmp7 = -tmp7;

            //Sub-expression: LLDI0033 = Times[-1,LLDI0009]
            tmp8 = -Triangle.Point1Z;

            //Sub-expression: LLDI0034 = Plus[LLDI0033,LLDI000C]
            tmp9 = tmp8 + Triangle.Point2Z;

            //Sub-expression: LLDI0035 = Times[-1,LLDI0034,LLDI002B]
            tmp3 = -1 * tmp9 * tmp3;

            //Sub-expression: LLDI0036 = Plus[LLDI0033,LLDI000F]
            tmp10 = tmp8 + Triangle.Point3Z;

            //Sub-expression: LLDI0037 = Times[LLDI002D,LLDI0036]
            tmp5 = tmp5 * tmp10;

            //Sub-expression: LLDI0038 = Plus[LLDI0035,LLDI0037]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0039 = Power[LLDI0038,2]
            tmp5 = tmp3 * tmp3;

            //Sub-expression: LLDI003A = Times[-1,LLDI0039]
            tmp5 = -tmp5;

            //Sub-expression: LLDI003B = Plus[LLDI0032,LLDI003A]
            tmp5 = tmp7 + tmp5;

            //Sub-expression: LLDI003C = Times[-1,LLDI0034,LLDI002E]
            tmp6 = -1 * tmp9 * tmp6;

            //Sub-expression: LLDI003D = Times[LLDI0029,LLDI0036]
            tmp1 = tmp1 * tmp10;

            //Sub-expression: LLDI003E = Plus[LLDI003C,LLDI003D]
            tmp1 = tmp6 + tmp1;

            //Sub-expression: LLDI003F = Power[LLDI003E,2]
            tmp6 = tmp1 * tmp1;

            //Sub-expression: LLDI0040 = Times[-1,LLDI003F]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0041 = Plus[LLDI003B,LLDI0040]
            tmp5 = tmp5 + tmp6;

            //Sub-expression: LLDI0042 = Power[LLDI0041,-1]
            tmp5 = 1 / tmp5;

            //Sub-expression: LLDI0043 = Times[LLDI0030,LLDI0042]
            tmp6 = tmp4 * tmp5;

            //Sub-expression: LLDI0044 = Plus[LLDI0004,LLDI002A]
            tmp2 = point.X + tmp2;

            //Sub-expression: LLDI0045 = Times[-1,LLDI0030,LLDI0044]
            tmp7 = -1 * tmp4 * tmp2;

            //Sub-expression: LLDI0046 = Plus[LLDI0006,LLDI0033]
            tmp8 = point.Z + tmp8;

            //Sub-expression: LLDI0047 = Times[LLDI003E,LLDI0046]
            tmp9 = tmp1 * tmp8;

            //Sub-expression: LLDI0048 = Plus[LLDI0045,LLDI0047]
            tmp7 = tmp7 + tmp9;

            //Sub-expression: LLDI0049 = Times[-1,LLDI0043,LLDI0048]
            tmp9 = -1 * tmp6 * tmp7;

            //Sub-expression: LLDI004A = Times[LLDI0038,LLDI0042]
            tmp10 = tmp3 * tmp5;

            //Sub-expression: LLDI004B = Times[-1,LLDI0038,LLDI0044]
            tmp11 = -1 * tmp3 * tmp2;

            //Sub-expression: LLDI004C = Plus[LLDI0005,LLDI0028]
            tmp0 = point.Y + tmp0;

            //Sub-expression: LLDI004D = Times[-1,LLDI003E,LLDI004C]
            tmp12 = -1 * tmp1 * tmp0;

            //Sub-expression: LLDI004E = Plus[LLDI004B,LLDI004D]
            tmp11 = tmp11 + tmp12;

            //Sub-expression: LLDI004F = Times[-1,LLDI004A,LLDI004E]
            tmp12 = -1 * tmp10 * tmp11;

            //Sub-expression: LLDI0050 = Plus[LLDI0049,LLDI004F]
            tmp9 = tmp9 + tmp12;

            //Sub-expression: LLDI0051 = Times[LLDI003E,LLDI0042]
            tmp5 = tmp1 * tmp5;

            //Sub-expression: LLDI0052 = Times[LLDI003E,LLDI0044]
            tmp1 = tmp1 * tmp2;

            //Sub-expression: LLDI0053 = Times[-1,LLDI0038,LLDI004C]
            tmp2 = -1 * tmp3 * tmp0;

            //Sub-expression: LLDI0054 = Plus[LLDI0052,LLDI0053]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0055 = Times[LLDI0030,LLDI0046]
            tmp2 = tmp4 * tmp8;

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0057 = Times[-1,LLDI0051,LLDI0056]
            tmp2 = -1 * tmp5 * tmp1;

            //Sub-expression: LLDI0058 = Plus[LLDI0050,LLDI0057]
            tmp2 = tmp9 + tmp2;

            //Sub-expression: LLDI0059 = Times[-1,LLDI0058]
            tmp2 = -tmp2;

            //Output: LLDI0001 = Plus[LLDI0007,LLDI0059]
            var pointImageX = Triangle.Point1X + tmp2;

            //Sub-expression: LLDI005A = Times[LLDI0030,LLDI004C]
            tmp0 = tmp4 * tmp0;

            //Sub-expression: LLDI005B = Times[LLDI0038,LLDI0046]
            tmp2 = tmp3 * tmp8;

            //Sub-expression: LLDI005C = Plus[LLDI005A,LLDI005B]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI005D = Times[LLDI0043,LLDI005C]
            tmp2 = tmp6 * tmp0;

            //Sub-expression: LLDI005E = Times[-1,LLDI0051,LLDI004E]
            tmp3 = -1 * tmp5 * tmp11;

            //Sub-expression: LLDI005F = Plus[LLDI005D,LLDI005E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0060 = Times[LLDI004A,LLDI0056]
            tmp3 = tmp10 * tmp1;

            //Sub-expression: LLDI0061 = Plus[LLDI005F,LLDI0060]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0062 = Times[-1,LLDI0061]
            tmp2 = -tmp2;

            //Output: LLDI0002 = Plus[LLDI0008,LLDI0062]
            var pointImageY = Triangle.Point1Y + tmp2;

            //Sub-expression: LLDI0063 = Times[LLDI004A,LLDI005C]
            tmp0 = tmp10 * tmp0;

            //Sub-expression: LLDI0064 = Times[LLDI0051,LLDI0048]
            tmp2 = tmp5 * tmp7;

            //Sub-expression: LLDI0065 = Plus[LLDI0063,LLDI0064]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0066 = Times[-1,LLDI0043,LLDI0056]
            tmp1 = -1 * tmp6 * tmp1;

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0068 = Times[-1,LLDI0067]
            tmp0 = -tmp0;

            //Output: LLDI0003 = Plus[LLDI0009,LLDI0068]
            var pointImageZ = Triangle.Point1Z + tmp0;


            //Finish GMac Macro Code Generation, 2018-10-05T10:56:14.0162855+02:00

            ComputePointReflectionCounter.End();

            return Float64Vector3D.Create(pointImageX, pointImageY, pointImageZ);
        }
    }
}