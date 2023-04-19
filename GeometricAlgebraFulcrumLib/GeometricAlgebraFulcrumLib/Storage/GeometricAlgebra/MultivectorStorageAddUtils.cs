using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    internal static class MultivectorStorageAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params VectorStorage<T>[] mvList)
        {
            return mvList.Aggregate(
                scalarProcessor.CreateVectorStorageZero(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<VectorStorage<T>> mvList)
        {
            return mvList.Aggregate(
                scalarProcessor.CreateVectorStorageZero(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv1, BivectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateBivectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params BivectorStorage<T>[] mvList)
        {
            return mvList.Aggregate(
                scalarProcessor.CreateBivectorZeroStorage(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<BivectorStorage<T>> mvList)
        {
            return mvList.Aggregate(
                scalarProcessor.CreateBivectorZeroStorage(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
        {
            if (mv1.Grade == mv2.Grade)
            {
                return scalarProcessor.Add(
                    mv1.GetLinVectorIndexScalarStorage(),
                    mv2.GetLinVectorIndexScalarStorage()
                ).CreateKVectorStorage(mv1.Grade);
            }

            return scalarProcessor.Add(
                mv1.Grade,
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.Grade,
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateMultivectorStorageGraded();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            //TODO: Make all binary operations like this
            return (mv1, mv2) switch
            {
                (VectorStorage<T> v1, VectorStorage<T> v2) =>
                    scalarProcessor.Add(v1, v2),

                (BivectorStorage<T> v1, BivectorStorage<T> v2) =>
                    scalarProcessor.Add(v1, v2),

                (KVectorStorage<T> kv1, KVectorStorage<T> kv2) =>
                    scalarProcessor.Add(kv1, kv2),

                (IMultivectorGradedStorage<T> v1, IMultivectorGradedStorage<T> v2) =>
                    scalarProcessor.Add(
                        v1.GetLinVectorGradedStorage(),
                        v2.GetLinVectorGradedStorage()
                    ).CreateMultivectorStorageGraded(),

                _ => scalarProcessor.CreateVectorStorageComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerms(mv2.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateMultivectorStorageSparse()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params IMultivectorStorage<T>[] mvList)
        {
            return mvList.Aggregate(
                (IMultivectorStorage<T>)scalarProcessor.CreateMultivectorStorageSparseZero(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<IMultivectorStorage<T>> mvList)
        {
            return mvList.Aggregate(
                (IMultivectorStorage<T>)scalarProcessor.CreateMultivectorStorageSparseZero(),
                scalarProcessor.Add
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateMultivectorGradedStorageComposer()
                        .SetTerms(gmv1.GetLinVectorGradedStorage())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateMultivectorStorageSparse(),

                _ =>
                    scalarProcessor.CreateVectorStorageComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateMultivectorStorageSparse()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar2, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateMultivectorGradedStorageComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(gmv1.GetLinVectorGradedStorage())
                        .RemoveZeroTerms()
                        .CreateMultivectorStorageSparse(),

                _ =>
                    scalarProcessor.CreateVectorStorageComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(mv1.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateMultivectorStorageSparse()
            };
        }
    }
}