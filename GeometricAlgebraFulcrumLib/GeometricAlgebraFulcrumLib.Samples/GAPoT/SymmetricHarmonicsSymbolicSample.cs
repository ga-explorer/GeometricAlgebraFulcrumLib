using System;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraFulcrumLib.Geometry.Subspaces;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class SymmetricHarmonicsSymbolicSample
    {
        public static IGeometricAlgebraProcessor<Expr> GeometricProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(63);
            
        public static MathematicaTextComposer TextComposer { get; }
            = MathematicaTextComposer.DefaultComposer;
            
        public static MathematicaLaTeXComposer LaTeXComposer { get; }
            = MathematicaLaTeXComposer.DefaultComposer;


        public static void ValidatePhasor(int k, int n, VectorStorage<Expr> phasor2)
        {
            var composer = GeometricProcessor.CreateVectorStorageComposer();

            for (var i = 0; i < n; i++)
            {
                var scalar = i == 0
                    ? $@"Cos[{k}*\[Theta]]".ToExpr()
                    : $@"Cos[{k}*(\[Theta] - 2*Pi*{i}/{n})]".ToExpr();

                composer.SetTerm((ulong) i, scalar);
            }

            var phasor1 = 
                composer
                    .CreateVectorStorage()
                    .MapVectorScalars(
                        scalar => Mfs.FullSimplify[scalar].Evaluate()
                    );

            var phasorDiff = 
                GeometricProcessor.GetNotZeroTerms(
                    GeometricProcessor.Subtract(phasor1, phasor2)
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

        public static Tuple<VectorStorage<Expr>, VectorStorage<Expr>> GetPhasorsTuple(int i, int k, int n)
        {
            Debug.Assert(n >= 2 && i >= 0 && i < n);

            var muStorage =
                GeometricProcessor.CreateVectorTermStorage(i, Expr.INT_ONE
                );

            var muSubspace = 
                GeoSubspace<Expr>.CreateDirect(GeometricProcessor, muStorage);

            //var muVector =
            //    GeoEuclideanVector<Expr>.Create(muStorage);

            var angle = i == 0
                ? $@"{k}*\[Theta]".ToExpr()
                : $@"{k}*(\[Theta] - 2*Pi*{i}/{n})".ToExpr();

            var rotor = GeometricProcessor.CreateEuclideanGivensRotor(
                i, n, angle
            );

            var phasor1 =
                rotor.OmMapVector(muStorage).FullSimplifyScalars().GetVectorPart();

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

            return new Tuple<VectorStorage<Expr>, VectorStorage<Expr>>(
                phasor1,
                phasor2
            );
        }

        public static Tuple<VectorStorage<Expr>, VectorStorage<Expr>> GetPhasorsTuple(int k, int n)
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
                        (IMultivectorStorage<Expr>) GeometricProcessor.CreateVectorZeroStorage(),
                        (current, vector) => GeometricProcessor.Add(current, vector)
                    )
                    .MapScalars(scalar => 
                        Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                    )
                    .GetVectorPart();

            var phasor2 =
                phasorTuples
                    .Select(t => t.Item2)
                    .Aggregate(
                        (IMultivectorStorage<Expr>) GeometricProcessor.CreateVectorZeroStorage(),
                        (current, vector) => GeometricProcessor.Add(current, vector)
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

            return new Tuple<VectorStorage<Expr>, VectorStorage<Expr>>(
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
                    GeometricProcessor.CreateStorageVectorUnitOnes(n);

                var (_, inputPhasor) = GetPhasorsTuple(1, n);

                var e1 = 
                    GeometricProcessor.CreateVectorBasisStorage(0);

                var e2 = 
                    GeometricProcessor.CreateVectorBasisStorage(1);

                var mv1 = 
                    inputPhasor.MapScalars(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "0")
                );

                var v1 = 
                    GeometricProcessor.Divide(mv1, GeometricProcessor.ENorm(mv1)).FullSimplifyScalars().GetVectorPart();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v1)
                );
                Console.WriteLine();

                var mv2 = 
                    inputPhasor.MapScalars(scalar => 
                        scalar.ReplaceAll(@"\[Theta]", "Pi/2")
                    );
                
                var v2 = 
                    GeometricProcessor.Divide(mv2, GeometricProcessor.ENorm(mv2)).FullSimplifyScalars();

                Console.WriteLine(
                    TextComposer.GetMultivectorText(v2)
                );
                Console.WriteLine();

                var clarkeMap =
                    //GeoEuclideanSimpleRotor<Expr>.Create(v1, v2, e1, e2);
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    GeometricProcessor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetVectorOmMappingMatrix(n, n)
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
                //        TextComposer.GetMultivectorText(rotorsArray[j].Storage)
                //    );
                //    Console.WriteLine();
                //}

                //var rotorsSequence = 
                //    GeoEuclideanSimpleRotorsSequence<Expr>.Create(rotorsArray);

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
                            .OmMapVector(phasor2)
                            .SimplifyScalars();

                    var phasorLength =
                        GeometricProcessor.ENorm(phasor2).FullSimplify();

                    var phasorScalarsSum =
                        GeometricProcessor.ESp(phasor2, unitKirchhoffVector).FullSimplify();

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
