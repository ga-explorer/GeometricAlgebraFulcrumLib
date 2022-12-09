using NumericalGeometryLib.BasicMath;
using NumericalGeometryLib.BasicMath.Tuples;
using NumericalGeometryLib.BasicMath.Tuples.Immutable;

namespace GraphicsComposerLib.Geometry.GeometricAlgebra.CGA5D
{
    public sealed class Cga5DFullMultivectorGeometry
        : Cga5DMultivectorGeometry
    {
        internal Cga5DFullMultivectorGeometry(double[] scalars, MultivectorNullSpaceKind nullSpaceKind)
            : base(scalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T13:32:56.0514935+02:00
            //Macro: main.cga5d.SdfOpns
            //Input Variables: 34 used, 1 not used, 35 total.
            //Temp Variables: 225 sub-expressions, 0 generated temps, 225 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 1 total.
            //Computations: 1.21238938053097 average, 274 total.
            //Memory Reads: 1.74778761061947 average, 395 total.
            //Memory Writes: 226 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#E0# = variable: Scalars[0]
            //   mv.#e1# = variable: Scalars[1]
            //   mv.#e2# = variable: Scalars[2]
            //   mv.#e1^e2# = variable: Scalars[3]
            //   mv.#e3# = variable: Scalars[4]
            //   mv.#e1^e3# = variable: Scalars[5]
            //   mv.#e2^e3# = variable: Scalars[6]
            //   mv.#e1^e2^e3# = variable: Scalars[7]
            //   mv.#ep# = variable: Scalars[8]
            //   mv.#e1^ep# = variable: Scalars[9]
            //   mv.#e2^ep# = variable: Scalars[10]
            //   mv.#e1^e2^ep# = variable: Scalars[11]
            //   mv.#e3^ep# = variable: Scalars[12]
            //   mv.#e1^e3^ep# = variable: Scalars[13]
            //   mv.#e2^e3^ep# = variable: Scalars[14]
            //   mv.#e1^e2^e3^ep# = variable: Scalars[15]
            //   mv.#en# = variable: Scalars[16]
            //   mv.#e1^en# = variable: Scalars[17]
            //   mv.#e2^en# = variable: Scalars[18]
            //   mv.#e1^e2^en# = variable: Scalars[19]
            //   mv.#e3^en# = variable: Scalars[20]
            //   mv.#e1^e3^en# = variable: Scalars[21]
            //   mv.#e2^e3^en# = variable: Scalars[22]
            //   mv.#e1^e2^e3^en# = variable: Scalars[23]
            //   mv.#ep^en# = variable: Scalars[24]
            //   mv.#e1^ep^en# = variable: Scalars[25]
            //   mv.#e2^ep^en# = variable: Scalars[26]
            //   mv.#e1^e2^ep^en# = variable: Scalars[27]
            //   mv.#e3^ep^en# = variable: Scalars[28]
            //   mv.#e1^e3^ep^en# = variable: Scalars[29]
            //   mv.#e2^e3^ep^en# = variable: Scalars[30]
            //   mv.#e1^e2^e3^ep^en# = variable: Scalars[31]

            //Sub-expression: LLDI0053 = Times[LLDI0002,LLDI0005]
            var tmp0 = point.X * Scalars[0];

            //Sub-expression: LLDI0054 = Power[LLDI0053,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI0055 = Times[LLDI0003,LLDI0005]
            var tmp1 = point.Y * Scalars[0];

            //Sub-expression: LLDI0056 = Power[LLDI0055,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0057 = Plus[LLDI0054,LLDI0056]
            tmp0 += tmp1;

            //Sub-expression: LLDI0058 = Times[-1,LLDI0003,LLDI0006]
            tmp1 = -1 * point.Y * Scalars[1];

            //Sub-expression: LLDI0059 = Times[LLDI0002,LLDI0007]
            var tmp2 = point.X * Scalars[2];

            //Sub-expression: LLDI005A = Plus[LLDI0058,LLDI0059]
            tmp1 += tmp2;

            //Sub-expression: LLDI005B = Power[LLDI005A,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI005C = Plus[LLDI0057,LLDI005B]
            tmp0 += tmp1;

            //Sub-expression: LLDI005D = Times[LLDI0004,LLDI0005]
            tmp1 = point.Z * Scalars[0];

            //Sub-expression: LLDI005E = Power[LLDI005D,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI005F = Plus[LLDI005C,LLDI005E]
            tmp0 += tmp1;

            //Sub-expression: LLDI0060 = Times[-1,LLDI0004,LLDI0006]
            tmp1 = -1 * point.Z * Scalars[1];

            //Sub-expression: LLDI0061 = Times[LLDI0002,LLDI0009]
            tmp2 = point.X * Scalars[4];

            //Sub-expression: LLDI0062 = Plus[LLDI0060,LLDI0061]
            tmp1 += tmp2;

            //Sub-expression: LLDI0063 = Power[LLDI0062,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0064 = Plus[LLDI005F,LLDI0063]
            tmp0 += tmp1;

            //Sub-expression: LLDI0065 = Times[-1,LLDI0004,LLDI0007]
            tmp1 = -1 * point.Z * Scalars[2];

            //Sub-expression: LLDI0066 = Times[LLDI0003,LLDI0009]
            tmp2 = point.Y * Scalars[4];

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp1 += tmp2;

            //Sub-expression: LLDI0068 = Power[LLDI0067,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0069 = Plus[LLDI0064,LLDI0068]
            tmp0 += tmp1;

            //Sub-expression: LLDI006A = Times[LLDI0004,LLDI0008]
            tmp1 = point.Z * Scalars[3];

            //Sub-expression: LLDI006B = Times[-1,LLDI0003,LLDI000A]
            tmp2 = -1 * point.Y * Scalars[5];

            //Sub-expression: LLDI006C = Plus[LLDI006A,LLDI006B]
            tmp1 += tmp2;

            //Sub-expression: LLDI006D = Times[LLDI0002,LLDI000B]
            tmp2 = point.X * Scalars[6];

            //Sub-expression: LLDI006E = Plus[LLDI006C,LLDI006D]
            tmp1 += tmp2;

            //Sub-expression: LLDI006F = Power[LLDI006E,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0070 = Plus[LLDI0069,LLDI006F]
            tmp0 += tmp1;

            //Sub-expression: LLDI0071 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0072 = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI0073 = Plus[LLDI0071,LLDI0072]
            tmp1 += tmp2;

            //Sub-expression: LLDI0074 = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI0075 = Plus[LLDI0073,LLDI0074]
            tmp1 += tmp2;

            //Sub-expression: LLDI0076 = Plus[-1,LLDI0075]
            tmp2 = -1 + tmp1;

            //Sub-expression: LLDI0077 = Times[Rational[1,2],LLDI0005,LLDI0076]
            var tmp3 = 0.5 * Scalars[0] * tmp2;

            //Sub-expression: LLDI0078 = Power[LLDI0077,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0079 = Plus[LLDI0070,LLDI0078]
            tmp0 += tmp3;

            //Sub-expression: LLDI007A = Times[LLDI0002,LLDI000D]
            tmp3 = point.X * Scalars[8];

            //Sub-expression: LLDI007B = Times[Rational[-1,2],LLDI0006,LLDI0076]
            var tmp4 = -0.5 * Scalars[1] * tmp2;

            //Sub-expression: LLDI007C = Plus[LLDI007A,LLDI007B]
            tmp3 += tmp4;

            //Sub-expression: LLDI007D = Power[LLDI007C,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI007E = Plus[LLDI0079,LLDI007D]
            tmp0 += tmp3;

            //Sub-expression: LLDI007F = Times[LLDI0003,LLDI000D]
            tmp3 = point.Y * Scalars[8];

            //Sub-expression: LLDI0080 = Times[Rational[-1,2],LLDI0007,LLDI0076]
            tmp4 = -0.5 * Scalars[2] * tmp2;

            //Sub-expression: LLDI0081 = Plus[LLDI007F,LLDI0080]
            tmp3 += tmp4;

            //Sub-expression: LLDI0082 = Power[LLDI0081,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0083 = Plus[LLDI007E,LLDI0082]
            tmp0 += tmp3;

            //Sub-expression: LLDI0084 = Times[-1,LLDI0003,LLDI000E]
            tmp3 = -1 * point.Y * Scalars[9];

            //Sub-expression: LLDI0085 = Times[LLDI0002,LLDI000F]
            tmp4 = point.X * Scalars[10];

            //Sub-expression: LLDI0086 = Plus[LLDI0084,LLDI0085]
            tmp3 += tmp4;

            //Sub-expression: LLDI0087 = Times[Rational[1,2],LLDI0008,LLDI0076]
            tmp4 = 0.5 * Scalars[3] * tmp2;

            //Sub-expression: LLDI0088 = Plus[LLDI0086,LLDI0087]
            tmp3 += tmp4;

            //Sub-expression: LLDI0089 = Power[LLDI0088,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI008A = Plus[LLDI0083,LLDI0089]
            tmp0 += tmp3;

            //Sub-expression: LLDI008B = Times[LLDI0004,LLDI000D]
            tmp3 = point.Z * Scalars[8];

            //Sub-expression: LLDI008C = Times[Rational[-1,2],LLDI0009,LLDI0076]
            tmp4 = -0.5 * Scalars[4] * tmp2;

            //Sub-expression: LLDI008D = Plus[LLDI008B,LLDI008C]
            tmp3 += tmp4;

            //Sub-expression: LLDI008E = Power[LLDI008D,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI008F = Plus[LLDI008A,LLDI008E]
            tmp0 += tmp3;

            //Sub-expression: LLDI0090 = Times[-1,LLDI0004,LLDI000E]
            tmp3 = -1 * point.Z * Scalars[9];

            //Sub-expression: LLDI0091 = Times[LLDI0002,LLDI0011]
            tmp4 = point.X * Scalars[12];

            //Sub-expression: LLDI0092 = Plus[LLDI0090,LLDI0091]
            tmp3 += tmp4;

            //Sub-expression: LLDI0093 = Times[Rational[1,2],LLDI000A,LLDI0076]
            tmp4 = 0.5 * Scalars[5] * tmp2;

            //Sub-expression: LLDI0094 = Plus[LLDI0092,LLDI0093]
            tmp3 += tmp4;

            //Sub-expression: LLDI0095 = Power[LLDI0094,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0096 = Plus[LLDI008F,LLDI0095]
            tmp0 += tmp3;

            //Sub-expression: LLDI0097 = Times[-1,LLDI0004,LLDI000F]
            tmp3 = -1 * point.Z * Scalars[10];

            //Sub-expression: LLDI0098 = Times[LLDI0003,LLDI0011]
            tmp4 = point.Y * Scalars[12];

            //Sub-expression: LLDI0099 = Plus[LLDI0097,LLDI0098]
            tmp3 += tmp4;

            //Sub-expression: LLDI009A = Times[Rational[1,2],LLDI000B,LLDI0076]
            tmp4 = 0.5 * Scalars[6] * tmp2;

            //Sub-expression: LLDI009B = Plus[LLDI0099,LLDI009A]
            tmp3 += tmp4;

            //Sub-expression: LLDI009C = Power[LLDI009B,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI009D = Plus[LLDI0096,LLDI009C]
            tmp0 += tmp3;

            //Sub-expression: LLDI009E = Times[LLDI0004,LLDI0010]
            tmp3 = point.Z * Scalars[11];

            //Sub-expression: LLDI009F = Times[-1,LLDI0003,LLDI0012]
            tmp4 = -1 * point.Y * Scalars[13];

            //Sub-expression: LLDI00A0 = Plus[LLDI009E,LLDI009F]
            tmp3 += tmp4;

            //Sub-expression: LLDI00A1 = Times[LLDI0002,LLDI0013]
            tmp4 = point.X * Scalars[14];

            //Sub-expression: LLDI00A2 = Plus[LLDI00A0,LLDI00A1]
            tmp3 += tmp4;

            //Sub-expression: LLDI00A3 = Times[Rational[-1,2],LLDI000C,LLDI0076]
            tmp4 = -0.5 * Scalars[7] * tmp2;

            //Sub-expression: LLDI00A4 = Plus[LLDI00A2,LLDI00A3]
            tmp3 += tmp4;

            //Sub-expression: LLDI00A5 = Power[LLDI00A4,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI00A6 = Plus[LLDI009D,LLDI00A5]
            tmp0 += tmp3;

            //Sub-expression: LLDI00A7 = Plus[1,LLDI0075]
            tmp3 = 1 + tmp1;

            //Sub-expression: LLDI00A8 = Times[Rational[1,2],LLDI0005,LLDI00A7]
            tmp4 = 0.5 * Scalars[0] * tmp3;

            //Sub-expression: LLDI00A9 = Power[LLDI00A8,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00AA = Times[-1,LLDI00A9]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00AB = Plus[LLDI00A6,LLDI00AA]
            tmp0 += tmp4;

            //Sub-expression: LLDI00AC = Times[LLDI0002,LLDI0015]
            tmp4 = point.X * Scalars[16];

            //Sub-expression: LLDI00AD = Times[Rational[-1,2],LLDI0006,LLDI00A7]
            var tmp5 = -0.5 * Scalars[1] * tmp3;

            //Sub-expression: LLDI00AE = Plus[LLDI00AC,LLDI00AD]
            tmp4 += tmp5;

            //Sub-expression: LLDI00AF = Power[LLDI00AE,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00B0 = Times[-1,LLDI00AF]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00B1 = Plus[LLDI00AB,LLDI00B0]
            tmp0 += tmp4;

            //Sub-expression: LLDI00B2 = Times[LLDI0003,LLDI0015]
            tmp4 = point.Y * Scalars[16];

            //Sub-expression: LLDI00B3 = Times[Rational[-1,2],LLDI0007,LLDI00A7]
            tmp5 = -0.5 * Scalars[2] * tmp3;

            //Sub-expression: LLDI00B4 = Plus[LLDI00B2,LLDI00B3]
            tmp4 += tmp5;

            //Sub-expression: LLDI00B5 = Power[LLDI00B4,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00B6 = Times[-1,LLDI00B5]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00B7 = Plus[LLDI00B1,LLDI00B6]
            tmp0 += tmp4;

            //Sub-expression: LLDI00B8 = Times[-1,LLDI0003,LLDI0016]
            tmp4 = -1 * point.Y * Scalars[17];

            //Sub-expression: LLDI00B9 = Times[LLDI0002,LLDI0017]
            tmp5 = point.X * Scalars[18];

            //Sub-expression: LLDI00BA = Plus[LLDI00B8,LLDI00B9]
            tmp4 += tmp5;

            //Sub-expression: LLDI00BB = Times[Rational[1,2],LLDI0008,LLDI00A7]
            tmp5 = 0.5 * Scalars[3] * tmp3;

            //Sub-expression: LLDI00BC = Plus[LLDI00BA,LLDI00BB]
            tmp4 += tmp5;

            //Sub-expression: LLDI00BD = Power[LLDI00BC,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00BE = Times[-1,LLDI00BD]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00BF = Plus[LLDI00B7,LLDI00BE]
            tmp0 += tmp4;

            //Sub-expression: LLDI00C0 = Times[LLDI0004,LLDI0015]
            tmp4 = point.Z * Scalars[16];

            //Sub-expression: LLDI00C1 = Times[Rational[-1,2],LLDI0009,LLDI00A7]
            tmp5 = -0.5 * Scalars[4] * tmp3;

            //Sub-expression: LLDI00C2 = Plus[LLDI00C0,LLDI00C1]
            tmp4 += tmp5;

            //Sub-expression: LLDI00C3 = Power[LLDI00C2,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00C4 = Times[-1,LLDI00C3]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00C5 = Plus[LLDI00BF,LLDI00C4]
            tmp0 += tmp4;

            //Sub-expression: LLDI00C6 = Times[-1,LLDI0004,LLDI0016]
            tmp4 = -1 * point.Z * Scalars[17];

            //Sub-expression: LLDI00C7 = Times[LLDI0002,LLDI0019]
            tmp5 = point.X * Scalars[20];

            //Sub-expression: LLDI00C8 = Plus[LLDI00C6,LLDI00C7]
            tmp4 += tmp5;

            //Sub-expression: LLDI00C9 = Times[Rational[1,2],LLDI000A,LLDI00A7]
            tmp5 = 0.5 * Scalars[5] * tmp3;

            //Sub-expression: LLDI00CA = Plus[LLDI00C8,LLDI00C9]
            tmp4 += tmp5;

            //Sub-expression: LLDI00CB = Power[LLDI00CA,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00CC = Times[-1,LLDI00CB]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00CD = Plus[LLDI00C5,LLDI00CC]
            tmp0 += tmp4;

            //Sub-expression: LLDI00CE = Times[-1,LLDI0004,LLDI0017]
            tmp4 = -1 * point.Z * Scalars[18];

            //Sub-expression: LLDI00CF = Times[LLDI0003,LLDI0019]
            tmp5 = point.Y * Scalars[20];

            //Sub-expression: LLDI00D0 = Plus[LLDI00CE,LLDI00CF]
            tmp4 += tmp5;

            //Sub-expression: LLDI00D1 = Times[Rational[1,2],LLDI000B,LLDI00A7]
            tmp5 = 0.5 * Scalars[6] * tmp3;

            //Sub-expression: LLDI00D2 = Plus[LLDI00D0,LLDI00D1]
            tmp4 += tmp5;

            //Sub-expression: LLDI00D3 = Power[LLDI00D2,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00D4 = Times[-1,LLDI00D3]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00D5 = Plus[LLDI00CD,LLDI00D4]
            tmp0 += tmp4;

            //Sub-expression: LLDI00D6 = Times[LLDI0004,LLDI0018]
            tmp4 = point.Z * Scalars[19];

            //Sub-expression: LLDI00D7 = Times[-1,LLDI0003,LLDI001A]
            tmp5 = -1 * point.Y * Scalars[21];

            //Sub-expression: LLDI00D8 = Plus[LLDI00D6,LLDI00D7]
            tmp4 += tmp5;

            //Sub-expression: LLDI00D9 = Times[LLDI0002,LLDI001B]
            tmp5 = point.X * Scalars[22];

            //Sub-expression: LLDI00DA = Plus[LLDI00D8,LLDI00D9]
            tmp4 += tmp5;

            //Sub-expression: LLDI00DB = Times[Rational[-1,2],LLDI000C,LLDI00A7]
            tmp5 = -0.5 * Scalars[7] * tmp3;

            //Sub-expression: LLDI00DC = Plus[LLDI00DA,LLDI00DB]
            tmp4 += tmp5;

            //Sub-expression: LLDI00DD = Power[LLDI00DC,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00DE = Times[-1,LLDI00DD]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00DF = Plus[LLDI00D5,LLDI00DE]
            tmp0 += tmp4;

            //Sub-expression: LLDI00E0 = Times[LLDI0015,LLDI0076]
            tmp4 = Scalars[16] * tmp2;

            //Sub-expression: LLDI00E1 = Times[-1,LLDI000D,LLDI00A7]
            tmp5 = -1 * Scalars[8] * tmp3;

            //Sub-expression: LLDI00E2 = Plus[LLDI00E0,LLDI00E1]
            tmp4 += tmp5;

            //Sub-expression: LLDI00E3 = Times[Rational[1,2],LLDI00E2]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI00E4 = Power[LLDI00E3,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00E5 = Times[-1,LLDI00E4]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00E6 = Plus[LLDI00DF,LLDI00E5]
            tmp0 += tmp4;

            //Sub-expression: LLDI00E7 = Plus[LLDI000E,LLDI0016]
            tmp4 = Scalars[9] + Scalars[17];

            //Sub-expression: LLDI00E8 = Times[2,LLDI0002,LLDI001D]
            tmp5 = 2 * point.X * Scalars[24];

            //Sub-expression: LLDI00E9 = Plus[LLDI00E7,LLDI00E8]
            tmp4 += tmp5;

            //Sub-expression: LLDI00EA = Times[LLDI000E,LLDI0075]
            tmp5 = Scalars[9] * tmp1;

            //Sub-expression: LLDI00EB = Plus[LLDI00E9,LLDI00EA]
            tmp4 += tmp5;

            //Sub-expression: LLDI00EC = Times[-1,LLDI0016,LLDI0075]
            tmp5 = -1 * Scalars[17] * tmp1;

            //Sub-expression: LLDI00ED = Plus[LLDI00EB,LLDI00EC]
            tmp4 += tmp5;

            //Sub-expression: LLDI00EE = Times[Rational[1,2],LLDI00ED]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI00EF = Power[LLDI00EE,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00F0 = Times[-1,LLDI00EF]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00F1 = Plus[LLDI00E6,LLDI00F0]
            tmp0 += tmp4;

            //Sub-expression: LLDI00F2 = Plus[LLDI000F,LLDI0017]
            tmp4 = Scalars[10] + Scalars[18];

            //Sub-expression: LLDI00F3 = Times[2,LLDI0003,LLDI001D]
            tmp5 = 2 * point.Y * Scalars[24];

            //Sub-expression: LLDI00F4 = Plus[LLDI00F2,LLDI00F3]
            tmp4 += tmp5;

            //Sub-expression: LLDI00F5 = Times[LLDI000F,LLDI0075]
            tmp5 = Scalars[10] * tmp1;

            //Sub-expression: LLDI00F6 = Plus[LLDI00F4,LLDI00F5]
            tmp4 += tmp5;

            //Sub-expression: LLDI00F7 = Times[-1,LLDI0017,LLDI0075]
            tmp5 = -1 * Scalars[18] * tmp1;

            //Sub-expression: LLDI00F8 = Plus[LLDI00F6,LLDI00F7]
            tmp4 += tmp5;

            //Sub-expression: LLDI00F9 = Times[Rational[1,2],LLDI00F8]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI00FA = Power[LLDI00F9,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00FB = Times[-1,LLDI00FA]
            tmp4 = -tmp4;

            //Sub-expression: LLDI00FC = Plus[LLDI00F1,LLDI00FB]
            tmp0 += tmp4;

            //Sub-expression: LLDI00FD = Times[-1,LLDI0003,LLDI001E]
            tmp4 = -1 * point.Y * Scalars[25];

            //Sub-expression: LLDI00FE = Times[LLDI0002,LLDI001F]
            tmp5 = point.X * Scalars[26];

            //Sub-expression: LLDI00FF = Plus[LLDI00FD,LLDI00FE]
            tmp4 += tmp5;

            //Sub-expression: LLDI0100 = Times[Rational[1,2],LLDI0018,LLDI0076]
            tmp5 = 0.5 * Scalars[19] * tmp2;

            //Sub-expression: LLDI0101 = Plus[LLDI00FF,LLDI0100]
            tmp4 += tmp5;

            //Sub-expression: LLDI0102 = Times[Rational[-1,2],LLDI0010,LLDI00A7]
            tmp5 = -0.5 * Scalars[11] * tmp3;

            //Sub-expression: LLDI0103 = Plus[LLDI0101,LLDI0102]
            tmp4 += tmp5;

            //Sub-expression: LLDI0104 = Power[LLDI0103,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0105 = Times[-1,LLDI0104]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0106 = Plus[LLDI00FC,LLDI0105]
            tmp0 += tmp4;

            //Sub-expression: LLDI0107 = Plus[LLDI0011,LLDI0019]
            tmp4 = Scalars[12] + Scalars[20];

            //Sub-expression: LLDI0108 = Times[2,LLDI0004,LLDI001D]
            tmp5 = 2 * point.Z * Scalars[24];

            //Sub-expression: LLDI0109 = Plus[LLDI0107,LLDI0108]
            tmp4 += tmp5;

            //Sub-expression: LLDI010A = Times[LLDI0011,LLDI0075]
            tmp5 = Scalars[12] * tmp1;

            //Sub-expression: LLDI010B = Plus[LLDI0109,LLDI010A]
            tmp4 += tmp5;

            //Sub-expression: LLDI010C = Times[-1,LLDI0019,LLDI0075]
            tmp5 = -1 * Scalars[20] * tmp1;

            //Sub-expression: LLDI010D = Plus[LLDI010B,LLDI010C]
            tmp4 += tmp5;

            //Sub-expression: LLDI010E = Times[Rational[1,2],LLDI010D]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI010F = Power[LLDI010E,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0110 = Times[-1,LLDI010F]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0111 = Plus[LLDI0106,LLDI0110]
            tmp0 += tmp4;

            //Sub-expression: LLDI0112 = Times[-1,LLDI0004,LLDI001E]
            tmp4 = -1 * point.Z * Scalars[25];

            //Sub-expression: LLDI0113 = Times[LLDI0002,LLDI0021]
            tmp5 = point.X * Scalars[28];

            //Sub-expression: LLDI0114 = Plus[LLDI0112,LLDI0113]
            tmp4 += tmp5;

            //Sub-expression: LLDI0115 = Times[Rational[1,2],LLDI001A,LLDI0076]
            tmp5 = 0.5 * Scalars[21] * tmp2;

            //Sub-expression: LLDI0116 = Plus[LLDI0114,LLDI0115]
            tmp4 += tmp5;

            //Sub-expression: LLDI0117 = Times[Rational[-1,2],LLDI0012,LLDI00A7]
            tmp5 = -0.5 * Scalars[13] * tmp3;

            //Sub-expression: LLDI0118 = Plus[LLDI0116,LLDI0117]
            tmp4 += tmp5;

            //Sub-expression: LLDI0119 = Power[LLDI0118,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI011A = Times[-1,LLDI0119]
            tmp4 = -tmp4;

            //Sub-expression: LLDI011B = Plus[LLDI0111,LLDI011A]
            tmp0 += tmp4;

            //Sub-expression: LLDI011C = Times[-1,LLDI0004,LLDI001F]
            tmp4 = -1 * point.Z * Scalars[26];

            //Sub-expression: LLDI011D = Times[LLDI0003,LLDI0021]
            tmp5 = point.Y * Scalars[28];

            //Sub-expression: LLDI011E = Plus[LLDI011C,LLDI011D]
            tmp4 += tmp5;

            //Sub-expression: LLDI011F = Times[Rational[1,2],LLDI001B,LLDI0076]
            tmp2 = 0.5 * Scalars[22] * tmp2;

            //Sub-expression: LLDI0120 = Plus[LLDI011E,LLDI011F]
            tmp2 = tmp4 + tmp2;

            //Sub-expression: LLDI0121 = Times[Rational[-1,2],LLDI0013,LLDI00A7]
            tmp3 = -0.5 * Scalars[14] * tmp3;

            //Sub-expression: LLDI0122 = Plus[LLDI0120,LLDI0121]
            tmp2 += tmp3;

            //Sub-expression: LLDI0123 = Power[LLDI0122,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI0124 = Times[-1,LLDI0123]
            tmp2 = -tmp2;

            //Sub-expression: LLDI0125 = Plus[LLDI011B,LLDI0124]
            tmp0 += tmp2;

            //Sub-expression: LLDI0126 = Plus[LLDI0014,LLDI001C]
            tmp2 = Scalars[15] + Scalars[23];

            //Sub-expression: LLDI0127 = Times[2,LLDI0004,LLDI0020]
            tmp3 = 2 * point.Z * Scalars[27];

            //Sub-expression: LLDI0128 = Plus[LLDI0126,LLDI0127]
            tmp2 += tmp3;

            //Sub-expression: LLDI0129 = Times[-2,LLDI0003,LLDI0022]
            tmp3 = -2 * point.Y * Scalars[29];

            //Sub-expression: LLDI012A = Plus[LLDI0128,LLDI0129]
            tmp2 += tmp3;

            //Sub-expression: LLDI012B = Times[2,LLDI0002,LLDI0023]
            tmp3 = 2 * point.X * Scalars[30];

            //Sub-expression: LLDI012C = Plus[LLDI012A,LLDI012B]
            tmp2 += tmp3;

            //Sub-expression: LLDI012D = Times[LLDI0014,LLDI0075]
            tmp3 = Scalars[15] * tmp1;

            //Sub-expression: LLDI012E = Plus[LLDI012C,LLDI012D]
            tmp2 += tmp3;

            //Sub-expression: LLDI012F = Times[-1,LLDI001C,LLDI0075]
            tmp1 = -1 * Scalars[23] * tmp1;

            //Sub-expression: LLDI0130 = Plus[LLDI012E,LLDI012F]
            tmp1 = tmp2 + tmp1;

            //Sub-expression: LLDI0131 = Times[Rational[1,2],LLDI0130]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI0132 = Power[LLDI0131,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0133 = Times[-1,LLDI0132]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI0125,LLDI0133]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T13:32:56.0754796+02:00

            return sdf;
        }

        protected override double ComputeSdfIpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T13:44:16.5061972+02:00
            //Macro: main.cga5d.SdfIpns
            //Input Variables: 34 used, 1 not used, 35 total.
            //Temp Variables: 230 sub-expressions, 0 generated temps, 230 total.
            //Target Temp Variables: 7 total.
            //Output Variables: 1 total.
            //Computations: 1.18614718614719 average, 274 total.
            //Memory Reads: 1.74025974025974 average, 402 total.
            //Memory Writes: 231 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#E0# = variable: Scalars[0]
            //   mv.#e1# = variable: Scalars[1]
            //   mv.#e2# = variable: Scalars[2]
            //   mv.#e1^e2# = variable: Scalars[3]
            //   mv.#e3# = variable: Scalars[4]
            //   mv.#e1^e3# = variable: Scalars[5]
            //   mv.#e2^e3# = variable: Scalars[6]
            //   mv.#e1^e2^e3# = variable: Scalars[7]
            //   mv.#ep# = variable: Scalars[8]
            //   mv.#e1^ep# = variable: Scalars[9]
            //   mv.#e2^ep# = variable: Scalars[10]
            //   mv.#e1^e2^ep# = variable: Scalars[11]
            //   mv.#e3^ep# = variable: Scalars[12]
            //   mv.#e1^e3^ep# = variable: Scalars[13]
            //   mv.#e2^e3^ep# = variable: Scalars[14]
            //   mv.#e1^e2^e3^ep# = variable: Scalars[15]
            //   mv.#en# = variable: Scalars[16]
            //   mv.#e1^en# = variable: Scalars[17]
            //   mv.#e2^en# = variable: Scalars[18]
            //   mv.#e1^e2^en# = variable: Scalars[19]
            //   mv.#e3^en# = variable: Scalars[20]
            //   mv.#e1^e3^en# = variable: Scalars[21]
            //   mv.#e2^e3^en# = variable: Scalars[22]
            //   mv.#e1^e2^e3^en# = variable: Scalars[23]
            //   mv.#ep^en# = variable: Scalars[24]
            //   mv.#e1^ep^en# = variable: Scalars[25]
            //   mv.#e2^ep^en# = variable: Scalars[26]
            //   mv.#e1^e2^ep^en# = variable: Scalars[27]
            //   mv.#e3^ep^en# = variable: Scalars[28]
            //   mv.#e1^e3^ep^en# = variable: Scalars[29]
            //   mv.#e2^e3^ep^en# = variable: Scalars[30]
            //   mv.#e1^e2^e3^ep^en# = variable: Scalars[31]

            //Sub-expression: LLDI0053 = Times[LLDI0002,LLDI0006]
            var tmp0 = point.X * Scalars[1];

            //Sub-expression: LLDI0054 = Times[LLDI0003,LLDI0007]
            var tmp1 = point.Y * Scalars[2];

            //Sub-expression: LLDI0055 = Plus[LLDI0053,LLDI0054]
            tmp0 += tmp1;

            //Sub-expression: LLDI0056 = Times[LLDI0004,LLDI0009]
            tmp1 = point.Z * Scalars[4];

            //Sub-expression: LLDI0057 = Plus[LLDI0055,LLDI0056]
            tmp0 += tmp1;

            //Sub-expression: LLDI0058 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0059 = Power[LLDI0003,2]
            var tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI005A = Plus[LLDI0058,LLDI0059]
            tmp1 += tmp2;

            //Sub-expression: LLDI005B = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI005C = Plus[LLDI005A,LLDI005B]
            tmp1 += tmp2;

            //Sub-expression: LLDI005D = Plus[-1,LLDI005C]
            tmp2 = -1 + tmp1;

            //Sub-expression: LLDI005E = Times[Rational[1,2],LLDI000D,LLDI005D]
            var tmp3 = 0.5 * Scalars[8] * tmp2;

            //Sub-expression: LLDI005F = Plus[LLDI0057,LLDI005E]
            tmp0 += tmp3;

            //Sub-expression: LLDI0060 = Plus[1,LLDI005C]
            tmp3 = 1 + tmp1;

            //Sub-expression: LLDI0061 = Times[Rational[-1,2],LLDI0015,LLDI0060]
            var tmp4 = -0.5 * Scalars[16] * tmp3;

            //Sub-expression: LLDI0062 = Plus[LLDI005F,LLDI0061]
            tmp0 += tmp4;

            //Sub-expression: LLDI0063 = Power[LLDI0062,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI0064 = Times[-2,LLDI0003,LLDI0008]
            tmp4 = -2 * point.Y * Scalars[3];

            //Sub-expression: LLDI0065 = Times[-2,LLDI0004,LLDI000A]
            var tmp5 = -2 * point.Z * Scalars[5];

            //Sub-expression: LLDI0066 = Plus[LLDI0064,LLDI0065]
            tmp4 += tmp5;

            //Sub-expression: LLDI0067 = Plus[LLDI0066,LLDI000E]
            tmp4 += Scalars[9];

            //Sub-expression: LLDI0068 = Plus[LLDI0067,LLDI0016]
            tmp4 += Scalars[17];

            //Sub-expression: LLDI0069 = Times[-1,LLDI000E,LLDI005C]
            tmp5 = -1 * Scalars[9] * tmp1;

            //Sub-expression: LLDI006A = Plus[LLDI0068,LLDI0069]
            tmp4 += tmp5;

            //Sub-expression: LLDI006B = Times[LLDI0016,LLDI005C]
            tmp5 = Scalars[17] * tmp1;

            //Sub-expression: LLDI006C = Plus[LLDI006A,LLDI006B]
            tmp4 += tmp5;

            //Sub-expression: LLDI006D = Times[Rational[1,2],LLDI006C]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI006E = Power[LLDI006D,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI006F = Plus[LLDI0063,LLDI006E]
            tmp0 += tmp4;

            //Sub-expression: LLDI0070 = Times[2,LLDI0002,LLDI0008]
            tmp4 = 2 * point.X * Scalars[3];

            //Sub-expression: LLDI0071 = Times[-2,LLDI0004,LLDI000B]
            tmp5 = -2 * point.Z * Scalars[6];

            //Sub-expression: LLDI0072 = Plus[LLDI0070,LLDI0071]
            tmp4 += tmp5;

            //Sub-expression: LLDI0073 = Plus[LLDI0072,LLDI000F]
            tmp4 += Scalars[10];

            //Sub-expression: LLDI0074 = Plus[LLDI0073,LLDI0017]
            tmp4 += Scalars[18];

            //Sub-expression: LLDI0075 = Times[-1,LLDI000F,LLDI005C]
            tmp5 = -1 * Scalars[10] * tmp1;

            //Sub-expression: LLDI0076 = Plus[LLDI0074,LLDI0075]
            tmp4 += tmp5;

            //Sub-expression: LLDI0077 = Times[LLDI0017,LLDI005C]
            tmp5 = Scalars[18] * tmp1;

            //Sub-expression: LLDI0078 = Plus[LLDI0076,LLDI0077]
            tmp4 += tmp5;

            //Sub-expression: LLDI0079 = Times[Rational[1,2],LLDI0078]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI007A = Power[LLDI0079,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI007B = Plus[LLDI006F,LLDI007A]
            tmp0 += tmp4;

            //Sub-expression: LLDI007C = Times[LLDI0004,LLDI000C]
            tmp4 = point.Z * Scalars[7];

            //Sub-expression: LLDI007D = Times[LLDI0010,LLDI005D]
            tmp5 = Scalars[11] * tmp2;

            //Sub-expression: LLDI007E = Times[-1,LLDI0018,LLDI0060]
            var tmp6 = -1 * Scalars[19] * tmp3;

            //Sub-expression: LLDI007F = Plus[LLDI007D,LLDI007E]
            tmp5 += tmp6;

            //Sub-expression: LLDI0080 = Times[Rational[1,2],LLDI007F]
            tmp5 = 0.5 * tmp5;

            //Sub-expression: LLDI0081 = Plus[LLDI007C,LLDI0080]
            tmp4 += tmp5;

            //Sub-expression: LLDI0082 = Power[LLDI0081,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0083 = Plus[LLDI007B,LLDI0082]
            tmp0 += tmp4;

            //Sub-expression: LLDI0084 = Times[2,LLDI0002,LLDI000A]
            tmp4 = 2 * point.X * Scalars[5];

            //Sub-expression: LLDI0085 = Times[2,LLDI0003,LLDI000B]
            tmp5 = 2 * point.Y * Scalars[6];

            //Sub-expression: LLDI0086 = Plus[LLDI0084,LLDI0085]
            tmp4 += tmp5;

            //Sub-expression: LLDI0087 = Plus[LLDI0086,LLDI0011]
            tmp4 += Scalars[12];

            //Sub-expression: LLDI0088 = Plus[LLDI0087,LLDI0019]
            tmp4 += Scalars[20];

            //Sub-expression: LLDI0089 = Times[-1,LLDI0011,LLDI005C]
            tmp5 = -1 * Scalars[12] * tmp1;

            //Sub-expression: LLDI008A = Plus[LLDI0088,LLDI0089]
            tmp4 += tmp5;

            //Sub-expression: LLDI008B = Times[LLDI0019,LLDI005C]
            tmp5 = Scalars[20] * tmp1;

            //Sub-expression: LLDI008C = Plus[LLDI008A,LLDI008B]
            tmp4 += tmp5;

            //Sub-expression: LLDI008D = Times[Rational[1,2],LLDI008C]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI008E = Power[LLDI008D,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI008F = Plus[LLDI0083,LLDI008E]
            tmp0 += tmp4;

            //Sub-expression: LLDI0090 = Times[-2,LLDI0003,LLDI000C]
            tmp4 = -2 * point.Y * Scalars[7];

            //Sub-expression: LLDI0091 = Times[LLDI0012,LLDI005D]
            tmp5 = Scalars[13] * tmp2;

            //Sub-expression: LLDI0092 = Plus[LLDI0090,LLDI0091]
            tmp4 += tmp5;

            //Sub-expression: LLDI0093 = Times[-1,LLDI001A,LLDI0060]
            tmp5 = -1 * Scalars[21] * tmp3;

            //Sub-expression: LLDI0094 = Plus[LLDI0092,LLDI0093]
            tmp4 += tmp5;

            //Sub-expression: LLDI0095 = Times[Rational[1,2],LLDI0094]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI0096 = Power[LLDI0095,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0097 = Plus[LLDI008F,LLDI0096]
            tmp0 += tmp4;

            //Sub-expression: LLDI0098 = Times[LLDI0002,LLDI000C]
            tmp4 = point.X * Scalars[7];

            //Sub-expression: LLDI0099 = Times[LLDI0013,LLDI005D]
            tmp5 = Scalars[14] * tmp2;

            //Sub-expression: LLDI009A = Times[-1,LLDI001B,LLDI0060]
            tmp6 = -1 * Scalars[22] * tmp3;

            //Sub-expression: LLDI009B = Plus[LLDI0099,LLDI009A]
            tmp5 += tmp6;

            //Sub-expression: LLDI009C = Times[Rational[1,2],LLDI009B]
            tmp5 = 0.5 * tmp5;

            //Sub-expression: LLDI009D = Plus[LLDI0098,LLDI009C]
            tmp4 += tmp5;

            //Sub-expression: LLDI009E = Power[LLDI009D,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI009F = Plus[LLDI0097,LLDI009E]
            tmp0 += tmp4;

            //Sub-expression: LLDI00A0 = Plus[LLDI0014,LLDI001C]
            tmp4 = Scalars[15] + Scalars[23];

            //Sub-expression: LLDI00A1 = Times[-1,LLDI0014,LLDI005C]
            tmp5 = -1 * Scalars[15] * tmp1;

            //Sub-expression: LLDI00A2 = Plus[LLDI00A0,LLDI00A1]
            tmp4 += tmp5;

            //Sub-expression: LLDI00A3 = Times[LLDI001C,LLDI005C]
            tmp5 = Scalars[23] * tmp1;

            //Sub-expression: LLDI00A4 = Plus[LLDI00A2,LLDI00A3]
            tmp4 += tmp5;

            //Sub-expression: LLDI00A5 = Times[Rational[1,2],LLDI00A4]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI00A6 = Power[LLDI00A5,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00A7 = Plus[LLDI009F,LLDI00A6]
            tmp0 += tmp4;

            //Sub-expression: LLDI00A8 = Times[LLDI0002,LLDI000E]
            tmp4 = point.X * Scalars[9];

            //Sub-expression: LLDI00A9 = Times[LLDI0003,LLDI000F]
            tmp5 = point.Y * Scalars[10];

            //Sub-expression: LLDI00AA = Plus[LLDI00A8,LLDI00A9]
            tmp4 += tmp5;

            //Sub-expression: LLDI00AB = Times[LLDI0004,LLDI0011]
            tmp5 = point.Z * Scalars[12];

            //Sub-expression: LLDI00AC = Plus[LLDI00AA,LLDI00AB]
            tmp4 += tmp5;

            //Sub-expression: LLDI00AD = Times[Rational[1,2],LLDI001D,LLDI0060]
            tmp5 = 0.5 * Scalars[24] * tmp3;

            //Sub-expression: LLDI00AE = Plus[LLDI00AC,LLDI00AD]
            tmp4 += tmp5;

            //Sub-expression: LLDI00AF = Power[LLDI00AE,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00B0 = Plus[LLDI00A7,LLDI00AF]
            tmp0 += tmp4;

            //Sub-expression: LLDI00B1 = Times[-1,LLDI0003,LLDI0010]
            tmp4 = -1 * point.Y * Scalars[11];

            //Sub-expression: LLDI00B2 = Times[-1,LLDI0004,LLDI0012]
            tmp5 = -1 * point.Z * Scalars[13];

            //Sub-expression: LLDI00B3 = Plus[LLDI00B1,LLDI00B2]
            tmp4 += tmp5;

            //Sub-expression: LLDI00B4 = Times[Rational[-1,2],LLDI001E,LLDI0060]
            tmp5 = -0.5 * Scalars[25] * tmp3;

            //Sub-expression: LLDI00B5 = Plus[LLDI00B3,LLDI00B4]
            tmp4 += tmp5;

            //Sub-expression: LLDI00B6 = Power[LLDI00B5,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00B7 = Plus[LLDI00B0,LLDI00B6]
            tmp0 += tmp4;

            //Sub-expression: LLDI00B8 = Times[LLDI0002,LLDI0010]
            tmp4 = point.X * Scalars[11];

            //Sub-expression: LLDI00B9 = Times[-1,LLDI0004,LLDI0013]
            tmp5 = -1 * point.Z * Scalars[14];

            //Sub-expression: LLDI00BA = Plus[LLDI00B8,LLDI00B9]
            tmp4 += tmp5;

            //Sub-expression: LLDI00BB = Times[Rational[-1,2],LLDI001F,LLDI0060]
            tmp5 = -0.5 * Scalars[26] * tmp3;

            //Sub-expression: LLDI00BC = Plus[LLDI00BA,LLDI00BB]
            tmp4 += tmp5;

            //Sub-expression: LLDI00BD = Power[LLDI00BC,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00BE = Plus[LLDI00B7,LLDI00BD]
            tmp0 += tmp4;

            //Sub-expression: LLDI00BF = Times[LLDI0004,LLDI0014]
            tmp4 = point.Z * Scalars[15];

            //Sub-expression: LLDI00C0 = Times[Rational[1,2],LLDI0020,LLDI0060]
            tmp5 = 0.5 * Scalars[27] * tmp3;

            //Sub-expression: LLDI00C1 = Plus[LLDI00BF,LLDI00C0]
            tmp4 += tmp5;

            //Sub-expression: LLDI00C2 = Power[LLDI00C1,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00C3 = Plus[LLDI00BE,LLDI00C2]
            tmp0 += tmp4;

            //Sub-expression: LLDI00C4 = Times[LLDI0002,LLDI0012]
            tmp4 = point.X * Scalars[13];

            //Sub-expression: LLDI00C5 = Times[LLDI0003,LLDI0013]
            tmp5 = point.Y * Scalars[14];

            //Sub-expression: LLDI00C6 = Plus[LLDI00C4,LLDI00C5]
            tmp4 += tmp5;

            //Sub-expression: LLDI00C7 = Times[Rational[-1,2],LLDI0021,LLDI0060]
            tmp5 = -0.5 * Scalars[28] * tmp3;

            //Sub-expression: LLDI00C8 = Plus[LLDI00C6,LLDI00C7]
            tmp4 += tmp5;

            //Sub-expression: LLDI00C9 = Power[LLDI00C8,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00CA = Plus[LLDI00C3,LLDI00C9]
            tmp0 += tmp4;

            //Sub-expression: LLDI00CB = Times[-2,LLDI0003,LLDI0014]
            tmp4 = -2 * point.Y * Scalars[15];

            //Sub-expression: LLDI00CC = Plus[LLDI00CB,LLDI0022]
            tmp4 += Scalars[29];

            //Sub-expression: LLDI00CD = Times[LLDI0022,LLDI005C]
            tmp5 = Scalars[29] * tmp1;

            //Sub-expression: LLDI00CE = Plus[LLDI00CC,LLDI00CD]
            tmp4 += tmp5;

            //Sub-expression: LLDI00CF = Times[Rational[1,2],LLDI00CE]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI00D0 = Power[LLDI00CF,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00D1 = Plus[LLDI00CA,LLDI00D0]
            tmp0 += tmp4;

            //Sub-expression: LLDI00D2 = Times[LLDI0002,LLDI0014]
            tmp4 = point.X * Scalars[15];

            //Sub-expression: LLDI00D3 = Times[Rational[1,2],LLDI0023,LLDI0060]
            tmp5 = 0.5 * Scalars[30] * tmp3;

            //Sub-expression: LLDI00D4 = Plus[LLDI00D2,LLDI00D3]
            tmp4 += tmp5;

            //Sub-expression: LLDI00D5 = Power[LLDI00D4,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI00D6 = Plus[LLDI00D1,LLDI00D5]
            tmp0 += tmp4;

            //Sub-expression: LLDI00D7 = Times[Rational[-1,2],LLDI0024,LLDI0060]
            tmp3 = -0.5 * Scalars[31] * tmp3;

            //Sub-expression: LLDI00D8 = Power[LLDI00D7,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI00D9 = Plus[LLDI00D6,LLDI00D8]
            tmp0 += tmp3;

            //Sub-expression: LLDI00DA = Times[LLDI0002,LLDI0016]
            tmp3 = point.X * Scalars[17];

            //Sub-expression: LLDI00DB = Times[LLDI0003,LLDI0017]
            tmp4 = point.Y * Scalars[18];

            //Sub-expression: LLDI00DC = Plus[LLDI00DA,LLDI00DB]
            tmp3 += tmp4;

            //Sub-expression: LLDI00DD = Times[LLDI0004,LLDI0019]
            tmp4 = point.Z * Scalars[20];

            //Sub-expression: LLDI00DE = Plus[LLDI00DC,LLDI00DD]
            tmp3 += tmp4;

            //Sub-expression: LLDI00DF = Times[Rational[1,2],LLDI001D,LLDI005D]
            tmp4 = 0.5 * Scalars[24] * tmp2;

            //Sub-expression: LLDI00E0 = Plus[LLDI00DE,LLDI00DF]
            tmp3 += tmp4;

            //Sub-expression: LLDI00E1 = Power[LLDI00E0,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI00E2 = Times[-1,LLDI00E1]
            tmp3 = -tmp3;

            //Sub-expression: LLDI00E3 = Plus[LLDI00D9,LLDI00E2]
            tmp0 += tmp3;

            //Sub-expression: LLDI00E4 = Times[-2,LLDI0003,LLDI0018]
            tmp3 = -2 * point.Y * Scalars[19];

            //Sub-expression: LLDI00E5 = Times[-2,LLDI0004,LLDI001A]
            tmp4 = -2 * point.Z * Scalars[21];

            //Sub-expression: LLDI00E6 = Plus[LLDI00E4,LLDI00E5]
            tmp3 += tmp4;

            //Sub-expression: LLDI00E7 = Plus[LLDI00E6,LLDI001E]
            tmp3 += Scalars[25];

            //Sub-expression: LLDI00E8 = Times[-1,LLDI001E,LLDI005C]
            tmp1 = -1 * Scalars[25] * tmp1;

            //Sub-expression: LLDI00E9 = Plus[LLDI00E7,LLDI00E8]
            tmp1 = tmp3 + tmp1;

            //Sub-expression: LLDI00EA = Times[Rational[1,2],LLDI00E9]
            tmp1 = 0.5 * tmp1;

            //Sub-expression: LLDI00EB = Power[LLDI00EA,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI00EC = Times[-1,LLDI00EB]
            tmp1 = -tmp1;

            //Sub-expression: LLDI00ED = Plus[LLDI00E3,LLDI00EC]
            tmp0 += tmp1;

            //Sub-expression: LLDI00EE = Times[LLDI0002,LLDI0018]
            tmp1 = point.X * Scalars[19];

            //Sub-expression: LLDI00EF = Times[-1,LLDI0004,LLDI001B]
            tmp3 = -1 * point.Z * Scalars[22];

            //Sub-expression: LLDI00F0 = Plus[LLDI00EE,LLDI00EF]
            tmp1 += tmp3;

            //Sub-expression: LLDI00F1 = Times[Rational[-1,2],LLDI001F,LLDI005D]
            tmp3 = -0.5 * Scalars[26] * tmp2;

            //Sub-expression: LLDI00F2 = Plus[LLDI00F0,LLDI00F1]
            tmp1 += tmp3;

            //Sub-expression: LLDI00F3 = Power[LLDI00F2,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI00F4 = Times[-1,LLDI00F3]
            tmp1 = -tmp1;

            //Sub-expression: LLDI00F5 = Plus[LLDI00ED,LLDI00F4]
            tmp0 += tmp1;

            //Sub-expression: LLDI00F6 = Times[LLDI0004,LLDI001C]
            tmp1 = point.Z * Scalars[23];

            //Sub-expression: LLDI00F7 = Times[Rational[1,2],LLDI0020,LLDI005D]
            tmp3 = 0.5 * Scalars[27] * tmp2;

            //Sub-expression: LLDI00F8 = Plus[LLDI00F6,LLDI00F7]
            tmp1 += tmp3;

            //Sub-expression: LLDI00F9 = Power[LLDI00F8,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI00FA = Times[-1,LLDI00F9]
            tmp1 = -tmp1;

            //Sub-expression: LLDI00FB = Plus[LLDI00F5,LLDI00FA]
            tmp0 += tmp1;

            //Sub-expression: LLDI00FC = Times[LLDI0002,LLDI001A]
            tmp1 = point.X * Scalars[21];

            //Sub-expression: LLDI00FD = Times[LLDI0003,LLDI001B]
            tmp3 = point.Y * Scalars[22];

            //Sub-expression: LLDI00FE = Plus[LLDI00FC,LLDI00FD]
            tmp1 += tmp3;

            //Sub-expression: LLDI00FF = Times[Rational[-1,2],LLDI0021,LLDI005D]
            tmp3 = -0.5 * Scalars[28] * tmp2;

            //Sub-expression: LLDI0100 = Plus[LLDI00FE,LLDI00FF]
            tmp1 += tmp3;

            //Sub-expression: LLDI0101 = Power[LLDI0100,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0102 = Times[-1,LLDI0101]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0103 = Plus[LLDI00FB,LLDI0102]
            tmp0 += tmp1;

            //Sub-expression: LLDI0104 = Times[-1,LLDI0003,LLDI001C]
            tmp1 = -1 * point.Y * Scalars[23];

            //Sub-expression: LLDI0105 = Times[Rational[1,2],LLDI0022,LLDI005D]
            tmp3 = 0.5 * Scalars[29] * tmp2;

            //Sub-expression: LLDI0106 = Plus[LLDI0104,LLDI0105]
            tmp1 += tmp3;

            //Sub-expression: LLDI0107 = Power[LLDI0106,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0108 = Times[-1,LLDI0107]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0109 = Plus[LLDI0103,LLDI0108]
            tmp0 += tmp1;

            //Sub-expression: LLDI010A = Times[LLDI0002,LLDI001C]
            tmp1 = point.X * Scalars[23];

            //Sub-expression: LLDI010B = Times[Rational[1,2],LLDI0023,LLDI005D]
            tmp3 = 0.5 * Scalars[30] * tmp2;

            //Sub-expression: LLDI010C = Plus[LLDI010A,LLDI010B]
            tmp1 += tmp3;

            //Sub-expression: LLDI010D = Power[LLDI010C,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI010E = Times[-1,LLDI010D]
            tmp1 = -tmp1;

            //Sub-expression: LLDI010F = Plus[LLDI0109,LLDI010E]
            tmp0 += tmp1;

            //Sub-expression: LLDI0110 = Times[Rational[-1,2],LLDI0024,LLDI005D]
            tmp1 = -0.5 * Scalars[31] * tmp2;

            //Sub-expression: LLDI0111 = Power[LLDI0110,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0112 = Times[-1,LLDI0111]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0113 = Plus[LLDI010F,LLDI0112]
            tmp0 += tmp1;

            //Sub-expression: LLDI0114 = Times[LLDI0002,LLDI001E]
            tmp1 = point.X * Scalars[25];

            //Sub-expression: LLDI0115 = Times[LLDI0003,LLDI001F]
            tmp2 = point.Y * Scalars[26];

            //Sub-expression: LLDI0116 = Plus[LLDI0114,LLDI0115]
            tmp1 += tmp2;

            //Sub-expression: LLDI0117 = Times[LLDI0004,LLDI0021]
            tmp2 = point.Z * Scalars[28];

            //Sub-expression: LLDI0118 = Plus[LLDI0116,LLDI0117]
            tmp1 += tmp2;

            //Sub-expression: LLDI0119 = Power[LLDI0118,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI011A = Times[-1,LLDI0119]
            tmp1 = -tmp1;

            //Sub-expression: LLDI011B = Plus[LLDI0113,LLDI011A]
            tmp0 += tmp1;

            //Sub-expression: LLDI011C = Times[-1,LLDI0003,LLDI0020]
            tmp1 = -1 * point.Y * Scalars[27];

            //Sub-expression: LLDI011D = Times[-1,LLDI0004,LLDI0022]
            tmp2 = -1 * point.Z * Scalars[29];

            //Sub-expression: LLDI011E = Plus[LLDI011C,LLDI011D]
            tmp1 += tmp2;

            //Sub-expression: LLDI011F = Power[LLDI011E,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0120 = Times[-1,LLDI011F]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0121 = Plus[LLDI011B,LLDI0120]
            tmp0 += tmp1;

            //Sub-expression: LLDI0122 = Times[LLDI0002,LLDI0020]
            tmp1 = point.X * Scalars[27];

            //Sub-expression: LLDI0123 = Times[-1,LLDI0004,LLDI0023]
            tmp2 = -1 * point.Z * Scalars[30];

            //Sub-expression: LLDI0124 = Plus[LLDI0122,LLDI0123]
            tmp1 += tmp2;

            //Sub-expression: LLDI0125 = Power[LLDI0124,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0126 = Times[-1,LLDI0125]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0127 = Plus[LLDI0121,LLDI0126]
            tmp0 += tmp1;

            //Sub-expression: LLDI0128 = Times[LLDI0004,LLDI0024]
            tmp1 = point.Z * Scalars[31];

            //Sub-expression: LLDI0129 = Power[LLDI0128,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI012A = Times[-1,LLDI0129]
            tmp1 = -tmp1;

            //Sub-expression: LLDI012B = Plus[LLDI0127,LLDI012A]
            tmp0 += tmp1;

            //Sub-expression: LLDI012C = Times[LLDI0002,LLDI0022]
            tmp1 = point.X * Scalars[29];

            //Sub-expression: LLDI012D = Times[LLDI0003,LLDI0023]
            tmp2 = point.Y * Scalars[30];

            //Sub-expression: LLDI012E = Plus[LLDI012C,LLDI012D]
            tmp1 += tmp2;

            //Sub-expression: LLDI012F = Power[LLDI012E,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0130 = Times[-1,LLDI012F]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0131 = Plus[LLDI012B,LLDI0130]
            tmp0 += tmp1;

            //Sub-expression: LLDI0132 = Times[-1,LLDI0003,LLDI0024]
            tmp1 = -1 * point.Y * Scalars[31];

            //Sub-expression: LLDI0133 = Power[LLDI0132,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0134 = Times[-1,LLDI0133]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0135 = Plus[LLDI0131,LLDI0134]
            tmp0 += tmp1;

            //Sub-expression: LLDI0136 = Times[LLDI0002,LLDI0024]
            tmp1 = point.X * Scalars[31];

            //Sub-expression: LLDI0137 = Power[LLDI0136,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI0138 = Times[-1,LLDI0137]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI0135,LLDI0138]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T13:44:16.5231869+02:00

            return sdf;
        }


