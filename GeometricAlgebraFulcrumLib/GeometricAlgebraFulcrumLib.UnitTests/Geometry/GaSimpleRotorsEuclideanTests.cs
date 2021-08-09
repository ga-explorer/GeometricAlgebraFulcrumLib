using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Random.Float64;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GaSimpleRotorsEuclideanTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;
        private readonly List<IGaStorageVector<double>> _vectorsList;
        private readonly List<GaPureRotor<double>> _rotorsList;


        public IGaProcessor<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(8);

        public GaScalarProcessorFloat64 ScalarProcessor
            => GaScalarProcessorFloat64.DefaultProcessor;

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;


        public GaSimpleRotorsEuclideanTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension,10);
            _vectorsList = new List<IGaStorageVector<double>>();
            _rotorsList = new List<GaPureRotor<double>>();
        }

        
        [OneTimeSetUp]
        public void ClassInit()
        {
            var count = 10;

            while (count > 0)
            {
                _rotorsList.Add(
                    (GaPureRotor<double>) _randomGenerator.GetEuclideanSimpleRotor(Processor)
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
                var u = Processor.DivideByENorm(_randomGenerator.GetVector());
                var v = Processor.DivideByENorm(_randomGenerator.GetVector());

                var rotor = 
                    Processor.CreateEuclideanRotor(u, v);

                var v1 = rotor.MapVector(u);

                var vectorDiffNormSquared = 
                    Processor.ENormSquared(Processor.Subtract(v1, v));

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