using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorSubtractUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv, T scalar2)
        {
            var scalar = mv.TryGetScalar(out var scalar1)
                ? scalarProcessor.Subtract(scalar1, scalar2)
                : scalarProcessor.Negative(scalar2);

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IGaScalarStorage<T> mv)
        {
            var scalar = mv.TryGetScalar(out var scalar2)
                ? scalarProcessor.Subtract(scalar1, scalar2)
                : scalar1;

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            if (mv1.TryGetScalar(out var scalar1))
                return mv2.TryGetScalar(out var scalar2)
                    ? scalarProcessor.Subtract(scalar1, scalar2).CreateStorageScalar()
                    : mv1;

            return mv2.IsEmpty()
                ? GaScalarStorage<T>.ZeroScalar
                : scalarProcessor.Negative(mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            return scalarProcessor.Subtract(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv1, IGaBivectorStorage<T> mv2)
        {
            return scalarProcessor.Subtract(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateBivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return scalarProcessor.Subtract(
                mv1.Grade,
                mv1.IndexScalarList,
                mv2.Grade,
                mv2.IndexScalarList
            ).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 when mv2 is IGaScalarStorage<T> s2 => 
                    Subtract(scalarProcessor, s1, s2),

                IGaVectorStorage<T> v1 when mv2 is IGaVectorStorage<T> v2 => 
                    Subtract(scalarProcessor, v1, v2),

                IGaBivectorStorage<T> bv1 when mv2 is IGaBivectorStorage<T> bv2 => 
                    Subtract(scalarProcessor, bv1, bv2),

                IGaKVectorStorage<T> kv1 when mv2 is IGaKVectorStorage<T> kv2 => 
                    Subtract(scalarProcessor, kv1, kv2),

                IGaMultivectorGradedStorage<T> gmv1 when mv2 is IGaMultivectorGradedStorage<T> gmv2 =>
                    scalarProcessor.Subtract(
                        gmv1.GradeIndexScalarList, 
                        gmv2.GradeIndexScalarList
                    ).CreateStorageGradedMultivector(),

                _ => scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .SubtractTerms(mv2.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => 
                    Subtract(scalarProcessor, s1, scalar2),

                IGaMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerms(gmv1.GradeIndexScalarList)
                        .SubtractTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .SubtractTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar2, IGaMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => 
                    Subtract(scalarProcessor, scalar2, s1),

                IGaMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .SubtractTerms(gmv1.GradeIndexScalarList)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .SubtractTerms(mv1.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }
    }
}