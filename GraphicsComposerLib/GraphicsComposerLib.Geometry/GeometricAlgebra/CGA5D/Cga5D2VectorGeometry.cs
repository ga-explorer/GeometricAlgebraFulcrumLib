using NumericalGeometryLib.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.GeometricAlgebra.CGA5D
{
    public sealed class Cga5D2VectorGeometry 
        : Cga5DkVectorGeometry
    {
        public override int Grade => 2;


        public Cga5D2VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind) : base(bladeScalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(ITuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T14:37:02.3911290+02:00
            //Macro: main.cga5d.SdfOpns
            //Input Variables: 13 used, 0 not used, 13 total.
            //Temp Variables: 90 sub-expressions, 0 generated temps, 90 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 1 total.
            //Computations: 1.20879120879121 average, 110 total.
            //Memory Reads: 1.73626373626374 average, 158 total.
            //Memory Writes: 91 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2# = variable: Scalars[0]
            //   mv.#e1^e3# = variable: Scalars[1]
            //   mv.#e2^e3# = variable: Scalars[2]
            //   mv.#e1^ep# = variable: Scalars[3]
            //   mv.#e2^ep# = variable: Scalars[4]
            //   mv.#e3^ep# = variable: Scalars[5]
            //   mv.#e1^en# = variable: Scalars[6]
            //   mv.#e2^en# = variable: Scalars[7]
            //   mv.#e3^en# = variable: Scalars[8]
            //   mv.#ep^en# = variable: Scalars[9]

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            //Sub-expression: LLDI0028 = Times[LLDI0004,LLDI0005]
            tmp0 = point.Z * Scalars[0];

            //Sub-expression: LLDI0029 = Times[-1,LLDI0003,LLDI0006]
            tmp1 = -1 * point.Y * Scalars[1];

            //Sub-expression: LLDI002A = Plus[LLDI0028,LLDI0029]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002B = Times[LLDI0002,LLDI0007]
            tmp1 = point.X * Scalars[2];

            //Sub-expression: LLDI002C = Plus[LLDI002A,LLDI002B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002D = Power[LLDI002C,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI002E = Times[-1,LLDI0003,LLDI0008]
            tmp1 = -1 * point.Y * Scalars[3];

            //Sub-expression: LLDI002F = Times[LLDI0002,LLDI0009]
            tmp2 = point.X * Scalars[4];

            //Sub-expression: LLDI0030 = Plus[LLDI002E,LLDI002F]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0031 = Power[LLDI0002,2]
            tmp2 = point.X * point.X;

            //Sub-expression: LLDI0032 = Power[LLDI0003,2]
            tmp3 = point.Y * point.Y;

            //Sub-expression: LLDI0033 = Plus[LLDI0031,LLDI0032]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0034 = Power[LLDI0004,2]
            tmp3 = point.Z * point.Z;

            //Sub-expression: LLDI0035 = Plus[LLDI0033,LLDI0034]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0036 = Plus[-1,LLDI0035]
            tmp3 = -1 + tmp2;

            //Sub-expression: LLDI0037 = Times[Rational[1,2],LLDI0005,LLDI0036]
            tmp4 = 0.5 * Scalars[0] * tmp3;

            //Sub-expression: LLDI0038 = Plus[LLDI0030,LLDI0037]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0039 = Power[LLDI0038,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI003A = Plus[LLDI002D,LLDI0039]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI003B = Times[-1,LLDI0004,LLDI0008]
            tmp1 = -1 * point.Z * Scalars[3];

            //Sub-expression: LLDI003C = Times[LLDI0002,LLDI000A]
            tmp4 = point.X * Scalars[5];

            //Sub-expression: LLDI003D = Plus[LLDI003B,LLDI003C]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI003E = Times[Rational[1,2],LLDI0006,LLDI0036]
            tmp4 = 0.5 * Scalars[1] * tmp3;

            //Sub-expression: LLDI003F = Plus[LLDI003D,LLDI003E]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0040 = Power[LLDI003F,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0041 = Plus[LLDI003A,LLDI0040]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0042 = Times[-1,LLDI0004,LLDI0009]
            tmp1 = -1 * point.Z * Scalars[4];

            //Sub-expression: LLDI0043 = Times[LLDI0003,LLDI000A]
            tmp4 = point.Y * Scalars[5];

            //Sub-expression: LLDI0044 = Plus[LLDI0042,LLDI0043]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0045 = Times[Rational[1,2],LLDI0007,LLDI0036]
            tmp3 = 0.5 * Scalars[2] * tmp3;

            //Sub-expression: LLDI0046 = Plus[LLDI0044,LLDI0045]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0047 = Power[LLDI0046,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0048 = Plus[LLDI0041,LLDI0047]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0049 = Times[-1,LLDI0003,LLDI000B]
            tmp1 = -1 * point.Y * Scalars[6];

            //Sub-expression: LLDI004A = Times[LLDI0002,LLDI000C]
            tmp3 = point.X * Scalars[7];

            //Sub-expression: LLDI004B = Plus[LLDI0049,LLDI004A]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI004C = Plus[1,LLDI0035]
            tmp3 = 1 + tmp2;

            //Sub-expression: LLDI004D = Times[Rational[1,2],LLDI0005,LLDI004C]
            tmp4 = 0.5 * Scalars[0] * tmp3;

            //Sub-expression: LLDI004E = Plus[LLDI004B,LLDI004D]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI004F = Power[LLDI004E,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0050 = Times[-1,LLDI004F]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0051 = Plus[LLDI0048,LLDI0050]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0052 = Times[-1,LLDI0004,LLDI000B]
            tmp1 = -1 * point.Z * Scalars[6];

            //Sub-expression: LLDI0053 = Times[LLDI0002,LLDI000D]
            tmp4 = point.X * Scalars[8];

            //Sub-expression: LLDI0054 = Plus[LLDI0052,LLDI0053]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0055 = Times[Rational[1,2],LLDI0006,LLDI004C]
            tmp4 = 0.5 * Scalars[1] * tmp3;

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI0057 = Power[LLDI0056,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0058 = Times[-1,LLDI0057]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0059 = Plus[LLDI0051,LLDI0058]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI005A = Times[-1,LLDI0004,LLDI000C]
            tmp1 = -1 * point.Z * Scalars[7];

            //Sub-expression: LLDI005B = Times[LLDI0003,LLDI000D]
            tmp4 = point.Y * Scalars[8];

            //Sub-expression: LLDI005C = Plus[LLDI005A,LLDI005B]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI005D = Times[Rational[1,2],LLDI0007,LLDI004C]
            tmp3 = 0.5 * Scalars[2] * tmp3;

            //Sub-expression: LLDI005E = Plus[LLDI005C,LLDI005D]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI005F = Power[LLDI005E,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0060 = Times[-1,LLDI005F]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0061 = Plus[LLDI0059,LLDI0060]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0062 = Plus[LLDI0008,LLDI000B]
            tmp1 = Scalars[3] + Scalars[6];

            //Sub-expression: LLDI0063 = Times[2,LLDI0002,LLDI000E]
            tmp3 = 2 * point.X * Scalars[9];

            //Sub-expression: LLDI0064 = Plus[LLDI0062,LLDI0063]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0065 = Times[LLDI0008,LLDI0035]
            tmp3 = Scalars[3] * tmp2;

            //Sub-expression: LLDI0066 = Plus[LLDI0064,LLDI0065]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0067 = Times[-1,LLDI000B,LLDI0035]
            tmp3 = -1 * Scalars[6] * tmp2;

            //Sub-expression: LLDI0068 = Plus[LLDI0066,LLDI0067]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0069 = Times[Rational[1,2],LLDI0068]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI006A = Power[LLDI0069,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI006B = Times[-1,LLDI006A]
            tmp1 = -tmp1;

            //Sub-expression: LLDI006C = Plus[LLDI0061,LLDI006B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI006D = Plus[LLDI0009,LLDI000C]
            tmp1 = Scalars[4] + Scalars[7];

            //Sub-expression: LLDI006E = Times[2,LLDI0003,LLDI000E]
            tmp3 = 2 * point.Y * Scalars[9];

            //Sub-expression: LLDI006F = Plus[LLDI006D,LLDI006E]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0070 = Times[LLDI0009,LLDI0035]
            tmp3 = Scalars[4] * tmp2;

            //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0072 = Times[-1,LLDI000C,LLDI0035]
            tmp3 = -1 * Scalars[7] * tmp2;

            //Sub-expression: LLDI0073 = Plus[LLDI0071,LLDI0072]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI0074 = Times[Rational[1,2],LLDI0073]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI0075 = Power[LLDI0074,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0076 = Times[-1,LLDI0075]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0077 = Plus[LLDI006C,LLDI0076]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0078 = Plus[LLDI000A,LLDI000D]
            tmp1 = Scalars[5] + Scalars[8];

            //Sub-expression: LLDI0079 = Times[2,LLDI0004,LLDI000E]
            tmp3 = 2 * point.Z * Scalars[9];

            //Sub-expression: LLDI007A = Plus[LLDI0078,LLDI0079]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI007B = Times[LLDI000A,LLDI0035]
            tmp3 = Scalars[5] * tmp2;

            //Sub-expression: LLDI007C = Plus[LLDI007A,LLDI007B]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI007D = Times[-1,LLDI000D,LLDI0035]
            tmp2 = -1 * Scalars[8] * tmp2;

            //Sub-expression: LLDI007E = Plus[LLDI007C,LLDI007D]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI007F = Times[Rational[1,2],LLDI007E]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI0080 = Power[LLDI007F,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0081 = Times[-1,LLDI0080]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI0077,LLDI0081]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T14:37:02.4091181+02:00

            return sdf;
        }

        protected override double ComputeSdfIpns(ITuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T14:37:58.9871040+02:00
            //Macro: main.cga5d.SdfIpns
            //Input Variables: 13 used, 0 not used, 13 total.
            //Temp Variables: 60 sub-expressions, 0 generated temps, 60 total.
            //Target Temp Variables: 4 total.
            //Output Variables: 1 total.
            //Computations: 1.18032786885246 average, 72 total.
            //Memory Reads: 1.77049180327869 average, 108 total.
            //Memory Writes: 61 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2# = variable: Scalars[0]
            //   mv.#e1^e3# = variable: Scalars[1]
            //   mv.#e2^e3# = variable: Scalars[2]
            //   mv.#e1^ep# = variable: Scalars[3]
            //   mv.#e2^ep# = variable: Scalars[4]
            //   mv.#e3^ep# = variable: Scalars[5]
            //   mv.#e1^en# = variable: Scalars[6]
            //   mv.#e2^en# = variable: Scalars[7]
            //   mv.#e3^en# = variable: Scalars[8]
            //   mv.#ep^en# = variable: Scalars[9]

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;

            //Sub-expression: LLDI0023 = Times[-2,LLDI0003,LLDI0005]
            tmp0 = -2 * point.Y * Scalars[0];

            //Sub-expression: LLDI0024 = Times[-2,LLDI0004,LLDI0006]
            tmp1 = -2 * point.Z * Scalars[1];

            //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0026 = Plus[LLDI0025,LLDI0008]
            tmp0 = tmp0 + Scalars[3];

            //Sub-expression: LLDI0027 = Plus[LLDI0026,LLDI000B]
            tmp0 = tmp0 + Scalars[6];

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

            //Sub-expression: LLDI002D = Times[-1,LLDI0008,LLDI002C]
            tmp2 = -1 * Scalars[3] * tmp1;

            //Sub-expression: LLDI002E = Plus[LLDI0027,LLDI002D]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI002F = Times[LLDI000B,LLDI002C]
            tmp2 = Scalars[6] * tmp1;

            //Sub-expression: LLDI0030 = Plus[LLDI002E,LLDI002F]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0031 = Times[Rational[1,2],LLDI0030]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI0032 = Power[LLDI0031,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI0033 = Times[2,LLDI0002,LLDI0005]
            tmp2 = 2 * point.X * Scalars[0];

            //Sub-expression: LLDI0034 = Times[-2,LLDI0004,LLDI0007]
            tmp3 = -2 * point.Z * Scalars[2];

            //Sub-expression: LLDI0035 = Plus[LLDI0033,LLDI0034]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0036 = Plus[LLDI0035,LLDI0009]
            tmp2 = tmp2 + Scalars[4];

            //Sub-expression: LLDI0037 = Plus[LLDI0036,LLDI000C]
            tmp2 = tmp2 + Scalars[7];

            //Sub-expression: LLDI0038 = Times[-1,LLDI0009,LLDI002C]
            tmp3 = -1 * Scalars[4] * tmp1;

            //Sub-expression: LLDI0039 = Plus[LLDI0037,LLDI0038]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI003A = Times[LLDI000C,LLDI002C]
            tmp3 = Scalars[7] * tmp1;

            //Sub-expression: LLDI003B = Plus[LLDI0039,LLDI003A]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI003C = Times[Rational[1,2],LLDI003B]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI003D = Power[LLDI003C,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI003E = Plus[LLDI0032,LLDI003D]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI003F = Times[2,LLDI0002,LLDI0006]
            tmp2 = 2 * point.X * Scalars[1];

            //Sub-expression: LLDI0040 = Times[2,LLDI0003,LLDI0007]
            tmp3 = 2 * point.Y * Scalars[2];

            //Sub-expression: LLDI0041 = Plus[LLDI003F,LLDI0040]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0042 = Plus[LLDI0041,LLDI000A]
            tmp2 = tmp2 + Scalars[5];

            //Sub-expression: LLDI0043 = Plus[LLDI0042,LLDI000D]
            tmp2 = tmp2 + Scalars[8];

            //Sub-expression: LLDI0044 = Times[-1,LLDI000A,LLDI002C]
            tmp3 = -1 * Scalars[5] * tmp1;

            //Sub-expression: LLDI0045 = Plus[LLDI0043,LLDI0044]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0046 = Times[LLDI000D,LLDI002C]
            tmp3 = Scalars[8] * tmp1;

            //Sub-expression: LLDI0047 = Plus[LLDI0045,LLDI0046]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0048 = Times[Rational[1,2],LLDI0047]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI0049 = Power[LLDI0048,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI004A = Plus[LLDI003E,LLDI0049]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI004B = Times[LLDI0002,LLDI0008]
            tmp2 = point.X * Scalars[3];

            //Sub-expression: LLDI004C = Times[LLDI0003,LLDI0009]
            tmp3 = point.Y * Scalars[4];

            //Sub-expression: LLDI004D = Plus[LLDI004B,LLDI004C]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI004E = Times[LLDI0004,LLDI000A]
            tmp3 = point.Z * Scalars[5];

            //Sub-expression: LLDI004F = Plus[LLDI004D,LLDI004E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0050 = Plus[1,LLDI002C]
            tmp3 = 1 + tmp1;

            //Sub-expression: LLDI0051 = Times[Rational[1,2],LLDI000E,LLDI0050]
            tmp3 = 0.5 * Scalars[9] * tmp3;

            //Sub-expression: LLDI0052 = Plus[LLDI004F,LLDI0051]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0053 = Power[LLDI0052,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI0054 = Plus[LLDI004A,LLDI0053]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0055 = Times[LLDI0002,LLDI000B]
            tmp2 = point.X * Scalars[6];

            //Sub-expression: LLDI0056 = Times[LLDI0003,LLDI000C]
            tmp3 = point.Y * Scalars[7];

            //Sub-expression: LLDI0057 = Plus[LLDI0055,LLDI0056]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0058 = Times[LLDI0004,LLDI000D]
            tmp3 = point.Z * Scalars[8];

            //Sub-expression: LLDI0059 = Plus[LLDI0057,LLDI0058]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI005A = Plus[-1,LLDI002C]
            tmp1 = -1 + tmp1;

            //Sub-expression: LLDI005B = Times[Rational[1,2],LLDI000E,LLDI005A]
            tmp1 = 0.5 * Scalars[9] * tmp1;

            //Sub-expression: LLDI005C = Plus[LLDI0059,LLDI005B]
            tmp1 = tmp2 + tmp1;

            //Sub-expression: LLDI005D = Power[LLDI005C,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI005E = Times[-1,LLDI005D]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI0054,LLDI005E]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T14:37:58.9871040+02:00

            return sdf;
        }
    }
}