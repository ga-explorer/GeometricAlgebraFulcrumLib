using System;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GeometricFrequency
{
    public static class Sample1
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Example1()
        {
            var assumeExpr = "And[V > 0, w > 0]".ToExpr();

            var va = "Sqrt[2] V Cos[w t]".ToExpr();
            var vb = "Sqrt[2] V Cos[w t - 2 Pi / 3]".ToExpr();
            var vc = "Sqrt[2] V Cos[w t + 2 Pi / 3]".ToExpr();

            var v = GeometricProcessor.CreateVector(va, vb, vc);
            var v1 = v.DifferentiateScalars("t", 1).SimplifyScalars(assumeExpr);
            var v2 = v.DifferentiateScalars("t", 2).SimplifyScalars(assumeExpr);
            var v3 = v.DifferentiateScalars("t", 3).SimplifyScalars(assumeExpr);

            var omega = (v1.Gp(v2) / v1.Gp(v1)).SimplifyScalars(assumeExpr);

            var u1 = v1;
            var e1 = u1.DivideByNorm().SimplifyScalars(assumeExpr);
            var e1d = e1.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var u2 = v2 - v2.ProjectOn(u1.GetSubspace());
            var e2 = u2.DivideByNorm().SimplifyScalars(assumeExpr);
            var e2d = e2.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var u3 = v3 - v3.ProjectOn(u1.GetSubspace()) - v3.ProjectOn(u2.GetSubspace());
            var e3 = u3.DivideByNorm().SimplifyScalars(assumeExpr);
            var e3d = e3.DifferentiateScalars("t").SimplifyScalars(assumeExpr);

            var k1 = e1.Sp(e2d).Simplify(assumeExpr);
            var k2 = e2.Sp(e3d).Simplify(assumeExpr);

            Console.WriteLine($@"$\boldsymbol{{v}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine($@"$\boldsymbol{{v}}^{{\prime\prime\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(v3)}$");
            Console.WriteLine($@"$\boldsymbol{{\varOmega}}\left(t\right) = {LaTeXComposer.GetMultivectorText(omega)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{1}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e1d)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{2}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e2d)}$");
            Console.WriteLine($@"$\boldsymbol{{e}}^{{\prime}}_{{3}}\left(t\right) = {LaTeXComposer.GetMultivectorText(e3d)}$");
            Console.WriteLine($@"$\kappa_{{1}}\left(t\right) = {LaTeXComposer.GetScalarText(k1)}$");
            Console.WriteLine($@"$\kappa_{{2}}\left(t\right) = {LaTeXComposer.GetScalarText(k2)}$");
            Console.WriteLine();
        }
    }
}
