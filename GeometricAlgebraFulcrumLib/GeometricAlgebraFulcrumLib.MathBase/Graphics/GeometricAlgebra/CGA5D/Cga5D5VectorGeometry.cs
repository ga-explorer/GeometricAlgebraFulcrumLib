using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D;

namespace GeometricAlgebraFulcrumLib.MathBase.Graphics.GeometricAlgebra.CGA5D
{
    public sealed class Cga5D5VectorGeometry
        : Cga5DkVectorGeometry
    {
        public override int Grade => 5;


        private Cga5D5VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
            : base(bladeScalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(IFloat64Tuple3D point)
        {
            return 0;
        }

        protected override double ComputeSdfIpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T20:59:17.9435771+02:00
            //Macro: main.cga5d.SdfIpns
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 24 sub-expressions, 0 generated temps, 24 total.
            //Target Temp Variables: 2 total.
            //Output Variables: 1 total.
            //Computations: 1.12 average, 28 total.
            //Memory Reads: 1.44 average, 36 total.
            //Memory Writes: 25 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#e1^e2^e3^ep^en# = variable: Scalars[0]

            double tmp0;
            double tmp1;

            //Sub-expression: LLDI001A = Power[LLDI0002,2]
            tmp0 = point.X * point.X;

            //Sub-expression: LLDI001B = Power[LLDI0003,2]
            tmp1 = point.Y * point.Y;

            //Sub-expression: LLDI001C = Plus[LLDI001A,LLDI001B]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001D = Power[LLDI0004,2]
            tmp1 = point.Z * point.Z;

            //Sub-expression: LLDI001E = Plus[LLDI001C,LLDI001D]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001F = Plus[1,LLDI001E]
            tmp1 = 1 + tmp0;

            //Sub-expression: LLDI0020 = Times[Rational[-1,2],LLDI0005,LLDI001F]
            tmp1 = -0.5 * Scalars[0] * tmp1;

            //Sub-expression: LLDI0021 = Power[LLDI0020,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0022 = Plus[-1,LLDI001E]
            tmp0 = -1 + tmp0;

            //Sub-expression: LLDI0023 = Times[Rational[-1,2],LLDI0005,LLDI0022]
            tmp0 = -0.5 * Scalars[0] * tmp0;

            //Sub-expression: LLDI0024 = Power[LLDI0023,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI0025 = Times[-1,LLDI0024]
            tmp0 = -tmp0;

            //Sub-expression: LLDI0026 = Plus[LLDI0021,LLDI0025]
            tmp0 = tmp1 + tmp0;

            //Sub-expression: LLDI0027 = Times[LLDI0004,LLDI0005]
            tmp1 = point.Z * Scalars[0];

            //Sub-expression: LLDI0028 = Power[LLDI0027,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0029 = Times[-1,LLDI0028]
            tmp1 = -tmp1;

            //Sub-expression: LLDI002A = Plus[LLDI0026,LLDI0029]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002B = Times[-1,LLDI0003,LLDI0005]
            tmp1 = -1 * point.Y * Scalars[0];

            //Sub-expression: LLDI002C = Power[LLDI002B,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI002D = Times[-1,LLDI002C]
            tmp1 = -tmp1;

            //Sub-expression: LLDI002E = Plus[LLDI002A,LLDI002D]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI002F = Times[LLDI0002,LLDI0005]
            tmp1 = point.X * Scalars[0];

            //Sub-expression: LLDI0030 = Power[LLDI002F,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0031 = Times[-1,LLDI0030]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI002E,LLDI0031]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T20:59:17.9555721+02:00

            return sdf;
        }
    }
}