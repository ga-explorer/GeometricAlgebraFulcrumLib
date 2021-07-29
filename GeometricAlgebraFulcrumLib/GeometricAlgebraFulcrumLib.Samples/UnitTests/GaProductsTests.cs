using System;
using System.Collections.Generic;
using System.Diagnostics;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Samples.UnitTests
{
    public sealed class GaProductsTests
    {
        private readonly GaRandomComposerFloat64 _randomGenerator;
        private readonly List<IGasMultivector<double>> _mvList1;
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
            _mvList1 = new List<IGasMultivector<double>>();
            _mvList2 = new List<GaPoTNumMultivector>();
            _scalar = _randomGenerator.GetScalar();
        }



        private GaPoTNumMultivector 
            CreateGaPoTMultivector(IGasMultivector<double> mvStorage)
        {
            var gapotMv = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mvStorage.GetIdScalarPairs())
                gapotMv.SetTerm((int) id, scalar);

            return gapotMv;
        }

        private GaPoTNumMultivector 
            Subtract(IGasMultivector<double> mv1, GaPoTNumMultivector mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mv1.GetIdScalarPairs())
                mvDiff.SetTerm((int) id, scalar);

            foreach (var term in mv2)
                mvDiff.AddTerm(term.IDsPattern, -term.Value);

            return mvDiff;
        }

        private GaPoTNumMultivector 
            Subtract(GaPoTNumMultivector mv1, IGasMultivector<double> mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var term in mv1)
                mvDiff.SetTerm(term.IDsPattern, term.Value);

            foreach (var (id, scalar) in mv2.GetIdScalarPairs())
                mvDiff.AddTerm((int) id, -scalar);

            return mvDiff;
        }
        
        private Func<IGasMultivector<double>, IGasMultivector<double>, IGasMultivector<double>> 
            GetBinaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "add" => GaProcessorAddUtils.Add,
                "subtract" => GaProcessorSubtractUtils.Subtract,
                "op" => GaProductOpUtils.Op,
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

        private Func<IGasMultivector<double>, IGasMultivector<double>, double> 
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

        private IGasMultivector<double> 
            LeftTimesScalar(IGasMultivector<double> storage)
        {
            return storage.Times(_scalar);
        }

        private GaPoTNumMultivector 
            LeftTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGasMultivector<double> 
            RightTimesScalar(IGasMultivector<double> storage)
        {
            return _scalar.Times(storage);
        }

        private GaPoTNumMultivector 
            RightTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGasMultivector<double> 
            DivideByScalar(IGasMultivector<double> storage)
        {
            return storage.Divide(_scalar);
        }

        private GaPoTNumMultivector 
            DivideByScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(1d / _scalar);
        }

        private Func<IGasMultivector<double>, IGasMultivector<double>> 
            GetUnaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => GaProductEucGpUtils.EGp,
                "egpReverse" => GaProductEucGpUtils.EGpReverse,
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

        private Func<IGasMultivector<double>, double> 
            GetUnaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "espSquared" => GaProductEucSpUtils.ESp,
                "espReverse" => GaProductEucNormUtils.ENormSquared,
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
            var kvSpaceDimension2 = GaBasisUtils.KvSpaceDimension(VSpaceDimension, 2);
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

        public void AssertCorrectInitialization()
        {
            Debug.Assert(_mvList1.Count == _mvList2.Count);

            for (var i = 0; i < _mvList1.Count; i++)
            {
                Debug.Assert(_mvList1[i].TermsCount == _mvList2[i].Count);

                var mvStorageDiff = 
                    Subtract(_mvList1[i], _mvList2[i]);

                Debug.Assert(mvStorageDiff.IsZero());
            }
        }
        
        public void AssertCorrectBinaryOperations(string funcName)
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
                }
            }
        }

        public void AssertCorrectBinaryOperationsWithScalarOutput(string funcName)
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

                    var storageDiff = Processor.CreateScalar(
                        result1 - result2
                    );

                    Debug.Assert(storageDiff.IsNearZero());
                }
            }
        }

        public void AssertCorrectUnaryOperations(string funcName)
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

                Debug.Assert(storageDiff.IsZero());
            }
        }

        public void AssertCorrectUnaryOperationsWithScalarOutput(string funcName)
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

                var storageDiff = Processor.CreateScalar(
                    result1 - result2
                );

                Debug.Assert(storageDiff.IsNearZero());
            }
        }
    }
}
