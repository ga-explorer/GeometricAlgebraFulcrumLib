using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.Basis;
using GeometricAlgebraFulcrumLib.Processing;
using GeometricAlgebraFulcrumLib.Processing.Implementations.Float64;
using GeometricAlgebraFulcrumLib.Processing.Products;
using GeometricAlgebraFulcrumLib.Processing.Products.Euclidean;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GaProcessorAddUtils = GeometricAlgebraFulcrumLib.Processing.GaProcessorAddUtils;
using GaProcessorSubtractUtils = GeometricAlgebraFulcrumLib.Processing.GaProcessorSubtractUtils;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample2
    {
        private static readonly Random RandomGenerator 
            = new(10);

        private static readonly IGaScalarProcessor<double> ScalarProcessor
            = GaScalarProcessorFloat64.DefaultProcessor;

        private static readonly List<IGasMultivector<double>> StoragesList1 
            = new();

        private static readonly List<IGasMultivector<double>> StoragesList2 
            = new();

        private static double _scalar
            = RandomGenerator.NextDouble();


        public static uint VSpaceDimension { get; }
            = 5;

        public static ulong GaSpaceDimension
            => 1UL << (int) VSpaceDimension;


        private static Dictionary<ulong, double> GetRandomKVectorDictionary(uint grade)
        {
            return GaBasisUtils
                .KvSpaceDimension(VSpaceDimension, grade)
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
                ScalarProcessor.CreateScalar(
                    RandomGenerator.NextDouble()
                )
            );

            //Create a set of term storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                StoragesList1.Add(
                    ScalarProcessor.CreateKVector(id,
                        RandomGenerator.NextDouble())
                );

            //Create a vector storage
            StoragesList1.Add(
                ScalarProcessor.CreateVector(GetRandomKVectorDictionary(1)
                )
            );

            //Create a bivector storage
            StoragesList1.Add(
                ScalarProcessor.CreateBivector(GetRandomKVectorDictionary(2)
                )
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                StoragesList1.Add(
                    ScalarProcessor.CreateKVector(grade,
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
                GaStorageFactory.CreateGradedMultivector(
                    ScalarProcessor, 
                    gradeIndexScalarDictionary
                )
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in StoragesList1)
                StoragesList2.Add(storage.GetTermsMultivectorCopy());

            Debug.Assert(StoragesList1.Count == StoragesList2.Count);

            for (var i = 0; i < StoragesList1.Count; i++)
            {
                Debug.Assert(StoragesList1[i].TermsCount == StoragesList2[i].TermsCount);

                var mvStorageDiff = StoragesList1[i].Subtract(StoragesList2[i]);

                Debug.Assert(mvStorageDiff.IsNearZero());
            }
        }

        private static Func<IGasMultivector<double>, IGasMultivector<double>, IGasMultivector<double>> GetBinaryOperationFunction(string funcName)
        {
            return funcName switch
            {
                "add" => GaProcessorAddUtils.Add,
                "subtract" => GaProcessorSubtractUtils.Subtract,
                "op" =>  GaProductOpUtils.Op,
                "egp" => GaProductEucGpUtils.EGp,
                "elcp" => GaProductEucLcpUtils.ELcp,
                "ercp" => GaProductEucRcpUtils.ERcp,
                "efdp" => GaProductEucFdpUtils.EFdp,
                "ehip" => GaProductEucHipUtils.EHip,
                "ecp" => GaProductEucCpUtils.ECp,
                "eacp" => GaProductEucAcpUtils.EAcp,
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
