using System;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Signatures;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class SymmetricHarmonicsSymbolicSample
    {
        public static GaMatrixProcessorMathematicaExpr MatrixProcessor { get; }
            = GaMatrixProcessorMathematicaExpr.DefaultProcessor;

        public static GaScalarProcessorMathematicaExpr ScalarProcessor { get; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor;
            
        public static GaTextComposerMathematicaExpr TextComposer { get; }
            = GaTextComposerMathematicaExpr.DefaultComposer;
            
        public static GaLaTeXComposerMathematicaExpr LaTeXComposer { get; }
            = GaLaTeXComposerMathematicaExpr.DefaultComposer;


        public static void ValidatePhasor(int k, int n, IGaVectorStorage<Expr> phasor2)
        {
            var composer = new GaKVectorStorageComposer<Expr>(ScalarProcessor, 1);

            for (var i = 0; i < n; i++)
            {
                var scalar = i == 0
                    ? $@"Cos[{k}*\[Theta]]".ToExpr()
                    : $@"Cos[{k}*(\[Theta] - 2*Pi*{i}/{n})]".ToExpr();

                composer.SetTerm((ulong) i, scalar);
            }

            var phasor1 = 
                composer
                    .GetVectorStorage()
                    .GetStorageCopy(
                        scalar => Mfs.FullSimplify[scalar].Evaluate()
                    )
                    .GetVectorPart();

            var phasorDiff = 
                phasor1
                    .Subtract(phasor2)
                    .GetStorageCopy(
                        scalar => Mfs.FullSimplify[scalar].Evaluate()
                    )
                    .GetNotZeroTerms();

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

        public static Tuple<IGaVectorStorage<Expr>, IGaVectorStorage<Expr>> GetPhasorsTuple(int i, int k, int n)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                GaVectorTermStorage<Expr>.Create(
                    ScalarProcessor, i, Expr.INT_ONE
                );

            var muSubspace = 
                GaEuclideanSubspace<Expr>.Create(muStorage);

            //var muVector =
            //    GaEuclideanVector<Expr>.Create(muStorage);

            var angle = i == 0
                ? $@"{k}*\[Theta]".ToExpr()
                : $@"{k}*(\[Theta] - 2*Pi*{i}/{n})".ToExpr();

            var rotor = GaEuclideanSimpleRotor<Expr>.CreateGivensRotor(
                ScalarProcessor, i, n, angle
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

            return new Tuple<IGaVectorStorage<Expr>, IGaVectorStorage<Expr>>(
                phasor1,
                phasor2
            );
        }

        public static Tuple<IGaVectorStorage<Expr>, IGaVectorStorage<Expr>> GetPhasorsTuple(int k, int n)
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
                        (IGaMultivectorStorage<Expr>) GaVectorTermStorage<Expr>.CreateZero(ScalarProcessor),
                        (current, vector) => current.Add(vector)
                    )
                    .GetStorageCopy(scalar => 
                        Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                    )
                    .GetVectorPart();

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        (IGaMultivectorStorage<Expr>) GaVectorTermStorage<Expr>.CreateZero(ScalarProcessor),
                        (current, vector) => current.Add(vector)
                    )
                    .GetStorageCopy(scalar => 
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

            return new Tuple<IGaVectorStorage<Expr>, IGaVectorStorage<Expr>>(
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
                    GaVectorStorage<Expr>.CreateUnitOnesVector(ScalarProcessor, n);

                var (_, inputPhasor) = GetPhasorsTuple(1, n);

                var e1 = 
                    GaVectorTermStorage<Expr>.CreateBasisVector(ScalarProcessor, 0);

                var e2 = 
                    GaVectorTermStorage<Expr>.CreateBasisVector(ScalarProcessor, 1);

                var mv1 = 
                    inputPhasor.GetStorageCopy(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "0")
                ).GetVectorPart();

                var v1 = 
                    mv1.Divide(mv1.ENorm()).FullSimplifyScalars().GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v1)
                );
                Console.WriteLine();

                var mv2 = 
                    inputPhasor.GetStorageCopy(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "Pi/2")
                    );
                
                var v2 = 
                    mv2.Divide(mv2.ENorm()).FullSimplifyScalars().GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v2)
                );
                Console.WriteLine();

                var clarkeMap =
                    //GaEuclideanSimpleRotor<Expr>.Create(v1, v2, e1, e2);
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    ScalarProcessor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetArray(n, n)
                        .MapItems(scalar => 
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
                        phasor2.ENorm().FullSimplify();

                    var phasorScalarsSum =
                        phasor2.ESp(unitKirchhoffVector).FullSimplify();

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
