using System;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GAPoT
{
    public static class MultiDerivativeSample
    {
        public static void Execute()
        {
            var processor = ScalarProcessorExpr
                .DefaultProcessor
                .CreateEuclideanRGaProcessor();

            var v1 = @"V Cos[\[Omega] t]".ToExpr();
            var v2 = @"V Cos[\[Omega] t + 2 Pi / 3]".ToExpr();

            //var dv1 = v1.DifferentiateScalar("t", 1);
            //var dv2 = v2.DifferentiateScalar("t", 1);

            var i1 = @"Sqrt[2] Subscript[C,1] Sin[\[Omega] t + Subscript[\[CurlyPhi],1]]".ToExpr();
            var i2 = @"Sqrt[2] Subscript[C,2] Sin[\[Omega] t + Subscript[\[CurlyPhi],2]]".ToExpr();

            var di1 = i1.DifferentiateScalar("t", 1);
            var di2 = i2.DifferentiateScalar("t", 1);

            var z = processor.CreateVector(
                i1, di1,
                i2, di2,
                v1, v2
            );

            var dz1 = z.DifferentiateScalars("t", 1);
            var dz2 = z.DifferentiateScalars("t", 2);
            var dz3 = z.DifferentiateScalars("t", 3);
            var dz4 = z.DifferentiateScalars("t", 4);

            var zOp12 = dz1.Op(dz2).FullSimplifyScalars();
            var zOp123 = zOp12.Op(dz3).FullSimplifyScalars();
            var zOp1234 = zOp123.Op(dz4).FullSimplifyScalars();


            Console.WriteLine($@"$v_1 = {v1.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$v_2 = {v2.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$i_1 = {i1.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$i_2 = {i2.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$z = {z.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d \boldsymbol{{z}}}}{{dt}}={dz1.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d^{{2}}\boldsymbol{{z}}}}{{dt^{{2}}}}={dz2.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d^{{3}}\boldsymbol{{z}}}}{{dt^{{3}}}}={dz3.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d^{{4}}\boldsymbol{{z}}}}{{dt^{{4}}}}={dz4.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d \boldsymbol{{z}}}}{{dt}}\wedge\frac{{d^{{2}}\boldsymbol{{z}}}}{{dt^{{2}}}} = {zOp12.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d \boldsymbol{{z}}}}{{dt}}\wedge\frac{{d^{{2}}\boldsymbol{{z}}}}{{dt^{{2}}}}\wedge\frac{{d^{{3}}\boldsymbol{{z}}}}{{dt^{{3}}}} = {zOp123.GetLaTeX()}$");
            Console.WriteLine();

            Console.WriteLine($@"$\frac{{d \boldsymbol{{z}}}}{{dt}}\wedge\frac{{d^{{2}}\boldsymbol{{z}}}}{{dt^{{2}}}}\wedge\frac{{d^{{3}}\boldsymbol{{z}}}}{{dt^{{3}}}}\wedge\frac{{d^{{4}}\boldsymbol{{z}}}}{{dt^{{4}}}} = {zOp1234.GetLaTeX()}$");
            Console.WriteLine();
        }
    }
}