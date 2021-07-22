using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Composers;

namespace GeometricAlgebraFulcrumLib.Processing
{
    public static class GaProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Add<T>(this IGasScalar<T> mv, T scalar2)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateScalar(
                scalarProcessor.Add(mv.Scalar, scalar2)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Add<T>(this T scalar2, IGasScalar<T> mv)
        {
            var scalarProcessor = mv.ScalarProcessor;

            return scalarProcessor.CreateScalar(
                scalarProcessor.Add(scalar2, mv.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasScalar<T> Add<T>(this IGasScalar<T> mv1, IGasScalar<T> mv2)
        {
            var scalarProcessor = mv1.ScalarProcessor;

            return scalarProcessor.CreateScalar(
                scalarProcessor.Add(mv1.Scalar, mv2.Scalar)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasVector<T> Add<T>(this IGasVector<T> mv1, IGasVector<T> mv2)
        {
            var composer = new GaKVectorStorageComposer<T>(
                mv1.ScalarProcessor, 
                1
            );

            composer.SetTerms(mv1.GetIndexScalarPairs());

            composer.AddTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasBivector<T> Add<T>(this IGasBivector<T> mv1, IGasBivector<T> mv2)
        {
            var composer = new GaBivectorStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIndexScalarPairs());

            composer.AddTerms(mv2.GetIndexScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> AddAsIGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv1, T scalar2)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.SetKVector(grade, indexScalarDictionary);

            composer.AddTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> AddAsIGaMultivectorGradedStorage<T>(this T scalar2, IGasGradedMultivector<T> mv1)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerm(0, scalar2);

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.AddKVector(grade, indexScalarDictionary);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasGradedMultivector<T> AddAsIGaMultivectorGradedStorage<T>(this IGasGradedMultivector<T> mv1, IGasGradedMultivector<T> mv2)
        {
            var composer = new GaMultivectorGradedStorageComposer<T>(
                mv1.ScalarProcessor
            );

            foreach (var (grade, indexScalarDictionary) in mv1.GetGradeIndexScalarDictionary())
                composer.SetKVector(grade, indexScalarDictionary);

            foreach (var (grade, indexScalarDictionary) in mv2.GetGradeIndexScalarDictionary())
                composer.AddKVector(grade, indexScalarDictionary);

            composer.RemoveZeroTerms();

            return composer.GetCompactGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> AddAsIGaMultivectorTermsStorage<T>(this IGasMultivector<T> mv1, T scalar2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIdScalarPairs());

            composer.AddTerm(0, scalar2);

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> AddAsIGaMultivectorTermsStorage<T>(this T scalar2, IGasMultivector<T> mv1)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerm(0, scalar2);

            composer.AddTerms(mv1.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IGasTermsMultivector<T> AddAsIGaMultivectorTermsStorage<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            var composer = new GaMultivectorTermsStorageComposer<T>(
                mv1.ScalarProcessor
            );

            composer.SetTerms(mv1.GetIdScalarPairs());

            composer.AddTerms(mv2.GetIdScalarPairs());

            composer.RemoveZeroTerms();

            return composer.GetCompactTermsStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Add<T>(this IGasMultivector<T> mv1, IGasMultivector<T> mv2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 when mv2 is IGasScalar<T> s2 => 
                    Add(s1, s2),

                IGasVector<T> v1 when mv2 is IGasVector<T> v2 => 
                    Add(v1, v2),

                IGasBivector<T> bv1 when mv2 is IGasBivector<T> bv2 => 
                    Add(bv1, bv2),

                IGasGradedMultivector<T> gmv1 when mv2 is IGasGradedMultivector<T> gmv2 =>
                    AddAsIGaMultivectorGradedStorage(gmv1, gmv2),

                _ => 
                    AddAsIGaMultivectorTermsStorage(mv1, mv2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Add<T>(this IGasMultivector<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    Add(s1, scalar2),

                IGasGradedMultivector<T> gmv1 =>
                    AddAsIGaMultivectorGradedStorage(gmv1, scalar2),

                _ => 
                    AddAsIGaMultivectorTermsStorage(mv1, scalar2)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGasMultivector<T> Add<T>(this T scalar2, IGasMultivector<T> mv1)
        {
            return mv1 switch
            {
                IGasScalar<T> s1 => 
                    Add(scalar2, s1),

                IGasGradedMultivector<T> gmv1 =>
                    AddAsIGaMultivectorGradedStorage(scalar2, gmv1),

                _ => 
                    AddAsIGaMultivectorTermsStorage(scalar2, mv1)
            };
        }
    }
}