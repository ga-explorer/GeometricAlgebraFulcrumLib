using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.Lite.Graphics.GeometricAlgebra.CGA5D
{
    public abstract class Cga5DMultivectorGeometry
        : MultivectorGeometry3D
    {
        public static Cga5D1VectorGeometry CreateOpnsPoint(Float64Vector3D point)
        {
            var scalars = new double[5];

            //Begin GMac Macro Code Generation, 2019-09-12T22:38:13.9775421+02:00
            //Macro: main.cga5d.ToPointMultivector
            //Input Variables: 3 used, 0 not used, 3 total.
            //Temp Variables: 7 sub-expressions, 0 generated temps, 7 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 5 total.
            //Computations: 0.75 average, 9 total.
            //Memory Reads: 1.16666666666667 average, 14 total.
            //Memory Writes: 12 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: Scalars[0]
            //   result.#e2# = variable: Scalars[1]
            //   result.#e3# = variable: Scalars[2]
            //   result.#ep# = variable: Scalars[3]
            //   result.#en# = variable: Scalars[4]
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z

            double tmp0;
            double tmp1;

            //Output: LLDI0001 = LLDI0006
            scalars[0] = point.X;

            //Output: LLDI0002 = LLDI0007
            scalars[1] = point.Y;

            //Output: LLDI0003 = LLDI0008
            scalars[2] = point.Z;

            //Sub-expression: LLDI000B = Power[LLDI0006,2]
            tmp0 = point.X * point.X;

            //Sub-expression: LLDI000C = Power[LLDI0007,2]
            tmp1 = point.Y * point.Y;

            //Sub-expression: LLDI000D = Plus[LLDI000B,LLDI000C]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI000E = Power[LLDI0008,2]
            tmp1 = point.Z * point.Z;

            //Sub-expression: LLDI000F = Plus[LLDI000D,LLDI000E]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0010 = Plus[-1,LLDI000F]
            tmp1 = -1 + tmp0;

            //Output: LLDI0004 = Times[Rational[1,2],LLDI0010]
            scalars[3] = 0.5 * tmp1;

            //Sub-expression: LLDI0011 = Plus[1,LLDI000F]
            tmp0 = 1 + tmp0;

            //Output: LLDI0005 = Times[Rational[1,2],LLDI0011]
            scalars[4] = 0.5 * tmp0;


            //Finish GMac Macro Code Generation, 2019-09-12T22:38:13.9925345+02:00

            return new Cga5D1VectorGeometry(
                scalars,
                MultivectorNullSpaceKind.OuterProductNullSpace
            );
        }

        public static Cga5D2VectorGeometry CreateOpnsPointPair(Float64Vector3D p1, Float64Vector3D p2)
        {
            var scalars = new double[10];

            //Begin GMac Macro Code Generation, 2019-09-12T22:58:09.1609127+02:00
            //Macro: main.cga5d.PointPairOpns
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 42 sub-expressions, 0 generated temps, 42 total.
            //Target Temp Variables: 5 total.
            //Output Variables: 10 total.
            //Computations: 1.17307692307692 average, 61 total.
            //Memory Reads: 1.67307692307692 average, 87 total.
            //Memory Writes: 52 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2# = variable: scalars[0]
            //   result.#e1^e3# = variable: scalars[1]
            //   result.#e2^e3# = variable: scalars[2]
            //   result.#e1^ep# = variable: scalars[3]
            //   result.#e2^ep# = variable: scalars[4]
            //   result.#e3^ep# = variable: scalars[5]
            //   result.#e1^en# = variable: scalars[6]
            //   result.#e2^en# = variable: scalars[7]
            //   result.#e3^en# = variable: scalars[8]
            //   result.#ep^en# = variable: scalars[9]
            //   p1.#e1# = variable: p1.X
            //   p1.#e2# = variable: p1.Y
            //   p1.#e3# = variable: p1.Z
            //   p2.#e1# = variable: p2.X
            //   p2.#e2# = variable: p2.Y
            //   p2.#e3# = variable: p2.Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;

            //Sub-expression: LLDI0021 = Times[-1,LLDI000C,LLDI000E]
            tmp0 = -1 * p1.Y * p2.X;

            //Sub-expression: LLDI0022 = Times[LLDI000B,LLDI000F]
            tmp1 = p1.X * p2.Y;

            //Output: LLDI0001 = Plus[LLDI0021,LLDI0022]
            scalars[0] = tmp0 + tmp1;

            //Sub-expression: LLDI0023 = Times[-1,LLDI000D,LLDI000E]
            tmp0 = -1 * p1.Z * p2.X;

            //Sub-expression: LLDI0024 = Times[LLDI000B,LLDI0010]
            tmp1 = p1.X * p2.Z;

            //Output: LLDI0002 = Plus[LLDI0023,LLDI0024]
            scalars[1] = tmp0 + tmp1;

            //Sub-expression: LLDI0025 = Times[-1,LLDI000D,LLDI000F]
            tmp0 = -1 * p1.Z * p2.Y;

            //Sub-expression: LLDI0026 = Times[LLDI000C,LLDI0010]
            tmp1 = p1.Y * p2.Z;

            //Output: LLDI0003 = Plus[LLDI0025,LLDI0026]
            scalars[2] = tmp0 + tmp1;

            //Sub-expression: LLDI0027 = Power[LLDI000B,2]
            tmp0 = p1.X * p1.X;

            //Sub-expression: LLDI0028 = Power[LLDI000C,2]
            tmp1 = p1.Y * p1.Y;

            //Sub-expression: LLDI0029 = Plus[LLDI0027,LLDI0028]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002A = Power[LLDI000D,2]
            tmp1 = p1.Z * p1.Z;

            //Sub-expression: LLDI002B = Plus[LLDI0029,LLDI002A]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002E = Power[LLDI000E,2]
            tmp1 = p2.X * p2.X;

            //Sub-expression: LLDI002F = Power[LLDI000F,2]
            tmp2 = p2.Y * p2.Y;

            //Sub-expression: LLDI0030 = Plus[LLDI002E,LLDI002F]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0031 = Power[LLDI0010,2]
            tmp2 = p2.Z * p2.Z;

            //Sub-expression: LLDI0032 = Plus[LLDI0030,LLDI0031]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0049 = Times[-1,LLDI0032]
            tmp2 = -tmp1;

            //Sub-expression: LLDI004A = Plus[LLDI002B,LLDI0049]
            tmp2 = tmp0 + tmp2;

            //Output: LLDI000A = Times[Rational[1,2],LLDI004A]
            scalars[9] = 0.5 * tmp2;

            //Sub-expression: LLDI002C = Times[-1,LLDI000E,LLDI002B]
            tmp2 = -1 * p2.X * tmp0;

            //Sub-expression: LLDI002D = Plus[LLDI000E,LLDI002C]
            tmp2 = p2.X + tmp2;

            //Sub-expression: LLDI0033 = Plus[-1,LLDI0032]
            tmp3 = -1 + tmp1;

            //Sub-expression: LLDI0034 = Times[LLDI000B,LLDI0033]
            tmp4 = p1.X * tmp3;

            //Sub-expression: LLDI0035 = Plus[LLDI002D,LLDI0034]
            tmp2 = tmp2 + tmp4;

            //Output: LLDI0004 = Times[Rational[1,2],LLDI0035]
            scalars[3] = 0.5 * tmp2;

            //Sub-expression: LLDI0036 = Times[-1,LLDI000F,LLDI002B]
            tmp2 = -1 * p2.Y * tmp0;

            //Sub-expression: LLDI0037 = Plus[LLDI000F,LLDI0036]
            tmp2 = p2.Y + tmp2;

            //Sub-expression: LLDI0038 = Times[LLDI000C,LLDI0033]
            tmp4 = p1.Y * tmp3;

            //Sub-expression: LLDI0039 = Plus[LLDI0037,LLDI0038]
            tmp2 = tmp2 + tmp4;

            //Output: LLDI0005 = Times[Rational[1,2],LLDI0039]
            scalars[4] = 0.5 * tmp2;

            //Sub-expression: LLDI003A = Times[-1,LLDI0010,LLDI002B]
            tmp2 = -1 * p2.Z * tmp0;

            //Sub-expression: LLDI003B = Plus[LLDI0010,LLDI003A]
            tmp2 = p2.Z + tmp2;

            //Sub-expression: LLDI003C = Times[LLDI000D,LLDI0033]
            tmp3 = p1.Z * tmp3;

            //Sub-expression: LLDI003D = Plus[LLDI003B,LLDI003C]
            tmp2 = tmp2 + tmp3;

            //Output: LLDI0006 = Times[Rational[1,2],LLDI003D]
            scalars[5] = 0.5 * tmp2;

            //Sub-expression: LLDI003E = Plus[1,LLDI002B]
            tmp0 = 1 + tmp0;

            //Sub-expression: LLDI003F = Times[-1,LLDI000E,LLDI003E]
            tmp2 = -1 * p2.X * tmp0;

            //Sub-expression: LLDI0040 = Plus[1,LLDI0032]
            tmp1 = 1 + tmp1;

            //Sub-expression: LLDI0041 = Times[LLDI000B,LLDI0040]
            tmp3 = p1.X * tmp1;

            //Sub-expression: LLDI0042 = Plus[LLDI003F,LLDI0041]
            tmp2 = tmp2 + tmp3;

            //Output: LLDI0007 = Times[Rational[1,2],LLDI0042]
            scalars[6] = 0.5 * tmp2;

            //Sub-expression: LLDI0043 = Times[-1,LLDI000F,LLDI003E]
            tmp2 = -1 * p2.Y * tmp0;

            //Sub-expression: LLDI0044 = Times[LLDI000C,LLDI0040]
            tmp3 = p1.Y * tmp1;

            //Sub-expression: LLDI0045 = Plus[LLDI0043,LLDI0044]
            tmp2 = tmp2 + tmp3;

            //Output: LLDI0008 = Times[Rational[1,2],LLDI0045]
            scalars[7] = 0.5 * tmp2;

            //Sub-expression: LLDI0046 = Times[-1,LLDI0010,LLDI003E]
            tmp0 = -1 * p2.Z * tmp0;

            //Sub-expression: LLDI0047 = Times[LLDI000D,LLDI0040]
            tmp1 = p1.Z * tmp1;

            //Sub-expression: LLDI0048 = Plus[LLDI0046,LLDI0047]
            tmp0 = tmp0 + tmp1;

            //Output: LLDI0009 = Times[Rational[1,2],LLDI0048]
            scalars[8] = 0.5 * tmp0;


            //Finish GMac Macro Code Generation, 2019-09-12T22:58:09.1769023+02:00

            return new Cga5D2VectorGeometry(
                scalars,
                MultivectorNullSpaceKind.OuterProductNullSpace
            );
        }

        public static Cga5D3VectorGeometry CreateOpnsLine(Float64Vector3D p1, Float64Vector3D p2)
        {
            throw new NotImplementedException();
        }

        public static Cga5D4VectorGeometry CreateOpnsPlane(Float64Vector3D p1, Float64Vector3D p2, Float64Vector3D p3)
        {
            throw new NotImplementedException();
        }

        public static Cga5D4VectorGeometry CreateOpnsPlane(double distance, Float64Vector3D normal)
        {
            throw new NotImplementedException();
        }

        public static Cga5D4VectorGeometry CreateOpnsPlane(Float64Vector3D point, Float64Vector3D normal)
        {
            throw new NotImplementedException();
        }


        public static Cga5D0VectorGeometry CreateIpnsScalar(double scalar)
        {
            throw new NotImplementedException();
        }

        public static Cga5D3VectorGeometry CreateIpnsPointPair(Float64Vector3D p1, Float64Vector3D p2)
        {
            var scalars = new double[10];

            //Begin GMac Macro Code Generation, 2019-09-12T23:04:37.0800727+02:00
            //Macro: main.cga5d.PointPairIpns
            //Input Variables: 6 used, 0 not used, 6 total.
            //Temp Variables: 46 sub-expressions, 0 generated temps, 46 total.
            //Target Temp Variables: 6 total.
            //Output Variables: 10 total.
            //Computations: 1.16071428571429 average, 65 total.
            //Memory Reads: 1.625 average, 91 total.
            //Memory Writes: 56 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3# = variable: scalars[0]
            //   result.#e1^e2^ep# = variable: scalars[1]
            //   result.#e1^e3^ep# = variable: scalars[2]
            //   result.#e2^e3^ep# = variable: scalars[3]
            //   result.#e1^e2^en# = variable: scalars[4]
            //   result.#e1^e3^en# = variable: scalars[5]
            //   result.#e2^e3^en# = variable: scalars[6]
            //   result.#e1^ep^en# = variable: scalars[7]
            //   result.#e2^ep^en# = variable: scalars[8]
            //   result.#e3^ep^en# = variable: scalars[9]
            //   p1.#e1# = variable: p1.X
            //   p1.#e2# = variable: p1.Y
            //   p1.#e3# = variable: p1.Z
            //   p2.#e1# = variable: p2.X
            //   p2.#e2# = variable: p2.Y
            //   p2.#e3# = variable: p2.Z

            double tmp0;
            double tmp1;
            double tmp2;
            double tmp3;
            double tmp4;
            double tmp5;

            //Sub-expression: LLDI005F = Times[-1,LLDI000D,LLDI000F]
            tmp0 = -1 * p1.Z * p2.Y;

            //Sub-expression: LLDI0060 = Times[LLDI000C,LLDI0010]
            tmp1 = p1.Y * p2.Z;

            //Output: LLDI0008 = Plus[LLDI005F,LLDI0060]
            scalars[7] = tmp0 + tmp1;

            //Sub-expression: LLDI0064 = Times[-1,LLDI000C,LLDI000E]
            tmp0 = -1 * p1.Y * p2.X;

            //Sub-expression: LLDI0065 = Times[LLDI000B,LLDI000F]
            tmp1 = p1.X * p2.Y;

            //Output: LLDI000A = Plus[LLDI0064,LLDI0065]
            scalars[9] = tmp0 + tmp1;

            //Sub-expression: LLDI0061 = Times[-1,LLDI000D,LLDI000E]
            tmp0 = -1 * p1.Z * p2.X;

            //Sub-expression: LLDI0062 = Times[LLDI000B,LLDI0010]
            tmp1 = p1.X * p2.Z;

            //Sub-expression: LLDI0063 = Plus[LLDI0061,LLDI0062]
            tmp0 = tmp0 + tmp1;

            //Output: LLDI0009 = Times[-1,LLDI0063]
            scalars[8] = -tmp0;

            //Sub-expression: LLDI0038 = Power[LLDI000B,2]
            tmp0 = p1.X * p1.X;

            //Sub-expression: LLDI0039 = Power[LLDI000C,2]
            tmp1 = p1.Y * p1.Y;

            //Sub-expression: LLDI003A = Plus[LLDI0038,LLDI0039]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI003B = Power[LLDI000D,2]
            tmp1 = p1.Z * p1.Z;

            //Sub-expression: LLDI003C = Plus[LLDI003A,LLDI003B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI003D = Power[LLDI000E,2]
            tmp1 = p2.X * p2.X;

            //Sub-expression: LLDI003E = Power[LLDI000F,2]
            tmp2 = p2.Y * p2.Y;

            //Sub-expression: LLDI003F = Plus[LLDI003D,LLDI003E]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0040 = Power[LLDI0010,2]
            tmp2 = p2.Z * p2.Z;

            //Sub-expression: LLDI0041 = Plus[LLDI003F,LLDI0040]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0042 = Times[-1,LLDI0041]
            tmp2 = -tmp1;

            //Sub-expression: LLDI0043 = Plus[LLDI003C,LLDI0042]
            tmp2 = tmp0 + tmp2;

            //Sub-expression: LLDI0044 = Times[Rational[1,2],LLDI0043]
            tmp2 = 0.5 * tmp2;

            //Output: LLDI0001 = Times[-1,LLDI0044]
            scalars[0] = -tmp2;

            //Sub-expression: LLDI0045 = Plus[1,LLDI003C]
            tmp2 = 1 + tmp0;

            //Sub-expression: LLDI0046 = Times[-1,LLDI0010,LLDI0045]
            tmp3 = -1 * p2.Z * tmp2;

            //Sub-expression: LLDI0047 = Plus[1,LLDI0041]
            tmp4 = 1 + tmp1;

            //Sub-expression: LLDI0048 = Times[LLDI000D,LLDI0047]
            tmp5 = p1.Z * tmp4;

            //Sub-expression: LLDI0049 = Plus[LLDI0046,LLDI0048]
            tmp3 = tmp3 + tmp5;

            //Output: LLDI0002 = Times[Rational[1,2],LLDI0049]
            scalars[1] = 0.5 * tmp3;

            //Sub-expression: LLDI004E = Times[-1,LLDI000E,LLDI0045]
            tmp3 = -1 * p2.X * tmp2;

            //Sub-expression: LLDI004F = Times[LLDI000B,LLDI0047]
            tmp5 = p1.X * tmp4;

            //Sub-expression: LLDI0050 = Plus[LLDI004E,LLDI004F]
            tmp3 = tmp3 + tmp5;

            //Output: LLDI0004 = Times[Rational[1,2],LLDI0050]
            scalars[3] = 0.5 * tmp3;

            //Sub-expression: LLDI0051 = Times[-1,LLDI0010,LLDI003C]
            tmp3 = -1 * p2.Z * tmp0;

            //Sub-expression: LLDI0052 = Plus[LLDI0010,LLDI0051]
            tmp3 = p2.Z + tmp3;

            //Sub-expression: LLDI0053 = Plus[-1,LLDI0041]
            tmp1 = -1 + tmp1;

            //Sub-expression: LLDI0054 = Times[LLDI000D,LLDI0053]
            tmp5 = p1.Z * tmp1;

            //Sub-expression: LLDI0055 = Plus[LLDI0052,LLDI0054]
            tmp3 = tmp3 + tmp5;

            //Output: LLDI0005 = Times[Rational[1,2],LLDI0055]
            scalars[4] = 0.5 * tmp3;

            //Sub-expression: LLDI005B = Times[-1,LLDI000E,LLDI003C]
            tmp3 = -1 * p2.X * tmp0;

            //Sub-expression: LLDI005C = Plus[LLDI000E,LLDI005B]
            tmp3 = p2.X + tmp3;

            //Sub-expression: LLDI005D = Times[LLDI000B,LLDI0053]
            tmp5 = p1.X * tmp1;

            //Sub-expression: LLDI005E = Plus[LLDI005C,LLDI005D]
            tmp3 = tmp3 + tmp5;

            //Output: LLDI0007 = Times[Rational[1,2],LLDI005E]
            scalars[6] = 0.5 * tmp3;

            //Sub-expression: LLDI004A = Times[-1,LLDI000F,LLDI0045]
            tmp2 = -1 * p2.Y * tmp2;

            //Sub-expression: LLDI004B = Times[LLDI000C,LLDI0047]
            tmp3 = p1.Y * tmp4;

            //Sub-expression: LLDI004C = Plus[LLDI004A,LLDI004B]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI004D = Times[Rational[1,2],LLDI004C]
            tmp2 = 0.5 * tmp2;

            //Output: LLDI0003 = Times[-1,LLDI004D]
            scalars[2] = -tmp2;

            //Sub-expression: LLDI0056 = Times[-1,LLDI000F,LLDI003C]
            tmp0 = -1 * p2.Y * tmp0;

            //Sub-expression: LLDI0057 = Plus[LLDI000F,LLDI0056]
            tmp0 = p2.Y + tmp0;

            //Sub-expression: LLDI0058 = Times[LLDI000C,LLDI0053]
            tmp1 = p1.Y * tmp1;

            //Sub-expression: LLDI0059 = Plus[LLDI0057,LLDI0058]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI005A = Times[Rational[1,2],LLDI0059]
            tmp0 = 0.5 * tmp0;

            //Output: LLDI0006 = Times[-1,LLDI005A]
            scalars[5] = -tmp0;


            //Finish GMac Macro Code Generation, 2019-09-12T23:04:37.0810692+02:00

            return new Cga5D3VectorGeometry(
                scalars,
                MultivectorNullSpaceKind.InnerProductNullSpace
            );
        }

        public static Cga5D2VectorGeometry CreateIpnsLine(Float64Vector3D p1, Float64Vector3D p2)
        {
            throw new NotImplementedException();
        }

        public static Cga5D1VectorGeometry CreateIpnsPlane(double distance, Float64Vector3D normal)
        {
            throw new NotImplementedException();
        }

        public static Cga5D1VectorGeometry CreateIpnsPlane(Float64Vector3D p1, Float64Vector3D p2, Float64Vector3D p3)
        {
            throw new NotImplementedException();
        }

        public static Cga5D1VectorGeometry CreateIpnsPlane(Float64Vector3D point, Float64Vector3D normal)
        {
            throw new NotImplementedException();
        }

        public static Cga5D4VectorGeometry CreateOpnsSphere(Float64Vector3D p1, Float64Vector3D p2, Float64Vector3D p3, Float64Vector3D p4)
        {
            var bladeScalars = new double[5];

            //Begin GMac Macro Code Generation, 2019-09-11T05:27:17.9500258+02:00
            //Macro: main.cga5d.SphereFromPointsOpns
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 164 sub-expressions, 0 generated temps, 164 total.
            //Target Temp Variables: 20 total.
            //Output Variables: 5 total.
            //Computations: 1.25443786982249 average, 212 total.
            //Memory Reads: 1.80473372781065 average, 305 total.
            //Memory Writes: 169 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3^ep# = variable: BladeScalars[0]
            //   result.#e1^e2^e3^en# = variable: BladeScalars[1]
            //   result.#e1^e2^ep^en# = variable: BladeScalars[2]
            //   result.#e1^e3^ep^en# = variable: BladeScalars[3]
            //   result.#e2^e3^ep^en# = variable: BladeScalars[4]
            //   p1.#e1# = variable: p1.X
            //   p1.#e2# = variable: p1.Y
            //   p1.#e3# = variable: p1.Z
            //   p2.#e1# = variable: p2.X
            //   p2.#e2# = variable: p2.Y
            //   p2.#e3# = variable: p2.Z
            //   p3.#e1# = variable: p3.X
            //   p3.#e2# = variable: p3.Y
            //   p3.#e3# = variable: p3.Z
            //   p4.#e1# = variable: p4.X
            //   p4.#e2# = variable: p4.Y
            //   p4.#e3# = variable: p4.Z

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
            double tmp13;
            double tmp14;
            double tmp15;
            double tmp16;
            double tmp17;
            double tmp18;
            double tmp19;

            //Sub-expression: LLDI005F = Power[LLDI000F,2]
            tmp0 = p4.X * p4.X;

            //Sub-expression: LLDI0060 = Power[LLDI0010,2]
            tmp1 = p4.Y * p4.Y;

            //Sub-expression: LLDI0061 = Plus[LLDI005F,LLDI0060]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0062 = Power[LLDI0011,2]
            tmp1 = p4.Z * p4.Z;

            //Sub-expression: LLDI0063 = Plus[LLDI0061,LLDI0062]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0064 = Plus[-1,LLDI0063]
            tmp1 = -1 + tmp0;

            //Sub-expression: LLDI0065 = Times[-1,LLDI0007,LLDI0009]
            tmp2 = -1 * p1.Y * p2.X;

            //Sub-expression: LLDI0066 = Times[LLDI0006,LLDI000A]
            tmp3 = p1.X * p2.Y;

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0068 = Times[LLDI000E,LLDI0067]
            tmp3 = p3.Z * tmp2;

            //Sub-expression: LLDI0069 = Times[-1,LLDI0008,LLDI0009]
            tmp4 = -1 * p1.Z * p2.X;

            //Sub-expression: LLDI006A = Times[LLDI0006,LLDI000B]
            tmp5 = p1.X * p2.Z;

            //Sub-expression: LLDI006B = Plus[LLDI0069,LLDI006A]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI006C = Times[-1,LLDI000D,LLDI006B]
            tmp5 = -1 * p3.Y * tmp4;

            //Sub-expression: LLDI006D = Plus[LLDI0068,LLDI006C]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI006E = Times[-1,LLDI0008,LLDI000A]
            tmp5 = -1 * p1.Z * p2.Y;

            //Sub-expression: LLDI006F = Times[LLDI0007,LLDI000B]
            tmp6 = p1.Y * p2.Z;

            //Sub-expression: LLDI0070 = Plus[LLDI006E,LLDI006F]
            tmp5 = tmp5 + tmp6;

            //Sub-expression: LLDI0071 = Times[LLDI000C,LLDI0070]
            tmp6 = p3.X * tmp5;

            //Sub-expression: LLDI0072 = Plus[LLDI006D,LLDI0071]
            tmp3 = tmp3 + tmp6;

            //Sub-expression: LLDI0073 = Times[Rational[1,2],LLDI0064,LLDI0072]
            tmp1 = 0.5 * tmp1 * tmp3;

            //Sub-expression: LLDI0074 = Power[LLDI000C,2]
            tmp6 = p3.X * p3.X;

            //Sub-expression: LLDI0075 = Power[LLDI000D,2]
            tmp7 = p3.Y * p3.Y;

            //Sub-expression: LLDI0076 = Plus[LLDI0074,LLDI0075]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI0077 = Power[LLDI000E,2]
            tmp7 = p3.Z * p3.Z;

            //Sub-expression: LLDI0078 = Plus[LLDI0076,LLDI0077]
            tmp6 = tmp6 + tmp7;

            //Sub-expression: LLDI0079 = Plus[-1,LLDI0078]
            tmp7 = -1 + tmp6;

            //Sub-expression: LLDI007A = Times[Rational[1,2],LLDI0079,LLDI0067]
            tmp8 = 0.5 * tmp7 * tmp2;

            //Sub-expression: LLDI007B = Power[LLDI0006,2]
            tmp9 = p1.X * p1.X;

            //Sub-expression: LLDI007C = Power[LLDI0007,2]
            tmp10 = p1.Y * p1.Y;

            //Sub-expression: LLDI007D = Plus[LLDI007B,LLDI007C]
            tmp9 = tmp9 + tmp10;

            //Sub-expression: LLDI007E = Power[LLDI0008,2]
            tmp10 = p1.Z * p1.Z;

            //Sub-expression: LLDI007F = Plus[LLDI007D,LLDI007E]
            tmp9 = tmp9 + tmp10;

            //Sub-expression: LLDI0080 = Times[-1,LLDI0009,LLDI007F]
            tmp10 = -1 * p2.X * tmp9;

            //Sub-expression: LLDI0081 = Plus[LLDI0009,LLDI0080]
            tmp10 = p2.X + tmp10;

            //Sub-expression: LLDI0082 = Power[LLDI0009,2]
            tmp11 = p2.X * p2.X;

            //Sub-expression: LLDI0083 = Power[LLDI000A,2]
            tmp12 = p2.Y * p2.Y;

            //Sub-expression: LLDI0084 = Plus[LLDI0082,LLDI0083]
            tmp11 = tmp11 + tmp12;

            //Sub-expression: LLDI0085 = Power[LLDI000B,2]
            tmp12 = p2.Z * p2.Z;

            //Sub-expression: LLDI0086 = Plus[LLDI0084,LLDI0085]
            tmp11 = tmp11 + tmp12;

            //Sub-expression: LLDI0087 = Plus[-1,LLDI0086]
            tmp12 = -1 + tmp11;

            //Sub-expression: LLDI0088 = Times[LLDI0006,LLDI0087]
            tmp13 = p1.X * tmp12;

            //Sub-expression: LLDI0089 = Plus[LLDI0081,LLDI0088]
            tmp10 = tmp10 + tmp13;

            //Sub-expression: LLDI008A = Times[Rational[1,2],LLDI0089]
            tmp10 = 0.5 * tmp10;

            //Sub-expression: LLDI008B = Times[-1,LLDI000D,LLDI008A]
            tmp13 = -1 * p3.Y * tmp10;

            //Sub-expression: LLDI008C = Plus[LLDI007A,LLDI008B]
            tmp8 = tmp8 + tmp13;

            //Sub-expression: LLDI008D = Times[-1,LLDI000A,LLDI007F]
            tmp13 = -1 * p2.Y * tmp9;

            //Sub-expression: LLDI008E = Plus[LLDI000A,LLDI008D]
            tmp13 = p2.Y + tmp13;

            //Sub-expression: LLDI008F = Times[LLDI0007,LLDI0087]
            tmp14 = p1.Y * tmp12;

            //Sub-expression: LLDI0090 = Plus[LLDI008E,LLDI008F]
            tmp13 = tmp13 + tmp14;

            //Sub-expression: LLDI0091 = Times[Rational[1,2],LLDI0090]
            tmp13 = 0.5 * tmp13;

            //Sub-expression: LLDI0092 = Times[LLDI000C,LLDI0091]
            tmp14 = p3.X * tmp13;

            //Sub-expression: LLDI0093 = Plus[LLDI008C,LLDI0092]
            tmp8 = tmp8 + tmp14;

            //Sub-expression: LLDI0094 = Times[-1,LLDI0011,LLDI0093]
            tmp14 = -1 * p4.Z * tmp8;

            //Sub-expression: LLDI0095 = Plus[LLDI0073,LLDI0094]
            tmp1 = tmp1 + tmp14;

            //Sub-expression: LLDI0096 = Times[Rational[1,2],LLDI0079,LLDI006B]
            tmp14 = 0.5 * tmp7 * tmp4;

            //Sub-expression: LLDI0097 = Times[-1,LLDI000E,LLDI008A]
            tmp15 = -1 * p3.Z * tmp10;

            //Sub-expression: LLDI0098 = Plus[LLDI0096,LLDI0097]
            tmp14 = tmp14 + tmp15;

            //Sub-expression: LLDI0099 = Times[-1,LLDI000B,LLDI007F]
            tmp15 = -1 * p2.Z * tmp9;

            //Sub-expression: LLDI009A = Plus[LLDI000B,LLDI0099]
            tmp15 = p2.Z + tmp15;

            //Sub-expression: LLDI009B = Times[LLDI0008,LLDI0087]
            tmp12 = p1.Z * tmp12;

            //Sub-expression: LLDI009C = Plus[LLDI009A,LLDI009B]
            tmp12 = tmp15 + tmp12;

            //Sub-expression: LLDI009D = Times[Rational[1,2],LLDI009C]
            tmp12 = 0.5 * tmp12;

            //Sub-expression: LLDI009E = Times[LLDI000C,LLDI009D]
            tmp15 = p3.X * tmp12;

            //Sub-expression: LLDI009F = Plus[LLDI0098,LLDI009E]
            tmp14 = tmp14 + tmp15;

            //Sub-expression: LLDI00A0 = Times[LLDI0010,LLDI009F]
            tmp15 = p4.Y * tmp14;

            //Sub-expression: LLDI00A1 = Plus[LLDI0095,LLDI00A0]
            tmp1 = tmp1 + tmp15;

            //Sub-expression: LLDI00A2 = Times[Rational[1,2],LLDI0079,LLDI0070]
            tmp7 = 0.5 * tmp7 * tmp5;

            //Sub-expression: LLDI00A3 = Times[-1,LLDI000E,LLDI0091]
            tmp15 = -1 * p3.Z * tmp13;

            //Sub-expression: LLDI00A4 = Plus[LLDI00A2,LLDI00A3]
            tmp7 = tmp7 + tmp15;

            //Sub-expression: LLDI00A5 = Times[LLDI000D,LLDI009D]
            tmp15 = p3.Y * tmp12;

            //Sub-expression: LLDI00A6 = Plus[LLDI00A4,LLDI00A5]
            tmp7 = tmp7 + tmp15;

            //Sub-expression: LLDI00A7 = Times[-1,LLDI000F,LLDI00A6]
            tmp15 = -1 * p4.X * tmp7;

            //Output: LLDI0001 = Plus[LLDI00A1,LLDI00A7]
            bladeScalars[0] = tmp1 + tmp15;

            //Sub-expression: LLDI00A8 = Plus[1,LLDI0063]
            tmp1 = 1 + tmp0;

            //Sub-expression: LLDI00A9 = Times[Rational[1,2],LLDI00A8,LLDI0072]
            tmp1 = 0.5 * tmp1 * tmp3;

            //Sub-expression: LLDI00AA = Plus[1,LLDI0078]
            tmp3 = 1 + tmp6;

            //Sub-expression: LLDI00AB = Times[Rational[1,2],LLDI00AA,LLDI0067]
            tmp2 = 0.5 * tmp3 * tmp2;

            //Sub-expression: LLDI00AC = Plus[1,LLDI007F]
            tmp15 = 1 + tmp9;

            //Sub-expression: LLDI00AD = Times[-1,LLDI0009,LLDI00AC]
            tmp16 = -1 * p2.X * tmp15;

            //Sub-expression: LLDI00AE = Plus[1,LLDI0086]
            tmp17 = 1 + tmp11;

            //Sub-expression: LLDI00AF = Times[LLDI0006,LLDI00AE]
            tmp18 = p1.X * tmp17;

            //Sub-expression: LLDI00B0 = Plus[LLDI00AD,LLDI00AF]
            tmp16 = tmp16 + tmp18;

            //Sub-expression: LLDI00B1 = Times[Rational[1,2],LLDI00B0]
            tmp16 = 0.5 * tmp16;

            //Sub-expression: LLDI00B2 = Times[-1,LLDI000D,LLDI00B1]
            tmp18 = -1 * p3.Y * tmp16;

            //Sub-expression: LLDI00B3 = Plus[LLDI00AB,LLDI00B2]
            tmp2 = tmp2 + tmp18;

            //Sub-expression: LLDI00B4 = Times[-1,LLDI000A,LLDI00AC]
            tmp18 = -1 * p2.Y * tmp15;

            //Sub-expression: LLDI00B5 = Times[LLDI0007,LLDI00AE]
            tmp19 = p1.Y * tmp17;

            //Sub-expression: LLDI00B6 = Plus[LLDI00B4,LLDI00B5]
            tmp18 = tmp18 + tmp19;

            //Sub-expression: LLDI00B7 = Times[Rational[1,2],LLDI00B6]
            tmp18 = 0.5 * tmp18;

            //Sub-expression: LLDI00B8 = Times[LLDI000C,LLDI00B7]
            tmp19 = p3.X * tmp18;

            //Sub-expression: LLDI00B9 = Plus[LLDI00B3,LLDI00B8]
            tmp2 = tmp2 + tmp19;

            //Sub-expression: LLDI00BA = Times[-1,LLDI0011,LLDI00B9]
            tmp19 = -1 * p4.Z * tmp2;

            //Sub-expression: LLDI00BB = Plus[LLDI00A9,LLDI00BA]
            tmp1 = tmp1 + tmp19;

            //Sub-expression: LLDI00BC = Times[Rational[1,2],LLDI00AA,LLDI006B]
            tmp4 = 0.5 * tmp3 * tmp4;

            //Sub-expression: LLDI00BD = Times[-1,LLDI000E,LLDI00B1]
            tmp19 = -1 * p3.Z * tmp16;

            //Sub-expression: LLDI00BE = Plus[LLDI00BC,LLDI00BD]
            tmp4 = tmp4 + tmp19;

            //Sub-expression: LLDI00BF = Times[-1,LLDI000B,LLDI00AC]
            tmp15 = -1 * p2.Z * tmp15;

            //Sub-expression: LLDI00C0 = Times[LLDI0008,LLDI00AE]
            tmp17 = p1.Z * tmp17;

            //Sub-expression: LLDI00C1 = Plus[LLDI00BF,LLDI00C0]
            tmp15 = tmp15 + tmp17;

            //Sub-expression: LLDI00C2 = Times[Rational[1,2],LLDI00C1]
            tmp15 = 0.5 * tmp15;

            //Sub-expression: LLDI00C3 = Times[LLDI000C,LLDI00C2]
            tmp17 = p3.X * tmp15;

            //Sub-expression: LLDI00C4 = Plus[LLDI00BE,LLDI00C3]
            tmp4 = tmp4 + tmp17;

            //Sub-expression: LLDI00C5 = Times[LLDI0010,LLDI00C4]
            tmp17 = p4.Y * tmp4;

            //Sub-expression: LLDI00C6 = Plus[LLDI00BB,LLDI00C5]
            tmp1 = tmp1 + tmp17;

            //Sub-expression: LLDI00C7 = Times[Rational[1,2],LLDI00AA,LLDI0070]
            tmp3 = 0.5 * tmp3 * tmp5;

            //Sub-expression: LLDI00C8 = Times[-1,LLDI000E,LLDI00B7]
            tmp5 = -1 * p3.Z * tmp18;

            //Sub-expression: LLDI00C9 = Plus[LLDI00C7,LLDI00C8]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI00CA = Times[LLDI000D,LLDI00C2]
            tmp5 = p3.Y * tmp15;

            //Sub-expression: LLDI00CB = Plus[LLDI00C9,LLDI00CA]
            tmp3 = tmp3 + tmp5;

            //Sub-expression: LLDI00CC = Times[-1,LLDI000F,LLDI00CB]
            tmp5 = -1 * p4.X * tmp3;

            //Output: LLDI0002 = Plus[LLDI00C6,LLDI00CC]
            bladeScalars[1] = tmp1 + tmp5;

            //Sub-expression: LLDI00CD = Times[LLDI0063,LLDI0093]
            tmp1 = tmp0 * tmp8;

            //Sub-expression: LLDI00CE = Plus[LLDI0093,LLDI00CD]
            tmp1 = tmp8 + tmp1;

            //Sub-expression: LLDI00CF = Plus[LLDI00CE,LLDI00B9]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00D0 = Times[-1,LLDI0063,LLDI00B9]
            tmp2 = -1 * tmp0 * tmp2;

            //Sub-expression: LLDI00D1 = Plus[LLDI00CF,LLDI00D0]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00D2 = Times[LLDI0078,LLDI008A]
            tmp2 = tmp6 * tmp10;

            //Sub-expression: LLDI00D3 = Plus[LLDI008A,LLDI00D2]
            tmp2 = tmp10 + tmp2;

            //Sub-expression: LLDI00D4 = Plus[LLDI00D3,LLDI00B1]
            tmp2 = tmp2 + tmp16;

            //Sub-expression: LLDI00D5 = Times[-1,LLDI0078,LLDI00B1]
            tmp5 = -1 * tmp6 * tmp16;

            //Sub-expression: LLDI00D6 = Plus[LLDI00D4,LLDI00D5]
            tmp2 = tmp2 + tmp5;

            //Sub-expression: LLDI00D7 = Times[-1,LLDI0086]
            tmp5 = -tmp11;

            //Sub-expression: LLDI00D8 = Plus[LLDI007F,LLDI00D7]
            tmp5 = tmp9 + tmp5;

            //Sub-expression: LLDI00D9 = Times[Rational[1,2],LLDI00D8]
            tmp5 = 0.5 * tmp5;

            //Sub-expression: LLDI00DA = Times[2,LLDI000C,LLDI00D9]
            tmp8 = 2 * p3.X * tmp5;

            //Sub-expression: LLDI00DB = Plus[LLDI00D6,LLDI00DA]
            tmp2 = tmp2 + tmp8;

            //Sub-expression: LLDI00DC = Times[Rational[1,2],LLDI00DB]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI00DD = Times[2,LLDI0010,LLDI00DC]
            tmp8 = 2 * p4.Y * tmp2;

            //Sub-expression: LLDI00DE = Plus[LLDI00D1,LLDI00DD]
            tmp1 = tmp1 + tmp8;

            //Sub-expression: LLDI00DF = Times[LLDI0078,LLDI0091]
            tmp8 = tmp6 * tmp13;

            //Sub-expression: LLDI00E0 = Plus[LLDI0091,LLDI00DF]
            tmp8 = tmp13 + tmp8;

            //Sub-expression: LLDI00E1 = Plus[LLDI00E0,LLDI00B7]
            tmp8 = tmp8 + tmp18;

            //Sub-expression: LLDI00E2 = Times[-1,LLDI0078,LLDI00B7]
            tmp9 = -1 * tmp6 * tmp18;

            //Sub-expression: LLDI00E3 = Plus[LLDI00E1,LLDI00E2]
            tmp8 = tmp8 + tmp9;

            //Sub-expression: LLDI00E4 = Times[2,LLDI000D,LLDI00D9]
            tmp9 = 2 * p3.Y * tmp5;

            //Sub-expression: LLDI00E5 = Plus[LLDI00E3,LLDI00E4]
            tmp8 = tmp8 + tmp9;

            //Sub-expression: LLDI00E6 = Times[Rational[1,2],LLDI00E5]
            tmp8 = 0.5 * tmp8;

            //Sub-expression: LLDI00E7 = Times[-2,LLDI000F,LLDI00E6]
            tmp9 = -2 * p4.X * tmp8;

            //Sub-expression: LLDI00E8 = Plus[LLDI00DE,LLDI00E7]
            tmp1 = tmp1 + tmp9;

            //Output: LLDI0003 = Times[Rational[1,2],LLDI00E8]
            bladeScalars[2] = 0.5 * tmp1;

            //Sub-expression: LLDI00E9 = Times[LLDI0063,LLDI009F]
            tmp1 = tmp0 * tmp14;

            //Sub-expression: LLDI00EA = Plus[LLDI009F,LLDI00E9]
            tmp1 = tmp14 + tmp1;

            //Sub-expression: LLDI00EB = Plus[LLDI00EA,LLDI00C4]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI00EC = Times[-1,LLDI0063,LLDI00C4]
            tmp4 = -1 * tmp0 * tmp4;

            //Sub-expression: LLDI00ED = Plus[LLDI00EB,LLDI00EC]
            tmp1 = tmp1 + tmp4;

            //Sub-expression: LLDI00EE = Times[2,LLDI0011,LLDI00DC]
            tmp2 = 2 * p4.Z * tmp2;

            //Sub-expression: LLDI00EF = Plus[LLDI00ED,LLDI00EE]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00F0 = Times[LLDI0078,LLDI009D]
            tmp2 = tmp6 * tmp12;

            //Sub-expression: LLDI00F1 = Plus[LLDI009D,LLDI00F0]
            tmp2 = tmp12 + tmp2;

            //Sub-expression: LLDI00F2 = Plus[LLDI00F1,LLDI00C2]
            tmp2 = tmp2 + tmp15;

            //Sub-expression: LLDI00F3 = Times[-1,LLDI0078,LLDI00C2]
            tmp4 = -1 * tmp6 * tmp15;

            //Sub-expression: LLDI00F4 = Plus[LLDI00F2,LLDI00F3]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI00F5 = Times[2,LLDI000E,LLDI00D9]
            tmp4 = 2 * p3.Z * tmp5;

            //Sub-expression: LLDI00F6 = Plus[LLDI00F4,LLDI00F5]
            tmp2 = tmp2 + tmp4;

            //Sub-expression: LLDI00F7 = Times[Rational[1,2],LLDI00F6]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI00F8 = Times[-2,LLDI000F,LLDI00F7]
            tmp4 = -2 * p4.X * tmp2;

            //Sub-expression: LLDI00F9 = Plus[LLDI00EF,LLDI00F8]
            tmp1 = tmp1 + tmp4;

            //Output: LLDI0004 = Times[Rational[1,2],LLDI00F9]
            bladeScalars[3] = 0.5 * tmp1;

            //Sub-expression: LLDI00FA = Times[LLDI0063,LLDI00A6]
            tmp1 = tmp0 * tmp7;

            //Sub-expression: LLDI00FB = Plus[LLDI00A6,LLDI00FA]
            tmp1 = tmp7 + tmp1;

            //Sub-expression: LLDI00FC = Plus[LLDI00FB,LLDI00CB]
            tmp1 = tmp1 + tmp3;

            //Sub-expression: LLDI00FD = Times[-1,LLDI0063,LLDI00CB]
            tmp0 = -1 * tmp0 * tmp3;

            //Sub-expression: LLDI00FE = Plus[LLDI00FC,LLDI00FD]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI00FF = Times[2,LLDI0011,LLDI00E6]
            tmp1 = 2 * p4.Z * tmp8;

            //Sub-expression: LLDI0100 = Plus[LLDI00FE,LLDI00FF]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0101 = Times[-2,LLDI0010,LLDI00F7]
            tmp1 = -2 * p4.Y * tmp2;

            //Sub-expression: LLDI0102 = Plus[LLDI0100,LLDI0101]
            tmp0 = tmp0 + tmp1;

            //Output: LLDI0005 = Times[Rational[1,2],LLDI0102]
            bladeScalars[4] = 0.5 * tmp0;


            //Finish GMac Macro Code Generation, 2019-09-11T05:27:17.9520230+02:00

            return new Cga5D4VectorGeometry(
                bladeScalars,
                MultivectorNullSpaceKind.OuterProductNullSpace
            );
        }

        public static Cga5D4VectorGeometry CreateOpnsSphere(Float64Vector3D center, double radius)
        {
            var bladeScalars = new double[5];

            //Begin GMac Macro Code Generation, 2019-09-11T05:23:06.3468968+02:00
            //Macro: main.cga5d.SphereOpns
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 5 total.
            //Computations: 0.933333333333333 average, 14 total.
            //Memory Reads: 1.2 average, 18 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result.#e1^e2^e3^ep# = variable: BladeScalars[0]
            //   result.#e1^e2^e3^en# = variable: BladeScalars[1]
            //   result.#e1^e2^ep^en# = variable: BladeScalars[2]
            //   result.#e1^e3^ep^en# = variable: BladeScalars[3]
            //   result.#e2^e3^ep^en# = variable: BladeScalars[4]
            //   center.#e1# = variable: center.X
            //   center.#e2# = variable: center.Y
            //   center.#e3# = variable: center.Z
            //   radius = variable: radius

            double tmp0;
            double tmp1;
            double tmp2;

            //Output: LLDI0003 = Times[-1,LLDI0008]
            bladeScalars[2] = -center.Z;

            //Output: LLDI0004 = LLDI0007
            bladeScalars[3] = center.Y;

            //Output: LLDI0005 = Times[-1,LLDI0006]
            bladeScalars[4] = -center.X;

            //Sub-expression: LLDI000E = Power[LLDI0009,2]
            tmp0 = radius * radius;

            //Sub-expression: LLDI000F = Times[-1,LLDI000E]
            tmp0 = -tmp0;

            //Sub-expression: LLDI0010 = Power[LLDI0006,2]
            tmp1 = center.X * center.X;

            //Sub-expression: LLDI0011 = Power[LLDI0007,2]
            tmp2 = center.Y * center.Y;

            //Sub-expression: LLDI0012 = Plus[LLDI0010,LLDI0011]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0013 = Power[LLDI0008,2]
            tmp2 = center.Z * center.Z;

            //Sub-expression: LLDI0014 = Plus[LLDI0012,LLDI0013]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0015 = Plus[LLDI000F,LLDI0014]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0016 = Plus[1,LLDI0015]
            tmp1 = 1 + tmp0;

            //Output: LLDI0001 = Times[Rational[1,2],LLDI0016]
            bladeScalars[0] = 0.5 * tmp1;

            //Sub-expression: LLDI0017 = Plus[-1,LLDI0015]
            tmp0 = -1 + tmp0;

            //Output: LLDI0002 = Times[Rational[1,2],LLDI0017]
            bladeScalars[1] = 0.5 * tmp0;


            //Finish GMac Macro Code Generation, 2019-09-11T05:23:06.3658850+02:00

            return new Cga5D4VectorGeometry(
                bladeScalars,
                MultivectorNullSpaceKind.OuterProductNullSpace
            );
        }

        public static Cga5D1VectorGeometry CreateIpnsSphere(Float64Vector3D p1, Float64Vector3D p2, Float64Vector3D p3, Float64Vector3D p4)
        {
            var bladeScalars = new double[5];

            //Begin GMac Macro Code Generation, 2019-09-11T05:33:28.8259986+02:00
            //Macro: main.cga5d.SphereFromPointsIpns
            //Input Variables: 12 used, 0 not used, 12 total.
            //Temp Variables: 167 sub-expressions, 0 generated temps, 167 total.
            //Target Temp Variables: 19 total.
            //Output Variables: 5 total.
            //Computations: 1.25 average, 215 total.
            //Memory Reads: 1.7906976744186 average, 308 total.
            //Memory Writes: 172 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: BladeScalars[0]
            //   result.#e2# = variable: BladeScalars[1]
            //   result.#e3# = variable: BladeScalars[2]
            //   result.#ep# = variable: BladeScalars[3]
            //   result.#en# = variable: BladeScalars[4]
            //   p1.#e1# = variable: p1.X
            //   p1.#e2# = variable: p1.Y
            //   p1.#e3# = variable: p1.Z
            //   p2.#e1# = variable: p2.X
            //   p2.#e2# = variable: p2.Y
            //   p2.#e3# = variable: p2.Z
            //   p3.#e1# = variable: p3.X
            //   p3.#e2# = variable: p3.Y
            //   p3.#e3# = variable: p3.Z
            //   p4.#e1# = variable: p4.X
            //   p4.#e2# = variable: p4.Y
            //   p4.#e3# = variable: p4.Z

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
            double tmp13;
            double tmp14;
            double tmp15;
            double tmp16;
            double tmp17;
            double tmp18;

            //Sub-expression: LLDI0063 = Power[LLDI000C,2]
            tmp0 = p3.X * p3.X;

            //Sub-expression: LLDI0064 = Power[LLDI000D,2]
            tmp1 = p3.Y * p3.Y;

            //Sub-expression: LLDI0065 = Plus[LLDI0063,LLDI0064]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0066 = Power[LLDI000E,2]
            tmp1 = p3.Z * p3.Z;

            //Sub-expression: LLDI0067 = Plus[LLDI0065,LLDI0066]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0069 = Times[-1,LLDI0008,LLDI000A]
            tmp1 = -1 * p1.Z * p2.Y;

            //Sub-expression: LLDI006A = Times[LLDI0007,LLDI000B]
            tmp2 = p1.Y * p2.Z;

            //Sub-expression: LLDI006B = Plus[LLDI0069,LLDI006A]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI006D = Power[LLDI0006,2]
            tmp2 = p1.X * p1.X;

            //Sub-expression: LLDI006E = Power[LLDI0007,2]
            tmp3 = p1.Y * p1.Y;

            //Sub-expression: LLDI006F = Plus[LLDI006D,LLDI006E]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0070 = Power[LLDI0008,2]
            tmp3 = p1.Z * p1.Z;

            //Sub-expression: LLDI0071 = Plus[LLDI006F,LLDI0070]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI0074 = Power[LLDI0009,2]
            tmp3 = p2.X * p2.X;

            //Sub-expression: LLDI0075 = Power[LLDI000A,2]
            tmp4 = p2.Y * p2.Y;

            //Sub-expression: LLDI0076 = Plus[LLDI0074,LLDI0075]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0077 = Power[LLDI000B,2]
            tmp4 = p2.Z * p2.Z;

            //Sub-expression: LLDI0078 = Plus[LLDI0076,LLDI0077]
            tmp3 = tmp3 + tmp4;

            //Sub-expression: LLDI0086 = Power[LLDI000F,2]
            tmp4 = p4.X * p4.X;

            //Sub-expression: LLDI0087 = Power[LLDI0010,2]
            tmp5 = p4.Y * p4.Y;

            //Sub-expression: LLDI0088 = Plus[LLDI0086,LLDI0087]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI0089 = Power[LLDI0011,2]
            tmp5 = p4.Z * p4.Z;

            //Sub-expression: LLDI008A = Plus[LLDI0088,LLDI0089]
            tmp4 = tmp4 + tmp5;

            //Sub-expression: LLDI008D = Plus[1,LLDI0067]
            tmp5 = 1 + tmp0;

            //Sub-expression: LLDI008E = Times[Rational[1,2],LLDI008D,LLDI006B]
            tmp6 = 0.5 * tmp5 * tmp1;

            //Sub-expression: LLDI008F = Plus[1,LLDI0071]
            tmp7 = 1 + tmp2;

            //Sub-expression: LLDI0090 = Times[-1,LLDI000A,LLDI008F]
            tmp8 = -1 * p2.Y * tmp7;

            //Sub-expression: LLDI0091 = Plus[1,LLDI0078]
            tmp9 = 1 + tmp3;

            //Sub-expression: LLDI0092 = Times[LLDI0007,LLDI0091]
            tmp10 = p1.Y * tmp9;

            //Sub-expression: LLDI0093 = Plus[LLDI0090,LLDI0092]
            tmp8 = tmp8 + tmp10;

            //Sub-expression: LLDI0094 = Times[Rational[1,2],LLDI0093]
            tmp8 = 0.5 * tmp8;

            //Sub-expression: LLDI0095 = Times[-1,LLDI000E,LLDI0094]
            tmp10 = -1 * p3.Z * tmp8;

            //Sub-expression: LLDI0096 = Plus[LLDI008E,LLDI0095]
            tmp6 = tmp6 + tmp10;

            //Sub-expression: LLDI0097 = Times[-1,LLDI000B,LLDI008F]
            tmp10 = -1 * p2.Z * tmp7;

            //Sub-expression: LLDI0098 = Times[LLDI0008,LLDI0091]
            tmp11 = p1.Z * tmp9;

            //Sub-expression: LLDI0099 = Plus[LLDI0097,LLDI0098]
            tmp10 = tmp10 + tmp11;

            //Sub-expression: LLDI009A = Times[Rational[1,2],LLDI0099]
            tmp10 = 0.5 * tmp10;

            //Sub-expression: LLDI009B = Times[LLDI000D,LLDI009A]
            tmp11 = p3.Y * tmp10;

            //Sub-expression: LLDI009C = Plus[LLDI0096,LLDI009B]
            tmp6 = tmp6 + tmp11;

            //Sub-expression: LLDI00B7 = Times[-1,LLDI0008,LLDI0009]
            tmp11 = -1 * p1.Z * p2.X;

            //Sub-expression: LLDI00B8 = Times[LLDI0006,LLDI000B]
            tmp12 = p1.X * p2.Z;

            //Sub-expression: LLDI00B9 = Plus[LLDI00B7,LLDI00B8]
            tmp11 = tmp11 + tmp12;

            //Sub-expression: LLDI00C6 = Times[Rational[1,2],LLDI008D,LLDI00B9]
            tmp12 = 0.5 * tmp5 * tmp11;

            //Sub-expression: LLDI00C7 = Times[-1,LLDI0009,LLDI008F]
            tmp7 = -1 * p2.X * tmp7;

            //Sub-expression: LLDI00C8 = Times[LLDI0006,LLDI0091]
            tmp9 = p1.X * tmp9;

            //Sub-expression: LLDI00C9 = Plus[LLDI00C7,LLDI00C8]
            tmp7 = tmp7 + tmp9;

            //Sub-expression: LLDI00CA = Times[Rational[1,2],LLDI00C9]
            tmp7 = 0.5 * tmp7;

            //Sub-expression: LLDI00CB = Times[-1,LLDI000E,LLDI00CA]
            tmp9 = -1 * p3.Z * tmp7;

            //Sub-expression: LLDI00CC = Plus[LLDI00C6,LLDI00CB]
            tmp9 = tmp12 + tmp9;

            //Sub-expression: LLDI00CD = Times[LLDI000C,LLDI009A]
            tmp12 = p3.X * tmp10;

            //Sub-expression: LLDI00CE = Plus[LLDI00CC,LLDI00CD]
            tmp9 = tmp9 + tmp12;

            //Sub-expression: LLDI00DF = Times[-1,LLDI0007,LLDI0009]
            tmp12 = -1 * p1.Y * p2.X;

            //Sub-expression: LLDI00E0 = Times[LLDI0006,LLDI000A]
            tmp13 = p1.X * p2.Y;

            //Sub-expression: LLDI00E1 = Plus[LLDI00DF,LLDI00E0]
            tmp12 = tmp12 + tmp13;

            //Sub-expression: LLDI00E9 = Times[Rational[1,2],LLDI008D,LLDI00E1]
            tmp5 = 0.5 * tmp5 * tmp12;

            //Sub-expression: LLDI00EA = Times[-1,LLDI000D,LLDI00CA]
            tmp13 = -1 * p3.Y * tmp7;

            //Sub-expression: LLDI00EB = Plus[LLDI00E9,LLDI00EA]
            tmp5 = tmp5 + tmp13;

            //Sub-expression: LLDI00EC = Times[LLDI000C,LLDI0094]
            tmp13 = p3.X * tmp8;

            //Sub-expression: LLDI00ED = Plus[LLDI00EB,LLDI00EC]
            tmp5 = tmp5 + tmp13;

            //Sub-expression: LLDI00F5 = Plus[1,LLDI008A]
            tmp13 = 1 + tmp4;

            //Sub-expression: LLDI00F6 = Times[LLDI000E,LLDI00E1]
            tmp14 = p3.Z * tmp12;

            //Sub-expression: LLDI00F7 = Times[-1,LLDI000D,LLDI00B9]
            tmp15 = -1 * p3.Y * tmp11;

            //Sub-expression: LLDI00F8 = Plus[LLDI00F6,LLDI00F7]
            tmp14 = tmp14 + tmp15;

            //Sub-expression: LLDI00F9 = Times[LLDI000C,LLDI006B]
            tmp15 = p3.X * tmp1;

            //Sub-expression: LLDI00FA = Plus[LLDI00F8,LLDI00F9]
            tmp14 = tmp14 + tmp15;

            //Sub-expression: LLDI00FB = Times[Rational[1,2],LLDI00F5,LLDI00FA]
            tmp13 = 0.5 * tmp13 * tmp14;

            //Sub-expression: LLDI00FC = Times[-1,LLDI0011,LLDI00ED]
            tmp15 = -1 * p4.Z * tmp5;

            //Sub-expression: LLDI00FD = Plus[LLDI00FB,LLDI00FC]
            tmp13 = tmp13 + tmp15;

            //Sub-expression: LLDI00FE = Times[LLDI0010,LLDI00CE]
            tmp15 = p4.Y * tmp9;

            //Sub-expression: LLDI00FF = Plus[LLDI00FD,LLDI00FE]
            tmp13 = tmp13 + tmp15;

            //Sub-expression: LLDI0100 = Times[-1,LLDI000F,LLDI009C]
            tmp15 = -1 * p4.X * tmp6;

            //Sub-expression: LLDI0101 = Plus[LLDI00FF,LLDI0100]
            tmp13 = tmp13 + tmp15;

            //Output: LLDI0004 = Times[-1,LLDI0101]
            bladeScalars[3] = -tmp13;

            //Sub-expression: LLDI0068 = Plus[-1,LLDI0067]
            tmp13 = -1 + tmp0;

            //Sub-expression: LLDI006C = Times[Rational[1,2],LLDI0068,LLDI006B]
            tmp1 = 0.5 * tmp13 * tmp1;

            //Sub-expression: LLDI0072 = Times[-1,LLDI000A,LLDI0071]
            tmp15 = -1 * p2.Y * tmp2;

            //Sub-expression: LLDI0073 = Plus[LLDI000A,LLDI0072]
            tmp15 = p2.Y + tmp15;

            //Sub-expression: LLDI0079 = Plus[-1,LLDI0078]
            tmp16 = -1 + tmp3;

            //Sub-expression: LLDI007A = Times[LLDI0007,LLDI0079]
            tmp17 = p1.Y * tmp16;

            //Sub-expression: LLDI007B = Plus[LLDI0073,LLDI007A]
            tmp15 = tmp15 + tmp17;

            //Sub-expression: LLDI007C = Times[Rational[1,2],LLDI007B]
            tmp15 = 0.5 * tmp15;

            //Sub-expression: LLDI007D = Times[-1,LLDI000E,LLDI007C]
            tmp17 = -1 * p3.Z * tmp15;

            //Sub-expression: LLDI007E = Plus[LLDI006C,LLDI007D]
            tmp1 = tmp1 + tmp17;

            //Sub-expression: LLDI007F = Times[-1,LLDI000B,LLDI0071]
            tmp17 = -1 * p2.Z * tmp2;

            //Sub-expression: LLDI0080 = Plus[LLDI000B,LLDI007F]
            tmp17 = p2.Z + tmp17;

            //Sub-expression: LLDI0081 = Times[LLDI0008,LLDI0079]
            tmp18 = p1.Z * tmp16;

            //Sub-expression: LLDI0082 = Plus[LLDI0080,LLDI0081]
            tmp17 = tmp17 + tmp18;

            //Sub-expression: LLDI0083 = Times[Rational[1,2],LLDI0082]
            tmp17 = 0.5 * tmp17;

            //Sub-expression: LLDI0084 = Times[LLDI000D,LLDI0083]
            tmp18 = p3.Y * tmp17;

            //Sub-expression: LLDI0085 = Plus[LLDI007E,LLDI0084]
            tmp1 = tmp1 + tmp18;

            //Sub-expression: LLDI00BA = Times[Rational[1,2],LLDI0068,LLDI00B9]
            tmp11 = 0.5 * tmp13 * tmp11;

            //Sub-expression: LLDI00BB = Times[-1,LLDI0009,LLDI0071]
            tmp18 = -1 * p2.X * tmp2;

            //Sub-expression: LLDI00BC = Plus[LLDI0009,LLDI00BB]
            tmp18 = p2.X + tmp18;

            //Sub-expression: LLDI00BD = Times[LLDI0006,LLDI0079]
            tmp16 = p1.X * tmp16;

            //Sub-expression: LLDI00BE = Plus[LLDI00BC,LLDI00BD]
            tmp16 = tmp18 + tmp16;

            //Sub-expression: LLDI00BF = Times[Rational[1,2],LLDI00BE]
            tmp16 = 0.5 * tmp16;

            //Sub-expression: LLDI00C0 = Times[-1,LLDI000E,LLDI00BF]
            tmp18 = -1 * p3.Z * tmp16;

            //Sub-expression: LLDI00C1 = Plus[LLDI00BA,LLDI00C0]
            tmp11 = tmp11 + tmp18;

            //Sub-expression: LLDI00C2 = Times[LLDI000C,LLDI0083]
            tmp18 = p3.X * tmp17;

            //Sub-expression: LLDI00C3 = Plus[LLDI00C1,LLDI00C2]
            tmp11 = tmp11 + tmp18;

            //Sub-expression: LLDI00E2 = Times[Rational[1,2],LLDI0068,LLDI00E1]
            tmp12 = 0.5 * tmp13 * tmp12;

            //Sub-expression: LLDI00E3 = Times[-1,LLDI000D,LLDI00BF]
            tmp13 = -1 * p3.Y * tmp16;

            //Sub-expression: LLDI00E4 = Plus[LLDI00E2,LLDI00E3]
            tmp12 = tmp12 + tmp13;

            //Sub-expression: LLDI00E5 = Times[LLDI000C,LLDI007C]
            tmp13 = p3.X * tmp15;

            //Sub-expression: LLDI00E6 = Plus[LLDI00E4,LLDI00E5]
            tmp12 = tmp12 + tmp13;

            //Sub-expression: LLDI0102 = Plus[-1,LLDI008A]
            tmp13 = -1 + tmp4;

            //Sub-expression: LLDI0103 = Times[Rational[1,2],LLDI0102,LLDI00FA]
            tmp13 = 0.5 * tmp13 * tmp14;

            //Sub-expression: LLDI0104 = Times[-1,LLDI0011,LLDI00E6]
            tmp14 = -1 * p4.Z * tmp12;

            //Sub-expression: LLDI0105 = Plus[LLDI0103,LLDI0104]
            tmp13 = tmp13 + tmp14;

            //Sub-expression: LLDI0106 = Times[LLDI0010,LLDI00C3]
            tmp14 = p4.Y * tmp11;

            //Sub-expression: LLDI0107 = Plus[LLDI0105,LLDI0106]
            tmp13 = tmp13 + tmp14;

            //Sub-expression: LLDI0108 = Times[-1,LLDI000F,LLDI0085]
            tmp14 = -1 * p4.X * tmp1;

            //Sub-expression: LLDI0109 = Plus[LLDI0107,LLDI0108]
            tmp13 = tmp13 + tmp14;

            //Output: LLDI0005 = Times[-1,LLDI0109]
            bladeScalars[4] = -tmp13;

            //Sub-expression: LLDI008B = Times[LLDI008A,LLDI0085]
            tmp13 = tmp4 * tmp1;

            //Sub-expression: LLDI008C = Plus[LLDI0085,LLDI008B]
            tmp1 = tmp1 + tmp13;

            //Sub-expression: LLDI009D = Plus[LLDI008C,LLDI009C]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI009E = Times[-1,LLDI008A,LLDI009C]
            tmp6 = -1 * tmp4 * tmp6;

            //Sub-expression: LLDI009F = Plus[LLDI009D,LLDI009E]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI00A0 = Times[LLDI0067,LLDI007C]
            tmp6 = tmp0 * tmp15;

            //Sub-expression: LLDI00A1 = Plus[LLDI007C,LLDI00A0]
            tmp6 = tmp15 + tmp6;

            //Sub-expression: LLDI00A2 = Plus[LLDI00A1,LLDI0094]
            tmp6 = tmp6 + tmp8;

            //Sub-expression: LLDI00A3 = Times[-1,LLDI0067,LLDI0094]
            tmp8 = -1 * tmp0 * tmp8;

            //Sub-expression: LLDI00A4 = Plus[LLDI00A2,LLDI00A3]
            tmp6 = tmp6 + tmp8;

            //Sub-expression: LLDI00A5 = Times[-1,LLDI0078]
            tmp3 = -tmp3;

            //Sub-expression: LLDI00A6 = Plus[LLDI0071,LLDI00A5]
            tmp2 = tmp2 + tmp3;

            //Sub-expression: LLDI00A7 = Times[Rational[1,2],LLDI00A6]
            tmp2 = 0.5 * tmp2;

            //Sub-expression: LLDI00A8 = Times[2,LLDI000D,LLDI00A7]
            tmp3 = 2 * p3.Y * tmp2;

            //Sub-expression: LLDI00A9 = Plus[LLDI00A4,LLDI00A8]
            tmp3 = tmp6 + tmp3;

            //Sub-expression: LLDI00AA = Times[Rational[1,2],LLDI00A9]
            tmp3 = 0.5 * tmp3;

            //Sub-expression: LLDI00AB = Times[2,LLDI0011,LLDI00AA]
            tmp6 = 2 * p4.Z * tmp3;

            //Sub-expression: LLDI00AC = Plus[LLDI009F,LLDI00AB]
            tmp1 = tmp1 + tmp6;

            //Sub-expression: LLDI00AD = Times[LLDI0067,LLDI0083]
            tmp6 = tmp0 * tmp17;

            //Sub-expression: LLDI00AE = Plus[LLDI0083,LLDI00AD]
            tmp6 = tmp17 + tmp6;

            //Sub-expression: LLDI00AF = Plus[LLDI00AE,LLDI009A]
            tmp6 = tmp6 + tmp10;

            //Sub-expression: LLDI00B0 = Times[-1,LLDI0067,LLDI009A]
            tmp8 = -1 * tmp0 * tmp10;

            //Sub-expression: LLDI00B1 = Plus[LLDI00AF,LLDI00B0]
            tmp6 = tmp6 + tmp8;

            //Sub-expression: LLDI00B2 = Times[2,LLDI000E,LLDI00A7]
            tmp8 = 2 * p3.Z * tmp2;

            //Sub-expression: LLDI00B3 = Plus[LLDI00B1,LLDI00B2]
            tmp6 = tmp6 + tmp8;

            //Sub-expression: LLDI00B4 = Times[Rational[1,2],LLDI00B3]
            tmp6 = 0.5 * tmp6;

            //Sub-expression: LLDI00B5 = Times[-2,LLDI0010,LLDI00B4]
            tmp8 = -2 * p4.Y * tmp6;

            //Sub-expression: LLDI00B6 = Plus[LLDI00AC,LLDI00B5]
            tmp1 = tmp1 + tmp8;

            //Output: LLDI0001 = Times[Rational[1,2],LLDI00B6]
            bladeScalars[0] = 0.5 * tmp1;

            //Sub-expression: LLDI00D2 = Times[LLDI0067,LLDI00BF]
            tmp1 = tmp0 * tmp16;

            //Sub-expression: LLDI00D3 = Plus[LLDI00BF,LLDI00D2]
            tmp1 = tmp16 + tmp1;

            //Sub-expression: LLDI00D4 = Plus[LLDI00D3,LLDI00CA]
            tmp1 = tmp1 + tmp7;

            //Sub-expression: LLDI00D5 = Times[-1,LLDI0067,LLDI00CA]
            tmp0 = -1 * tmp0 * tmp7;

            //Sub-expression: LLDI00D6 = Plus[LLDI00D4,LLDI00D5]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI00D7 = Times[2,LLDI000C,LLDI00A7]
            tmp1 = 2 * p3.X * tmp2;

            //Sub-expression: LLDI00D8 = Plus[LLDI00D6,LLDI00D7]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI00D9 = Times[Rational[1,2],LLDI00D8]
            tmp0 = 0.5 * tmp0;

            //Sub-expression: LLDI00E7 = Times[LLDI008A,LLDI00E6]
            tmp1 = tmp4 * tmp12;

            //Sub-expression: LLDI00E8 = Plus[LLDI00E6,LLDI00E7]
            tmp1 = tmp12 + tmp1;

            //Sub-expression: LLDI00EE = Plus[LLDI00E8,LLDI00ED]
            tmp1 = tmp1 + tmp5;

            //Sub-expression: LLDI00EF = Times[-1,LLDI008A,LLDI00ED]
            tmp2 = -1 * tmp4 * tmp5;

            //Sub-expression: LLDI00F0 = Plus[LLDI00EE,LLDI00EF]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00F1 = Times[2,LLDI0010,LLDI00D9]
            tmp2 = 2 * p4.Y * tmp0;

            //Sub-expression: LLDI00F2 = Plus[LLDI00F0,LLDI00F1]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00F3 = Times[-2,LLDI000F,LLDI00AA]
            tmp2 = -2 * p4.X * tmp3;

            //Sub-expression: LLDI00F4 = Plus[LLDI00F2,LLDI00F3]
            tmp1 = tmp1 + tmp2;

            //Output: LLDI0003 = Times[Rational[1,2],LLDI00F4]
            bladeScalars[2] = 0.5 * tmp1;

            //Sub-expression: LLDI00C4 = Times[LLDI008A,LLDI00C3]
            tmp1 = tmp4 * tmp11;

            //Sub-expression: LLDI00C5 = Plus[LLDI00C3,LLDI00C4]
            tmp1 = tmp11 + tmp1;

            //Sub-expression: LLDI00CF = Plus[LLDI00C5,LLDI00CE]
            tmp1 = tmp1 + tmp9;

            //Sub-expression: LLDI00D0 = Times[-1,LLDI008A,LLDI00CE]
            tmp2 = -1 * tmp4 * tmp9;

            //Sub-expression: LLDI00D1 = Plus[LLDI00CF,LLDI00D0]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI00DA = Times[2,LLDI0011,LLDI00D9]
            tmp0 = 2 * p4.Z * tmp0;

            //Sub-expression: LLDI00DB = Plus[LLDI00D1,LLDI00DA]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI00DC = Times[-2,LLDI000F,LLDI00B4]
            tmp1 = -2 * p4.X * tmp6;

            //Sub-expression: LLDI00DD = Plus[LLDI00DB,LLDI00DC]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI00DE = Times[Rational[1,2],LLDI00DD]
            tmp0 = 0.5 * tmp0;

            //Output: LLDI0002 = Times[-1,LLDI00DE]
            bladeScalars[1] = -tmp0;


            //Finish GMac Macro Code Generation, 2019-09-11T05:33:28.8289975+02:00

            return new Cga5D1VectorGeometry(
                bladeScalars,
                MultivectorNullSpaceKind.InnerProductNullSpace
            );
        }

        public static Cga5D1VectorGeometry CreateIpnsSphere(Float64Vector3D center, double radius)
        {
            var bladeScalars = new double[5];

            //Begin GMac Macro Code Generation, 2019-09-10T22:56:42.3839498+02:00
            //Macro: main.cga5d.SphereIPNS
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 10 sub-expressions, 0 generated temps, 10 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 5 total.
            //Computations: 0.8 average, 12 total.
            //Memory Reads: 1.2 average, 18 total.
            //Memory Writes: 15 total.
            //
            //Macro Binding Data: 
            //   result.#e1# = variable: BladeScalars[0]
            //   result.#e2# = variable: BladeScalars[1]
            //   result.#e3# = variable: BladeScalars[2]
            //   result.#ep# = variable: BladeScalars[3]
            //   result.#en# = variable: BladeScalars[4]
            //   center.#e1# = variable: center.X
            //   center.#e2# = variable: center.Y
            //   center.#e3# = variable: center.Z
            //   radius = variable: radius

            double tmp0;
            double tmp1;
            double tmp2;

            //Output: LLDI0001 = LLDI0006
            bladeScalars[0] = center.X;

            //Output: LLDI0002 = LLDI0007
            bladeScalars[1] = center.Y;

            //Output: LLDI0003 = LLDI0008
            bladeScalars[2] = center.Z;

            //Sub-expression: LLDI000E = Power[LLDI0009,2]
            tmp0 = radius * radius;

            //Sub-expression: LLDI000F = Times[-1,LLDI000E]
            tmp0 = -tmp0;

            //Sub-expression: LLDI0010 = Power[LLDI0006,2]
            tmp1 = center.X * center.X;

            //Sub-expression: LLDI0011 = Power[LLDI0007,2]
            tmp2 = center.Y * center.Y;

            //Sub-expression: LLDI0012 = Plus[LLDI0010,LLDI0011]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0013 = Power[LLDI0008,2]
            tmp2 = center.Z * center.Z;

            //Sub-expression: LLDI0014 = Plus[LLDI0012,LLDI0013]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0015 = Plus[LLDI000F,LLDI0014]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0016 = Plus[-1,LLDI0015]
            tmp1 = -1 + tmp0;

            //Output: LLDI0004 = Times[Rational[1,2],LLDI0016]
            bladeScalars[3] = 0.5 * tmp1;

            //Sub-expression: LLDI0017 = Plus[1,LLDI0015]
            tmp0 = 1 + tmp0;

            //Output: LLDI0005 = Times[Rational[1,2],LLDI0017]
            bladeScalars[4] = 0.5 * tmp0;


            //Finish GMac Macro Code Generation, 2019-09-10T22:56:42.4019385+02:00

            return new Cga5D1VectorGeometry(
                bladeScalars,
                MultivectorNullSpaceKind.InnerProductNullSpace
            );
        }


        protected Cga5DMultivectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
            : base(bladeScalars, nullSpaceKind)
        {
        }
    }
}