        private Float64Tuple3D GetOpnsNormal(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T13:48:25.0433393+02:00
            //Macro: main.cga5d.GetNormalOpns
            //Input Variables: 35 used, 1 not used, 36 total.
            //Temp Variables: 779 sub-expressions, 0 generated temps, 779 total.
            //Target Temp Variables: 77 total.
            //Output Variables: 4 total.
            //Computations: 1.19157088122605 average, 933 total.
            //Memory Reads: 1.72286079182631 average, 1349 total.
            //Memory Writes: 783 total.
            //
            //Macro Binding Data: 
            //   result.d1 = variable: var d1
            //   result.d2 = variable: var d2
            //   result.d3 = variable: var d3
            //   result.d4 = variable: var d4
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   distanceDelta = variable: DistanceDelta
            //   mv.#E0# = variable: Scalars[0]
            //   mv.#e1# = variable: Scalars[1]
            //   mv.#e2# = variable: Scalars[2]
            //   mv.#e1^e2# = variable: Scalars[3]
            //   mv.#e3# = variable: Scalars[4]
            //   mv.#e1^e3# = variable: Scalars[5]
            //   mv.#e2^e3# = variable: Scalars[6]
            //   mv.#e1^e2^e3# = variable: Scalars[7]
            //   mv.#ep# = variable: Scalars[8]
            //   mv.#e1^ep# = variable: Scalars[9]
            //   mv.#e2^ep# = variable: Scalars[10]
            //   mv.#e1^e2^ep# = variable: Scalars[11]
            //   mv.#e3^ep# = variable: Scalars[12]
            //   mv.#e1^e3^ep# = variable: Scalars[13]
            //   mv.#e2^e3^ep# = variable: Scalars[14]
            //   mv.#e1^e2^e3^ep# = variable: Scalars[15]
            //   mv.#en# = variable: Scalars[16]
            //   mv.#e1^en# = variable: Scalars[17]
            //   mv.#e2^en# = variable: Scalars[18]
            //   mv.#e1^e2^en# = variable: Scalars[19]
            //   mv.#e3^en# = variable: Scalars[20]
            //   mv.#e1^e3^en# = variable: Scalars[21]
            //   mv.#e2^e3^en# = variable: Scalars[22]
            //   mv.#e1^e2^e3^en# = variable: Scalars[23]
            //   mv.#ep^en# = variable: Scalars[24]
            //   mv.#e1^ep^en# = variable: Scalars[25]
            //   mv.#e2^ep^en# = variable: Scalars[26]
            //   mv.#e1^e2^ep^en# = variable: Scalars[27]
            //   mv.#e3^ep^en# = variable: Scalars[28]
            //   mv.#e1^e3^ep^en# = variable: Scalars[29]
            //   mv.#e2^e3^ep^en# = variable: Scalars[30]
            //   mv.#e1^e2^e3^ep^en# = variable: Scalars[31]

            double tmp1;
            double tmp2;
            double tmp3;
            double tmp5;
            double tmp6;
            double tmp7;
            double tmp8;
            double tmp9;
            double tmp10;
            double tmp12;
            double tmp13;
            double tmp14;
            double tmp15;
            double tmp17;
            double tmp18;
            double tmp19;
            double tmp20;
            double tmp21;
            double tmp22;
            double tmp24;
            double tmp26;
            double tmp27;
            double tmp28;
            double tmp30;
            double tmp32;
            double tmp33;
            double tmp34;
            double tmp35;
            double tmp37;
            double tmp38;
            double tmp41;
            double tmp42;
            double tmp43;
            double tmp44;
            double tmp45;
            double tmp47;
            double tmp48;
            double tmp49;
            double tmp51;
            double tmp52;
            double tmp54;
            double tmp55;
            double tmp56;
            double tmp57;
            double tmp60;
            double tmp61;
            double tmp62;
            double tmp63;
            double tmp64;
            double tmp65;
            double tmp66;
            double tmp67;
            double tmp68;
            double tmp69;
            double tmp70;
            double tmp71;
            double tmp72;
            double tmp73;
            double tmp74;

            //Sub-expression: LLDI0156 = Plus[LLDI0005,LLDI0008]
            var tmp0 = point.X + SdfDistanceDelta;

            //Sub-expression: LLDI0157 = Times[LLDI0009,LLDI0156]
            tmp1 = Scalars[0] * tmp0;

            //Sub-expression: LLDI0158 = Power[LLDI0157,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI015E = Times[LLDI000B,LLDI0156]
            tmp2 = Scalars[2] * tmp0;

            //Sub-expression: LLDI0167 = Times[LLDI000D,LLDI0156]
            tmp3 = Scalars[4] * tmp0;

            //Sub-expression: LLDI0171 = Times[LLDI000F,LLDI0156]
            var tmp4 = Scalars[6] * tmp0;

            //Sub-expression: LLDI0178 = Power[LLDI0156,2]
            tmp5 = tmp0 * tmp0;

            //Sub-expression: LLDI0181 = Times[LLDI0011,LLDI0156]
            tmp6 = Scalars[8] * tmp0;

            //Sub-expression: LLDI018B = Times[LLDI0013,LLDI0156]
            tmp7 = Scalars[10] * tmp0;

            //Sub-expression: LLDI0197 = Times[LLDI0015,LLDI0156]
            tmp8 = Scalars[12] * tmp0;

            //Sub-expression: LLDI01A5 = Times[LLDI0017,LLDI0156]
            tmp9 = Scalars[14] * tmp0;

            //Sub-expression: LLDI01B3 = Times[LLDI0019,LLDI0156]
            tmp10 = Scalars[16] * tmp0;

            //Sub-expression: LLDI01BF = Times[LLDI001B,LLDI0156]
            var tmp11 = Scalars[18] * tmp0;

            //Sub-expression: LLDI01CD = Times[LLDI001D,LLDI0156]
            tmp12 = Scalars[20] * tmp0;

            //Sub-expression: LLDI01DD = Times[LLDI001F,LLDI0156]
            tmp13 = Scalars[22] * tmp0;

            //Sub-expression: LLDI01EE = Plus[LLDI0012,LLDI001A]
            tmp14 = Scalars[9] + Scalars[17];

            //Sub-expression: LLDI01EF = Times[2,LLDI0021,LLDI0156]
            tmp15 = 2 * Scalars[24] * tmp0;

            //Sub-expression: LLDI01F0 = Plus[LLDI01EE,LLDI01EF]
            tmp15 = tmp14 + tmp15;

            //Sub-expression: LLDI01F9 = Plus[LLDI0013,LLDI001B]
            var tmp16 = Scalars[10] + Scalars[18];

            //Sub-expression: LLDI0204 = Times[LLDI0023,LLDI0156]
            tmp17 = Scalars[26] * tmp0;

            //Sub-expression: LLDI020E = Plus[LLDI0015,LLDI001D]
            tmp18 = Scalars[12] + Scalars[20];

            //Sub-expression: LLDI0219 = Times[LLDI0025,LLDI0156]
            tmp19 = Scalars[28] * tmp0;

            //Sub-expression: LLDI022D = Plus[LLDI0018,LLDI0020]
            tmp20 = Scalars[15] + Scalars[23];

            //Sub-expression: LLDI022E = Times[2,LLDI0027,LLDI0156]
            tmp0 = 2 * Scalars[30] * tmp0;

            //Sub-expression: LLDI022F = Plus[LLDI022D,LLDI022E]
            tmp0 = tmp20 + tmp0;

            //Sub-expression: LLDI0243 = Plus[LLDI0007,LLDI0008]
            tmp21 = point.Z + SdfDistanceDelta;

            //Sub-expression: LLDI0244 = Times[LLDI0009,LLDI0243]
            tmp22 = Scalars[0] * tmp21;

            //Sub-expression: LLDI0245 = Power[LLDI0244,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0248 = Times[-1,LLDI000A,LLDI0243]
            var tmp23 = -1 * Scalars[1] * tmp21;

            //Sub-expression: LLDI024C = Times[-1,LLDI000B,LLDI0243]
            tmp24 = -1 * Scalars[2] * tmp21;

            //Sub-expression: LLDI0252 = Times[LLDI000C,LLDI0243]
            var tmp25 = Scalars[3] * tmp21;

            //Sub-expression: LLDI0258 = Power[LLDI0243,2]
            tmp26 = tmp21 * tmp21;

            //Sub-expression: LLDI026D = Times[LLDI0011,LLDI0243]
            tmp27 = Scalars[8] * tmp21;

            //Sub-expression: LLDI0273 = Times[-1,LLDI0012,LLDI0243]
            tmp28 = -1 * Scalars[9] * tmp21;

            //Sub-expression: LLDI0279 = Times[-1,LLDI0013,LLDI0243]
            var tmp29 = -1 * Scalars[10] * tmp21;

            //Sub-expression: LLDI0281 = Times[LLDI0014,LLDI0243]
            tmp30 = Scalars[11] * tmp21;

            //Sub-expression: LLDI029E = Times[LLDI0019,LLDI0243]
            var tmp31 = Scalars[16] * tmp21;

            //Sub-expression: LLDI02A5 = Times[-1,LLDI001A,LLDI0243]
            tmp32 = -1 * Scalars[17] * tmp21;

            //Sub-expression: LLDI02AC = Times[-1,LLDI001B,LLDI0243]
            tmp33 = -1 * Scalars[18] * tmp21;

            //Sub-expression: LLDI02B5 = Times[LLDI001C,LLDI0243]
            tmp34 = Scalars[19] * tmp21;

            //Sub-expression: LLDI02DE = Times[2,LLDI0021,LLDI0243]
            tmp35 = 2 * Scalars[24] * tmp21;

            //Sub-expression: LLDI02DF = Plus[LLDI020E,LLDI02DE]
            tmp35 = tmp18 + tmp35;

            //Sub-expression: LLDI02E9 = Times[-1,LLDI0022,LLDI0243]
            var tmp36 = -1 * Scalars[25] * tmp21;

            //Sub-expression: LLDI02F2 = Times[-1,LLDI0023,LLDI0243]
            tmp37 = -1 * Scalars[26] * tmp21;

            //Sub-expression: LLDI02FE = Times[2,LLDI0024,LLDI0243]
            tmp21 = 2 * Scalars[27] * tmp21;

            //Sub-expression: LLDI0307 = Plus[LLDI0006,LLDI0008]
            tmp38 = point.Y + SdfDistanceDelta;

            //Sub-expression: LLDI0308 = Times[LLDI0009,LLDI0307]
            var tmp39 = Scalars[0] * tmp38;

            //Sub-expression: LLDI0309 = Power[LLDI0308,2]
            tmp39 *= tmp39;

            //Sub-expression: LLDI030B = Times[-1,LLDI000A,LLDI0307]
            var tmp40 = -1 * Scalars[1] * tmp38;

            //Sub-expression: LLDI0313 = Times[LLDI000D,LLDI0307]
            tmp41 = Scalars[4] * tmp38;

            //Sub-expression: LLDI0317 = Times[-1,LLDI000E,LLDI0307]
            tmp42 = -1 * Scalars[5] * tmp38;

            //Sub-expression: LLDI031C = Power[LLDI0307,2]
            tmp43 = tmp38 * tmp38;

            //Sub-expression: LLDI0327 = Times[LLDI0011,LLDI0307]
            tmp44 = Scalars[8] * tmp38;

            //Sub-expression: LLDI032C = Times[-1,LLDI0012,LLDI0307]
            tmp45 = -1 * Scalars[9] * tmp38;

            //Sub-expression: LLDI033B = Times[LLDI0015,LLDI0307]
            var tmp46 = Scalars[12] * tmp38;

            //Sub-expression: LLDI0341 = Times[-1,LLDI0016,LLDI0307]
            tmp47 = -1 * Scalars[13] * tmp38;

            //Sub-expression: LLDI0352 = Times[LLDI0019,LLDI0307]
            tmp48 = Scalars[16] * tmp38;

            //Sub-expression: LLDI0358 = Times[-1,LLDI001A,LLDI0307]
            tmp49 = -1 * Scalars[17] * tmp38;

            //Sub-expression: LLDI036A = Times[LLDI001D,LLDI0307]
            var tmp50 = Scalars[20] * tmp38;

            //Sub-expression: LLDI0371 = Times[-1,LLDI001E,LLDI0307]
            tmp51 = -1 * Scalars[21] * tmp38;

            //Sub-expression: LLDI0388 = Times[2,LLDI0021,LLDI0307]
            tmp52 = 2 * Scalars[24] * tmp38;

            //Sub-expression: LLDI0389 = Plus[LLDI01F9,LLDI0388]
            tmp52 = tmp16 + tmp52;

            //Sub-expression: LLDI0392 = Times[-1,LLDI0022,LLDI0307]
            var tmp53 = -1 * Scalars[25] * tmp38;

            //Sub-expression: LLDI03AB = Times[LLDI0025,LLDI0307]
            tmp54 = Scalars[28] * tmp38;

            //Sub-expression: LLDI03B4 = Times[-2,LLDI0026,LLDI0307]
            tmp38 = -2 * Scalars[29] * tmp38;

            //Sub-expression: LLDI03BE = Plus[LLDI0158,LLDI0309]
            tmp55 = tmp1 + tmp39;

            //Sub-expression: LLDI03BF = Plus[LLDI015E,LLDI030B]
            tmp56 = tmp2 + tmp40;

            //Sub-expression: LLDI03C0 = Power[LLDI03BF,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI03C1 = Plus[LLDI03BE,LLDI03C0]
            tmp55 += tmp56;

            //Sub-expression: LLDI03C2 = Plus[LLDI03C1,LLDI0245]
            tmp55 += tmp22;

            //Sub-expression: LLDI03C3 = Plus[LLDI0167,LLDI0248]
            tmp56 = tmp3 + tmp23;

            //Sub-expression: LLDI03C4 = Power[LLDI03C3,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI03C5 = Plus[LLDI03C2,LLDI03C4]
            tmp55 += tmp56;

            //Sub-expression: LLDI03C6 = Plus[LLDI0313,LLDI024C]
            tmp56 = tmp41 + tmp24;

            //Sub-expression: LLDI03C7 = Power[LLDI03C6,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI03C8 = Plus[LLDI03C5,LLDI03C7]
            tmp55 += tmp56;

            //Sub-expression: LLDI03C9 = Plus[LLDI0171,LLDI0317]
            tmp56 = tmp4 + tmp42;

            //Sub-expression: LLDI03CA = Plus[LLDI03C9,LLDI0252]
            tmp56 += tmp25;

            //Sub-expression: LLDI03CB = Power[LLDI03CA,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI03CC = Plus[LLDI03C8,LLDI03CB]
            tmp55 += tmp56;

            //Sub-expression: LLDI03CD = Plus[LLDI0178,LLDI031C]
            tmp56 = tmp5 + tmp43;

            //Sub-expression: LLDI03CE = Plus[LLDI03CD,LLDI0258]
            tmp56 += tmp26;

            //Sub-expression: LLDI03CF = Plus[-1,LLDI03CE]
            tmp57 = -1 + tmp56;

            //Sub-expression: LLDI03D0 = Times[Rational[1,2],LLDI0009,LLDI03CF]
            var tmp58 = 0.5 * Scalars[0] * tmp57;

            //Sub-expression: LLDI03D1 = Power[LLDI03D0,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03D2 = Plus[LLDI03CC,LLDI03D1]
            tmp55 += tmp58;

            //Sub-expression: LLDI03D3 = Times[Rational[-1,2],LLDI000A,LLDI03CF]
            tmp58 = -0.5 * Scalars[1] * tmp57;

            //Sub-expression: LLDI03D4 = Plus[LLDI0181,LLDI03D3]
            tmp58 = tmp6 + tmp58;

            //Sub-expression: LLDI03D5 = Power[LLDI03D4,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03D6 = Plus[LLDI03D2,LLDI03D5]
            tmp55 += tmp58;

            //Sub-expression: LLDI03D7 = Times[Rational[-1,2],LLDI000B,LLDI03CF]
            tmp58 = -0.5 * Scalars[2] * tmp57;

            //Sub-expression: LLDI03D8 = Plus[LLDI0327,LLDI03D7]
            tmp58 = tmp44 + tmp58;

            //Sub-expression: LLDI03D9 = Power[LLDI03D8,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03DA = Plus[LLDI03D6,LLDI03D9]
            tmp55 += tmp58;

            //Sub-expression: LLDI03DB = Plus[LLDI018B,LLDI032C]
            tmp58 = tmp7 + tmp45;

            //Sub-expression: LLDI03DC = Times[Rational[1,2],LLDI000C,LLDI03CF]
            var tmp59 = 0.5 * Scalars[3] * tmp57;

            //Sub-expression: LLDI03DD = Plus[LLDI03DB,LLDI03DC]
            tmp58 += tmp59;

            //Sub-expression: LLDI03DE = Power[LLDI03DD,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03DF = Plus[LLDI03DA,LLDI03DE]
            tmp55 += tmp58;

            //Sub-expression: LLDI03E0 = Times[Rational[-1,2],LLDI000D,LLDI03CF]
            tmp58 = -0.5 * Scalars[4] * tmp57;

            //Sub-expression: LLDI03E1 = Plus[LLDI026D,LLDI03E0]
            tmp58 = tmp27 + tmp58;

            //Sub-expression: LLDI03E2 = Power[LLDI03E1,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03E3 = Plus[LLDI03DF,LLDI03E2]
            tmp55 += tmp58;

            //Sub-expression: LLDI03E4 = Plus[LLDI0197,LLDI0273]
            tmp58 = tmp8 + tmp28;

            //Sub-expression: LLDI03E5 = Times[Rational[1,2],LLDI000E,LLDI03CF]
            tmp59 = 0.5 * Scalars[5] * tmp57;

            //Sub-expression: LLDI03E6 = Plus[LLDI03E4,LLDI03E5]
            tmp58 += tmp59;

            //Sub-expression: LLDI03E7 = Power[LLDI03E6,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03E8 = Plus[LLDI03E3,LLDI03E7]
            tmp55 += tmp58;

            //Sub-expression: LLDI03E9 = Plus[LLDI033B,LLDI0279]
            tmp58 = tmp46 + tmp29;

            //Sub-expression: LLDI03EA = Times[Rational[1,2],LLDI000F,LLDI03CF]
            tmp59 = 0.5 * Scalars[6] * tmp57;

            //Sub-expression: LLDI03EB = Plus[LLDI03E9,LLDI03EA]
            tmp58 += tmp59;

            //Sub-expression: LLDI03EC = Power[LLDI03EB,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03ED = Plus[LLDI03E8,LLDI03EC]
            tmp55 += tmp58;

            //Sub-expression: LLDI03EE = Plus[LLDI01A5,LLDI0341]
            tmp58 = tmp9 + tmp47;

            //Sub-expression: LLDI03EF = Plus[LLDI03EE,LLDI0281]
            tmp58 += tmp30;

            //Sub-expression: LLDI03F0 = Times[Rational[-1,2],LLDI0010,LLDI03CF]
            tmp59 = -0.5 * Scalars[7] * tmp57;

            //Sub-expression: LLDI03F1 = Plus[LLDI03EF,LLDI03F0]
            tmp58 += tmp59;

            //Sub-expression: LLDI03F2 = Power[LLDI03F1,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI03F3 = Plus[LLDI03ED,LLDI03F2]
            tmp55 += tmp58;

            //Sub-expression: LLDI03F4 = Plus[1,LLDI03CE]
            tmp58 = 1 + tmp56;

            //Sub-expression: LLDI03F5 = Times[Rational[1,2],LLDI0009,LLDI03F4]
            tmp59 = 0.5 * Scalars[0] * tmp58;

            //Sub-expression: LLDI03F6 = Power[LLDI03F5,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03F7 = Times[-1,LLDI03F6]
            tmp59 = -tmp59;

            //Sub-expression: LLDI03F8 = Plus[LLDI03F3,LLDI03F7]
            tmp55 += tmp59;

            //Sub-expression: LLDI03F9 = Times[Rational[-1,2],LLDI000A,LLDI03F4]
            tmp59 = -0.5 * Scalars[1] * tmp58;

            //Sub-expression: LLDI03FA = Plus[LLDI01B3,LLDI03F9]
            tmp59 = tmp10 + tmp59;

            //Sub-expression: LLDI03FB = Power[LLDI03FA,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03FC = Times[-1,LLDI03FB]
            tmp59 = -tmp59;

            //Sub-expression: LLDI03FD = Plus[LLDI03F8,LLDI03FC]
            tmp55 += tmp59;

            //Sub-expression: LLDI03FE = Times[Rational[-1,2],LLDI000B,LLDI03F4]
            tmp59 = -0.5 * Scalars[2] * tmp58;

            //Sub-expression: LLDI03FF = Plus[LLDI0352,LLDI03FE]
            tmp59 = tmp48 + tmp59;

            //Sub-expression: LLDI0400 = Power[LLDI03FF,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0401 = Times[-1,LLDI0400]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0402 = Plus[LLDI03FD,LLDI0401]
            tmp55 += tmp59;

            //Sub-expression: LLDI0403 = Plus[LLDI01BF,LLDI0358]
            tmp59 = tmp11 + tmp49;

            //Sub-expression: LLDI0404 = Times[Rational[1,2],LLDI000C,LLDI03F4]
            tmp60 = 0.5 * Scalars[3] * tmp58;

            //Sub-expression: LLDI0405 = Plus[LLDI0403,LLDI0404]
            tmp59 += tmp60;

            //Sub-expression: LLDI0406 = Power[LLDI0405,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0407 = Times[-1,LLDI0406]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0408 = Plus[LLDI0402,LLDI0407]
            tmp55 += tmp59;

            //Sub-expression: LLDI0409 = Times[Rational[-1,2],LLDI000D,LLDI03F4]
            tmp59 = -0.5 * Scalars[4] * tmp58;

            //Sub-expression: LLDI040A = Plus[LLDI029E,LLDI0409]
            tmp59 = tmp31 + tmp59;

            //Sub-expression: LLDI040B = Power[LLDI040A,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI040C = Times[-1,LLDI040B]
            tmp59 = -tmp59;

            //Sub-expression: LLDI040D = Plus[LLDI0408,LLDI040C]
            tmp55 += tmp59;

            //Sub-expression: LLDI040E = Plus[LLDI01CD,LLDI02A5]
            tmp59 = tmp12 + tmp32;

            //Sub-expression: LLDI040F = Times[Rational[1,2],LLDI000E,LLDI03F4]
            tmp60 = 0.5 * Scalars[5] * tmp58;

            //Sub-expression: LLDI0410 = Plus[LLDI040E,LLDI040F]
            tmp59 += tmp60;

            //Sub-expression: LLDI0411 = Power[LLDI0410,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0412 = Times[-1,LLDI0411]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0413 = Plus[LLDI040D,LLDI0412]
            tmp55 += tmp59;

            //Sub-expression: LLDI0414 = Plus[LLDI036A,LLDI02AC]
            tmp59 = tmp50 + tmp33;

            //Sub-expression: LLDI0415 = Times[Rational[1,2],LLDI000F,LLDI03F4]
            tmp60 = 0.5 * Scalars[6] * tmp58;

            //Sub-expression: LLDI0416 = Plus[LLDI0414,LLDI0415]
            tmp59 += tmp60;

            //Sub-expression: LLDI0417 = Power[LLDI0416,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0418 = Times[-1,LLDI0417]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0419 = Plus[LLDI0413,LLDI0418]
            tmp55 += tmp59;

            //Sub-expression: LLDI041A = Plus[LLDI01DD,LLDI0371]
            tmp59 = tmp13 + tmp51;

            //Sub-expression: LLDI041B = Plus[LLDI041A,LLDI02B5]
            tmp59 += tmp34;

            //Sub-expression: LLDI041C = Times[Rational[-1,2],LLDI0010,LLDI03F4]
            tmp60 = -0.5 * Scalars[7] * tmp58;

            //Sub-expression: LLDI041D = Plus[LLDI041B,LLDI041C]
            tmp59 += tmp60;

            //Sub-expression: LLDI041E = Power[LLDI041D,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI041F = Times[-1,LLDI041E]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0420 = Plus[LLDI0419,LLDI041F]
            tmp55 += tmp59;

            //Sub-expression: LLDI0421 = Times[LLDI0019,LLDI03CF]
            tmp59 = Scalars[16] * tmp57;

            //Sub-expression: LLDI0422 = Times[-1,LLDI0011,LLDI03F4]
            tmp60 = -1 * Scalars[8] * tmp58;

            //Sub-expression: LLDI0423 = Plus[LLDI0421,LLDI0422]
            tmp59 += tmp60;

            //Sub-expression: LLDI0424 = Times[Rational[1,2],LLDI0423]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI0425 = Power[LLDI0424,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0426 = Times[-1,LLDI0425]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0427 = Plus[LLDI0420,LLDI0426]
            tmp55 += tmp59;

            //Sub-expression: LLDI0428 = Times[LLDI0012,LLDI03CE]
            tmp59 = Scalars[9] * tmp56;

            //Sub-expression: LLDI0429 = Plus[LLDI01F0,LLDI0428]
            tmp59 = tmp15 + tmp59;

            //Sub-expression: LLDI042A = Times[-1,LLDI001A,LLDI03CE]
            tmp60 = -1 * Scalars[17] * tmp56;

            //Sub-expression: LLDI042B = Plus[LLDI0429,LLDI042A]
            tmp59 += tmp60;

            //Sub-expression: LLDI042C = Times[Rational[1,2],LLDI042B]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI042D = Power[LLDI042C,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI042E = Times[-1,LLDI042D]
            tmp59 = -tmp59;

            //Sub-expression: LLDI042F = Plus[LLDI0427,LLDI042E]
            tmp55 += tmp59;

            //Sub-expression: LLDI0430 = Times[LLDI0013,LLDI03CE]
            tmp59 = Scalars[10] * tmp56;

            //Sub-expression: LLDI0431 = Plus[LLDI0389,LLDI0430]
            tmp59 = tmp52 + tmp59;

            //Sub-expression: LLDI0432 = Times[-1,LLDI001B,LLDI03CE]
            tmp60 = -1 * Scalars[18] * tmp56;

            //Sub-expression: LLDI0433 = Plus[LLDI0431,LLDI0432]
            tmp59 += tmp60;

            //Sub-expression: LLDI0434 = Times[Rational[1,2],LLDI0433]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI0435 = Power[LLDI0434,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0436 = Times[-1,LLDI0435]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0437 = Plus[LLDI042F,LLDI0436]
            tmp55 += tmp59;

            //Sub-expression: LLDI0438 = Plus[LLDI0204,LLDI0392]
            tmp59 = tmp17 + tmp53;

            //Sub-expression: LLDI0439 = Times[Rational[1,2],LLDI001C,LLDI03CF]
            tmp60 = 0.5 * Scalars[19] * tmp57;

            //Sub-expression: LLDI043A = Plus[LLDI0438,LLDI0439]
            tmp59 += tmp60;

            //Sub-expression: LLDI043B = Times[Rational[-1,2],LLDI0014,LLDI03F4]
            tmp60 = -0.5 * Scalars[11] * tmp58;

            //Sub-expression: LLDI043C = Plus[LLDI043A,LLDI043B]
            tmp59 += tmp60;

            //Sub-expression: LLDI043D = Power[LLDI043C,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI043E = Times[-1,LLDI043D]
            tmp59 = -tmp59;

            //Sub-expression: LLDI043F = Plus[LLDI0437,LLDI043E]
            tmp55 += tmp59;

            //Sub-expression: LLDI0440 = Times[LLDI0015,LLDI03CE]
            tmp59 = Scalars[12] * tmp56;

            //Sub-expression: LLDI0441 = Plus[LLDI02DF,LLDI0440]
            tmp59 = tmp35 + tmp59;

            //Sub-expression: LLDI0442 = Times[-1,LLDI001D,LLDI03CE]
            tmp60 = -1 * Scalars[20] * tmp56;

            //Sub-expression: LLDI0443 = Plus[LLDI0441,LLDI0442]
            tmp59 += tmp60;

            //Sub-expression: LLDI0444 = Times[Rational[1,2],LLDI0443]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI0445 = Power[LLDI0444,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0446 = Times[-1,LLDI0445]
            tmp59 = -tmp59;

            //Sub-expression: LLDI0447 = Plus[LLDI043F,LLDI0446]
            tmp55 += tmp59;

            //Sub-expression: LLDI0448 = Plus[LLDI0219,LLDI02E9]
            tmp59 = tmp19 + tmp36;

            //Sub-expression: LLDI0449 = Times[Rational[1,2],LLDI001E,LLDI03CF]
            tmp60 = 0.5 * Scalars[21] * tmp57;

            //Sub-expression: LLDI044A = Plus[LLDI0448,LLDI0449]
            tmp59 += tmp60;

            //Sub-expression: LLDI044B = Times[Rational[-1,2],LLDI0016,LLDI03F4]
            tmp60 = -0.5 * Scalars[13] * tmp58;

            //Sub-expression: LLDI044C = Plus[LLDI044A,LLDI044B]
            tmp59 += tmp60;

            //Sub-expression: LLDI044D = Power[LLDI044C,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI044E = Times[-1,LLDI044D]
            tmp59 = -tmp59;

            //Sub-expression: LLDI044F = Plus[LLDI0447,LLDI044E]
            tmp55 += tmp59;

            //Sub-expression: LLDI0450 = Plus[LLDI03AB,LLDI02F2]
            tmp59 = tmp54 + tmp37;

            //Sub-expression: LLDI0451 = Times[Rational[1,2],LLDI001F,LLDI03CF]
            tmp57 = 0.5 * Scalars[22] * tmp57;

            //Sub-expression: LLDI0452 = Plus[LLDI0450,LLDI0451]
            tmp57 = tmp59 + tmp57;

            //Sub-expression: LLDI0453 = Times[Rational[-1,2],LLDI0017,LLDI03F4]
            tmp58 = -0.5 * Scalars[14] * tmp58;

            //Sub-expression: LLDI0454 = Plus[LLDI0452,LLDI0453]
            tmp57 += tmp58;

            //Sub-expression: LLDI0455 = Power[LLDI0454,2]
            tmp57 *= tmp57;

            //Sub-expression: LLDI0456 = Times[-1,LLDI0455]
            tmp57 = -tmp57;

            //Sub-expression: LLDI0457 = Plus[LLDI044F,LLDI0456]
            tmp55 += tmp57;

            //Sub-expression: LLDI0458 = Plus[LLDI022F,LLDI03B4]
            tmp57 = tmp0 + tmp38;

            //Sub-expression: LLDI0459 = Plus[LLDI0458,LLDI02FE]
            tmp57 += tmp21;

            //Sub-expression: LLDI045A = Times[LLDI0018,LLDI03CE]
            tmp58 = Scalars[15] * tmp56;

            //Sub-expression: LLDI045B = Plus[LLDI0459,LLDI045A]
            tmp57 += tmp58;

            //Sub-expression: LLDI045C = Times[-1,LLDI0020,LLDI03CE]
            tmp56 = -1 * Scalars[23] * tmp56;

            //Sub-expression: LLDI045D = Plus[LLDI045B,LLDI045C]
            tmp56 = tmp57 + tmp56;

            //Sub-expression: LLDI045E = Times[Rational[1,2],LLDI045D]
            tmp56 = 0.5 * tmp56;

            //Sub-expression: LLDI045F = Power[LLDI045E,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0460 = Times[-1,LLDI045F]
            tmp56 = -tmp56;

            //Output: LLDI0004 = Plus[LLDI0457,LLDI0460]
            var d4 = tmp55 + tmp56;

            //Sub-expression: LLDI0159 = Times[-1,LLDI0008]
            tmp55 = -SdfDistanceDelta;

            //Sub-expression: LLDI015A = Plus[LLDI0006,LLDI0159]
            tmp56 = point.Y + tmp55;

            //Sub-expression: LLDI015B = Times[LLDI0009,LLDI015A]
            tmp57 = Scalars[0] * tmp56;

            //Sub-expression: LLDI015C = Power[LLDI015B,2]
            tmp57 *= tmp57;

            //Sub-expression: LLDI015D = Plus[LLDI0158,LLDI015C]
            tmp1 += tmp57;

            //Sub-expression: LLDI015F = Times[-1,LLDI000A,LLDI015A]
            tmp58 = -1 * Scalars[1] * tmp56;

            //Sub-expression: LLDI0160 = Plus[LLDI015E,LLDI015F]
            tmp2 += tmp58;

            //Sub-expression: LLDI0161 = Power[LLDI0160,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI0162 = Plus[LLDI015D,LLDI0161]
            tmp1 += tmp2;

            //Sub-expression: LLDI0163 = Plus[LLDI0007,LLDI0159]
            tmp2 = point.Z + tmp55;

            //Sub-expression: LLDI0164 = Times[LLDI0009,LLDI0163]
            tmp59 = Scalars[0] * tmp2;

            //Sub-expression: LLDI0165 = Power[LLDI0164,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0166 = Plus[LLDI0162,LLDI0165]
            tmp1 += tmp59;

            //Sub-expression: LLDI0168 = Times[-1,LLDI000A,LLDI0163]
            tmp60 = -1 * Scalars[1] * tmp2;

            //Sub-expression: LLDI0169 = Plus[LLDI0167,LLDI0168]
            tmp3 += tmp60;

            //Sub-expression: LLDI016A = Power[LLDI0169,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI016B = Plus[LLDI0166,LLDI016A]
            tmp1 += tmp3;

            //Sub-expression: LLDI016C = Times[LLDI000D,LLDI015A]
            tmp3 = Scalars[4] * tmp56;

            //Sub-expression: LLDI016D = Times[-1,LLDI000B,LLDI0163]
            tmp61 = -1 * Scalars[2] * tmp2;

            //Sub-expression: LLDI016E = Plus[LLDI016C,LLDI016D]
            tmp62 = tmp3 + tmp61;

            //Sub-expression: LLDI016F = Power[LLDI016E,2]
            tmp62 *= tmp62;

            //Sub-expression: LLDI0170 = Plus[LLDI016B,LLDI016F]
            tmp1 += tmp62;

            //Sub-expression: LLDI0172 = Times[-1,LLDI000E,LLDI015A]
            tmp62 = -1 * Scalars[5] * tmp56;

            //Sub-expression: LLDI0173 = Plus[LLDI0171,LLDI0172]
            tmp4 += tmp62;

            //Sub-expression: LLDI0174 = Times[LLDI000C,LLDI0163]
            tmp63 = Scalars[3] * tmp2;

            //Sub-expression: LLDI0175 = Plus[LLDI0173,LLDI0174]
            tmp4 += tmp63;

            //Sub-expression: LLDI0176 = Power[LLDI0175,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0177 = Plus[LLDI0170,LLDI0176]
            tmp1 += tmp4;

            //Sub-expression: LLDI0179 = Power[LLDI015A,2]
            tmp4 = tmp56 * tmp56;

            //Sub-expression: LLDI017A = Plus[LLDI0178,LLDI0179]
            tmp5 += tmp4;

            //Sub-expression: LLDI017B = Power[LLDI0163,2]
            tmp64 = tmp2 * tmp2;

            //Sub-expression: LLDI017C = Plus[LLDI017A,LLDI017B]
            tmp5 += tmp64;

            //Sub-expression: LLDI017D = Plus[-1,LLDI017C]
            tmp65 = -1 + tmp5;

            //Sub-expression: LLDI017E = Times[Rational[1,2],LLDI0009,LLDI017D]
            tmp66 = 0.5 * Scalars[0] * tmp65;

            //Sub-expression: LLDI017F = Power[LLDI017E,2]
            tmp66 *= tmp66;

            //Sub-expression: LLDI0180 = Plus[LLDI0177,LLDI017F]
            tmp1 += tmp66;

            //Sub-expression: LLDI0182 = Times[Rational[-1,2],LLDI000A,LLDI017D]
            tmp66 = -0.5 * Scalars[1] * tmp65;

            //Sub-expression: LLDI0183 = Plus[LLDI0181,LLDI0182]
            tmp6 += tmp66;

            //Sub-expression: LLDI0184 = Power[LLDI0183,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0185 = Plus[LLDI0180,LLDI0184]
            tmp1 += tmp6;

            //Sub-expression: LLDI0186 = Times[LLDI0011,LLDI015A]
            tmp6 = Scalars[8] * tmp56;

            //Sub-expression: LLDI0187 = Times[Rational[-1,2],LLDI000B,LLDI017D]
            tmp66 = -0.5 * Scalars[2] * tmp65;

            //Sub-expression: LLDI0188 = Plus[LLDI0186,LLDI0187]
            tmp66 = tmp6 + tmp66;

            //Sub-expression: LLDI0189 = Power[LLDI0188,2]
            tmp66 *= tmp66;

            //Sub-expression: LLDI018A = Plus[LLDI0185,LLDI0189]
            tmp1 += tmp66;

            //Sub-expression: LLDI018C = Times[-1,LLDI0012,LLDI015A]
            tmp66 = -1 * Scalars[9] * tmp56;

            //Sub-expression: LLDI018D = Plus[LLDI018B,LLDI018C]
            tmp7 += tmp66;

            //Sub-expression: LLDI018E = Times[Rational[1,2],LLDI000C,LLDI017D]
            tmp67 = 0.5 * Scalars[3] * tmp65;

            //Sub-expression: LLDI018F = Plus[LLDI018D,LLDI018E]
            tmp7 += tmp67;

            //Sub-expression: LLDI0190 = Power[LLDI018F,2]
            tmp7 *= tmp7;

            //Sub-expression: LLDI0191 = Plus[LLDI018A,LLDI0190]
            tmp1 += tmp7;

            //Sub-expression: LLDI0192 = Times[LLDI0011,LLDI0163]
            tmp7 = Scalars[8] * tmp2;

            //Sub-expression: LLDI0193 = Times[Rational[-1,2],LLDI000D,LLDI017D]
            tmp67 = -0.5 * Scalars[4] * tmp65;

            //Sub-expression: LLDI0194 = Plus[LLDI0192,LLDI0193]
            tmp67 = tmp7 + tmp67;

            //Sub-expression: LLDI0195 = Power[LLDI0194,2]
            tmp67 *= tmp67;

            //Sub-expression: LLDI0196 = Plus[LLDI0191,LLDI0195]
            tmp1 += tmp67;

            //Sub-expression: LLDI0198 = Times[-1,LLDI0012,LLDI0163]
            tmp67 = -1 * Scalars[9] * tmp2;

            //Sub-expression: LLDI0199 = Plus[LLDI0197,LLDI0198]
            tmp8 += tmp67;

            //Sub-expression: LLDI019A = Times[Rational[1,2],LLDI000E,LLDI017D]
            tmp68 = 0.5 * Scalars[5] * tmp65;

            //Sub-expression: LLDI019B = Plus[LLDI0199,LLDI019A]
            tmp8 += tmp68;

            //Sub-expression: LLDI019C = Power[LLDI019B,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI019D = Plus[LLDI0196,LLDI019C]
            tmp1 += tmp8;

            //Sub-expression: LLDI019E = Times[LLDI0015,LLDI015A]
            tmp8 = Scalars[12] * tmp56;

            //Sub-expression: LLDI019F = Times[-1,LLDI0013,LLDI0163]
            tmp68 = -1 * Scalars[10] * tmp2;

            //Sub-expression: LLDI01A0 = Plus[LLDI019E,LLDI019F]
            tmp69 = tmp8 + tmp68;

            //Sub-expression: LLDI01A1 = Times[Rational[1,2],LLDI000F,LLDI017D]
            tmp70 = 0.5 * Scalars[6] * tmp65;

            //Sub-expression: LLDI01A2 = Plus[LLDI01A0,LLDI01A1]
            tmp69 += tmp70;

            //Sub-expression: LLDI01A3 = Power[LLDI01A2,2]
            tmp69 *= tmp69;

            //Sub-expression: LLDI01A4 = Plus[LLDI019D,LLDI01A3]
            tmp1 += tmp69;

            //Sub-expression: LLDI01A6 = Times[-1,LLDI0016,LLDI015A]
            tmp69 = -1 * Scalars[13] * tmp56;

            //Sub-expression: LLDI01A7 = Plus[LLDI01A5,LLDI01A6]
            tmp9 += tmp69;

            //Sub-expression: LLDI01A8 = Times[LLDI0014,LLDI0163]
            tmp70 = Scalars[11] * tmp2;

            //Sub-expression: LLDI01A9 = Plus[LLDI01A7,LLDI01A8]
            tmp9 += tmp70;

            //Sub-expression: LLDI01AA = Times[Rational[-1,2],LLDI0010,LLDI017D]
            tmp71 = -0.5 * Scalars[7] * tmp65;

            //Sub-expression: LLDI01AB = Plus[LLDI01A9,LLDI01AA]
            tmp9 += tmp71;

            //Sub-expression: LLDI01AC = Power[LLDI01AB,2]
            tmp9 *= tmp9;

            //Sub-expression: LLDI01AD = Plus[LLDI01A4,LLDI01AC]
            tmp1 += tmp9;

            //Sub-expression: LLDI01AE = Plus[1,LLDI017C]
            tmp9 = 1 + tmp5;

            //Sub-expression: LLDI01AF = Times[Rational[1,2],LLDI0009,LLDI01AE]
            tmp71 = 0.5 * Scalars[0] * tmp9;

            //Sub-expression: LLDI01B0 = Power[LLDI01AF,2]
            tmp71 *= tmp71;

            //Sub-expression: LLDI01B1 = Times[-1,LLDI01B0]
            tmp71 = -tmp71;

            //Sub-expression: LLDI01B2 = Plus[LLDI01AD,LLDI01B1]
            tmp1 += tmp71;

            //Sub-expression: LLDI01B4 = Times[Rational[-1,2],LLDI000A,LLDI01AE]
            tmp71 = -0.5 * Scalars[1] * tmp9;

            //Sub-expression: LLDI01B5 = Plus[LLDI01B3,LLDI01B4]
            tmp10 += tmp71;

            //Sub-expression: LLDI01B6 = Power[LLDI01B5,2]
            tmp10 *= tmp10;

            //Sub-expression: LLDI01B7 = Times[-1,LLDI01B6]
            tmp10 = -tmp10;

            //Sub-expression: LLDI01B8 = Plus[LLDI01B2,LLDI01B7]
            tmp1 += tmp10;

            //Sub-expression: LLDI01B9 = Times[LLDI0019,LLDI015A]
            tmp10 = Scalars[16] * tmp56;

            //Sub-expression: LLDI01BA = Times[Rational[-1,2],LLDI000B,LLDI01AE]
            tmp71 = -0.5 * Scalars[2] * tmp9;

            //Sub-expression: LLDI01BB = Plus[LLDI01B9,LLDI01BA]
            tmp71 = tmp10 + tmp71;

            //Sub-expression: LLDI01BC = Power[LLDI01BB,2]
            tmp71 *= tmp71;

            //Sub-expression: LLDI01BD = Times[-1,LLDI01BC]
            tmp71 = -tmp71;

            //Sub-expression: LLDI01BE = Plus[LLDI01B8,LLDI01BD]
            tmp1 += tmp71;

            //Sub-expression: LLDI01C0 = Times[-1,LLDI001A,LLDI015A]
            tmp71 = -1 * Scalars[17] * tmp56;

            //Sub-expression: LLDI01C1 = Plus[LLDI01BF,LLDI01C0]
            tmp11 += tmp71;

            //Sub-expression: LLDI01C2 = Times[Rational[1,2],LLDI000C,LLDI01AE]
            tmp72 = 0.5 * Scalars[3] * tmp9;

            //Sub-expression: LLDI01C3 = Plus[LLDI01C1,LLDI01C2]
            tmp11 += tmp72;

            //Sub-expression: LLDI01C4 = Power[LLDI01C3,2]
            tmp11 *= tmp11;

            //Sub-expression: LLDI01C5 = Times[-1,LLDI01C4]
            tmp11 = -tmp11;

            //Sub-expression: LLDI01C6 = Plus[LLDI01BE,LLDI01C5]
            tmp1 += tmp11;

            //Sub-expression: LLDI01C7 = Times[LLDI0019,LLDI0163]
            tmp11 = Scalars[16] * tmp2;

            //Sub-expression: LLDI01C8 = Times[Rational[-1,2],LLDI000D,LLDI01AE]
            tmp72 = -0.5 * Scalars[4] * tmp9;

            //Sub-expression: LLDI01C9 = Plus[LLDI01C7,LLDI01C8]
            tmp72 = tmp11 + tmp72;

            //Sub-expression: LLDI01CA = Power[LLDI01C9,2]
            tmp72 *= tmp72;

            //Sub-expression: LLDI01CB = Times[-1,LLDI01CA]
            tmp72 = -tmp72;

            //Sub-expression: LLDI01CC = Plus[LLDI01C6,LLDI01CB]
            tmp1 += tmp72;

            //Sub-expression: LLDI01CE = Times[-1,LLDI001A,LLDI0163]
            tmp72 = -1 * Scalars[17] * tmp2;

            //Sub-expression: LLDI01CF = Plus[LLDI01CD,LLDI01CE]
            tmp12 += tmp72;

            //Sub-expression: LLDI01D0 = Times[Rational[1,2],LLDI000E,LLDI01AE]
            tmp73 = 0.5 * Scalars[5] * tmp9;

            //Sub-expression: LLDI01D1 = Plus[LLDI01CF,LLDI01D0]
            tmp12 += tmp73;

            //Sub-expression: LLDI01D2 = Power[LLDI01D1,2]
            tmp12 *= tmp12;

            //Sub-expression: LLDI01D3 = Times[-1,LLDI01D2]
            tmp12 = -tmp12;

            //Sub-expression: LLDI01D4 = Plus[LLDI01CC,LLDI01D3]
            tmp1 += tmp12;

            //Sub-expression: LLDI01D5 = Times[LLDI001D,LLDI015A]
            tmp12 = Scalars[20] * tmp56;

            //Sub-expression: LLDI01D6 = Times[-1,LLDI001B,LLDI0163]
            tmp73 = -1 * Scalars[18] * tmp2;

            //Sub-expression: LLDI01D7 = Plus[LLDI01D5,LLDI01D6]
            tmp74 = tmp12 + tmp73;

            //Sub-expression: LLDI01D8 = Times[Rational[1,2],LLDI000F,LLDI01AE]
            var tmp75 = 0.5 * Scalars[6] * tmp9;

            //Sub-expression: LLDI01D9 = Plus[LLDI01D7,LLDI01D8]
            tmp74 += tmp75;

            //Sub-expression: LLDI01DA = Power[LLDI01D9,2]
            tmp74 *= tmp74;

            //Sub-expression: LLDI01DB = Times[-1,LLDI01DA]
            tmp74 = -tmp74;

            //Sub-expression: LLDI01DC = Plus[LLDI01D4,LLDI01DB]
            tmp1 += tmp74;

            //Sub-expression: LLDI01DE = Times[-1,LLDI001E,LLDI015A]
            tmp74 = -1 * Scalars[21] * tmp56;

            //Sub-expression: LLDI01DF = Plus[LLDI01DD,LLDI01DE]
            tmp13 += tmp74;

            //Sub-expression: LLDI01E0 = Times[LLDI001C,LLDI0163]
            tmp75 = Scalars[19] * tmp2;

            //Sub-expression: LLDI01E1 = Plus[LLDI01DF,LLDI01E0]
            tmp13 += tmp75;

            //Sub-expression: LLDI01E2 = Times[Rational[-1,2],LLDI0010,LLDI01AE]
            var tmp76 = -0.5 * Scalars[7] * tmp9;

            //Sub-expression: LLDI01E3 = Plus[LLDI01E1,LLDI01E2]
            tmp13 += tmp76;

            //Sub-expression: LLDI01E4 = Power[LLDI01E3,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI01E5 = Times[-1,LLDI01E4]
            tmp13 = -tmp13;

            //Sub-expression: LLDI01E6 = Plus[LLDI01DC,LLDI01E5]
            tmp1 += tmp13;

            //Sub-expression: LLDI01E7 = Times[LLDI0019,LLDI017D]
            tmp13 = Scalars[16] * tmp65;

            //Sub-expression: LLDI01E8 = Times[-1,LLDI0011,LLDI01AE]
            tmp76 = -1 * Scalars[8] * tmp9;

            //Sub-expression: LLDI01E9 = Plus[LLDI01E7,LLDI01E8]
            tmp13 += tmp76;

            //Sub-expression: LLDI01EA = Times[Rational[1,2],LLDI01E9]
            tmp13 = 0.5 * tmp13;

            //Sub-expression: LLDI01EB = Power[LLDI01EA,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI01EC = Times[-1,LLDI01EB]
            tmp13 = -tmp13;

            //Sub-expression: LLDI01ED = Plus[LLDI01E6,LLDI01EC]
            tmp1 += tmp13;

            //Sub-expression: LLDI01F1 = Times[LLDI0012,LLDI017C]
            tmp13 = Scalars[9] * tmp5;

            //Sub-expression: LLDI01F2 = Plus[LLDI01F0,LLDI01F1]
            tmp13 = tmp15 + tmp13;

            //Sub-expression: LLDI01F3 = Times[-1,LLDI001A,LLDI017C]
            tmp15 = -1 * Scalars[17] * tmp5;

            //Sub-expression: LLDI01F4 = Plus[LLDI01F2,LLDI01F3]
            tmp13 += tmp15;

            //Sub-expression: LLDI01F5 = Times[Rational[1,2],LLDI01F4]
            tmp13 = 0.5 * tmp13;

            //Sub-expression: LLDI01F6 = Power[LLDI01F5,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI01F7 = Times[-1,LLDI01F6]
            tmp13 = -tmp13;

            //Sub-expression: LLDI01F8 = Plus[LLDI01ED,LLDI01F7]
            tmp1 += tmp13;

            //Sub-expression: LLDI01FA = Times[2,LLDI0021,LLDI015A]
            tmp13 = 2 * Scalars[24] * tmp56;

            //Sub-expression: LLDI01FB = Plus[LLDI01F9,LLDI01FA]
            tmp13 = tmp16 + tmp13;

            //Sub-expression: LLDI01FC = Times[LLDI0013,LLDI017C]
            tmp15 = Scalars[10] * tmp5;

            //Sub-expression: LLDI01FD = Plus[LLDI01FB,LLDI01FC]
            tmp15 = tmp13 + tmp15;

            //Sub-expression: LLDI01FE = Times[-1,LLDI001B,LLDI017C]
            tmp16 = -1 * Scalars[18] * tmp5;

            //Sub-expression: LLDI01FF = Plus[LLDI01FD,LLDI01FE]
            tmp15 += tmp16;

            //Sub-expression: LLDI0200 = Times[Rational[1,2],LLDI01FF]
            tmp15 = 0.5 * tmp15;

            //Sub-expression: LLDI0201 = Power[LLDI0200,2]
            tmp15 *= tmp15;

            //Sub-expression: LLDI0202 = Times[-1,LLDI0201]
            tmp15 = -tmp15;

            //Sub-expression: LLDI0203 = Plus[LLDI01F8,LLDI0202]
            tmp1 += tmp15;

            //Sub-expression: LLDI0205 = Times[-1,LLDI0022,LLDI015A]
            tmp15 = -1 * Scalars[25] * tmp56;

            //Sub-expression: LLDI0206 = Plus[LLDI0204,LLDI0205]
            tmp16 = tmp17 + tmp15;

            //Sub-expression: LLDI0207 = Times[Rational[1,2],LLDI001C,LLDI017D]
            tmp17 = 0.5 * Scalars[19] * tmp65;

            //Sub-expression: LLDI0208 = Plus[LLDI0206,LLDI0207]
            tmp16 += tmp17;

            //Sub-expression: LLDI0209 = Times[Rational[-1,2],LLDI0014,LLDI01AE]
            tmp17 = -0.5 * Scalars[11] * tmp9;

            //Sub-expression: LLDI020A = Plus[LLDI0208,LLDI0209]
            tmp16 += tmp17;

            //Sub-expression: LLDI020B = Power[LLDI020A,2]
            tmp16 *= tmp16;

            //Sub-expression: LLDI020C = Times[-1,LLDI020B]
            tmp16 = -tmp16;

            //Sub-expression: LLDI020D = Plus[LLDI0203,LLDI020C]
            tmp1 += tmp16;

            //Sub-expression: LLDI020F = Times[2,LLDI0021,LLDI0163]
            tmp16 = 2 * Scalars[24] * tmp2;

            //Sub-expression: LLDI0210 = Plus[LLDI020E,LLDI020F]
            tmp16 = tmp18 + tmp16;

            //Sub-expression: LLDI0211 = Times[LLDI0015,LLDI017C]
            tmp17 = Scalars[12] * tmp5;

            //Sub-expression: LLDI0212 = Plus[LLDI0210,LLDI0211]
            tmp17 = tmp16 + tmp17;

            //Sub-expression: LLDI0213 = Times[-1,LLDI001D,LLDI017C]
            tmp18 = -1 * Scalars[20] * tmp5;

            //Sub-expression: LLDI0214 = Plus[LLDI0212,LLDI0213]
            tmp17 += tmp18;

            //Sub-expression: LLDI0215 = Times[Rational[1,2],LLDI0214]
            tmp17 = 0.5 * tmp17;

            //Sub-expression: LLDI0216 = Power[LLDI0215,2]
            tmp17 *= tmp17;

            //Sub-expression: LLDI0217 = Times[-1,LLDI0216]
            tmp17 = -tmp17;

            //Sub-expression: LLDI0218 = Plus[LLDI020D,LLDI0217]
            tmp1 += tmp17;

            //Sub-expression: LLDI021A = Times[-1,LLDI0022,LLDI0163]
            tmp17 = -1 * Scalars[25] * tmp2;

            //Sub-expression: LLDI021B = Plus[LLDI0219,LLDI021A]
            tmp18 = tmp19 + tmp17;

            //Sub-expression: LLDI021C = Times[Rational[1,2],LLDI001E,LLDI017D]
            tmp19 = 0.5 * Scalars[21] * tmp65;

            //Sub-expression: LLDI021D = Plus[LLDI021B,LLDI021C]
            tmp18 += tmp19;

            //Sub-expression: LLDI021E = Times[Rational[-1,2],LLDI0016,LLDI01AE]
            tmp19 = -0.5 * Scalars[13] * tmp9;

            //Sub-expression: LLDI021F = Plus[LLDI021D,LLDI021E]
            tmp18 += tmp19;

            //Sub-expression: LLDI0220 = Power[LLDI021F,2]
            tmp18 *= tmp18;

            //Sub-expression: LLDI0221 = Times[-1,LLDI0220]
            tmp18 = -tmp18;

            //Sub-expression: LLDI0222 = Plus[LLDI0218,LLDI0221]
            tmp1 += tmp18;

            //Sub-expression: LLDI0223 = Times[LLDI0025,LLDI015A]
            tmp18 = Scalars[28] * tmp56;

            //Sub-expression: LLDI0224 = Times[-1,LLDI0023,LLDI0163]
            tmp19 = -1 * Scalars[26] * tmp2;

            //Sub-expression: LLDI0225 = Plus[LLDI0223,LLDI0224]
            tmp76 = tmp18 + tmp19;

            //Sub-expression: LLDI0226 = Times[Rational[1,2],LLDI001F,LLDI017D]
            tmp65 = 0.5 * Scalars[22] * tmp65;

            //Sub-expression: LLDI0227 = Plus[LLDI0225,LLDI0226]
            tmp65 = tmp76 + tmp65;

            //Sub-expression: LLDI0228 = Times[Rational[-1,2],LLDI0017,LLDI01AE]
            tmp9 = -0.5 * Scalars[14] * tmp9;

            //Sub-expression: LLDI0229 = Plus[LLDI0227,LLDI0228]
            tmp9 = tmp65 + tmp9;

            //Sub-expression: LLDI022A = Power[LLDI0229,2]
            tmp9 *= tmp9;

            //Sub-expression: LLDI022B = Times[-1,LLDI022A]
            tmp9 = -tmp9;

            //Sub-expression: LLDI022C = Plus[LLDI0222,LLDI022B]
            tmp1 += tmp9;

            //Sub-expression: LLDI0230 = Times[-2,LLDI0026,LLDI015A]
            tmp9 = -2 * Scalars[29] * tmp56;

            //Sub-expression: LLDI0231 = Plus[LLDI022F,LLDI0230]
            tmp0 += tmp9;

            //Sub-expression: LLDI0232 = Times[2,LLDI0024,LLDI0163]
            tmp2 = 2 * Scalars[27] * tmp2;

            //Sub-expression: LLDI0233 = Plus[LLDI0231,LLDI0232]
            tmp0 += tmp2;

            //Sub-expression: LLDI0234 = Times[LLDI0018,LLDI017C]
            tmp56 = Scalars[15] * tmp5;

            //Sub-expression: LLDI0235 = Plus[LLDI0233,LLDI0234]
            tmp0 += tmp56;

            //Sub-expression: LLDI0236 = Times[-1,LLDI0020,LLDI017C]
            tmp5 = -1 * Scalars[23] * tmp5;

            //Sub-expression: LLDI0237 = Plus[LLDI0235,LLDI0236]
            tmp0 += tmp5;

            //Sub-expression: LLDI0238 = Times[Rational[1,2],LLDI0237]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI0239 = Power[LLDI0238,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI023A = Times[-1,LLDI0239]
            tmp0 = -tmp0;

            //Output: LLDI0001 = Plus[LLDI022C,LLDI023A]
            var d1 = tmp1 + tmp0;

            //Sub-expression: LLDI023B = Plus[LLDI0005,LLDI0159]
            tmp0 = point.X + tmp55;

            //Sub-expression: LLDI023C = Times[LLDI0009,LLDI023B]
            tmp1 = Scalars[0] * tmp0;

            //Sub-expression: LLDI023D = Power[LLDI023C,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI023E = Plus[LLDI023D,LLDI015C]
            tmp5 = tmp1 + tmp57;

            //Sub-expression: LLDI023F = Times[LLDI000B,LLDI023B]
            tmp55 = Scalars[2] * tmp0;

            //Sub-expression: LLDI0240 = Plus[LLDI023F,LLDI015F]
            tmp56 = tmp55 + tmp58;

            //Sub-expression: LLDI0241 = Power[LLDI0240,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0242 = Plus[LLDI023E,LLDI0241]
            tmp5 += tmp56;

            //Sub-expression: LLDI0246 = Plus[LLDI0242,LLDI0245]
            tmp5 += tmp22;

            //Sub-expression: LLDI0247 = Times[LLDI000D,LLDI023B]
            tmp22 = Scalars[4] * tmp0;

            //Sub-expression: LLDI0249 = Plus[LLDI0247,LLDI0248]
            tmp23 = tmp22 + tmp23;

            //Sub-expression: LLDI024A = Power[LLDI0249,2]
            tmp23 *= tmp23;

            //Sub-expression: LLDI024B = Plus[LLDI0246,LLDI024A]
            tmp5 += tmp23;

            //Sub-expression: LLDI024D = Plus[LLDI016C,LLDI024C]
            tmp3 += tmp24;

            //Sub-expression: LLDI024E = Power[LLDI024D,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI024F = Plus[LLDI024B,LLDI024E]
            tmp3 = tmp5 + tmp3;

            //Sub-expression: LLDI0250 = Times[LLDI000F,LLDI023B]
            tmp5 = Scalars[6] * tmp0;

            //Sub-expression: LLDI0251 = Plus[LLDI0250,LLDI0172]
            tmp23 = tmp5 + tmp62;

            //Sub-expression: LLDI0253 = Plus[LLDI0251,LLDI0252]
            tmp23 += tmp25;

            //Sub-expression: LLDI0254 = Power[LLDI0253,2]
            tmp23 *= tmp23;

            //Sub-expression: LLDI0255 = Plus[LLDI024F,LLDI0254]
            tmp3 += tmp23;

            //Sub-expression: LLDI0256 = Power[LLDI023B,2]
            tmp23 = tmp0 * tmp0;

            //Sub-expression: LLDI0257 = Plus[LLDI0256,LLDI0179]
            tmp4 = tmp23 + tmp4;

            //Sub-expression: LLDI0259 = Plus[LLDI0257,LLDI0258]
            tmp4 += tmp26;

            //Sub-expression: LLDI025A = Plus[-1,LLDI0259]
            tmp24 = -1 + tmp4;

            //Sub-expression: LLDI025B = Times[Rational[1,2],LLDI0009,LLDI025A]
            tmp25 = 0.5 * Scalars[0] * tmp24;

            //Sub-expression: LLDI025C = Power[LLDI025B,2]
            tmp25 *= tmp25;

            //Sub-expression: LLDI025D = Plus[LLDI0255,LLDI025C]
            tmp3 += tmp25;

            //Sub-expression: LLDI025E = Times[LLDI0011,LLDI023B]
            tmp25 = Scalars[8] * tmp0;

            //Sub-expression: LLDI025F = Times[Rational[-1,2],LLDI000A,LLDI025A]
            tmp26 = -0.5 * Scalars[1] * tmp24;

            //Sub-expression: LLDI0260 = Plus[LLDI025E,LLDI025F]
            tmp26 = tmp25 + tmp26;

            //Sub-expression: LLDI0261 = Power[LLDI0260,2]
            tmp26 *= tmp26;

            //Sub-expression: LLDI0262 = Plus[LLDI025D,LLDI0261]
            tmp3 += tmp26;

            //Sub-expression: LLDI0263 = Times[Rational[-1,2],LLDI000B,LLDI025A]
            tmp26 = -0.5 * Scalars[2] * tmp24;

            //Sub-expression: LLDI0264 = Plus[LLDI0186,LLDI0263]
            tmp6 += tmp26;

            //Sub-expression: LLDI0265 = Power[LLDI0264,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0266 = Plus[LLDI0262,LLDI0265]
            tmp3 += tmp6;

            //Sub-expression: LLDI0267 = Times[LLDI0013,LLDI023B]
            tmp6 = Scalars[10] * tmp0;

            //Sub-expression: LLDI0268 = Plus[LLDI0267,LLDI018C]
            tmp26 = tmp6 + tmp66;

            //Sub-expression: LLDI0269 = Times[Rational[1,2],LLDI000C,LLDI025A]
            tmp56 = 0.5 * Scalars[3] * tmp24;

            //Sub-expression: LLDI026A = Plus[LLDI0268,LLDI0269]
            tmp26 += tmp56;

            //Sub-expression: LLDI026B = Power[LLDI026A,2]
            tmp26 *= tmp26;

            //Sub-expression: LLDI026C = Plus[LLDI0266,LLDI026B]
            tmp3 += tmp26;

            //Sub-expression: LLDI026E = Times[Rational[-1,2],LLDI000D,LLDI025A]
            tmp26 = -0.5 * Scalars[4] * tmp24;

            //Sub-expression: LLDI026F = Plus[LLDI026D,LLDI026E]
            tmp26 = tmp27 + tmp26;

            //Sub-expression: LLDI0270 = Power[LLDI026F,2]
            tmp26 *= tmp26;

            //Sub-expression: LLDI0271 = Plus[LLDI026C,LLDI0270]
            tmp3 += tmp26;

            //Sub-expression: LLDI0272 = Times[LLDI0015,LLDI023B]
            tmp26 = Scalars[12] * tmp0;

            //Sub-expression: LLDI0274 = Plus[LLDI0272,LLDI0273]
            tmp27 = tmp26 + tmp28;

            //Sub-expression: LLDI0275 = Times[Rational[1,2],LLDI000E,LLDI025A]
            tmp28 = 0.5 * Scalars[5] * tmp24;

            //Sub-expression: LLDI0276 = Plus[LLDI0274,LLDI0275]
            tmp27 += tmp28;

            //Sub-expression: LLDI0277 = Power[LLDI0276,2]
            tmp27 *= tmp27;

            //Sub-expression: LLDI0278 = Plus[LLDI0271,LLDI0277]
            tmp3 += tmp27;

            //Sub-expression: LLDI027A = Plus[LLDI019E,LLDI0279]
            tmp8 += tmp29;

            //Sub-expression: LLDI027B = Times[Rational[1,2],LLDI000F,LLDI025A]
            tmp27 = 0.5 * Scalars[6] * tmp24;

            //Sub-expression: LLDI027C = Plus[LLDI027A,LLDI027B]
            tmp8 += tmp27;

            //Sub-expression: LLDI027D = Power[LLDI027C,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI027E = Plus[LLDI0278,LLDI027D]
            tmp3 += tmp8;

            //Sub-expression: LLDI027F = Times[LLDI0017,LLDI023B]
            tmp8 = Scalars[14] * tmp0;

            //Sub-expression: LLDI0280 = Plus[LLDI027F,LLDI01A6]
            tmp27 = tmp8 + tmp69;

            //Sub-expression: LLDI0282 = Plus[LLDI0280,LLDI0281]
            tmp27 += tmp30;

            //Sub-expression: LLDI0283 = Times[Rational[-1,2],LLDI0010,LLDI025A]
            tmp28 = -0.5 * Scalars[7] * tmp24;

            //Sub-expression: LLDI0284 = Plus[LLDI0282,LLDI0283]
            tmp27 += tmp28;

            //Sub-expression: LLDI0285 = Power[LLDI0284,2]
            tmp27 *= tmp27;

            //Sub-expression: LLDI0286 = Plus[LLDI027E,LLDI0285]
            tmp3 += tmp27;

            //Sub-expression: LLDI0287 = Plus[1,LLDI0259]
            tmp27 = 1 + tmp4;

            //Sub-expression: LLDI0288 = Times[Rational[1,2],LLDI0009,LLDI0287]
            tmp28 = 0.5 * Scalars[0] * tmp27;

            //Sub-expression: LLDI0289 = Power[LLDI0288,2]
            tmp28 *= tmp28;

            //Sub-expression: LLDI028A = Times[-1,LLDI0289]
            tmp28 = -tmp28;

            //Sub-expression: LLDI028B = Plus[LLDI0286,LLDI028A]
            tmp3 += tmp28;

            //Sub-expression: LLDI028C = Times[LLDI0019,LLDI023B]
            tmp28 = Scalars[16] * tmp0;

            //Sub-expression: LLDI028D = Times[Rational[-1,2],LLDI000A,LLDI0287]
            tmp29 = -0.5 * Scalars[1] * tmp27;

            //Sub-expression: LLDI028E = Plus[LLDI028C,LLDI028D]
            tmp29 = tmp28 + tmp29;

            //Sub-expression: LLDI028F = Power[LLDI028E,2]
            tmp29 *= tmp29;

            //Sub-expression: LLDI0290 = Times[-1,LLDI028F]
            tmp29 = -tmp29;

            //Sub-expression: LLDI0291 = Plus[LLDI028B,LLDI0290]
            tmp3 += tmp29;

            //Sub-expression: LLDI0292 = Times[Rational[-1,2],LLDI000B,LLDI0287]
            tmp29 = -0.5 * Scalars[2] * tmp27;

            //Sub-expression: LLDI0293 = Plus[LLDI01B9,LLDI0292]
            tmp10 += tmp29;

            //Sub-expression: LLDI0294 = Power[LLDI0293,2]
            tmp10 *= tmp10;

            //Sub-expression: LLDI0295 = Times[-1,LLDI0294]
            tmp10 = -tmp10;

            //Sub-expression: LLDI0296 = Plus[LLDI0291,LLDI0295]
            tmp3 += tmp10;

            //Sub-expression: LLDI0297 = Times[LLDI001B,LLDI023B]
            tmp10 = Scalars[18] * tmp0;

            //Sub-expression: LLDI0298 = Plus[LLDI0297,LLDI01C0]
            tmp29 = tmp10 + tmp71;

            //Sub-expression: LLDI0299 = Times[Rational[1,2],LLDI000C,LLDI0287]
            tmp30 = 0.5 * Scalars[3] * tmp27;

            //Sub-expression: LLDI029A = Plus[LLDI0298,LLDI0299]
            tmp29 += tmp30;

            //Sub-expression: LLDI029B = Power[LLDI029A,2]
            tmp29 *= tmp29;

            //Sub-expression: LLDI029C = Times[-1,LLDI029B]
            tmp29 = -tmp29;

            //Sub-expression: LLDI029D = Plus[LLDI0296,LLDI029C]
            tmp3 += tmp29;

            //Sub-expression: LLDI029F = Times[Rational[-1,2],LLDI000D,LLDI0287]
            tmp29 = -0.5 * Scalars[4] * tmp27;

            //Sub-expression: LLDI02A0 = Plus[LLDI029E,LLDI029F]
            tmp29 = tmp31 + tmp29;

            //Sub-expression: LLDI02A1 = Power[LLDI02A0,2]
            tmp29 *= tmp29;

            //Sub-expression: LLDI02A2 = Times[-1,LLDI02A1]
            tmp29 = -tmp29;

            //Sub-expression: LLDI02A3 = Plus[LLDI029D,LLDI02A2]
            tmp3 += tmp29;

            //Sub-expression: LLDI02A4 = Times[LLDI001D,LLDI023B]
            tmp29 = Scalars[20] * tmp0;

            //Sub-expression: LLDI02A6 = Plus[LLDI02A4,LLDI02A5]
            tmp30 = tmp29 + tmp32;

            //Sub-expression: LLDI02A7 = Times[Rational[1,2],LLDI000E,LLDI0287]
            tmp31 = 0.5 * Scalars[5] * tmp27;

            //Sub-expression: LLDI02A8 = Plus[LLDI02A6,LLDI02A7]
            tmp30 += tmp31;

            //Sub-expression: LLDI02A9 = Power[LLDI02A8,2]
            tmp30 *= tmp30;

            //Sub-expression: LLDI02AA = Times[-1,LLDI02A9]
            tmp30 = -tmp30;

            //Sub-expression: LLDI02AB = Plus[LLDI02A3,LLDI02AA]
            tmp3 += tmp30;

            //Sub-expression: LLDI02AD = Plus[LLDI01D5,LLDI02AC]
            tmp12 += tmp33;

            //Sub-expression: LLDI02AE = Times[Rational[1,2],LLDI000F,LLDI0287]
            tmp30 = 0.5 * Scalars[6] * tmp27;

            //Sub-expression: LLDI02AF = Plus[LLDI02AD,LLDI02AE]
            tmp12 += tmp30;

            //Sub-expression: LLDI02B0 = Power[LLDI02AF,2]
            tmp12 *= tmp12;

            //Sub-expression: LLDI02B1 = Times[-1,LLDI02B0]
            tmp12 = -tmp12;

            //Sub-expression: LLDI02B2 = Plus[LLDI02AB,LLDI02B1]
            tmp3 += tmp12;

            //Sub-expression: LLDI02B3 = Times[LLDI001F,LLDI023B]
            tmp12 = Scalars[22] * tmp0;

            //Sub-expression: LLDI02B4 = Plus[LLDI02B3,LLDI01DE]
            tmp30 = tmp12 + tmp74;

            //Sub-expression: LLDI02B6 = Plus[LLDI02B4,LLDI02B5]
            tmp30 += tmp34;

            //Sub-expression: LLDI02B7 = Times[Rational[-1,2],LLDI0010,LLDI0287]
            tmp31 = -0.5 * Scalars[7] * tmp27;

            //Sub-expression: LLDI02B8 = Plus[LLDI02B6,LLDI02B7]
            tmp30 += tmp31;

            //Sub-expression: LLDI02B9 = Power[LLDI02B8,2]
            tmp30 *= tmp30;

            //Sub-expression: LLDI02BA = Times[-1,LLDI02B9]
            tmp30 = -tmp30;

            //Sub-expression: LLDI02BB = Plus[LLDI02B2,LLDI02BA]
            tmp3 += tmp30;

            //Sub-expression: LLDI02BC = Times[LLDI0019,LLDI025A]
            tmp30 = Scalars[16] * tmp24;

            //Sub-expression: LLDI02BD = Times[-1,LLDI0011,LLDI0287]
            tmp31 = -1 * Scalars[8] * tmp27;

            //Sub-expression: LLDI02BE = Plus[LLDI02BC,LLDI02BD]
            tmp30 += tmp31;

            //Sub-expression: LLDI02BF = Times[Rational[1,2],LLDI02BE]
            tmp30 = 0.5 * tmp30;

            //Sub-expression: LLDI02C0 = Power[LLDI02BF,2]
            tmp30 *= tmp30;

            //Sub-expression: LLDI02C1 = Times[-1,LLDI02C0]
            tmp30 = -tmp30;

            //Sub-expression: LLDI02C2 = Plus[LLDI02BB,LLDI02C1]
            tmp3 += tmp30;

            //Sub-expression: LLDI02C3 = Times[2,LLDI0021,LLDI023B]
            tmp30 = 2 * Scalars[24] * tmp0;

            //Sub-expression: LLDI02C4 = Plus[LLDI01EE,LLDI02C3]
            tmp14 += tmp30;

            //Sub-expression: LLDI02C5 = Times[LLDI0012,LLDI0259]
            tmp30 = Scalars[9] * tmp4;

            //Sub-expression: LLDI02C6 = Plus[LLDI02C4,LLDI02C5]
            tmp30 = tmp14 + tmp30;

            //Sub-expression: LLDI02C7 = Times[-1,LLDI001A,LLDI0259]
            tmp31 = -1 * Scalars[17] * tmp4;

            //Sub-expression: LLDI02C8 = Plus[LLDI02C6,LLDI02C7]
            tmp30 += tmp31;

            //Sub-expression: LLDI02C9 = Times[Rational[1,2],LLDI02C8]
            tmp30 = 0.5 * tmp30;

            //Sub-expression: LLDI02CA = Power[LLDI02C9,2]
            tmp30 *= tmp30;

            //Sub-expression: LLDI02CB = Times[-1,LLDI02CA]
            tmp30 = -tmp30;

            //Sub-expression: LLDI02CC = Plus[LLDI02C2,LLDI02CB]
            tmp3 += tmp30;

            //Sub-expression: LLDI02CD = Times[LLDI0013,LLDI0259]
            tmp30 = Scalars[10] * tmp4;

            //Sub-expression: LLDI02CE = Plus[LLDI01FB,LLDI02CD]
            tmp13 += tmp30;

            //Sub-expression: LLDI02CF = Times[-1,LLDI001B,LLDI0259]
            tmp30 = -1 * Scalars[18] * tmp4;

            //Sub-expression: LLDI02D0 = Plus[LLDI02CE,LLDI02CF]
            tmp13 += tmp30;

            //Sub-expression: LLDI02D1 = Times[Rational[1,2],LLDI02D0]
            tmp13 = 0.5 * tmp13;

            //Sub-expression: LLDI02D2 = Power[LLDI02D1,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI02D3 = Times[-1,LLDI02D2]
            tmp13 = -tmp13;

            //Sub-expression: LLDI02D4 = Plus[LLDI02CC,LLDI02D3]
            tmp3 += tmp13;

            //Sub-expression: LLDI02D5 = Times[LLDI0023,LLDI023B]
            tmp13 = Scalars[26] * tmp0;

            //Sub-expression: LLDI02D6 = Plus[LLDI02D5,LLDI0205]
            tmp15 = tmp13 + tmp15;

            //Sub-expression: LLDI02D7 = Times[Rational[1,2],LLDI001C,LLDI025A]
            tmp30 = 0.5 * Scalars[19] * tmp24;

            //Sub-expression: LLDI02D8 = Plus[LLDI02D6,LLDI02D7]
            tmp15 += tmp30;

            //Sub-expression: LLDI02D9 = Times[Rational[-1,2],LLDI0014,LLDI0287]
            tmp30 = -0.5 * Scalars[11] * tmp27;

            //Sub-expression: LLDI02DA = Plus[LLDI02D8,LLDI02D9]
            tmp15 += tmp30;

            //Sub-expression: LLDI02DB = Power[LLDI02DA,2]
            tmp15 *= tmp15;

            //Sub-expression: LLDI02DC = Times[-1,LLDI02DB]
            tmp15 = -tmp15;

            //Sub-expression: LLDI02DD = Plus[LLDI02D4,LLDI02DC]
            tmp3 += tmp15;

            //Sub-expression: LLDI02E0 = Times[LLDI0015,LLDI0259]
            tmp15 = Scalars[12] * tmp4;

            //Sub-expression: LLDI02E1 = Plus[LLDI02DF,LLDI02E0]
            tmp15 = tmp35 + tmp15;

            //Sub-expression: LLDI02E2 = Times[-1,LLDI001D,LLDI0259]
            tmp30 = -1 * Scalars[20] * tmp4;

            //Sub-expression: LLDI02E3 = Plus[LLDI02E1,LLDI02E2]
            tmp15 += tmp30;

            //Sub-expression: LLDI02E4 = Times[Rational[1,2],LLDI02E3]
            tmp15 = 0.5 * tmp15;

            //Sub-expression: LLDI02E5 = Power[LLDI02E4,2]
            tmp15 *= tmp15;

            //Sub-expression: LLDI02E6 = Times[-1,LLDI02E5]
            tmp15 = -tmp15;

            //Sub-expression: LLDI02E7 = Plus[LLDI02DD,LLDI02E6]
            tmp3 += tmp15;

            //Sub-expression: LLDI02E8 = Times[LLDI0025,LLDI023B]
            tmp15 = Scalars[28] * tmp0;

            //Sub-expression: LLDI02EA = Plus[LLDI02E8,LLDI02E9]
            tmp30 = tmp15 + tmp36;

            //Sub-expression: LLDI02EB = Times[Rational[1,2],LLDI001E,LLDI025A]
            tmp31 = 0.5 * Scalars[21] * tmp24;

            //Sub-expression: LLDI02EC = Plus[LLDI02EA,LLDI02EB]
            tmp30 += tmp31;

            //Sub-expression: LLDI02ED = Times[Rational[-1,2],LLDI0016,LLDI0287]
            tmp31 = -0.5 * Scalars[13] * tmp27;

            //Sub-expression: LLDI02EE = Plus[LLDI02EC,LLDI02ED]
            tmp30 += tmp31;

            //Sub-expression: LLDI02EF = Power[LLDI02EE,2]
            tmp30 *= tmp30;

            //Sub-expression: LLDI02F0 = Times[-1,LLDI02EF]
            tmp30 = -tmp30;

            //Sub-expression: LLDI02F1 = Plus[LLDI02E7,LLDI02F0]
            tmp3 += tmp30;

            //Sub-expression: LLDI02F3 = Plus[LLDI0223,LLDI02F2]
            tmp18 += tmp37;

            //Sub-expression: LLDI02F4 = Times[Rational[1,2],LLDI001F,LLDI025A]
            tmp24 = 0.5 * Scalars[22] * tmp24;

            //Sub-expression: LLDI02F5 = Plus[LLDI02F3,LLDI02F4]
            tmp18 += tmp24;

            //Sub-expression: LLDI02F6 = Times[Rational[-1,2],LLDI0017,LLDI0287]
            tmp24 = -0.5 * Scalars[14] * tmp27;

            //Sub-expression: LLDI02F7 = Plus[LLDI02F5,LLDI02F6]
            tmp18 += tmp24;

            //Sub-expression: LLDI02F8 = Power[LLDI02F7,2]
            tmp18 *= tmp18;

            //Sub-expression: LLDI02F9 = Times[-1,LLDI02F8]
            tmp18 = -tmp18;

            //Sub-expression: LLDI02FA = Plus[LLDI02F1,LLDI02F9]
            tmp3 += tmp18;

            //Sub-expression: LLDI02FB = Times[2,LLDI0027,LLDI023B]
            tmp0 = 2 * Scalars[30] * tmp0;

            //Sub-expression: LLDI02FC = Plus[LLDI022D,LLDI02FB]
            tmp0 = tmp20 + tmp0;

            //Sub-expression: LLDI02FD = Plus[LLDI02FC,LLDI0230]
            tmp9 = tmp0 + tmp9;

            //Sub-expression: LLDI02FF = Plus[LLDI02FD,LLDI02FE]
            tmp9 += tmp21;

            //Sub-expression: LLDI0300 = Times[LLDI0018,LLDI0259]
            tmp18 = Scalars[15] * tmp4;

            //Sub-expression: LLDI0301 = Plus[LLDI02FF,LLDI0300]
            tmp9 += tmp18;

            //Sub-expression: LLDI0302 = Times[-1,LLDI0020,LLDI0259]
            tmp4 = -1 * Scalars[23] * tmp4;

            //Sub-expression: LLDI0303 = Plus[LLDI0301,LLDI0302]
            tmp4 = tmp9 + tmp4;

            //Sub-expression: LLDI0304 = Times[Rational[1,2],LLDI0303]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI0305 = Power[LLDI0304,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0306 = Times[-1,LLDI0305]
            tmp4 = -tmp4;

            //Output: LLDI0002 = Plus[LLDI02FA,LLDI0306]
            var d2 = tmp3 + tmp4;

            //Sub-expression: LLDI030A = Plus[LLDI023D,LLDI0309]
            tmp1 += tmp39;

            //Sub-expression: LLDI030C = Plus[LLDI023F,LLDI030B]
            tmp3 = tmp55 + tmp40;

            //Sub-expression: LLDI030D = Power[LLDI030C,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI030E = Plus[LLDI030A,LLDI030D]
            tmp1 += tmp3;

            //Sub-expression: LLDI030F = Plus[LLDI030E,LLDI0165]
            tmp1 += tmp59;

            //Sub-expression: LLDI0310 = Plus[LLDI0247,LLDI0168]
            tmp3 = tmp22 + tmp60;

            //Sub-expression: LLDI0311 = Power[LLDI0310,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0312 = Plus[LLDI030F,LLDI0311]
            tmp1 += tmp3;

            //Sub-expression: LLDI0314 = Plus[LLDI0313,LLDI016D]
            tmp3 = tmp41 + tmp61;

            //Sub-expression: LLDI0315 = Power[LLDI0314,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0316 = Plus[LLDI0312,LLDI0315]
            tmp1 += tmp3;

            //Sub-expression: LLDI0318 = Plus[LLDI0250,LLDI0317]
            tmp3 = tmp5 + tmp42;

            //Sub-expression: LLDI0319 = Plus[LLDI0318,LLDI0174]
            tmp3 += tmp63;

            //Sub-expression: LLDI031A = Power[LLDI0319,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI031B = Plus[LLDI0316,LLDI031A]
            tmp1 += tmp3;

            //Sub-expression: LLDI031D = Plus[LLDI0256,LLDI031C]
            tmp3 = tmp23 + tmp43;

            //Sub-expression: LLDI031E = Plus[LLDI031D,LLDI017B]
            tmp3 += tmp64;

            //Sub-expression: LLDI031F = Plus[-1,LLDI031E]
            tmp4 = -1 + tmp3;

            //Sub-expression: LLDI0320 = Times[Rational[1,2],LLDI0009,LLDI031F]
            tmp5 = 0.5 * Scalars[0] * tmp4;

            //Sub-expression: LLDI0321 = Power[LLDI0320,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0322 = Plus[LLDI031B,LLDI0321]
            tmp1 += tmp5;

            //Sub-expression: LLDI0323 = Times[Rational[-1,2],LLDI000A,LLDI031F]
            tmp5 = -0.5 * Scalars[1] * tmp4;

            //Sub-expression: LLDI0324 = Plus[LLDI025E,LLDI0323]
            tmp5 = tmp25 + tmp5;

            //Sub-expression: LLDI0325 = Power[LLDI0324,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0326 = Plus[LLDI0322,LLDI0325]
            tmp1 += tmp5;

            //Sub-expression: LLDI0328 = Times[Rational[-1,2],LLDI000B,LLDI031F]
            tmp5 = -0.5 * Scalars[2] * tmp4;

            //Sub-expression: LLDI0329 = Plus[LLDI0327,LLDI0328]
            tmp5 = tmp44 + tmp5;

            //Sub-expression: LLDI032A = Power[LLDI0329,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI032B = Plus[LLDI0326,LLDI032A]
            tmp1 += tmp5;

            //Sub-expression: LLDI032D = Plus[LLDI0267,LLDI032C]
            tmp5 = tmp6 + tmp45;

            //Sub-expression: LLDI032E = Times[Rational[1,2],LLDI000C,LLDI031F]
            tmp6 = 0.5 * Scalars[3] * tmp4;

            //Sub-expression: LLDI032F = Plus[LLDI032D,LLDI032E]
            tmp5 += tmp6;

            //Sub-expression: LLDI0330 = Power[LLDI032F,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0331 = Plus[LLDI032B,LLDI0330]
            tmp1 += tmp5;

            //Sub-expression: LLDI0332 = Times[Rational[-1,2],LLDI000D,LLDI031F]
            tmp5 = -0.5 * Scalars[4] * tmp4;

            //Sub-expression: LLDI0333 = Plus[LLDI0192,LLDI0332]
            tmp5 = tmp7 + tmp5;

            //Sub-expression: LLDI0334 = Power[LLDI0333,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0335 = Plus[LLDI0331,LLDI0334]
            tmp1 += tmp5;

            //Sub-expression: LLDI0336 = Plus[LLDI0272,LLDI0198]
            tmp5 = tmp26 + tmp67;

            //Sub-expression: LLDI0337 = Times[Rational[1,2],LLDI000E,LLDI031F]
            tmp6 = 0.5 * Scalars[5] * tmp4;

            //Sub-expression: LLDI0338 = Plus[LLDI0336,LLDI0337]
            tmp5 += tmp6;

            //Sub-expression: LLDI0339 = Power[LLDI0338,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI033A = Plus[LLDI0335,LLDI0339]
            tmp1 += tmp5;

            //Sub-expression: LLDI033C = Plus[LLDI033B,LLDI019F]
            tmp5 = tmp46 + tmp68;

            //Sub-expression: LLDI033D = Times[Rational[1,2],LLDI000F,LLDI031F]
            tmp6 = 0.5 * Scalars[6] * tmp4;

            //Sub-expression: LLDI033E = Plus[LLDI033C,LLDI033D]
            tmp5 += tmp6;

            //Sub-expression: LLDI033F = Power[LLDI033E,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0340 = Plus[LLDI033A,LLDI033F]
            tmp1 += tmp5;

            //Sub-expression: LLDI0342 = Plus[LLDI027F,LLDI0341]
            tmp5 = tmp8 + tmp47;

            //Sub-expression: LLDI0343 = Plus[LLDI0342,LLDI01A8]
            tmp5 += tmp70;

            //Sub-expression: LLDI0344 = Times[Rational[-1,2],LLDI0010,LLDI031F]
            tmp6 = -0.5 * Scalars[7] * tmp4;

            //Sub-expression: LLDI0345 = Plus[LLDI0343,LLDI0344]
            tmp5 += tmp6;

            //Sub-expression: LLDI0346 = Power[LLDI0345,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0347 = Plus[LLDI0340,LLDI0346]
            tmp1 += tmp5;

            //Sub-expression: LLDI0348 = Plus[1,LLDI031E]
            tmp5 = 1 + tmp3;

            //Sub-expression: LLDI0349 = Times[Rational[1,2],LLDI0009,LLDI0348]
            tmp6 = 0.5 * Scalars[0] * tmp5;

            //Sub-expression: LLDI034A = Power[LLDI0349,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI034B = Times[-1,LLDI034A]
            tmp6 = -tmp6;

            //Sub-expression: LLDI034C = Plus[LLDI0347,LLDI034B]
            tmp1 += tmp6;

            //Sub-expression: LLDI034D = Times[Rational[-1,2],LLDI000A,LLDI0348]
            tmp6 = -0.5 * Scalars[1] * tmp5;

            //Sub-expression: LLDI034E = Plus[LLDI028C,LLDI034D]
            tmp6 = tmp28 + tmp6;

            //Sub-expression: LLDI034F = Power[LLDI034E,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0350 = Times[-1,LLDI034F]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0351 = Plus[LLDI034C,LLDI0350]
            tmp1 += tmp6;

            //Sub-expression: LLDI0353 = Times[Rational[-1,2],LLDI000B,LLDI0348]
            tmp6 = -0.5 * Scalars[2] * tmp5;

            //Sub-expression: LLDI0354 = Plus[LLDI0352,LLDI0353]
            tmp6 = tmp48 + tmp6;

            //Sub-expression: LLDI0355 = Power[LLDI0354,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0356 = Times[-1,LLDI0355]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0357 = Plus[LLDI0351,LLDI0356]
            tmp1 += tmp6;

            //Sub-expression: LLDI0359 = Plus[LLDI0297,LLDI0358]
            tmp6 = tmp10 + tmp49;

            //Sub-expression: LLDI035A = Times[Rational[1,2],LLDI000C,LLDI0348]
            tmp7 = 0.5 * Scalars[3] * tmp5;

            //Sub-expression: LLDI035B = Plus[LLDI0359,LLDI035A]
            tmp6 += tmp7;

            //Sub-expression: LLDI035C = Power[LLDI035B,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI035D = Times[-1,LLDI035C]
            tmp6 = -tmp6;

            //Sub-expression: LLDI035E = Plus[LLDI0357,LLDI035D]
            tmp1 += tmp6;

            //Sub-expression: LLDI035F = Times[Rational[-1,2],LLDI000D,LLDI0348]
            tmp6 = -0.5 * Scalars[4] * tmp5;

            //Sub-expression: LLDI0360 = Plus[LLDI01C7,LLDI035F]
            tmp6 = tmp11 + tmp6;

            //Sub-expression: LLDI0361 = Power[LLDI0360,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0362 = Times[-1,LLDI0361]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0363 = Plus[LLDI035E,LLDI0362]
            tmp1 += tmp6;

            //Sub-expression: LLDI0364 = Plus[LLDI02A4,LLDI01CE]
            tmp6 = tmp29 + tmp72;

            //Sub-expression: LLDI0365 = Times[Rational[1,2],LLDI000E,LLDI0348]
            tmp7 = 0.5 * Scalars[5] * tmp5;

            //Sub-expression: LLDI0366 = Plus[LLDI0364,LLDI0365]
            tmp6 += tmp7;

            //Sub-expression: LLDI0367 = Power[LLDI0366,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0368 = Times[-1,LLDI0367]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0369 = Plus[LLDI0363,LLDI0368]
            tmp1 += tmp6;

            //Sub-expression: LLDI036B = Plus[LLDI036A,LLDI01D6]
            tmp6 = tmp50 + tmp73;

            //Sub-expression: LLDI036C = Times[Rational[1,2],LLDI000F,LLDI0348]
            tmp7 = 0.5 * Scalars[6] * tmp5;

            //Sub-expression: LLDI036D = Plus[LLDI036B,LLDI036C]
            tmp6 += tmp7;

            //Sub-expression: LLDI036E = Power[LLDI036D,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI036F = Times[-1,LLDI036E]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0370 = Plus[LLDI0369,LLDI036F]
            tmp1 += tmp6;

            //Sub-expression: LLDI0372 = Plus[LLDI02B3,LLDI0371]
            tmp6 = tmp12 + tmp51;

            //Sub-expression: LLDI0373 = Plus[LLDI0372,LLDI01E0]
            tmp6 += tmp75;

            //Sub-expression: LLDI0374 = Times[Rational[-1,2],LLDI0010,LLDI0348]
            tmp7 = -0.5 * Scalars[7] * tmp5;

            //Sub-expression: LLDI0375 = Plus[LLDI0373,LLDI0374]
            tmp6 += tmp7;

            //Sub-expression: LLDI0376 = Power[LLDI0375,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0377 = Times[-1,LLDI0376]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0378 = Plus[LLDI0370,LLDI0377]
            tmp1 += tmp6;

            //Sub-expression: LLDI0379 = Times[LLDI0019,LLDI031F]
            tmp6 = Scalars[16] * tmp4;

            //Sub-expression: LLDI037A = Times[-1,LLDI0011,LLDI0348]
            tmp7 = -1 * Scalars[8] * tmp5;

            //Sub-expression: LLDI037B = Plus[LLDI0379,LLDI037A]
            tmp6 += tmp7;

            //Sub-expression: LLDI037C = Times[Rational[1,2],LLDI037B]
            tmp6 = 0.5 * tmp6;

            //Sub-expression: LLDI037D = Power[LLDI037C,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI037E = Times[-1,LLDI037D]
            tmp6 = -tmp6;

            //Sub-expression: LLDI037F = Plus[LLDI0378,LLDI037E]
            tmp1 += tmp6;

            //Sub-expression: LLDI0380 = Times[LLDI0012,LLDI031E]
            tmp6 = Scalars[9] * tmp3;

            //Sub-expression: LLDI0381 = Plus[LLDI02C4,LLDI0380]
            tmp6 = tmp14 + tmp6;

            //Sub-expression: LLDI0382 = Times[-1,LLDI001A,LLDI031E]
            tmp7 = -1 * Scalars[17] * tmp3;

            //Sub-expression: LLDI0383 = Plus[LLDI0381,LLDI0382]
            tmp6 += tmp7;

            //Sub-expression: LLDI0384 = Times[Rational[1,2],LLDI0383]
            tmp6 = 0.5 * tmp6;

            //Sub-expression: LLDI0385 = Power[LLDI0384,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0386 = Times[-1,LLDI0385]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0387 = Plus[LLDI037F,LLDI0386]
            tmp1 += tmp6;

            //Sub-expression: LLDI038A = Times[LLDI0013,LLDI031E]
            tmp6 = Scalars[10] * tmp3;

            //Sub-expression: LLDI038B = Plus[LLDI0389,LLDI038A]
            tmp6 = tmp52 + tmp6;

            //Sub-expression: LLDI038C = Times[-1,LLDI001B,LLDI031E]
            tmp7 = -1 * Scalars[18] * tmp3;

            //Sub-expression: LLDI038D = Plus[LLDI038B,LLDI038C]
            tmp6 += tmp7;

            //Sub-expression: LLDI038E = Times[Rational[1,2],LLDI038D]
            tmp6 = 0.5 * tmp6;

            //Sub-expression: LLDI038F = Power[LLDI038E,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0390 = Times[-1,LLDI038F]
            tmp6 = -tmp6;

            //Sub-expression: LLDI0391 = Plus[LLDI0387,LLDI0390]
            tmp1 += tmp6;

            //Sub-expression: LLDI0393 = Plus[LLDI02D5,LLDI0392]
            tmp6 = tmp13 + tmp53;

            //Sub-expression: LLDI0394 = Times[Rational[1,2],LLDI001C,LLDI031F]
            tmp7 = 0.5 * Scalars[19] * tmp4;

            //Sub-expression: LLDI0395 = Plus[LLDI0393,LLDI0394]
            tmp6 += tmp7;

            //Sub-expression: LLDI0396 = Times[Rational[-1,2],LLDI0014,LLDI0348]
            tmp7 = -0.5 * Scalars[11] * tmp5;

            //Sub-expression: LLDI0397 = Plus[LLDI0395,LLDI0396]
            tmp6 += tmp7;

            //Sub-expression: LLDI0398 = Power[LLDI0397,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI0399 = Times[-1,LLDI0398]
            tmp6 = -tmp6;

            //Sub-expression: LLDI039A = Plus[LLDI0391,LLDI0399]
            tmp1 += tmp6;

            //Sub-expression: LLDI039B = Times[LLDI0015,LLDI031E]
            tmp6 = Scalars[12] * tmp3;

            //Sub-expression: LLDI039C = Plus[LLDI0210,LLDI039B]
            tmp6 = tmp16 + tmp6;

            //Sub-expression: LLDI039D = Times[-1,LLDI001D,LLDI031E]
            tmp7 = -1 * Scalars[20] * tmp3;

            //Sub-expression: LLDI039E = Plus[LLDI039C,LLDI039D]
            tmp6 += tmp7;

            //Sub-expression: LLDI039F = Times[Rational[1,2],LLDI039E]
            tmp6 = 0.5 * tmp6;

            //Sub-expression: LLDI03A0 = Power[LLDI039F,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI03A1 = Times[-1,LLDI03A0]
            tmp6 = -tmp6;

            //Sub-expression: LLDI03A2 = Plus[LLDI039A,LLDI03A1]
            tmp1 += tmp6;

            //Sub-expression: LLDI03A3 = Plus[LLDI02E8,LLDI021A]
            tmp6 = tmp15 + tmp17;

            //Sub-expression: LLDI03A4 = Times[Rational[1,2],LLDI001E,LLDI031F]
            tmp7 = 0.5 * Scalars[21] * tmp4;

            //Sub-expression: LLDI03A5 = Plus[LLDI03A3,LLDI03A4]
            tmp6 += tmp7;

            //Sub-expression: LLDI03A6 = Times[Rational[-1,2],LLDI0016,LLDI0348]
            tmp7 = -0.5 * Scalars[13] * tmp5;

            //Sub-expression: LLDI03A7 = Plus[LLDI03A5,LLDI03A6]
            tmp6 += tmp7;

            //Sub-expression: LLDI03A8 = Power[LLDI03A7,2]
            tmp6 *= tmp6;

            //Sub-expression: LLDI03A9 = Times[-1,LLDI03A8]
            tmp6 = -tmp6;

            //Sub-expression: LLDI03AA = Plus[LLDI03A2,LLDI03A9]
            tmp1 += tmp6;

            //Sub-expression: LLDI03AC = Plus[LLDI03AB,LLDI0224]
            tmp6 = tmp54 + tmp19;

            //Sub-expression: LLDI03AD = Times[Rational[1,2],LLDI001F,LLDI031F]
            tmp4 = 0.5 * Scalars[22] * tmp4;

            //Sub-expression: LLDI03AE = Plus[LLDI03AC,LLDI03AD]
            tmp4 = tmp6 + tmp4;

            //Sub-expression: LLDI03AF = Times[Rational[-1,2],LLDI0017,LLDI0348]
            tmp5 = -0.5 * Scalars[14] * tmp5;

            //Sub-expression: LLDI03B0 = Plus[LLDI03AE,LLDI03AF]
            tmp4 += tmp5;

            //Sub-expression: LLDI03B1 = Power[LLDI03B0,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI03B2 = Times[-1,LLDI03B1]
            tmp4 = -tmp4;

            //Sub-expression: LLDI03B3 = Plus[LLDI03AA,LLDI03B2]
            tmp1 += tmp4;

            //Sub-expression: LLDI03B5 = Plus[LLDI02FC,LLDI03B4]
            tmp0 += tmp38;

            //Sub-expression: LLDI03B6 = Plus[LLDI03B5,LLDI0232]
            tmp0 += tmp2;

            //Sub-expression: LLDI03B7 = Times[LLDI0018,LLDI031E]
            tmp2 = Scalars[15] * tmp3;

            //Sub-expression: LLDI03B8 = Plus[LLDI03B6,LLDI03B7]
            tmp0 += tmp2;

            //Sub-expression: LLDI03B9 = Times[-1,LLDI0020,LLDI031E]
            tmp2 = -1 * Scalars[23] * tmp3;

            //Sub-expression: LLDI03BA = Plus[LLDI03B8,LLDI03B9]
            tmp0 += tmp2;

            //Sub-expression: LLDI03BB = Times[Rational[1,2],LLDI03BA]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI03BC = Power[LLDI03BB,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI03BD = Times[-1,LLDI03BC]
            tmp0 = -tmp0;

            //Output: LLDI0003 = Plus[LLDI03B3,LLDI03BD]
            var d3 = tmp1 + tmp0;


            //Finish GMac Macro Code Generation, 2019-09-12T13:48:25.0663263+02:00


            d1 = CorrectSdf(d1);
            d2 = CorrectSdf(d2);
            d3 = CorrectSdf(d3);
            d4 = CorrectSdf(d4);

            return new Float64Tuple3D(
                d4 + d1 - d2 - d3,
                d4 - d1 - d2 + d3,
                d4 - d1 + d2 - d3
            ).ToUnitVector();
        }

