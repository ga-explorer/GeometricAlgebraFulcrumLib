using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample1
    {
        public static void Execute()
        {
            var vSpaceDimensions = 5;
            var scalarProcessor = Float64ScalarProcessor.DefaultProcessor;
            var textComposer = Float64TextComposer.DefaultComposer;

            var randomGenerator = new Random(10);

            var vectorStorage1 = scalarProcessor.CreateGaVectorStorage(Enumerable
                    .Range(0, vSpaceDimensions)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            var vectorStorage2 = scalarProcessor.CreateGaVectorStorage(Enumerable
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

            Console.WriteLine(textComposer.GetTermsText(idScalarDictionary.GetIndexScalarRecords()));
        }
    }
}