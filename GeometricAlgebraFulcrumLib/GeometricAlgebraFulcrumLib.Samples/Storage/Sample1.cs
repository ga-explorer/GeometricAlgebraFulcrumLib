using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Multivectors.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample1
    {
        public static void Execute()
        {
            var vSpaceDimensions = 5;
            var scalarProcessor = ScalarProcessorOfFloat64.DefaultProcessor;
            var processor = scalarProcessor.CreateEuclideanRGaProcessor();
            var textComposer = TextComposerFloat64.DefaultComposer;

            var randomGenerator = new Random(10);

            var vectorStorage1 = processor.CreateVector(
                Enumerable
                    .Range(0, vSpaceDimensions)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            var vectorStorage2 = processor.CreateVector(
                Enumerable
                    .Range(0, vSpaceDimensions - 2)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            Console.WriteLine($"vSpaceDimension1: {vectorStorage1.VSpaceDimensions}");
            Console.WriteLine($"vSpaceDimension2: {vectorStorage2.VSpaceDimensions}");

            var gbtStack =
                RGaGbtProductsStack2<double>.Create(
                    vectorStorage1,
                    vectorStorage2
                );

            var multivector =
                processor.CreateMultivector(
                    gbtStack.GetEGpIdScalarRecords()
                );

            Console.WriteLine(textComposer.GetTermsText(multivector));
        }
    }
}