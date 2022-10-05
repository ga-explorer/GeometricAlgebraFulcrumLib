using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Multivectors;
using GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Samples.Storage
{
    public static class Sample2
    {
        private static readonly Random RandomGenerator 
            = new(10);

        private static readonly IGeometricAlgebraEuclideanProcessor<double> Processor
            = ScalarAlgebraFloat64Processor.DefaultProcessor.CreateGeometricAlgebraEuclideanProcessor(5);

        private static readonly List<GaMultivector<double>> sList1 
            = new();

        private static readonly List<GaMultivector<double>> sList2 
            = new();

        private static double _scalar
            = RandomGenerator.NextDouble();


        public static uint VSpaceDimension { get; }
            = Processor.VSpaceDimension;

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
            sList1.Add(
                Processor.CreateKVectorScalar(
                    RandomGenerator.NextDouble()
                ).AsMultivector()
            );

            //Create a set of term storages
            for (var id = 0UL; id < GaSpaceDimension; id++)
                sList1.Add(
                    Processor.CreateKVectorTerm(
                        id,
                        RandomGenerator.NextDouble()
                    ).AsMultivector()
                );

            //Create a vector storage
            sList1.Add(
                Processor.CreateVector(GetRandomKVectorDictionary(1)).AsMultivector()
            );

            //Create a bivector storage
            sList1.Add(
                Processor.CreateBivector(GetRandomKVectorDictionary(2)).AsMultivector()
            );

            //Create k-vector storages
            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                sList1.Add(
                    Processor.CreateKVector(
                        grade, 
                        GetRandomKVectorDictionary(grade)
                    ).AsMultivector()
                );

            //Create graded multivector storage
            var gradeIndexScalarDictionary = 
                new Dictionary<uint, Dictionary<ulong, double>>();

            for (var grade = 0U; grade <= VSpaceDimension; grade++)
                gradeIndexScalarDictionary.Add(
                    grade, 
                    GetRandomKVectorDictionary(grade)
                );

            sList1.Add(
                Processor.CreateMultivectorGraded(gradeIndexScalarDictionary)
            );

            //Convert all storages into multivector terms storages
            foreach (var storage in sList1)
                sList2.Add(
                    Processor.CreateMultivectorSparse(
                        storage.MultivectorStorage.GetLinVectorIdScalarStorage().GetCopy()
                    )
                );

            Debug.Assert(sList1.Count == sList2.Count);

            for (var i = 0; i < sList1.Count; i++)
            {
                Debug.Assert(sList1[i].MultivectorStorage.TermsCount == sList2[i].MultivectorStorage.TermsCount);

                var mvDiff = sList1[i] - sList2[i];

                Debug.Assert(mvDiff.IsNearZero());
            }
        }

        private static Func<GaMultivector<double>, GaMultivector<double>, GaMultivector<double>> GetBinaryOperationFunction(string funcName)
        {
            return funcName switch
            {
                "add" => (mv1, mv2) => mv1 + mv2,
                "subtract" => (mv1, mv2) => mv1 - mv2,
                "op" =>  (mv1, mv2) => mv1.Op(mv2),
                "egp" => (mv1, mv2) => mv1.Gp(mv2),
                "elcp" => (mv1, mv2) => mv1.ELcp(mv2),
                "ercp" => (mv1, mv2) => mv1.ERcp(mv2),
                "efdp" => (mv1, mv2) => mv1.EFdp(mv2),
                "ehip" => (mv1, mv2) => mv1.EHip(mv2),
                "ecp" => (mv1, mv2) => mv1.ECp(mv2),
                "eacp" => (mv1, mv2) => mv1.EAcp(mv2),
                _ => null
            };
        }

        public static void AssertCorrectBinaryOperations(string funcName)
        {
            var func = GetBinaryOperationFunction(funcName);

            for (var i = 0; i < sList1.Count; i++)
            {
                var storage1 = sList1[i];
                var terms1 = sList2[i];
                
                for (var j = 0; j < sList1.Count; j++)
                {
                    var storage2 = sList1[j];
                    var terms2 = sList2[j];

                    var result1 = func(storage1, storage2);
                    var result2 = func(terms1, terms2);

                    var storageDiff = result1 - result2;

                    Debug.Assert(storageDiff.IsNearZero());
                }
            }
        }

        public static void AssertCorrectESp()
        {
            for (var i = 0; i < sList1.Count; i++)
            {
                var storage1 = sList1[i];
                var terms1 = sList2[i];
                
                for (var j = 0; j < sList1.Count; j++)
                {
                    var storage2 = sList1[j];
                    var terms2 = sList2[j];

                    var result1 = storage1.ESp(storage2);
                    var result2 = terms1.ESp(terms2);

                    var storageDiff = result1 - result2;

                    Debug.Assert(
                        storageDiff.IsNearZero()
                    );
                }
            }
        }
    }
}
