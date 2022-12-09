using System;
using System.Linq;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Products;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Text;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample1
    {
        public static void Execute()
        {
            var vSpaceDimensions = 5;
            var scalarProcessor = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(10);
            var textComposer = TextFloat64Composer.DefaultComposer;

            var randomGenerator = new Random(10);

            var vectorStorage1 = scalarProcessor.CreateVector(
                Enumerable
                    .Range(0, vSpaceDimensions)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            var vectorStorage2 = scalarProcessor.CreateVector(
                Enumerable
                    .Range(0, vSpaceDimensions - 2)
                    .ToDictionary(
                        i => (ulong)i,
                        _ => randomGenerator.NextDouble()
                    )
            );

            Console.WriteLine($"vSpaceDimension1: {vectorStorage1.VectorStorage.MinVSpaceDimension}");
            Console.WriteLine($"vSpaceDimension2: {vectorStorage2.VectorStorage.MinVSpaceDimension}");

            var gbtStack =
                GeoGbtProductsStack2<double>.Create(
                    scalarProcessor,
                    vectorStorage1.VectorStorage,
                    vectorStorage2.VectorStorage
                );

            var idScalarDictionary =
                gbtStack
                    .GetEGpIdScalarRecords()
                    .CreateMultivectorSparse(scalarProcessor, true)
                    .MultivectorStorage
                    .GetLinVectorIdScalarStorage();

            Console.WriteLine(textComposer.GetTermsText(idScalarDictionary.GetIndexScalarRecords()));
        }
    }
}