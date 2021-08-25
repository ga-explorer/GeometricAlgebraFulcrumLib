using System;
using System.Collections.Generic;
using System.Diagnostics;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Utils;
using GeometricAlgebraFulcrumLib.Processing.Multivectors;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Random.Float64;
using GeometricAlgebraFulcrumLib.Processing.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using NUnit.Framework;

namespace GeometricAlgebraFulcrumLib.UnitTests.Processing
{
    [TestFixture]
    public sealed class GaProductsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;
        private readonly List<IGaStorageMultivector<double>> _mvList1;
        private readonly List<GaPoTNumMultivector> _mvList2;
        private readonly double _scalar;


        public IGaProcessor<double> Processor { get; }
            = GaScalarProcessorFloat64.DefaultProcessor.CreateEuclideanProcessor(5);

        public uint VSpaceDimension 
            => Processor.VSpaceDimension;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        public GaProductsTests()
        {
            _randomGenerator = new GaRandomComposerFloat64(VSpaceDimension, 10);
            _mvList1 = new List<IGaStorageMultivector<double>>();
            _mvList2 = new List<GaPoTNumMultivector>();
            _scalar = _randomGenerator.GetScalar();
        }



        private GaPoTNumMultivector 
            CreateGaPoTMultivector(IGaStorageMultivector<double> mvStorage)
        {
            var gapotMv = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mvStorage.GetIdScalarRecords())
                gapotMv.SetTerm((int) id, scalar);

