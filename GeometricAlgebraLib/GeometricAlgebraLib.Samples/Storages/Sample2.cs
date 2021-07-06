using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using GeometricAlgebraLib.Algebra.Basis;
using GeometricAlgebraLib.Algebra.Signatures;
using GeometricAlgebraLib.Processing.Implementations.Float64;
using GeometricAlgebraLib.Processing.Scalars;
using GeometricAlgebraLib.Storage;

namespace GeometricAlgebraLib.Samples.Storages
{
    public static class Sample2
    {
        private static readonly Random RandomGenerator 
            = new(10);

        private static readonly IGaScalarProcessor<double> ScalarProcessor
            = GaScalarProcessorFloat64.DefaultProcessor;

        private static readonly List<IGaMultivectorStorage<double>> StoragesList1 
            = new();

        private static readonly List<IGaMultivectorStorage<double>> StoragesList2 
            = new();

        private static double _scalar
            = RandomGenerator.NextDouble();


        public static int VSpaceDimension { get; }
            = 5;

        public static ulong GaSpaceDimension
            => VSpaceDimension.ToGaSpaceDimension();


        private static Dictionary<ulong, double> GetRandomKVectorDictionary(int grade)
        {
            return Enumerable
                .Range(0, (int)GaBasisUtils.KvSpaceDimension(VSpaceDimension, grade))
                .ToDictionary(
                    index => (ulong)index, 
                    _ => RandomGenerator.NextDouble()
                );
        }

        public static void ClassInit()
        {
            //Create a scalar storage
            StoragesList1.Add(
                GaScalarTermStorage<double>.Create(
                    ScalarProcessor,
                    RandomGenerator.NextDouble())
            );

            //Create a set of term storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                StoragesList1.Add(
                    GaKVectorTermStorage<double>.Create(
                        ScalarProcessor,
                        id,
                        RandomGenerator.NextDouble())
                );

            //Create a vector storage
            StoragesList1.Add(
                GaVectorStorage<double>.Create(
                    ScalarProcessor, 
                    GetRandomKVectorDictionary(1)
                )
            );

            //Create a bivector storage
            StoragesList1.Add(
                GaBivectorStorage<double>.Create(
                    ScalarProcessor, 
                    GetRandomKVectorDictionary(2)
                )
            );

            //Create k-vector storages
            for (var grade = 0; grade <= VSpaceDimension; grade++)
                StoragesList1.Add(
                    GaKVectorStorage<double>.Create(
                        ScalarProcessor, 
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

            StoragesList1.Add(
                GaMultivectorGradedStorage<double>.Create(
                    ScalarProcessor, 
                    gradeIndexScalarDictionary
                )
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in StoragesList1)
                StoragesList2.Add(storage.GetMultivectorTermsStorageCopy());

            Debug.Assert(StoragesList1.Count == StoragesList2.Count);

            for (var i = 0; i < StoragesList1.Count; i++)
            {
                Debug.Assert(StoragesList1[i].TermsCount == StoragesList2[i].TermsCount);

                var mvStorageDiff = StoragesList1[i].Subtract(StoragesList2[i]);

                Debug.Assert(mvStorageDiff.IsNearZero());
            }
        }

        private static Func<IGaMultivectorStorage<double>, IGaMultivectorStorage<double>, IGaMultivectorStorage<double>> GetBinaryOperationFunction(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => mv1.Add(mv2),
                "subtract" => (mv1, mv2) => mv1.Subtract(mv2),
                "op" => (mv1, mv2) => mv1.Op(mv2),
                "egp" => GaSignatureUtils.EGp,
                "elcp" => GaSignatureUtils.ELcp,
                "ercp" => GaSignatureUtils.ERcp,
                "efdp" => GaSignatureUtils.EFdp,
                "ehip" => GaSignatureUtils.EHip,
                "ecp" => GaSignatureUtils.ECp,
                "eacp" => GaSignatureUtils.EAcp,
                _ => null
            };
        }

        public static void AssertCorrectBinaryOperations(string funcName)
        {
            var func = GetBinaryOperationFunction(funcName);

            for (var i = 0; i < StoragesList1.Count; i++)
            {
                var storage1 = StoragesList1[i];
                var termsStorage1 = StoragesList2[i];
                
                for (var j = 0; j < StoragesList1.Count; j++)
                {
                    var storage2 = StoragesList1[j];
                    var termsStorage2 = StoragesList2[j];

                    var result1 = func(storage1, storage2);
                    var result2 = func(termsStorage1, termsStorage2);

                    var storageDiff = result1.Subtract(result2);

                    Debug.Assert(storageDiff.IsNearZero());
                }
            }
        }

        public static void AssertCorrectESp()
        {
            for (var i = 0; i < StoragesList1.Count; i++)
            {
                var storage1 = StoragesList1[i];
                var termsStorage1 = StoragesList2[i];
                
                for (var j = 0; j < StoragesList1.Count; j++)
                {
                    var storage2 = StoragesList1[j];
                    var termsStorage2 = StoragesList2[j];

                    var result1 = storage1.ESp(storage2);
                    var result2 = termsStorage1.ESp(termsStorage2);

                    var storageDiff = result1 - result2;

                    Debug.Assert(
                        ScalarProcessor.IsNearZero(storageDiff)
                    );
                }
            }
        }
    }
}
