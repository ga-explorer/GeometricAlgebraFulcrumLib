using System;
using System.Collections.Generic;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry;

[TestFixture]
public sealed class GeoSimpleRotorsEuclideanTests
{
    private readonly RGaFloat64RandomComposer _randomGenerator;
    private readonly List<RGaFloat64Vector> _vectorsList;
    private readonly List<RGaFloat64PureRotor> _rotorsList;


    public RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;
        
    public int VSpaceDimensions 
        => 8;


    public GeoSimpleRotorsEuclideanTests()
    {
        _randomGenerator = GeometricProcessor.CreateRGaRandomComposer(VSpaceDimensions, 10);
        _vectorsList = new List<RGaFloat64Vector>();
        _rotorsList = new List<RGaFloat64PureRotor>();
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
                u.CreatePureRotor(v);

            var v1 = rotor.OmMap(u);

            var vectorDiffNormSquared = 
                (v1 - v).ENormSquared().ScalarValue();

            if (!vectorDiffNormSquared.IsNearZero())
            {
                Console.WriteLine(vectorDiffNormSquared);
            }

            Assert.IsTrue(vectorDiffNormSquared.IsNearZero());

            count--;
        }
    }
}