            return gapotMv;
        }

        private GaPoTNumMultivector 
            Subtract(IGaStorageMultivector<double> mv1, GaPoTNumMultivector mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mv1.GetIdScalarRecords())
                mvDiff.SetTerm((int) id, scalar);

            foreach (var term in mv2)
                mvDiff.AddTerm(term.IDsPattern, -term.Value);

            return mvDiff;
        }

        private GaPoTNumMultivector 
            Subtract(GaPoTNumMultivector mv1, IGaStorageMultivector<double> mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var term in mv1)
                mvDiff.SetTerm(term.IDsPattern, term.Value);

            foreach (var (id, scalar) in mv2.GetIdScalarRecords())
                mvDiff.AddTerm((int) id, -scalar);

            return mvDiff;
        }
        
        private Func<IGaStorageMultivector<double>, IGaStorageMultivector<double>, IGaStorageMultivector<double>> 
            GetBinaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => Processor.Add(mv1, mv2),
                "subtract" => (mv1, mv2) => Processor.Subtract(mv1, mv2),
                "op" => (mv1, mv2) => Processor.Op(mv1, mv2),
                "egp" => (mv1, mv2) => Processor.Gp(mv1, mv2),
                "elcp" => (mv1, mv2) => Processor.Lcp(mv1, mv2),
                "ercp" => (mv1, mv2) => Processor.Rcp(mv1, mv2),
                "efdp" => (mv1, mv2) => Processor.Fdp(mv1, mv2),
                "ehip" => (mv1, mv2) => Processor.Hip(mv1, mv2),
                "ecp" => (mv1, mv2) => Processor.Cp(mv1, mv2),
                "eacp" => (mv1, mv2) => Processor.Acp(mv1, mv2),
                _ => null
            };
        }

        private Func<GaPoTNumMultivector, GaPoTNumMultivector, GaPoTNumMultivector> 
            GetBinaryOperationFunction2(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => mv1.Add(mv2),
                "subtract" => (mv1, mv2) => mv1.Subtract(mv2),
                "op" => (mv1, mv2) => mv1.Op(mv2),
                "egp" => (mv1, mv2) => mv1.Gp(mv2),
                "elcp" => (mv1, mv2) => mv1.Lcp(mv2),
                "ercp" => (mv1, mv2) => mv1.Rcp(mv2),
                "efdp" => (mv1, mv2) => mv1.Fdp(mv2),
                "ehip" => (mv1, mv2) => mv1.Hip(mv2),
                "ecp" => (mv1, mv2) => mv1.Cp(mv2),
                "eacp" => (mv1, mv2) => mv1.Acp(mv2),
                _ => null
            };
        }

        private Func<IGaStorageMultivector<double>, IGaStorageMultivector<double>, double> 
            GetBinaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "esp" => (mv1, mv2) => Processor.Sp(mv1, mv2),
                _ => null
            };
        }

        private Func<GaPoTNumMultivector, GaPoTNumMultivector, double> 
            GetBinaryOperationFunctionWithScalarOutput2(string funcName)
        {
            return funcName switch
            {
                "esp" => (mv1, mv2) => mv1.Sp(mv2).GetTermValue(0),
                _ => null
            };
        }

        private IGaStorageMultivector<double> 
            LeftTimesScalar(IGaStorageMultivector<double> storage)
        {
            return Processor.Times(storage, _scalar);
        }

        private GaPoTNumMultivector 
            LeftTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGaStorageMultivector<double> 
            RightTimesScalar(IGaStorageMultivector<double> storage)
        {
            return Processor.Times(_scalar, storage);
        }

        private GaPoTNumMultivector 
            RightTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGaStorageMultivector<double> 
            DivideByScalar(IGaStorageMultivector<double> storage)
        {
            return Processor.Divide(storage, _scalar);
        }

        private GaPoTNumMultivector 
            DivideByScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(1d / _scalar);
        }

        private Func<IGaStorageMultivector<double>, IGaStorageMultivector<double>> 
            GetUnaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => mv => Processor.EGp(mv),
                "egpReverse" => mv => Processor.EGpReverse(mv),
                _ => null
            };
        }

        private Func<GaPoTNumMultivector, GaPoTNumMultivector> 
            GetUnaryOperationFunction2(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => mv => mv.Gp(mv),
                "egpReverse" => mv => mv.Gp(mv.Reverse()),
                _ => null
            };
        }

        private Func<IGaStorageMultivector<double>, double> 
            GetUnaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "espSquared" => mv => Processor.ESp(mv),
                "espReverse" => mv => Processor.ENormSquared(mv),
                _ => null
            };
        }

        private Func<GaPoTNumMultivector, double> 
            GetUnaryOperationFunctionWithScalarOutput2(string funcName)
        {
            return funcName switch
            {
                "espSquared" => mv => mv.Sp(mv).GetTermValue(0),
                "espReverse" => mv => mv.Sp(mv.Reverse()).GetTermValue(0),
                _ => null
            };
        }


        [OneTimeSetUp]
        public void ClassInit()
        {
            //Create a scalar storage
            _mvList1.Add(
                _randomGenerator.GetScalarTerm()
            );

            //Create a set of vector terms storages
            for (var index = 0; index < VSpaceDimension; index++)
                _mvList1.Add(
                    _randomGenerator.GetVectorTermByIndex((ulong) index)
                );

            //Create a set of bivector terms storages
            var kvSpaceDimension2 = VSpaceDimension.KVectorSpaceDimension(2);
            for (var index = 0UL; index < kvSpaceDimension2; index++)
                _mvList1.Add(
                    _randomGenerator.GetBivectorTermByIndex(index)
                );

            //Create a set of blade terms storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                _mvList1.Add(
                    _randomGenerator.GetKVectorTermById(id)
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
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                _mvList1.Add(
                    _randomGenerator.GetKVectorOfGrade(grade)
                );

            //Create graded multivector storage
            _mvList1.Add(
                _randomGenerator.GetGradedMultivector()
            );

            //Create terms multivector storage
            _mvList1.Add(
                _randomGenerator.GetTermsMultivector()
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in _mvList1)
                _mvList2.Add(CreateGaPoTMultivector(storage));
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
            [Values("add", "subtract", "op", "egp", "elcp", "ercp", "efdp", "ehip", "ecp", "eacp")] string funcName
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
            [Values("esp")] string funcName
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

                    var storageDiff = Processor.CreateStorageScalar(
                        result1 - result2
                    );

                    Assert.IsTrue(Processor.IsNearZero(storageDiff));
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
            [Values("espSquared", "espReverse")] string funcName
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

                var storageDiff = Processor.CreateStorageScalar(
                    result1 - result2
                );

                Assert.IsTrue(Processor.IsNearZero(storageDiff));
            }
        }
    }
}
