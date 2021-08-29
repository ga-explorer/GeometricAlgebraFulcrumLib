using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Composers;

namespace GeometricAlgebraFulcrumLib.Utilities.Factories
{
    public static class GaMultivectorGradedStorageFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static LaVectorGradedStorageComposer<T> CreateStorageGradedMultivectorComposer<T>(this IScalarProcessor<T> scalarProcessor)
        {
            return new LaVectorGradedStorageComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorGradedStorage<T> CreateStorageGradedMultivector<T>(this ILaVectorGradedStorage<T> termsList)
        {
            return GaMultivectorGradedStorage<T>.Create(termsList);
        }

        public static IGaMultivectorGradedStorage<T> CreateStorageGradedMultivector<T>(this IScalarProcessor<T> scalarProcessor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var gradesCount = 
                gradeIndexScalarDictionary.Count;

            if (gradesCount == 0)
                return GaScalarStorage<T>.ZeroScalar;

            if (gradesCount > 1)
            {
                var gradedDictionary =
                    gradeIndexScalarDictionary.CopyToDictionary(
                        indexScalarDict => 
                            indexScalarDict.CreateLaVectorStorage()
                    ).CreateLaVectorGradedStorage();

                return GaMultivectorGradedStorage<T>.Create(gradedDictionary);
            }

            var (grade, indexScalarDictionary) =
                gradeIndexScalarDictionary.First();

            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return GaKVectorStorage<T>.ZeroKVector(grade);

            if (termsCount > 1)
                return grade switch
                {
                    0 => GaScalarStorage<T>.Create(indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.ScalarZero),
                    1 => GaVectorStorage<T>.Create(indexScalarDictionary),
                    2 => GaBivectorStorage<T>.Create(indexScalarDictionary),
                    _ => GaKVectorStorage<T>.Create(grade, indexScalarDictionary)
                };

            var (index, scalar) =
                indexScalarDictionary.First();

            return grade switch
            {
                0 => GaScalarStorage<T>.Create(scalar),
                1 => GaVectorStorage<T>.Create(index, scalar),
                2 => GaBivectorStorage<T>.Create(index, scalar),
                _ => GaKVectorStorage<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> CreateStorageGradedMultivector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .SetTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateGaMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> CreateStorageGradedMultivector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .SetTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateGaMultivectorGradedStorage();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> SumToStorageGradedMultivector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IndexScalarRecord<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateGaMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> SumToStorageGradedMultivector<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<GradeIndexScalarRecord<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateGaMultivectorGradedStorage();
        }
    }
}