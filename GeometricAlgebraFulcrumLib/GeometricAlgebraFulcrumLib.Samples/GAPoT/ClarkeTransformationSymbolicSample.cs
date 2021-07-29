using System;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Symbolic;
using GeometricAlgebraFulcrumLib.Symbolic.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica;
using GeometricAlgebraFulcrumLib.Symbolic.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Symbolic.Processors;
using GeometricAlgebraFulcrumLib.Symbolic.Text;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class ClarkeTransformationSymbolicSample
    {
        public static IGaProcessorEuclidean<Expr> Processor { get; }
            = GaSymbolicUtils.EuclideanProcessor;

        public static GaScalarProcessorMathematicaExpr ScalarProcessor { get; }
            = GaScalarProcessorMathematicaExpr.DefaultProcessor;
            
        public static GaTextComposerMathematicaExpr TextComposer { get; }
            = GaTextComposerMathematicaExpr.DefaultComposer;
            
        public static GaLaTeXComposerMathematicaExpr LaTeXComposer { get; }
            = GaLaTeXComposerMathematicaExpr.DefaultComposer;


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
                        .MapItems(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
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
                    linearMapQ
                        .GetVectorsMappingArray(n, n)
                        .MapItems(scalar => 
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
                        .MapItems(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
                Console.WriteLine("R Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(linearMapRArray)
                );
                Console.WriteLine();

                var rotorsSequence = linearMapQ.CreateSimpleRotorsSequence();

                Console.WriteLine("Q Map Rotors:");
                for (var i = 0; i < rotorsSequence.Count; i++)
                {
                    var rotor = rotorsSequence[i].Rotor;

                    Console.WriteLine(
                        TextComposer.GetMultivectorText(rotor)
                    );
                    Console.WriteLine();
                }
            }
        }
    }
}