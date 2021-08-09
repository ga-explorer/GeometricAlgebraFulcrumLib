using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Random.Float64;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage
{
    [TestFixture]
    public sealed class GaBladeRelationsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;

        private readonly List<IGaStorageKVector<double>> _bladesList;

        private readonly double _scalar;

        private GaStorageScalar<double> _scalarStorage;


        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public uint VSpaceDimension 
            => 8;

        public ulong GaSpaceDimension
            => 1UL << (int) VSpaceDimension;


        public GaBladeRelationsTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension,10);
            _scalar = _randomGenerator.GetScalar();
            _bladesList = new List<IGaStorageKVector<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _bladesList.Add(_randomGenerator.GetBlade(grade));

            _scalarStorage
                = ScalarProcessor.CreateStorageScalar(_scalar);
        }

        [Test]
        public void AssertScaling()
        {
            IGaStorageMultivector<double> blade2;
            IGaStorageMultivector<double> diff;

            foreach (var blade1 in _bladesList)
            {
                blade2 = ScalarProcessor.Divide(ScalarProcessor.Times(blade1, _scalar), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Times(_scalar, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Op(_scalarStorage, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Op(blade1, _scalarStorage), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.EGp(_scalarStorage, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.EGp(blade1, _scalarStorage), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));
            }
        }
    }
}