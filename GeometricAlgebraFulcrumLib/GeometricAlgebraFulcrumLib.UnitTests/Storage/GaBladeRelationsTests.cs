using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage
{
    [TestFixture]
    public sealed class BladeRelationsTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;

        private readonly List<KVectorStorage<double>> _bladesList;

        private readonly double _scalar;

        //private GeoScalarStorage<double> _scalarStorage;


        public IGeometricAlgebraProcessor<double> GeometricProcessor
            => ScalarAlgebraFloat64Processor
                .DefaultProcessor
                .CreateGeometricAlgebraEuclideanProcessor(8);

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension
            => GeometricProcessor.GaSpaceDimension;


        public BladeRelationsTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _scalar = _randomGenerator.GetScalar();
            _bladesList = new List<KVectorStorage<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _bladesList.Add(_randomGenerator.GetBladeStorage(grade));

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
                blade2 = GeometricProcessor.Divide(GeometricProcessor.Times(blade1, _scalar), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));

                blade2 = GeometricProcessor.Divide(GeometricProcessor.Times(_scalar, blade1), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));

                blade2 = GeometricProcessor.Divide(GeometricProcessor.Op(_scalar, blade1), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));

                blade2 = GeometricProcessor.Divide(GeometricProcessor.Op(blade1, _scalar), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));

                blade2 = GeometricProcessor.Divide(GeometricProcessor.EGp(_scalar, blade1), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));

                blade2 = GeometricProcessor.Divide(GeometricProcessor.EGp(blade1, _scalar), _scalar);
                diff = GeometricProcessor.Subtract(blade1, blade2);
                Assert.IsTrue(GeometricProcessor.IsNearZero(diff));
            }
        }
    }
}