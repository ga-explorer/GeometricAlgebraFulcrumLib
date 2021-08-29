using System;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class MultiDerivativeSample
    {
        public static void Execute()
        {
            var processor = MathematicaScalarProcessor.DefaultProcessor;

            var vab = @"V Cos[\[Omega] t]".ToExpr();
            var vbc = @"V Cos[\[Omega] t - 2 Pi / 3]".ToExpr();

            var ia = @"C Cos[\[Omega] t - \[CurlyPhi]]".ToExpr();
            var ib = @"C Cos[\[Omega] t - \[CurlyPhi] - 2 Pi / 3]".ToExpr();

            var y = processor.CreateGaVectorStorage(
                vab,
                vbc,
                vab.IntegrateScalar("t"),
                vbc.IntegrateScalar("t"),
                ia,
                ib
            );

            var dy1 = y.DifferentiateScalars("t");
            var dy2 = dy1.DifferentiateScalars("t");
            var dy3 = dy2.DifferentiateScalars("t");
            var dy4 = dy3.DifferentiateScalars("t");

            var z1 = processor.Op(y, dy1, dy2, dy3).FullSimplifyScalars();
            var z2 = processor.Op(dy1, dy2, dy3, dy4).FullSimplifyScalars();

            
            Console.WriteLine("LaTeX Output:");
            Console.WriteLine($"y = {y.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"dy1 = {dy1.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"dy2 = {dy2.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"dy3 = {dy3.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"dy4 = {dy4.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"z1 = {z1.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"z2 = {z2.GetLaTeX()}");
            Console.WriteLine();
        }
    }
}