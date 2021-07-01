using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraLib.Geometry.Euclidean;
using GeometricAlgebraLib.Implementations.Float64;
using GeometricAlgebraLib.Multivectors.Basis;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GaEuclideanSimpleRotorsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;

        private readonly List<IGaVectorStorage<double>> _vectorsList;

        private readonly List<GaEuclideanSimpleRotor<double>> _rotorsList;


        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public int VSpaceDimension 
            => 8;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public GaEuclideanSimpleRotorsTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension,10);
            _vectorsList = new List<IGaVectorStorage<double>>();
            _rotorsList = new List<GaEuclideanSimpleRotor<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            var count = 10;

            while (count > 0)
            {
                _rotorsList.Add(
                    _randomGenerator.GetEuclideanSimpleRotor()
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
                var vector1 = _randomGenerator.GetVector();
                var vector2 = _randomGenerator.GetVector();

                var rotor = 
                    GaEuclideanSimpleRotor<double>.Create(vector1, vector2);

                var vector3 = rotor.MapVector(vector1);

                var vectorDiffNormSquared = 
                    vector3.Subtract(vector2).ENormSquared();

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