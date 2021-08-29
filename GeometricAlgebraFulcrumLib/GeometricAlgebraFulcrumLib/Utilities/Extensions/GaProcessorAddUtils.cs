using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv, T scalar2)
        {
            var scalar = mv.TryGetScalar(out var scalar1)
                ? scalarProcessor.Add(scalar1, scalar2)
                : scalar2;

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar1, IGaScalarStorage<T> mv)
        {
            var scalar = mv.TryGetScalar(out var scalar2)
                ? scalarProcessor.Add(scalar1, scalar2)
                : scalar1;

            return scalar.CreateStorageScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv1, IGaScalarStorage<T> mv2)
        {
            if (mv1.TryGetScalar(out var scalar1))
                return mv2.TryGetScalar(out var scalar2)
                    ? scalarProcessor.Add(scalar1, scalar2).CreateStorageScalar()
                    : mv1;

            return mv2.IsEmpty()
                ? GaScalarStorage<T>.ZeroScalar
                : mv2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv1, IGaVectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateGaVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv1, IGaBivectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.IndexScalarList,
                mv2.IndexScalarList
            ).CreateBivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv1, IGaKVectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.Grade,
                mv1.IndexScalarList,
                mv2.Grade,
                mv2.IndexScalarList
            ).CreateStorageGradedMultivector();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, IGaMultivectorStorage<T> mv2)
        {
            //TODO: Make all binary operations like this
            return (mv1, mv2) switch
            {
                (IGaScalarStorage<T> v1, IGaScalarStorage<T> v2) => 
                    Add(scalarProcessor, v1, v2),

                (IGaVectorStorage<T> v1, IGaVectorStorage<T> v2) => 
                    Add(scalarProcessor, v1, v2),

                (IGaBivectorStorage<T> v1, IGaBivectorStorage<T> v2) => 
                    Add(scalarProcessor, v1, v2),

                (IGaKVectorStorage<T> kv1, IGaKVectorStorage<T> kv2) => 
                    Add(scalarProcessor, kv1, kv2),

                (IGaMultivectorGradedStorage<T> v1, IGaMultivectorGradedStorage<T> v2) =>
                    scalarProcessor.Add(
                        v1.GradeIndexScalarList, 
                        v2.GradeIndexScalarList
                    ).CreateStorageGradedMultivector(),

                _ => scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerms(mv2.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => 
                    Add(scalarProcessor, s1, scalar2),

                IGaMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerms(gmv1.GradeIndexScalarList)
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar2, IGaMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IGaScalarStorage<T> s1 => 
                    Add(scalarProcessor, scalar2, s1),

                IGaMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateStorageGradedMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(gmv1.GradeIndexScalarList)
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage(),

                _ => 
                    scalarProcessor
                        .CreateStorageSparseMultivectorComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(mv1.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateGaMultivectorSparseStorage()
            };
        }
    }
}