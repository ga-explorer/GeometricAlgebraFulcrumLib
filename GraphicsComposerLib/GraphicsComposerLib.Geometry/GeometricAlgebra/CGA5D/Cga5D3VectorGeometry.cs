using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.GeometricAlgebra.CGA5D
{
    public sealed class Cga5D3VectorGeometry 
        : Cga5DkVectorGeometry
    {
        public override int Grade => 3;


        public Cga5D3VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind) : base(bladeScalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T14:55:20.7671010+02:00
            //Macro: main.cga5d.SdfOpns
            //Input Variables: 13 used, 0 not used, 13 total.
            //Temp Variables: 54 sub-expressions, 0 generated temps, 54 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 1 total.
            //Computations: 1.23636363636364 average, 68 total.
            //Memory Reads: 1.74545454545455 average, 96 total.
            //Memory Writes: 55 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2^e3# = variable: Scalars[0]
            //   mv.#e1^e2^ep# = variable: Scalars[1]
            //   mv.#e1^e3^ep# = variable: Scalars[2]
            //   mv.#e2^e3^ep# = variable: Scalars[3]
            //   mv.#e1^e2^en# = variable: Scalars[4]
            //   mv.#e1^e3^en# = variable: Scalars[5]
            //   mv.#e2^e3^en# = variable: Scalars[6]
            //   mv.#e1^ep^en# = variable: Scalars[7]
            //   mv.#e2^ep^en# = variable: Scalars[8]
            //   mv.#e3^ep^en# = variable: Scalars[9]

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            //Sub-expression: LLDI0023 = Times[LLDI0004,LLDI0006]
            tmp0 = point.Z * Scalars[1];

            //Sub-expression: LLDI0024 = Times[-1,LLDI0003,LLDI0007]
            tmp1 = -1 * point.Y * Scalars[2];

            //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0026 = Times[LLDI0002,LLDI0008]
            tmp1 = point.X * Scalars[3];

            //Sub-expression: LLDI0027 = Plus[LLDI0025,LLDI0026]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0028 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0029 = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI002A = Plus[LLDI0028,LLDI0029]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI002B = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI002C = Plus[LLDI002A,LLDI002B]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI002D = Plus[-1,LLDI002C]
            tmp2 = -1 + tmp1;

            //Sub-expression: LLDI002E = Times[Rational[-1,2],LLDI0005,LLDI002D]
            tmp3 = -0.5 * Scalars[0] * tmp2;

            //Sub-expression: LLDI002F = Plus[LLDI0027,LLDI002E]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0030 = Power[LLDI002F,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI0031 = Times[LLDI0004,LLDI0009]
            tmp3 = point.Z * Scalars[4];

            //Sub-expression: LLDI0032 = Times[-1,LLDI0003,LLDI000A]
            tmp4 = -1 * point.Y * Scalars[5];

            //Sub-expression: LLDI0033 = Plus[LLDI0031,LLDI0032]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0034 = Times[LLDI0002,LLDI000B]
            tmp4 = point.X * Scalars[6];

            //Sub-expression: LLDI0035 = Plus[LLDI0033,LLDI0034]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0036 = Plus[1,LLDI002C]
            tmp1 = 1 + tmp1;

            //Sub-expression: LLDI0037 = Times[Rational[-1,2],LLDI0005,LLDI0036]
            tmp4 = -0.5 * Scalars[0] * tmp1;

            //Sub-expression: LLDI0038 = Plus[LLDI0035,LLDI0037]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0039 = Power[LLDI0038,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI003A = Times[-1,LLDI0039]
            tmp3 = -tmp3;

            //Sub-expression: LLDI003B = Plus[LLDI0030,LLDI003A]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI003C = Times[-1,LLDI0003,LLDI000C]
            tmp3 = -1 * point.Y * Scalars[7];

            //Sub-expression: LLDI003D = Times[LLDI0002,LLDI000D]
            tmp4 = point.X * Scalars[8];

            //Sub-expression: LLDI003E = Plus[LLDI003C,LLDI003D]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI003F = Times[Rational[1,2],LLDI0009,LLDI002D]
            tmp4 = 0.5 * Scalars[4] * tmp2;

            //Sub-expression: LLDI0040 = Plus[LLDI003E,LLDI003F]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0041 = Times[Rational[-1,2],LLDI0006,LLDI0036]
            tmp4 = -0.5 * Scalars[1] * tmp1;

            //Sub-expression: LLDI0042 = Plus[LLDI0040,LLDI0041]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0043 = Power[LLDI0042,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI0044 = Times[-1,LLDI0043]
            tmp3 = -tmp3;

            //Sub-expression: LLDI0045 = Plus[LLDI003B,LLDI0044]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0046 = Times[-1,LLDI0004,LLDI000C]
            tmp3 = -1 * point.Z * Scalars[7];

            //Sub-expression: LLDI0047 = Times[LLDI0002,LLDI000E]
            tmp4 = point.X * Scalars[9];

            //Sub-expression: LLDI0048 = Plus[LLDI0046,LLDI0047]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0049 = Times[Rational[1,2],LLDI000A,LLDI002D]
            tmp4 = 0.5 * Scalars[5] * tmp2;

            //Sub-expression: LLDI004A = Plus[LLDI0048,LLDI0049]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI004B = Times[Rational[-1,2],LLDI0007,LLDI0036]
            tmp4 = -0.5 * Scalars[2] * tmp1;

            //Sub-expression: LLDI004C = Plus[LLDI004A,LLDI004B]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI004D = Power[LLDI004C,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI004E = Times[-1,LLDI004D]
            tmp3 = -tmp3;

            //Sub-expression: LLDI004F = Plus[LLDI0045,LLDI004E]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0050 = Times[-1,LLDI0004,LLDI000D]
            tmp3 = -1 * point.Z * Scalars[8];

            //Sub-expression: LLDI0051 = Times[LLDI0003,LLDI000E]
            tmp4 = point.Y * Scalars[9];

            //Sub-expression: LLDI0052 = Plus[LLDI0050,LLDI0051]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0053 = Times[Rational[1,2],LLDI000B,LLDI002D]
            tmp2 = 0.5 * Scalars[6] * tmp2;

            //Sub-expression: LLDI0054 = Plus[LLDI0052,LLDI0053]
            tmp2 = tmp3 + tmp2;

            //Sub-expression: LLDI0055 = Times[Rational[-1,2],LLDI0008,LLDI0036]
            tmp1 = -0.5 * Scalars[3] * tmp1;

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp1 = tmp2 + tmp1;

            //Sub-expression: LLDI0057 = Power[LLDI0056,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0058 = Times[-1,LLDI0057]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI004F,LLDI0058]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T14:55:20.7860890+02:00

            return sdf;
        }

        protected override double ComputeSdfIpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T14:56:20.5076710+02:00
            //Macro: main.cga5d.SdfIpns
            //Input Variables: 13 used, 0 not used, 13 total.
            //Temp Variables: 84 sub-expressions, 0 generated temps, 84 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 1 total.
            //Computations: 1.18823529411765 average, 101 total.
            //Memory Reads: 1.72941176470588 average, 147 total.
            //Memory Writes: 85 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2^e3# = variable: Scalars[0]
            //   mv.#e1^e2^ep# = variable: Scalars[1]
            //   mv.#e1^e3^ep# = variable: Scalars[2]
            //   mv.#e2^e3^ep# = variable: Scalars[3]
            //   mv.#e1^e2^en# = variable: Scalars[4]
            //   mv.#e1^e3^en# = variable: Scalars[5]
            //   mv.#e2^e3^en# = variable: Scalars[6]
            //   mv.#e1^ep^en# = variable: Scalars[7]
            //   mv.#e2^ep^en# = variable: Scalars[8]
            //   mv.#e3^ep^en# = variable: Scalars[9]

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;
            double tmp6;

            //Sub-expression: LLDI0028 = Times[LLDI0004,LLDI0005]
            tmp0 = point.Z * Scalars[0];

            //Sub-expression: LLDI0029 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI002A = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI002B = Plus[LLDI0029,LLDI002A]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI002C = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI002D = Plus[LLDI002B,LLDI002C]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI002E = Plus[-1,LLDI002D]
            tmp2 = -1 + tmp1;

            //Sub-expression: LLDI002F = Times[LLDI0006,LLDI002E]
            tmp3 = Scalars[1] * tmp2;

            //Sub-expression: LLDI0030 = Plus[1,LLDI002D]
            tmp4 = 1 + tmp1;

            //Sub-expression: LLDI0031 = Times[-1,LLDI0009,LLDI0030]
            tmp5 = -1 * Scalars[4] * tmp4;

            //Sub-expression: LLDI0032 = Plus[LLDI002F,LLDI0031]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0033 = Times[Rational[1,2],LLDI0032]
            tmp3 = 0.5 * tmp3;

            //Sub-expression: LLDI0034 = Plus[LLDI0028,LLDI0033]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0035 = Power[LLDI0034,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI0036 = Times[-2,LLDI0003,LLDI0005]
            tmp3 = -2 * point.Y * Scalars[0];

            //Sub-expression: LLDI0037 = Times[LLDI0007,LLDI002E]
            tmp5 = Scalars[2] * tmp2;

            //Sub-expression: LLDI0038 = Plus[LLDI0036,LLDI0037]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0039 = Times[-1,LLDI000A,LLDI0030]
            tmp5 = -1 * Scalars[5] * tmp4;

            //Sub-expression: LLDI003A = Plus[LLDI0038,LLDI0039]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI003B = Times[Rational[1,2],LLDI003A]
            tmp3 = 0.5 * tmp3;

            //Sub-expression: LLDI003C = Power[LLDI003B,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI003D = Plus[LLDI0035,LLDI003C]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI003E = Times[LLDI0002,LLDI0005]
            tmp3 = point.X * Scalars[0];

            //Sub-expression: LLDI003F = Times[LLDI0008,LLDI002E]
            tmp5 = Scalars[3] * tmp2;

            //Sub-expression: LLDI0040 = Times[-1,LLDI000B,LLDI0030]
            tmp6 = -1 * Scalars[6] * tmp4;

            //Sub-expression: LLDI0041 = Plus[LLDI003F,LLDI0040]
            tmp5 = tmp5 + tmp6;

            //Sub-expression: LLDI0042 = Times[Rational[1,2],LLDI0041]
            tmp5 = 0.5 * tmp5;

            //Sub-expression: LLDI0043 = Plus[LLDI003E,LLDI0042]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0044 = Power[LLDI0043,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI0045 = Plus[LLDI003D,LLDI0044]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0046 = Times[-1,LLDI0003,LLDI0006]
            tmp3 = -1 * point.Y * Scalars[1];

            //Sub-expression: LLDI0047 = Times[-1,LLDI0004,LLDI0007]
            tmp5 = -1 * point.Z * Scalars[2];

            //Sub-expression: LLDI0048 = Plus[LLDI0046,LLDI0047]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0049 = Times[Rational[-1,2],LLDI000C,LLDI0030]
            tmp5 = -0.5 * Scalars[7] * tmp4;

            //Sub-expression: LLDI004A = Plus[LLDI0048,LLDI0049]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI004B = Power[LLDI004A,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI004C = Plus[LLDI0045,LLDI004B]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI004D = Times[LLDI0002,LLDI0006]
            tmp3 = point.X * Scalars[1];

            //Sub-expression: LLDI004E = Times[-1,LLDI0004,LLDI0008]
            tmp5 = -1 * point.Z * Scalars[3];

            //Sub-expression: LLDI004F = Plus[LLDI004D,LLDI004E]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0050 = Times[Rational[-1,2],LLDI000D,LLDI0030]
            tmp5 = -0.5 * Scalars[8] * tmp4;

            //Sub-expression: LLDI0051 = Plus[LLDI004F,LLDI0050]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0052 = Power[LLDI0051,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI0053 = Plus[LLDI004C,LLDI0052]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI0054 = Times[LLDI0002,LLDI0007]
            tmp3 = point.X * Scalars[2];

            //Sub-expression: LLDI0055 = Times[LLDI0003,LLDI0008]
            tmp5 = point.Y * Scalars[3];

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI0057 = Times[Rational[-1,2],LLDI000E,LLDI0030]
            tmp4 = -0.5 * Scalars[9] * tmp4;

            //Sub-expression: LLDI0058 = Plus[LLDI0056,LLDI0057]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0059 = Power[LLDI0058,2]
            tmp3 = tmp3 * tmp3;

            //Sub-expression: LLDI005A = Plus[LLDI0053,LLDI0059]
            tmp0 = tmp0 + tmp3;

            //Sub-expression: LLDI005B = Times[-2,LLDI0003,LLDI0009]
            tmp3 = -2 * point.Y * Scalars[4];

            //Sub-expression: LLDI005C = Times[-2,LLDI0004,LLDI000A]
            tmp4 = -2 * point.Z * Scalars[5];

            //Sub-expression: LLDI005D = Plus[LLDI005B,LLDI005C]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI005E = Plus[LLDI005D,LLDI000C]
            tmp3 = tmp3 + Scalars[7];

            //Sub-expression: LLDI005F = Times[-1,LLDI000C,LLDI002D]
            tmp1 = -1 * Scalars[7] * tmp1;

            //Sub-expression: LLDI0060 = Plus[LLDI005E,LLDI005F]
            tmp1 = tmp3 + tmp1;

            //Sub-expression: LLDI0061 = Times[Rational[1,2],LLDI0060]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI0062 = Power[LLDI0061,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0063 = Times[-1,LLDI0062]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0064 = Plus[LLDI005A,LLDI0063]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0065 = Times[LLDI0002,LLDI0009]
            tmp1 = point.X * Scalars[4];

            //Sub-expression: LLDI0066 = Times[-1,LLDI0004,LLDI000B]
            tmp3 = -1 * point.Z * Scalars[6];

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0068 = Times[Rational[-1,2],LLDI000D,LLDI002E]
            tmp3 = -0.5 * Scalars[8] * tmp2;

            //Sub-expression: LLDI0069 = Plus[LLDI0067,LLDI0068]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI006A = Power[LLDI0069,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI006B = Times[-1,LLDI006A]
            tmp1 = -tmp1;

            //Sub-expression: LLDI006C = Plus[LLDI0064,LLDI006B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI006D = Times[LLDI0002,LLDI000A]
            tmp1 = point.X * Scalars[5];

            //Sub-expression: LLDI006E = Times[LLDI0003,LLDI000B]
            tmp3 = point.Y * Scalars[6];

            //Sub-expression: LLDI006F = Plus[LLDI006D,LLDI006E]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0070 = Times[Rational[-1,2],LLDI000E,LLDI002E]
            tmp2 = -0.5 * Scalars[9] * tmp2;

            //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0072 = Power[LLDI0071,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0073 = Times[-1,LLDI0072]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0074 = Plus[LLDI006C,LLDI0073]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0075 = Times[LLDI0002,LLDI000C]
            tmp1 = point.X * Scalars[7];

            //Sub-expression: LLDI0076 = Times[LLDI0003,LLDI000D]
            tmp2 = point.Y * Scalars[8];

            //Sub-expression: LLDI0077 = Plus[LLDI0075,LLDI0076]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0078 = Times[LLDI0004,LLDI000E]
            tmp2 = point.Z * Scalars[9];

            //Sub-expression: LLDI0079 = Plus[LLDI0077,LLDI0078]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI007A = Power[LLDI0079,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI007B = Times[-1,LLDI007A]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI0074,LLDI007B]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T14:56:20.5086693+02:00

            return sdf;
        }
    }
}