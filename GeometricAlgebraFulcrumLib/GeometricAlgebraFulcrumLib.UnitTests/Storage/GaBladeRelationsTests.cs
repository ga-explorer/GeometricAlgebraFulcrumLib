using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage
{
    [TestFixture]
    public sealed class GaBladeRelationsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;

        private readonly List<IGasKVector<double>> _bladesList;

        private readonly double _scalar;

        private GasScalar<double> _scalarStorage;


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
            _bladesList = new List<IGasKVector<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _bladesList.Add(_randomGenerator.GetBlade(grade));

            _scalarStorage
                = ScalarProcessor.CreateScalar(_scalar);
        }

        [Test]
        public void AssertScaling()
        {
            IGasMultivector<double> blade2;
            IGasMultivector<double> diff;

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