using System;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for the standard
            // 64-bit floating point scalars
            var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor;

            // Create a 3-dimensional Euclidean geometric algebra processor based on the
            // selected scalar processor
            var geometricProcessor = scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

            // This is a pre-defined text generator for displaying multivectors
            // with 64-bit floating point scalars
            var textComposer = TextFloat64Composer.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with 64-bit floating point scalars
            var latexComposer = LaTeXFloat64Composer.DefaultComposer;

            // Create two GA vectors each having 3 components
            var u = geometricProcessor.CreateVector(1.2, -1, 1.25);
            var v = geometricProcessor.CreateVector(2.1, 0.9, 2.1);

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
}
