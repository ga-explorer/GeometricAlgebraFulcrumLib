using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

internal static class MultivectorStorageSubtractUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
    {
        return scalarProcessor.Subtract(
            mv1.GetLinVectorIndexScalarStorage(),
            mv2.GetLinVectorIndexScalarStorage()
        ).CreateVectorStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv1, BivectorStorage<T> mv2)
    {
        return scalarProcessor.Subtract(
            mv1.GetLinVectorIndexScalarStorage(),
            mv2.GetLinVectorIndexScalarStorage()
        ).CreateBivectorStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
    {
        if (mv1.Grade == mv2.Grade)
        {
            return scalarProcessor.Subtract(
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateKVectorStorage(mv1.Grade);
        }

        return scalarProcessor.Subtract(
            mv1.Grade,
            mv1.GetLinVectorIndexScalarStorage(),
            mv2.Grade,
            mv2.GetLinVectorIndexScalarStorage()
        ).CreateMultivectorStorageGraded();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
    {
        return mv1 switch
        {
            VectorStorage<T> v1 when mv2 is VectorStorage<T> v2 =>
                scalarProcessor.Subtract(v1, v2),

            BivectorStorage<T> bv1 when mv2 is BivectorStorage<T> bv2 =>
                scalarProcessor.Subtract(bv1, bv2),

            KVectorStorage<T> kv1 when mv2 is KVectorStorage<T> kv2 =>
                scalarProcessor.Subtract(kv1, kv2),

            IMultivectorGradedStorage<T> gmv1 when mv2 is IMultivectorGradedStorage<T> gmv2 =>
                scalarProcessor.Subtract(
                    gmv1.GetLinVectorGradedStorage(),
                    gmv2.GetLinVectorGradedStorage()
                ).CreateMultivectorStorageGraded(),

            _ => scalarProcessor.CreateVectorStorageComposer()
                .SetTerms(mv1.GetIdScalarRecords())
                .SubtractTerms(mv2.GetIdScalarRecords())
                .RemoveZeroTerms()
                .CreateMultivectorStorageSparse()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, T scalar2)
    {
        return mv1 switch
        {
            IMultivectorGradedStorage<T> gmv1 =>
                scalarProcessor
                    .CreateMultivectorGradedStorageComposer()
                    .SetTerms(gmv1.GetLinVectorGradedStorage())
                    .SubtractTerm(0, scalar2)
                    .RemoveZeroTerms()
                    .CreateMultivectorStorageSparse(),

            _ =>
                scalarProcessor.CreateVectorStorageComposer()
                    .SetTerms(mv1.GetIdScalarRecords())
                    .SubtractTerm(0, scalar2)
                    .RemoveZeroTerms()
                    .CreateMultivectorStorageSparse()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar2, IMultivectorStorage<T> mv1)
    {
        return mv1 switch
        {
            IMultivectorGradedStorage<T> gmv1 =>
                scalarProcessor
                    .CreateMultivectorGradedStorageComposer()
                    .SetTerm(0, scalar2)
                    .SubtractTerms(gmv1.GetLinVectorGradedStorage())
                    .RemoveZeroTerms()
                    .CreateMultivectorStorageSparse(),

            _ =>
                scalarProcessor.CreateVectorStorageComposer()
                    .SetTerm(0, scalar2)
                    .SubtractTerms(mv1.GetIdScalarRecords())
                    .RemoveZeroTerms()
                    .CreateMultivectorStorageSparse()
        };
    }
}