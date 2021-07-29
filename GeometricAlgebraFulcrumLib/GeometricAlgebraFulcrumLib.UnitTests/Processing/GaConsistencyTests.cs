using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing
{
    [TestFixture]
    public sealed class GaConsistencyTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;
        private readonly List<IGasMultivector<double>> _mvListTested;
        private readonly List<IGasTermsMultivector<double>> _mvListRef;
        private readonly double _scalar;


        public IGaProcessor<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(5);

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public GaConsistencyTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension, 10);
            _mvListTested = new List<IGasMultivector<double>>();
            _mvListRef = new List<IGasTermsMultivector<double>>();
            _scalar = _randomGenerator.GetScalar();
        }
        
        private IGasMultivector<double> LeftTimesScalar(IGasMultivector<double> storage)
        {
            return storage.Times(_scalar);
        }

        private IGasMultivector<double> RightTimesScalar(IGasMultivector<double> storage)
        {
            return _scalar.Times(storage);
        }
        
        private IGasMultivector<double> DivideByScalar(IGasMultivector<double> storage)
        {
            return storage.Divide(_scalar);
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
            var kvSpaceDimension2 = GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);
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
                _mvListRef.Add(storage.GetTermsMultivectorCopy());
        }

        [Test]
        public void AssertCorrectInitialization()
        {
            Assert.IsTrue(_mvListTested.Count == _mvListRef.Count);

            for (var i = 0; i < _mvListTested.Count; i++)
            {
                Assert.IsTrue(_mvListTested[i].TermsCount == _mvListRef[i].TermsCount);

                var mvStorageDiff = 
                    _mvListTested[i].Subtract(_mvListRef[i]);

                Assert.IsTrue(mvStorageDiff.IsZero());
            }
        }

        private bool TestDiffIsZero(int i, Func<IGasMultivector<double>, IGasMultivector<double>> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i]);

            var refMv =
                opFunction(_mvListRef[i]);

            return tstMv.Subtract(refMv).IsZero();
        }

        private bool TestDiffIsZero(int i, int j, Func<IGasMultivector<double>, IGasMultivector<double>, IGasMultivector<double>> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(_mvListRef[i], _mvListRef[j]);

            return tstMv.Subtract(refMv).IsZero();
        }
        
        private bool TestDiffIsZero(int i, Func<IGasMultivector<double>, double> opFunction)
        {
            var scalarProcessor = 
                _mvListTested[i].ScalarProcessor;

            var tstMv = 
                opFunction(_mvListTested[i]);

            var refMv =
                opFunction(_mvListRef[i]);

            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(tstMv, refMv)
            );
        }
        
        private bool TestDiffIsZero(int i, int j, Func<IGasMultivector<double>, IGasMultivector<double>, double> opFunction)
        {
            var scalarProcessor = 
                _mvListTested[i].ScalarProcessor;

            var tstMv = 
                opFunction(_mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(_mvListRef[i], _mvListRef[j]);

            return scalarProcessor.IsZero(
                scalarProcessor.Subtract(tstMv, refMv)
            );
        }

        [Test]
        public void AssertOperations1()
        {
            for (var i = 0; i < _mvListTested.Count; i++)
            {
                // Test unary operations on multivectors
                Assert.IsTrue(TestDiffIsZero(i, LeftTimesScalar));
                Assert.IsTrue(TestDiffIsZero(i, RightTimesScalar));
                Assert.IsTrue(TestDiffIsZero(i, DivideByScalar));
                Assert.IsTrue(TestDiffIsZero(i, GaProductEucGpUtils.EGp));
                Assert.IsTrue(TestDiffIsZero(i, GaProductEucGpUtils.EGpReverse));
                Assert.IsTrue(TestDiffIsZero(i, GaProductEucSpUtils.ESp));
                Assert.IsTrue(TestDiffIsZero(i, GaProductEucNormUtils.ENorm));
                Assert.IsTrue(TestDiffIsZero(i, GaProductEucNormUtils.ENormSquared));

                for (var j = 0; j < _mvListTested.Count; j++)
                {
                    // Test binary operations on multivectors
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProcessorAddUtils.Add));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProcessorSubtractUtils.Subtract));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductOpUtils.Op));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucGpUtils.EGp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucGpUtils.EGpReverse));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucSpUtils.ESp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucLcpUtils.ELcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucRcpUtils.ERcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucFdpUtils.EFdp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucHipUtils.EHip));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucAcpUtils.EAcp));
                    Assert.IsTrue(TestDiffIsZero(i, j, GaProductEucCpUtils.ECp));
                }
            }
        }
        
        
    }
}