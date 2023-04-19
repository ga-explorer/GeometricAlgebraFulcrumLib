using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Matrices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Subspaces;
using GeometricAlgebraFulcrumLib.MathBase.Text;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GAPoT
{
    public static class SymmetricHarmonicsNumericSample
    {
        private static Random RandomGenerator { get; }
            = new Random(10);

        public static MatrixAlgebraFloat64Processor MatrixProcessor { get; }
            = MatrixAlgebraFloat64Processor.DefaultProcessor;

        public static XGaFloat64Processor GeometricProcessor { get; }
            = XGaFloat64Processor.Euclidean;

        public static TextComposerFloat64 TextComposer { get; }
            = TextComposerFloat64.DefaultComposer;

        public static LaTeXComposerFloat64 LaTeXComposer { get; }
            = LaTeXComposerFloat64.DefaultComposer;


        public static void ValidatePhasor(int k, int n, XGaFloat64Vector phasor2, double theta)
        {
            var composer = GeometricProcessor.CreateComposer();

            for (var i = 0; i < n; i++)
            {
                var angle = i == 0
                    ? Math.Cos(k * theta)
                    : Math.Cos(k * theta - 2d * Math.PI * i / n);

                composer.SetTerm((ulong)i, angle);
            }

            var phasor1 =
                composer.GetVector();

            var phasorDiff =
                (phasor1 - phasor2);

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

        public static Tuple<XGaFloat64Vector, XGaFloat64Vector> GetComponentPhasorsTuple(int i, int k, int n, double theta)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                GeometricProcessor.CreateVector(i, 1);

            var muSubspace = muStorage.ToSubspace();

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

            return new Tuple<XGaFloat64Vector, XGaFloat64Vector>(phasor1, phasor2);
        }

        public static Tuple<XGaFloat64Vector, XGaFloat64Vector> GetPhasorsTuple(int k, int n, double theta)
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
                        GeometricProcessor.CreateZeroVector(),
                        (current, vector) => current + vector
                    );

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        GeometricProcessor.CreateZeroVector(),
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

            return new Tuple<XGaFloat64Vector, XGaFloat64Vector>(
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
                    GeometricProcessor.CreateSymmetricUnitVector(n);

                var theta =
                    RandomGenerator.NextDouble() * 2d * Math.PI;

                var (_, inputPhasor) = GetPhasorsTuple(1, n, theta);

                var e1 =
                    GeometricProcessor.CreateVector(0);

                var e2 =
                    GeometricProcessor.CreateVector(1);

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
                    GeometricProcessor.CreateClarkeRotationMap(n);

                var clarkeArray =
                    clarkeMap.GetVectorMapArray(n);

                var clarkeMatrix = clarkeArray.ToMatrix();

                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var eigenPairsCount = clarkeMatrix.MatrixEigenDecomposition(
                    out var realPairs,
                    out var imagPairs
                );

                Console.WriteLine(@"Eigen Values\Vectors:");

                for (var i = 0; i < eigenPairsCount; i++)
                {
                    Console.WriteLine($"Eigen Value {i + 1}");
                    Console.WriteLine(
                        $"Real part: {TextComposer.GetScalarText(realPairs[i].Item1)}"
                    );
                    Console.WriteLine(
                        $"Imag part: {TextComposer.GetScalarText(imagPairs[i].Item1)}"
                    );
                    Console.WriteLine();

                    Console.WriteLine($"Eigen Vector {i + 1}");
                    Console.WriteLine(
                        $"Real part: {TextComposer.GetArrayText(realPairs[i].Item2)}"
                    );
                    Console.WriteLine(
                        $"Imag part: {TextComposer.GetArrayText(imagPairs[i].Item2)}"
                    );
                    Console.WriteLine();
                }

                var rotorsArray = new XGaFloat64PureRotor[n / 2];

                var eigenValueList = new List<System.Numerics.Complex>(n / 2);
                for (var j = 0; j < rotorsArray.Length; j++)
                {
                    // Ignore identity rotations
                    if ((realPairs[j].Item1 - 1d).IsNearZero() && imagPairs[j].Item1.IsNearZero())
                        continue;

                    // Ignore complex conjugate eigen values (only take first one of the pair)
                    if (eigenValueList.FindIndex(c => c.IsNearConjugateTo(realPairs[j].Item1, imagPairs[j].Item1)) >= 0)
                        continue;

                    eigenValueList.Add(
                        new System.Numerics.Complex(realPairs[j].Item1, imagPairs[j].Item1)
                    );

                    rotorsArray[j] =
                        GeometricProcessor.ComplexEigenPairToPureRotor(
                            realPairs[j].Item1,
                            imagPairs[j].Item1,
                            realPairs[j].Item2.CreateXGaVector(GeometricProcessor),
                            imagPairs[j].Item2.CreateXGaVector(GeometricProcessor)
                        );

                    Console.WriteLine($"Rotor {j + 1}");
                    Console.WriteLine(
                        TextComposer.GetMultivectorText(rotorsArray[j].Multivector)
                    );
                    Console.WriteLine();
                }

                var rotorsSequence =
                    XGaFloat64PureRotorsSequence.Create(rotorsArray);

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