using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing;

[TestFixture]
public sealed class MultivectorConsistencyTests
{
    private readonly XGaFloat64RandomComposer _randomGenerator;
    private readonly List<XGaFloat64Multivector> _mvListTested;
    private readonly List<XGaFloat64Multivector> _mvListRef;
    private readonly double _scalar;


    public XGaFloat64Processor GeometricProcessor { get; }
        = XGaFloat64Processor.Euclidean;

    public int VSpaceDimensions 
        => 5;

    public ulong GaSpaceDimensions
        => 1UL << VSpaceDimensions;


    public MultivectorConsistencyTests()
    {
        _randomGenerator = GeometricProcessor.CreateXGaRandomComposer(VSpaceDimensions, 10);
        _mvListTested = new List<XGaFloat64Multivector>();
        _mvListRef = new List<XGaFloat64Multivector>();
        _scalar = _randomGenerator.GetScalarValue();
    }
        

    [OneTimeSetUp]
    public void ClassInit()
    {
        //Create a scalar storage
        _mvListTested.Add(
            _randomGenerator.GetScalar()
        );

        //Create a set of vector terms storages
        for (var index = 0; index < VSpaceDimensions; index++)
            _mvListTested.Add(
                _randomGenerator.GetVector(index)
            );

        //Create a set of bivector terms storages
        var kvSpaceDimension2 = (int) VSpaceDimensions.KVectorSpaceDimensions(2);
        for (var index = 0; index < kvSpaceDimension2; index++)
            _mvListTested.Add(
                _randomGenerator.GetBivector(index)
            );

        //Create a set of blade terms storages
        for (var id = 0UL; id < GaSpaceDimensions; id++)
            _mvListTested.Add(
                _randomGenerator.GetKVector(id)
            );

        //Create a vector storage
        _mvListTested.Add(
            _randomGenerator.GetVector()
        );

        //Create a bivector storage
        _mvListTested.Add(
            _randomGenerator.GetBivector()
        );

        //Create k-vector storages
        for (var grade = 0; grade <= VSpaceDimensions; grade++)
            _mvListTested.Add(
                _randomGenerator.GetKVectorOfGrade(grade)
            );

        //Create graded multivector storage
        _mvListTested.Add(
            _randomGenerator.GetMultivector()
        );

        //Create terms multivector storage
        _mvListTested.Add(
            _randomGenerator.GetUniformMultivector((int) GaSpaceDimensions)
        );

        //Convert all storages into multivector terms storages
        foreach (var storage in _mvListTested)
            _mvListRef.Add(storage);
    }

    [Test]
    public void AssertCorrectInitialization()
    {
        Assert.That(_mvListTested.Count == _mvListRef.Count);

        for (var i = 0; i < _mvListTested.Count; i++)
        {
            Assert.That(_mvListTested[i].Count == _mvListRef[i].Count);

            var mvDiff = 
                _mvListTested[i] - _mvListRef[i];

            Assert.That(mvDiff.IsZero);
        }
    }

    private bool TestDiffIsZero(int i, Func<XGaFloat64Multivector, XGaFloat64Multivector> opFunction)
    {
        var tstMv = 
            opFunction(_mvListTested[i]);

        var refMv =
            opFunction(_mvListRef[i]);

        return (tstMv - refMv).IsZero;
    }

    private bool TestDiffIsZero(int i, int j, Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> opFunction)
    {
        var tstMv = 
            opFunction(_mvListTested[i], _mvListTested[j]);

        var refMv =
            opFunction(_mvListRef[i], _mvListRef[j]);

        return (tstMv - refMv).IsZero;
    }
        
    private bool TestDiffIsZero(int i, Func<XGaFloat64Multivector, double> opFunction)
    {
        var tstMv = 
            opFunction(_mvListTested[i]);

        var refMv =
            opFunction(_mvListRef[i]);

        return (tstMv - refMv).IsZero();
    }
        
    private bool TestDiffIsZero(int i, int j, Func<XGaFloat64Multivector, XGaFloat64Multivector, double> opFunction)
    {
        var tstMv = 
            opFunction(_mvListTested[i], _mvListTested[j]);

        var refMv =
            opFunction(_mvListRef[i], _mvListRef[j]);

        return (tstMv - refMv).IsZero();
    }

    [Test]
    public void AssertOperations1()
    {
        for (var i = 0; i < _mvListTested.Count; i++)
        {
            // Test unary operations on multivectors
            Assert.That(TestDiffIsZero(i, mv => _scalar * mv));
            Assert.That(TestDiffIsZero(i, mv => mv * _scalar));
            Assert.That(TestDiffIsZero(i, mv => mv / _scalar));
            Assert.That(TestDiffIsZero(i, mv => mv.Gp(mv)));
            Assert.That(TestDiffIsZero(i, mv => mv.Gp(mv.Reverse())));
            Assert.That(TestDiffIsZero(i, mv => mv.SpSquared().ScalarValue));
            Assert.That(TestDiffIsZero(i, mv => mv.Norm().ScalarValue));
            Assert.That(TestDiffIsZero(i, mv => mv.NormSquared().ScalarValue));

            for (var j = 0; j < _mvListTested.Count; j++)
            {
                // Test binary operations on multivectors
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1 + mv2));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1 - mv2));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Op(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Gp(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Gp(mv2.Reverse())));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Sp(mv2).ScalarValue));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Lcp(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Rcp(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Fdp(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Hip(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Acp(mv2)));
                Assert.That(TestDiffIsZero(i, j, (mv1, mv2) => mv1.Cp(mv2)));
            }
        }
    }
}