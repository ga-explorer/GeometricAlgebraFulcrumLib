using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Storage;

[TestFixture]
public sealed class BladeOperationTests
{
    private readonly RGaFloat64RandomComposer _randomGenerator;

    private readonly List<RGaFloat64KVector> _bladesList;

    private readonly RGaFloat64Scalar _scalar;

    //private GeoScalarStorage<double> _scalarStorage;


    public RGaFloat64Processor GeometricProcessor
        => RGaFloat64Processor.Euclidean;

    public int VSpaceDimensions 
        => 8;

    public ulong GaSpaceDimensions
        => VSpaceDimensions.ToGaSpaceDimension();


    public BladeOperationTests()
    {
        _randomGenerator = GeometricProcessor.CreateRGaRandomComposer(VSpaceDimensions, 10);
        _scalar = _randomGenerator.GetScalar();
        _bladesList = new List<RGaFloat64KVector>();
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
        RGaFloat64Multivector blade2;
        RGaFloat64Multivector diff;

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