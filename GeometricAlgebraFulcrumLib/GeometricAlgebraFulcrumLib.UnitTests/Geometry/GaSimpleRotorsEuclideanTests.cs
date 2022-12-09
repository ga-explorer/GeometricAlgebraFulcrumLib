using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GeoSimpleRotorsEuclideanTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
        private readonly List<GaVector<double>> _vectorsList;
        private readonly List<PureRotor<double>> _rotorsList;


        public IGeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(8);
        
        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;


        public GeoSimpleRotorsEuclideanTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _vectorsList = new List<GaVector<double>>();
            _rotorsList = new List<PureRotor<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            var count = 10;

            while (count > 0)
            {
                _rotorsList.Add(
                    _randomGenerator.GetEuclideanPureRotor()
                );

                count--;
            }

            count = 100;

            while (count > 0)
            {
                _vectorsList.Add(
                    _randomGenerator.GetVector()
                );

                count--;
            }
        }

        [Test]
        public void AssertRotations()
        {
            var count = 1;
            while (count > 0)
            {
                var u = _randomGenerator.GetVector().DivideByENorm();
                var v = _randomGenerator.GetVector().DivideByENorm();

                var rotor = 
                    GeometricProcessor.CreatePureRotor(u, v);

                var v1 = rotor.OmMap(u);

                var vectorDiffNormSquared = 
                    (v1 - v).ENormSquared().ScalarValue;

                if (!vectorDiffNormSquared.IsNearZero())
                {
                    Console.WriteLine(vectorDiffNormSquared);
                }

                Assert.IsTrue(vectorDiffNormSquared.IsNearZero());

                count--;
            }
        }
    }
}