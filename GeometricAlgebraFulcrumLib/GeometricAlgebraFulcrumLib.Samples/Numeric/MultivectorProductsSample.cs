using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Numeric
{
    public static class MultivectorProductsSample
    {
        // This is a pre-defined scalar processor for numeric scalars
        public static ScalarAlgebraFloat64Processor ScalarProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; } 
            = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

        // This is a pre-defined text generator for displaying multivectors
        public static TextFloat64Composer TextComposer { get; }
            = TextFloat64Composer.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        public static LaTeXFloat64Composer LaTeXComposer { get; }
            = LaTeXFloat64Composer.DefaultComposer;


        public static void Example1()
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
            var uvOp = u.Op(v);
            var uvSp = u.Sp(v);
            var uvGp = u.Gp(v);

            // Display a text representation of the vectors and their outer product
            Console.WriteLine($@"u = {textComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"v = {textComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"u op v = {textComposer.GetMultivectorText(uvOp)}");
            Console.WriteLine($@"u sp v = {textComposer.GetScalarText(uvSp)}");
            Console.WriteLine($@"u gp v = {textComposer.GetMultivectorText(uvGp)}");
            Console.WriteLine();

            // Display a LaTeX representation of the vectors and their outer product
            Console.WriteLine($@"\boldsymbol{{u}} = {latexComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"\boldsymbol{{v}} = {latexComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"\boldsymbol{{u}} \wedge \boldsymbol{{v}} = {latexComposer.GetMultivectorText(uvOp)}");
            Console.WriteLine($@"\boldsymbol{{u}} \rfloor \boldsymbol{{v}} = {latexComposer.GetScalarText(uvSp)}");
            Console.WriteLine($@"\boldsymbol{{u}} \boldsymbol{{v}} = {latexComposer.GetMultivectorText(uvGp)}");
            Console.WriteLine();
        }
    }
}
