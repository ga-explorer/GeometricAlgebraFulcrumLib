using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.Mathematica
{
    public static class Sample3
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for symbolic
            // Wolfram Mathematica scalars using Expr objects
            var scalarProcessor = ScalarProcessorOfWolframExpr.DefaultProcessor;
            
            // Create a 3-dimensional Euclidean geometric algebra processor based on the
            // selected scalar processor
            var geometricProcessor = scalarProcessor.CreateEuclideanRGaProcessor();

            // This is a pre-defined text generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var textComposer = TextComposerExpr.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var latexComposer = LaTeXComposerExpr.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            var u = geometricProcessor.CreateVector(3, i => $"Subscript[u,{i + 1}]");
            var v = geometricProcessor.CreateVector(3, i => $"Subscript[v,{i + 1}]");

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