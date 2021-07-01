using System.Collections.Generic;
using GeometricAlgebraLib.Implementations.Float64;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraLib.UnitTests.Storages
{
    [TestFixture]
    public sealed class GaBladeRelationsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;

        private readonly List<IGaKVectorStorage<double>> _bladesList;

        private readonly double _scalar;

        private GaScalarTermStorage<double> _scalarStorage;


        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public int VSpaceDimension 
            => 8;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public GaBladeRelationsTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension,10);
            _scalar = _randomGenerator.GetScalar();
            _bladesList = new List<IGaKVectorStorage<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                _bladesList.Add(_randomGenerator.GetBlade(grade));

            _scalarStorage
                = GaScalarTermStorage<double>.Create(ScalarProcessor, _scalar);
        }

        [Test]
        public void AssertScaling()
        {
            IGaMultivectorStorage<double> blade2;
            IGaMultivectorStorage<double> diff;

            foreach (var blade1 in _bladesList)
            {
                blade2 = blade1.Times(_scalar).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalar.Times(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalarStorage.Op(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = blade1.Op(_scalarStorage).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = _scalarStorage.EGp(blade1).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());

                blade2 = blade1.EGp(_scalarStorage).Divide(_scalar);
                diff = blade1.Subtract(blade2);
                Assert.IsTrue(diff.IsNearZero());
            }
        }
    }
}