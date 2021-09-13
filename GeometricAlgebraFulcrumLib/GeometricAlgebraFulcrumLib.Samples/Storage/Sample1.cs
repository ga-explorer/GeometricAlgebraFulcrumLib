using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample1
    {
        public static void Execute()
        {
            var vSpaceDimensions = 5;
            var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor;
            var textComposer = TextFloat64Composer.DefaultComposer;

            var randomGenerator = new Random(10);

            var vectorStorage1 = scalarProcessor.CreateVectorStorage(Enumerable
                    .Range(0, vSpaceDimensions)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            var vectorStorage2 = scalarProcessor.CreateVectorStorage(Enumerable
                    .Range(0, vSpaceDimensions - 2)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            Console.WriteLine($"vSpaceDimension1: {vectorStorage1.MinVSpaceDimension}");
            Console.WriteLine($"vSpaceDimension2: {vectorStorage2.MinVSpaceDimension}");

            var gbtStack =
                GeoGbtProductsStack2<double>.Create(
                    scalarProcessor,
                    vectorStorage1,
                    vectorStorage2
                );

            var idScalarDictionary =
                gbtStack
                    .GetEGpIdScalarRecords()
                    .SumToMultivectorSparseStorage(scalarProcessor)
                    .GetLinVectorIdScalarStorage();

            Console.WriteLine(textComposer.GetTermsText(idScalarDictionary.GetIndexScalarRecords()));
        }
    }
}