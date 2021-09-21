using System;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.Applications.GaPoT;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using GeometricAlgebraFulcrumLib.Mathematica.Text;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.Samples.GAPoT
{
    public static class ClarkeTransformationSymbolicSample
    {
        public static IGeometricAlgebraEuclideanProcessor<Expr> GeometricProcessor { get; }
            = MathematicaUtils.EuclideanProcessor;

        public static ScalarAlgebraMathematicaProcessor ScalarProcessor { get; }
            = ScalarAlgebraMathematicaProcessor.DefaultProcessor;
            
        public static TextMathematicaComposer TextComposer { get; }
            = TextMathematicaComposer.DefaultComposer;
            
        public static LaTeXMathematicaComposer LaTeXComposer { get; }
            = LaTeXMathematicaComposer.DefaultComposer;


        public static void Execute()
        {
            for (var n = 3; n <= 4; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var clarkeMap =
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    GeometricProcessor.CreateClarkeMap(n);

                var clarkeArray = 
                    clarkeMap
                        .GetVectorOmMappingMatrix(n, n)
                        .MapScalars(scalar => 
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );
                
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
                    linearMapQ
                        .GetVectorOmMappingMatrix(n, n)
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
                        .GetVectorOmMappingMatrix(n, n)
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