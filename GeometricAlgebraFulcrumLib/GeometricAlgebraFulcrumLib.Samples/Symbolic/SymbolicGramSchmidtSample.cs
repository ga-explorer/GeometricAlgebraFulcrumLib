using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Frames;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic;

public static class SymbolicGramSchmidtSample
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

    public static uint VSpaceDimension 
        => GeometricProcessor.VSpaceDimension;


    public static void Example1()
    {


        var a =
            Enumerable
                .Range(0, (int) VSpaceDimension)
                .Select(k => $"Subscript[a, {k}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var b =
            Enumerable
                .Range(0, (int) VSpaceDimension)
                .Select(k => $"Subscript[b, {k}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var c =
            Enumerable
                .Range(0, (int) VSpaceDimension)
                .Select(k => $"Subscript[c, {k}]".ToExpr())
                .CreateVector(GeometricProcessor);
        
        var d =
            Enumerable
                .Range(0, (int) VSpaceDimension)
                .Select(k => $"Subscript[d, {k}]".ToExpr())
                .CreateVector(GeometricProcessor);

        var frame1 = 
            new[] {a, b, c, d}.ApplyGramSchmidt(false);

        var frame2 = 
            new[] {a, b, c, d}.ApplyGramSchmidtByProjections(false);

        var kappaArray1 = new Scalar<Expr>[VSpaceDimension - 1];
        var kappaArray2 = new Scalar<Expr>[VSpaceDimension - 1];

        for (var i = 0; i < VSpaceDimension - 1; i++)
        {
            kappaArray1[i] = frame1[i + 1].NormSquared() / frame1[i].Norm();
            kappaArray2[i] = frame2[i + 1].NormSquared() / frame2[i].Norm();
        }

        var scalingFactors =
            kappaArray1
                .Zip(kappaArray2)
                .Select(f => (f.First / f.Second))
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