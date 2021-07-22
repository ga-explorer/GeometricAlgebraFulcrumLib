using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Generic;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GaEuclideanSimpleRotorsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;

        private readonly List<IGasVector<double>> _vectorsList;

        private readonly List<GaEuclideanSimpleRotor<double>> _rotorsList;


        public GaProcessorGenericOrthonormal<double> Processor { get; }
            = GaProcessorGenericOrthonormal<double>.CreateEuclidean(
                GaScalarProcessorFloat64.DefaultProcessor,
                8
            );

        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;


        public GaEuclideanSimpleRotorsTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension,10);
            _vectorsList = new List<IGasVector<double>>();
            _rotorsList = new List<GaEuclideanSimpleRotor<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            var count = 10;

            while (count > 0)
            {
                _rotorsList.Add(
                    _randomGenerator.GetEuclideanSimpleRotor(Processor)
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
                    GaEuclideanSimpleRotor<double>.Create(
                        Processor, 
                        vector1, 
                        vector2
                    );

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