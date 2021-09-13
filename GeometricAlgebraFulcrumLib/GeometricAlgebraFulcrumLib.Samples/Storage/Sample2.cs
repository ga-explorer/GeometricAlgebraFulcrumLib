using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample2
    {
        private static readonly Random RandomGenerator 
            = new(10);

        private static readonly IScalarAlgebraProcessor<double> ScalarProcessor
            = ScalarAlgebraFloat64Processor.DefaultProcessor;

        private static readonly List<IMultivectorStorage<double>> StoragesList1 
            = new();

        private static readonly List<IMultivectorStorage<double>> StoragesList2 
            = new();

        private static double _scalar
            = RandomGenerator.NextDouble();


        public static uint VSpaceDimension { get; }
            = 5;

        public static ulong GaSpaceDimension
            => 1UL << (int) VSpaceDimension;


        private static Dictionary<ulong, double> GetRandomKVectorDictionary(uint grade)
        {
            return VSpaceDimension
                .KVectorSpaceDimension(grade)
                .GetRange()
                .ToDictionary(
                    index => index, 
                    _ => RandomGenerator.NextDouble()
                );
        }

        public static void ClassInit()
        {
            //Create a scalar storage
            StoragesList1.Add(
                ScalarProcessor.CreateKVectorScalarStorage(
                    RandomGenerator.NextDouble()
                )
            );

            //Create a set of term storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                StoragesList1.Add(
                    ScalarProcessor.CreateKVectorTermStorage(id,
                        RandomGenerator.NextDouble())
                );

            //Create a vector storage
            StoragesList1.Add(
                ScalarProcessor.CreateVectorStorage(GetRandomKVectorDictionary(1)
                )
            );

            //Create a bivector storage
            StoragesList1.Add(
                ScalarProcessor.CreateBivectorStorage(GetRandomKVectorDictionary(2)
                )
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                StoragesList1.Add(
                    ScalarProcessor.CreateKVectorStorage(grade,
                        GetRandomKVectorDictionary(grade)
                    )
                );

            //Create graded multivector storage
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, double>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetRandomKVectorDictionary(grade)
                );

            StoragesList1.Add(
                ScalarProcessor.CreateMultivectorGradedStorage(gradeIndexScalarDictionary
                )
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in StoragesList1)
                StoragesList2.Add(storage.GetLinVectorIdScalarStorage().GetCopy().CreateMultivectorSparseStorage());

            Debug.Assert(StoragesList1.Count == StoragesList2.Count);

            for (var i = 0; i < StoragesList1.Count; i++)
            {
                Debug.Assert(StoragesList1[i].TermsCount == StoragesList2[i].TermsCount);

                var mvStorageDiff = ScalarProcessor.Subtract(StoragesList1[i], StoragesList2[i]);

                Debug.Assert(ScalarProcessor.IsNearZero(mvStorageDiff));
            }
        }

        private static Func<IMultivectorStorage<double>, IMultivectorStorage<double>, IMultivectorStorage<double>> GetBinaryOperationFunction(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => ScalarProcessor.Add(mv1, mv2),
                "subtract" => (mv1, mv2) => ScalarProcessor.Subtract(mv1, mv2),
                "op" =>  (mv1, mv2) => ScalarProcessor.Op(mv1, mv2),
                "egp" => (mv1, mv2) => ScalarProcessor.EGp(mv1, mv2),
                "elcp" => (mv1, mv2) => ScalarProcessor.ELcp(mv1, mv2),
                "ercp" => (mv1, mv2) => ScalarProcessor.ERcp(mv1, mv2),
                "efdp" => (mv1, mv2) => ScalarProcessor.EFdp(mv1, mv2),
                "ehip" => (mv1, mv2) => ScalarProcessor.EHip(mv1, mv2),
                "ecp" => (mv1, mv2) => ScalarProcessor.ECp(mv1, mv2),
                "eacp" => (mv1, mv2) => ScalarProcessor.EAcp(mv1, mv2),
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

                    var storageDiff = ScalarProcessor.Subtract(result1, result2);

                    Debug.Assert(ScalarProcessor.IsNearZero(storageDiff));
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

                    var result1 = ScalarProcessor.ESp(storage1, storage2);
                    var result2 = ScalarProcessor.ESp(termsStorage1, termsStorage2);

                    var storageDiff = result1 - result2;

                    Debug.Assert(
                        ScalarProcessor.IsNearZero(storageDiff)
                    );
                }
            }
        }
    }
}
