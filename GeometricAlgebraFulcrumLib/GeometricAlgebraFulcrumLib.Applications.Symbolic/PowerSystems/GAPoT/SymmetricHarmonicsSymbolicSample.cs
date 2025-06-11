using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Matrices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Mathematica.Algebra;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Structures.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Utilities.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Applications.Symbolic.PowerSystems.GAPoT;

public static class SymmetricHarmonicsSymbolicSample
{
    public static XGaProcessor<Expr> GeometricProcessor { get; }
        = XGaProcessor<Expr>.CreateEuclidean(ScalarProcessorOfWolframExpr.Instance);

    public static TextComposerExpr TextComposer { get; }
        = TextComposerExpr.DefaultComposer;

    public static LaTeXComposerOfWolframExpr LaTeXComposer { get; }
        = LaTeXComposerOfWolframExpr.DefaultComposer;


    public static void ValidatePhasor(int k, int n, XGaVector<Expr> phasor2)
    {
        var composer = GeometricProcessor.CreateVectorComposer();

        for (var i = 0; i < n; i++)
        {
            var scalar = i == 0
                ? $@"Cos[{k}*\[Theta]]".ToExpr()
                : $@"Cos[{k}*(\[Theta] - 2*Pi*{i}/{n})]".ToExpr();

            composer.SetVectorTerm(i, scalar);
        }

        var phasor1 =
            composer
                .GetVector()
                .MapScalars(
                    scalar => Mfs.FullSimplify[scalar].Evaluate()
                );

        var phasorDiff =
            (phasor1 - phasor2).MapScalars(
                scalar => Mfs.FullSimplify[scalar].Evaluate()
            );

        //Console.WriteLine(
        //    LaTeXComposer.GetMultivectorText(phasor1)
        //);
        //Console.WriteLine();

        //Console.WriteLine(
        //    LaTeXComposer.GetMultivectorText(phasor2)
        //);
        //Console.WriteLine();

        Console.WriteLine("Phasors Difference: ");
        Console.WriteLine(TextComposer.GetTermsText(phasorDiff));
        Console.WriteLine();
    }

    public static Tuple<XGaVector<Expr>, XGaVector<Expr>> GetPhasorsTuple(int i, int k, int n)
    {
        Debug.Assert(n >= 2 && i >= 0 && i < n);

        var mu =
            GeometricProcessor.VectorTerm(i);

        var muSubspace =
            mu.ToSubspace();

        //var muVector =
        //    GeoEuclideanVector<Expr>.Create(mu);

        var angle = i == 0
            ? $@"{k}*\[Theta]".RadiansToPolarAngle(GeometricProcessor.ScalarProcessor)
            : $@"{k}*(\[Theta] - 2*Pi*{i}/{n})".RadiansToPolarAngle(GeometricProcessor.ScalarProcessor);

        var rotor = GeometricProcessor.CreateGivensRotor(
            i, n, angle
        );

        var phasor1 =
            rotor.OmMap(mu).FullSimplifyScalars();

        var phasor2 =
            muSubspace.Project(phasor1).FullSimplifyScalars();


        //Console.WriteLine(
        //    LaTeXComposer.GetMultivectorText(rotor.)
        //);
        //Console.WriteLine();

        //Console.WriteLine(
        //    LaTeXComposer.GetMultivectorText(phasor1)
        //);
        //Console.WriteLine();

        //Console.WriteLine(
        //    LaTeXComposer.GetMultivectorText(phasor2)
        //);
        //Console.WriteLine();

        return new Tuple<XGaVector<Expr>, XGaVector<Expr>>(
            phasor1,
            phasor2
        );
    }

