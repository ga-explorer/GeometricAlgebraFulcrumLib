using System;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.AngouriMath
{
    public static class Sample2
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the symbolic
            // AngouriMath scalars using Entity objects
            var processor = AngouriMathScalarProcessor.DefaultProcessor;

            // This is a pre-defined text generator for displaying multivectors
            // with symbolic AngouriMath scalars using Entity objects
            var textComposer = AngouriMathTextComposer.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with symbolic AngouriMath scalars using Entity objects
            var latexComposer = AngouriMathLaTeXComposer.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = processor.CreateGaVectorStorage(3, i => $"u_{i + 1}");
            var v = processor.CreateGaVectorStorage(3, i => $"v_{i + 1}");

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