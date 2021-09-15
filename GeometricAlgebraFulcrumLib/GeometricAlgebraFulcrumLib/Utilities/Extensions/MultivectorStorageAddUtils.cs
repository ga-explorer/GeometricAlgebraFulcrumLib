using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageAddUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv1, VectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateVectorStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv1, BivectorStorage<T> mv2)
        {
            return scalarProcessor.Add(
                mv1.GetLinVectorIndexScalarStorage(),
                mv2.GetLinVectorIndexScalarStorage()
            ).CreateBivectorStorage();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv1, KVectorStorage<T> mv2)
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
            ).CreateMultivectorGradedStorage();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, IMultivectorStorage<T> mv2)
        {
            //TODO: Make all binary operations like this
            return (mv1, mv2) switch
            {
                (VectorStorage<T> v1, VectorStorage<T> v2) => 
                    Add(scalarProcessor, v1, v2),

                (BivectorStorage<T> v1, BivectorStorage<T> v2) => 
                    Add(scalarProcessor, v1, v2),

                (KVectorStorage<T> kv1, KVectorStorage<T> kv2) => 
                    Add(scalarProcessor, kv1, kv2),

                (IMultivectorGradedStorage<T> v1, IMultivectorGradedStorage<T> v2) =>
                    scalarProcessor.Add(
                        v1.GetLinVectorGradedStorage(), 
                        v2.GetLinVectorGradedStorage()
                    ).CreateMultivectorGradedStorage(),

                _ => scalarProcessor.CreateVectorStorageComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerms(mv2.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv1, T scalar2)
        {
            return mv1 switch
            {
                IMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateMultivectorGradedStorageComposer()
                        .SetTerms(gmv1.GetLinVectorGradedStorage())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateMultivectorSparseStorage(),

                _ => 
                    scalarProcessor.CreateVectorStorageComposer()
                        .SetTerms(mv1.GetIdScalarRecords())
                        .AddTerm(0, scalar2)
                        .RemoveZeroTerms()
                        .CreateMultivectorSparseStorage()
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Add<T>(this IScalarAlgebraProcessor<T> scalarProcessor, T scalar2, IMultivectorStorage<T> mv1)
        {
            return mv1 switch
            {
                IMultivectorGradedStorage<T> gmv1 =>
                    scalarProcessor
                        .CreateMultivectorGradedStorageComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(gmv1.GetLinVectorGradedStorage())
                        .RemoveZeroTerms()
                        .CreateMultivectorSparseStorage(),

                _ => 
                    scalarProcessor.CreateVectorStorageComposer()
                        .SetTerm(0, scalar2)
                        .AddTerms(mv1.GetIdScalarRecords())
                        .RemoveZeroTerms()
                        .CreateMultivectorSparseStorage()
            };
        }
    }
}