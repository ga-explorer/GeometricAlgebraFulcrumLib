using System;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.PHCurves
{
    public static class Sample2
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
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


        public static void Execute1()
        {
            var u = 
                GeometricProcessor.CreateVector(Expr.INT_ONE, Expr.INT_ONE, Expr.INT_ONE);
                //GeometricProcessor.CreateVectorBasis(0);

            var v = 
                GeometricProcessor.CreateVector("2".ToExpr(), Expr.INT_ONE, Expr.INT_MINUSONE);

            var scaledRotor = GeometricProcessor.CreateScaledPureRotor(
                u, 
                v
            );

            var scaledRotorInv = 
                scaledRotor.GetPureScaledRotorInverse();

            var v1 = 
                scaledRotor.OmMapVector(u).FullSimplifyScalars();

            var u1 = 
                scaledRotorInv.OmMapVector(v).FullSimplifyScalars();

            Console.WriteLine($@"$u = {LaTeXComposer.GetMultivectorText(u)}$");
            Console.WriteLine($@"$v = {LaTeXComposer.GetMultivectorText(v)}$");
            Console.WriteLine($@"$R = {LaTeXComposer.GetMultivectorText(scaledRotor)}$");
            Console.WriteLine($@"$R u R^{{\sim}} = {LaTeXComposer.GetMultivectorText(v1)}$");
            Console.WriteLine($@"$R^{{\sim}} v R = {LaTeXComposer.GetMultivectorText(u1)}$");
            Console.WriteLine();
        }

        public static void Execute2()
        {
            const int degree = 4;

            var t = "t".ToSymbolExpr();
            
            var basisSet = new BernsteinBasisSet<Expr>(ScalarProcessor, degree);

            var b = new Expr[degree + 1];

            for (var i = 0; i <= degree; i++)
            {
                b[i] = basisSet.GetValue(i, t);

                Console.WriteLine($@"$B_{{{i},{degree}}} \left( t \right) = {LaTeXComposer.GetScalarText(b[i])}$");
                Console.WriteLine();
            }

            for (var i = 0; i <= degree; i++)
            {
                for (var j = i; j <= degree; j++)
                {
                    var bij = ScalarProcessor.Times(b[i], b[j]);
                    var fij = bij.IntegrateScalar(
                        t, 
                        Expr.INT_ZERO, 
                        Expr.INT_ONE
                    );

                    Console.WriteLine($@"$f_{{{i},{j}}} \left( t \right) = {LaTeXComposer.GetScalarText(bij)}$");
                    Console.WriteLine($@"$F_{{{i},{j}}} = {LaTeXComposer.GetScalarText(fij)}$");
                    Console.WriteLine();
                }
            }
        }

        public static void Execute()
        {
            const int degree = 1;

            var t = "t".ToSymbolExpr();
            
            var basisSet = new BernsteinBasisSet<Expr>(ScalarProcessor, degree);

            var e1 = 
                GeometricProcessor.CreateVectorBasis(0);

            var p1 =
                //GeometricProcessor.CreateVector(
                //    "1".ToExpr(),
                //    "1".ToExpr(),
                //    "0".ToExpr()
                //);
                GeometricProcessor.CreateVector(
                    "Subscript[p, 11]".ToExpr(),
                    "Subscript[p, 12]".ToExpr(),
                    "Subscript[p, 13]".ToExpr()
                );

            var d1 = p1 - e1;
            //var d1u = d1.DivideByENorm();

            var a0 = GeometricProcessor.CreateScalar("1/Sqrt[2]".ToExpr());
            var scaledRotor1 = GeometricProcessor.CreateScaledPureRotor(e1, d1 / 2);
            var a1 = GeometricProcessor.CreateMultivector(scaledRotor1.Multivector);

            var d1by2 = scaledRotor1.OmMapVector(e1).FullSimplifyScalars();
            Console.WriteLine($@"$d1/2 = {LaTeXComposer.GetMultivectorText(d1by2)}$");
            Console.WriteLine();

            var a00 = e1;
            var a11 = d1;
            var a01 = (a0.Gp(e1).Gp(a1.Reverse()) + a1.Gp(e1).Gp(a0));//.GetVectorPart();

            var b0 = basisSet.GetValue(0, t);
            var b1 = basisSet.GetValue(1, t);

            var a = a0 * b0 + a1 * b1;

            var cda = a.Gp(e1).Gp(a.Reverse());

            var cd = 
                a00 * b0 * b0 + 
                a01 * b0 * b1 +
                a11 * b1 * b1;

            var cd0 = cda.GetMultivectorStorage().MapScalars(s => s.ReplaceAll(t, Expr.INT_ZERO)).FullSimplifyScalars();
            var cd1 = cda.GetMultivectorStorage().MapScalars(s => s.ReplaceAll(t, Expr.INT_ONE)).FullSimplifyScalars();

            Console.WriteLine($@"$c^{{{{\prime}}}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cda)}$");
            Console.WriteLine($@"$c^{{{{\prime}}}} \left( t \right) = {LaTeXComposer.GetMultivectorText(cd)}$");
            Console.WriteLine($@"$c^{{{{\prime}}}} \left( 0 \right) = {LaTeXComposer.GetMultivectorText(cd0)}$");
            Console.WriteLine($@"$c^{{{{\prime}}}} \left( 1 \right) = {LaTeXComposer.GetMultivectorText(cd1)}$");
            Console.WriteLine();

            return;

            //var a = new Multivector<Expr>[degree + 1];

            //for (var i = 0; i <= degree; i++)
            //    a[i] = $"Subscript[a,{i},0]".ToExpr() + 
            //           GeometricProcessor.CreateBivector(
            //               $"Subscript[a,{i},3]".ToExpr(),
            //               $"Subscript[a,{i},2]".ToExpr(),
            //               $"Subscript[a,{i},1]".ToExpr()
            //           );

            //var scaledRotorPolynomial = basisSet.GetValue(
            //    "t".ToSymbolExpr(),
            //    a.Select(s => s.MultivectorStorage)
            //);

            //var scaledRotorPolynomialReverse = 
            //    GeometricProcessor.Reverse(scaledRotorPolynomial);

            //// Fixed unit vector for rotation operator
            //var e = 
            //    GeometricProcessor.CreateVectorBasis(1);

            //var phCurveHodograph = GeometricProcessor.Gp(
            //    scaledRotorPolynomial,
            //    e.VectorStorage,
            //    scaledRotorPolynomialReverse
            //).GetVectorPart();

            //var assumption = 
            //    @$"Reals".ToExpr();
            //    //@$"t >= 0 && t <= 1".ToExpr();

            //var sigmaPolynomial = 
            //    Mfs.Collect[Mfs.ExpandAll[GeometricProcessor.ENormSquared(phCurveHodograph)], "t".ToSymbolExpr()].FullSimplify(assumption);



            //Console.WriteLine($@"$\boldsymbol{{A}}\left(t\right) = {LaTeXComposer.GetMultivectorText(scaledRotorPolynomial)}$");
            //Console.WriteLine();

            //Console.WriteLine($@"$\boldsymbol{{A}}^{{\sim}}\left(t\right) = {LaTeXComposer.GetMultivectorText(scaledRotorPolynomialReverse)}$");
            //Console.WriteLine();

            //Console.WriteLine($@"$\boldsymbol{{c}}^{{\prime}}\left(t\right) = {LaTeXComposer.GetMultivectorText(phCurveHodograph)}$");
            //Console.WriteLine();
            
            //Console.WriteLine($@"$\sigma \left( t \right) = {LaTeXComposer.GetScalarText(sigmaPolynomial)}$");
            //Console.WriteLine();

            //for (var i = 0; i <= degree; i++)
            //{
            //    Console.WriteLine($"$A_{{{i}}} = {LaTeXComposer.GetMultivectorText(a[i])}$");
            //}
            //Console.WriteLine();

            //for (var i = 0; i <= degree; i++)
            //{
            //    for (var j = 0; j <= i; j++)
            //    {
            //        var aij = 
            //            a[i].Gp(e).Gp(a[j].Reverse()) + 
            //            a[j].Gp(e).Gp(a[i].Reverse());

            //        Console.WriteLine($"$A_{{{i}{j}}} = {LaTeXComposer.GetMultivectorText(aij)}$");
            //        Console.WriteLine();
            //    }
            //}
        }
    }
}