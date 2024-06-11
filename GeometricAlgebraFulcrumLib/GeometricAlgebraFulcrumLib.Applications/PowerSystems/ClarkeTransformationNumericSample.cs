using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Versors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Utilities.Text;

namespace GeometricAlgebraFulcrumLib.Applications.PowerSystems;

public static class ClarkeTransformationNumericSample
{
    public static XGaFloat64Processor GeometricProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    public static TextComposerFloat64 TextComposer { get; }
        = TextComposerFloat64.DefaultComposer;

    public static LaTeXComposerFloat64 LaTeXComposer { get; }
        = LaTeXComposerFloat64.DefaultComposer;


    public static void Execute()
    {
        for (var n = 3; n <= 6; n++)
        {
            Console.WriteLine($"n = {n}:");
            Console.WriteLine();

            var clarkeMap =
                //ScalarProcessor.CreateSimpleKirchhoffRotor(n);
                GeometricProcessor.CreateClarkeRotationMap(n);

            var clarkeArray =
                clarkeMap.GetVectorMapArray(n, n);

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
                linearMapQ.GetVectorMapArray(n, n);

            Console.WriteLine("Q Matrix:");
            Console.WriteLine(
                TextComposer.GetArrayText(linearMapQArray)
            );
            Console.WriteLine();

            var linearMapRArray =
                linearMapR.GetVectorMapArray(n, n);

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