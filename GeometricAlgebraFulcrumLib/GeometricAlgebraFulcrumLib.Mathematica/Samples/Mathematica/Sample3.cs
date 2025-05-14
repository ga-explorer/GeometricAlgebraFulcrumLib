using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Mathematica.Samples.Mathematica;

public static class Sample3
{
    public static void Execute()
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        var geometricProcessor = scalarProcessor.CreateEuclideanXGaProcessor();

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        var textComposer = TextComposerExpr.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        var latexComposer = LaTeXComposerOfWolframExpr.DefaultComposer;

        // Create two vectors each having 3 components (a 3-dimensional GA)
        var u = geometricProcessor.Vector(3, i => $"Subscript[u,{i + 1}]");
        var v = geometricProcessor.Vector(3, i => $"Subscript[v,{i + 1}]");

        // Compute their outer product as a bivector
        var bv = u.Op(v);

        // Display a text representation of the vectors and their outer product
        Console.WriteLine($@"u = {textComposer.GetMultivectorText(u)}");
        Console.WriteLine($@"v = {textComposer.GetMultivectorText(v)}");
        Console.WriteLine($@"u op v = {textComposer.GetMultivectorText(bv)}");
        Console.WriteLine();

        // Display a LaTeX representation of the vectors and their outer product
        Console.WriteLine($@"\boldsymbol{{u}} = {latexComposer.GetMultivectorText(u)}");
        Console.WriteLine($@"\boldsymbol{{v}} = {latexComposer.GetMultivectorText(v)}");
        Console.WriteLine($@"\boldsymbol{{u}}\wedge\boldsymbol{{v}} = {latexComposer.GetMultivectorText(bv)}");
        Console.WriteLine();
    }
}