using System;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.TextComposers.Float64;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the standard
            // 64-bit floating point scalars
            var processor = GaScalarProcessorFloat64.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with 64-bit floating point scalars
            var textComposer = GaTextComposerFloat64.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with 64-bit floating point scalars
            var latexComposer = GaLaTeXComposerFloat64.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateStorageVector(1.2, -1, 1.25);
            var v = processor.CreateStorageVector(2.1, 0.9, 2.1);

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
