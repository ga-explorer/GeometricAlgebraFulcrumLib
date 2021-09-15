using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Utilities.Composers;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class MultivectorGradedStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorGradedStorageComposer<T> CreateMultivectorGradedStorageComposer<T>(this IScalarAlgebraProcessor<T> scalarProcessor)
        {
            return new VectorGradedStorageComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> CreateMultivectorGradedStorage<T>(this ILinVectorGradedStorage<T> termsList)
        {
            return MultivectorGradedStorage<T>.Create(termsList);
        }

        public static IMultivectorGradedStorage<T> CreateMultivectorGradedStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
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
                    1 => VectorStorage<T>.CreateVector(indexScalarDictionary),
                    2 => BivectorStorage<T>.CreateBivector(indexScalarDictionary),
                    _ => KVectorStorage<T>.CreateKVector(grade, indexScalarDictionary)
                };

            var (index, scalar) =
                indexScalarDictionary.First();

            return grade switch
            {
                0 => KVectorStorage<T>.CreateKVectorScalar(scalar),
                1 => VectorStorage<T>.CreateVector(index, scalar),
                2 => BivectorStorage<T>.CreateBivector(index, scalar),
                _ => KVectorStorage<T>.CreateKVector(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> CreateMultivectorGradedStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .SetTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> CreateMultivectorGradedStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .SetTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> SumToMultivectorGradedStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> SumToMultivectorGradedStorage<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateMultivectorGradedStorageComposer()
                .AddTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateMultivectorGradedStorage();
        }
    }
}