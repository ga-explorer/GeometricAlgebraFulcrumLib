using System;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Versors;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.PowerSystems.GAPoT
{
    public static class ClarkeTransformationNumericSample
    {
        public static GeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(63);

        public static TextFloat64Composer TextComposer { get; }
            = TextFloat64Composer.DefaultComposer;

        public static LaTeXFloat64Composer LaTeXComposer { get; }
            = LaTeXFloat64Composer.DefaultComposer;


        public static void Execute()
        {
            for (var n = 3; n <= 6; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var clarkeMap =
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    GeometricProcessor.CreateClarkeMap(n);

                var clarkeArray =
                    clarkeMap.GetVectorOmMappingMatrix(n, n);

                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray.ToArray())
                );
                Console.WriteLine();

                var (linearMapQ, linearMapR) =
                    GeometricProcessor.GetHouseholderQRDecomposition(clarkeMap, n);

                Console.WriteLine("Q Map Vectors:");
                foreach (var versor in linearMapQ)
                {
                    var vector = versor.Vector;

                    Console.WriteLine(
                        TextComposer.GetMultivectorText(vector)
                    );
                    Console.WriteLine();
                }

                var linearMapQArray =
                    linearMapQ.GetVectorOmMappingMatrix(n, n);

                Console.WriteLine("Q Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapQArray.ToArray())
                );
                Console.WriteLine();

                var linearMapRArray =
                    linearMapR.GetVectorOmMappingMatrix(n, n);

                Console.WriteLine("R Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapRArray.ToArray())
                );
                Console.WriteLine();

                var rotorsSequence = linearMapQ.CreatePureRotorsSequence();

                Console.WriteLine("Q Map Rotors:");
                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotor = rotorsSequence[i].Multivector;

                    Console.WriteLine(
                        TextComposer.GetMultivectorText(rotor)
                    );
                    Console.WriteLine();
                }
            }
        }
    }
}