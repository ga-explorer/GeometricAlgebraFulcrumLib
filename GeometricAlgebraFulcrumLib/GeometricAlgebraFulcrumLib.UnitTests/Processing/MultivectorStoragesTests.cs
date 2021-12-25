using System;
using System.Collections.Generic;
using System.Diagnostics;
using NumericalGeometryLib.GeometricAlgebra.Basis;
using NumericalGeometryLib.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing
{
    [TestFixture]
    public sealed class MultivectorStoragesTests
    {
        private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
        private readonly List<IMultivectorStorage<double>> _mvList1;
        private readonly List<GaMultivector> _mvList2;
        private readonly double _scalar;


        public GaBasisSet BasisSet { get; }
            = GaBasisSet.CreateEuclidean(5);

        public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(5);

        public uint VSpaceDimension 
            => GeometricProcessor.VSpaceDimension;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public MultivectorStoragesTests()
        {
            _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
            _mvList1 = new List<IMultivectorStorage<double>>();
            _mvList2 = new List<GaMultivector>();
            _scalar = _randomGenerator.GetScalar();
        }


        private GaMultivector CreateGeoPoTMultivector(IMultivectorStorage<double> mvStorage)
        {
            var gapotMv = BasisSet.CreateZero();

            foreach (var (id, scalar) in mvStorage.GetIdScalarRecords())
                gapotMv[id] = scalar;

            return gapotMv;
        }

        private GaMultivector Subtract(IMultivectorStorage<double> mv1, GaMultivector mv2)
        {
            var mvDiff = BasisSet.CreateZero();

            foreach (var (id, scalar) in mv1.GetIdScalarRecords())
                mvDiff[id] = scalar;

            foreach (var (id, scalar) in mv2)
                mvDiff[id] -= scalar;

            return mvDiff.RemoveNearZeroScalars();
        }

        private GaMultivector Subtract(GaMultivector mv1, IMultivectorStorage<double> mv2)
        {
            var mvDiff = BasisSet.CreateZero();

            foreach (var (id, scalar) in mv1)
                mvDiff[id] = scalar;

            foreach (var (id, scalar) in mv2.GetIdScalarRecords())
                mvDiff[id] -= scalar;

            return mvDiff.RemoveNearZeroScalars();
        }
        
        private Func<IMultivectorStorage<double>, IMultivectorStorage<double>, IMultivectorStorage<double>> GetBinaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => GeometricProcessor.Add(mv1, mv2),
                "subtract" => (mv1, mv2) => GeometricProcessor.Subtract(mv1, mv2),
                "op" => (mv1, mv2) => GeometricProcessor.Op(mv1, mv2),
                "gp" => (mv1, mv2) => GeometricProcessor.Gp(mv1, mv2),
                "lcp" => (mv1, mv2) => GeometricProcessor.Lcp(mv1, mv2),
                "rcp" => (mv1, mv2) => GeometricProcessor.Rcp(mv1, mv2),
                "fdp" => (mv1, mv2) => GeometricProcessor.Fdp(mv1, mv2),
                "hip" => (mv1, mv2) => GeometricProcessor.Hip(mv1, mv2),
                "cp" => (mv1, mv2) => GeometricProcessor.Cp(mv1, mv2),
                "acp" => (mv1, mv2) => GeometricProcessor.Acp(mv1, mv2),
                _ => null
            };
        }

        private Func<GaMultivector, GaMultivector, GaMultivector> GetBinaryOperationFunction2(string funcName)
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

        private Func<IMultivectorStorage<double>, IMultivectorStorage<double>, double> GetBinaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "sp" => (mv1, mv2) => GeometricProcessor.Sp(mv1, mv2),
                _ => null
            };
        }

        private Func<GaMultivector, GaMultivector, double> GetBinaryOperationFunctionWithScalarOutput2(string funcName)
        {
            return funcName switch
            {
                "sp" => (mv1, mv2) => mv1.Sp(mv2),
                _ => null
            };
        }

        private IMultivectorStorage<double> LeftTimesScalar(IMultivectorStorage<double> storage)
        {
            return GeometricProcessor.Times(storage, _scalar);
        }

        private GaMultivector LeftTimesScalar(GaMultivector storage)
        {
            return storage * _scalar;
        }

        private IMultivectorStorage<double> RightTimesScalar(IMultivectorStorage<double> storage)
        {
            return GeometricProcessor.Times(_scalar, storage);
        }

        private GaMultivector RightTimesScalar(GaMultivector storage)
        {
            return storage * _scalar;
        }

        private IMultivectorStorage<double> DivideByScalar(IMultivectorStorage<double> storage)
        {
            return GeometricProcessor.Divide(storage, _scalar);
        }

        private GaMultivector DivideByScalar(GaMultivector storage)
        {
            return storage / _scalar;
        }

        private Func<IMultivectorStorage<double>, IMultivectorStorage<double>> GetUnaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => mv => GeometricProcessor.Gp(mv),
                "egpReverse" => mv => GeometricProcessor.GpReverse(mv),
                _ => null
            };
        }

        private Func<GaMultivector, GaMultivector> GetUnaryOperationFunction2(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => mv => mv.GpSquared(),
                "egpReverse" => mv => mv.GpReverse(),
                _ => null
            };
        }

        private Func<IMultivectorStorage<double>, double> GetUnaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "spSquared" => mv => GeometricProcessor.Sp(mv),
                "spReverse" => mv => GeometricProcessor.NormSquared(mv),
                _ => null
            };
        }

        private Func<GaMultivector, double> GetUnaryOperationFunctionWithScalarOutput2(string funcName)
        {
            return funcName switch
            {
                "spSquared" => mv => mv.Sp(mv),
                "spReverse" => mv => mv.Sp(mv.Reverse()),
                _ => null
            };
        }


        [OneTimeSetUp]
        public void ClassInit()
        {
            //Create a scalar storage
            _mvList1.Add(
                _randomGenerator.GetScalarTermStorage()
            );

            //Create a set of vector terms storages
            for (var index = 0; index < VSpaceDimension; index++)
                _mvList1.Add(
                    _randomGenerator.GetVectorTermStorageByIndex((ulong) index)
                );

            //Create a set of bivector terms storages
            var kvSpaceDimension2 = VSpaceDimension.KVectorSpaceDimension(2);
            for (var index = 0UL; index < kvSpaceDimension2; index++)
                _mvList1.Add(
                    _randomGenerator.GetBivectorTermStorageByIndex(index)
                );

            //Create a set of blade terms storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                _mvList1.Add(
                    _randomGenerator.GetKVectorTermStorageById(id)
                );

            //Create a vector storage
            _mvList1.Add(
                _randomGenerator.GetVectorStorage()
            );

            //Create a bivector storage
            _mvList1.Add(
                _randomGenerator.GetBivectorStorage()
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _mvList1.Add(
                    _randomGenerator.GetKVectorStorageOfGrade(grade)
                );

            //Create graded multivector storage
            _mvList1.Add(
                _randomGenerator.GetGradedMultivectorStorage()
            );

            //Create terms multivector storage
            _mvList1.Add(
                _randomGenerator.GetTermsMultivectorStorage()
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in _mvList1)
                _mvList2.Add(CreateGeoPoTMultivector(storage));
        }

        [Test]
        public void AssertCorrectInitialization()
        {
            Debug.Assert(_mvList1.Count == _mvList2.Count);
            Assert.IsTrue(_mvList1.Count == _mvList2.Count);

            for (var i = 0; i < _mvList1.Count; i++)
            {
                Debug.Assert(_mvList1[i].TermsCount == _mvList2[i].Count);
                Assert.IsTrue(_mvList1[i].TermsCount == _mvList2[i].Count);

                var mvStorageDiff = 
                    Subtract(_mvList1[i], _mvList2[i]);

                Assert.IsTrue(mvStorageDiff.IsZero());
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

                    Debug.Assert(storageDiff.IsZero());
                    Assert.IsTrue(storageDiff.IsZero());
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

                    var storageDiff = GeometricProcessor.CreateKVectorScalarStorage(
                        result1 - result2
                    );

                    Assert.IsTrue(GeometricProcessor.IsNearZero(storageDiff));
                }
            }
        }

        [Test, Combinatorial]
        public void AssertCorrectUnaryOperations(
            [Values("leftTimesScalar", "rightTimesScalar", "divideByScalar", "egpSquared", "egpReverse")] string funcName
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

                Assert.IsTrue(storageDiff.IsZero());
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

                var storageDiff = GeometricProcessor.CreateKVectorScalarStorage(
                    result1 - result2
                );

                Debug.Assert(GeometricProcessor.IsNearZero(storageDiff));    
                Assert.IsTrue(GeometricProcessor.IsNearZero(storageDiff));
            }
        }

        [Test]
        public void AssertBinaryWithSelfOperations()
        {
            for (var i = 0; i < _mvList1.Count; i++)
            {
                var storage1 = _mvList1[i];
                var termsStorage1 = _mvList2[i];

                var result1 = GeometricProcessor.Gp(storage1);
                var result2 = termsStorage1.Gp(termsStorage1);

                var storageDiff = Subtract(result1, result2);

                Assert.IsTrue(storageDiff.IsZero());


                result1 = GeometricProcessor.GpReverse(storage1);
                result2 = termsStorage1.Gp(termsStorage1.Reverse());

                storageDiff = Subtract(result1, result2);

                Assert.IsTrue(storageDiff.IsZero());


                var scalar1 = GeometricProcessor.Sp(storage1);
                var scalar2 = termsStorage1.Sp(termsStorage1);

                var scalarDiff = scalar1 - scalar2;

                Assert.IsTrue(GeometricProcessor.IsNearZero(scalarDiff));
                

                scalar1 = GeometricProcessor.NormSquared(storage1);
                scalar2 = termsStorage1.NormSquared();

                scalarDiff = scalar1 - scalar2;

                Assert.IsTrue(GeometricProcessor.IsNearZero(scalarDiff));
            }
        }
    }

    //[TestFixture]
    //public sealed class MultivectorStoragesTests
    //{
    //    private readonly GeometricAlgebraRandomComposer<double> _randomGenerator;
    //    private readonly List<IMultivectorStorage<double>> _mvList1;
    //    private readonly List<GeoPoTNumMultivector> _mvList2;
    //    private readonly double _scalar;


    //    public IGeometricAlgebraProcessor<double> GeometricProcessor { get; }
    //        = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(5);

    //    public uint VSpaceDimension 
    //        => GeometricProcessor.VSpaceDimension;

    //    public ulong GaSpaceDimension
    //        => VSpaceDimension.ToGaSpaceDimension();


    //    public MultivectorStoragesTests()
    //    {
    //        _randomGenerator = GeometricProcessor.CreateGeometricRandomComposer(10);
    //        _mvList1 = new List<IMultivectorStorage<double>>();
    //        _mvList2 = new List<GeoPoTNumMultivector>();
    //        _scalar = _randomGenerator.GetScalar();
    //    }



    //    private GeoPoTNumMultivector 
    //        CreateGeoPoTMultivector(IMultivectorStorage<double> mvStorage)
    //    {
    //        var gapotMv = GeoPoTNumMultivector.CreateZero();

    //        foreach (var (id, scalar) in mvStorage.GetIdScalarRecords())
    //            gapotMv.SetTerm((int) id, scalar);

    //        return gapotMv;
    //    }

    //    private GeoPoTNumMultivector 
    //        Subtract(IMultivectorStorage<double> mv1, GeoPoTNumMultivector mv2)
    //    {
    //        var mvDiff = GeoPoTNumMultivector.CreateZero();

    //        foreach (var (id, scalar) in mv1.GetIdScalarRecords())
    //            mvDiff.SetTerm((int) id, scalar);

    //        foreach (var term in mv2)
    //            mvDiff.AddTerm(term.IDsPattern, -term.Value);

    //        return mvDiff;
    //    }

    //    private GeoPoTNumMultivector 
    //        Subtract(GeoPoTNumMultivector mv1, IMultivectorStorage<double> mv2)
    //    {
    //        var mvDiff = GeoPoTNumMultivector.CreateZero();

    //        foreach (var term in mv1)
    //            mvDiff.SetTerm(term.IDsPattern, term.Value);

    //        foreach (var (id, scalar) in mv2.GetIdScalarRecords())
    //            mvDiff.AddTerm((int) id, -scalar);

    //        return mvDiff;
    //    }
        
    //    private Func<IMultivectorStorage<double>, IMultivectorStorage<double>, IMultivectorStorage<double>> 
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

    //    private Func<IMultivectorStorage<double>, IMultivectorStorage<double>, double> 
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

    //    private IMultivectorStorage<double> 
    //        LeftTimesScalar(IMultivectorStorage<double> storage)
    //    {
    //        return GeometricProcessor.Times(storage, _scalar);
    //    }

    //    private GeoPoTNumMultivector 
    //        LeftTimesScalar(GeoPoTNumMultivector storage)
    //    {
    //        return storage.ScaleBy(_scalar);
    //    }

    //    private IMultivectorStorage<double> 
    //        RightTimesScalar(IMultivectorStorage<double> storage)
    //    {
    //        return GeometricProcessor.Times(_scalar, storage);
    //    }

    //    private GeoPoTNumMultivector 
    //        RightTimesScalar(GeoPoTNumMultivector storage)
    //    {
    //        return storage.ScaleBy(_scalar);
    //    }

    //    private IMultivectorStorage<double> 
    //        DivideByScalar(IMultivectorStorage<double> storage)
    //    {
    //        return GeometricProcessor.Divide(storage, _scalar);
    //    }

    //    private GeoPoTNumMultivector 
    //        DivideByScalar(GeoPoTNumMultivector storage)
    //    {
    //        return storage.ScaleBy(1d / _scalar);
    //    }

    //    private Func<IMultivectorStorage<double>, IMultivectorStorage<double>> 
    //        GetUnaryOperationFunction1(string funcName)
    //    {
    //        return funcName switch
    //        {
    //            "leftTimesScalar" => LeftTimesScalar,
    //            "rightTimesScalar" => RightTimesScalar,
    //            "divideByScalar" => DivideByScalar,
    //            "egpSquared" => mv => GeometricProcessor.EGp(mv),
    //            "egpReverse" => mv => GeometricProcessor.EGpReverse(mv),
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
    //            "egpSquared" => mv => mv.Gp(mv),
    //            "egpReverse" => mv => mv.Gp(mv.Reverse()),
    //            _ => null
    //        };
    //    }

    //    private Func<IMultivectorStorage<double>, double> 
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
    //        for (var index = 0; index < VSpaceDimension; index++)
    //            _mvList1.Add(
    //                _randomGenerator.GetVectorTermStorageByIndex((ulong) index)
    //            );

    //        //Create a set of bivector terms storages
    //        var kvSpaceDimension2 = VSpaceDimension.KVectorSpaceDimension(2);
    //        for (var index = 0UL; index < kvSpaceDimension2; index++)
    //            _mvList1.Add(
    //                _randomGenerator.GetBivectorTermStorageByIndex(index)
    //            );

    //        //Create a set of blade terms storages
    //        for (var id = 0UL; id < GaSpaceDimension; id++)
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
    //        for (var grade = 0U; grade <= VSpaceDimension; grade++)
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
    //        Assert.IsTrue(_mvList1.Count == _mvList2.Count);

    //        for (var i = 0; i < _mvList1.Count; i++)
    //        {
    //            Debug.Assert(_mvList1[i].TermsCount == _mvList2[i].Count);
    //            Assert.IsTrue(_mvList1[i].TermsCount == _mvList2[i].Count);

    //            var mvStorageDiff = 
    //                Subtract(_mvList1[i], _mvList2[i]);

    //            Assert.IsTrue(mvStorageDiff.IsZero());
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
    //                Assert.IsTrue(storageDiff.IsZero());
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

    //                Assert.IsTrue(GeometricProcessor.IsNearZero(storageDiff));
    //            }
    //        }
    //    }

    //    [Test, Combinatorial]
    //    public void AssertCorrectUnaryOperations(
    //        [Values("leftTimesScalar", "rightTimesScalar", "divideByScalar", "egpSquared", "egpReverse")] string funcName
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

    //            Assert.IsTrue(storageDiff.IsZero());
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

    //            Assert.IsTrue(GeometricProcessor.IsNearZero(storageDiff));
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

    //            Assert.IsTrue(storageDiff.IsZero());


    //            result1 = GeometricProcessor.GpReverse(storage1);
    //            result2 = termsStorage1.Gp(termsStorage1.Reverse());

    //            storageDiff = Subtract(result1, result2);

    //            Assert.IsTrue(storageDiff.IsZero());


    //            var scalar1 = GeometricProcessor.Sp(storage1);
    //            var scalar2 = termsStorage1.Sp(termsStorage1).GetScalar();

    //            var scalarDiff = scalar1 - scalar2;

    //            Assert.IsTrue(GeometricProcessor.IsNearZero(scalarDiff));
                

    //            scalar1 = GeometricProcessor.NormSquared(storage1);
    //            scalar2 = termsStorage1.Norm2();

    //            scalarDiff = scalar1 - scalar2;

    //            Assert.IsTrue(GeometricProcessor.IsNearZero(scalarDiff));
    //        }
    //    }
    //}
}
