using System;
using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.PHCurves
{
    /// <summary>
    /// Investigation of Pythagorean Hodograph Curve Mapping using scaled versor reflections
    /// See paper "A geometric product formulation for spatial Pythagorean hodograph curves
    /// with applications to Hermite interpolation"
    /// https://www.sciencedirect.com/science/article/abs/pii/S016783960700009X
    /// </summary>
    public static class Sample1
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


        /// <summary>
        /// Compute and Display Bernstein Basis Sets in symbolic form
        /// </summary>
        public static void Example1()
        {
            var parameterValue = "t".ToSymbolExpr();
            var alpha = @"\[Alpha]".ToExpr();
            var beta = @"\[Beta]".ToExpr();

            for (var degree = 2; degree <= 5; degree++)
            {
                Console.WriteLine($"GBT Bernstein Basis Degree: {degree}");
                Console.WriteLine();

                //var basis = new GbtBernsteinBasis<Expr>(ScalarProcessor, degree)
                //{
                //    Alpha = alpha,
                //    Beta = beta
                //};

                var basis = new GbBernsteinBasisSet<Expr>(
                    ScalarProcessor, 
                    degree, 
                    t => "Subscript[f, 0, 2]".ToExpr(),
                    t => "Subscript[f, 2, 2]".ToExpr()
                );

                var valueSum = ScalarProcessor.ScalarZero;
                var assumption = 
                    @$"{parameterValue} >= 0 && {parameterValue} <= 1".ToExpr();

                //var assumption = 
                //    @$"{parameterValue} >= 0 && {parameterValue} <= 1 && {alpha} >= -1 && {alpha} <= 1 && {beta} >= -1 && {beta} <= 1".ToExpr();

                for (var index = 0; index <= degree; index++)
                {
                    var value = Mfs.Collect[
                        basis.GetValue(index, parameterValue).FullSimplify(assumption),
                        Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                    ];

                    valueSum = ScalarProcessor.Add(valueSum, value);

                    Console.WriteLine($"Index {index}: ${LaTeXComposer.GetScalarText(value)}$");
                }

                valueSum = valueSum.FullSimplify();

                Console.WriteLine();
                Console.WriteLine($"Sum of values: ${LaTeXComposer.GetScalarText(valueSum)}$");
                Console.WriteLine();
            }
        }
    }
}