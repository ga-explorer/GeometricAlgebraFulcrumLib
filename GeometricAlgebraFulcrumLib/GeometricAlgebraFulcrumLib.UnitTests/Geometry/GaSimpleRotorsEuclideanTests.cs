using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Geometry.Euclidean;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GaSimpleRotorsEuclideanTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;
        private readonly List<IGasVector<double>> _vectorsList;
        private readonly List<GaEuclideanSimpleRotor<double>> _rotorsList;


        public IGaProcessor<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(8);

        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;


        public GaSimpleRotorsEuclideanTests()
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
                var u = _randomGenerator.GetVector().DivideByENorm();
                var v = _randomGenerator.GetVector().DivideByENorm();

                var rotor = 
                    GaEuclideanSimpleRotor<double>.Create(Processor, u, v);

                var v1 = rotor.MapVector(u);

                var vectorDiffNormSquared = 
                    v1.Subtract(v).ENormSquared();

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