﻿using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Context.Processors;
using GeometricAlgebraFulcrumLib.MetaProgramming.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Samples.Algebra.AngouriMath;

public static class Sample2
{
    public static void Execute()
    {
        // This is a pre-defined scalar processor for the symbolic
        // AngouriMath scalars using Entity objects
        var scalarProcessor = ScalarProcessorOfAngouriMathEntity.Instance;

        var vSpaceDimensions = 3;

        // Create a 3-dimensional Euclidean geometric algebra processor based on the
        // selected scalar processor
        var geometricProcessor = scalarProcessor.CreateEuclideanXGaProcessor();

        // This is a pre-defined text generator for displaying multivectors
        // with symbolic AngouriMath scalars using Entity objects
        var textComposer = TextComposerEntity.DefaultComposer;

        // This is a pre-defined LaTeX generator for displaying multivectors
        // with symbolic AngouriMath scalars using Entity objects
        var latexComposer = LaTeXAngouriMathComposer.DefaultComposer;

        // Create two vectors each having 3 components (a 3-dimensional GA)
        var u = geometricProcessor.Vector(3, i => $"u_{i + 1}");
        var v = geometricProcessor.Vector(3, i => $"v_{i + 1}");

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