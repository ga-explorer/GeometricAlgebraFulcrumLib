using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Algebra.GeometricAlgebra;

public static class MultivectorProductsSample
{
    // This is a pre-defined scalar processor for numeric scalars
    public static ScalarProcessorOfFloat64 ScalarProcessor { get; }
        = ScalarProcessorOfFloat64.Instance;

    // Create a 3-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    public static int VSpaceDimensions
        => 3;

    // This is a pre-defined text generator for displaying multivectors
    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;


    public static void Example1()
    {
        // This is a pre-defined scalar processor for the standard
        // 64-bit floating point scalars
        var scalarProcessor = ScalarProcessorOfFloat64.Instance;

        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        var geometricProcessor = RGaFloat64Processor.Euclidean;

        var vSpaceDimensions = 3;

        // This is a pre-defined text generator for displaying multivectors
        // with 64-bit floating point scalars
        var textComposer = TextComposerFloat64.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with 64-bit floating point scalars
        var latexComposer = LaTeXComposerFloat64.DefaultComposer;

        // Create two GA vectors each having 3 components
        var u = geometricProcessor.Vector(1.2, -1, 1.25);
        var v = geometricProcessor.Vector(2.1, 0.9, 2.1);

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