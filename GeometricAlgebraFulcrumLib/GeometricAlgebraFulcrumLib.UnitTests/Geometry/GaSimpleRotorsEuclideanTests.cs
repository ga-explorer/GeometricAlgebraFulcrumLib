using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Rotors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GeoSimpleRotorsEuclideanTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
        private readonly List<VectorStorage<double>> _vectorsList;
        private readonly List<PureRotor<double>> _rotorsList;


        public IGeometricAlgebraEuclideanProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(8);
        
        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;


        public GeoSimpleRotorsEuclideanTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _vectorsList = new List<VectorStorage<double>>();
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
                    _randomGenerator.GetVectorStorage()
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
                var u = GeometricProcessor.DivideByENorm(_randomGenerator.GetVectorStorage());
                var v = GeometricProcessor.DivideByENorm(_randomGenerator.GetVectorStorage());

                var rotor = 
                    GeometricProcessor.CreatePureRotor(u, v);

                var v1 = rotor.OmMapVector(u);

                var vectorDiffNormSquared = 
                    GeometricProcessor.ENormSquared(GeometricProcessor.Subtract(v1, v));

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