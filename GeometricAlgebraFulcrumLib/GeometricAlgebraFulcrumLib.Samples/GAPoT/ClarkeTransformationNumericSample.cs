using System;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Versors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Generic;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.TextComposers.Float64;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class ClarkeTransformationNumericSample
    {
        public static GaProcessorGenericEuclidean<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(63);

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
                    Processor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap.GetVectorsMappingArray(n, n);
                
                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var (linearMapQ, linearMapR) = 
                    Processor.GetHouseholderQRDecomposition(clarkeMap, n);

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
                    linearMapQ.GetVectorsMappingArray(n, n);
                
                Console.WriteLine("Q Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapQArray)
                );
                Console.WriteLine();

                var linearMapRArray = 
                    linearMapR.GetVectorsMappingArray(n, n);
                
                Console.WriteLine("R Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapRArray)
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