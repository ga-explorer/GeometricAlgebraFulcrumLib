using GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.GeometricAlgebra.CGA5D;

public sealed class Cga5D1VectorGeometry 
    : Cga5DkVectorGeometry
{
    public override int Grade => 1;


    internal Cga5D1VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
        : base(bladeScalars, nullSpaceKind)
    {
    }


    protected override double ComputeSdfOpns(ILinFloat64Vector3D point)
    {
        //Begin GMac Macro Code Generation, 2019-09-12T00:15:03.5096498+02:00
        //Macro: main.cga5d.SdfOpns
        //Input Variables: 8 used, 0 not used, 8 total.
        //Temp Variables: 60 sub-expressions, 0 generated temps, 60 total.
        //Target Temp Variables: 5 total.
        //Output Variables: 1 total.
        //Computations: 1.16393442622951 average, 71 total.
        //Memory Reads: 1.67213114754098 average, 102 total.
        //Memory Writes: 61 total.
        //
        //Macro Binding Data: 
        //   result = variable: var sdf
        //   point.#e1# = variable: point.X
        //   point.#e2# = variable: point.Y
        //   point.#e3# = variable: point.Z
        //   mv.#e1# = variable: BladeScalars[0]
        //   mv.#e2# = variable: BladeScalars[1]
        //   mv.#e3# = variable: BladeScalars[2]
        //   mv.#ep# = variable: BladeScalars[3]
        //   mv.#en# = variable: BladeScalars[4]

        double tmp0;
        double tmp1;
        double tmp2;
        double tmp3;
        double tmp4;

        //Sub-expression: LLDI0023 = Times[-1,LLDI0003,LLDI0005]
        tmp0 = -1 * point.Y * Scalars[0];

        //Sub-expression: LLDI0024 = Times[LLDI0002,LLDI0006]
        tmp1 = point.X * Scalars[1];

        //Sub-expression: LLDI0025 = Plus[LLDI0023,LLDI0024]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0026 = Power[LLDI0025,2]
        tmp0 = tmp0 * tmp0;

        //Sub-expression: LLDI0027 = Times[-1,LLDI0004,LLDI0005]
        tmp1 = -1 * point.Z * Scalars[0];

        //Sub-expression: LLDI0028 = Times[LLDI0002,LLDI0007]
        tmp2 = point.X * Scalars[2];

        //Sub-expression: LLDI0029 = Plus[LLDI0027,LLDI0028]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI002A = Power[LLDI0029,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI002B = Plus[LLDI0026,LLDI002A]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI002C = Times[-1,LLDI0004,LLDI0006]
        tmp1 = -1 * point.Z * Scalars[1];

        //Sub-expression: LLDI002D = Times[LLDI0003,LLDI0007]
        tmp2 = point.Y * Scalars[2];

        //Sub-expression: LLDI002E = Plus[LLDI002C,LLDI002D]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI002F = Power[LLDI002E,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI0030 = Plus[LLDI002B,LLDI002F]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0031 = Times[LLDI0002,LLDI0008]
        tmp1 = point.X * Scalars[3];

        //Sub-expression: LLDI0032 = Power[LLDI0002,2]
        tmp2 = point.X * point.X;

        //Sub-expression: LLDI0033 = Power[LLDI0003,2]
        tmp3 = point.Y * point.Y;

        //Sub-expression: LLDI0034 = Plus[LLDI0032,LLDI0033]
        tmp2 = tmp2 + tmp3;

        //Sub-expression: LLDI0035 = Power[LLDI0004,2]
        tmp3 = point.Z * point.Z;

        //Sub-expression: LLDI0036 = Plus[LLDI0034,LLDI0035]
        tmp2 = tmp2 + tmp3;

        //Sub-expression: LLDI0037 = Plus[-1,LLDI0036]
        tmp3 = -1 + tmp2;

        //Sub-expression: LLDI0038 = Times[Rational[-1,2],LLDI0005,LLDI0037]
        tmp4 = -0.5 * Scalars[0] * tmp3;

        //Sub-expression: LLDI0039 = Plus[LLDI0031,LLDI0038]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI003A = Power[LLDI0039,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI003B = Plus[LLDI0030,LLDI003A]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI003C = Times[LLDI0003,LLDI0008]
        tmp1 = point.Y * Scalars[3];

        //Sub-expression: LLDI003D = Times[Rational[-1,2],LLDI0006,LLDI0037]
        tmp4 = -0.5 * Scalars[1] * tmp3;

        //Sub-expression: LLDI003E = Plus[LLDI003C,LLDI003D]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI003F = Power[LLDI003E,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI0040 = Plus[LLDI003B,LLDI003F]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0041 = Times[LLDI0004,LLDI0008]
        tmp1 = point.Z * Scalars[3];

        //Sub-expression: LLDI0042 = Times[Rational[-1,2],LLDI0007,LLDI0037]
        tmp4 = -0.5 * Scalars[2] * tmp3;

        //Sub-expression: LLDI0043 = Plus[LLDI0041,LLDI0042]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI0044 = Power[LLDI0043,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI0045 = Plus[LLDI0040,LLDI0044]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0046 = Times[LLDI0002,LLDI0009]
        tmp1 = point.X * Scalars[4];

        //Sub-expression: LLDI0047 = Plus[1,LLDI0036]
        tmp2 = 1 + tmp2;

        //Sub-expression: LLDI0048 = Times[Rational[-1,2],LLDI0005,LLDI0047]
        tmp4 = -0.5 * Scalars[0] * tmp2;

        //Sub-expression: LLDI0049 = Plus[LLDI0046,LLDI0048]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI004A = Power[LLDI0049,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI004B = Times[-1,LLDI004A]
        tmp1 = -tmp1;

        //Sub-expression: LLDI004C = Plus[LLDI0045,LLDI004B]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI004D = Times[LLDI0003,LLDI0009]
        tmp1 = point.Y * Scalars[4];

        //Sub-expression: LLDI004E = Times[Rational[-1,2],LLDI0006,LLDI0047]
        tmp4 = -0.5 * Scalars[1] * tmp2;

        //Sub-expression: LLDI004F = Plus[LLDI004D,LLDI004E]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI0050 = Power[LLDI004F,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI0051 = Times[-1,LLDI0050]
        tmp1 = -tmp1;

        //Sub-expression: LLDI0052 = Plus[LLDI004C,LLDI0051]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0053 = Times[LLDI0004,LLDI0009]
        tmp1 = point.Z * Scalars[4];

        //Sub-expression: LLDI0054 = Times[Rational[-1,2],LLDI0007,LLDI0047]
        tmp4 = -0.5 * Scalars[2] * tmp2;

        //Sub-expression: LLDI0055 = Plus[LLDI0053,LLDI0054]
        tmp1 = tmp1 + tmp4;

        //Sub-expression: LLDI0056 = Power[LLDI0055,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI0057 = Times[-1,LLDI0056]
        tmp1 = -tmp1;

        //Sub-expression: LLDI0058 = Plus[LLDI0052,LLDI0057]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI0059 = Times[LLDI0009,LLDI0037]
        tmp1 = Scalars[4] * tmp3;

        //Sub-expression: LLDI005A = Times[-1,LLDI0008,LLDI0047]
        tmp2 = -1 * Scalars[3] * tmp2;

        //Sub-expression: LLDI005B = Plus[LLDI0059,LLDI005A]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI005C = Times[Rational[1,2],LLDI005B]
        tmp1 = 0.5 * tmp1;

        //Sub-expression: LLDI005D = Power[LLDI005C,2]
        tmp1 = tmp1 * tmp1;

        //Sub-expression: LLDI005E = Times[-1,LLDI005D]
        tmp1 = -tmp1;

        //Output: LLDI0001 = Plus[LLDI0058,LLDI005E]
        var sdf = tmp0 + tmp1;


        //Finish GMac Macro Code Generation, 2019-09-12T00:15:03.5326369+02:00

        return sdf;
    }

    protected override double ComputeSdfIpns(ILinFloat64Vector3D point)
    {
        //Begin GMac Macro Code Generation, 2019-09-10T23:11:17.6287673+02:00
        //Macro: main.cga5d.SdfIpns
        //Input Variables: 8 used, 0 not used, 8 total.
        //Temp Variables: 16 sub-expressions, 0 generated temps, 16 total.
        //Target Temp Variables: 3 total.
        //Output Variables: 1 total.
        //Computations: 1.11764705882353 average, 19 total.
        //Memory Reads: 1.64705882352941 average, 28 total.
        //Memory Writes: 17 total.
        //
        //Macro Binding Data: 
        //   result = variable: var sdf
        //   point.#e1# = variable: point.X
        //   point.#e2# = variable: point.Y
        //   point.#e3# = variable: point.Z
        //   mv.#e1# = variable: BladeScalars[0]
        //   mv.#e2# = variable: BladeScalars[1]
        //   mv.#e3# = variable: BladeScalars[2]
        //   mv.#ep# = variable: BladeScalars[3]
        //   mv.#en# = variable: BladeScalars[4]

        double tmp0;
        double tmp1;
        double tmp2;

        //Sub-expression: LLDI001A = Times[LLDI0002,LLDI0005]
        tmp0 = point.X * Scalars[0];

        //Sub-expression: LLDI001B = Times[LLDI0003,LLDI0006]
        tmp1 = point.Y * Scalars[1];

        //Sub-expression: LLDI001C = Plus[LLDI001A,LLDI001B]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI001D = Times[LLDI0004,LLDI0007]
        tmp1 = point.Z * Scalars[2];

        //Sub-expression: LLDI001E = Plus[LLDI001C,LLDI001D]
        tmp0 = tmp0 + tmp1;

        //Sub-expression: LLDI001F = Power[LLDI0002,2]
        tmp1 = point.X * point.X;

        //Sub-expression: LLDI0020 = Power[LLDI0003,2]
        tmp2 = point.Y * point.Y;

        //Sub-expression: LLDI0021 = Plus[LLDI001F,LLDI0020]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI0022 = Power[LLDI0004,2]
        tmp2 = point.Z * point.Z;

        //Sub-expression: LLDI0023 = Plus[LLDI0021,LLDI0022]
        tmp1 = tmp1 + tmp2;

        //Sub-expression: LLDI0024 = Plus[-1,LLDI0023]
        tmp2 = -1 + tmp1;

        //Sub-expression: LLDI0025 = Times[Rational[1,2],LLDI0008,LLDI0024]
        tmp2 = 0.5 * Scalars[3] * tmp2;

        //Sub-expression: LLDI0026 = Plus[LLDI001E,LLDI0025]
        tmp0 = tmp0 + tmp2;

        //Sub-expression: LLDI0027 = Plus[1,LLDI0023]
        tmp1 = 1 + tmp1;

        //Sub-expression: LLDI0028 = Times[Rational[-1,2],LLDI0009,LLDI0027]
        tmp1 = -0.5 * Scalars[4] * tmp1;

        //Sub-expression: LLDI0029 = Plus[LLDI0026,LLDI0028]
        tmp0 = tmp0 + tmp1;

        //Output: LLDI0001 = Power[LLDI0029,2]
        var sdf = tmp0 * tmp0;


        //Finish GMac Macro Code Generation, 2019-09-10T23:11:17.6467547+02:00

        return sdf;
    }

    //public override double GetRayStep(Line3D ray, double t0)
    //{
    //    //Begin GMac Macro Code Generation, 2019-09-11T06:02:47.0241522+02:00
    //    //Macro: main.cga5d.GetRayStepIpns
    //    //Input Variables: 13 used, 0 not used, 13 total.
    //    //Temp Variables: 44 sub-expressions, 0 generated temps, 44 total.
    //    //Target Temp Variables: 6 total.
    //    //Output Variables: 2 total.
    //    //Computations: 1.08695652173913 average, 50 total.
    //    //Memory Reads: 1.73913043478261 average, 80 total.
    //    //Memory Writes: 46 total.
    //    //
    //    //Macro Binding Data: 
    //    //   result.sdf0 = variable: var sdf0
    //    //   result.sdf1 = variable: var sdf1
    //    //   mv.#e1# = variable: BladeScalars[0]
    //    //   mv.#e2# = variable: BladeScalars[1]
    //    //   mv.#e3# = variable: BladeScalars[2]
    //    //   mv.#ep# = variable: BladeScalars[3]
    //    //   mv.#en# = variable: BladeScalars[4]
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

    //    //Sub-expression: LLDI003D = Times[LLDI000B,LLDI000F]
    //    tmp0 = ray.DirectionX * t0;

    //    //Sub-expression: LLDI003E = Plus[LLDI0008,LLDI003D]
    //    tmp0 = ray.OriginX + tmp0;

    //    //Sub-expression: LLDI003F = Times[LLDI0003,LLDI003E]
    //    tmp1 = BladeScalars[0] * tmp0;

    //    //Sub-expression: LLDI0040 = Times[LLDI000C,LLDI000F]
    //    tmp2 = ray.DirectionY * t0;

    //    //Sub-expression: LLDI0041 = Plus[LLDI0009,LLDI0040]
    //    tmp2 = ray.OriginY + tmp2;

    //    //Sub-expression: LLDI0042 = Times[LLDI0004,LLDI0041]
    //    tmp3 = BladeScalars[1] * tmp2;

    //    //Sub-expression: LLDI0043 = Plus[LLDI003F,LLDI0042]
    //    tmp1 = tmp1 + tmp3;

    //    //Sub-expression: LLDI0044 = Times[LLDI000D,LLDI000F]
    //    tmp3 = ray.DirectionZ * t0;

    //    //Sub-expression: LLDI0045 = Plus[LLDI000A,LLDI0044]
    //    tmp3 = ray.OriginZ + tmp3;

    //    //Sub-expression: LLDI0046 = Times[LLDI0005,LLDI0045]
    //    tmp4 = BladeScalars[2] * tmp3;

    //    //Sub-expression: LLDI0047 = Plus[LLDI0043,LLDI0046]
    //    tmp1 = tmp1 + tmp4;

    //    //Sub-expression: LLDI0048 = Power[LLDI003E,2]
    //    tmp4 = tmp0 * tmp0;

    //    //Sub-expression: LLDI0049 = Power[LLDI0041,2]
    //    tmp5 = tmp2 * tmp2;

    //    //Sub-expression: LLDI004A = Plus[LLDI0048,LLDI0049]
    //    tmp4 = tmp4 + tmp5;

    //    //Sub-expression: LLDI004B = Power[LLDI0045,2]
    //    tmp5 = tmp3 * tmp3;

    //    //Sub-expression: LLDI004C = Plus[LLDI004A,LLDI004B]
    //    tmp4 = tmp4 + tmp5;

    //    //Sub-expression: LLDI004D = Plus[-1,LLDI004C]
    //    tmp5 = -1 + tmp4;

    //    //Sub-expression: LLDI004E = Times[Rational[1,2],LLDI0006,LLDI004D]
    //    tmp5 = 0.5 * BladeScalars[3] * tmp5;

    //    //Sub-expression: LLDI004F = Plus[LLDI0047,LLDI004E]
    //    tmp1 = tmp1 + tmp5;

    //    //Sub-expression: LLDI0050 = Plus[1,LLDI004C]
    //    tmp4 = 1 + tmp4;

    //    //Sub-expression: LLDI0051 = Times[Rational[-1,2],LLDI0007,LLDI0050]
    //    tmp4 = -0.5 * BladeScalars[4] * tmp4;

    //    //Sub-expression: LLDI0052 = Plus[LLDI004F,LLDI0051]
    //    tmp1 = tmp1 + tmp4;

    //    //Output: LLDI0001 = Power[LLDI0052,2]
    //    var sdf0 = tmp1 * tmp1;

    //    //Sub-expression: LLDI0053 = Times[LLDI000B,LLDI000E]
    //    tmp1 = ray.DirectionX * DistanceDelta;

    //    //Sub-expression: LLDI0054 = Plus[LLDI003E,LLDI0053]
    //    tmp0 = tmp0 + tmp1;

    //    //Sub-expression: LLDI0055 = Times[LLDI0003,LLDI0054]
    //    tmp1 = BladeScalars[0] * tmp0;

    //    //Sub-expression: LLDI0056 = Times[LLDI000C,LLDI000E]
    //    tmp4 = ray.DirectionY * DistanceDelta;

    //    //Sub-expression: LLDI0057 = Plus[LLDI0041,LLDI0056]
    //    tmp2 = tmp2 + tmp4;

    //    //Sub-expression: LLDI0058 = Times[LLDI0004,LLDI0057]
    //    tmp4 = BladeScalars[1] * tmp2;

    //    //Sub-expression: LLDI0059 = Plus[LLDI0055,LLDI0058]
    //    tmp1 = tmp1 + tmp4;

    //    //Sub-expression: LLDI005A = Times[LLDI000D,LLDI000E]
    //    tmp4 = ray.DirectionZ * DistanceDelta;

    //    //Sub-expression: LLDI005B = Plus[LLDI0045,LLDI005A]
    //    tmp3 = tmp3 + tmp4;

    //    //Sub-expression: LLDI005C = Times[LLDI0005,LLDI005B]
    //    tmp4 = BladeScalars[2] * tmp3;

    //    //Sub-expression: LLDI005D = Plus[LLDI0059,LLDI005C]
    //    tmp1 = tmp1 + tmp4;

    //    //Sub-expression: LLDI005E = Power[LLDI0054,2]
    //    tmp0 = tmp0 * tmp0;

    //    //Sub-expression: LLDI005F = Power[LLDI0057,2]
    //    tmp2 = tmp2 * tmp2;

    //    //Sub-expression: LLDI0060 = Plus[LLDI005E,LLDI005F]
    //    tmp0 = tmp0 + tmp2;

    //    //Sub-expression: LLDI0061 = Power[LLDI005B,2]
    //    tmp2 = tmp3 * tmp3;

    //    //Sub-expression: LLDI0062 = Plus[LLDI0060,LLDI0061]
    //    tmp0 = tmp0 + tmp2;

    //    //Sub-expression: LLDI0063 = Plus[-1,LLDI0062]
    //    tmp2 = -1 + tmp0;

    //    //Sub-expression: LLDI0064 = Times[Rational[1,2],LLDI0006,LLDI0063]
    //    tmp2 = 0.5 * BladeScalars[3] * tmp2;

    //    //Sub-expression: LLDI0065 = Plus[LLDI005D,LLDI0064]
    //    tmp1 = tmp1 + tmp2;

    //    //Sub-expression: LLDI0066 = Plus[1,LLDI0062]
    //    tmp0 = 1 + tmp0;

    //    //Sub-expression: LLDI0067 = Times[Rational[-1,2],LLDI0007,LLDI0066]
    //    tmp0 = -0.5 * BladeScalars[4] * tmp0;

    //    //Sub-expression: LLDI0068 = Plus[LLDI0065,LLDI0067]
    //    tmp0 = tmp1 + tmp0;

    //    //Output: LLDI0002 = Power[LLDI0068,2]
    //    var sdf1 = tmp0 * tmp0;


    //    //Finish GMac Macro Code Generation, 2019-09-11T06:02:47.0411422+02:00

    //    sdf0 = CorrectSdf(sdf0);
    //    sdf1 = CorrectSdf(sdf1);

    //    var rayStep = DistanceDelta * sdf0 / (sdf0 - sdf1);

    //    Debug.Assert(!double.IsNaN(rayStep));

    //    return rayStep;
    //}

    //public override Tuple3D GetNormal(Tuple3D point)
    //{
    //    //Begin GMac Macro Code Generation, 2019-09-11T06:22:42.5605317+02:00
    //    //Macro: main.cga5d.GetNormalIpns
    //    //Input Variables: 9 used, 0 not used, 9 total.
    //    //Temp Variables: 59 sub-expressions, 0 generated temps, 59 total.
    //    //Target Temp Variables: 12 total.
    //    //Output Variables: 4 total.
    //    //Computations: 1.12698412698413 average, 71 total.
    //    //Memory Reads: 1.6984126984127 average, 107 total.
    //    //Memory Writes: 63 total.
    //    //
    //    //Macro Binding Data: 
    //    //   result.d1 = variable: var d1
    //    //   result.d2 = variable: var d2
    //    //   result.d3 = variable: var d3
    //    //   result.d4 = variable: var d4
    //    //   mv.#e1# = variable: BladeScalars[0]
    //    //   mv.#e2# = variable: BladeScalars[1]
    //    //   mv.#e3# = variable: BladeScalars[2]
    //    //   mv.#ep# = variable: BladeScalars[3]
    //    //   mv.#en# = variable: BladeScalars[4]
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

    //    //Sub-expression: LLDI0069 = Plus[LLDI000A,LLDI000D]
    //    tmp0 = point.X + DistanceDelta;

    //    //Sub-expression: LLDI006A = Times[LLDI0005,LLDI0069]
    //    tmp1 = BladeScalars[0] * tmp0;

    //    //Sub-expression: LLDI0072 = Power[LLDI0069,2]
    //    tmp0 = tmp0 * tmp0;

    //    //Sub-expression: LLDI0080 = Plus[LLDI000C,LLDI000D]
    //    tmp2 = point.Z + DistanceDelta;

    //    //Sub-expression: LLDI0081 = Times[LLDI0007,LLDI0080]
    //    tmp3 = BladeScalars[2] * tmp2;

    //    //Sub-expression: LLDI0085 = Power[LLDI0080,2]
    //    tmp2 = tmp2 * tmp2;

    //    //Sub-expression: LLDI008D = Plus[LLDI000B,LLDI000D]
    //    tmp4 = point.Y + DistanceDelta;

    //    //Sub-expression: LLDI008E = Times[LLDI0006,LLDI008D]
    //    tmp5 = BladeScalars[1] * tmp4;

    //    //Sub-expression: LLDI0091 = Power[LLDI008D,2]
    //    tmp4 = tmp4 * tmp4;

    //    //Sub-expression: LLDI009A = Plus[LLDI006A,LLDI008E]
    //    tmp6 = tmp1 + tmp5;

    //    //Sub-expression: LLDI009B = Plus[LLDI009A,LLDI0081]
    //    tmp6 = tmp6 + tmp3;

    //    //Sub-expression: LLDI009C = Plus[LLDI0072,LLDI0091]
    //    tmp7 = tmp0 + tmp4;

    //    //Sub-expression: LLDI009D = Plus[LLDI009C,LLDI0085]
    //    tmp7 = tmp7 + tmp2;

    //    //Sub-expression: LLDI009E = Plus[-1,LLDI009D]
    //    tmp8 = -1 + tmp7;

    //    //Sub-expression: LLDI009F = Times[Rational[1,2],LLDI0008,LLDI009E]
    //    tmp8 = 0.5 * BladeScalars[3] * tmp8;

    //    //Sub-expression: LLDI00A0 = Plus[LLDI009B,LLDI009F]
    //    tmp6 = tmp6 + tmp8;

    //    //Sub-expression: LLDI00A1 = Plus[1,LLDI009D]
    //    tmp7 = 1 + tmp7;

    //    //Sub-expression: LLDI00A2 = Times[Rational[-1,2],LLDI0009,LLDI00A1]
    //    tmp7 = -0.5 * BladeScalars[4] * tmp7;

    //    //Sub-expression: LLDI00A3 = Plus[LLDI00A0,LLDI00A2]
    //    tmp6 = tmp6 + tmp7;

    //    //Output: LLDI0004 = Power[LLDI00A3,2]
    //    var d4 = tmp6 * tmp6;

    //    //Sub-expression: LLDI006B = Times[-1,LLDI000D]
    //    tmp6 = -DistanceDelta;

    //    //Sub-expression: LLDI006C = Plus[LLDI000B,LLDI006B]
    //    tmp7 = point.Y + tmp6;

    //    //Sub-expression: LLDI006D = Times[LLDI0006,LLDI006C]
    //    tmp8 = BladeScalars[1] * tmp7;

    //    //Sub-expression: LLDI006E = Plus[LLDI006A,LLDI006D]
    //    tmp1 = tmp1 + tmp8;

    //    //Sub-expression: LLDI006F = Plus[LLDI000C,LLDI006B]
    //    tmp9 = point.Z + tmp6;

    //    //Sub-expression: LLDI0070 = Times[LLDI0007,LLDI006F]
    //    tmp10 = BladeScalars[2] * tmp9;

    //    //Sub-expression: LLDI0071 = Plus[LLDI006E,LLDI0070]
    //    tmp1 = tmp1 + tmp10;

    //    //Sub-expression: LLDI0073 = Power[LLDI006C,2]
    //    tmp7 = tmp7 * tmp7;

    //    //Sub-expression: LLDI0074 = Plus[LLDI0072,LLDI0073]
    //    tmp0 = tmp0 + tmp7;

    //    //Sub-expression: LLDI0075 = Power[LLDI006F,2]
    //    tmp9 = tmp9 * tmp9;

    //    //Sub-expression: LLDI0076 = Plus[LLDI0074,LLDI0075]
    //    tmp0 = tmp0 + tmp9;

    //    //Sub-expression: LLDI0077 = Plus[-1,LLDI0076]
    //    tmp11 = -1 + tmp0;

    //    //Sub-expression: LLDI0078 = Times[Rational[1,2],LLDI0008,LLDI0077]
    //    tmp11 = 0.5 * BladeScalars[3] * tmp11;

    //    //Sub-expression: LLDI0079 = Plus[LLDI0071,LLDI0078]
    //    tmp1 = tmp1 + tmp11;

    //    //Sub-expression: LLDI007A = Plus[1,LLDI0076]
    //    tmp0 = 1 + tmp0;

    //    //Sub-expression: LLDI007B = Times[Rational[-1,2],LLDI0009,LLDI007A]
    //    tmp0 = -0.5 * BladeScalars[4] * tmp0;

    //    //Sub-expression: LLDI007C = Plus[LLDI0079,LLDI007B]
    //    tmp0 = tmp1 + tmp0;

    //    //Output: LLDI0001 = Power[LLDI007C,2]
    //    var d1 = tmp0 * tmp0;

    //    //Sub-expression: LLDI007D = Plus[LLDI000A,LLDI006B]
    //    tmp0 = point.X + tmp6;

    //    //Sub-expression: LLDI007E = Times[LLDI0005,LLDI007D]
    //    tmp1 = BladeScalars[0] * tmp0;

    //    //Sub-expression: LLDI007F = Plus[LLDI007E,LLDI006D]
    //    tmp6 = tmp1 + tmp8;

    //    //Sub-expression: LLDI0082 = Plus[LLDI007F,LLDI0081]
    //    tmp3 = tmp6 + tmp3;

    //    //Sub-expression: LLDI0083 = Power[LLDI007D,2]
    //    tmp0 = tmp0 * tmp0;

    //    //Sub-expression: LLDI0084 = Plus[LLDI0083,LLDI0073]
    //    tmp6 = tmp0 + tmp7;

    //    //Sub-expression: LLDI0086 = Plus[LLDI0084,LLDI0085]
    //    tmp2 = tmp6 + tmp2;

    //    //Sub-expression: LLDI0087 = Plus[-1,LLDI0086]
    //    tmp6 = -1 + tmp2;

    //    //Sub-expression: LLDI0088 = Times[Rational[1,2],LLDI0008,LLDI0087]
    //    tmp6 = 0.5 * BladeScalars[3] * tmp6;

    //    //Sub-expression: LLDI0089 = Plus[LLDI0082,LLDI0088]
    //    tmp3 = tmp3 + tmp6;

    //    //Sub-expression: LLDI008A = Plus[1,LLDI0086]
    //    tmp2 = 1 + tmp2;

    //    //Sub-expression: LLDI008B = Times[Rational[-1,2],LLDI0009,LLDI008A]
    //    tmp2 = -0.5 * BladeScalars[4] * tmp2;

    //    //Sub-expression: LLDI008C = Plus[LLDI0089,LLDI008B]
    //    tmp2 = tmp3 + tmp2;

    //    //Output: LLDI0002 = Power[LLDI008C,2]
    //    var d2 = tmp2 * tmp2;

    //    //Sub-expression: LLDI008F = Plus[LLDI007E,LLDI008E]
    //    tmp1 = tmp1 + tmp5;

    //    //Sub-expression: LLDI0090 = Plus[LLDI008F,LLDI0070]
    //    tmp1 = tmp1 + tmp10;

    //    //Sub-expression: LLDI0092 = Plus[LLDI0083,LLDI0091]
    //    tmp0 = tmp0 + tmp4;

    //    //Sub-expression: LLDI0093 = Plus[LLDI0092,LLDI0075]
    //    tmp0 = tmp0 + tmp9;

    //    //Sub-expression: LLDI0094 = Plus[-1,LLDI0093]
    //    tmp2 = -1 + tmp0;

    //    //Sub-expression: LLDI0095 = Times[Rational[1,2],LLDI0008,LLDI0094]
    //    tmp2 = 0.5 * BladeScalars[3] * tmp2;

    //    //Sub-expression: LLDI0096 = Plus[LLDI0090,LLDI0095]
    //    tmp1 = tmp1 + tmp2;

    //    //Sub-expression: LLDI0097 = Plus[1,LLDI0093]
    //    tmp0 = 1 + tmp0;

    //    //Sub-expression: LLDI0098 = Times[Rational[-1,2],LLDI0009,LLDI0097]
    //    tmp0 = -0.5 * BladeScalars[4] * tmp0;

    //    //Sub-expression: LLDI0099 = Plus[LLDI0096,LLDI0098]
    //    tmp0 = tmp1 + tmp0;

    //    //Output: LLDI0003 = Power[LLDI0099,2]
    //    var d3 = tmp0 * tmp0;


    //    //Finish GMac Macro Code Generation, 2019-09-11T06:22:42.5615296+02:00

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