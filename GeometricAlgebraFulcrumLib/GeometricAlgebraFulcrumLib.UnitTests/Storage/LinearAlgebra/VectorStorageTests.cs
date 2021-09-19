using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage.LinearAlgebra
{
    [TestFixture]
    public sealed class VectorStorageTests
    {
        private const int DenseCount = 32;
        private readonly LinearAlgebraRandomComposer<double> _randomComposer;
        private IReadOnlyList<ILinVectorStorage<double>> _storageList1;
        private IReadOnlyList<LinVectorSparseStorage<double>> _storageList2;
        private IReadOnlyList<LinVectorArrayStorage<double>> _storageList3;

        public ILinearAlgebraProcessor<double> LinearProcessor
            => ScalarAlgebraFloat64Processor.DefaultProcessor.CreateLinearAlgebraProcessor();

        public VectorStorageTests()
        {
            _randomComposer = LinearProcessor.CreateLinearRandomComposer(10);
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            _storageList1 = _randomComposer.GetLinVectorStorages(DenseCount);
            _storageList2 = _storageList1.Select(storage => storage.ToLinVectorSparseStorage()).ToArray();
            _storageList3 = _storageList1.Select(storage => storage.ToLinVectorArrayStorage(LinearProcessor.ScalarZero)).ToArray();
        }


        [Test]
        public void AssertConsistency()
        {
            var count = _storageList1.Count;
            for (var index = 0; index < count; index++)
            {
                var s1 = _storageList1[index];
                var s2 = _storageList2[index];
                var s3 = _storageList3[index];

                var d12 = LinearProcessor.Subtract(s1, s2);
                Assert.IsTrue(LinearProcessor.IsNearZero(d12));

                var d13 = LinearProcessor.Subtract(s1, s3);
                Assert.IsTrue(LinearProcessor.IsNearZero(d13));

                var d23 = LinearProcessor.Subtract(s2, s3);
                Assert.IsTrue(LinearProcessor.IsNearZero(d23));
            }
        }
    }
}