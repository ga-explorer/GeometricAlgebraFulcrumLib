using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.GeometricAlgebra.CGA5D
{
    public sealed class Cga5D4VectorGeometry 
        : Cga5DkVectorGeometry
    {
        public override int Grade => 4;


        internal Cga5D4VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
            : base(bladeScalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(IFloat64Vector3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-10T11:30:44.4859400+02:00
            //Macro: main.cga5d.Sdf
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 18 sub-expressions, 0 generated temps, 18 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.21052631578947 average, 23 total.
            //Memory Reads: 1.68421052631579 average, 32 total.
            //Memory Writes: 19 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2^e3^ep# = variable: BladeScalars[0]
            //   mv.#e1^e2^e3^en# = variable: BladeScalars[1]
            //   mv.#e1^e2^ep^en# = variable: BladeScalars[2]
            //   mv.#e1^e3^ep^en# = variable: BladeScalars[3]
            //   mv.#e2^e3^ep^en# = variable: BladeScalars[4]

            double tmp0;
            double tmp1;
            double tmp2;

            //Sub-expression: LLDI001D = Plus[LLDI0005,LLDI0006]
            tmp0 = Scalars[0] + Scalars[1];

            //Sub-expression: LLDI001E = Times[2,LLDI0004,LLDI0007]
            tmp1 = 2 * point.Z * Scalars[2];

            //Sub-expression: LLDI001F = Plus[LLDI001D,LLDI001E]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0020 = Times[-2,LLDI0003,LLDI0008]
            tmp1 = -2 * point.Y * Scalars[3];

            //Sub-expression: LLDI0021 = Plus[LLDI001F,LLDI0020]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0022 = Times[2,LLDI0002,LLDI0009]
            tmp1 = 2 * point.X * Scalars[4];

            //Sub-expression: LLDI0023 = Plus[LLDI0021,LLDI0022]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0024 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0025 = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI0026 = Plus[LLDI0024,LLDI0025]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0027 = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI0028 = Plus[LLDI0026,LLDI0027]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0029 = Times[LLDI0005,LLDI0028]
            tmp2 = Scalars[0] * tmp1;

            //Sub-expression: LLDI002A = Plus[LLDI0023,LLDI0029]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI002B = Times[-1,LLDI0006,LLDI0028]
            tmp1 = -1 * Scalars[1] * tmp1;

            //Sub-expression: LLDI002C = Plus[LLDI002A,LLDI002B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002D = Times[Rational[1,2],LLDI002C]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI002E = Power[LLDI002D,2]
            tmp0 = tmp0 * tmp0;

            //Output: LLDI0001 = Times[-1,LLDI002E]
            var sdf = -tmp0;

            return sdf;
        }

        protected override double ComputeSdfIpns(IFloat64Vector3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T00:12:18.2867283+02:00
            //Macro: main.cga5d.SdfIpns
            //Input Variables: 8 used, 0 not used, 8 total.
            //Temp Variables: 66 sub-expressions, 0 generated temps, 66 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 1 total.
            //Computations: 1.16417910447761 average, 78 total.
            //Memory Reads: 1.65671641791045 average, 111 total.
            //Memory Writes: 67 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2^e3^ep# = variable: BladeScalars[0]
            //   mv.#e1^e2^e3^en# = variable: BladeScalars[1]
            //   mv.#e1^e2^ep^en# = variable: BladeScalars[2]
            //   mv.#e1^e3^ep^en# = variable: BladeScalars[3]
            //   mv.#e2^e3^ep^en# = variable: BladeScalars[4]

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            //Sub-expression: LLDI0023 = Plus[LLDI0005,LLDI0006]
            tmp0 = Scalars[0] + Scalars[1];

            //Sub-expression: LLDI0024 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0025 = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI0026 = Plus[LLDI0024,LLDI0025]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0027 = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI0028 = Plus[LLDI0026,LLDI0027]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0029 = Times[-1,LLDI0005,LLDI0028]
            tmp2 = -1 * Scalars[0] * tmp1;

            //Sub-expression: LLDI002A = Plus[LLDI0023,LLDI0029]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI002B = Times[LLDI0006,LLDI0028]
            tmp2 = Scalars[1] * tmp1;

            //Sub-expression: LLDI002C = Plus[LLDI002A,LLDI002B]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI002D = Times[Rational[1,2],LLDI002C]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI002E = Power[LLDI002D,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI002F = Times[LLDI0004,LLDI0005]
            tmp2 = point.Z * Scalars[0];

            //Sub-expression: LLDI0030 = Plus[1,LLDI0028]
            tmp3 = 1 + tmp1;

            //Sub-expression: LLDI0031 = Times[Rational[1,2],LLDI0007,LLDI0030]
            tmp4 = 0.5 * Scalars[2] * tmp3;

            //Sub-expression: LLDI0032 = Plus[LLDI002F,LLDI0031]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI0033 = Power[LLDI0032,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI0034 = Plus[LLDI002E,LLDI0033]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0035 = Times[-2,LLDI0003,LLDI0005]
            tmp2 = -2 * point.Y * Scalars[0];

            //Sub-expression: LLDI0036 = Plus[LLDI0035,LLDI0008]
            tmp2 = tmp2 + Scalars[3];

            //Sub-expression: LLDI0037 = Times[LLDI0008,LLDI0028]
            tmp4 = Scalars[3] * tmp1;

            //Sub-expression: LLDI0038 = Plus[LLDI0036,LLDI0037]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI0039 = Times[Rational[1,2],LLDI0038]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI003A = Power[LLDI0039,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI003B = Plus[LLDI0034,LLDI003A]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI003C = Times[LLDI0002,LLDI0005]
            tmp2 = point.X * Scalars[0];

            //Sub-expression: LLDI003D = Times[Rational[1,2],LLDI0009,LLDI0030]
            tmp3 = 0.5 * Scalars[4] * tmp3;

            //Sub-expression: LLDI003E = Plus[LLDI003C,LLDI003D]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI003F = Power[LLDI003E,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI0040 = Plus[LLDI003B,LLDI003F]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0041 = Times[LLDI0004,LLDI0006]
            tmp2 = point.Z * Scalars[1];

            //Sub-expression: LLDI0042 = Plus[-1,LLDI0028]
            tmp1 = -1 + tmp1;

            //Sub-expression: LLDI0043 = Times[Rational[1,2],LLDI0007,LLDI0042]
            tmp3 = 0.5 * Scalars[2] * tmp1;

            //Sub-expression: LLDI0044 = Plus[LLDI0041,LLDI0043]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0045 = Power[LLDI0044,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI0046 = Times[-1,LLDI0045]
            tmp2 = -tmp2;

            //Sub-expression: LLDI0047 = Plus[LLDI0040,LLDI0046]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI0048 = Times[-1,LLDI0003,LLDI0006]
            tmp2 = -1 * point.Y * Scalars[1];

            //Sub-expression: LLDI0049 = Times[Rational[1,2],LLDI0008,LLDI0042]
            tmp3 = 0.5 * Scalars[3] * tmp1;

            //Sub-expression: LLDI004A = Plus[LLDI0048,LLDI0049]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI004B = Power[LLDI004A,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI004C = Times[-1,LLDI004B]
            tmp2 = -tmp2;

            //Sub-expression: LLDI004D = Plus[LLDI0047,LLDI004C]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI004E = Times[LLDI0002,LLDI0006]
            tmp2 = point.X * Scalars[1];

            //Sub-expression: LLDI004F = Times[Rational[1,2],LLDI0009,LLDI0042]
            tmp1 = 0.5 * Scalars[4] * tmp1;

            //Sub-expression: LLDI0050 = Plus[LLDI004E,LLDI004F]
            tmp1 = tmp2 + tmp1;

            //Sub-expression: LLDI0051 = Power[LLDI0050,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0052 = Times[-1,LLDI0051]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0053 = Plus[LLDI004D,LLDI0052]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0054 = Times[-1,LLDI0003,LLDI0007]
            tmp1 = -1 * point.Y * Scalars[2];

            //Sub-expression: LLDI0055 = Times[-1,LLDI0004,LLDI0008]
            tmp2 = -1 * point.Z * Scalars[3];

            //Sub-expression: LLDI0056 = Plus[LLDI0054,LLDI0055]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0057 = Power[LLDI0056,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0058 = Times[-1,LLDI0057]
            tmp1 = -tmp1;

            //Sub-expression: LLDI0059 = Plus[LLDI0053,LLDI0058]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI005A = Times[LLDI0002,LLDI0007]
            tmp1 = point.X * Scalars[2];

            //Sub-expression: LLDI005B = Times[-1,LLDI0004,LLDI0009]
            tmp2 = -1 * point.Z * Scalars[4];

            //Sub-expression: LLDI005C = Plus[LLDI005A,LLDI005B]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI005D = Power[LLDI005C,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI005E = Times[-1,LLDI005D]
            tmp1 = -tmp1;

            //Sub-expression: LLDI005F = Plus[LLDI0059,LLDI005E]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0060 = Times[LLDI0002,LLDI0008]
            tmp1 = point.X * Scalars[3];

            //Sub-expression: LLDI0061 = Times[LLDI0003,LLDI0009]
            tmp2 = point.Y * Scalars[4];

            //Sub-expression: LLDI0062 = Plus[LLDI0060,LLDI0061]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0063 = Power[LLDI0062,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0064 = Times[-1,LLDI0063]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI005F,LLDI0064]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T00:12:18.3037173+02:00

            return sdf;
        }

        //public override double GetRayStep(Line3D ray, double t0)
        //{
        //    //Begin GMac Macro Code Generation, 2019-09-10T21:49:45.5835283+02:00
        //    //Macro: main.cga5d.GetRayStep
        //    //Input Variables: 13 used, 0 not used, 13 total.
        //    //Temp Variables: 47 sub-expressions, 0 generated temps, 47 total.
        //    //Target Temp Variables: 7 total.
        //    //Output Variables: 2 total.
        //    //Computations: 1.16326530612245 average, 57 total.
        //    //Memory Reads: 1.75510204081633 average, 86 total.
        //    //Memory Writes: 49 total.
        //    //
        //    //Macro Binding Data: 
        //    //   result.sdf0 = variable: var sdf0
        //    //   result.sdf1 = variable: var sdf1
        //    //   mv.#e1^e2^e3^ep# = variable: BladeScalars[0]
        //    //   mv.#e1^e2^e3^en# = variable: BladeScalars[1]
        //    //   mv.#e1^e2^ep^en# = variable: BladeScalars[2]
        //    //   mv.#e1^e3^ep^en# = variable: BladeScalars[3]
        //    //   mv.#e2^e3^ep^en# = variable: BladeScalars[4]
        //    //   rayOrigin.#e1# = variable: ray.OriginX
        //    //   rayOrigin.#e2# = variable: ray.OriginY
        //    //   rayOrigin.#e3# = variable: ray.OriginZ
        //    //   rayDirection.#e1# = variable: ray.DirectionX
        //    //   rayDirection.#e2# = variable: ray.DirectionY
        //    //   rayDirection.#e3# = variable: ray.DirectionZ
        //    //   distanceDelta = variable: DistanceDelta
        //    //   t0 = variable: t0

        //    double tmp0;
        //    double tmp1;
        //    double tmp2;
        //    double tmp3;
        //    double tmp4;
        //    double tmp5;
        //    double tmp6;

        //    //Sub-expression: LLDI003D = Plus[LLDI0003,LLDI0004]
        //    tmp0 = BladeScalars[0] + BladeScalars[1];

        //    //Sub-expression: LLDI003E = Times[LLDI000B,LLDI000F]
        //    tmp1 = ray.DirectionX * t0;

        //    //Sub-expression: LLDI003F = Plus[LLDI0008,LLDI003E]
        //    tmp1 = ray.OriginX + tmp1;

        //    //Sub-expression: LLDI0040 = Times[2,LLDI0007,LLDI003F]
        //    tmp2 = 2 * BladeScalars[4] * tmp1;

        //    //Sub-expression: LLDI0041 = Plus[LLDI003D,LLDI0040]
        //    tmp2 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0042 = Times[LLDI000C,LLDI000F]
        //    tmp3 = ray.DirectionY * t0;

        //    //Sub-expression: LLDI0043 = Plus[LLDI0009,LLDI0042]
        //    tmp3 = ray.OriginY + tmp3;

        //    //Sub-expression: LLDI0044 = Times[-2,LLDI0006,LLDI0043]
        //    tmp4 = -2 * BladeScalars[3] * tmp3;

        //    //Sub-expression: LLDI0045 = Plus[LLDI0041,LLDI0044]
        //    tmp2 = tmp2 + tmp4;

        //    //Sub-expression: LLDI0046 = Times[LLDI000D,LLDI000F]
        //    tmp4 = ray.DirectionZ * t0;

        //    //Sub-expression: LLDI0047 = Plus[LLDI000A,LLDI0046]
        //    tmp4 = ray.OriginZ + tmp4;

        //    //Sub-expression: LLDI0048 = Times[2,LLDI0005,LLDI0047]
        //    tmp5 = 2 * BladeScalars[2] * tmp4;

        //    //Sub-expression: LLDI0049 = Plus[LLDI0045,LLDI0048]
        //    tmp2 = tmp2 + tmp5;

        //    //Sub-expression: LLDI004A = Power[LLDI003F,2]
        //    tmp5 = tmp1 * tmp1;

        //    //Sub-expression: LLDI004B = Power[LLDI0043,2]
        //    tmp6 = tmp3 * tmp3;

        //    //Sub-expression: LLDI004C = Plus[LLDI004A,LLDI004B]
        //    tmp5 = tmp5 + tmp6;

        //    //Sub-expression: LLDI004D = Power[LLDI0047,2]
        //    tmp6 = tmp4 * tmp4;

        //    //Sub-expression: LLDI004E = Plus[LLDI004C,LLDI004D]
        //    tmp5 = tmp5 + tmp6;

        //    //Sub-expression: LLDI004F = Times[LLDI0003,LLDI004E]
        //    tmp6 = BladeScalars[0] * tmp5;

        //    //Sub-expression: LLDI0050 = Plus[LLDI0049,LLDI004F]
        //    tmp2 = tmp2 + tmp6;

        //    //Sub-expression: LLDI0051 = Times[-1,LLDI0004,LLDI004E]
        //    tmp5 = -1 * BladeScalars[1] * tmp5;

        //    //Sub-expression: LLDI0052 = Plus[LLDI0050,LLDI0051]
        //    tmp2 = tmp2 + tmp5;

        //    //Sub-expression: LLDI0053 = Times[Rational[1,2],LLDI0052]
        //    tmp2 = 0.5 * tmp2;

        //    //Sub-expression: LLDI0054 = Power[LLDI0053,2]
        //    tmp2 = tmp2 * tmp2;

        //    //Output: LLDI0001 = Times[-1,LLDI0054]
        //    var sdf0 = -tmp2;

        //    //Sub-expression: LLDI0055 = Times[LLDI000B,LLDI000E]
        //    tmp2 = ray.DirectionX * DistanceDelta;

        //    //Sub-expression: LLDI0056 = Plus[LLDI003F,LLDI0055]
        //    tmp1 = tmp1 + tmp2;

        //    //Sub-expression: LLDI0057 = Times[2,LLDI0007,LLDI0056]
        //    tmp2 = 2 * BladeScalars[4] * tmp1;

        //    //Sub-expression: LLDI0058 = Plus[LLDI003D,LLDI0057]
        //    tmp0 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0059 = Times[LLDI000C,LLDI000E]
        //    tmp2 = ray.DirectionY * DistanceDelta;

        //    //Sub-expression: LLDI005A = Plus[LLDI0043,LLDI0059]
        //    tmp2 = tmp3 + tmp2;

        //    //Sub-expression: LLDI005B = Times[-2,LLDI0006,LLDI005A]
        //    tmp3 = -2 * BladeScalars[3] * tmp2;

        //    //Sub-expression: LLDI005C = Plus[LLDI0058,LLDI005B]
        //    tmp0 = tmp0 + tmp3;

        //    //Sub-expression: LLDI005D = Times[LLDI000D,LLDI000E]
        //    tmp3 = ray.DirectionZ * DistanceDelta;

        //    //Sub-expression: LLDI005E = Plus[LLDI0047,LLDI005D]
        //    tmp3 = tmp4 + tmp3;

        //    //Sub-expression: LLDI005F = Times[2,LLDI0005,LLDI005E]
        //    tmp4 = 2 * BladeScalars[2] * tmp3;

        //    //Sub-expression: LLDI0060 = Plus[LLDI005C,LLDI005F]
        //    tmp0 = tmp0 + tmp4;

        //    //Sub-expression: LLDI0061 = Power[LLDI0056,2]
        //    tmp1 = tmp1 * tmp1;

        //    //Sub-expression: LLDI0062 = Power[LLDI005A,2]
        //    tmp2 = tmp2 * tmp2;

        //    //Sub-expression: LLDI0063 = Plus[LLDI0061,LLDI0062]
        //    tmp1 = tmp1 + tmp2;

        //    //Sub-expression: LLDI0064 = Power[LLDI005E,2]
        //    tmp2 = tmp3 * tmp3;

        //    //Sub-expression: LLDI0065 = Plus[LLDI0063,LLDI0064]
        //    tmp1 = tmp1 + tmp2;

        //    //Sub-expression: LLDI0066 = Times[LLDI0003,LLDI0065]
        //    tmp2 = BladeScalars[0] * tmp1;

        //    //Sub-expression: LLDI0067 = Plus[LLDI0060,LLDI0066]
        //    tmp0 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0068 = Times[-1,LLDI0004,LLDI0065]
        //    tmp1 = -1 * BladeScalars[1] * tmp1;

        //    //Sub-expression: LLDI0069 = Plus[LLDI0067,LLDI0068]
        //    tmp0 = tmp0 + tmp1;

        //    //Sub-expression: LLDI006A = Times[Rational[1,2],LLDI0069]
        //    tmp0 = 0.5 * tmp0;

        //    //Sub-expression: LLDI006B = Power[LLDI006A,2]
        //    tmp0 = tmp0 * tmp0;

        //    //Output: LLDI0002 = Times[-1,LLDI006B]
        //    var sdf1 = -tmp0;


        //    //Finish GMac Macro Code Generation, 2019-09-10T21:49:45.6035156+02:00

        //    sdf0 = CorrectSdf(sdf0);
        //    sdf1 = CorrectSdf(sdf1);

        //    var rayStep = DistanceDelta * sdf0 / (sdf0 - sdf1);

        //    Debug.Assert(!double.IsNaN(rayStep));

        //    return rayStep;
        //}

        //public override Tuple3D GetNormal(Tuple3D point)
        //{
        //    //Begin GMac Macro Code Generation, 2019-09-10T22:31:49.4510846+02:00
        //    //Macro: main.cga5d.GetNormal
        //    //Input Variables: 9 used, 0 not used, 9 total.
        //    //Temp Variables: 62 sub-expressions, 0 generated temps, 62 total.
        //    //Target Temp Variables: 13 total.
        //    //Output Variables: 4 total.
        //    //Computations: 1.15151515151515 average, 76 total.
        //    //Memory Reads: 1.71212121212121 average, 113 total.
        //    //Memory Writes: 66 total.
        //    //
        //    //Macro Binding Data: 
        //    //   result.d1 = variable: var d1
        //    //   result.d2 = variable: var d2
        //    //   result.d3 = variable: var d3
        //    //   result.d4 = variable: var d4
        //    //   mv.#e1^e2^e3^ep# = variable: BladeScalars[0]
        //    //   mv.#e1^e2^e3^en# = variable: BladeScalars[1]
        //    //   mv.#e1^e2^ep^en# = variable: BladeScalars[2]
        //    //   mv.#e1^e3^ep^en# = variable: BladeScalars[3]
        //    //   mv.#e2^e3^ep^en# = variable: BladeScalars[4]
        //    //   point.#e1# = variable: point.X
        //    //   point.#e2# = variable: point.Y
        //    //   point.#e3# = variable: point.Z
        //    //   distanceDelta = variable: DistanceDelta

        //    double tmp0;
        //    double tmp1;
        //    double tmp2;
        //    double tmp3;
        //    double tmp4;
        //    double tmp5;
        //    double tmp6;
        //    double tmp7;
        //    double tmp8;
        //    double tmp9;
        //    double tmp10;
        //    double tmp11;
        //    double tmp12;

        //    //Sub-expression: LLDI0069 = Plus[LLDI0005,LLDI0006]
        //    tmp0 = BladeScalars[0] + BladeScalars[1];

        //    //Sub-expression: LLDI006A = Plus[LLDI000A,LLDI000D]
        //    tmp1 = point.X + DistanceDelta;

        //    //Sub-expression: LLDI006B = Times[2,LLDI0009,LLDI006A]
        //    tmp2 = 2 * BladeScalars[4] * tmp1;

        //    //Sub-expression: LLDI006C = Plus[LLDI0069,LLDI006B]
        //    tmp2 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0074 = Power[LLDI006A,2]
        //    tmp1 = tmp1 * tmp1;

        //    //Sub-expression: LLDI0083 = Plus[LLDI000C,LLDI000D]
        //    tmp3 = point.Z + DistanceDelta;

        //    //Sub-expression: LLDI0084 = Times[2,LLDI0007,LLDI0083]
        //    tmp4 = 2 * BladeScalars[2] * tmp3;

        //    //Sub-expression: LLDI0088 = Power[LLDI0083,2]
        //    tmp3 = tmp3 * tmp3;

        //    //Sub-expression: LLDI0090 = Plus[LLDI000B,LLDI000D]
        //    tmp5 = point.Y + DistanceDelta;

        //    //Sub-expression: LLDI0091 = Times[-2,LLDI0008,LLDI0090]
        //    tmp6 = -2 * BladeScalars[3] * tmp5;

        //    //Sub-expression: LLDI0094 = Power[LLDI0090,2]
        //    tmp5 = tmp5 * tmp5;

        //    //Sub-expression: LLDI009D = Plus[LLDI006C,LLDI0091]
        //    tmp7 = tmp2 + tmp6;

        //    //Sub-expression: LLDI009E = Plus[LLDI009D,LLDI0084]
        //    tmp7 = tmp7 + tmp4;

        //    //Sub-expression: LLDI009F = Plus[LLDI0074,LLDI0094]
        //    tmp8 = tmp1 + tmp5;

        //    //Sub-expression: LLDI00A0 = Plus[LLDI009F,LLDI0088]
        //    tmp8 = tmp8 + tmp3;

        //    //Sub-expression: LLDI00A1 = Times[LLDI0005,LLDI00A0]
        //    tmp9 = BladeScalars[0] * tmp8;

        //    //Sub-expression: LLDI00A2 = Plus[LLDI009E,LLDI00A1]
        //    tmp7 = tmp7 + tmp9;

        //    //Sub-expression: LLDI00A3 = Times[-1,LLDI0006,LLDI00A0]
        //    tmp8 = -1 * BladeScalars[1] * tmp8;

        //    //Sub-expression: LLDI00A4 = Plus[LLDI00A2,LLDI00A3]
        //    tmp7 = tmp7 + tmp8;

        //    //Sub-expression: LLDI00A5 = Times[Rational[1,2],LLDI00A4]
        //    tmp7 = 0.5 * tmp7;

        //    //Sub-expression: LLDI00A6 = Power[LLDI00A5,2]
        //    tmp7 = tmp7 * tmp7;

        //    //Output: LLDI0004 = Times[-1,LLDI00A6]
        //    var d4 = -tmp7;

        //    //Sub-expression: LLDI006D = Times[-1,LLDI000D]
        //    tmp7 = -DistanceDelta;

        //    //Sub-expression: LLDI006E = Plus[LLDI000B,LLDI006D]
        //    tmp8 = point.Y + tmp7;

        //    //Sub-expression: LLDI006F = Times[-2,LLDI0008,LLDI006E]
        //    tmp9 = -2 * BladeScalars[3] * tmp8;

        //    //Sub-expression: LLDI0070 = Plus[LLDI006C,LLDI006F]
        //    tmp2 = tmp2 + tmp9;

        //    //Sub-expression: LLDI0071 = Plus[LLDI000C,LLDI006D]
        //    tmp10 = point.Z + tmp7;

        //    //Sub-expression: LLDI0072 = Times[2,LLDI0007,LLDI0071]
        //    tmp11 = 2 * BladeScalars[2] * tmp10;

        //    //Sub-expression: LLDI0073 = Plus[LLDI0070,LLDI0072]
        //    tmp2 = tmp2 + tmp11;

        //    //Sub-expression: LLDI0075 = Power[LLDI006E,2]
        //    tmp8 = tmp8 * tmp8;

        //    //Sub-expression: LLDI0076 = Plus[LLDI0074,LLDI0075]
        //    tmp1 = tmp1 + tmp8;

        //    //Sub-expression: LLDI0077 = Power[LLDI0071,2]
        //    tmp10 = tmp10 * tmp10;

        //    //Sub-expression: LLDI0078 = Plus[LLDI0076,LLDI0077]
        //    tmp1 = tmp1 + tmp10;

        //    //Sub-expression: LLDI0079 = Times[LLDI0005,LLDI0078]
        //    tmp12 = BladeScalars[0] * tmp1;

        //    //Sub-expression: LLDI007A = Plus[LLDI0073,LLDI0079]
        //    tmp2 = tmp2 + tmp12;

        //    //Sub-expression: LLDI007B = Times[-1,LLDI0006,LLDI0078]
        //    tmp1 = -1 * BladeScalars[1] * tmp1;

        //    //Sub-expression: LLDI007C = Plus[LLDI007A,LLDI007B]
        //    tmp1 = tmp2 + tmp1;

        //    //Sub-expression: LLDI007D = Times[Rational[1,2],LLDI007C]
        //    tmp1 = 0.5 * tmp1;

        //    //Sub-expression: LLDI007E = Power[LLDI007D,2]
        //    tmp1 = tmp1 * tmp1;

        //    //Output: LLDI0001 = Times[-1,LLDI007E]
        //    var d1 = -tmp1;

        //    //Sub-expression: LLDI007F = Plus[LLDI000A,LLDI006D]
        //    tmp1 = point.X + tmp7;

        //    //Sub-expression: LLDI0080 = Times[2,LLDI0009,LLDI007F]
        //    tmp2 = 2 * BladeScalars[4] * tmp1;

        //    //Sub-expression: LLDI0081 = Plus[LLDI0069,LLDI0080]
        //    tmp0 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0082 = Plus[LLDI0081,LLDI006F]
        //    tmp2 = tmp0 + tmp9;

        //    //Sub-expression: LLDI0085 = Plus[LLDI0082,LLDI0084]
        //    tmp2 = tmp2 + tmp4;

        //    //Sub-expression: LLDI0086 = Power[LLDI007F,2]
        //    tmp1 = tmp1 * tmp1;

        //    //Sub-expression: LLDI0087 = Plus[LLDI0086,LLDI0075]
        //    tmp4 = tmp1 + tmp8;

        //    //Sub-expression: LLDI0089 = Plus[LLDI0087,LLDI0088]
        //    tmp3 = tmp4 + tmp3;

        //    //Sub-expression: LLDI008A = Times[LLDI0005,LLDI0089]
        //    tmp4 = BladeScalars[0] * tmp3;

        //    //Sub-expression: LLDI008B = Plus[LLDI0085,LLDI008A]
        //    tmp2 = tmp2 + tmp4;

        //    //Sub-expression: LLDI008C = Times[-1,LLDI0006,LLDI0089]
        //    tmp3 = -1 * BladeScalars[1] * tmp3;

        //    //Sub-expression: LLDI008D = Plus[LLDI008B,LLDI008C]
        //    tmp2 = tmp2 + tmp3;

        //    //Sub-expression: LLDI008E = Times[Rational[1,2],LLDI008D]
        //    tmp2 = 0.5 * tmp2;

        //    //Sub-expression: LLDI008F = Power[LLDI008E,2]
        //    tmp2 = tmp2 * tmp2;

        //    //Output: LLDI0002 = Times[-1,LLDI008F]
        //    var d2 = -tmp2;

        //    //Sub-expression: LLDI0092 = Plus[LLDI0081,LLDI0091]
        //    tmp0 = tmp0 + tmp6;

        //    //Sub-expression: LLDI0093 = Plus[LLDI0092,LLDI0072]
        //    tmp0 = tmp0 + tmp11;

        //    //Sub-expression: LLDI0095 = Plus[LLDI0086,LLDI0094]
        //    tmp1 = tmp1 + tmp5;

        //    //Sub-expression: LLDI0096 = Plus[LLDI0095,LLDI0077]
        //    tmp1 = tmp1 + tmp10;

        //    //Sub-expression: LLDI0097 = Times[LLDI0005,LLDI0096]
        //    tmp2 = BladeScalars[0] * tmp1;

        //    //Sub-expression: LLDI0098 = Plus[LLDI0093,LLDI0097]
        //    tmp0 = tmp0 + tmp2;

        //    //Sub-expression: LLDI0099 = Times[-1,LLDI0006,LLDI0096]
        //    tmp1 = -1 * BladeScalars[1] * tmp1;

        //    //Sub-expression: LLDI009A = Plus[LLDI0098,LLDI0099]
        //    tmp0 = tmp0 + tmp1;

        //    //Sub-expression: LLDI009B = Times[Rational[1,2],LLDI009A]
        //    tmp0 = 0.5 * tmp0;

        //    //Sub-expression: LLDI009C = Power[LLDI009B,2]
        //    tmp0 = tmp0 * tmp0;

        //    //Output: LLDI0003 = Times[-1,LLDI009C]
        //    var d3 = -tmp0;


        //    //Finish GMac Macro Code Generation, 2019-09-10T22:31:49.4695859+02:00

        //    d1 = CorrectSdf(d1);
        //    d2 = CorrectSdf(d2);
        //    d3 = CorrectSdf(d3);
        //    d4 = CorrectSdf(d4);

        //    return new Tuple3D(
        //        d4 + d1 - d2 - d3,
        //        d4 - d1 - d2 + d3,
        //        d4 - d1 + d2 - d3
        //    ).Normalize();
        //}
    }
}