    public static Tuple<XGaVector<Expr>, XGaVector<Expr>> GetPhasorsTuple(int k, int n)
    {
        var phasorTuples =
            Enumerable
                .Range(0, n)
                .Select(i => GetPhasorsTuple(i, k, n))
                .ToArray();

        var phasor1 =
            phasorTuples
                .Select(t => t.Item1)
                .Aggregate(
                    GeometricProcessor.VectorZero,
                    (current, vector) => current + vector
                )
                .MapScalars(scalar =>
                    Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                );

        var phasor2 =
            phasorTuples
                .Select(t => t.Item2)
                .Aggregate(
                    GeometricProcessor.VectorZero,
                    (current, vector) => current + vector
                )
                .MapScalars(scalar =>
                    Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                );

        ValidatePhasor(k, n, phasor2);

        Console.WriteLine(
            TextComposer.GetMultivectorText(phasor1)
        );
        Console.WriteLine();

        Console.WriteLine(
            TextComposer.GetMultivectorText(phasor2)
        );
        Console.WriteLine();

        return new Tuple<XGaVector<Expr>, XGaVector<Expr>>(
            phasor1,
            phasor2
        );
    }

    public static void Execute()
    {
        for (var n = 3; n <= 6; n++)
        {
            Console.WriteLine($"n = {n}:");
            Console.WriteLine();

            var unitKirchhoffVector =
                GeometricProcessor.VectorSymmetricUnit(n);

            var (_, inputPhasor) = GetPhasorsTuple(1, n);

            var e1 =
                GeometricProcessor.VectorTerm(0);

            var e2 =
                GeometricProcessor.VectorTerm(1);

            var mv1 =
                inputPhasor.MapScalars(scalar =>
                    scalar.ReplaceAll(@"\[Theta]", "0")
                );

            var v1 =
                (mv1 / mv1.ENorm()).FullSimplifyScalars();

            Console.WriteLine(
                TextComposer.GetMultivectorText(v1)
            );
            Console.WriteLine();

            var mv2 =
                inputPhasor.MapScalars(scalar =>
                    scalar.ReplaceAll(@"\[Theta]", "Pi/2")
                );

            var v2 =
                (mv2 / mv2.ENorm()).FullSimplifyScalars();

            Console.WriteLine(
                TextComposer.GetMultivectorText(v2)
            );
            Console.WriteLine();

            var clarkeMap =
                //GeoEuclideanSimpleRotor<Expr>.Create(v1, v2, e1, e2);
                //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                GeometricProcessor.CreateClarkeRotationMap(n);

            var clarkeArray =
                clarkeMap
                    .GetVectorMapArray(n)
                    .MapScalars(scalar =>
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                    //Mfs.N[scalar].Simplify()
                    );

            Console.WriteLine("Generated Clarke Matrix:");
            Console.WriteLine(
                TextComposer.GetArrayText(clarkeArray)
            );
            Console.WriteLine();

            //var eigenPairsCount = MatrixProcessor.EigenDecomposition(
            //    MatrixProcessor.CreateMatrix(clarkeArray),
            //    out var realPairs,
            //    out var imagPairs
            //);

            //Console.WriteLine(@"Eigen Values\Vectors:");

            //for (var i = 0; i < eigenPairsCount; i++)
            //{
            //    Console.WriteLine($"Eigen Value {i+1}");
            //    Console.WriteLine(
            //        $"Real part: {TextComposer.GetScalarText(realPairs[i].Item1)}"
            //    );
            //    Console.WriteLine(
            //        $"Imag part: {TextComposer.GetScalarText(imagPairs[i].Item1)}"
            //    );
            //    Console.WriteLine();

            //    Console.WriteLine($"Eigen Vector {i+1}");
            //    Console.WriteLine(
            //        $"Real part: {TextComposer.GetArrayText(realPairs[i].Item2)}"
            //    );
            //    Console.WriteLine(
            //        $"Imag part: {TextComposer.GetArrayText(imagPairs[i].Item2)}"
            //    );
            //    Console.WriteLine();
            //}

            //var rotorsArray = new GeoEuclideanSimpleRotor<Expr>[n / 2];

            //for (var j = 0; j < rotorsArray.Length; j++)
            //{
            //    var i = 2 * j;

            //    rotorsArray[j] =
            //        ScalarProcessor.ComplexEigenPairToEuclideanSimpleRotor(
            //            realPairs[i].Item1,
            //            imagPairs[i].Item1,
            //            realPairs[i].Item2,
            //            imagPairs[i].Item2
            //        );

            //    Console.WriteLine($"Rotor {j+1}");
            //    Console.WriteLine(
            //        TextComposer.GetMultivectorText(rotorsArray[j].)
            //    );
            //    Console.WriteLine();
            //}

            //var rotorsSequence = 
            //    GeoEuclideanSimpleRotorsSequence<Expr>.Create(rotorsArray);

            //Console.WriteLine($"Final Rotor");
            //Console.WriteLine(
            //    TextComposer.GetMultivectorText(rotorsSequence.GetFinalRotor().)
            //);
            //Console.WriteLine();

            //Console.WriteLine($"Final Rotor Matrix");
            //Console.WriteLine(
            //    TextComposer.GetArrayText(rotorsSequence.GetFinalMatrix(n))
            //);
            //Console.WriteLine();

            //if (n == 5)
            //{
            //    var clarkeArray1 = 
            //        GeoPoTUtils
            //            .CreateClarkeArray5D(ScalarProcessor)
            //            .MapItems(scalar => 
            //                Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
            //            );

            //    Console.WriteLine("5D Clarke Matrix:");
            //    Console.WriteLine(
            //        LaTeXComposer.GetArrayDisplayEquationText(clarkeArray1)
            //    );
            //    Console.WriteLine();

            //    var clarkeArrayDiff =
            //        ScalarProcessor
            //            .Subtract(clarkeArray1, clarkeArray)
            //            .MapItems(scalar => 
            //                Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
            //            );

            //    Console.WriteLine("5D Clarke Matrix - Generated Clarke Matrix:");
            //    Console.WriteLine(
            //        TextComposer.GetArrayText(clarkeArrayDiff)
            //    );
            //    Console.WriteLine();
            //}

            //if (n == 6)
            //{
            //    var clarkeArray1 =
            //        GeoPoTUtils
            //            .CreateClarkeArray6D(ScalarProcessor)
            //            .MapItems(scalar =>
            //                Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
            //            );

            //    Console.WriteLine("6D Clarke Matrix:");
            //    Console.WriteLine(
            //        LaTeXComposer.GetArrayDisplayEquationText(clarkeArray1)
            //    );
            //    Console.WriteLine();

            //    var clarkeArrayDiff =
            //        ScalarProcessor
            //            .Subtract(clarkeArray1, clarkeArray)
            //            .MapItems(scalar =>
            //                Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
            //            );

            //    Console.WriteLine("6D Clarke Matrix - Generated Clarke Matrix:");
            //    Console.WriteLine(
            //        TextComposer.GetArrayText(clarkeArrayDiff)
            //    );
            //    Console.WriteLine();
            //}

            for (var k = 1; k <= 12; k++)
            {
                Console.WriteLine($"k = {k}:");
                Console.WriteLine();

                var (phasor1, phasor2) =
                    GetPhasorsTuple(k, n);

                var mappedPhasor =
                    clarkeMap
                        .OmMap(phasor2)
                        .SimplifyScalars();

                var phasorLength =
                    phasor2.ENorm().ScalarValue.FullSimplify();

                var phasorScalarsSum =
                    phasor2.ESp(unitKirchhoffVector).ScalarValue.FullSimplify();

                Console.WriteLine("Length of phasor:");
                Console.WriteLine(
                    TextComposer.GetScalarText(phasorLength)
                );
                Console.WriteLine();

                Console.WriteLine("Sum of phasor scalars:");
                Console.WriteLine(
                    TextComposer.GetScalarText(phasorScalarsSum)
                );
                Console.WriteLine();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(mappedPhasor)
                );
                Console.WriteLine();
            }
        }
    }
}