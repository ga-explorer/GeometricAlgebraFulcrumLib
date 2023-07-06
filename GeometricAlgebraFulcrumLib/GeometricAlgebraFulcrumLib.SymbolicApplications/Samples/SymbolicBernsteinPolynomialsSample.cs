using GeometricAlgebraFulcrumLib.Algebra.PolynomialAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples
{
    public static class SymbolicBernsteinPolynomialsSample
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
            = ScalarProcessorOfWolframExpr.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
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
        /// Compute and Display Bernstein Basis polynomial sets of
        /// various degrees in symbolic form
        /// </summary>
        public static void Example1()
        {
            var parameterValue = "t".ToSymbolExpr();
            
            for (var degree = 1; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree: {degree}");
                Console.WriteLine();
                
                var basis = BernsteinBasisSet<Expr>.Create(
                    ScalarProcessor, 
                    degree
                );

                var assumption = 
                    @$"{parameterValue} >= 0 && {parameterValue} <= 1".ToExpr();


                var valueSum1 = ScalarProcessor.ScalarZero;
                var valueSum2 = ScalarProcessor.ScalarZero;
                for (var index = 0; index <= degree; index++)
                {
                    var value = Mfs.Collect[
                        basis.GetValue(index, parameterValue).ScalarValue.FullSimplify(assumption),
                        Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                    ];

                    var value0 = basis.GetValue(index, Expr.INT_ZERO).FullSimplifyScalar();
                    var value1 = basis.GetValue(index, Expr.INT_ONE).FullSimplifyScalar();

                    valueSum1 = ScalarProcessor.Add(valueSum1, value);

                    var bi = $"Subscript[b,{index}]".ToExpr();
                    valueSum2 = ScalarProcessor.Add(
                        valueSum2, 
                        ScalarProcessor.Times(bi, value)
                    );

                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(value)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(0\right) = {LaTeXComposer.GetScalarText(value0)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(1\right) = {LaTeXComposer.GetScalarText(value1)}$");
                    Console.WriteLine();
                }

                valueSum1 = valueSum1.FullSimplify();
                valueSum2 = valueSum2.FullSimplify();

                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum1)}$");
                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}b_{{i}} B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum2)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Compute and Display Generalized Blended Bernstein Basis polynomial sets of
        /// various degrees in symbolic form
        /// </summary>
        public static void Example2()
        {
            var parameterValue = "t".ToSymbolExpr();
            
            for (var degree = 2; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree: {degree}");
                Console.WriteLine();
                
                var basis = new GbBernsteinBasisSet<Expr>(
                    ScalarProcessor, 
                    degree, 
                    t => "Subscript[f, 0, 2]".ToExpr(),
                    t => "Subscript[f, 2, 2]".ToExpr()
                );

                var assumption = 
                    @$"{parameterValue} >= 0 && {parameterValue} <= 1".ToExpr();


                var valueSum1 = ScalarProcessor.ScalarZero;
                var valueSum2 = ScalarProcessor.ScalarZero;
                for (var index = 0; index <= degree; index++)
                {
                    var value = Mfs.Collect[
                        basis.GetValue(index, parameterValue).FullSimplifyScalar(assumption),
                        Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                    ];

                    var value0 = basis.GetValue(index, Expr.INT_ZERO).FullSimplifyScalar();
                    var value1 = basis.GetValue(index, Expr.INT_ONE).FullSimplifyScalar();

                    valueSum1 = ScalarProcessor.Add(valueSum1, value);

                    var bi = $"Subscript[b,{index}]".ToExpr();
                    valueSum2 = ScalarProcessor.Add(
                        valueSum2, 
                        ScalarProcessor.Times(bi, value)
                    );

                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(value)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(0\right) = {LaTeXComposer.GetScalarText(value0)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(1\right) = {LaTeXComposer.GetScalarText(value1)}$");
                    Console.WriteLine();
                }

                valueSum1 = valueSum1.FullSimplify();
                valueSum2 = Mfs.Collect[
                    valueSum2.FullSimplify(),
                    Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                ];

                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum1)}$");
                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}b_{{i}} B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum2)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Compute and display Generalized Blended Trigonometric Bernstein Basis polynomial sets of
        /// various degrees in symbolic form
        /// </summary>
        public static void Example3()
        {
            var parameterValue = "t".ToSymbolExpr();
            
            for (var degree = 2; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree: {degree}");
                Console.WriteLine();

                // This is used for Generalized Blended Trigonometric Bernstein Basis polynomials
                var alpha = @"\[Alpha]".ToExpr();
                var beta = @"\[Beta]".ToExpr();

                var basis = new GbtBernsteinBasisSet<Expr>(ScalarProcessor, degree)
                {
                    Alpha = alpha,
                    Beta = beta
                };

                var assumption =
                    @$"{parameterValue} >= 0 && {parameterValue} <= 1 && {alpha} >= -1 && {alpha} <= 1 && {beta} >= -1 && {beta} <= 1".ToExpr();

                var valueSum1 = ScalarProcessor.ScalarZero;
                var valueSum2 = ScalarProcessor.ScalarZero;
                for (var index = 0; index <= degree; index++)
                {
                    var value = Mfs.Collect[
                        basis.GetValue(index, parameterValue).FullSimplifyScalar(assumption),
                        Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                    ];

                    var value0 = basis.GetValue(index, Expr.INT_ZERO).FullSimplifyScalar();
                    var value1 = basis.GetValue(index, Expr.INT_ONE).FullSimplifyScalar();

                    valueSum1 = ScalarProcessor.Add(valueSum1, value);

                    var bi = $"Subscript[b,{index}]".ToExpr();
                    valueSum2 = ScalarProcessor.Add(
                        valueSum2, 
                        ScalarProcessor.Times(bi, value)
                    );

                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(value)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(0\right) = {LaTeXComposer.GetScalarText(value0)}$");
                    Console.WriteLine($@"$B_{{{index},{degree}}}\left(1\right) = {LaTeXComposer.GetScalarText(value1)}$");
                    Console.WriteLine();
                }

                valueSum1 = valueSum1.FullSimplify();
                valueSum2 = Mfs.Collect[
                    valueSum2.FullSimplify(),
                    Mfs.List["Subscript[f, 0, 2]".ToExpr(), "Subscript[f, 2, 2]".ToExpr()]
                ];

                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum1)}$");
                Console.WriteLine();
                Console.WriteLine($@"$\sum_{{i=0}}^{{{degree}}}b_{{i}} B_{{i,{degree}}}\left(t\right) = {LaTeXComposer.GetScalarText(valueSum2)}$");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Define and display products of pairs of Bernstein basis polynomials and
        /// their integrals
        /// </summary>
        public static void Example4()
        {
            // Define and display set of Bernstein basis polynomials with degree 4
            const int degree = 5;
            
            var t = "t".ToSymbolExpr();
            
            var basisSet = BernsteinBasisSet<Expr>.Create(ScalarProcessor, degree);
            
            var b = new Expr[degree + 1];
            for (var i = 0; i <= degree; i++)
            {
                b[i] = basisSet.GetValue(i, t).ScalarValue;

                Console.WriteLine($@"$B_{{{i},{degree}}} \left( t \right) = {LaTeXComposer.GetScalarText(b[i])}$");
                Console.WriteLine();
            }

            // Define and display products of pairs of Bernstein basis polynomials and
            // their integrals
            var basisSet2 = BernsteinBasisPairProductSet<Expr>.Create(basisSet);
            var basisSet2Integral = BernsteinBasisPairProductIntegralSet<Expr>.Create(basisSet2);

            for (var i = 0; i <= degree; i++)
            {
                for (var j = i; j <= degree; j++)
                {
                    // The direct symbolic method
                    var bij = Mfs.Expand[ScalarProcessor.Times(b[i], b[j])].Evaluate();
                    var fij = Mfs.Expand[bij.IntegrateScalar("t")].Evaluate();
                    var fijAt1 = Mfs.Expand[bij.IntegrateScalar("t", Expr.INT_ZERO, Expr.INT_ONE)].Evaluate();

                    // The programmed method
                    var bij1 = Mfs.Expand[basisSet2.GetValue(i, j, t)].Evaluate();
                    var fij1 = Mfs.Expand[basisSet2Integral.GetValue(i, j, t)].Evaluate();
                    var fij1At1 = Mfs.Expand[basisSet2Integral.GetValue(i, j, Expr.INT_ONE)].Evaluate();

                    var bijDiff = Mfs.Subtract[bij, bij1].FullSimplify();
                    var fijDiff = Mfs.Subtract[fij, fij1].FullSimplify();
                    var fijAt1Diff = Mfs.Subtract[fijAt1, fij1At1].FullSimplify();

                    Console.WriteLine($@"$f_{{{i},{j}}} \left( t \right) = {LaTeXComposer.GetScalarText(bij)}$");
                    Console.WriteLine($@"$f_{{{i},{j}}} \left( t \right) = {LaTeXComposer.GetScalarText(bij1)}$");
                    Console.WriteLine($@"$F_{{{i},{j}}} \left( t \right) = {LaTeXComposer.GetScalarText(fij)}$");
                    Console.WriteLine($@"$F_{{{i},{j}}} \left( t \right) = {LaTeXComposer.GetScalarText(fij1)}$");
                    Console.WriteLine($@"$F_{{{i},{j}}} \left( 1 \right) = {LaTeXComposer.GetScalarText(fijAt1)}$");
                    Console.WriteLine($@"$F_{{{i},{j}}} \left( 1 \right) = {LaTeXComposer.GetScalarText(fij1At1)}$");
                    Console.WriteLine();

                    // These should all be zero
                    Console.WriteLine($@"bijDiff = ${LaTeXComposer.GetScalarText(bijDiff)}$");
                    Console.WriteLine($@"fijDiff = ${LaTeXComposer.GetScalarText(fijDiff)}$");
                    Console.WriteLine($@"fijAt1Diff = ${LaTeXComposer.GetScalarText(fijAt1Diff)}$");
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Define and display the full definite integrals of products of pairs
        /// of Bernstein basis polynomials
        /// </summary>
        public static void Example5()
        {
            for (var degree = 1; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree {degree}");
                Console.WriteLine();

                var basisSet = BernsteinBasisSet<Expr>.Create(ScalarProcessor, degree);
                var basisSet2 = basisSet.CreatePairProductSet();
                var basisSet2Integral = basisSet2.CreateIntegralSet();

                var fArray = basisSet2Integral.GetValuesAt1();

                //var fArray = new Expr[degree + 1, degree + 1];
                //for (var i = 0; i <= degree; i++)
                //{
                //    fArray[i, i] = basisSet2Integral.GetValue(
                //        i, 
                //        i, 
                //        Expr.INT_ONE
                //    );

                //    for (var j = i + 1; j <= degree; j++)
                //    {
                //        var fij = basisSet2Integral.GetValue(
                //            i, 
                //            j, 
                //            Expr.INT_ONE
                //        );

                //        fArray[i, j] = fij;
                //        fArray[j, i] = fij;
                //    }
                //}

                Console.WriteLine($@"$F_{{{degree}}} = {LaTeXComposer.GetArrayText(fArray)}$");
                Console.WriteLine();
            }
        }
        
        /// <summary>
        /// Define and display the full definite integrals of products of pairs
        /// of Generalized Blended Trigonometric Bernstein basis polynomials
        /// </summary>
        public static void Example6()
        {
            var t = "t".ToSymbolExpr();

            for (var degree = 2; degree <= 7; degree++)
            {
                Console.WriteLine($"Degree {degree}");
                Console.WriteLine();

                var basisSet = new GbtBernsteinBasisSet<Expr>(ScalarProcessor, degree)
                {
                    Alpha = @"\[Alpha]".ToExpr(),
                    Beta = @"\[Beta]".ToExpr()
                };

                //var basisSet2 = new BernsteinBasisPairProductSet<Expr>(basisSet);
                //var basisSet2Integral = new BernsteinBasisPairProductIntegralSet<Expr>(basisSet2);

                var fArray = new Expr[degree + 1, degree + 1];
                for (var i = 0; i <= degree; i++)
                {
                    var bi = basisSet.GetValue(i, t);

                    fArray[i, i] = Mfs.Times[bi, bi].IntegrateScalar(
                        t, 
                        Expr.INT_ZERO, 
                        Expr.INT_ONE
                    );

                    for (var j = i + 1; j <= degree; j++)
                    {
                        var bj = basisSet.GetValue(j, t);

                        var fij = Mfs.Times[bi, bj].IntegrateScalar(
                            t, 
                            Expr.INT_ZERO, 
                            Expr.INT_ONE
                        );

                        fArray[i, j] = fij;
                        fArray[j, i] = fij;
                    }
                }

                Console.WriteLine($@"$F_{{{degree}}} = {LaTeXComposer.GetArrayText(fArray)}$");
                Console.WriteLine();
            }
        }
    }
}