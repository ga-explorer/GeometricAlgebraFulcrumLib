using System;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the symbolic
            // Wolfram Mathematica scalars using Expr objects
            var processor = GaScalarProcessorMathematicaExpr.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var textComposer = GaTextComposerMathematicaExpr.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var latexComposer = GaLaTeXComposerMathematicaExpr.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateStorageVector(3, i => $"Subscript[u,{i + 1}]".ToExpr());
            var v = processor.CreateStorageVector(3, i => $"Subscript[v,{i + 1}]".ToExpr());

            // Compute their outer product as a bivector
            var bv = processor.Op(u, v);

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
}