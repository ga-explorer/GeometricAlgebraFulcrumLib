using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.LinearMaps.Rotors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Geometry;

[TestFixture]
public sealed class GeoSimpleRotorsEuclideanTests
{
    private readonly XGaFloat64RandomComposer _randomGenerator;
    private readonly List<XGaFloat64Vector> _vectorsList;
    private readonly List<XGaFloat64PureRotor> _rotorsList;


    public XGaFloat64Processor GeometricProcessor { get; }
        = XGaFloat64Processor.Euclidean;
        
    public int VSpaceDimensions 
        => 8;


    public GeoSimpleRotorsEuclideanTests()
    {
        _randomGenerator = GeometricProcessor.CreateXGaRandomComposer(VSpaceDimensions, 10);
        _vectorsList = new List<XGaFloat64Vector>();
        _rotorsList = new List<XGaFloat64PureRotor>();
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
                (v1 - v).ENormSquared().ScalarValue;

            if (!vectorDiffNormSquared.IsNearZero())
            {
                Console.WriteLine(vectorDiffNormSquared);
            }

            Assert.That(vectorDiffNormSquared.IsNearZero());

            count--;
        }
    }
}