using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.TextComposers.Float64;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample1
    {
        public static void Execute()
        {
            var vSpaceDimensions = 5;
            var scalarProcessor = GaScalarProcessorFloat64.DefaultProcessor;
            var textComposer = GaTextComposerFloat64.DefaultComposer;

            var randomGenerator = new Random(10);

            var vectorStorage1 = scalarProcessor.CreateStorageVector(Enumerable
                    .Range(0, vSpaceDimensions)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            var vectorStorage2 = scalarProcessor.CreateStorageVector(Enumerable
                    .Range(0, vSpaceDimensions - 2)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            Console.WriteLine($"vSpaceDimension1: {vectorStorage1.MinVSpaceDimension}");
            Console.WriteLine($"vSpaceDimension2: {vectorStorage2.MinVSpaceDimension}");

            var gbtStack =
                GaGbtProductsStack2<double>.Create(
                    scalarProcessor,
                    vectorStorage1,
                    vectorStorage2
                );

            var idScalarDictionary =
                gbtStack
                    .GetEGpIdScalarRecords()
                    .SumToStorageSparseMultivector(scalarProcessor)
                    .GetIdScalarList();

            Console.WriteLine(textComposer.GetTermsText(idScalarDictionary.GetKeyValueRecords()));
        }
    }
}