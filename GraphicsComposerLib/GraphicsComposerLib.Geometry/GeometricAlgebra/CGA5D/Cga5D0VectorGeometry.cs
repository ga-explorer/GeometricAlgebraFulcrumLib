using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples;

namespace GraphicsComposerLib.Geometry.GeometricAlgebra.CGA5D
{
    public sealed class Cga5D0VectorGeometry 
        : Cga5DkVectorGeometry
    {
        public override int Grade => 0;


        internal Cga5D0VectorGeometry(double[] bladeScalars, MultivectorNullSpaceKind nullSpaceKind)
            : base(bladeScalars, nullSpaceKind)
        {
        }


        protected override double ComputeSdfOpns(IFloat64Tuple3D point)
        {
            //Begin GMac Macro Code Generation, 2019-09-12T14:27:10.3203401+02:00
            //Macro: main.cga5d.SdfOpns
            //Input Variables: 4 used, 0 not used, 4 total.
            //Temp Variables: 21 sub-expressions, 0 generated temps, 21 total.
            //Target Temp Variables: 3 total.
            //Output Variables: 1 total.
            //Computations: 1.09090909090909 average, 24 total.
            //Memory Reads: 1.5 average, 33 total.
            //Memory Writes: 22 total.
            //
            //Macro Binding Data: 
            //   result = variable: var sdf
            //   point.#e1# = variable: point.X
            //   point.#e2# = variable: point.Y
            //   point.#e3# = variable: point.Z
            //   mv.#E0# = variable: Scalars[0]

            double tmp0;
            double tmp1;
            double tmp2;

            //Sub-expression: LLDI001A = Times[LLDI0002,LLDI0005]
            tmp0 = point.X * Scalars[0];

            //Sub-expression: LLDI001B = Power[LLDI001A,2]
            tmp0 = tmp0 * tmp0;

            //Sub-expression: LLDI001C = Times[LLDI0003,LLDI0005]
            tmp1 = point.Y * Scalars[0];

            //Sub-expression: LLDI001D = Power[LLDI001C,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI001E = Plus[LLDI001B,LLDI001D]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI001F = Times[LLDI0004,LLDI0005]
            tmp1 = point.Z * Scalars[0];

            //Sub-expression: LLDI0020 = Power[LLDI001F,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI0021 = Plus[LLDI001E,LLDI0020]
            tmp0 = tmp0 + tmp1;

            //Sub-expression: LLDI0022 = Power[LLDI0002,2]
            tmp1 = point.X * point.X;

            //Sub-expression: LLDI0023 = Power[LLDI0003,2]
            tmp2 = point.Y * point.Y;

            //Sub-expression: LLDI0024 = Plus[LLDI0022,LLDI0023]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0025 = Power[LLDI0004,2]
            tmp2 = point.Z * point.Z;

            //Sub-expression: LLDI0026 = Plus[LLDI0024,LLDI0025]
            tmp1 = tmp1 + tmp2;

            //Sub-expression: LLDI0027 = Plus[-1,LLDI0026]
            tmp2 = -1 + tmp1;

            //Sub-expression: LLDI0028 = Times[Rational[1,2],LLDI0005,LLDI0027]
            tmp2 = 0.5 * Scalars[0] * tmp2;

            //Sub-expression: LLDI0029 = Power[LLDI0028,2]
            tmp2 = tmp2 * tmp2;

            //Sub-expression: LLDI002A = Plus[LLDI0021,LLDI0029]
            tmp0 = tmp0 + tmp2;

            //Sub-expression: LLDI002B = Plus[1,LLDI0026]
            tmp1 = 1 + tmp1;

            //Sub-expression: LLDI002C = Times[Rational[1,2],LLDI0005,LLDI002B]
            tmp1 = 0.5 * Scalars[0] * tmp1;

            //Sub-expression: LLDI002D = Power[LLDI002C,2]
            tmp1 = tmp1 * tmp1;

            //Sub-expression: LLDI002E = Times[-1,LLDI002D]
            tmp1 = -tmp1;

            //Output: LLDI0001 = Plus[LLDI002A,LLDI002E]
            var sdf = tmp0 + tmp1;


            //Finish GMac Macro Code Generation, 2019-09-12T14:27:10.3413284+02:00

            return sdf;
        }

        protected override double ComputeSdfIpns(IFloat64Tuple3D point)
        {
            return 0;
        }
    }
}