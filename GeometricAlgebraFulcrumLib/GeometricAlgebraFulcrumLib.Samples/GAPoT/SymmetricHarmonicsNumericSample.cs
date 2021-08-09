using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Utils;
using GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.TextComposers.Float64;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class SymmetricHarmonicsNumericSample
    {
        private static Random RandomGenerator { get; }
            = new Random(10);

        public static GaMatrixProcessorFloat64 MatrixProcessor { get; }
            = GaMatrixProcessorFloat64.DefaultProcessor;

        public static GaScalarProcessorFloat64 ScalarProcessor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor;
            
        public static IGaProcessor<double> Processor { get; }
            = ScalarProcessor.CreateEuclideanProcessor(63);

        public static GaTextComposerFloat64 TextComposer { get; }
            = GaTextComposerFloat64.DefaultComposer;
            
        public static GaLaTeXComposerFloat64 LaTeXComposer { get; }
            = GaLaTeXComposerFloat64.DefaultComposer;


        public static void ValidatePhasor(int k, int n, IGaStorageVector<double> phasor2, double theta)
        {
            var composer = ScalarProcessor.CreateStorageComposerVector();

            for (var i = 0; i < n; i++)
            {
                var angle = i == 0
                    ? Math.Cos(k * theta)
                    : Math.Cos(k * theta - 2d * Math.PI * i / n);

                composer.SetTerm((ulong) i, angle);
            }

            var phasor1 = 
                composer.GetVector();

            var phasorDiff = 
                ScalarProcessor.GetNotZeroTerms(
                    ScalarProcessor.Subtract(phasor1, phasor2)
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

        public static Tuple<IGaStorageVector<double>, IGaStorageVector<double>> GetComponentPhasorsTuple(int i, int k, int n, double theta)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                ScalarProcessor.CreateStorageVector(i, 1
                );

            var muSubspace = 
                GaSubspace<double>.Create(Processor, muStorage);

            //var muVector =
            //    GaEuclideanVector<double>.Create(muStorage);

            var angle = i == 0
                ? Math.Cos(k * theta)
                : Math.Cos(k * theta - 2d * Math.PI * i / n);

            var rotor = Processor.CreateEuclideanGivensRotor(
                i, n, angle
            );

            var phasor1 =
                rotor.MapVector(muStorage).GetVectorPart();

            var phasor2 = 
                muSubspace.Project(phasor1).GetVectorPart();
            
            
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

            return new Tuple<IGaStorageVector<double>, IGaStorageVector<double>>(
                phasor1,
                phasor2
            );
        }

        public static Tuple<IGaStorageVector<double>, IGaStorageVector<double>> GetPhasorsTuple(int k, int n, double theta)
        {
            var phasorTuples =
                Enumerable
                    .Range(0, n)
                    .Select(i => GetComponentPhasorsTuple(i, k, n, theta))
                    .ToArray();

            var phasor1 =
                phasorTuples
                    .Select(t => t.Item1)
                    .Aggregate(
                        (IGaStorageMultivector<double>) ScalarProcessor.CreateStorageZeroVector(),
                        (current, vector) => ScalarProcessor.Add(current, vector)
                    )
                    .GetVectorPart();

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        (IGaStorageMultivector<double>) ScalarProcessor.CreateStorageZeroVector(),
                        (current, vector) => ScalarProcessor.Add(current, vector)
                    )
                    .GetVectorPart();

            ValidatePhasor(k, n, phasor2, theta);

            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor1)
            );
            Console.WriteLine();
                
            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor2)
            );
            Console.WriteLine();

            return new Tuple<IGaStorageVector<double>, IGaStorageVector<double>>(
                phasor1,
                phasor2
            );
        }

        public static void Execute()
        {
            for (var n = 5; n <= 6; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var unitKirchhoffVector = 
                    ScalarProcessor.CreateStorageUnitOnesVector(n);

                var theta = 
                    RandomGenerator.NextDouble() * 2d * Math.PI;

                var (_, inputPhasor) = GetPhasorsTuple(1, n, theta);

                var e1 = 
                    ScalarProcessor.CreateStorageBasisVector(0);

                var e2 = 
                    ScalarProcessor.CreateStorageBasisVector(1);

                var (_, mv1) = GetPhasorsTuple(1, n, 0);

                var v1 = 
                    ScalarProcessor.Divide(mv1, ScalarProcessor.ENorm(mv1)).GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v1)
                );
                Console.WriteLine();

                var (_, mv2) = GetPhasorsTuple(1, n, 0.5d * Math.PI);
                
                var v2 = 
                    ScalarProcessor.Divide(mv2, ScalarProcessor.ENorm(mv2)).GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v2)
                );
                Console.WriteLine();

                var clarkeMap =
                    //GaEuclideanSimpleRotor<double>.Create(v1, v2, e1, e2);
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    Processor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap.GetVectorsMappingArray(n, n);
                
                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var eigenPairsCount = MatrixProcessor.EigenDecomposition(
                    MatrixProcessor.CreateMatrix(clarkeArray),
                    out var realPairs,
                    out var imagPairs
                );

                Console.WriteLine(@"Eigen Values\Vectors:");

                for (var i = 0; i < eigenPairsCount; i++)
                {
                    Console.WriteLine($"Eigen Value {i+1}");
                    Console.WriteLine(
                        $"Real part: {TextComposer.GetScalarText(realPairs[i].Item1)}"
                    );
                    Console.WriteLine(
                        $"Imag part: {TextComposer.GetScalarText(imagPairs[i].Item1)}"
                    );
                    Console.WriteLine();

                    Console.WriteLine($"Eigen Vector {i+1}");
                    Console.WriteLine(
                        $"Real part: {TextComposer.GetArrayText(realPairs[i].Item2)}"
                    );
                    Console.WriteLine(
                        $"Imag part: {TextComposer.GetArrayText(imagPairs[i].Item2)}"
                    );
                    Console.WriteLine();
                }

                var rotorsArray = new GaPureRotor<double>[n / 2];

                for (var j = 0; j < rotorsArray.Length; j++)
                {
                    var i = j; //2 * j;

                    rotorsArray[j] =
                        Processor.ComplexEigenPairToEuclideanSimpleRotor(
                            realPairs[i].Item1,
                            imagPairs[i].Item1,
                            realPairs[i].Item2,
                            imagPairs[i].Item2
                        );

                    Console.WriteLine($"Rotor {j+1}");
                    Console.WriteLine(
                        TextComposer.GetMultivectorText(rotorsArray[j].Multivector)
                    );
                    Console.WriteLine();
                }

                var rotorsSequence = 
                    GaPureRotorsSequence<double>.Create(Processor, rotorsArray);
                
                Console.WriteLine($"Final Rotor");
                Console.WriteLine(
                    TextComposer.GetMultivectorText(rotorsSequence.GetFinalRotor().Multivector)
                );
                Console.WriteLine();
                
                Console.WriteLine($"Final Rotor Matrix");
                Console.WriteLine(
                    TextComposer.GetArrayText(rotorsSequence.GetFinalMatrix(n))
                );
                Console.WriteLine();

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

                //for (var k = 1; k <= n; k++)
                //{
                //    Console.WriteLine($"k = {k}:");
                //    Console.WriteLine();

                //    var (phasor1, phasor2) = 
                //        GetPhasorsTuple(k, n);

                //    var mappedPhasor = 
                //        clarkeMap
                //            .Map(phasor2)
                //            .SimplifyScalars();

                //    var phasorLength = 
                //        phasor2.ENorm().FullSimplify();

                //    var phasorScalarsSum = 
                //        phasor2.ESp(unitKirchhoffVector).FullSimplify();

                //    Console.WriteLine("Length of phasor:");
                //    Console.WriteLine(
                //        TextComposer.GetScalarText(phasorLength)
                //    );
                //    Console.WriteLine();

                //    Console.WriteLine("Sum of phasor scalars:");
                //    Console.WriteLine(
                //        TextComposer.GetScalarText(phasorScalarsSum)
                //    );
                //    Console.WriteLine();

                //    Console.WriteLine(
                //        TextComposer.GetMultivectorText(mappedPhasor)
                //    );
                //    Console.WriteLine();
                //}
            }
        }
    }
}