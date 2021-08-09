using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Factories;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Binary
{
    public static class GaProcessorSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorGraded<T> SubtractAsIGaMultivectorGradedStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, T scalar2)
        {
            return scalarProcessor
                .CreateStorageComposerGradedMultivector()
                .SetKVectors(mv1.GetGradeIndexScalarDictionary())
                .SubtractScalar(scalar2)
                .RemoveZeroTerms()
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorGraded<T> SubtractAsIGaMultivectorGradedStorage<T>(IGaScalarProcessor<T> scalarProcessor, T scalar2, IGaStorageMultivectorGraded<T> mv1)
        {
            return scalarProcessor
                .CreateStorageComposerGradedMultivector()
                .SetScalar(scalar2)
                .SubtractKVectors(mv1.GetGradeIndexScalarDictionary())
                .RemoveZeroTerms()
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorGraded<T> SubtractAsIGaMultivectorGradedStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv1, IGaStorageMultivectorGraded<T> mv2)
        {
            return scalarProcessor
                .CreateStorageComposerGradedMultivector()
                .SetKVectors(mv1.GetGradeIndexScalarDictionary())
                .SubtractKVectors(mv2.GetGradeIndexScalarDictionary())
                .RemoveZeroTerms()
                .GetGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorSparse<T> SubtractAsIGaMultivectorTermsStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, T scalar2)
        {
            return scalarProcessor
                .CreateStorageComposerSparseMultivector()
                .SetKVectors(mv1.GetGradeIndexScalarDictionary())
                .SubtractScalar(scalar2)
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorSparse<T> SubtractAsIGaMultivectorTermsStorage<T>(IGaScalarProcessor<T> scalarProcessor, T scalar2, IGaStorageMultivector<T> mv1)
        {
            return scalarProcessor
                .CreateStorageComposerSparseMultivector()
                .SetScalar(scalar2)
                .SubtractKVectors(mv1.GetGradeIndexScalarDictionary())
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGaStorageMultivectorSparse<T> SubtractAsIGaMultivectorTermsStorage<T>(IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return scalarProcessor
                .CreateStorageComposerSparseMultivector()
                .SetKVectors(mv1.GetGradeIndexScalarDictionary())
                .SubtractKVectors(mv2.GetGradeIndexScalarDictionary())
                .RemoveZeroTerms()
                .GetSparseMultivector();
        }



        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv, T scalar2)
        {
            if (mv.IsEmpty())
                return scalarProcessor.Negative(scalar2).CreateStorageScalar();

            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Subtract(mv.FirstScalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar2, IGaStorageScalar<T> mv)
        {
            if (mv.IsEmpty())
                return scalar2.CreateStorageScalar();

            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Subtract(scalar2, mv.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv1, IGaStorageScalar<T> mv2)
        {
            if (mv1.IsEmpty())
            {
                return mv2.IsEmpty() 
                    ? GaStorageScalar<T>.ZeroScalar 
                    : scalarProcessor.Negative(mv2.FirstScalar).CreateStorageScalar();
            }

            return scalarProcessor.CreateStorageScalar(
                scalarProcessor.Subtract(mv1.FirstScalar, mv2.FirstScalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv1, IGaStorageVector<T> mv2)
        {
            return scalarProcessor
                .CreateStorageComposerVector()
                .SetTerms(mv1.IndexScalarDictionary)
                .SubtractTerms(mv2.IndexScalarDictionary)
                .RemoveZeroTerms()
                .GetVector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv1, IGaStorageBivector<T> mv2)
        {
            return scalarProcessor
                .CreateStorageComposerBivector()
                .SetTerms(mv1.IndexScalarDictionary)
                .SubtractTerms(mv2.IndexScalarDictionary)
                .RemoveZeroTerms()
                .GetBivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, IGaStorageMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 when mv2 is IGaStorageScalar<T> s2 => 
                    Subtract(scalarProcessor, s1, s2),

                IGaStorageVector<T> v1 when mv2 is IGaStorageVector<T> v2 => 
                    Subtract(scalarProcessor, v1, v2),

                IGaStorageBivector<T> bv1 when mv2 is IGaStorageBivector<T> bv2 => 
                    Subtract(scalarProcessor, bv1, bv2),

                IGaStorageMultivectorGraded<T> gmv1 when mv2 is IGaStorageMultivectorGraded<T> gmv2 =>
                    SubtractAsIGaMultivectorGradedStorage(scalarProcessor, gmv1, gmv2),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(scalarProcessor, mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => 
                    Subtract(scalarProcessor, s1, scalar2),

                IGaStorageMultivectorGraded<T> gmv1 =>
                    SubtractAsIGaMultivectorGradedStorage(scalarProcessor, gmv1, scalar2),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(scalarProcessor, mv1, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Subtract<T>(this IGaScalarProcessor<T> scalarProcessor, T scalar2, IGaStorageMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGaStorageScalar<T> s1 => 
                    Subtract(scalarProcessor, scalar2, s1),

                IGaStorageMultivectorGraded<T> gmv1 =>
                    SubtractAsIGaMultivectorGradedStorage(scalarProcessor, scalar2, gmv1),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(scalarProcessor, scalar2, mv1)
            };
        }
    }
}