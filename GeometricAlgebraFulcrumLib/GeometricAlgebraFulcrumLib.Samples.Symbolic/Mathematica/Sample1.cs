using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra.Scalars;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;

namespace GeometricAlgebraFulcrumLib.Samples.Symbolic.Mathematica;

public static class Sample1
{
    public static void Execute()
    {
        // This is a pre-defined scalar processor for symbolic
        // Wolfram Mathematica scalars using Expr objects
        var scalarProcessor = ScalarProcessorOfWolframExpr.Instance;
            
        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        var geometricProcessor = scalarProcessor.CreateEuclideanRGaProcessor();

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        var textComposer = TextComposerExpr.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic Wolfram Mathematica scalars using Expr objects
        var latexComposer = LaTeXComposerOfWolframExpr.DefaultComposer;

        // Create two vectors each having 3 components (a 3-dimensional GA)
        //var u = geometricProcessor.Vector(3, i => $"Subscript[u,{i + 1}]");
        var u = 
            geometricProcessor.VectorTerm(0);

        var v = 
            geometricProcessor.Vector("x", "y", "Sqrt[1 - x * x - y * y]");
        //geometricProcessor.Vector(3, i => $"Subscript[v,{i + 1}]").DivideByENorm();

        var unitBallAssumption =
            "Element[{x, y, z}, Ball[]]".ToExpr();

        var realAssumption =
            "Element[{x, y}, Reals]".ToExpr();

        //var unitLengthAssumption = 
        //    Mfs.Equal[geometricProcessor.ENormSquared(v), Expr.INT_ONE].Evaluate();

        // The rotor is defined to align u to v
        var rotor = u.CreatePureRotor(v, true);

        var e1 = geometricProcessor.VectorTerm(0);
        var e2 = geometricProcessor.VectorTerm(1);
        var e3 = geometricProcessor.VectorTerm(2);
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