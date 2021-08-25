using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;
using GeometricAlgebraFulcrumLib.TextComposers;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class SymmetricHarmonicsSymbolicSample
    {
        public static IGaProcessor<Expr> Processor { get; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor.CreateEuclideanProcessor(63);
            
        public static GaTextComposerMathematicaExpr TextComposer { get; }
            = GaTextComposerMathematicaExpr.DefaultComposer;
            
        public static GaLaTeXComposerMathematicaExpr LaTeXComposer { get; }
            = GaLaTeXComposerMathematicaExpr.DefaultComposer;


        public static void ValidatePhasor(int k, int n, IGaStorageVector<Expr> phasor2)
        {
            var composer = Processor.CreateStorageKVectorComposer();

            for (var i = 0; i < n; i++)
            {
                var scalar = i == 0
                    ? $@"Cos[{k}*\[Theta]]".ToExpr()
                    : $@"Cos[{k}*(\[Theta] - 2*Pi*{i}/{n})]".ToExpr();

                composer.SetTerm((ulong) i, scalar);
            }

            var phasor1 = 
                composer
                    .CreateStorageVector()
                    .MapVectorScalars(
                        scalar => Mfs.FullSimplify[scalar].Evaluate()
                    );

            var phasorDiff = 
                Processor.GetNotZeroTerms(
                    Processor.Subtract(phasor1, phasor2)
                    .MapScalars(
                        scalar => Mfs.FullSimplify[scalar].Evaluate()
                    )
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

        public static Tuple<IGaStorageVector<Expr>, IGaStorageVector<Expr>> GetPhasorsTuple(int i, int k, int n)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                Processor.CreateStorageVector(i, Expr.INT_ONE
                );

            var muSubspace = 
                GaSubspace<Expr>.Create(Processor, muStorage);

            //var muVector =
            //    GaEuclideanVector<Expr>.Create(muStorage);

            var angle = i == 0
                ? $@"{k}*\[Theta]".ToExpr()
                : $@"{k}*(\[Theta] - 2*Pi*{i}/{n})".ToExpr();

            var rotor = Processor.CreateEuclideanGivensRotor(
                i, n, angle
            );

            var phasor1 =
                rotor.MapVector(muStorage).FullSimplifyScalars().GetVectorPart();

            var phasor2 = 
                muSubspace.Project(phasor1).FullSimplifyScalars().GetVectorPart();
            
            
            //Console.WriteLine(
            //    LaTeXComposer.GetMultivectorText(rotor.Storage)
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

            return new Tuple<IGaStorageVector<Expr>, IGaStorageVector<Expr>>(
                phasor1,
                phasor2
            );
        }

        public static Tuple<IGaStorageVector<Expr>, IGaStorageVector<Expr>> GetPhasorsTuple(int k, int n)
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
                        (IGaStorageMultivector<Expr>) Processor.CreateStorageZeroVector(),
                        (current, vector) => Processor.Add(current, vector)
                    )
                    .MapScalars(scalar => 
                        Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                    )
                    .GetVectorPart();

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        (IGaStorageMultivector<Expr>) Processor.CreateStorageZeroVector(),
                        (current, vector) => Processor.Add(current, vector)
                    )
                    .MapScalars(scalar => 
                        Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                    )
                    .GetVectorPart();

            ValidatePhasor(k, n, phasor2);

            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor1)
            );
            Console.WriteLine();
                
            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor2)
            );
            Console.WriteLine();

            return new Tuple<IGaStorageVector<Expr>, IGaStorageVector<Expr>>(
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
                    Processor.CreateStorageUnitOnesVector(n);

                var (_, inputPhasor) = GetPhasorsTuple(1, n);

                var e1 = 
                    Processor.CreateStorageBasisVector(0);

                var e2 = 
                    Processor.CreateStorageBasisVector(1);

                var mv1 = 
                    inputPhasor.MapScalars(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "0")
                );

                var v1 = 
                    Processor.Divide(mv1, Processor.ENorm(mv1)).FullSimplifyScalars().GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v1)
                );
                Console.WriteLine();

                var mv2 = 
                    inputPhasor.MapScalars(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "Pi/2")
                    );
                
                var v2 = 
                    Processor.Divide(mv2, Processor.ENorm(mv2)).FullSimplifyScalars();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v2)
                );
                Console.WriteLine();

                var clarkeMap =
                    //GaEuclideanSimpleRotor<Expr>.Create(v1, v2, e1, e2);
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    Processor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetVectorsMappingArray(n, n)
                        .MapValues(scalar => 
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

                //var rotorsArray = new GaEuclideanSimpleRotor<Expr>[n / 2];

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
                //        TextComposer.GetMultivectorText(rotorsArray[j].Storage)
                //    );
                //    Console.WriteLine();
                //}

                //var rotorsSequence = 
                //    GaEuclideanSimpleRotorsSequence<Expr>.Create(rotorsArray);

                //Console.WriteLine($"Final Rotor");
                //Console.WriteLine(
                //    TextComposer.GetMultivectorText(rotorsSequence.GetFinalRotor().Storage)
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
                //        GaPoTUtils
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
                //        GaPoTUtils
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
                            .MapVector(phasor2)
                            .SimplifyScalars();

                    var phasorLength =
                        Processor.ENorm(phasor2).FullSimplify();

                    var phasorScalarsSum =
                        Processor.ESp(phasor2, unitKirchhoffVector).FullSimplify();

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
}
