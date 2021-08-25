using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Structures;
using GeometricAlgebraFulcrumLib.Structures.Composers;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageMultivectorGradedFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaListGradedComposer<T> CreateStorageGradedMultivectorComposer<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaListGradedComposer<T>(scalarProcessor);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaListGraded<T> termsList)
        {
            return GaStorageMultivectorGraded<T>.Create(termsList);
        }

        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, Dictionary<uint, Dictionary<ulong, T>> gradeIndexScalarDictionary)
        {
            var gradesCount = 
                gradeIndexScalarDictionary.Count;

            if (gradesCount == 0)
                return GaStorageScalar<T>.ZeroScalar;

            if (gradesCount > 1)
            {
                var gradedDictionary =
                    gradeIndexScalarDictionary.CopyToDictionary(
                        indexScalarDict => 
                            indexScalarDict.CreateEvenList()
                    ).CreateGradedList();

                return GaStorageMultivectorGraded<T>.Create(gradedDictionary);
            }

            var (grade, indexScalarDictionary) =
                gradeIndexScalarDictionary.First();

            var termsCount = 
                indexScalarDictionary.Count;

            if (termsCount == 0)
                return GaStorageKVector<T>.ZeroKVector(grade);

            if (termsCount > 1)
                return grade switch
                {
                    0 => GaStorageScalar<T>.Create(indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.GetZeroScalar()),
                    1 => GaStorageVector<T>.Create(indexScalarDictionary),
                    2 => GaStorageBivector<T>.Create(indexScalarDictionary),
                    _ => GaStorageKVector<T>.Create(grade, indexScalarDictionary)
                };

            var (index, scalar) =
                indexScalarDictionary.First();

            return grade switch
            {
                0 => GaStorageScalar<T>.Create(scalar),
                1 => GaStorageVector<T>.Create(index, scalar),
                2 => GaStorageBivector<T>.Create(index, scalar),
                _ => GaStorageKVector<T>.Create(grade, index, scalar)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordKeyValue<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .SetTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .SetTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateStorageGradedMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> SumToStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordKeyValue<T>> idScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddTerms(idScalarTuples)
                .RemoveZeroTerms()
                .CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> SumToStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<GaRecordGradeKeyValue<T>> gradeIndexScalarTuples)
        {
            return scalarProcessor
                .CreateStorageGradedMultivectorComposer()
                .AddTerms(gradeIndexScalarTuples)
                .RemoveZeroTerms()
                .CreateStorageGradedMultivector();
        }
    }
}