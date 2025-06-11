using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage;

[TestFixture]
public sealed class BladeOperationTests
{
    private readonly XGaFloat64RandomComposer _randomGenerator;

    private readonly List<XGaFloat64KVector> _bladesList;

    private readonly XGaFloat64Scalar _scalar;

    //private GeoScalarStorage<double> _scalarStorage;


    public XGaFloat64Processor GeometricProcessor
        => XGaFloat64Processor.Euclidean;

    public int VSpaceDimensions 
        => 8;

    public ulong GaSpaceDimensions
        => VSpaceDimensions.GaSpaceDimensions();


    public BladeOperationTests()
    {
        _randomGenerator = GeometricProcessor.CreateXGaRandomComposer(VSpaceDimensions, 10);
        _scalar = _randomGenerator.GetScalar();
        _bladesList = new List<XGaFloat64KVector>();
    }

        
    [OneTimeSetUp]
    public void ClassInit()
    {
        for (var grade = 0; grade <= VSpaceDimensions; grade++)
            _bladesList.Add(_randomGenerator.GetBlade(grade));

        //_scalarStorage
        //    = ScalarProcessor.CreateKVectorScalarStorage(_scalar);
    }

    [Test]
    public void AssertScaling()
    {
        XGaFloat64Multivector blade2;
        XGaFloat64Multivector diff;

        foreach (var blade1 in _bladesList)
        {
            blade2 = ((blade1 * _scalar) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());

            blade2 = ((_scalar * blade1) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());

            blade2 = (_scalar.Op(blade1) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());

            blade2 = (blade1.Op(_scalar) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());

            blade2 = (_scalar.EGp(blade1) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());

            blade2 = (blade1.EGp(_scalar) / _scalar);
            diff = blade1 - blade2;
            Assert.That(diff.IsNearZero());
        }
    }
}