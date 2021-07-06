using System;
using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Processing.Implementations.Float64;
using GeometricAlgebraLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraLib.Text;

namespace GeometricAlgebraLib.Samples.GAPoT
{
    public static class ClarkeTransformationNumericSample
    {
        public static GaScalarProcessorFloat64 ScalarProcessor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor;
            
        public static GaTextComposerFloat64 TextComposer { get; }
            = GaTextComposerFloat64.DefaultComposer;
            
        public static GaLaTeXComposerFloat64 LaTeXComposer { get; }
            = GaLaTeXComposerFloat64.DefaultComposer;


        public static void Execute()
        {
            for (var n = 3; n <= 6; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var clarkeMap =
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    ScalarProcessor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetArray(n, n);
                
                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var (linearMapQ, linearMapR) = 
                    clarkeMap.GetHouseholderQRDecomposition(n);

                Console.WriteLine("Q Map Vectors:");
                for (var i = 0; i < linearMapQ.UnitVectorStorages.Count; i++)
                {
                    var vector = linearMapQ.UnitVectorStorages[i];

                    Console.WriteLine(
                        TextComposer.GetMultivectorText(vector)
                    );
                    Console.WriteLine();
                }

                var linearMapQArray = 
                    linearMapQ
                        .GetArray(n, n);
                
                Console.WriteLine("Q Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapQArray)
                );
                Console.WriteLine();

                var linearMapRArray = 
                    linearMapR
                        .GetArray(n, n);
                
                Console.WriteLine("R Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapRArray)
                );
                Console.WriteLine();

                var rotorsSequence = linearMapQ.CreateSimpleRotorsSequence();

                Console.WriteLine("Q Map Rotors:");
                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotor = rotorsSequence[i].Storage;

                    Console.WriteLine(
                        TextComposer.GetMultivectorText(rotor)
                    );
                    Console.WriteLine();
                }
            }
        }
    }
}