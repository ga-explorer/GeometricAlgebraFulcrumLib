using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.BSplines;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples
{
    public static class SymbolicBSplineSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
            = ScalarProcessorOfWolframExpr.DefaultProcessor;
            
        // Create a 6-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static RGaProcessor<Expr> GeometricProcessor { get; } 
            = ScalarProcessor.CreateEuclideanRGaProcessor();

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static TextComposerExpr TextComposer { get; }
            = TextComposerExpr.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        public static LaTeXComposerExpr LaTeXComposer { get; }
            = LaTeXComposerExpr.DefaultComposer;


        /// <summary>
        /// Display symbolic representations of spline basis of degree 0
        /// </summary>
        public static void Example1()
        {
            var knotVector = new BSplineKnotVector<Expr>(ScalarProcessor);

            var t = "t".ToSymbolExpr();
            var knotValues = new Expr[4];

            for (var i = 0; i < knotValues.Length; i++)
                knotValues[i] = $"Subscript[t, {i}]".ToExpr();

            knotVector.AppendKnot(knotValues[0], 3);
            
            for (var i = 1; i < knotValues.Length - 1; i++)
                knotVector.AppendKnot(knotValues[i], 2);

            knotVector.AppendKnot(knotValues[^1], 3);

            var knotValuesText =
                knotVector
                    .GetKnotValues()
                    .Select(v => LaTeXComposer.GetScalarText(v))
                    .Concatenate(",");

            Console.WriteLine($@"$\boldsymbol{{\mu}}=\left\{{ {knotValuesText} \right\}}$");
            Console.WriteLine();

            for (var index = 0; index < knotVector.Size - 1; index++)
            {
                var boxCar = knotVector.BoxCar(index, t);

                Console.WriteLine($@"$N_{{{index},\boldsymbol{{\mu}}}}^{{0}}\left(t\right)={LaTeXComposer.GetScalarText(boxCar)}$");
                Console.WriteLine();
            }
        }

        public static void Example2()
        {
            var degree = 3;
            var knotVector = new BSplineKnotVector<Expr>(ScalarProcessor);

            var t = "t".ToSymbolExpr();

            var knotValueCount = 6;
            var knotValues = 
                knotValueCount.GetRange().Select(i => Mfs.Rational[i, knotValueCount - 1].Evaluate()).ToArray();

            //var knotValues = 
            //    6.GetRange().Select(i => $"Subscript[t, {i}]".ToExpr()).ToArray();

            //for (var i = 0; i < knotValues.Length; i++)
            //    knotValues[i] = $"Subscript[t, {i}]".ToExpr();

            knotVector.AppendKnot(knotValues[0], 3);
            
            for (var i = 1; i < knotValues.Length - 1; i++)
                knotVector.AppendKnot(knotValues[i], 1);

            knotVector.AppendKnot(knotValues[^1], 3);

            var knotValuesText =
                knotVector
                    .GetKnotValues()
                    .Select(v => LaTeXComposer.GetScalarText(v))
                    .Concatenate(",");

            Console.WriteLine($@"$\boldsymbol{{\mu}}=\left\{{ {knotValuesText} \right\}}$");
            Console.WriteLine();


            var splineBasisSet = knotVector.CreateBSplineBasisSet(degree);

            for (var index = 0; index < splineBasisSet.ControlPointsCount; index++)
            {
                var value = splineBasisSet.GetValue(index, t).Collect(t);

                Console.WriteLine($@"$N_{{{index},\boldsymbol{{\mu}}}}^{{{degree}}}\left(t\right)={LaTeXComposer.GetScalarText(value)}$");
                Console.WriteLine();
            }
        }
    }
}