using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing;

[TestFixture]
public sealed class MultivectorStoragesTests
{
    private readonly RGaFloat64RandomComposer _randomGenerator;
    private readonly List<RGaFloat64Multivector> _mvList1;
    private readonly List<XGaFloat64Multivector> _mvList2;
    private readonly double _scalar;


    public XGaFloat64Processor BasisSet { get; }
        = XGaFloat64Processor.Euclidean;

    public RGaFloat64Processor GeometricProcessor { get; }
        = RGaFloat64Processor.Euclidean;

    public int VSpaceDimensions 
        => 5;

    public ulong GaSpaceDimensions
        => VSpaceDimensions.ToGaSpaceDimension();


    public MultivectorStoragesTests()
    {
        _randomGenerator = GeometricProcessor.CreateRGaRandomComposer(VSpaceDimensions, 10);
        _mvList1 = new List<RGaFloat64Multivector>();
        _mvList2 = new List<XGaFloat64Multivector>();
        _scalar = _randomGenerator.GetScalarValue();
    }


    private XGaFloat64Multivector CreateGeoPoTMultivector(RGaFloat64Multivector mvStorage)
    {
        var gapotMv = BasisSet.CreateComposer();

        foreach (var (id, scalar) in mvStorage.IdScalarPairs)
            gapotMv[id] = scalar;

        return gapotMv.GetSimpleMultivector();
    }

    private XGaFloat64Multivector Subtract(RGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        var mvDiff = BasisSet.CreateComposer();

        foreach (var (id, scalar) in mv1.IdScalarPairs)
            mvDiff[id] = scalar;

        foreach (var (id, scalar) in mv2.IdScalarPairs)
            mvDiff[id] -= scalar;

        return mvDiff.GetSimpleMultivector();
    }