        private Float64Tuple3D GetIpnsNormal(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T13:49:30.9667858+02:00
            //Macro: main.cga5d.GetNormalIpns
            //Input Variables: 35 used, 1 not used, 36 total.
            //Temp Variables: 789 sub-expressions, 0 generated temps, 789 total.
            //Target Temp Variables: 75 total.
            //Output Variables: 4 total.
            //Computations: 1.17402269861286 average, 931 total.
            //Memory Reads: 1.72383354350567 average, 1367 total.
            //Memory Writes: 793 total.
            //
            //Macro Binding Data: 
            //   result.d1 = variable: var d1
            //   result.d2 = variable: var d2
            //   result.d3 = variable: var d3
            //   result.d4 = variable: var d4
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   distanceDelta = variable: DistanceDelta
            //   mv.#E0# = variable: Scalars[0]
            //   mv.#e1# = variable: Scalars[1]
            //   mv.#e2# = variable: Scalars[2]
            //   mv.#e1^e2# = variable: Scalars[3]
            //   mv.#e3# = variable: Scalars[4]
            //   mv.#e1^e3# = variable: Scalars[5]
            //   mv.#e2^e3# = variable: Scalars[6]
            //   mv.#e1^e2^e3# = variable: Scalars[7]
            //   mv.#ep# = variable: Scalars[8]
            //   mv.#e1^ep# = variable: Scalars[9]
            //   mv.#e2^ep# = variable: Scalars[10]
            //   mv.#e1^e2^ep# = variable: Scalars[11]
            //   mv.#e3^ep# = variable: Scalars[12]
            //   mv.#e1^e3^ep# = variable: Scalars[13]
            //   mv.#e2^e3^ep# = variable: Scalars[14]
            //   mv.#e1^e2^e3^ep# = variable: Scalars[15]
            //   mv.#en# = variable: Scalars[16]
            //   mv.#e1^en# = variable: Scalars[17]
            //   mv.#e2^en# = variable: Scalars[18]
            //   mv.#e1^e2^en# = variable: Scalars[19]
            //   mv.#e3^en# = variable: Scalars[20]
            //   mv.#e1^e3^en# = variable: Scalars[21]
            //   mv.#e2^e3^en# = variable: Scalars[22]
            //   mv.#e1^e2^e3^en# = variable: Scalars[23]
            //   mv.#ep^en# = variable: Scalars[24]
            //   mv.#e1^ep^en# = variable: Scalars[25]
            //   mv.#e2^ep^en# = variable: Scalars[26]
            //   mv.#e1^e2^ep^en# = variable: Scalars[27]
            //   mv.#e3^ep^en# = variable: Scalars[28]
            //   mv.#e1^e3^ep^en# = variable: Scalars[29]
            //   mv.#e2^e3^ep^en# = variable: Scalars[30]
            //   mv.#e1^e2^e3^ep^en# = variable: Scalars[31]

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
            double tmp13;
            double tmp14;
            double tmp15;
            double tmp16;
            double tmp17;
            double tmp18;
            double tmp19;
            double tmp20;
            double tmp21;
            double tmp22;
            double tmp23;
            double tmp24;
            double tmp25;
            double tmp26;
            double tmp27;
            double tmp28;
            double tmp29;
            double tmp30;
            double tmp31;
            double tmp32;
            double tmp33;
            double tmp34;
            double tmp35;
            double tmp36;
            double tmp37;
            double tmp38;
            double tmp39;
            double tmp40;
            double tmp41;
            double tmp42;
            double tmp43;
            double tmp44;
            double tmp45;
            double tmp46;
            double tmp47;
            double tmp48;
            double tmp49;
            double tmp50;
            double tmp51;
            double tmp52;
            double tmp53;
            double tmp54;
            double tmp55;
            double tmp56;
            double tmp57;
            double tmp58;
            double tmp59;
            double tmp60;
            double tmp61;
            double tmp62;
            double tmp63;
            double tmp64;
            double tmp65;
            double tmp66;
            double tmp67;
            double tmp68;
            double tmp69;
            double tmp70;
            double tmp71;
            double tmp72;
            double tmp73;

            //Sub-expression: LLDI0156 = Plus[LLDI0005,LLDI0008]
            var tmp0 = point.X + SdfDistanceDelta;

            //Sub-expression: LLDI0157 = Times[LLDI000A,LLDI0156]
            tmp1 = Scalars[1] * tmp0;

            //Sub-expression: LLDI015F = Power[LLDI0156,2]
            tmp2 = tmp0 * tmp0;

            //Sub-expression: LLDI016B = Plus[LLDI0012,LLDI001A]
            tmp3 = Scalars[9] + Scalars[17];

            //Sub-expression: LLDI0177 = Plus[LLDI0013,LLDI001B]
            tmp4 = Scalars[10] + Scalars[18];

            //Sub-expression: LLDI0178 = Times[2,LLDI000C,LLDI0156]
            tmp5 = 2 * Scalars[3] * tmp0;

            //Sub-expression: LLDI0179 = Plus[LLDI0177,LLDI0178]
            tmp5 = tmp4 + tmp5;

            //Sub-expression: LLDI018A = Plus[LLDI0015,LLDI001D]
            tmp6 = Scalars[12] + Scalars[20];

            //Sub-expression: LLDI018B = Times[2,LLDI000E,LLDI0156]
            tmp7 = 2 * Scalars[5] * tmp0;

            //Sub-expression: LLDI018C = Plus[LLDI018A,LLDI018B]
            tmp7 = tmp6 + tmp7;

            //Sub-expression: LLDI019E = Times[LLDI0010,LLDI0156]
            tmp8 = Scalars[7] * tmp0;

            //Sub-expression: LLDI01A5 = Plus[LLDI0018,LLDI0020]
            tmp9 = Scalars[15] + Scalars[23];

            //Sub-expression: LLDI01AD = Times[LLDI0012,LLDI0156]
            tmp10 = Scalars[9] * tmp0;

            //Sub-expression: LLDI01BD = Times[LLDI0014,LLDI0156]
            tmp11 = Scalars[11] * tmp0;

            //Sub-expression: LLDI01C9 = Times[LLDI0016,LLDI0156]
            tmp12 = Scalars[13] * tmp0;

            //Sub-expression: LLDI01D7 = Times[LLDI0018,LLDI0156]
            tmp13 = Scalars[15] * tmp0;

            //Sub-expression: LLDI01DF = Times[LLDI001A,LLDI0156]
            tmp14 = Scalars[17] * tmp0;

            //Sub-expression: LLDI01F4 = Times[LLDI001C,LLDI0156]
            tmp15 = Scalars[19] * tmp0;

            //Sub-expression: LLDI0202 = Times[LLDI001E,LLDI0156]
            tmp16 = Scalars[21] * tmp0;

            //Sub-expression: LLDI0210 = Times[LLDI0020,LLDI0156]
            tmp17 = Scalars[23] * tmp0;

            //Sub-expression: LLDI021A = Times[LLDI0022,LLDI0156]
            tmp18 = Scalars[25] * tmp0;

            //Sub-expression: LLDI0228 = Times[LLDI0024,LLDI0156]
            tmp19 = Scalars[27] * tmp0;

            //Sub-expression: LLDI0232 = Times[LLDI0026,LLDI0156]
            tmp20 = Scalars[29] * tmp0;

            //Sub-expression: LLDI023C = Times[LLDI0028,LLDI0156]
            tmp0 = Scalars[31] * tmp0;

            //Sub-expression: LLDI023D = Power[LLDI023C,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI023E = Times[-1,LLDI023D]
            tmp0 = -tmp0;

            //Sub-expression: LLDI0242 = Plus[LLDI0007,LLDI0008]
            tmp21 = point.Z + SdfDistanceDelta;

            //Sub-expression: LLDI0243 = Times[LLDI000D,LLDI0242]
            tmp22 = Scalars[4] * tmp21;

            //Sub-expression: LLDI0247 = Power[LLDI0242,2]
            tmp23 = tmp21 * tmp21;

            //Sub-expression: LLDI0250 = Times[-2,LLDI000E,LLDI0242]
            tmp24 = -2 * Scalars[5] * tmp21;

            //Sub-expression: LLDI025B = Times[-2,LLDI000F,LLDI0242]
            tmp25 = -2 * Scalars[6] * tmp21;

            //Sub-expression: LLDI0264 = Times[LLDI0010,LLDI0242]
            tmp26 = Scalars[7] * tmp21;

            //Sub-expression: LLDI028C = Times[LLDI0015,LLDI0242]
            tmp27 = Scalars[12] * tmp21;

            //Sub-expression: LLDI0292 = Times[-1,LLDI0016,LLDI0242]
            tmp28 = -1 * Scalars[13] * tmp21;

            //Sub-expression: LLDI0299 = Times[-1,LLDI0017,LLDI0242]
            tmp29 = -1 * Scalars[14] * tmp21;

            //Sub-expression: LLDI029F = Times[LLDI0018,LLDI0242]
            tmp30 = Scalars[15] * tmp21;

            //Sub-expression: LLDI02B9 = Times[LLDI001D,LLDI0242]
            tmp31 = Scalars[20] * tmp21;

            //Sub-expression: LLDI02C0 = Times[LLDI001E,LLDI0242]
            tmp32 = Scalars[21] * tmp21;

            //Sub-expression: LLDI02CB = Times[-1,LLDI001F,LLDI0242]
            tmp33 = -1 * Scalars[22] * tmp21;

            //Sub-expression: LLDI02D2 = Times[LLDI0020,LLDI0242]
            tmp34 = Scalars[23] * tmp21;

            //Sub-expression: LLDI02F0 = Times[LLDI0025,LLDI0242]
            tmp35 = Scalars[28] * tmp21;

            //Sub-expression: LLDI02F5 = Times[-1,LLDI0026,LLDI0242]
            tmp36 = -1 * Scalars[29] * tmp21;

            //Sub-expression: LLDI02FB = Times[-1,LLDI0027,LLDI0242]
            tmp37 = -1 * Scalars[30] * tmp21;

            //Sub-expression: LLDI0300 = Times[LLDI0028,LLDI0242]
            tmp21 = Scalars[31] * tmp21;

            //Sub-expression: LLDI0301 = Power[LLDI0300,2]
            tmp21 *= tmp21;

            //Sub-expression: LLDI0302 = Times[-1,LLDI0301]
            tmp21 = -tmp21;

            //Sub-expression: LLDI030D = Plus[LLDI0006,LLDI0008]
            tmp38 = point.Y + SdfDistanceDelta;

            //Sub-expression: LLDI030E = Times[LLDI000B,LLDI030D]
            tmp39 = Scalars[2] * tmp38;

            //Sub-expression: LLDI0311 = Power[LLDI030D,2]
            tmp40 = tmp38 * tmp38;

            //Sub-expression: LLDI031B = Times[-2,LLDI000C,LLDI030D]
            tmp41 = -2 * Scalars[3] * tmp38;

            //Sub-expression: LLDI031C = Plus[LLDI016B,LLDI031B]
            tmp41 = tmp3 + tmp41;

            //Sub-expression: LLDI0333 = Times[2,LLDI000F,LLDI030D]
            tmp42 = 2 * Scalars[6] * tmp38;

            //Sub-expression: LLDI033C = Times[-2,LLDI0010,LLDI030D]
            tmp43 = -2 * Scalars[7] * tmp38;

            //Sub-expression: LLDI0351 = Times[LLDI0013,LLDI030D]
            tmp44 = Scalars[10] * tmp38;

            //Sub-expression: LLDI0358 = Times[-1,LLDI0014,LLDI030D]
            tmp45 = -1 * Scalars[11] * tmp38;

            //Sub-expression: LLDI0367 = Times[LLDI0017,LLDI030D]
            tmp46 = Scalars[14] * tmp38;

            //Sub-expression: LLDI036D = Times[-2,LLDI0018,LLDI030D]
            tmp47 = -2 * Scalars[15] * tmp38;

            //Sub-expression: LLDI036E = Plus[LLDI0026,LLDI036D]
            tmp47 = Scalars[29] + tmp47;

            //Sub-expression: LLDI037B = Times[LLDI001B,LLDI030D]
            tmp48 = Scalars[18] * tmp38;

            //Sub-expression: LLDI0383 = Times[LLDI001C,LLDI030D]
            tmp49 = Scalars[19] * tmp38;

            //Sub-expression: LLDI0398 = Times[LLDI001F,LLDI030D]
            tmp50 = Scalars[22] * tmp38;

            //Sub-expression: LLDI039F = Times[-1,LLDI0020,LLDI030D]
            tmp51 = -1 * Scalars[23] * tmp38;

            //Sub-expression: LLDI03AE = Times[LLDI0023,LLDI030D]
            tmp52 = Scalars[26] * tmp38;

            //Sub-expression: LLDI03B4 = Times[-1,LLDI0024,LLDI030D]
            tmp53 = -1 * Scalars[27] * tmp38;

            //Sub-expression: LLDI03BE = Times[LLDI0027,LLDI030D]
            tmp54 = Scalars[30] * tmp38;

            //Sub-expression: LLDI03C3 = Times[-1,LLDI0028,LLDI030D]
            tmp38 = -1 * Scalars[31] * tmp38;

            //Sub-expression: LLDI03C4 = Power[LLDI03C3,2]
            tmp38 *= tmp38;

            //Sub-expression: LLDI03C5 = Times[-1,LLDI03C4]
            tmp38 = -tmp38;

            //Sub-expression: LLDI03C7 = Plus[LLDI0157,LLDI030E]
            tmp55 = tmp1 + tmp39;

            //Sub-expression: LLDI03C8 = Plus[LLDI03C7,LLDI0243]
            tmp55 += tmp22;

            //Sub-expression: LLDI03C9 = Plus[LLDI015F,LLDI0311]
            tmp56 = tmp2 + tmp40;

            //Sub-expression: LLDI03CA = Plus[LLDI03C9,LLDI0247]
            tmp56 += tmp23;

            //Sub-expression: LLDI03CB = Plus[-1,LLDI03CA]
            tmp57 = -1 + tmp56;

            //Sub-expression: LLDI03CC = Times[Rational[1,2],LLDI0011,LLDI03CB]
            tmp58 = 0.5 * Scalars[8] * tmp57;

            //Sub-expression: LLDI03CD = Plus[LLDI03C8,LLDI03CC]
            tmp55 += tmp58;

            //Sub-expression: LLDI03CE = Plus[1,LLDI03CA]
            tmp58 = 1 + tmp56;

            //Sub-expression: LLDI03CF = Times[Rational[-1,2],LLDI0019,LLDI03CE]
            tmp59 = -0.5 * Scalars[16] * tmp58;

            //Sub-expression: LLDI03D0 = Plus[LLDI03CD,LLDI03CF]
            tmp55 += tmp59;

            //Sub-expression: LLDI03D1 = Power[LLDI03D0,2]
            tmp55 *= tmp55;

            //Sub-expression: LLDI03D2 = Plus[LLDI031C,LLDI0250]
            tmp59 = tmp41 + tmp24;

            //Sub-expression: LLDI03D3 = Times[-1,LLDI0012,LLDI03CA]
            tmp60 = -1 * Scalars[9] * tmp56;

            //Sub-expression: LLDI03D4 = Plus[LLDI03D2,LLDI03D3]
            tmp59 += tmp60;

            //Sub-expression: LLDI03D5 = Times[LLDI001A,LLDI03CA]
            tmp60 = Scalars[17] * tmp56;

            //Sub-expression: LLDI03D6 = Plus[LLDI03D4,LLDI03D5]
            tmp59 += tmp60;

            //Sub-expression: LLDI03D7 = Times[Rational[1,2],LLDI03D6]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI03D8 = Power[LLDI03D7,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03D9 = Plus[LLDI03D1,LLDI03D8]
            tmp55 += tmp59;

            //Sub-expression: LLDI03DA = Plus[LLDI0179,LLDI025B]
            tmp59 = tmp5 + tmp25;

            //Sub-expression: LLDI03DB = Times[-1,LLDI0013,LLDI03CA]
            tmp60 = -1 * Scalars[10] * tmp56;

            //Sub-expression: LLDI03DC = Plus[LLDI03DA,LLDI03DB]
            tmp59 += tmp60;

            //Sub-expression: LLDI03DD = Times[LLDI001B,LLDI03CA]
            tmp60 = Scalars[18] * tmp56;

            //Sub-expression: LLDI03DE = Plus[LLDI03DC,LLDI03DD]
            tmp59 += tmp60;

            //Sub-expression: LLDI03DF = Times[Rational[1,2],LLDI03DE]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI03E0 = Power[LLDI03DF,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03E1 = Plus[LLDI03D9,LLDI03E0]
            tmp55 += tmp59;

            //Sub-expression: LLDI03E2 = Times[Rational[1,2],LLDI0014,LLDI03CB]
            tmp59 = 0.5 * Scalars[11] * tmp57;

            //Sub-expression: LLDI03E3 = Plus[LLDI0264,LLDI03E2]
            tmp59 = tmp26 + tmp59;

            //Sub-expression: LLDI03E4 = Times[Rational[-1,2],LLDI001C,LLDI03CE]
            tmp60 = -0.5 * Scalars[19] * tmp58;

            //Sub-expression: LLDI03E5 = Plus[LLDI03E3,LLDI03E4]
            tmp59 += tmp60;

            //Sub-expression: LLDI03E6 = Power[LLDI03E5,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03E7 = Plus[LLDI03E1,LLDI03E6]
            tmp55 += tmp59;

            //Sub-expression: LLDI03E8 = Plus[LLDI018C,LLDI0333]
            tmp59 = tmp7 + tmp42;

            //Sub-expression: LLDI03E9 = Times[-1,LLDI0015,LLDI03CA]
            tmp60 = -1 * Scalars[12] * tmp56;

            //Sub-expression: LLDI03EA = Plus[LLDI03E8,LLDI03E9]
            tmp59 += tmp60;

            //Sub-expression: LLDI03EB = Times[LLDI001D,LLDI03CA]
            tmp60 = Scalars[20] * tmp56;

            //Sub-expression: LLDI03EC = Plus[LLDI03EA,LLDI03EB]
            tmp59 += tmp60;

            //Sub-expression: LLDI03ED = Times[Rational[1,2],LLDI03EC]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI03EE = Power[LLDI03ED,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03EF = Plus[LLDI03E7,LLDI03EE]
            tmp55 += tmp59;

            //Sub-expression: LLDI03F0 = Times[LLDI0016,LLDI03CB]
            tmp59 = Scalars[13] * tmp57;

            //Sub-expression: LLDI03F1 = Plus[LLDI033C,LLDI03F0]
            tmp59 = tmp43 + tmp59;

            //Sub-expression: LLDI03F2 = Times[-1,LLDI001E,LLDI03CE]
            tmp60 = -1 * Scalars[21] * tmp58;

            //Sub-expression: LLDI03F3 = Plus[LLDI03F1,LLDI03F2]
            tmp59 += tmp60;

            //Sub-expression: LLDI03F4 = Times[Rational[1,2],LLDI03F3]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI03F5 = Power[LLDI03F4,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03F6 = Plus[LLDI03EF,LLDI03F5]
            tmp55 += tmp59;

            //Sub-expression: LLDI03F7 = Times[Rational[1,2],LLDI0017,LLDI03CB]
            tmp59 = 0.5 * Scalars[14] * tmp57;

            //Sub-expression: LLDI03F8 = Plus[LLDI019E,LLDI03F7]
            tmp59 = tmp8 + tmp59;

            //Sub-expression: LLDI03F9 = Times[Rational[-1,2],LLDI001F,LLDI03CE]
            tmp60 = -0.5 * Scalars[22] * tmp58;

            //Sub-expression: LLDI03FA = Plus[LLDI03F8,LLDI03F9]
            tmp59 += tmp60;

            //Sub-expression: LLDI03FB = Power[LLDI03FA,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI03FC = Plus[LLDI03F6,LLDI03FB]
            tmp55 += tmp59;

            //Sub-expression: LLDI03FD = Times[-1,LLDI0018,LLDI03CA]
            tmp59 = -1 * Scalars[15] * tmp56;

            //Sub-expression: LLDI03FE = Plus[LLDI01A5,LLDI03FD]
            tmp59 = tmp9 + tmp59;

            //Sub-expression: LLDI03FF = Times[LLDI0020,LLDI03CA]
            tmp60 = Scalars[23] * tmp56;

            //Sub-expression: LLDI0400 = Plus[LLDI03FE,LLDI03FF]
            tmp59 += tmp60;

            //Sub-expression: LLDI0401 = Times[Rational[1,2],LLDI0400]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI0402 = Power[LLDI0401,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0403 = Plus[LLDI03FC,LLDI0402]
            tmp55 += tmp59;

            //Sub-expression: LLDI0404 = Plus[LLDI01AD,LLDI0351]
            tmp59 = tmp10 + tmp44;

            //Sub-expression: LLDI0405 = Plus[LLDI0404,LLDI028C]
            tmp59 += tmp27;

            //Sub-expression: LLDI0406 = Times[Rational[1,2],LLDI0021,LLDI03CE]
            tmp60 = 0.5 * Scalars[24] * tmp58;

            //Sub-expression: LLDI0407 = Plus[LLDI0405,LLDI0406]
            tmp59 += tmp60;

            //Sub-expression: LLDI0408 = Power[LLDI0407,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0409 = Plus[LLDI0403,LLDI0408]
            tmp55 += tmp59;

            //Sub-expression: LLDI040A = Plus[LLDI0358,LLDI0292]
            tmp59 = tmp45 + tmp28;

            //Sub-expression: LLDI040B = Times[Rational[-1,2],LLDI0022,LLDI03CE]
            tmp60 = -0.5 * Scalars[25] * tmp58;

            //Sub-expression: LLDI040C = Plus[LLDI040A,LLDI040B]
            tmp59 += tmp60;

            //Sub-expression: LLDI040D = Power[LLDI040C,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI040E = Plus[LLDI0409,LLDI040D]
            tmp55 += tmp59;

            //Sub-expression: LLDI040F = Plus[LLDI01BD,LLDI0299]
            tmp59 = tmp11 + tmp29;

            //Sub-expression: LLDI0410 = Times[Rational[-1,2],LLDI0023,LLDI03CE]
            tmp60 = -0.5 * Scalars[26] * tmp58;

            //Sub-expression: LLDI0411 = Plus[LLDI040F,LLDI0410]
            tmp59 += tmp60;

            //Sub-expression: LLDI0412 = Power[LLDI0411,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0413 = Plus[LLDI040E,LLDI0412]
            tmp55 += tmp59;

            //Sub-expression: LLDI0414 = Times[Rational[1,2],LLDI0024,LLDI03CE]
            tmp59 = 0.5 * Scalars[27] * tmp58;

            //Sub-expression: LLDI0415 = Plus[LLDI029F,LLDI0414]
            tmp59 = tmp30 + tmp59;

            //Sub-expression: LLDI0416 = Power[LLDI0415,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0417 = Plus[LLDI0413,LLDI0416]
            tmp55 += tmp59;

            //Sub-expression: LLDI0418 = Plus[LLDI01C9,LLDI0367]
            tmp59 = tmp12 + tmp46;

            //Sub-expression: LLDI0419 = Times[Rational[-1,2],LLDI0025,LLDI03CE]
            tmp60 = -0.5 * Scalars[28] * tmp58;

            //Sub-expression: LLDI041A = Plus[LLDI0418,LLDI0419]
            tmp59 += tmp60;

            //Sub-expression: LLDI041B = Power[LLDI041A,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI041C = Plus[LLDI0417,LLDI041B]
            tmp55 += tmp59;

            //Sub-expression: LLDI041D = Times[LLDI0026,LLDI03CA]
            tmp59 = Scalars[29] * tmp56;

            //Sub-expression: LLDI041E = Plus[LLDI036E,LLDI041D]
            tmp59 = tmp47 + tmp59;

            //Sub-expression: LLDI041F = Times[Rational[1,2],LLDI041E]
            tmp59 = 0.5 * tmp59;

            //Sub-expression: LLDI0420 = Power[LLDI041F,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0421 = Plus[LLDI041C,LLDI0420]
            tmp55 += tmp59;

            //Sub-expression: LLDI0422 = Times[Rational[1,2],LLDI0027,LLDI03CE]
            tmp59 = 0.5 * Scalars[30] * tmp58;

            //Sub-expression: LLDI0423 = Plus[LLDI01D7,LLDI0422]
            tmp59 = tmp13 + tmp59;

            //Sub-expression: LLDI0424 = Power[LLDI0423,2]
            tmp59 *= tmp59;

            //Sub-expression: LLDI0425 = Plus[LLDI0421,LLDI0424]
            tmp55 += tmp59;

            //Sub-expression: LLDI0426 = Times[Rational[-1,2],LLDI0028,LLDI03CE]
            tmp58 = -0.5 * Scalars[31] * tmp58;

            //Sub-expression: LLDI0427 = Power[LLDI0426,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI0428 = Plus[LLDI0425,LLDI0427]
            tmp55 += tmp58;

            //Sub-expression: LLDI0429 = Plus[LLDI01DF,LLDI037B]
            tmp58 = tmp14 + tmp48;

            //Sub-expression: LLDI042A = Plus[LLDI0429,LLDI02B9]
            tmp58 += tmp31;

            //Sub-expression: LLDI042B = Times[Rational[1,2],LLDI0021,LLDI03CB]
            tmp59 = 0.5 * Scalars[24] * tmp57;

            //Sub-expression: LLDI042C = Plus[LLDI042A,LLDI042B]
            tmp58 += tmp59;

            //Sub-expression: LLDI042D = Power[LLDI042C,2]
            tmp58 *= tmp58;

            //Sub-expression: LLDI042E = Times[-1,LLDI042D]
            tmp58 = -tmp58;

            //Sub-expression: LLDI042F = Plus[LLDI0428,LLDI042E]
            tmp55 += tmp58;

            //Sub-expression: LLDI0430 = Plus[LLDI0383,LLDI02C0]
            tmp58 = tmp49 + tmp32;

            //Sub-expression: LLDI0431 = Times[-2,LLDI0430]
            tmp58 = -2 * tmp58;

            //Sub-expression: LLDI0432 = Plus[LLDI0022,LLDI0431]
            tmp58 = Scalars[25] + tmp58;

            //Sub-expression: LLDI0433 = Times[-1,LLDI0022,LLDI03CA]
            tmp56 = -1 * Scalars[25] * tmp56;

            //Sub-expression: LLDI0434 = Plus[LLDI0432,LLDI0433]
            tmp56 = tmp58 + tmp56;

            //Sub-expression: LLDI0435 = Times[Rational[1,2],LLDI0434]
            tmp56 = 0.5 * tmp56;

            //Sub-expression: LLDI0436 = Power[LLDI0435,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0437 = Times[-1,LLDI0436]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0438 = Plus[LLDI042F,LLDI0437]
            tmp55 += tmp56;

            //Sub-expression: LLDI0439 = Plus[LLDI01F4,LLDI02CB]
            tmp56 = tmp15 + tmp33;

            //Sub-expression: LLDI043A = Times[Rational[-1,2],LLDI0023,LLDI03CB]
            tmp58 = -0.5 * Scalars[26] * tmp57;

            //Sub-expression: LLDI043B = Plus[LLDI0439,LLDI043A]
            tmp56 += tmp58;

            //Sub-expression: LLDI043C = Power[LLDI043B,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI043D = Times[-1,LLDI043C]
            tmp56 = -tmp56;

            //Sub-expression: LLDI043E = Plus[LLDI0438,LLDI043D]
            tmp55 += tmp56;

            //Sub-expression: LLDI043F = Times[Rational[1,2],LLDI0024,LLDI03CB]
            tmp56 = 0.5 * Scalars[27] * tmp57;

            //Sub-expression: LLDI0440 = Plus[LLDI02D2,LLDI043F]
            tmp56 = tmp34 + tmp56;

            //Sub-expression: LLDI0441 = Power[LLDI0440,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0442 = Times[-1,LLDI0441]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0443 = Plus[LLDI043E,LLDI0442]
            tmp55 += tmp56;

            //Sub-expression: LLDI0444 = Plus[LLDI0202,LLDI0398]
            tmp56 = tmp16 + tmp50;

            //Sub-expression: LLDI0445 = Times[Rational[-1,2],LLDI0025,LLDI03CB]
            tmp58 = -0.5 * Scalars[28] * tmp57;

            //Sub-expression: LLDI0446 = Plus[LLDI0444,LLDI0445]
            tmp56 += tmp58;

            //Sub-expression: LLDI0447 = Power[LLDI0446,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0448 = Times[-1,LLDI0447]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0449 = Plus[LLDI0443,LLDI0448]
            tmp55 += tmp56;

            //Sub-expression: LLDI044A = Times[Rational[1,2],LLDI0026,LLDI03CB]
            tmp56 = 0.5 * Scalars[29] * tmp57;

            //Sub-expression: LLDI044B = Plus[LLDI039F,LLDI044A]
            tmp56 = tmp51 + tmp56;

            //Sub-expression: LLDI044C = Power[LLDI044B,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI044D = Times[-1,LLDI044C]
            tmp56 = -tmp56;

            //Sub-expression: LLDI044E = Plus[LLDI0449,LLDI044D]
            tmp55 += tmp56;

            //Sub-expression: LLDI044F = Times[Rational[1,2],LLDI0027,LLDI03CB]
            tmp56 = 0.5 * Scalars[30] * tmp57;

            //Sub-expression: LLDI0450 = Plus[LLDI0210,LLDI044F]
            tmp56 = tmp17 + tmp56;

            //Sub-expression: LLDI0451 = Power[LLDI0450,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0452 = Times[-1,LLDI0451]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0453 = Plus[LLDI044E,LLDI0452]
            tmp55 += tmp56;

            //Sub-expression: LLDI0454 = Times[Rational[-1,2],LLDI0028,LLDI03CB]
            tmp56 = -0.5 * Scalars[31] * tmp57;

            //Sub-expression: LLDI0455 = Power[LLDI0454,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0456 = Times[-1,LLDI0455]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0457 = Plus[LLDI0453,LLDI0456]
            tmp55 += tmp56;

            //Sub-expression: LLDI0458 = Plus[LLDI021A,LLDI03AE]
            tmp56 = tmp18 + tmp52;

            //Sub-expression: LLDI0459 = Plus[LLDI0458,LLDI02F0]
            tmp56 += tmp35;

            //Sub-expression: LLDI045A = Power[LLDI0459,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI045B = Times[-1,LLDI045A]
            tmp56 = -tmp56;

            //Sub-expression: LLDI045C = Plus[LLDI0457,LLDI045B]
            tmp55 += tmp56;

            //Sub-expression: LLDI045D = Plus[LLDI03B4,LLDI02F5]
            tmp56 = tmp53 + tmp36;

            //Sub-expression: LLDI045E = Power[LLDI045D,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI045F = Times[-1,LLDI045E]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0460 = Plus[LLDI045C,LLDI045F]
            tmp55 += tmp56;

            //Sub-expression: LLDI0461 = Plus[LLDI0228,LLDI02FB]
            tmp56 = tmp19 + tmp37;

            //Sub-expression: LLDI0462 = Power[LLDI0461,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0463 = Times[-1,LLDI0462]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0464 = Plus[LLDI0460,LLDI0463]
            tmp55 += tmp56;

            //Sub-expression: LLDI0465 = Plus[LLDI0464,LLDI0302]
            tmp55 += tmp21;

            //Sub-expression: LLDI0466 = Plus[LLDI0232,LLDI03BE]
            tmp56 = tmp20 + tmp54;

            //Sub-expression: LLDI0467 = Power[LLDI0466,2]
            tmp56 *= tmp56;

            //Sub-expression: LLDI0468 = Times[-1,LLDI0467]
            tmp56 = -tmp56;

            //Sub-expression: LLDI0469 = Plus[LLDI0465,LLDI0468]
            tmp55 += tmp56;

            //Sub-expression: LLDI046A = Plus[LLDI0469,LLDI03C5]
            tmp55 += tmp38;

            //Output: LLDI0004 = Plus[LLDI046A,LLDI023E]
            var d4 = tmp55 + tmp0;

            //Sub-expression: LLDI0158 = Times[-1,LLDI0008]
            tmp55 = -SdfDistanceDelta;

            //Sub-expression: LLDI0159 = Plus[LLDI0006,LLDI0158]
            tmp56 = point.Y + tmp55;

            //Sub-expression: LLDI015A = Times[LLDI000B,LLDI0159]
            tmp57 = Scalars[2] * tmp56;

            //Sub-expression: LLDI015B = Plus[LLDI0157,LLDI015A]
            tmp1 += tmp57;

            //Sub-expression: LLDI015C = Plus[LLDI0007,LLDI0158]
            tmp58 = point.Z + tmp55;

            //Sub-expression: LLDI015D = Times[LLDI000D,LLDI015C]
            tmp59 = Scalars[4] * tmp58;

            //Sub-expression: LLDI015E = Plus[LLDI015B,LLDI015D]
            tmp1 += tmp59;

            //Sub-expression: LLDI0160 = Power[LLDI0159,2]
            tmp60 = tmp56 * tmp56;

            //Sub-expression: LLDI0161 = Plus[LLDI015F,LLDI0160]
            tmp2 += tmp60;

            //Sub-expression: LLDI0162 = Power[LLDI015C,2]
            tmp61 = tmp58 * tmp58;

            //Sub-expression: LLDI0163 = Plus[LLDI0161,LLDI0162]
            tmp2 += tmp61;

            //Sub-expression: LLDI0164 = Plus[-1,LLDI0163]
            tmp62 = -1 + tmp2;

            //Sub-expression: LLDI0165 = Times[Rational[1,2],LLDI0011,LLDI0164]
            tmp63 = 0.5 * Scalars[8] * tmp62;

            //Sub-expression: LLDI0166 = Plus[LLDI015E,LLDI0165]
            tmp1 += tmp63;

            //Sub-expression: LLDI0167 = Plus[1,LLDI0163]
            tmp63 = 1 + tmp2;

            //Sub-expression: LLDI0168 = Times[Rational[-1,2],LLDI0019,LLDI0167]
            tmp64 = -0.5 * Scalars[16] * tmp63;

            //Sub-expression: LLDI0169 = Plus[LLDI0166,LLDI0168]
            tmp1 += tmp64;

            //Sub-expression: LLDI016A = Power[LLDI0169,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI016C = Times[-2,LLDI000C,LLDI0159]
            tmp64 = -2 * Scalars[3] * tmp56;

            //Sub-expression: LLDI016D = Plus[LLDI016B,LLDI016C]
            tmp3 += tmp64;

            //Sub-expression: LLDI016E = Times[-2,LLDI000E,LLDI015C]
            tmp64 = -2 * Scalars[5] * tmp58;

            //Sub-expression: LLDI016F = Plus[LLDI016D,LLDI016E]
            tmp65 = tmp3 + tmp64;

            //Sub-expression: LLDI0170 = Times[-1,LLDI0012,LLDI0163]
            tmp66 = -1 * Scalars[9] * tmp2;

            //Sub-expression: LLDI0171 = Plus[LLDI016F,LLDI0170]
            tmp65 += tmp66;

            //Sub-expression: LLDI0172 = Times[LLDI001A,LLDI0163]
            tmp66 = Scalars[17] * tmp2;

            //Sub-expression: LLDI0173 = Plus[LLDI0171,LLDI0172]
            tmp65 += tmp66;

            //Sub-expression: LLDI0174 = Times[Rational[1,2],LLDI0173]
            tmp65 = 0.5 * tmp65;

            //Sub-expression: LLDI0175 = Power[LLDI0174,2]
            tmp65 *= tmp65;

            //Sub-expression: LLDI0176 = Plus[LLDI016A,LLDI0175]
            tmp1 += tmp65;

            //Sub-expression: LLDI017A = Times[-2,LLDI000F,LLDI015C]
            tmp65 = -2 * Scalars[6] * tmp58;

            //Sub-expression: LLDI017B = Plus[LLDI0179,LLDI017A]
            tmp5 += tmp65;

            //Sub-expression: LLDI017C = Times[-1,LLDI0013,LLDI0163]
            tmp66 = -1 * Scalars[10] * tmp2;

            //Sub-expression: LLDI017D = Plus[LLDI017B,LLDI017C]
            tmp5 += tmp66;

            //Sub-expression: LLDI017E = Times[LLDI001B,LLDI0163]
            tmp66 = Scalars[18] * tmp2;

            //Sub-expression: LLDI017F = Plus[LLDI017D,LLDI017E]
            tmp5 += tmp66;

            //Sub-expression: LLDI0180 = Times[Rational[1,2],LLDI017F]
            tmp5 = 0.5 * tmp5;

            //Sub-expression: LLDI0181 = Power[LLDI0180,2]
            tmp5 *= tmp5;

            //Sub-expression: LLDI0182 = Plus[LLDI0176,LLDI0181]
            tmp1 += tmp5;

            //Sub-expression: LLDI0183 = Times[LLDI0010,LLDI015C]
            tmp5 = Scalars[7] * tmp58;

            //Sub-expression: LLDI0184 = Times[Rational[1,2],LLDI0014,LLDI0164]
            tmp66 = 0.5 * Scalars[11] * tmp62;

            //Sub-expression: LLDI0185 = Plus[LLDI0183,LLDI0184]
            tmp66 = tmp5 + tmp66;

            //Sub-expression: LLDI0186 = Times[Rational[-1,2],LLDI001C,LLDI0167]
            tmp67 = -0.5 * Scalars[19] * tmp63;

            //Sub-expression: LLDI0187 = Plus[LLDI0185,LLDI0186]
            tmp66 += tmp67;

            //Sub-expression: LLDI0188 = Power[LLDI0187,2]
            tmp66 *= tmp66;

            //Sub-expression: LLDI0189 = Plus[LLDI0182,LLDI0188]
            tmp1 += tmp66;

            //Sub-expression: LLDI018D = Times[2,LLDI000F,LLDI0159]
            tmp66 = 2 * Scalars[6] * tmp56;

            //Sub-expression: LLDI018E = Plus[LLDI018C,LLDI018D]
            tmp7 += tmp66;

            //Sub-expression: LLDI018F = Times[-1,LLDI0015,LLDI0163]
            tmp67 = -1 * Scalars[12] * tmp2;

            //Sub-expression: LLDI0190 = Plus[LLDI018E,LLDI018F]
            tmp7 += tmp67;

            //Sub-expression: LLDI0191 = Times[LLDI001D,LLDI0163]
            tmp67 = Scalars[20] * tmp2;

            //Sub-expression: LLDI0192 = Plus[LLDI0190,LLDI0191]
            tmp7 += tmp67;

            //Sub-expression: LLDI0193 = Times[Rational[1,2],LLDI0192]
            tmp7 = 0.5 * tmp7;

            //Sub-expression: LLDI0194 = Power[LLDI0193,2]
            tmp7 *= tmp7;

            //Sub-expression: LLDI0195 = Plus[LLDI0189,LLDI0194]
            tmp1 += tmp7;

            //Sub-expression: LLDI0196 = Times[-2,LLDI0010,LLDI0159]
            tmp7 = -2 * Scalars[7] * tmp56;

            //Sub-expression: LLDI0197 = Times[LLDI0016,LLDI0164]
            tmp67 = Scalars[13] * tmp62;

            //Sub-expression: LLDI0198 = Plus[LLDI0196,LLDI0197]
            tmp67 = tmp7 + tmp67;

            //Sub-expression: LLDI0199 = Times[-1,LLDI001E,LLDI0167]
            tmp68 = -1 * Scalars[21] * tmp63;

            //Sub-expression: LLDI019A = Plus[LLDI0198,LLDI0199]
            tmp67 += tmp68;

            //Sub-expression: LLDI019B = Times[Rational[1,2],LLDI019A]
            tmp67 = 0.5 * tmp67;

            //Sub-expression: LLDI019C = Power[LLDI019B,2]
            tmp67 *= tmp67;

            //Sub-expression: LLDI019D = Plus[LLDI0195,LLDI019C]
            tmp1 += tmp67;

            //Sub-expression: LLDI019F = Times[Rational[1,2],LLDI0017,LLDI0164]
            tmp67 = 0.5 * Scalars[14] * tmp62;

            //Sub-expression: LLDI01A0 = Plus[LLDI019E,LLDI019F]
            tmp8 += tmp67;

            //Sub-expression: LLDI01A1 = Times[Rational[-1,2],LLDI001F,LLDI0167]
            tmp67 = -0.5 * Scalars[22] * tmp63;

            //Sub-expression: LLDI01A2 = Plus[LLDI01A0,LLDI01A1]
            tmp8 += tmp67;

            //Sub-expression: LLDI01A3 = Power[LLDI01A2,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI01A4 = Plus[LLDI019D,LLDI01A3]
            tmp1 += tmp8;

            //Sub-expression: LLDI01A6 = Times[-1,LLDI0018,LLDI0163]
            tmp8 = -1 * Scalars[15] * tmp2;

            //Sub-expression: LLDI01A7 = Plus[LLDI01A5,LLDI01A6]
            tmp8 = tmp9 + tmp8;

            //Sub-expression: LLDI01A8 = Times[LLDI0020,LLDI0163]
            tmp67 = Scalars[23] * tmp2;

            //Sub-expression: LLDI01A9 = Plus[LLDI01A7,LLDI01A8]
            tmp8 += tmp67;

            //Sub-expression: LLDI01AA = Times[Rational[1,2],LLDI01A9]
            tmp8 = 0.5 * tmp8;

            //Sub-expression: LLDI01AB = Power[LLDI01AA,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI01AC = Plus[LLDI01A4,LLDI01AB]
            tmp1 += tmp8;

            //Sub-expression: LLDI01AE = Times[LLDI0013,LLDI0159]
            tmp8 = Scalars[10] * tmp56;

            //Sub-expression: LLDI01AF = Plus[LLDI01AD,LLDI01AE]
            tmp10 += tmp8;

            //Sub-expression: LLDI01B0 = Times[LLDI0015,LLDI015C]
            tmp67 = Scalars[12] * tmp58;

            //Sub-expression: LLDI01B1 = Plus[LLDI01AF,LLDI01B0]
            tmp10 += tmp67;

            //Sub-expression: LLDI01B2 = Times[Rational[1,2],LLDI0021,LLDI0167]
            tmp68 = 0.5 * Scalars[24] * tmp63;

            //Sub-expression: LLDI01B3 = Plus[LLDI01B1,LLDI01B2]
            tmp10 += tmp68;

            //Sub-expression: LLDI01B4 = Power[LLDI01B3,2]
            tmp10 *= tmp10;

            //Sub-expression: LLDI01B5 = Plus[LLDI01AC,LLDI01B4]
            tmp1 += tmp10;

            //Sub-expression: LLDI01B6 = Times[-1,LLDI0014,LLDI0159]
            tmp10 = -1 * Scalars[11] * tmp56;

            //Sub-expression: LLDI01B7 = Times[-1,LLDI0016,LLDI015C]
            tmp68 = -1 * Scalars[13] * tmp58;

            //Sub-expression: LLDI01B8 = Plus[LLDI01B6,LLDI01B7]
            tmp69 = tmp10 + tmp68;

            //Sub-expression: LLDI01B9 = Times[Rational[-1,2],LLDI0022,LLDI0167]
            tmp70 = -0.5 * Scalars[25] * tmp63;

            //Sub-expression: LLDI01BA = Plus[LLDI01B8,LLDI01B9]
            tmp69 += tmp70;

            //Sub-expression: LLDI01BB = Power[LLDI01BA,2]
            tmp69 *= tmp69;

            //Sub-expression: LLDI01BC = Plus[LLDI01B5,LLDI01BB]
            tmp1 += tmp69;

            //Sub-expression: LLDI01BE = Times[-1,LLDI0017,LLDI015C]
            tmp69 = -1 * Scalars[14] * tmp58;

            //Sub-expression: LLDI01BF = Plus[LLDI01BD,LLDI01BE]
            tmp11 += tmp69;

            //Sub-expression: LLDI01C0 = Times[Rational[-1,2],LLDI0023,LLDI0167]
            tmp70 = -0.5 * Scalars[26] * tmp63;

            //Sub-expression: LLDI01C1 = Plus[LLDI01BF,LLDI01C0]
            tmp11 += tmp70;

            //Sub-expression: LLDI01C2 = Power[LLDI01C1,2]
            tmp11 *= tmp11;

            //Sub-expression: LLDI01C3 = Plus[LLDI01BC,LLDI01C2]
            tmp1 += tmp11;

            //Sub-expression: LLDI01C4 = Times[LLDI0018,LLDI015C]
            tmp11 = Scalars[15] * tmp58;

            //Sub-expression: LLDI01C5 = Times[Rational[1,2],LLDI0024,LLDI0167]
            tmp70 = 0.5 * Scalars[27] * tmp63;

            //Sub-expression: LLDI01C6 = Plus[LLDI01C4,LLDI01C5]
            tmp70 = tmp11 + tmp70;

            //Sub-expression: LLDI01C7 = Power[LLDI01C6,2]
            tmp70 *= tmp70;

            //Sub-expression: LLDI01C8 = Plus[LLDI01C3,LLDI01C7]
            tmp1 += tmp70;

            //Sub-expression: LLDI01CA = Times[LLDI0017,LLDI0159]
            tmp70 = Scalars[14] * tmp56;

            //Sub-expression: LLDI01CB = Plus[LLDI01C9,LLDI01CA]
            tmp12 += tmp70;

            //Sub-expression: LLDI01CC = Times[Rational[-1,2],LLDI0025,LLDI0167]
            tmp71 = -0.5 * Scalars[28] * tmp63;

            //Sub-expression: LLDI01CD = Plus[LLDI01CB,LLDI01CC]
            tmp12 += tmp71;

            //Sub-expression: LLDI01CE = Power[LLDI01CD,2]
            tmp12 *= tmp12;

            //Sub-expression: LLDI01CF = Plus[LLDI01C8,LLDI01CE]
            tmp1 += tmp12;

            //Sub-expression: LLDI01D0 = Times[-2,LLDI0018,LLDI0159]
            tmp12 = -2 * Scalars[15] * tmp56;

            //Sub-expression: LLDI01D1 = Plus[LLDI0026,LLDI01D0]
            tmp12 = Scalars[29] + tmp12;

            //Sub-expression: LLDI01D2 = Times[LLDI0026,LLDI0163]
            tmp71 = Scalars[29] * tmp2;

            //Sub-expression: LLDI01D3 = Plus[LLDI01D1,LLDI01D2]
            tmp71 = tmp12 + tmp71;

            //Sub-expression: LLDI01D4 = Times[Rational[1,2],LLDI01D3]
            tmp71 = 0.5 * tmp71;

            //Sub-expression: LLDI01D5 = Power[LLDI01D4,2]
            tmp71 *= tmp71;

            //Sub-expression: LLDI01D6 = Plus[LLDI01CF,LLDI01D5]
            tmp1 += tmp71;

            //Sub-expression: LLDI01D8 = Times[Rational[1,2],LLDI0027,LLDI0167]
            tmp71 = 0.5 * Scalars[30] * tmp63;

            //Sub-expression: LLDI01D9 = Plus[LLDI01D7,LLDI01D8]
            tmp13 += tmp71;

            //Sub-expression: LLDI01DA = Power[LLDI01D9,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI01DB = Plus[LLDI01D6,LLDI01DA]
            tmp1 += tmp13;

            //Sub-expression: LLDI01DC = Times[Rational[-1,2],LLDI0028,LLDI0167]
            tmp13 = -0.5 * Scalars[31] * tmp63;

            //Sub-expression: LLDI01DD = Power[LLDI01DC,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI01DE = Plus[LLDI01DB,LLDI01DD]
            tmp1 += tmp13;

            //Sub-expression: LLDI01E0 = Times[LLDI001B,LLDI0159]
            tmp13 = Scalars[18] * tmp56;

            //Sub-expression: LLDI01E1 = Plus[LLDI01DF,LLDI01E0]
            tmp14 += tmp13;

            //Sub-expression: LLDI01E2 = Times[LLDI001D,LLDI015C]
            tmp63 = Scalars[20] * tmp58;

            //Sub-expression: LLDI01E3 = Plus[LLDI01E1,LLDI01E2]
            tmp14 += tmp63;

            //Sub-expression: LLDI01E4 = Times[Rational[1,2],LLDI0021,LLDI0164]
            tmp71 = 0.5 * Scalars[24] * tmp62;

            //Sub-expression: LLDI01E5 = Plus[LLDI01E3,LLDI01E4]
            tmp14 += tmp71;

            //Sub-expression: LLDI01E6 = Power[LLDI01E5,2]
            tmp14 *= tmp14;

            //Sub-expression: LLDI01E7 = Times[-1,LLDI01E6]
            tmp14 = -tmp14;

            //Sub-expression: LLDI01E8 = Plus[LLDI01DE,LLDI01E7]
            tmp1 += tmp14;

            //Sub-expression: LLDI01E9 = Times[LLDI001C,LLDI0159]
            tmp14 = Scalars[19] * tmp56;

            //Sub-expression: LLDI01EA = Times[LLDI001E,LLDI015C]
            tmp71 = Scalars[21] * tmp58;

            //Sub-expression: LLDI01EB = Plus[LLDI01E9,LLDI01EA]
            tmp72 = tmp14 + tmp71;

            //Sub-expression: LLDI01EC = Times[-2,LLDI01EB]
            tmp72 = -2 * tmp72;

            //Sub-expression: LLDI01ED = Plus[LLDI0022,LLDI01EC]
            tmp72 = Scalars[25] + tmp72;

            //Sub-expression: LLDI01EE = Times[-1,LLDI0022,LLDI0163]
            tmp2 = -1 * Scalars[25] * tmp2;

            //Sub-expression: LLDI01EF = Plus[LLDI01ED,LLDI01EE]
            tmp2 = tmp72 + tmp2;

            //Sub-expression: LLDI01F0 = Times[Rational[1,2],LLDI01EF]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI01F1 = Power[LLDI01F0,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI01F2 = Times[-1,LLDI01F1]
            tmp2 = -tmp2;

            //Sub-expression: LLDI01F3 = Plus[LLDI01E8,LLDI01F2]
            tmp1 += tmp2;

            //Sub-expression: LLDI01F5 = Times[-1,LLDI001F,LLDI015C]
            tmp2 = -1 * Scalars[22] * tmp58;

            //Sub-expression: LLDI01F6 = Plus[LLDI01F4,LLDI01F5]
            tmp15 += tmp2;

            //Sub-expression: LLDI01F7 = Times[Rational[-1,2],LLDI0023,LLDI0164]
            tmp72 = -0.5 * Scalars[26] * tmp62;

            //Sub-expression: LLDI01F8 = Plus[LLDI01F6,LLDI01F7]
            tmp15 += tmp72;

            //Sub-expression: LLDI01F9 = Power[LLDI01F8,2]
            tmp15 *= tmp15;

            //Sub-expression: LLDI01FA = Times[-1,LLDI01F9]
            tmp15 = -tmp15;

            //Sub-expression: LLDI01FB = Plus[LLDI01F3,LLDI01FA]
            tmp1 += tmp15;

            //Sub-expression: LLDI01FC = Times[LLDI0020,LLDI015C]
            tmp15 = Scalars[23] * tmp58;

            //Sub-expression: LLDI01FD = Times[Rational[1,2],LLDI0024,LLDI0164]
            tmp72 = 0.5 * Scalars[27] * tmp62;

            //Sub-expression: LLDI01FE = Plus[LLDI01FC,LLDI01FD]
            tmp72 = tmp15 + tmp72;

            //Sub-expression: LLDI01FF = Power[LLDI01FE,2]
            tmp72 *= tmp72;

            //Sub-expression: LLDI0200 = Times[-1,LLDI01FF]
            tmp72 = -tmp72;

            //Sub-expression: LLDI0201 = Plus[LLDI01FB,LLDI0200]
            tmp1 += tmp72;

            //Sub-expression: LLDI0203 = Times[LLDI001F,LLDI0159]
            tmp72 = Scalars[22] * tmp56;

            //Sub-expression: LLDI0204 = Plus[LLDI0202,LLDI0203]
            tmp16 += tmp72;

            //Sub-expression: LLDI0205 = Times[Rational[-1,2],LLDI0025,LLDI0164]
            tmp73 = -0.5 * Scalars[28] * tmp62;

            //Sub-expression: LLDI0206 = Plus[LLDI0204,LLDI0205]
            tmp16 += tmp73;

            //Sub-expression: LLDI0207 = Power[LLDI0206,2]
            tmp16 *= tmp16;

            //Sub-expression: LLDI0208 = Times[-1,LLDI0207]
            tmp16 = -tmp16;

            //Sub-expression: LLDI0209 = Plus[LLDI0201,LLDI0208]
            tmp1 += tmp16;

            //Sub-expression: LLDI020A = Times[-1,LLDI0020,LLDI0159]
            tmp16 = -1 * Scalars[23] * tmp56;

            //Sub-expression: LLDI020B = Times[Rational[1,2],LLDI0026,LLDI0164]
            tmp73 = 0.5 * Scalars[29] * tmp62;

            //Sub-expression: LLDI020C = Plus[LLDI020A,LLDI020B]
            tmp73 = tmp16 + tmp73;

            //Sub-expression: LLDI020D = Power[LLDI020C,2]
            tmp73 *= tmp73;

            //Sub-expression: LLDI020E = Times[-1,LLDI020D]
            tmp73 = -tmp73;

            //Sub-expression: LLDI020F = Plus[LLDI0209,LLDI020E]
            tmp1 += tmp73;

            //Sub-expression: LLDI0211 = Times[Rational[1,2],LLDI0027,LLDI0164]
            tmp73 = 0.5 * Scalars[30] * tmp62;

            //Sub-expression: LLDI0212 = Plus[LLDI0210,LLDI0211]
            tmp17 += tmp73;

            //Sub-expression: LLDI0213 = Power[LLDI0212,2]
            tmp17 *= tmp17;

            //Sub-expression: LLDI0214 = Times[-1,LLDI0213]
            tmp17 = -tmp17;

            //Sub-expression: LLDI0215 = Plus[LLDI020F,LLDI0214]
            tmp1 += tmp17;

            //Sub-expression: LLDI0216 = Times[Rational[-1,2],LLDI0028,LLDI0164]
            tmp17 = -0.5 * Scalars[31] * tmp62;

            //Sub-expression: LLDI0217 = Power[LLDI0216,2]
            tmp17 *= tmp17;

            //Sub-expression: LLDI0218 = Times[-1,LLDI0217]
            tmp17 = -tmp17;

            //Sub-expression: LLDI0219 = Plus[LLDI0215,LLDI0218]
            tmp1 += tmp17;

            //Sub-expression: LLDI021B = Times[LLDI0023,LLDI0159]
            tmp17 = Scalars[26] * tmp56;

            //Sub-expression: LLDI021C = Plus[LLDI021A,LLDI021B]
            tmp18 += tmp17;

            //Sub-expression: LLDI021D = Times[LLDI0025,LLDI015C]
            tmp62 = Scalars[28] * tmp58;

            //Sub-expression: LLDI021E = Plus[LLDI021C,LLDI021D]
            tmp18 += tmp62;

            //Sub-expression: LLDI021F = Power[LLDI021E,2]
            tmp18 *= tmp18;

            //Sub-expression: LLDI0220 = Times[-1,LLDI021F]
            tmp18 = -tmp18;

            //Sub-expression: LLDI0221 = Plus[LLDI0219,LLDI0220]
            tmp1 += tmp18;

            //Sub-expression: LLDI0222 = Times[-1,LLDI0024,LLDI0159]
            tmp18 = -1 * Scalars[27] * tmp56;

            //Sub-expression: LLDI0223 = Times[-1,LLDI0026,LLDI015C]
            tmp73 = -1 * Scalars[29] * tmp58;

            //Sub-expression: LLDI0224 = Plus[LLDI0222,LLDI0223]
            var tmp74 = tmp18 + tmp73;

            //Sub-expression: LLDI0225 = Power[LLDI0224,2]
            tmp74 *= tmp74;

            //Sub-expression: LLDI0226 = Times[-1,LLDI0225]
            tmp74 = -tmp74;

            //Sub-expression: LLDI0227 = Plus[LLDI0221,LLDI0226]
            tmp1 += tmp74;

            //Sub-expression: LLDI0229 = Times[-1,LLDI0027,LLDI015C]
            tmp74 = -1 * Scalars[30] * tmp58;

            //Sub-expression: LLDI022A = Plus[LLDI0228,LLDI0229]
            tmp19 += tmp74;

            //Sub-expression: LLDI022B = Power[LLDI022A,2]
            tmp19 *= tmp19;

            //Sub-expression: LLDI022C = Times[-1,LLDI022B]
            tmp19 = -tmp19;

            //Sub-expression: LLDI022D = Plus[LLDI0227,LLDI022C]
            tmp1 += tmp19;

            //Sub-expression: LLDI022E = Times[LLDI0028,LLDI015C]
            tmp19 = Scalars[31] * tmp58;

            //Sub-expression: LLDI022F = Power[LLDI022E,2]
            tmp19 *= tmp19;

            //Sub-expression: LLDI0230 = Times[-1,LLDI022F]
            tmp19 = -tmp19;

            //Sub-expression: LLDI0231 = Plus[LLDI022D,LLDI0230]
            tmp1 += tmp19;

            //Sub-expression: LLDI0233 = Times[LLDI0027,LLDI0159]
            tmp58 = Scalars[30] * tmp56;

            //Sub-expression: LLDI0234 = Plus[LLDI0232,LLDI0233]
            tmp20 += tmp58;

            //Sub-expression: LLDI0235 = Power[LLDI0234,2]
            tmp20 *= tmp20;

            //Sub-expression: LLDI0236 = Times[-1,LLDI0235]
            tmp20 = -tmp20;

            //Sub-expression: LLDI0237 = Plus[LLDI0231,LLDI0236]
            tmp1 += tmp20;

            //Sub-expression: LLDI0238 = Times[-1,LLDI0028,LLDI0159]
            tmp20 = -1 * Scalars[31] * tmp56;

            //Sub-expression: LLDI0239 = Power[LLDI0238,2]
            tmp20 *= tmp20;

            //Sub-expression: LLDI023A = Times[-1,LLDI0239]
            tmp20 = -tmp20;

            //Sub-expression: LLDI023B = Plus[LLDI0237,LLDI023A]
            tmp1 += tmp20;

            //Output: LLDI0001 = Plus[LLDI023B,LLDI023E]
            var d1 = tmp1 + tmp0;

            //Sub-expression: LLDI023F = Plus[LLDI0005,LLDI0158]
            tmp0 = point.X + tmp55;

            //Sub-expression: LLDI0240 = Times[LLDI000A,LLDI023F]
            tmp1 = Scalars[1] * tmp0;

            //Sub-expression: LLDI0241 = Plus[LLDI0240,LLDI015A]
            tmp55 = tmp1 + tmp57;

            //Sub-expression: LLDI0244 = Plus[LLDI0241,LLDI0243]
            tmp22 = tmp55 + tmp22;

            //Sub-expression: LLDI0245 = Power[LLDI023F,2]
            tmp55 = tmp0 * tmp0;

            //Sub-expression: LLDI0246 = Plus[LLDI0245,LLDI0160]
            tmp56 = tmp55 + tmp60;

            //Sub-expression: LLDI0248 = Plus[LLDI0246,LLDI0247]
            tmp23 = tmp56 + tmp23;

            //Sub-expression: LLDI0249 = Plus[-1,LLDI0248]
            tmp56 = -1 + tmp23;

            //Sub-expression: LLDI024A = Times[Rational[1,2],LLDI0011,LLDI0249]
            tmp57 = 0.5 * Scalars[8] * tmp56;

            //Sub-expression: LLDI024B = Plus[LLDI0244,LLDI024A]
            tmp22 += tmp57;

            //Sub-expression: LLDI024C = Plus[1,LLDI0248]
            tmp57 = 1 + tmp23;

            //Sub-expression: LLDI024D = Times[Rational[-1,2],LLDI0019,LLDI024C]
            tmp60 = -0.5 * Scalars[16] * tmp57;

            //Sub-expression: LLDI024E = Plus[LLDI024B,LLDI024D]
            tmp22 += tmp60;

            //Sub-expression: LLDI024F = Power[LLDI024E,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0251 = Plus[LLDI016D,LLDI0250]
            tmp3 += tmp24;

            //Sub-expression: LLDI0252 = Times[-1,LLDI0012,LLDI0248]
            tmp24 = -1 * Scalars[9] * tmp23;

            //Sub-expression: LLDI0253 = Plus[LLDI0251,LLDI0252]
            tmp3 += tmp24;

            //Sub-expression: LLDI0254 = Times[LLDI001A,LLDI0248]
            tmp24 = Scalars[17] * tmp23;

            //Sub-expression: LLDI0255 = Plus[LLDI0253,LLDI0254]
            tmp3 += tmp24;

            //Sub-expression: LLDI0256 = Times[Rational[1,2],LLDI0255]
            tmp3 = 0.5 * tmp3;

            //Sub-expression: LLDI0257 = Power[LLDI0256,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI0258 = Plus[LLDI024F,LLDI0257]
            tmp3 = tmp22 + tmp3;

            //Sub-expression: LLDI0259 = Times[2,LLDI000C,LLDI023F]
            tmp22 = 2 * Scalars[3] * tmp0;

            //Sub-expression: LLDI025A = Plus[LLDI0177,LLDI0259]
            tmp4 += tmp22;

            //Sub-expression: LLDI025C = Plus[LLDI025A,LLDI025B]
            tmp22 = tmp4 + tmp25;

            //Sub-expression: LLDI025D = Times[-1,LLDI0013,LLDI0248]
            tmp24 = -1 * Scalars[10] * tmp23;

            //Sub-expression: LLDI025E = Plus[LLDI025C,LLDI025D]
            tmp22 += tmp24;

            //Sub-expression: LLDI025F = Times[LLDI001B,LLDI0248]
            tmp24 = Scalars[18] * tmp23;

            //Sub-expression: LLDI0260 = Plus[LLDI025E,LLDI025F]
            tmp22 += tmp24;

            //Sub-expression: LLDI0261 = Times[Rational[1,2],LLDI0260]
            tmp22 = 0.5 * tmp22;

            //Sub-expression: LLDI0262 = Power[LLDI0261,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0263 = Plus[LLDI0258,LLDI0262]
            tmp3 += tmp22;

            //Sub-expression: LLDI0265 = Times[Rational[1,2],LLDI0014,LLDI0249]
            tmp22 = 0.5 * Scalars[11] * tmp56;

            //Sub-expression: LLDI0266 = Plus[LLDI0264,LLDI0265]
            tmp22 = tmp26 + tmp22;

            //Sub-expression: LLDI0267 = Times[Rational[-1,2],LLDI001C,LLDI024C]
            tmp24 = -0.5 * Scalars[19] * tmp57;

            //Sub-expression: LLDI0268 = Plus[LLDI0266,LLDI0267]
            tmp22 += tmp24;

            //Sub-expression: LLDI0269 = Power[LLDI0268,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI026A = Plus[LLDI0263,LLDI0269]
            tmp3 += tmp22;

            //Sub-expression: LLDI026B = Times[2,LLDI000E,LLDI023F]
            tmp22 = 2 * Scalars[5] * tmp0;

            //Sub-expression: LLDI026C = Plus[LLDI018A,LLDI026B]
            tmp6 += tmp22;

            //Sub-expression: LLDI026D = Plus[LLDI026C,LLDI018D]
            tmp22 = tmp6 + tmp66;

            //Sub-expression: LLDI026E = Times[-1,LLDI0015,LLDI0248]
            tmp24 = -1 * Scalars[12] * tmp23;

            //Sub-expression: LLDI026F = Plus[LLDI026D,LLDI026E]
            tmp22 += tmp24;

            //Sub-expression: LLDI0270 = Times[LLDI001D,LLDI0248]
            tmp24 = Scalars[20] * tmp23;

            //Sub-expression: LLDI0271 = Plus[LLDI026F,LLDI0270]
            tmp22 += tmp24;

            //Sub-expression: LLDI0272 = Times[Rational[1,2],LLDI0271]
            tmp22 = 0.5 * tmp22;

            //Sub-expression: LLDI0273 = Power[LLDI0272,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0274 = Plus[LLDI026A,LLDI0273]
            tmp3 += tmp22;

            //Sub-expression: LLDI0275 = Times[LLDI0016,LLDI0249]
            tmp22 = Scalars[13] * tmp56;

            //Sub-expression: LLDI0276 = Plus[LLDI0196,LLDI0275]
            tmp7 += tmp22;

            //Sub-expression: LLDI0277 = Times[-1,LLDI001E,LLDI024C]
            tmp22 = -1 * Scalars[21] * tmp57;

            //Sub-expression: LLDI0278 = Plus[LLDI0276,LLDI0277]
            tmp7 += tmp22;

            //Sub-expression: LLDI0279 = Times[Rational[1,2],LLDI0278]
            tmp7 = 0.5 * tmp7;

            //Sub-expression: LLDI027A = Power[LLDI0279,2]
            tmp7 *= tmp7;

            //Sub-expression: LLDI027B = Plus[LLDI0274,LLDI027A]
            tmp3 += tmp7;

            //Sub-expression: LLDI027C = Times[LLDI0010,LLDI023F]
            tmp7 = Scalars[7] * tmp0;

            //Sub-expression: LLDI027D = Times[Rational[1,2],LLDI0017,LLDI0249]
            tmp22 = 0.5 * Scalars[14] * tmp56;

            //Sub-expression: LLDI027E = Plus[LLDI027C,LLDI027D]
            tmp22 = tmp7 + tmp22;

            //Sub-expression: LLDI027F = Times[Rational[-1,2],LLDI001F,LLDI024C]
            tmp24 = -0.5 * Scalars[22] * tmp57;

            //Sub-expression: LLDI0280 = Plus[LLDI027E,LLDI027F]
            tmp22 += tmp24;

            //Sub-expression: LLDI0281 = Power[LLDI0280,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0282 = Plus[LLDI027B,LLDI0281]
            tmp3 += tmp22;

            //Sub-expression: LLDI0283 = Times[-1,LLDI0018,LLDI0248]
            tmp22 = -1 * Scalars[15] * tmp23;

            //Sub-expression: LLDI0284 = Plus[LLDI01A5,LLDI0283]
            tmp22 = tmp9 + tmp22;

            //Sub-expression: LLDI0285 = Times[LLDI0020,LLDI0248]
            tmp24 = Scalars[23] * tmp23;

            //Sub-expression: LLDI0286 = Plus[LLDI0284,LLDI0285]
            tmp22 += tmp24;

            //Sub-expression: LLDI0287 = Times[Rational[1,2],LLDI0286]
            tmp22 = 0.5 * tmp22;

            //Sub-expression: LLDI0288 = Power[LLDI0287,2]
            tmp22 *= tmp22;

            //Sub-expression: LLDI0289 = Plus[LLDI0282,LLDI0288]
            tmp3 += tmp22;

            //Sub-expression: LLDI028A = Times[LLDI0012,LLDI023F]
            tmp22 = Scalars[9] * tmp0;

            //Sub-expression: LLDI028B = Plus[LLDI028A,LLDI01AE]
            tmp8 = tmp22 + tmp8;

            //Sub-expression: LLDI028D = Plus[LLDI028B,LLDI028C]
            tmp8 += tmp27;

            //Sub-expression: LLDI028E = Times[Rational[1,2],LLDI0021,LLDI024C]
            tmp24 = 0.5 * Scalars[24] * tmp57;

            //Sub-expression: LLDI028F = Plus[LLDI028D,LLDI028E]
            tmp8 += tmp24;

            //Sub-expression: LLDI0290 = Power[LLDI028F,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI0291 = Plus[LLDI0289,LLDI0290]
            tmp3 += tmp8;

            //Sub-expression: LLDI0293 = Plus[LLDI01B6,LLDI0292]
            tmp8 = tmp10 + tmp28;

            //Sub-expression: LLDI0294 = Times[Rational[-1,2],LLDI0022,LLDI024C]
            tmp10 = -0.5 * Scalars[25] * tmp57;

            //Sub-expression: LLDI0295 = Plus[LLDI0293,LLDI0294]
            tmp8 += tmp10;

            //Sub-expression: LLDI0296 = Power[LLDI0295,2]
            tmp8 *= tmp8;

            //Sub-expression: LLDI0297 = Plus[LLDI0291,LLDI0296]
            tmp3 += tmp8;

            //Sub-expression: LLDI0298 = Times[LLDI0014,LLDI023F]
            tmp8 = Scalars[11] * tmp0;

            //Sub-expression: LLDI029A = Plus[LLDI0298,LLDI0299]
            tmp10 = tmp8 + tmp29;

            //Sub-expression: LLDI029B = Times[Rational[-1,2],LLDI0023,LLDI024C]
            tmp24 = -0.5 * Scalars[26] * tmp57;

            //Sub-expression: LLDI029C = Plus[LLDI029A,LLDI029B]
            tmp10 += tmp24;

            //Sub-expression: LLDI029D = Power[LLDI029C,2]
            tmp10 *= tmp10;

            //Sub-expression: LLDI029E = Plus[LLDI0297,LLDI029D]
            tmp3 += tmp10;

            //Sub-expression: LLDI02A0 = Times[Rational[1,2],LLDI0024,LLDI024C]
            tmp10 = 0.5 * Scalars[27] * tmp57;

            //Sub-expression: LLDI02A1 = Plus[LLDI029F,LLDI02A0]
            tmp10 = tmp30 + tmp10;

            //Sub-expression: LLDI02A2 = Power[LLDI02A1,2]
            tmp10 *= tmp10;

            //Sub-expression: LLDI02A3 = Plus[LLDI029E,LLDI02A2]
            tmp3 += tmp10;

            //Sub-expression: LLDI02A4 = Times[LLDI0016,LLDI023F]
            tmp10 = Scalars[13] * tmp0;

            //Sub-expression: LLDI02A5 = Plus[LLDI02A4,LLDI01CA]
            tmp24 = tmp10 + tmp70;

            //Sub-expression: LLDI02A6 = Times[Rational[-1,2],LLDI0025,LLDI024C]
            tmp25 = -0.5 * Scalars[28] * tmp57;

            //Sub-expression: LLDI02A7 = Plus[LLDI02A5,LLDI02A6]
            tmp24 += tmp25;

            //Sub-expression: LLDI02A8 = Power[LLDI02A7,2]
            tmp24 *= tmp24;

            //Sub-expression: LLDI02A9 = Plus[LLDI02A3,LLDI02A8]
            tmp3 += tmp24;

            //Sub-expression: LLDI02AA = Times[LLDI0026,LLDI0248]
            tmp24 = Scalars[29] * tmp23;

            //Sub-expression: LLDI02AB = Plus[LLDI01D1,LLDI02AA]
            tmp12 += tmp24;

            //Sub-expression: LLDI02AC = Times[Rational[1,2],LLDI02AB]
            tmp12 = 0.5 * tmp12;

            //Sub-expression: LLDI02AD = Power[LLDI02AC,2]
            tmp12 *= tmp12;

            //Sub-expression: LLDI02AE = Plus[LLDI02A9,LLDI02AD]
            tmp3 += tmp12;

            //Sub-expression: LLDI02AF = Times[LLDI0018,LLDI023F]
            tmp12 = Scalars[15] * tmp0;

            //Sub-expression: LLDI02B0 = Times[Rational[1,2],LLDI0027,LLDI024C]
            tmp24 = 0.5 * Scalars[30] * tmp57;

            //Sub-expression: LLDI02B1 = Plus[LLDI02AF,LLDI02B0]
            tmp24 = tmp12 + tmp24;

            //Sub-expression: LLDI02B2 = Power[LLDI02B1,2]
            tmp24 *= tmp24;

            //Sub-expression: LLDI02B3 = Plus[LLDI02AE,LLDI02B2]
            tmp3 += tmp24;

            //Sub-expression: LLDI02B4 = Times[Rational[-1,2],LLDI0028,LLDI024C]
            tmp24 = -0.5 * Scalars[31] * tmp57;

            //Sub-expression: LLDI02B5 = Power[LLDI02B4,2]
            tmp24 *= tmp24;

            //Sub-expression: LLDI02B6 = Plus[LLDI02B3,LLDI02B5]
            tmp3 += tmp24;

            //Sub-expression: LLDI02B7 = Times[LLDI001A,LLDI023F]
            tmp24 = Scalars[17] * tmp0;

            //Sub-expression: LLDI02B8 = Plus[LLDI02B7,LLDI01E0]
            tmp13 = tmp24 + tmp13;

            //Sub-expression: LLDI02BA = Plus[LLDI02B8,LLDI02B9]
            tmp13 += tmp31;

            //Sub-expression: LLDI02BB = Times[Rational[1,2],LLDI0021,LLDI0249]
            tmp25 = 0.5 * Scalars[24] * tmp56;

            //Sub-expression: LLDI02BC = Plus[LLDI02BA,LLDI02BB]
            tmp13 += tmp25;

            //Sub-expression: LLDI02BD = Power[LLDI02BC,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI02BE = Times[-1,LLDI02BD]
            tmp13 = -tmp13;

            //Sub-expression: LLDI02BF = Plus[LLDI02B6,LLDI02BE]
            tmp3 += tmp13;

            //Sub-expression: LLDI02C1 = Plus[LLDI01E9,LLDI02C0]
            tmp13 = tmp14 + tmp32;

            //Sub-expression: LLDI02C2 = Times[-2,LLDI02C1]
            tmp13 = -2 * tmp13;

            //Sub-expression: LLDI02C3 = Plus[LLDI0022,LLDI02C2]
            tmp13 = Scalars[25] + tmp13;

            //Sub-expression: LLDI02C4 = Times[-1,LLDI0022,LLDI0248]
            tmp14 = -1 * Scalars[25] * tmp23;

            //Sub-expression: LLDI02C5 = Plus[LLDI02C3,LLDI02C4]
            tmp13 += tmp14;

            //Sub-expression: LLDI02C6 = Times[Rational[1,2],LLDI02C5]
            tmp13 = 0.5 * tmp13;

            //Sub-expression: LLDI02C7 = Power[LLDI02C6,2]
            tmp13 *= tmp13;

            //Sub-expression: LLDI02C8 = Times[-1,LLDI02C7]
            tmp13 = -tmp13;

            //Sub-expression: LLDI02C9 = Plus[LLDI02BF,LLDI02C8]
            tmp3 += tmp13;

            //Sub-expression: LLDI02CA = Times[LLDI001C,LLDI023F]
            tmp13 = Scalars[19] * tmp0;

            //Sub-expression: LLDI02CC = Plus[LLDI02CA,LLDI02CB]
            tmp14 = tmp13 + tmp33;

            //Sub-expression: LLDI02CD = Times[Rational[-1,2],LLDI0023,LLDI0249]
            tmp23 = -0.5 * Scalars[26] * tmp56;

            //Sub-expression: LLDI02CE = Plus[LLDI02CC,LLDI02CD]
            tmp14 += tmp23;

            //Sub-expression: LLDI02CF = Power[LLDI02CE,2]
            tmp14 *= tmp14;

            //Sub-expression: LLDI02D0 = Times[-1,LLDI02CF]
            tmp14 = -tmp14;

            //Sub-expression: LLDI02D1 = Plus[LLDI02C9,LLDI02D0]
            tmp3 += tmp14;

            //Sub-expression: LLDI02D3 = Times[Rational[1,2],LLDI0024,LLDI0249]
            tmp14 = 0.5 * Scalars[27] * tmp56;

            //Sub-expression: LLDI02D4 = Plus[LLDI02D2,LLDI02D3]
            tmp14 = tmp34 + tmp14;

            //Sub-expression: LLDI02D5 = Power[LLDI02D4,2]
            tmp14 *= tmp14;

            //Sub-expression: LLDI02D6 = Times[-1,LLDI02D5]
            tmp14 = -tmp14;

            //Sub-expression: LLDI02D7 = Plus[LLDI02D1,LLDI02D6]
            tmp3 += tmp14;

            //Sub-expression: LLDI02D8 = Times[LLDI001E,LLDI023F]
            tmp14 = Scalars[21] * tmp0;

            //Sub-expression: LLDI02D9 = Plus[LLDI02D8,LLDI0203]
            tmp23 = tmp14 + tmp72;

            //Sub-expression: LLDI02DA = Times[Rational[-1,2],LLDI0025,LLDI0249]
            tmp25 = -0.5 * Scalars[28] * tmp56;

            //Sub-expression: LLDI02DB = Plus[LLDI02D9,LLDI02DA]
            tmp23 += tmp25;

            //Sub-expression: LLDI02DC = Power[LLDI02DB,2]
            tmp23 *= tmp23;

            //Sub-expression: LLDI02DD = Times[-1,LLDI02DC]
            tmp23 = -tmp23;

            //Sub-expression: LLDI02DE = Plus[LLDI02D7,LLDI02DD]
            tmp3 += tmp23;

            //Sub-expression: LLDI02DF = Times[Rational[1,2],LLDI0026,LLDI0249]
            tmp23 = 0.5 * Scalars[29] * tmp56;

            //Sub-expression: LLDI02E0 = Plus[LLDI020A,LLDI02DF]
            tmp16 += tmp23;

            //Sub-expression: LLDI02E1 = Power[LLDI02E0,2]
            tmp16 *= tmp16;

            //Sub-expression: LLDI02E2 = Times[-1,LLDI02E1]
            tmp16 = -tmp16;

            //Sub-expression: LLDI02E3 = Plus[LLDI02DE,LLDI02E2]
            tmp3 += tmp16;

            //Sub-expression: LLDI02E4 = Times[LLDI0020,LLDI023F]
            tmp16 = Scalars[23] * tmp0;

            //Sub-expression: LLDI02E5 = Times[Rational[1,2],LLDI0027,LLDI0249]
            tmp23 = 0.5 * Scalars[30] * tmp56;

            //Sub-expression: LLDI02E6 = Plus[LLDI02E4,LLDI02E5]
            tmp23 = tmp16 + tmp23;

            //Sub-expression: LLDI02E7 = Power[LLDI02E6,2]
            tmp23 *= tmp23;

            //Sub-expression: LLDI02E8 = Times[-1,LLDI02E7]
            tmp23 = -tmp23;

            //Sub-expression: LLDI02E9 = Plus[LLDI02E3,LLDI02E8]
            tmp3 += tmp23;

            //Sub-expression: LLDI02EA = Times[Rational[-1,2],LLDI0028,LLDI0249]
            tmp23 = -0.5 * Scalars[31] * tmp56;

            //Sub-expression: LLDI02EB = Power[LLDI02EA,2]
            tmp23 *= tmp23;

            //Sub-expression: LLDI02EC = Times[-1,LLDI02EB]
            tmp23 = -tmp23;

            //Sub-expression: LLDI02ED = Plus[LLDI02E9,LLDI02EC]
            tmp3 += tmp23;

            //Sub-expression: LLDI02EE = Times[LLDI0022,LLDI023F]
            tmp23 = Scalars[25] * tmp0;

            //Sub-expression: LLDI02EF = Plus[LLDI02EE,LLDI021B]
            tmp17 = tmp23 + tmp17;

            //Sub-expression: LLDI02F1 = Plus[LLDI02EF,LLDI02F0]
            tmp17 += tmp35;

            //Sub-expression: LLDI02F2 = Power[LLDI02F1,2]
            tmp17 *= tmp17;

            //Sub-expression: LLDI02F3 = Times[-1,LLDI02F2]
            tmp17 = -tmp17;

            //Sub-expression: LLDI02F4 = Plus[LLDI02ED,LLDI02F3]
            tmp3 += tmp17;

            //Sub-expression: LLDI02F6 = Plus[LLDI0222,LLDI02F5]
            tmp17 = tmp18 + tmp36;

            //Sub-expression: LLDI02F7 = Power[LLDI02F6,2]
            tmp17 *= tmp17;

            //Sub-expression: LLDI02F8 = Times[-1,LLDI02F7]
            tmp17 = -tmp17;

            //Sub-expression: LLDI02F9 = Plus[LLDI02F4,LLDI02F8]
            tmp3 += tmp17;

            //Sub-expression: LLDI02FA = Times[LLDI0024,LLDI023F]
            tmp17 = Scalars[27] * tmp0;

            //Sub-expression: LLDI02FC = Plus[LLDI02FA,LLDI02FB]
            tmp18 = tmp17 + tmp37;

            //Sub-expression: LLDI02FD = Power[LLDI02FC,2]
            tmp18 *= tmp18;

            //Sub-expression: LLDI02FE = Times[-1,LLDI02FD]
            tmp18 = -tmp18;

            //Sub-expression: LLDI02FF = Plus[LLDI02F9,LLDI02FE]
            tmp3 += tmp18;

            //Sub-expression: LLDI0303 = Plus[LLDI02FF,LLDI0302]
            tmp3 += tmp21;

            //Sub-expression: LLDI0304 = Times[LLDI0026,LLDI023F]
            tmp18 = Scalars[29] * tmp0;

            //Sub-expression: LLDI0305 = Plus[LLDI0304,LLDI0233]
            tmp21 = tmp18 + tmp58;

            //Sub-expression: LLDI0306 = Power[LLDI0305,2]
            tmp21 *= tmp21;

            //Sub-expression: LLDI0307 = Times[-1,LLDI0306]
            tmp21 = -tmp21;

            //Sub-expression: LLDI0308 = Plus[LLDI0303,LLDI0307]
            tmp3 += tmp21;

            //Sub-expression: LLDI0309 = Plus[LLDI0308,LLDI023A]
            tmp3 += tmp20;

            //Sub-expression: LLDI030A = Times[LLDI0028,LLDI023F]
            tmp0 = Scalars[31] * tmp0;

            //Sub-expression: LLDI030B = Power[LLDI030A,2]
            tmp0 *= tmp0;

            //Sub-expression: LLDI030C = Times[-1,LLDI030B]
            tmp0 = -tmp0;

            //Output: LLDI0002 = Plus[LLDI0309,LLDI030C]
            var d2 = tmp3 + tmp0;

            //Sub-expression: LLDI030F = Plus[LLDI0240,LLDI030E]
            tmp1 += tmp39;

            //Sub-expression: LLDI0310 = Plus[LLDI030F,LLDI015D]
            tmp1 += tmp59;

            //Sub-expression: LLDI0312 = Plus[LLDI0245,LLDI0311]
            tmp3 = tmp55 + tmp40;

            //Sub-expression: LLDI0313 = Plus[LLDI0312,LLDI0162]
            tmp3 += tmp61;

            //Sub-expression: LLDI0314 = Plus[-1,LLDI0313]
            tmp20 = -1 + tmp3;

            //Sub-expression: LLDI0315 = Times[Rational[1,2],LLDI0011,LLDI0314]
            tmp21 = 0.5 * Scalars[8] * tmp20;

            //Sub-expression: LLDI0316 = Plus[LLDI0310,LLDI0315]
            tmp1 += tmp21;

            //Sub-expression: LLDI0317 = Plus[1,LLDI0313]
            tmp21 = 1 + tmp3;

            //Sub-expression: LLDI0318 = Times[Rational[-1,2],LLDI0019,LLDI0317]
            tmp25 = -0.5 * Scalars[16] * tmp21;

            //Sub-expression: LLDI0319 = Plus[LLDI0316,LLDI0318]
            tmp1 += tmp25;

            //Sub-expression: LLDI031A = Power[LLDI0319,2]
            tmp1 *= tmp1;

            //Sub-expression: LLDI031D = Plus[LLDI031C,LLDI016E]
            tmp25 = tmp41 + tmp64;

            //Sub-expression: LLDI031E = Times[-1,LLDI0012,LLDI0313]
            tmp26 = -1 * Scalars[9] * tmp3;

            //Sub-expression: LLDI031F = Plus[LLDI031D,LLDI031E]
            tmp25 += tmp26;

            //Sub-expression: LLDI0320 = Times[LLDI001A,LLDI0313]
            tmp26 = Scalars[17] * tmp3;

            //Sub-expression: LLDI0321 = Plus[LLDI031F,LLDI0320]
            tmp25 += tmp26;

            //Sub-expression: LLDI0322 = Times[Rational[1,2],LLDI0321]
            tmp25 = 0.5 * tmp25;

            //Sub-expression: LLDI0323 = Power[LLDI0322,2]
            tmp25 *= tmp25;

            //Sub-expression: LLDI0324 = Plus[LLDI031A,LLDI0323]
            tmp1 += tmp25;

            //Sub-expression: LLDI0325 = Plus[LLDI025A,LLDI017A]
            tmp4 += tmp65;

            //Sub-expression: LLDI0326 = Times[-1,LLDI0013,LLDI0313]
            tmp25 = -1 * Scalars[10] * tmp3;

            //Sub-expression: LLDI0327 = Plus[LLDI0325,LLDI0326]
            tmp4 += tmp25;

            //Sub-expression: LLDI0328 = Times[LLDI001B,LLDI0313]
            tmp25 = Scalars[18] * tmp3;

            //Sub-expression: LLDI0329 = Plus[LLDI0327,LLDI0328]
            tmp4 += tmp25;

            //Sub-expression: LLDI032A = Times[Rational[1,2],LLDI0329]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI032B = Power[LLDI032A,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI032C = Plus[LLDI0324,LLDI032B]
            tmp1 += tmp4;

            //Sub-expression: LLDI032D = Times[Rational[1,2],LLDI0014,LLDI0314]
            tmp4 = 0.5 * Scalars[11] * tmp20;

            //Sub-expression: LLDI032E = Plus[LLDI0183,LLDI032D]
            tmp4 = tmp5 + tmp4;

            //Sub-expression: LLDI032F = Times[Rational[-1,2],LLDI001C,LLDI0317]
            tmp5 = -0.5 * Scalars[19] * tmp21;

            //Sub-expression: LLDI0330 = Plus[LLDI032E,LLDI032F]
            tmp4 += tmp5;

            //Sub-expression: LLDI0331 = Power[LLDI0330,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0332 = Plus[LLDI032C,LLDI0331]
            tmp1 += tmp4;

            //Sub-expression: LLDI0334 = Plus[LLDI026C,LLDI0333]
            tmp4 = tmp6 + tmp42;

            //Sub-expression: LLDI0335 = Times[-1,LLDI0015,LLDI0313]
            tmp5 = -1 * Scalars[12] * tmp3;

            //Sub-expression: LLDI0336 = Plus[LLDI0334,LLDI0335]
            tmp4 += tmp5;

            //Sub-expression: LLDI0337 = Times[LLDI001D,LLDI0313]
            tmp5 = Scalars[20] * tmp3;

            //Sub-expression: LLDI0338 = Plus[LLDI0336,LLDI0337]
            tmp4 += tmp5;

            //Sub-expression: LLDI0339 = Times[Rational[1,2],LLDI0338]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI033A = Power[LLDI0339,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI033B = Plus[LLDI0332,LLDI033A]
            tmp1 += tmp4;

            //Sub-expression: LLDI033D = Times[LLDI0016,LLDI0314]
            tmp4 = Scalars[13] * tmp20;

            //Sub-expression: LLDI033E = Plus[LLDI033C,LLDI033D]
            tmp4 = tmp43 + tmp4;

            //Sub-expression: LLDI033F = Times[-1,LLDI001E,LLDI0317]
            tmp5 = -1 * Scalars[21] * tmp21;

            //Sub-expression: LLDI0340 = Plus[LLDI033E,LLDI033F]
            tmp4 += tmp5;

            //Sub-expression: LLDI0341 = Times[Rational[1,2],LLDI0340]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI0342 = Power[LLDI0341,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0343 = Plus[LLDI033B,LLDI0342]
            tmp1 += tmp4;

            //Sub-expression: LLDI0344 = Times[Rational[1,2],LLDI0017,LLDI0314]
            tmp4 = 0.5 * Scalars[14] * tmp20;

            //Sub-expression: LLDI0345 = Plus[LLDI027C,LLDI0344]
            tmp4 = tmp7 + tmp4;

            //Sub-expression: LLDI0346 = Times[Rational[-1,2],LLDI001F,LLDI0317]
            tmp5 = -0.5 * Scalars[22] * tmp21;

            //Sub-expression: LLDI0347 = Plus[LLDI0345,LLDI0346]
            tmp4 += tmp5;

            //Sub-expression: LLDI0348 = Power[LLDI0347,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0349 = Plus[LLDI0343,LLDI0348]
            tmp1 += tmp4;

            //Sub-expression: LLDI034A = Times[-1,LLDI0018,LLDI0313]
            tmp4 = -1 * Scalars[15] * tmp3;

            //Sub-expression: LLDI034B = Plus[LLDI01A5,LLDI034A]
            tmp4 = tmp9 + tmp4;

            //Sub-expression: LLDI034C = Times[LLDI0020,LLDI0313]
            tmp5 = Scalars[23] * tmp3;

            //Sub-expression: LLDI034D = Plus[LLDI034B,LLDI034C]
            tmp4 += tmp5;

            //Sub-expression: LLDI034E = Times[Rational[1,2],LLDI034D]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI034F = Power[LLDI034E,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0350 = Plus[LLDI0349,LLDI034F]
            tmp1 += tmp4;

            //Sub-expression: LLDI0352 = Plus[LLDI028A,LLDI0351]
            tmp4 = tmp22 + tmp44;

            //Sub-expression: LLDI0353 = Plus[LLDI0352,LLDI01B0]
            tmp4 += tmp67;

            //Sub-expression: LLDI0354 = Times[Rational[1,2],LLDI0021,LLDI0317]
            tmp5 = 0.5 * Scalars[24] * tmp21;

            //Sub-expression: LLDI0355 = Plus[LLDI0353,LLDI0354]
            tmp4 += tmp5;

            //Sub-expression: LLDI0356 = Power[LLDI0355,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0357 = Plus[LLDI0350,LLDI0356]
            tmp1 += tmp4;

            //Sub-expression: LLDI0359 = Plus[LLDI0358,LLDI01B7]
            tmp4 = tmp45 + tmp68;

            //Sub-expression: LLDI035A = Times[Rational[-1,2],LLDI0022,LLDI0317]
            tmp5 = -0.5 * Scalars[25] * tmp21;

            //Sub-expression: LLDI035B = Plus[LLDI0359,LLDI035A]
            tmp4 += tmp5;

            //Sub-expression: LLDI035C = Power[LLDI035B,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI035D = Plus[LLDI0357,LLDI035C]
            tmp1 += tmp4;

            //Sub-expression: LLDI035E = Plus[LLDI0298,LLDI01BE]
            tmp4 = tmp8 + tmp69;

            //Sub-expression: LLDI035F = Times[Rational[-1,2],LLDI0023,LLDI0317]
            tmp5 = -0.5 * Scalars[26] * tmp21;

            //Sub-expression: LLDI0360 = Plus[LLDI035E,LLDI035F]
            tmp4 += tmp5;

            //Sub-expression: LLDI0361 = Power[LLDI0360,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0362 = Plus[LLDI035D,LLDI0361]
            tmp1 += tmp4;

            //Sub-expression: LLDI0363 = Times[Rational[1,2],LLDI0024,LLDI0317]
            tmp4 = 0.5 * Scalars[27] * tmp21;

            //Sub-expression: LLDI0364 = Plus[LLDI01C4,LLDI0363]
            tmp4 = tmp11 + tmp4;

            //Sub-expression: LLDI0365 = Power[LLDI0364,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0366 = Plus[LLDI0362,LLDI0365]
            tmp1 += tmp4;

            //Sub-expression: LLDI0368 = Plus[LLDI02A4,LLDI0367]
            tmp4 = tmp10 + tmp46;

            //Sub-expression: LLDI0369 = Times[Rational[-1,2],LLDI0025,LLDI0317]
            tmp5 = -0.5 * Scalars[28] * tmp21;

            //Sub-expression: LLDI036A = Plus[LLDI0368,LLDI0369]
            tmp4 += tmp5;

            //Sub-expression: LLDI036B = Power[LLDI036A,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI036C = Plus[LLDI0366,LLDI036B]
            tmp1 += tmp4;

            //Sub-expression: LLDI036F = Times[LLDI0026,LLDI0313]
            tmp4 = Scalars[29] * tmp3;

            //Sub-expression: LLDI0370 = Plus[LLDI036E,LLDI036F]
            tmp4 = tmp47 + tmp4;

            //Sub-expression: LLDI0371 = Times[Rational[1,2],LLDI0370]
            tmp4 = 0.5 * tmp4;

            //Sub-expression: LLDI0372 = Power[LLDI0371,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0373 = Plus[LLDI036C,LLDI0372]
            tmp1 += tmp4;

            //Sub-expression: LLDI0374 = Times[Rational[1,2],LLDI0027,LLDI0317]
            tmp4 = 0.5 * Scalars[30] * tmp21;

            //Sub-expression: LLDI0375 = Plus[LLDI02AF,LLDI0374]
            tmp4 = tmp12 + tmp4;

            //Sub-expression: LLDI0376 = Power[LLDI0375,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0377 = Plus[LLDI0373,LLDI0376]
            tmp1 += tmp4;

            //Sub-expression: LLDI0378 = Times[Rational[-1,2],LLDI0028,LLDI0317]
            tmp4 = -0.5 * Scalars[31] * tmp21;

            //Sub-expression: LLDI0379 = Power[LLDI0378,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI037A = Plus[LLDI0377,LLDI0379]
            tmp1 += tmp4;

            //Sub-expression: LLDI037C = Plus[LLDI02B7,LLDI037B]
            tmp4 = tmp24 + tmp48;

            //Sub-expression: LLDI037D = Plus[LLDI037C,LLDI01E2]
            tmp4 += tmp63;

            //Sub-expression: LLDI037E = Times[Rational[1,2],LLDI0021,LLDI0314]
            tmp5 = 0.5 * Scalars[24] * tmp20;

            //Sub-expression: LLDI037F = Plus[LLDI037D,LLDI037E]
            tmp4 += tmp5;

            //Sub-expression: LLDI0380 = Power[LLDI037F,2]
            tmp4 *= tmp4;

            //Sub-expression: LLDI0381 = Times[-1,LLDI0380]
            tmp4 = -tmp4;

            //Sub-expression: LLDI0382 = Plus[LLDI037A,LLDI0381]
            tmp1 += tmp4;

            //Sub-expression: LLDI0384 = Plus[LLDI0383,LLDI01EA]
            tmp4 = tmp49 + tmp71;

            //Sub-expression: LLDI0385 = Times[-2,LLDI0384]
            tmp4 = -2 * tmp4;

            //Sub-expression: LLDI0386 = Plus[LLDI0022,LLDI0385]
            tmp4 = Scalars[25] + tmp4;

            //Sub-expression: LLDI0387 = Times[-1,LLDI0022,LLDI0313]
            tmp3 = -1 * Scalars[25] * tmp3;

            //Sub-expression: LLDI0388 = Plus[LLDI0386,LLDI0387]
            tmp3 = tmp4 + tmp3;

            //Sub-expression: LLDI0389 = Times[Rational[1,2],LLDI0388]
            tmp3 = 0.5 * tmp3;

            //Sub-expression: LLDI038A = Power[LLDI0389,2]
            tmp3 *= tmp3;

            //Sub-expression: LLDI038B = Times[-1,LLDI038A]
            tmp3 = -tmp3;

            //Sub-expression: LLDI038C = Plus[LLDI0382,LLDI038B]
            tmp1 += tmp3;

            //Sub-expression: LLDI038D = Plus[LLDI02CA,LLDI01F5]
            tmp2 = tmp13 + tmp2;

            //Sub-expression: LLDI038E = Times[Rational[-1,2],LLDI0023,LLDI0314]
            tmp3 = -0.5 * Scalars[26] * tmp20;

            //Sub-expression: LLDI038F = Plus[LLDI038D,LLDI038E]
            tmp2 += tmp3;

            //Sub-expression: LLDI0390 = Power[LLDI038F,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI0391 = Times[-1,LLDI0390]
            tmp2 = -tmp2;

            //Sub-expression: LLDI0392 = Plus[LLDI038C,LLDI0391]
            tmp1 += tmp2;

            //Sub-expression: LLDI0393 = Times[Rational[1,2],LLDI0024,LLDI0314]
            tmp2 = 0.5 * Scalars[27] * tmp20;

            //Sub-expression: LLDI0394 = Plus[LLDI01FC,LLDI0393]
            tmp2 = tmp15 + tmp2;

            //Sub-expression: LLDI0395 = Power[LLDI0394,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI0396 = Times[-1,LLDI0395]
            tmp2 = -tmp2;

            //Sub-expression: LLDI0397 = Plus[LLDI0392,LLDI0396]
            tmp1 += tmp2;

            //Sub-expression: LLDI0399 = Plus[LLDI02D8,LLDI0398]
            tmp2 = tmp14 + tmp50;

            //Sub-expression: LLDI039A = Times[Rational[-1,2],LLDI0025,LLDI0314]
            tmp3 = -0.5 * Scalars[28] * tmp20;

            //Sub-expression: LLDI039B = Plus[LLDI0399,LLDI039A]
            tmp2 += tmp3;

            //Sub-expression: LLDI039C = Power[LLDI039B,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI039D = Times[-1,LLDI039C]
            tmp2 = -tmp2;

            //Sub-expression: LLDI039E = Plus[LLDI0397,LLDI039D]
            tmp1 += tmp2;

            //Sub-expression: LLDI03A0 = Times[Rational[1,2],LLDI0026,LLDI0314]
            tmp2 = 0.5 * Scalars[29] * tmp20;

            //Sub-expression: LLDI03A1 = Plus[LLDI039F,LLDI03A0]
            tmp2 = tmp51 + tmp2;

            //Sub-expression: LLDI03A2 = Power[LLDI03A1,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03A3 = Times[-1,LLDI03A2]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03A4 = Plus[LLDI039E,LLDI03A3]
            tmp1 += tmp2;

            //Sub-expression: LLDI03A5 = Times[Rational[1,2],LLDI0027,LLDI0314]
            tmp2 = 0.5 * Scalars[30] * tmp20;

            //Sub-expression: LLDI03A6 = Plus[LLDI02E4,LLDI03A5]
            tmp2 = tmp16 + tmp2;

            //Sub-expression: LLDI03A7 = Power[LLDI03A6,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03A8 = Times[-1,LLDI03A7]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03A9 = Plus[LLDI03A4,LLDI03A8]
            tmp1 += tmp2;

            //Sub-expression: LLDI03AA = Times[Rational[-1,2],LLDI0028,LLDI0314]
            tmp2 = -0.5 * Scalars[31] * tmp20;

            //Sub-expression: LLDI03AB = Power[LLDI03AA,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03AC = Times[-1,LLDI03AB]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03AD = Plus[LLDI03A9,LLDI03AC]
            tmp1 += tmp2;

            //Sub-expression: LLDI03AF = Plus[LLDI02EE,LLDI03AE]
            tmp2 = tmp23 + tmp52;

            //Sub-expression: LLDI03B0 = Plus[LLDI03AF,LLDI021D]
            tmp2 += tmp62;

            //Sub-expression: LLDI03B1 = Power[LLDI03B0,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03B2 = Times[-1,LLDI03B1]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03B3 = Plus[LLDI03AD,LLDI03B2]
            tmp1 += tmp2;

            //Sub-expression: LLDI03B5 = Plus[LLDI03B4,LLDI0223]
            tmp2 = tmp53 + tmp73;

            //Sub-expression: LLDI03B6 = Power[LLDI03B5,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03B7 = Times[-1,LLDI03B6]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03B8 = Plus[LLDI03B3,LLDI03B7]
            tmp1 += tmp2;

            //Sub-expression: LLDI03B9 = Plus[LLDI02FA,LLDI0229]
            tmp2 = tmp17 + tmp74;

            //Sub-expression: LLDI03BA = Power[LLDI03B9,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03BB = Times[-1,LLDI03BA]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03BC = Plus[LLDI03B8,LLDI03BB]
            tmp1 += tmp2;

            //Sub-expression: LLDI03BD = Plus[LLDI03BC,LLDI0230]
            tmp1 += tmp19;

            //Sub-expression: LLDI03BF = Plus[LLDI0304,LLDI03BE]
            tmp2 = tmp18 + tmp54;

            //Sub-expression: LLDI03C0 = Power[LLDI03BF,2]
            tmp2 *= tmp2;

            //Sub-expression: LLDI03C1 = Times[-1,LLDI03C0]
            tmp2 = -tmp2;

            //Sub-expression: LLDI03C2 = Plus[LLDI03BD,LLDI03C1]
            tmp1 += tmp2;

            //Sub-expression: LLDI03C6 = Plus[LLDI03C2,LLDI03C5]
            tmp1 += tmp38;

            //Output: LLDI0003 = Plus[LLDI03C6,LLDI030C]
            var d3 = tmp1 + tmp0;


            //Finish GMac Macro Code Generation, 2019-09-12T13:49:30.9727830+02:00

            d1 = CorrectSdf(d1);
            d2 = CorrectSdf(d2);
            d3 = CorrectSdf(d3);
            d4 = CorrectSdf(d4);

            return new Float64Tuple3D(
                d4 + d1 - d2 - d3,
                d4 - d1 - d2 + d3,
                d4 - d1 + d2 - d3
            ).ToUnitVector();
        }

        public override Float64Tuple3D ComputeSdfNormal(IFloat64Tuple3D point)
        {
            return NullSpaceKind == MultivectorNullSpaceKind.OuterProductNullSpace
                ? GetOpnsNormal(point)
                : GetIpnsNormal(point);
        }
    }
}