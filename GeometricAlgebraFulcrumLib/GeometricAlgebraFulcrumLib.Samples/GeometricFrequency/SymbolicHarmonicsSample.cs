using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.SignalProcessing;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using TextComposerLib.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GeometricFrequency;

public static class SymbolicHarmonicsSample
{
    // This is a pre-defined scalar processor for symbolic
    // Wolfram Mathematica scalars using Expr objects
    public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
        = ScalarAlgebraMathematicaProcessor.DefaultProcessor;

    public static uint VSpaceDimension 
        => 3;

    public static int HarmonicsCount
        => 4;

    // Create a 6-dimensional Euclidean geometric algebra processor based on the
    // selected scalar processor
    public static GeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; } 
        = ScalarProcessor.CreateGeometricAlgebraEuclideanProcessor(VSpaceDimension);

    // This is a pre-defined text generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static TextMathematicaComposer TextComposer { get; }
        = TextMathematicaComposer.DefaultComposer;

    // This is a pre-defined LaTeX generator for displaying multivectors
    // with symbolic Wolfram Mathematica scalars using Expr objects
    public static LaTeXMathematicaComposer LaTeXComposer { get; }
        = LaTeXMathematicaComposer.DefaultComposer;


    public static void Example1()
    {
        var vSpaceDimensionMin = VSpaceDimension - 1;

        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExprArray = new List<string>();
        for (var i = 1; i <= vSpaceDimensionMin; i++)
        for (var j = 1; j <= HarmonicsCount; j++)
            assumeExprArray.Add($"Subscript[V,{i}{j}]");

        var assumeExpr1 = 
            assumeExprArray.Select(s => $"{s} > 0").Concatenate(", ");

        var assumeExpr2 = 
            assumeExprArray.Concatenate(" | ");

        var assumeExpr = 
            $@"And[t >= 0, t <= 1, \[Omega] > 0, {assumeExpr1}, Element[t | \[Omega] | {assumeExpr2}, Reals], Element[a, Vectors[{VSpaceDimension}, Reals]]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);
        var wtArray = 
            Enumerable
                .Range(1, HarmonicsCount)
                .Select(k => k * w * t)
                .ToArray();

        var aVectors = new GaVector<Expr>[HarmonicsCount];
        var bVectors = new GaVector<Expr>[HarmonicsCount];

        for (var i = 0; i < HarmonicsCount; i++)
        {
            var j = i + 1;

            aVectors[i] = 
                Enumerable
                    .Range(0, (int) vSpaceDimensionMin)
                    .Select(k => $"Subscript[V,{k + 1}{j}] * Cos[2 * Pi * {k} / {vSpaceDimensionMin}]".ToExpr())
                    .CreateVector(GeometricProcessor);

            bVectors[i] =
                Enumerable
                    .Range(0, (int) vSpaceDimensionMin)
                    .Select(k => $"Subscript[V,{k + 1}{j}] * Sin[2 * Pi * {k} / {vSpaceDimensionMin}]".ToExpr())
                    .CreateVector(GeometricProcessor);

        }

        var basisVector = GeometricProcessor.CreateVectorBasis(VSpaceDimension - 1);
        var v1 = GeometricProcessor.CreateVectorZero();
        var v2 = GeometricProcessor.CreateVectorZero();
        var v2Dt1 = GeometricProcessor.CreateVectorZero();

        for (var i = 0; i < HarmonicsCount; i++)
        {
            var r = i + 1;

            // Harmonic component using classical term
            v1 += wtArray[i].Cos() * aVectors[i] + wtArray[i].Sin() * bVectors[i];

            // Harmonic component and its derivative using rotor term
            var a1 = aVectors[i];
            var b1 = bVectors[i];
            var a2 = basisVector * a1.Norm();
            var b2 = basisVector * b1.Norm();

            var aSubspace = a1.GetSubspace();
            var bSubspace = b1.GetSubspace();

            var ia = a1.DivideByNorm().Op(basisVector);
            var ib = b1.DivideByNorm().Op(basisVector);

            var ra = GeometricProcessor.CreatePureRotor(wtArray[i], ia);
            var rb = GeometricProcessor.CreatePureRotor(wtArray[i], ib);

            v2 += aSubspace.Project(ra.OmMap(a1)) + 
                  bSubspace.Project(rb.OmMap(b2));

            v2Dt1 += r * w * (
                -(aSubspace.Project(ra.OmMap(a2))) + 
                bSubspace.Project(rb.OmMap(b1))
            );

            //v2 += a.Op(b).GetSubspace().Project(
            //    ra.OmMap(a) + b.Norm() * rb.OmMap(basisVector)
            //);
        }

        var v1Dt1 = v1.DifferentiateScalars(t);
        v2Dt1 = v2Dt1.FullSimplifyScalars();

        var vDiff = (v1 - v2).FullSimplifyScalars();
        var vDt1Diff = (v1Dt1 - v2Dt1).FullSimplifyScalars();

        for (var i = 0; i < HarmonicsCount; i++)
        {
            Console.WriteLine(@$"$\boldsymbol{{a}}_{{{i + 1}}}={LaTeXComposer.GetMultivectorText(aVectors[0])}$");
            Console.WriteLine(@$"$\boldsymbol{{b}}_{{{i + 1}}}={LaTeXComposer.GetMultivectorText(bVectors[0])}$");
            Console.WriteLine();
        }

        Console.WriteLine(@$"$\boldsymbol{{v}}_{{1}}={LaTeXComposer.GetMultivectorText(v1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}_{{2}}={LaTeXComposer.GetMultivectorText(v2)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}_{{2}} - \boldsymbol{{v}}_{{1}}={LaTeXComposer.GetMultivectorText(vDiff)}$");
        Console.WriteLine();

        Console.WriteLine(@$"$\boldsymbol{{v}}_{{1}}^{{\prime}}={LaTeXComposer.GetMultivectorText(v1Dt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}_{{2}}^{{\prime}}={LaTeXComposer.GetMultivectorText(v2Dt1)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}_{{2}}^{{\prime}} - \boldsymbol{{v}}_{{1}}^{{\prime}}={LaTeXComposer.GetMultivectorText(vDt1Diff)}$");
        Console.WriteLine();
    }

    public static void Example2()
    {
        LaTeXComposer.BasisName = @"\boldsymbol{\sigma}";

        var assumeExprArray = new List<string>();
        for (var i = 1; i <= VSpaceDimension; i++)
        for (var j = 1; j <= HarmonicsCount; j++)
        {
            assumeExprArray.Add($@"Subscript[V,{i}{j}]");
            assumeExprArray.Add($@"Subscript[\[Phi],{i}{j}]");
        }

        var assumeExpr1 = 
            assumeExprArray.Select(s => $"{s} > 0").Concatenate(", ");

        var assumeExpr2 = 
            assumeExprArray.Concatenate(" | ");

        var assumeExpr = 
            $@"And[t >= 0, \[Omega] > 0, {assumeExpr1}, Element[t | \[Omega] | {assumeExpr2}, Reals]]".ToExpr();

        MathematicaInterface.DefaultCas.SetGlobalAssumptions(assumeExpr);

        var t = "t".CreateScalar(ScalarProcessor);
        var w = @"\[Omega]".CreateScalar(ScalarProcessor);

        var signalComposer = 
            HarmonicSignalComposer<Expr>.Create(t, w);

        var v = GeometricProcessor.CreateVectorZero();

        for (var r = 1; r <= HarmonicsCount; r++)
        {
            var harmonicFactor = 
                r.CreateScalar(ScalarProcessor);

            var magnitudeList =
                Enumerable
                    .Range(1, (int) VSpaceDimension)
                    .Select(k => $@"Subscript[V,{k}{r}]".CreateScalar(ScalarProcessor))
                    .ToArray();

            var phaseList =
                Enumerable
                    .Range(1, (int) VSpaceDimension)
                    .Select(k => $@"Subscript[\[Phi],{k}{r}]".CreateScalar(ScalarProcessor))
                    .ToArray();

            v += signalComposer.GenerateEvenSignalComponents(
                harmonicFactor,
                magnitudeList,
                phaseList
            ).CreateVector(GeometricProcessor);
        }

        var vDt1 = 
            v.DifferentiateScalars(t);

        var vDt1NormSquared = 
            vDt1.NormSquared().MapScalar(s => 
                Mfs.TrigExpand[Mfs.ExpandAll[s]].FullSimplify()
            );

        Console.WriteLine(@$"$\boldsymbol{{v}} = {LaTeXComposer.GetMultivectorText(v)}$");
        Console.WriteLine(@$"$\boldsymbol{{v}}^{{\prime}} = {LaTeXComposer.GetMultivectorText(vDt1)}$");
        Console.WriteLine(@$"$\left\Vert \boldsymbol{{v}}^{{\prime}}\right\Vert ^{{2}} = {LaTeXComposer.GetScalarText(vDt1NormSquared)}$");
        Console.WriteLine();
    }
}