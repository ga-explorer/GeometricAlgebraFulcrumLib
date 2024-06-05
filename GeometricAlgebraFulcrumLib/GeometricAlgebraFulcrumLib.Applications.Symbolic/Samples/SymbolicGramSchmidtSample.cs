using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.Samples;

public static class SymbolicGramSchmidtSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
        = ScalarProcessorOfWolframExpr.Instance;

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
    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;

    public static uint VSpaceDimensions 
        => 3;


    public static void Example1()
    {


        var a =
            Enumerable
                .Range(0, (int) VSpaceDimensions)
                .Select(k => $"Subscript[a, {k}]".ToExpr())
                .Vector(GeometricProcessor);

        var b =
            Enumerable
                .Range(0, (int) VSpaceDimensions)
                .Select(k => $"Subscript[b, {k}]".ToExpr())
                .Vector(GeometricProcessor);

        var c =
            Enumerable
                .Range(0, (int) VSpaceDimensions)
                .Select(k => $"Subscript[c, {k}]".ToExpr())
                .Vector(GeometricProcessor);
        
        var d =
            Enumerable
                .Range(0, (int) VSpaceDimensions)
                .Select(k => $"Subscript[d, {k}]".ToExpr())
                .Vector(GeometricProcessor);

        var frame1 = 
            new[] {a, b, c, d}.ApplyGramSchmidt(false);

        var frame2 = 
            new[] {a, b, c, d}.ApplyGramSchmidtByProjections(false);

        var kappaArray1 = new IScalar<Expr>[VSpaceDimensions - 1];
        var kappaArray2 = new IScalar<Expr>[VSpaceDimensions - 1];

        for (var i = 0; i < VSpaceDimensions - 1; i++)
        {
            kappaArray1[i] = frame1[i + 1].NormSquared() / frame1[i].Norm();
            kappaArray2[i] = frame2[i + 1].NormSquared() / frame2[i].Norm();
        }

        var scalingFactors =
            kappaArray1
                .Zip(kappaArray2)
                .Select(f => f.First.Divide(f.Second))
                .ToArray();

        for (var i = 0; i < kappaArray1.Length; i++)
        {
            //var v1 = frame1[i];
            //var v2 = frame2[i];
            var scalingFactor = scalingFactors[i];

            //Console.WriteLine(@$"$v_{{{i + 1}}} = {LaTeXComposer.GetMultivectorText(v1)}$");
            //Console.WriteLine(@$"$v_{{{i + 1}}} = {LaTeXComposer.GetMultivectorText(v2)}$");
            Console.WriteLine(@$"$s = {LaTeXComposer.GetScalarText(scalingFactor)}$");
            Console.WriteLine();
        }
    }
}