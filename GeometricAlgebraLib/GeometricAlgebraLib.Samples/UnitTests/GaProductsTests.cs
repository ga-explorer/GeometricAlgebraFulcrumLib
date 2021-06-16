using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GAPoTNumLib.GAPoT;
using GeometricAlgebraLib.Frames;
using GeometricAlgebraLib.Implementations.Float64;
using GeometricAlgebraLib.Processors.Multivectors;
using GeometricAlgebraLib.Processors.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Samples.UnitTests
{
    public sealed class GaProductsTests
    {
        private static readonly Random RandomGenerator 
            = new Random(10);

        private readonly IGaScalarProcessor<double> _scalarsDomain
            = GaScalarProcessorFloat64.DefaultProcessor;

        private readonly List<IGaMultivectorStorage<double>> _mvList1
            = new List<IGaMultivectorStorage<double>>();

        private readonly List<GaPoTNumMultivector> _mvList2
            = new List<GaPoTNumMultivector>();

        private readonly double _scalar
            = RandomGenerator.NextDouble();


        public int VSpaceDimension { get; }
            = 5;

        public ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        private Dictionary<ulong, double> 
            GetRandomKVectorDictionary(int grade)
        {
            return Enumerable
                .Range(0, (int)GaFrameUtils.KvSpaceDimension(VSpaceDimension, grade))
                .ToDictionary(
                    index => (ulong)index, 
                    _ => RandomGenerator.NextDouble()
                );
        }

        private GaPoTNumMultivector 
            CreateGaPoTMultivector(IGaMultivectorStorage<double> mvStorage)
        {
            var gapotMv = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mvStorage.GetIdScalarPairs())
                gapotMv.SetTerm((int) id, scalar);

            return gapotMv;
        }

        private GaPoTNumMultivector 
            Subtract(IGaMultivectorStorage<double> mv1, GaPoTNumMultivector mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var (id, scalar) in mv1.GetIdScalarPairs())
                mvDiff.SetTerm((int) id, scalar);

            foreach (var term in mv2)
                mvDiff.AddTerm(term.IDsPattern, -term.Value);

            return mvDiff;
        }

        private GaPoTNumMultivector 
            Subtract(GaPoTNumMultivector mv1, IGaMultivectorStorage<double> mv2)
        {
            var mvDiff = GaPoTNumMultivector.CreateZero();

            foreach (var term in mv1)
                mvDiff.SetTerm(term.IDsPattern, term.Value);

            foreach (var (id, scalar) in mv2.GetIdScalarPairs())
                mvDiff.AddTerm((int) id, -scalar);

            return mvDiff;
        }
        
        private Func<IGaMultivectorStorage<double>, IGaMultivectorStorage<double>, IGaMultivectorStorage<double>> 
            GetBinaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => mv1.Add(mv2),
                "subtract" => (mv1, mv2) => mv1.Subtract(mv2),
                "op" => (mv1, mv2) => mv1.Op(mv2),
                "egp" => (mv1, mv2) => mv1.EGp(mv2),
                "elcp" => (mv1, mv2) => mv1.ELcp(mv2),
                "ercp" => (mv1, mv2) => mv1.ERcp(mv2),
                "efdp" => (mv1, mv2) => mv1.EFdp(mv2),
                "ehip" => (mv1, mv2) => mv1.EHip(mv2),
                "ecp" => (mv1, mv2) => mv1.ECp(mv2),
                "eacp" => (mv1, mv2) => mv1.EAcp(mv2),
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

        private Func<IGaMultivectorStorage<double>, IGaMultivectorStorage<double>, double> 
            GetBinaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "esp" => GaMultivectorsProcessorUtils.ESp,
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

        private IGaMultivectorStorage<double> 
            LeftTimesScalar(IGaMultivectorStorage<double> storage)
        {
            return storage.Times(_scalar);
        }

        private GaPoTNumMultivector 
            LeftTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGaMultivectorStorage<double> 
            RightTimesScalar(IGaMultivectorStorage<double> storage)
        {
            return _scalar.Times(storage);
        }

        private GaPoTNumMultivector 
            RightTimesScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(_scalar);
        }

        private IGaMultivectorStorage<double> 
            DivideByScalar(IGaMultivectorStorage<double> storage)
        {
            return storage.Divide(_scalar);
        }

        private GaPoTNumMultivector 
            DivideByScalar(GaPoTNumMultivector storage)
        {
            return storage.ScaleBy(1d / _scalar);
        }

        private Func<IGaMultivectorStorage<double>, IGaMultivectorStorage<double>> 
            GetUnaryOperationFunction1(string funcName)
        {
            return funcName switch
            {
                "leftTimesScalar" => LeftTimesScalar,
                "rightTimesScalar" => RightTimesScalar,
                "divideByScalar" => DivideByScalar,
                "egpSquared" => GaMultivectorsProcessorUtils.EGpSquared,
                "egpReverse" => GaMultivectorsProcessorUtils.EGpReverse,
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

        private Func<IGaMultivectorStorage<double>, double> 
            GetUnaryOperationFunctionWithScalarOutput1(string funcName)
        {
            return funcName switch
            {
                "espSquared" => GaMultivectorsProcessorUtils.ESpSquared,
                "espReverse" => GaMultivectorsProcessorUtils.ESpReverse,
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
                GaScalarTermStorage<double>.Create(
                    _scalarsDomain,
                    RandomGenerator.NextDouble()
                )
            );

            //Create a set of vector terms storages
            for (var index = 0UL; index < (ulong) VSpaceDimension; index++)
                _mvList1.Add(
                    GaVectorTermStorage<double>.Create(
                        _scalarsDomain,
                        index,
                        RandomGenerator.NextDouble()
                    )
                );

            //Create a set of bivector terms storages
            var kvSpaceDimension2 = GaFrameUtils.KvSpaceDimension(VSpaceDimension, 2);
            for (var index = 0UL; index < kvSpaceDimension2; index++)
                _mvList1.Add(
                    GaBivectorTermStorage<double>.Create(
                        _scalarsDomain,
                        index,
                        RandomGenerator.NextDouble()
                    )
                );

            //Create a set of blade terms storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                _mvList1.Add(
                    GaKVectorTermStorage<double>.Create(
                        _scalarsDomain,
                        id,
                        RandomGenerator.NextDouble()
                    )
                );

            //Create a vector storage
            _mvList1.Add(
                GaVectorStorage<double>.Create(
                    _scalarsDomain, 
                    GetRandomKVectorDictionary(1)
                )
            );

            //Create a bivector storage
            _mvList1.Add(
                GaBivectorStorage<double>.Create(
                    _scalarsDomain, 
                    GetRandomKVectorDictionary(2)
                )
            );

            //Create k-vector storages
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                _mvList1.Add(
                    GaKVectorStorage<double>.Create(
                        _scalarsDomain, 
                        grade,
                        GetRandomKVectorDictionary(grade)
                    )
                );

            //Create graded multivector storage
            var gradeIndexScalarDictionary = 
                new Dictionary<int, Dictionary<ulong, double>>();

            for (var grade = 0; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetRandomKVectorDictionary(grade)
                );

            _mvList1.Add(
                GaMultivectorGradedStorage<double>.Create(
                    _scalarsDomain, 
                    gradeIndexScalarDictionary
                )
            );

            //Create terms multivector storage
            var idScalarDictionary = 
                new Dictionary<ulong, double>();

            for (var id = 0UL; id < GaSpaceDimension; id++)
                idScalarDictionary.Add(
                    id, 
                    RandomGenerator.NextDouble()
                );

            _mvList1.Add(
                GaMultivectorTermsStorage<double>.Create(
                    _scalarsDomain, 
                    idScalarDictionary
                )
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

                    var storageDiff = GaScalarTermStorage<double>.Create(
                        _scalarsDomain, 
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

                var storageDiff = GaScalarTermStorage<double>.Create(
                    _scalarsDomain,
                    result1 - result2
                );

                Debug.Assert(storageDiff.IsNearZero());
            }
        }
    }
}
