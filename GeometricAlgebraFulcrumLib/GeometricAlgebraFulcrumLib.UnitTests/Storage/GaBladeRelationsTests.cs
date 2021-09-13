using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage
{
    [TestFixture]
    public sealed class BladeRelationsTests
    {
        private readonly GeometricAlgebraRandomFloat64Composer _randomGenerator;

        private readonly List<KVectorStorage<double>> _bladesList;

        private readonly double _scalar;

        //private GeoScalarStorage<double> _scalarStorage;


        public ScalarAlgebraFloat64Processor ScalarProcessor
            => ScalarAlgebraFloat64Processor.DefaultProcessor;

        public uint VSpaceDimension 
            => 8;

        public ulong GaSpaceDimension
            => 1UL << (int) VSpaceDimension;


        public BladeRelationsTests()
        {
            _randomGenerator = new GeometricAlgebraRandomFloat64Composer(VSpaceDimension,10);
            _scalar = _randomGenerator.GetScalar();
            _bladesList = new List<KVectorStorage<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _bladesList.Add(_randomGenerator.GetBlade(grade));

            //_scalarStorage
            //    = ScalarProcessor.CreateKVectorScalarStorage(_scalar);
        }

        [Test]
        public void AssertScaling()
        {
            IMultivectorStorage<double> blade2;
            IMultivectorStorage<double> diff;

            foreach (var blade1 in _bladesList)
            {
                blade2 = ScalarProcessor.Divide(ScalarProcessor.Times(blade1, _scalar), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Times(_scalar, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Op(_scalar, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.Op(blade1, _scalar), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.EGp(_scalar, blade1), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));

                blade2 = ScalarProcessor.Divide(ScalarProcessor.EGp(blade1, _scalar), _scalar);
                diff = ScalarProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(ScalarProcessor.IsNearZero(diff));
            }
        }
    }
}