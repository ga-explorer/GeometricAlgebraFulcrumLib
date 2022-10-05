using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing
{
    [TestFixture]
    public sealed class MultivectorConsistencyTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
        private readonly List<GaMultivector<double>> _mvListTested;
        private readonly List<GaMultivector<double>> _mvListRef;
        private readonly double _scalar;


        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(5);

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public MultivectorConsistencyTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _mvListTested = new List<GaMultivector<double>>();
            _mvListRef = new List<GaMultivector<double>>();
            _scalar = _randomGenerator.GetScalar();
        }
        

        [OneTimeSetUp]
        public void ClassInit()
        {
            //Create a scalar storage
            _mvListTested.Add(
                _randomGenerator.GetScalarTerm().AsMultivector()
            );

            //Create a set of vector terms storages
            for (var index = 0; index < VSpaceDimension; index++)
                _mvListTested.Add(
                    _randomGenerator.GetVectorTermByIndex((ulong) index).AsMultivector()
                );

            //Create a set of bivector terms storages
            var kvSpaceDimension2 = VSpaceDimension.KVectorSpaceDimension(2);
            for (var index = 0UL; index < kvSpaceDimension2; index++)
                _mvListTested.Add(
                    _randomGenerator.GetBivectorTermByIndex(index).AsMultivector()
                );

            //Create a set of blade terms storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                _mvListTested.Add(
                    _randomGenerator.GetKVectorTermById(id).AsMultivector()
                );

            //Create a vector storage
            _mvListTested.Add(
                _randomGenerator.GetVector().AsMultivector()
            );

            //Create a bivector storage
            _mvListTested.Add(
                _randomGenerator.GetBivector().AsMultivector()
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _mvListTested.Add(
                    _randomGenerator.GetKVectorOfGrade(grade).AsMultivector()
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
                _mvListRef.Add(
                    GeometricProcessor.CreateMultivector(
                    storage.MultivectorStorage.GetLinVectorIdScalarStorage().GetCopy()
                )
            );
        }

        [Test]
        public void AssertCorrectInitialization()
        {
            Assert.IsTrue(_mvListTested.Count == _mvListRef.Count);

            for (var i = 0; i < _mvListTested.Count; i++)
            {
                Assert.IsTrue(_mvListTested[i].MultivectorStorage.TermsCount == _mvListRef[i].MultivectorStorage.TermsCount);

                var mvDiff = 
                    _mvListTested[i] - _mvListRef[i];

                Assert.IsTrue(mvDiff.IsZero());
            }
        }

        private bool TestDiffIsZero(int i, Func<GaMultivector<double>, GaMultivector<double>> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i]);

            var refMv =
                opFunction(_mvListRef[i]);

            return (tstMv - refMv).IsZero();
        }

        private bool TestDiffIsZero(int i, int j, Func<GaMultivector<double>, GaMultivector<double>, GaMultivector<double>> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(_mvListRef[i], _mvListRef[j]);

            return (tstMv - refMv).IsZero();
        }
        
        private bool TestDiffIsZero(int i, Func<GaMultivector<double>, double> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i]);

            var refMv =
                opFunction(_mvListRef[i]);

            return GeometricProcessor.IsZero(
                GeometricProcessor.Subtract(tstMv, refMv)
            );
        }
        
        private bool TestDiffIsZero(int i, int j, Func<GaMultivector<double>, GaMultivector<double>, double> opFunction)
        {
            var tstMv = 
                opFunction(_mvListTested[i], _mvListTested[j]);

            var refMv =
                opFunction(_mvListRef[i], _mvListRef[j]);

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
                Assert.IsTrue(TestDiffIsZero(i, mv => _scalar * mv));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv * _scalar));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv / _scalar));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv.GpSquared()));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv.GpReverse()));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv.SpSquared().ScalarValue));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv.Norm().ScalarValue));
                Assert.IsTrue(TestDiffIsZero(i, mv => mv.NormSquared().ScalarValue));

                for (var j = 0; j < _mvListTested.Count; j++)
                {
                    // Test binary operations on multivectors
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1 + mv2));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1 - mv2));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Op(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Gp(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.GpReverse(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Sp(mv2).ScalarValue));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Lcp(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Rcp(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Fdp(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Hip(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Acp(mv2)));
                    Assert.IsTrue(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Cp(mv2)));
                }
            }
        }
    }
}