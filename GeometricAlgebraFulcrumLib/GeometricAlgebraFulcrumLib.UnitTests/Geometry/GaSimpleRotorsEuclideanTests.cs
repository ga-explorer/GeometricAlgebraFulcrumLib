using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Geometry.Rotors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Random;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry
{
    [TestFixture]
    public sealed class GaSimpleRotorsEuclideanTests
    {
        private readonly GaFloat64RandomComposer _randomGenerator;
        private readonly List<IGaVectorStorage<double>> _vectorsList;
        private readonly List<GaPureRotor<double>> _rotorsList;


        public IGaProcessor<double> Processor { get; }
            = Float64ScalarProcessor.DefaultProcessor.CreateGaEuclideanProcessor(8);
        
        public uint VSpaceDimension 
            => Processor.VSpaceDimension;


        public GaSimpleRotorsEuclideanTests()
        {
            _randomGenerator = new GaFloat64RandomComposer(VSpaceDimension,10);
            _vectorsList = new List<IGaVectorStorage<double>>();
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