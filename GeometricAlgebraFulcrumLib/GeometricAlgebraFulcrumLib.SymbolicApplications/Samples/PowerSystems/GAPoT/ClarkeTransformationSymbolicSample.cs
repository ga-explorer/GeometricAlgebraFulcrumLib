using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Generic.Matrices;
using GeometricAlgebraFulcrumLib.Mathematica;
using GeometricAlgebraFulcrumLib.Mathematica.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Mathematica.Mathematica.ExprFactory;
using GeometricAlgebraFulcrumLib.Mathematica.Processors;
using Wolfram.NETLink;

namespace GeometricAlgebraFulcrumLib.SymbolicApplications.Samples.PowerSystems.GAPoT
{
    public static class ClarkeTransformationSymbolicSample
    {
        public static XGaProcessor<Expr> GeometricProcessor { get; }
            = XGaMathematicaUtils.EuclideanProcessor;

        public static ScalarProcessorOfWolframExpr ScalarProcessor { get; }
            = ScalarProcessorOfWolframExpr.DefaultProcessor;

        public static TextComposerExpr TextComposer { get; }
            = TextComposerExpr.DefaultComposer;

        public static LaTeXComposerExpr LaTeXComposer { get; }
            = LaTeXComposerExpr.DefaultComposer;


        public static void Execute()
        {
            for (var n = 3; n <= 4; n++)
            {
                Console.WriteLine($"n = {n}:");
                Console.WriteLine();

                var clarkeMap =
                    //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                    GeometricProcessor.CreateClarkeRotationMap(n);

                var clarkeArray =
                    clarkeMap
                        .GetVectorMapArray(n, n)
                        .MapScalars(scalar =>
                            Mfs.TrigReduce[Mfs.FullSimplify[scalar]].Evaluate()
                        );

                Console.WriteLine("Generated Clarke Matrix:");
                Console.WriteLine(
                    TextComposer.GetArrayText(clarkeArray)
                );
                Console.WriteLine();

                var (linearMapQ, linearMapR) =
                    clarkeMap.GetHouseholderQRDecomposition(n);

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
                        .GetVectorMapArray(n, n)
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
                        .GetVectorMapArray(n, n)
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