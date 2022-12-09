using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Mathematica
{
    public static class Sample1
    {
        public static void Execute()
        {
            // This is a pre-defined scalar processor for symbolic
            // Wolfram Mathematica scalars using Expr objects
            var scalarProcessor = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
            // Create a 3-dimensional Euclidean geometric algebra processor based on the
            // selected scalar processor
            var geometricProcessor = scalarProcessor.CreateGeometricAlgebraEuclideanProcessor(3);

            // This is a pre-defined text generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var textComposer = TextMathematicaComposer.DefaultComposer;

            // This is a pre-defined LaTeX generator for displaying multivectors
            // with symbolic Wolfram Mathematica scalars using Expr objects
            var latexComposer = LaTeXMathematicaComposer.DefaultComposer;

            // Create two vectors each having 3 components (a 3-dimensional GA)
            //var u = geometricProcessor.CreateVectorFromText(3, i => $"Subscript[u,{i + 1}]");
            var u = 
                geometricProcessor.CreateVectorBasis(0);

            var v = 
                geometricProcessor.CreateVectorFromText("x", "y", "Sqrt[1 - x * x - y * y]");
                //geometricProcessor.CreateVectorFromText(3, i => $"Subscript[v,{i + 1}]").DivideByENorm();

            var unitBallAssumption =
                "Element[{x, y, z}, Ball[]]".ToExpr();

            var realAssumption =
                "Element[{x, y}, Reals]".ToExpr();

            //var unitLengthAssumption = 
            //    Mfs.Equal[geometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();

            // The rotor is defined to align u to v
            var rotor = geometricProcessor.CreatePureRotor(u, v, true);

            var e1 = geometricProcessor.CreateVectorBasis(0);
            var e2 = geometricProcessor.CreateVectorBasis(1);
            var e3 = geometricProcessor.CreateVectorBasis(2);
            var y1 = rotor.OmMap(e1).FullSimplifyScalars(realAssumption);
            var y2 = rotor.OmMap(e2).FullSimplifyScalars(realAssumption);
            var y3 = rotor.OmMap(e3).FullSimplifyScalars(realAssumption);

            // Display a text representation of the vectors
            Console.WriteLine($@"u = {textComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"v = {textComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"e1 = {textComposer.GetMultivectorText(e1)}");
            Console.WriteLine($@"e2 = {textComposer.GetMultivectorText(e2)}");
            Console.WriteLine($@"e3 = {textComposer.GetMultivectorText(e3)}");
            Console.WriteLine($@"y1 = {textComposer.GetMultivectorText(y1)}");
            Console.WriteLine($@"y2 = {textComposer.GetMultivectorText(y2)}");
            Console.WriteLine($@"y3 = {textComposer.GetMultivectorText(y3)}");
            Console.WriteLine();

            // Display a LaTeX representation of the vectors
            Console.WriteLine($@"\boldsymbol{{u}} = {latexComposer.GetMultivectorText(u)}");
            Console.WriteLine($@"\boldsymbol{{v}} = {latexComposer.GetMultivectorText(v)}");
            Console.WriteLine($@"\boldsymbol{{e_1}} = {latexComposer.GetMultivectorText(e1)}");
            Console.WriteLine($@"\boldsymbol{{e_2}} = {latexComposer.GetMultivectorText(e2)}");
            Console.WriteLine($@"\boldsymbol{{e_3}} = {latexComposer.GetMultivectorText(e3)}");
            Console.WriteLine($@"\boldsymbol{{y_1}} = {latexComposer.GetMultivectorText(y1)}");
            Console.WriteLine($@"\boldsymbol{{y_2}} = {latexComposer.GetMultivectorText(y2)}");
            Console.WriteLine($@"\boldsymbol{{y_3}} = {latexComposer.GetMultivectorText(y3)}");
            Console.WriteLine();
        }
    }
}