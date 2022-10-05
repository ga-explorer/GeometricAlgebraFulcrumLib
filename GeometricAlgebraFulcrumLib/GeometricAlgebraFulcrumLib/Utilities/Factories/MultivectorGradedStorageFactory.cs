using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    internal static class MultivectorGradedStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> CreateMultivectorStorageGraded<T>(this ILinVectorGradedStorage<T> termsList)
        {
            return MultivectorGradedStorage<T>.Create(termsList);
        }

        public static IMultivectorGradedStorage<T> CreateMultivectorStorageGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var gradesCount = 
                gradeIndexScalarDictionary.Count;

            if (gradesCount == 0)
                return KVectorStorage<T>.ZeroScalar;

            if (gradesCount > 1)
            {
                var gradedDictionary =
                    gradeIndexScalarDictionary.ToDictionary(
                        indexScalarDict => 
                            indexScalarDict.CreateLinVectorStorage()
                    ).CreateLinVectorGradedStorage();

                return MultivectorGradedStorage<T>.Create(gradedDictionary);
            }

            var (grade, indexScalarDictionary) =
                gradeIndexScalarDictionary.First();

            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return KVectorStorage<T>.CreateKVectorZero(grade);

            if (termsCount > 1)
                return grade switch
                {
                    0 => KVectorStorage<T>.CreateKVectorScalar(indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.ScalarZero),
                    1 => VectorStorage<T>.CreateVectorStorage(indexScalarDictionary),
                    2 => BivectorStorage<T>.Create(indexScalarDictionary),
                    _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
                };

            var (index, scalar) =
                indexScalarDictionary.First();

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVectorStorage(index, scalar),
                2 => BivectorStorage<T>.Create(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> CreateMultivectorStorageGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarRecords)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .SetTerms(idScalarRecords)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> CreateMultivectorStorageGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .SetTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> SumToMultivectorStorageGraded<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }
    }
}