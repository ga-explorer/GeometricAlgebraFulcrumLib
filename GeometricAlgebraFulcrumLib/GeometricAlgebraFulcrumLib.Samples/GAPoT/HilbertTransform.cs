using System;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class HilbertTransform
    {
        public static void Execute()
        {
            var va = @"Subscript[V,a] Cos[\[Omega] t]".ToExpr();
            var vb = @"Subscript[V,b] Cos[\[Omega] t - 2 Pi / 3]".ToExpr();
            var vc = @"Subscript[V,c] Cos[\[Omega] t - 4 Pi / 3]".ToExpr();

            var hva = va.HilbertTransformScalar("t", @"\[Omega]");
            var hvb = vb.HilbertTransformScalar("t", @"\[Omega]");
            var hvc = vc.HilbertTransformScalar("t", @"\[Omega]");

            var dva = va.DifferentiateScalar("t");
            var dvb = vb.DifferentiateScalar("t");
            var dvc = vc.DifferentiateScalar("t");

            var hdva = dva.HilbertTransformScalar("t", @"\[Omega]");
            var hdvb = dvb.HilbertTransformScalar("t", @"\[Omega]");
            var hdvc = dvc.HilbertTransformScalar("t", @"\[Omega]");

            var v = GaSymbolicUtils.CreateVector(
                va, hva, vb, hvb, vc, hvc
            );

            var dv = GaSymbolicUtils.CreateVector(
                dva, hdva, dvb, hdvb, dvc, hdvc
            );

            var u = 
                GaSymbolicUtils.ScalarProcessor.Op(v, dv);

            Console.WriteLine("Text Format:");
            Console.WriteLine($"v = {v.GetText()}");
            Console.WriteLine();

            Console.WriteLine($"dv = {dv.GetText()}");
            Console.WriteLine();

            Console.WriteLine($"v op dv = {u.GetText()}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("LaTeX Format:");
            Console.WriteLine($"v = {v.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"dv = {dv.GetLaTeX()}");
            Console.WriteLine();

            Console.WriteLine($"v op dv = {u.GetLaTeX()}");
            Console.WriteLine(); 
        }
    }
}