using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Composers;
using GeometricAlgebraFulcrumLib.Structures.Even;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Factories
{
    public static class GaStorageMultivectorGradedFactory
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor)
        {
            return new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<uint, IGaEvenDictionary<T>> gradedDictionary)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor
            );

            composer.AddKVectors(gradedDictionary);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, uint grade, IReadOnlyDictionary<ulong, T> indexScalarDictionary)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor
            );

            composer.AddKVector(grade, indexScalarDictionary);

            return composer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IReadOnlyDictionary<ulong, T> idScalarsDictionary) 
        {
            return new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor,
                idScalarsDictionary
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarsPairs) 
        {
            return new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor,
                idScalarsPairs
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarsTuples) 
        {
            return new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor,
                idScalarsTuples
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageComposerMultivectorGraded<T> CreateStorageComposerGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarsTuples) 
        {
            return new GaStorageComposerMultivectorGraded<T>(
                scalarProcessor,
                gradeIndexScalarsTuples
            );
        }



        //[MethodImpl(MethodImplOptions.AggressiveInlining)]

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
                            indexScalarDict.CreateEvenDictionary()
                    ).CreateGradedDictionary();

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
                    0 => GaStorageScalar<T>.Create(indexScalarDictionary.TryGetValue(0, out var s) ? s : scalarProcessor.ZeroScalar),
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

        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.SetTerms(idScalarPairs);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }

        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.SetTerms(idScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }

        public static IGaStorageMultivectorGraded<T> CreateStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.SetTerms(gradeIndexScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }

        public static IGaStorageMultivectorGraded<T> SumToStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<KeyValuePair<ulong, T>> idScalarPairs)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.AddTerms(idScalarPairs);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }

        public static IGaStorageMultivectorGraded<T> SumToStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<ulong, T>> idScalarTuples)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.AddTerms(idScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }

        public static IGaStorageMultivectorGraded<T> SumToStorageGradedMultivector<T>(this IGaScalarProcessor<T> scalarProcessor, IEnumerable<Tuple<uint, ulong, T>> gradeIndexScalarTuples)
        {
            var composer = new GaStorageComposerMultivectorGraded<T>(scalarProcessor);

            composer.AddTerms(gradeIndexScalarTuples);

            composer.RemoveZeroTerms();

            return composer.GetGradedMultivector();
        }
    }
}