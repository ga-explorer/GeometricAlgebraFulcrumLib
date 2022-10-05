using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage
{
    [TestFixture]
    public sealed class BladeOperationTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;

        private readonly List<GaKVector<double>> _bladesList;

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


        public BladeOperationTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _scalar = _randomGenerator.GetScalar();
            _bladesList = new List<GaKVector<double>>();
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
            GaMultivector<double> blade2;
            GaMultivector<double> diff;

            foreach (var blade1 in _bladesList)
            {
                blade2 = ((blade1 * _scalar) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());

                blade2 = ((_scalar * blade1) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());

                blade2 = (_scalar.Op(blade1) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());

                blade2 = (blade1.Op(_scalar) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());

                blade2 = (_scalar.EGp(blade1) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());

                blade2 = (blade1.EGp(_scalar) / _scalar).AsMultivector();
                diff = blade1 - blade2;
                Assert.IsTrue(diff.IsNearZero());
            }
        }
    }
}