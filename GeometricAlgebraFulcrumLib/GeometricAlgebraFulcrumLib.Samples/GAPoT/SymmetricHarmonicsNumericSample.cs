using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class SymmetricHarmonicsNumericSample
    {
        private static Random RandomGenerator { get; }
            = new Random(10);

        public static LinearAlgebraFloat64Processor MatrixProcessor { get; }
            = LinearAlgebraFloat64Processor.DefaultProcessor;
            
        public static IGeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(63);

        public static TextFloat64Composer TextComposer { get; }
            = TextFloat64Composer.DefaultComposer;
            
        public static LaTeXFloat64Composer LaTeXComposer { get; }
            = LaTeXFloat64Composer.DefaultComposer;


        public static void ValidatePhasor(int k, int n, Vector<double> phasor2, double theta)
        {
            var composer = GeometricProcessor.CreateVectorStorageComposer();

            for (var i = 0; i < n; i++)
            {
                var angle = i == 0
                    ? Math.Cos(k * theta)
                    : Math.Cos(k * theta - 2d * Math.PI * i / n);

                composer.SetTerm((ulong) i, angle);
            }

            var phasor1 = 
                composer.CreateVector();

            var phasorDiff = 
                GeometricProcessor.GetNotZeroTerms(
                    (phasor1 - phasor2).VectorStorage
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

        public static Tuple<Vector<double>, Vector<double>> GetComponentPhasorsTuple(int i, int k, int n, double theta)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                GeometricProcessor.CreateVectorTerm(i, 1);

            var muSubspace = muStorage.GetSubspace();

            //var muVector =
            //    GeoEuclideanVector<double>.Create(muStorage);

            var angle = i == 0
                ? Math.Cos(k * theta)
                : Math.Cos(k * theta - 2d * Math.PI * i / n);

            var rotor = GeometricProcessor.CreateGivensRotor(
                i, n, angle
            );

            var phasor1 =
                rotor.OmMap(muStorage);

            var phasor2 = 
                muSubspace.Project(phasor1);
            
            
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

            return new Tuple<Vector<double>, Vector<double>>(phasor1, phasor2);
        }

        public static Tuple<Vector<double>, Vector<double>> GetPhasorsTuple(int k, int n, double theta)
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
                        GeometricProcessor.CreateVectorZero(),
                        (current, vector) => current + vector
                    );

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        GeometricProcessor.CreateVectorZero(),
                        (current, vector) => current + vector
                    );

            ValidatePhasor(k, n, phasor2, theta);

            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor1)
            );
            Console.WriteLine();
                
            Console.WriteLine(
                TextComposer.GetMultivectorText(phasor2)
            );
            Console.WriteLine();

            return new Tuple<Vector<double>, Vector<double>>(
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
                    GeometricProcessor.CreateVectorSymmetricUnit(n);

                var theta = 
                    RandomGenerator.NextDouble() * 2d * Math.PI;

                var (_, inputPhasor) = GetPhasorsTuple(1, n, theta);

                var e1 = 
                    GeometricProcessor.CreateVectorBasis(0);

                var e2 = 
                    GeometricProcessor.CreateVectorBasis(1);

                var (_, mv1) = GetPhasorsTuple(1, n, 0);

                var v1 = mv1 / mv1.ENorm();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v1)
                );
                Console.WriteLine();

                var (_, mv2) = GetPhasorsTuple(1, n, 0.5d * Math.PI);
                
                var v2 = mv2 / mv2.ENorm();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v2)
                );
                Console.WriteLine();

                var clarkeMap =
                    //GeoEuclideanSimpleRotor<double>.Create(v1, v2, e1, e2);
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    GeometricProcessor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap.GetVectorOmMappingMatrix(n, n);
                
                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var eigenPairsCount = MatrixProcessor.MatrixEigenDecomposition(
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

                var rotorsArray = new PureRotor<double>[n / 2];

                for (var j = 0; j < rotorsArray.Length; j++)
                {
                    var i = j; //2 * j;

                    rotorsArray[j] =
                        GeometricProcessor.ComplexEigenPairToPureRotor(
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
                    PureRotorsSequence<double>.Create(GeometricProcessor, rotorsArray);
                
                Console.WriteLine($"Final Rotor");
                Console.WriteLine(
                    TextComposer.GetMultivectorText(rotorsSequence.GetFinalRotor().Multivector)
                );
                Console.WriteLine();
                
                Console.WriteLine($"Final Rotor Matrix");
                Console.WriteLine(
                    TextComposer.GetArrayText(rotorsSequence.GetFinalRotorArray(n))
                );
                Console.WriteLine();

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