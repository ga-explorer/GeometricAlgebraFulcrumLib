using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing
{
    [TestFixture]
    public sealed class GeoConsistencyTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
        private readonly List<IMultivectorStorage<double>> _mvListTested;
        private readonly List<MultivectorStorage<double>> _mvListRef;
        private readonly double _scalar;


        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(5);

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public GeoConsistencyTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _mvListTested = new List<IMultivectorStorage<double>>();
            _mvListRef = new List<MultivectorStorage<double>>();
            _scalar = _randomGenerator.GetScalar();
        }
        

        [OneTimeSetUp]
        public void ClassInit()
        {
            //Create a scalar storage
            _mvListTested.Add(
                _randomGenerator.GetScalarTerm()
            );

            //Create a set of vector terms storages
            for (var index = 0; index < VSpaceDimension; index++)
                _mvListTested.Add(
                    _randomGenerator.GetVectorTermByIndex((ulong) index)
                );

            //Create a set of bivector terms storages
            var kvSpaceDimension2 = VSpaceDimension.KVectorSpaceDimension(2);
            for (var index = 0UL; index < kvSpaceDimension2; index++)
                _mvListTested.Add(
                    _randomGenerator.GetBivectorTermByIndex(index)
                );

            //Create a set of blade terms storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                _mvListTested.Add(
                    _randomGenerator.GetKVectorTermById(id)
                );

            //Create a vector storage
            _mvListTested.Add(
                _randomGenerator.GetVector()
            );

            //Create a bivector storage
            _mvListTested.Add(
                _randomGenerator.GetBivector()
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _mvListTested.Add(
                    _randomGenerator.GetKVectorOfGrade(grade)
                );

            //Create graded multivector storage
            _mvListTested.Add(
                _randomGenerator.GetGradedMultivector()
            );

            //Create terms multivector storage
            _mvListTested.Add(
                _randomGenerator.GetTermsMultivector()
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in _mvListTested)
                _mvListRef.Add(storage.GetLinVectorIdScalarStorage().GetCopy().CreateMultivectorSparseStorage());
        }

        [Test]
        public void AssertCorrectInitialization()
        {
            Assert.IsTrue(_mvListTested.Count == _mvListRef.Count);

            for (var i = 0; i < _mvListTested.Count; i++)
            {
                Assert.IsTrue(_mvListTested[i].TermsCount == _mvListRef[i].TermsCount);

                var mvStorageDiff = 
                    GeometricProcessor.Subtract(_mvListTested[i], _mvListRef[i]);

                Assert.IsTrue(GeometricProcessor.IsZero(mvStorageDiff));
            }
        }

        private bool TestDiffIsZero(int i, Func<IGeometricAlgebraProcessor<double>, IMultivectorStorage<double>, IMultivectorStorage<double>> opFunction)
        {
            var tstMv = 
                opFunction(GeometricProcessor, _mvListTested[i]);

            var refMv =
                opFunction(GeometricProcessor, _mvListRef[i]);

            return GeometricProcessor.IsZero(GeometricProcessor.Subtract(tstMv, refMv));
        }

        private bool TestDiffIsZero(int i, int j, Func<IGeometricAlgebraProcessor<double>, IMultivectorStorage<double>, IMultivectorStorage<double>, IMultivectorStorage<double>> opFunction)
        {
            var tstMv = 
                opFunction(GeometricProcessor, _mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(GeometricProcessor, _mvListRef[i], _mvListRef[j]);

            return GeometricProcessor.IsZero(GeometricProcessor.Subtract(tstMv, refMv));
        }
        
        private bool TestDiffIsZero(int i, Func<IGeometricAlgebraProcessor<double>, IMultivectorStorage<double>, double> opFunction)
        {
            var tstMv = 
                opFunction(GeometricProcessor, _mvListTested[i]);

            var refMv =
                opFunction(GeometricProcessor, _mvListRef[i]);

            return GeometricProcessor.IsZero(
                GeometricProcessor.Subtract(tstMv, refMv)
            );
        }
        
        private bool TestDiffIsZero(int i, int j, Func<IGeometricAlgebraProcessor<double>, IMultivectorStorage<double>, IMultivectorStorage<double>, double> opFunction)
        {
            var tstMv = 
                opFunction(GeometricProcessor, _mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(GeometricProcessor, _mvListRef[i], _mvListRef[j]);

            return GeometricProcessor.IsZero(
                GeometricProcessor.Subtract(tstMv, refMv)
            );
        }

        [Test]
        public void AssertOperations1()
        {
            for (var i = 0; i < _mvListTested.Count; i++)
            {
                // Test unary operations on multivectors
                Assert.IsTrue(TestDiffIsZero(i, (processor, mv) => processor.Times(_scalar, mv)));
                Assert.IsTrue(TestDiffIsZero(i, (processor, mv) => processor.Times(mv, _scalar)));
                Assert.IsTrue(TestDiffIsZero(i, (processor, mv) => processor.Divide(mv, _scalar)));
                Assert.IsTrue(TestDiffIsZero(i, MultivectorStorageGpEucUtils.EGp));
                Assert.IsTrue(TestDiffIsZero(i, MultivectorStorageGpEucUtils.EGpReverse));
                Assert.IsTrue(TestDiffIsZero(i, MultivectorStorageSpEucUtils.ESp));
                Assert.IsTrue(TestDiffIsZero(i, MultivectorStorageNormEucUtils.ENorm));
                Assert.IsTrue(TestDiffIsZero(i, MultivectorStorageNormEucUtils.ENormSquared));

                for (var j = 0; j < _mvListTested.Count; j++)
                {
                    // Test binary operations on multivectors
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageAddUtils.Add));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageSubtractUtils.Subtract));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageOpUtils.Op));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageGpEucUtils.EGp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageGpEucUtils.EGpReverse));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageSpEucUtils.ESp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageLcpEucUtils.ELcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageRcpEucUtils.ERcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageFdpEucUtils.EFdp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageHipEucUtils.EHip));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageAcpEucUtils.EAcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, MultivectorStorageCpEucUtils.ECp));
                }
            }
        }
    }
}