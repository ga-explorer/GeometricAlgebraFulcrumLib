using System;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class ClarkeTransformationSymbolicSample
    {
        public static IGaProcessorEuclidean<Expr> Processor { get; }
            = GaSymbolicUtils.EuclideanProcessor;

        public static MathematicaScalarProcessor ScalarProcessor { get; }
            = MathematicaScalarProcessor.DefaultProcessor;
            
        public static MathematicaTextComposer TextComposer { get; }
            = MathematicaTextComposer.DefaultComposer;
            
        public static MathematicaLaTeXComposer LaTeXComposer { get; }
            = MathematicaLaTeXComposer.DefaultComposer;


        public static void Execute()
        {
            for (var n = 3; n <= 4; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var clarkeMap =
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    Processor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetVectorsMappingArray(n, n)
                        .MapScalars(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray.ToArray())
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
                    linearMapQ
                        .GetVectorsMappingArray(n, n)
                        .MapScalars(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
                Console.WriteLine("Q Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapQArray)
                );
                Console.WriteLine();

                var linearMapRArray = 
                    linearMapR
                        .GetVectorsMappingArray(n, n)
                        .MapScalars(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
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