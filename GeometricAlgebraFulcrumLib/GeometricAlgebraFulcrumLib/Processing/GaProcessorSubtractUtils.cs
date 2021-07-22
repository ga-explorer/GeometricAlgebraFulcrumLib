using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Subtract<T>(this IGasScalar<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateScalar(
                scalarProcessor.Subtract(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Subtract<T>(this IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            return scalarProcessor.CreateScalar(
                scalarProcessor.Subtract(mv1.Scalar, mv2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> Subtract<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            var composer = new GaKVectorStorageComposer<T>(
                mv1.ScalarProcessor, 
                1
            );

            composer.SetTerms(mv1.GetIndexScalarPairs());

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> Subtract<T>(this IGasBivector<T> mv1, IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIndexScalarPairs());

            composer.SubtractTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> SubtractAsIGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv1, T scalar2)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.SetKVector(grade, indexScalarDictionary);

            composer.SubtractTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> SubtractAsIGaMultivectorGradedStorage<T>(this T scalar2, IGasGradedMultivector<T> mv1)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerm(0, scalar2);

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.SubtractKVector(grade, indexScalarDictionary);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> SubtractAsIGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.SetKVector(grade, indexScalarDictionary);

            foreach (var (grade, indexScalarDictionary) in mv2.GetGradeIndexScalarDictionary())
                composer.SubtractKVector(grade, indexScalarDictionary);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> SubtractAsIGaMultivectorTermsStorage<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIdScalarPairs());

            composer.SubtractTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> SubtractAsIGaMultivectorTermsStorage<T>(this IGasMultivector<T> mv1, T scalar2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIdScalarPairs());

            composer.SubtractTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> SubtractAsIGaMultivectorTermsStorage<T>(this T scalar2, IGasMultivector<T> mv1)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerm(0, scalar2);

            composer.SubtractTerms(mv1.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Subtract<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Subtract(s1, s2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    Subtract(v1, v2),

                IGasBivector<T> bv1 when mv2 is IGasBivector<T> bv2 => 
                    Subtract(bv1, bv2),

                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 =>
                    SubtractAsIGaMultivectorGradedStorage(gmv1, gmv2),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Subtract<T>(this IGasMultivector<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    Subtract(s1, scalar2),

                IGasGradedMultivector<T> gmv1 =>
                    SubtractAsIGaMultivectorGradedStorage(gmv1, scalar2),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(mv1, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Subtract<T>(this T scalar2, IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    Subtract(scalar2, s1),

                IGasGradedMultivector<T> gmv1 =>
                    SubtractAsIGaMultivectorGradedStorage(scalar2, gmv1),

                _ => 
                    SubtractAsIGaMultivectorTermsStorage(scalar2, mv1)
            };
        }
    }
}