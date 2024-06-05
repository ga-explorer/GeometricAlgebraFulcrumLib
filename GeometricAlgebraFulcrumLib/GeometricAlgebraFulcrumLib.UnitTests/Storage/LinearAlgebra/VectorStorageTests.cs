//using System.Collections.Generic;
//using System.Linq;
//using GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Generic;
//using GeometricAlgebraFulcrumLib.Algebra.Scalars;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
//using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;
//using GeometricAlgebraFulcrumLib.Utilities.Composers;
//using GeometricAlgebraFulcrumLib.Utilities.Factories;
//using NUnit.Framework;

//namespace GeometricAlgebraFulcrumLib.UnitTests.Storage.LinearAlgebra;

//[TestFixture]
//public sealed class VectorStorageTests
//{
//    private const int DenseCount = 32;
//    private readonly LinearAlgebraRandomComposer<double> _randomComposer;
//    private IReadOnlyList<ILinVectorStorage<double>> _storageList1;
//    private IReadOnlyList<LinVectorSparseStorage<double>> _storageList2;
//    private IReadOnlyList<LinVectorArrayStorage<double>> _storageList3;

//    public ILinearProcessor<double> LinearProcessor
//        => ScalarProcessorOfFloat64.Instance.CreateLinearAlgebraProcessor();

//    public VectorStorageTests()
//    {
//        _randomComposer = LinearProcessor.CreateLinearRandomComposer(10);
//    }

        
//    [OneTimeSetUp]
//    public void ClassInit()
//    {
//        _storageList1 = _randomComposer.GetLinVectorStorages(DenseCount);
//        _storageList2 = _storageList1.Select(storage => storage.ToLinVectorSparseStorage()).ToArray();
//        _storageList3 = _storageList1.Select(storage => storage.ToLinVectorArrayStorage(LinearProcessor.ZeroValue)).ToArray();
//    }


//    [Test]
//    public void AssertConsistency()
//    {
//        var count = _storageList1.Count;
//        for (var index = 0; index < count; index++)
//        {
//            var s1 = _storageList1[index];
//            var s2 = _storageList2[index];
//            var s3 = _storageList3[index];

//            var d12 = LinearProcessor.Subtract(s1, s2);
//            Assert.That(LinearProcessor.IsNearZero(d12));

//            var d13 = LinearProcessor.Subtract(s1, s3);
//            Assert.That(LinearProcessor.IsNearZero(d13));

//            var d23 = LinearProcessor.Subtract(s2, s3);
//            Assert.That(LinearProcessor.IsNearZero(d23));
//        }
//    }
//}