    private XGaFloat64Multivector Subtract(XGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
    {
        var mvDiff = BasisSet.CreateComposer();

        foreach (var (id, scalar) in mv1.IdScalarPairs)
            mvDiff[id] = scalar;

        foreach (var (id, scalar) in mv2.IdScalarPairs)
            mvDiff[id] -= scalar;

        return mvDiff.GetSimpleMultivector();
    }
        
    private Func<RGaFloat64Multivector, RGaFloat64Multivector, RGaFloat64Multivector> GetBinaryOperationFunction1(string funcName)
    {
        return funcName switch
        {
            "add" => (mv1, mv2) => mv1 + mv2,
            "subtract" => (mv1, mv2) => mv1 - mv2,
            "op" => (mv1, mv2) => mv1.Op(mv2),
            "gp" => (mv1, mv2) => mv1.Gp(mv2),
            "lcp" => (mv1, mv2) => mv1.Lcp(mv2),
            "rcp" => (mv1, mv2) => mv1.Rcp(mv2),
            "fdp" => (mv1, mv2) => mv1.Fdp(mv2),
            "hip" => (mv1, mv2) => mv1.Hip(mv2),
            "cp" => (mv1, mv2) => mv1.Cp(mv2),
            "acp" => (mv1, mv2) => mv1.Acp(mv2),
            _ => null
        };
    }

    private Func<XGaFloat64Multivector, XGaFloat64Multivector, XGaFloat64Multivector> GetBinaryOperationFunction2(string funcName)
    {
        return funcName switch
        {
            "add" => (mv1, mv2) => mv1 + mv2,
            "subtract" => (mv1, mv2) => mv1 - mv2,
            "op" => (mv1, mv2) => mv1.Op(mv2),
            "gp" => (mv1, mv2) => mv1.Gp(mv2),
            "lcp" => (mv1, mv2) => mv1.Lcp(mv2),
            "rcp" => (mv1, mv2) => mv1.Rcp(mv2),
            "fdp" => (mv1, mv2) => mv1.Fdp(mv2),
            "hip" => (mv1, mv2) => mv1.Hip(mv2),
            "cp" => (mv1, mv2) => mv1.Cp(mv2),
            "acp" => (mv1, mv2) => mv1.Acp(mv2),
            _ => null
        };
    }

    private Func<RGaFloat64Multivector, RGaFloat64Multivector, double> GetBinaryOperationFunctionWithScalarOutput1(string funcName)
    {
        return funcName switch
        {
            "sp" => (mv1, mv2) => mv1.Sp(mv2).ScalarValue,
            _ => null
        };
    }

    private Func<XGaFloat64Multivector, XGaFloat64Multivector, double> GetBinaryOperationFunctionWithScalarOutput2(string funcName)
    {
        return funcName switch
        {
            "sp" => (mv1, mv2) => mv1.Sp(mv2).ScalarValue,
            _ => null
        };
    }

    private RGaFloat64Multivector LeftTimesScalar(RGaFloat64Multivector storage)
    {
        return storage * _scalar;
    }

    private XGaFloat64Multivector LeftTimesScalar(XGaFloat64Multivector storage)
    {
        return storage * _scalar;
    }

    private RGaFloat64Multivector RightTimesScalar(RGaFloat64Multivector storage)
    {
        return _scalar * storage;
    }

    private XGaFloat64Multivector RightTimesScalar(XGaFloat64Multivector storage)
    {
        return storage * _scalar;
    }

    private RGaFloat64Multivector DivideByScalar(RGaFloat64Multivector storage)
    {
        return storage / _scalar;
    }

    private XGaFloat64Multivector DivideByScalar(XGaFloat64Multivector storage)
    {
        return storage / _scalar;
    }

    private Func<RGaFloat64Multivector, RGaFloat64Multivector> GetUnaryOperationFunction1(string funcName)
    {
        return funcName switch
        {
            "leftTimesScalar" => LeftTimesScalar,
            "rightTimesScalar" => RightTimesScalar,
            "divideByScalar" => DivideByScalar,
            "gpSquared" => mv => mv.Gp(mv),
            "gpReverse" => mv => mv.Gp(mv.Reverse()),
            _ => null
        };
    }

    private Func<XGaFloat64Multivector, XGaFloat64Multivector> GetUnaryOperationFunction2(string funcName)
    {
        return funcName switch
        {
            "leftTimesScalar" => LeftTimesScalar,
            "rightTimesScalar" => RightTimesScalar,
            "divideByScalar" => DivideByScalar,
            "gpSquared" => mv => mv.Gp(mv),
            "gpReverse" => mv => mv.Gp(mv.Reverse()),
            _ => null
        };
    }

    private Func<RGaFloat64Multivector, double> GetUnaryOperationFunctionWithScalarOutput1(string funcName)
    {
        return funcName switch
        {
            "spSquared" => mv => mv.SpSquared().ScalarValue,
            "spReverse" => mv => mv.NormSquared().ScalarValue,
            _ => null
        };
    }

    private Func<XGaFloat64Multivector, double> GetUnaryOperationFunctionWithScalarOutput2(string funcName)
    {
        return funcName switch
        {
            "spSquared" => mv => mv.Sp(mv).ScalarValue,
            "spReverse" => mv => mv.Sp(mv.Reverse()).ScalarValue,
            _ => null
        };
    }


    [OneTimeSetUp]
    public void ClassInit()
    {
        //Create a scalar storage
        _mvList1.Add(
            _randomGenerator.GetScalar()
        );

        //Create a set of vector terms storages
        for (var index = 0; index < VSpaceDimensions; index++)
            _mvList1.Add(
                _randomGenerator.GetVector(index)
            );

        //Create a set of bivector terms storages
        var kvSpaceDimension2 = (int)VSpaceDimensions.KVectorSpaceDimension(2);
        for (var index = 0; index < kvSpaceDimension2; index++)
            _mvList1.Add(
                _randomGenerator.GetBivector(index)
            );

        //Create a set of blade terms storages
        for (var id = 0UL; id < GaSpaceDimensions; id++)
            _mvList1.Add(
                _randomGenerator.GetKVector(id)
            );

        //Create a vector storage
        _mvList1.Add(
            _randomGenerator.GetVector()
        );

        //Create a bivector storage
        _mvList1.Add(
            _randomGenerator.GetBivector()
        );

        //Create k-vector storages
        for (var grade = 0; grade <= VSpaceDimensions; grade++)
            _mvList1.Add(
                _randomGenerator.GetKVectorOfGrade(grade)
            );

        //Create graded multivector storage
        _mvList1.Add(
            _randomGenerator.GetMultivector()
        );

        //Create terms multivector storage
        _mvList1.Add(
            _randomGenerator.GetUniformMultivector((int) GaSpaceDimensions)
        );

        //Convert all storages into multivector terms storages
        foreach (var storage in _mvList1)
            _mvList2.Add(CreateGeoPoTMultivector(storage));
    }

    [Test]
    public void AssertCorrectInitialization()
    {
        Debug.Assert(_mvList1.Count == _mvList2.Count);
        Assert.That(_mvList1.Count == _mvList2.Count);

        for (var i = 0; i < _mvList1.Count; i++)
        {
            Debug.Assert(_mvList1[i].Count == _mvList2[i].Count);
            Assert.That(_mvList1[i].Count == _mvList2[i].Count);

            var mvStorageDiff = 
                Subtract(_mvList1[i], _mvList2[i]);

            Assert.That(mvStorageDiff.IsZero);
        }
    }
        
    [Test, Combinatorial]
    public void AssertCorrectBinaryOperations(
        [Values("add", "subtract", "op", "gp", "lcp", "rcp", "fdp", "hip", "cp", "acp")] string funcName
    )
    {
        var func1 = GetBinaryOperationFunction1(funcName);
        var func2 = GetBinaryOperationFunction2(funcName);

        for (var i = 0; i < _mvList1.Count; i++)
        {
            var storage1 = _mvList1[i];
            var termsStorage1 = _mvList2[i];
                
            for (var j = 0; j < _mvList1.Count; j++)
            {
                var storage2 = _mvList1[j];
                var termsStorage2 = _mvList2[j];

                var result1 = func1(storage1, storage2);
                var result2 = func2(termsStorage1, termsStorage2);

                var storageDiff = Subtract(result1, result2);

                Debug.Assert(storageDiff.IsZero);
                Assert.That(storageDiff.IsZero);
            }
        }
    }

    [Test, Combinatorial]
    public void AssertCorrectBinaryOperationsWithScalarOutput(
        [Values("sp")] string funcName
    )
    {
        var testedFunction1 = 
            GetBinaryOperationFunctionWithScalarOutput1(funcName);

        var testedFunction2 = 
            GetBinaryOperationFunctionWithScalarOutput2(funcName);

        for (var i = 0; i < _mvList1.Count; i++)
        {
            var storage1 = _mvList1[i];
            var termsStorage1 = _mvList2[i];
                
            for (var j = 0; j < _mvList1.Count; j++)
            {
                var storage2 = _mvList1[j];
                var termsStorage2 = _mvList2[j];

                var result1 = testedFunction1(storage1, storage2);
                var result2 = testedFunction2(termsStorage1, termsStorage2);

                var storageDiff = GeometricProcessor.Scalar(
                    result1 - result2
                );

                Assert.That(storageDiff.IsNearZero());
            }
        }
    }

    [Test, Combinatorial]
    public void AssertCorrectUnaryOperations(
        [Values("leftTimesScalar", "rightTimesScalar", "divideByScalar", "gpSquared", "gpReverse")] string funcName
    )
    {
        var testedFunction1 =
            GetUnaryOperationFunction1(funcName);

        var testedFunction2 =
            GetUnaryOperationFunction2(funcName);

        for (var i = 0; i < _mvList1.Count; i++)
        {
            var storage1 = _mvList1[i];
            var termsStorage1 = _mvList2[i];

            var result1 = testedFunction1(storage1);
            var result2 = testedFunction2(termsStorage1);

            var storageDiff = Subtract(result1, result2);

            Assert.That(storageDiff.IsZero);
        }
    }

    [Test, Combinatorial]
    public void AssertCorrectUnaryOperationsWithScalarOutput(
        [Values("spSquared", "spReverse")] string funcName
    )
    {
        var testedFunction1 =
            GetUnaryOperationFunctionWithScalarOutput1(funcName);

        var testedFunction2 =
            GetUnaryOperationFunctionWithScalarOutput2(funcName);

        for (var i = 0; i < _mvList1.Count; i++)
        {
            var storage1 = _mvList1[i];
            var termsStorage1 = _mvList2[i];

            var result1 = testedFunction1(storage1);
            var result2 = testedFunction2(termsStorage1);

            var storageDiff = GeometricProcessor.Scalar(
                result1 - result2
            );

            Debug.Assert(storageDiff.IsNearZero());    
            Assert.That(storageDiff.IsNearZero());
        }
    }

    [Test]
    public void AssertBinaryWithSelfOperations()
    {
        for (var i = 0; i < _mvList1.Count; i++)
        {
            var storage1 = _mvList1[i];
            var termsStorage1 = _mvList2[i];

            var result1 = storage1.Gp(storage1);
            var result2 = termsStorage1.Gp(termsStorage1);

            var storageDiff = Subtract(result1, result2);

            Assert.That(storageDiff.IsZero);


            result1 = storage1.Gp(storage1.Reverse());
            result2 = termsStorage1.Gp(termsStorage1.Reverse());

            storageDiff = Subtract(result1, result2);

            Assert.That(storageDiff.IsZero);


            var scalar1 = storage1.SpSquared();
            var scalar2 = termsStorage1.Sp(termsStorage1).ToScalar();

            var scalarDiff = scalar1 - scalar2.ScalarValue;

            Assert.That(scalarDiff.IsNearZero());
                

            scalar1 = storage1.NormSquared();
            scalar2 = termsStorage1.NormSquared();

            scalarDiff = scalar1 - scalar2.ScalarValue;

            Assert.That(scalarDiff.IsNearZero());
        }
    }
}

//[TestFixture]
//public sealed class MultivectorStoragesTests
//{
//    private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
//    private readonly List<Multivector<double>> _mvList1;
//    private readonly List<GeoPoTNumMultivector> _mvList2;
//    private readonly double _scalar;


//    public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }
//        = ScalarAlgebraFloat64Processor.Instance.CreateGeometricAlgebraEuclideanProcessor(5);

//    public uint VSpaceDimensions 
//        => GeometricProcessor.VSpaceDimensions;

//    public ulong GaSpaceDimensions
//        => VSpaceDimensions.ToGaSpaceDimension();


//    public MultivectorStoragesTests()
//    {
//        _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
//        _mvList1 = new List<Multivector<double>>();
//        _mvList2 = new List<GeoPoTNumMultivector>();
//        _scalar = _randomGenerator.GetScalar();
//    }



//    private GeoPoTNumMultivector 
//        CreateGeoPoTMultivector(Multivector<double> mvStorage)
//    {
//        var gapotMv = GeoPoTNumMultivector.CreateZero();

//        foreach (var (id, scalar) in mvStorage.IdScalarPairs)
//            gapotMv.SetTerm((int) id, scalar);

//        return gapotMv;
//    }

//    private GeoPoTNumMultivector 
//        Subtract(Multivector<double> mv1, GeoPoTNumMultivector mv2)
//    {
//        var mvDiff = GeoPoTNumMultivector.CreateZero();

//        foreach (var (id, scalar) in mv1.IdScalarPairs)
//            mvDiff.SetTerm((int) id, scalar);

//        foreach (var term in mv2)
//            mvDiff.AddTerm(term.IDsPattern, -term.Value);

//        return mvDiff;
//    }

//    private GeoPoTNumMultivector 
//        Subtract(GeoPoTNumMultivector mv1, Multivector<double> mv2)
//    {
//        var mvDiff = GeoPoTNumMultivector.CreateZero();

//        foreach (var term in mv1)
//            mvDiff.SetTerm(term.IDsPattern, term.Value);

//        foreach (var (id, scalar) in mv2.IdScalarPairs)
//            mvDiff.AddTerm((int) id, -scalar);

//        return mvDiff;
//    }
        
//    private Func<Multivector<double>, Multivector<double>, Multivector<double>> 
//        GetBinaryOperationFunction1(string funcName)
//    {
//        return funcName switch
//        {
//            "add" => (mv1, mv2) => GeometricProcessor.Add(mv1, mv2),
//            "subtract" => (mv1, mv2) => GeometricProcessor.Subtract(mv1, mv2),
//            "op" => (mv1, mv2) => GeometricProcessor.Op(mv1, mv2),
//            "gp" => (mv1, mv2) => GeometricProcessor.Gp(mv1, mv2),
//            "lcp" => (mv1, mv2) => GeometricProcessor.Lcp(mv1, mv2),
//            "rcp" => (mv1, mv2) => GeometricProcessor.Rcp(mv1, mv2),
//            "fdp" => (mv1, mv2) => GeometricProcessor.Fdp(mv1, mv2),
//            "hip" => (mv1, mv2) => GeometricProcessor.Hip(mv1, mv2),
//            "cp" => (mv1, mv2) => GeometricProcessor.Cp(mv1, mv2),
//            "acp" => (mv1, mv2) => GeometricProcessor.Acp(mv1, mv2),
//            _ => null
//        };
//    }

//    private Func<GeoPoTNumMultivector, GeoPoTNumMultivector, GeoPoTNumMultivector> 
//        GetBinaryOperationFunction2(string funcName)
//    {
//        return funcName switch
//        {
//            "add" => (mv1, mv2) => mv1.Add(mv2),
//            "subtract" => (mv1, mv2) => mv1.Subtract(mv2),
//            "op" => (mv1, mv2) => mv1.Op(mv2),
//            "gp" => (mv1, mv2) => mv1.Gp(mv2),
//            "lcp" => (mv1, mv2) => mv1.Lcp(mv2),
//            "rcp" => (mv1, mv2) => mv1.Rcp(mv2),
//            "fdp" => (mv1, mv2) => mv1.Fdp(mv2),
//            "hip" => (mv1, mv2) => mv1.Hip(mv2),
//            "cp" => (mv1, mv2) => mv1.Cp(mv2),
//            "acp" => (mv1, mv2) => mv1.Acp(mv2),
//            _ => null
//        };
//    }

//    private Func<Multivector<double>, Multivector<double>, double> 
//        GetBinaryOperationFunctionWithScalarOutput1(string funcName)
//    {
//        return funcName switch
//        {
//            "sp" => (mv1, mv2) => GeometricProcessor.Sp(mv1, mv2),
//            _ => null
//        };
//    }

//    private Func<GeoPoTNumMultivector, GeoPoTNumMultivector, double> 
//        GetBinaryOperationFunctionWithScalarOutput2(string funcName)
//    {
//        return funcName switch
//        {
//            "sp" => (mv1, mv2) => mv1.Sp(mv2).GetTermValue(0),
//            _ => null
//        };
//    }

//    private Multivector<double> 
//        LeftTimesScalar(Multivector<double> storage)
//    {
//        return GeometricProcessor.Times(storage, _scalar);
//    }

//    private GeoPoTNumMultivector 
//        LeftTimesScalar(GeoPoTNumMultivector storage)
//    {
//        return storage.ScaleBy(_scalar);
//    }

//    private Multivector<double> 
//        RightTimesScalar(Multivector<double> storage)
//    {
//        return GeometricProcessor.Times(_scalar, storage);
//    }

//    private GeoPoTNumMultivector 
//        RightTimesScalar(GeoPoTNumMultivector storage)
//    {
//        return storage.ScaleBy(_scalar);
//    }

//    private Multivector<double> 
//        DivideByScalar(Multivector<double> storage)
//    {
//        return GeometricProcessor.Divide(storage, _scalar);
//    }

//    private GeoPoTNumMultivector 
//        DivideByScalar(GeoPoTNumMultivector storage)
//    {
//        return storage.ScaleBy(1d / _scalar);
//    }

//    private Func<Multivector<double>, Multivector<double>> 
//        GetUnaryOperationFunction1(string funcName)
//    {
//        return funcName switch
//        {
//            "leftTimesScalar" => LeftTimesScalar,
//            "rightTimesScalar" => RightTimesScalar,
//            "divideByScalar" => DivideByScalar,
//            "gpSquared" => mv => GeometricProcessor.EGp(mv),
//            "gpReverse" => mv => GeometricProcessor.EGpReverse(mv),
//            _ => null
//        };
//    }

//    private Func<GeoPoTNumMultivector, GeoPoTNumMultivector> 
//        GetUnaryOperationFunction2(string funcName)
//    {
//        return funcName switch
//        {
//            "leftTimesScalar" => LeftTimesScalar,
//            "rightTimesScalar" => RightTimesScalar,
//            "divideByScalar" => DivideByScalar,
//            "gpSquared" => mv => mv.Gp(mv),
//            "gpReverse" => mv => mv.Gp(mv.Reverse()),
//            _ => null
//        };
//    }

//    private Func<Multivector<double>, double> 
//        GetUnaryOperationFunctionWithScalarOutput1(string funcName)
//    {
//        return funcName switch
//        {
//            "spSquared" => mv => GeometricProcessor.ESp(mv),
//            "spReverse" => mv => GeometricProcessor.ENormSquared(mv),
//            _ => null
//        };
//    }

//    private Func<GeoPoTNumMultivector, double> 
//        GetUnaryOperationFunctionWithScalarOutput2(string funcName)
//    {
//        return funcName switch
//        {
//            "spSquared" => mv => mv.Sp(mv).GetTermValue(0),
//            "spReverse" => mv => mv.Sp(mv.Reverse()).GetTermValue(0),
//            _ => null
//        };
//    }


//    [OneTimeSetUp]
//    public void ClassInit()
//    {
//        //Create a scalar storage
//        _mvList1.Add(
//            _randomGenerator.GetScalarTermStorage()
//        );

//        //Create a set of vector terms storages
//        for (var index = 0; index < VSpaceDimensions; index++)
//            _mvList1.Add(
//                _randomGenerator.GetVectorTermStorageByIndex((ulong) index)
//            );

//        //Create a set of bivector terms storages
//        var kvSpaceDimension2 = VSpaceDimensions.KVectorSpaceDimension(2);
//        for (var index = 0UL; index < kvSpaceDimension2; index++)
//            _mvList1.Add(
//                _randomGenerator.GetBivectorTermStorageByIndex(index)
//            );

//        //Create a set of blade terms storages
//        for (var id = 0UL; id < GaSpaceDimensions; id++)
//            _mvList1.Add(
//                _randomGenerator.GetKVectorTermStorageById(id)
//            );

//        //Create a vector storage
//        _mvList1.Add(
//            _randomGenerator.GetVectorStorage()
//        );

//        //Create a bivector storage
//        _mvList1.Add(
//            _randomGenerator.GetBivectorStorage()
//        );

//        //Create k-vector storages
//        for (var grade = 0U; grade <= VSpaceDimensions; grade++)
//            _mvList1.Add(
//                _randomGenerator.GetKVectorStorageOfGrade(grade)
//            );

//        //Create graded multivector storage
//        _mvList1.Add(
//            _randomGenerator.GetGradedMultivectorStorage()
//        );

//        //Create terms multivector storage
//        _mvList1.Add(
//            _randomGenerator.GetTermsMultivectorStorage()
//        );

//        //Convert all storages into multivector terms storages
//        foreach (var storage in _mvList1)
//            _mvList2.Add(CreateGeoPoTMultivector(storage));
//    }

//    [Test]
//    public void AssertCorrectInitialization()
//    {
//        Debug.Assert(_mvList1.Count == _mvList2.Count);
//        Assert.That(_mvList1.Count == _mvList2.Count);

//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            Debug.Assert(_mvList1[i].TermsCount == _mvList2[i].Count);
//            Assert.That(_mvList1[i].TermsCount == _mvList2[i].Count);

//            var mvStorageDiff = 
//                Subtract(_mvList1[i], _mvList2[i]);

//            Assert.That(mvStorageDiff.IsZero());
//        }
//    }
        
//    [Test, Combinatorial]
//    public void AssertCorrectBinaryOperations(
//        [Values("add", "subtract", "op", "gp", "lcp", "rcp", "fdp", "hip", "cp", "acp")] string funcName
//    )
//    {
//        var func1 = GetBinaryOperationFunction1(funcName);
//        var func2 = GetBinaryOperationFunction2(funcName);

//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            var storage1 = _mvList1[i];
//            var termsStorage1 = _mvList2[i];
                
//            for (var j = 0; j < _mvList1.Count; j++)
//            {
//                var storage2 = _mvList1[j];
//                var termsStorage2 = _mvList2[j];

//                var result1 = func1(storage1, storage2);
//                var result2 = func2(termsStorage1, termsStorage2);

//                var storageDiff = Subtract(result1, result2);

//                Debug.Assert(storageDiff.IsZero());
//                Assert.That(storageDiff.IsZero());
//            }
//        }
//    }

//    [Test, Combinatorial]
//    public void AssertCorrectBinaryOperationsWithScalarOutput(
//        [Values("sp")] string funcName
//    )
//    {
//        var testedFunction1 = 
//            GetBinaryOperationFunctionWithScalarOutput1(funcName);

//        var testedFunction2 = 
//            GetBinaryOperationFunctionWithScalarOutput2(funcName);

//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            var storage1 = _mvList1[i];
//            var termsStorage1 = _mvList2[i];
                
//            for (var j = 0; j < _mvList1.Count; j++)
//            {
//                var storage2 = _mvList1[j];
//                var termsStorage2 = _mvList2[j];

//                var result1 = testedFunction1(storage1, storage2);
//                var result2 = testedFunction2(termsStorage1, termsStorage2);

//                var storageDiff = GeometricProcessor.CreateKVectorScalarStorage(
//                    result1 - result2
//                );

//                Assert.That(GeometricProcessor.IsNearZero(storageDiff));
//            }
//        }
//    }

//    [Test, Combinatorial]
//    public void AssertCorrectUnaryOperations(
//        [Values("leftTimesScalar", "rightTimesScalar", "divideByScalar", "gpSquared", "gpReverse")] string funcName
//    )
//    {
//        var testedFunction1 =
//            GetUnaryOperationFunction1(funcName);

//        var testedFunction2 =
//            GetUnaryOperationFunction2(funcName);

//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            var storage1 = _mvList1[i];
//            var termsStorage1 = _mvList2[i];

//            var result1 = testedFunction1(storage1);
//            var result2 = testedFunction2(termsStorage1);

//            var storageDiff = Subtract(result1, result2);

//            Assert.That(storageDiff.IsZero());
//        }
//    }

//    [Test, Combinatorial]
//    public void AssertCorrectUnaryOperationsWithScalarOutput(
//        [Values("spSquared", "spReverse")] string funcName
//    )
//    {
//        var testedFunction1 =
//            GetUnaryOperationFunctionWithScalarOutput1(funcName);

//        var testedFunction2 =
//            GetUnaryOperationFunctionWithScalarOutput2(funcName);

//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            var storage1 = _mvList1[i];
//            var termsStorage1 = _mvList2[i];

//            var result1 = testedFunction1(storage1);
//            var result2 = testedFunction2(termsStorage1);

//            var storageDiff = GeometricProcessor.CreateKVectorScalarStorage(
//                result1 - result2
//            );

//            Assert.That(GeometricProcessor.IsNearZero(storageDiff));
//        }
//    }

//    [Test]
//    public void AssertBinaryWithSelfOperations()
//    {
//        for (var i = 0; i < _mvList1.Count; i++)
//        {
//            var storage1 = _mvList1[i];
//            var termsStorage1 = _mvList2[i];

//            var result1 = GeometricProcessor.Gp(storage1);
//            var result2 = termsStorage1.Gp(termsStorage1);

//            var storageDiff = Subtract(result1, result2);

//            Assert.That(storageDiff.IsZero());


//            result1 = GeometricProcessor.GpReverse(storage1);
//            result2 = termsStorage1.Gp(termsStorage1.Reverse());

//            storageDiff = Subtract(result1, result2);

//            Assert.That(storageDiff.IsZero());


//            var scalar1 = GeometricProcessor.Sp(storage1);
//            var scalar2 = termsStorage1.Sp(termsStorage1).GetScalar();

//            var scalarDiff = scalar1 - scalar2;

//            Assert.That(GeometricProcessor.IsNearZero(scalarDiff));
                

//            scalar1 = GeometricProcessor.NormSquared(storage1);
//            scalar2 = termsStorage1.Norm2();

//            scalarDiff = scalar1 - scalar2;

//            Assert.That(GeometricProcessor.IsNearZero(scalarDiff));
//        }
//    }
//}