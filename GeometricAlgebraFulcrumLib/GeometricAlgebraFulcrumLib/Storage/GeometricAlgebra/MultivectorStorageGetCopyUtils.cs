using System;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

public static class MultivectorStorageGetCopyUtils
{
    public static MultivectorStorage<T> GetStorageCopy<T>(this MultivectorStorage<T> mv)
    {
        return mv switch
        {
            MultivectorStorage<T> mv1 =>
                mv1.GetMultivectorCopy(),

            _ => throw new InvalidOperationException()
        };
    }

    public static IMultivectorGradedStorage<T> GetStorageCopy<T>(this IMultivectorGradedStorage<T> mv)
    {
        return mv switch
        {
            VectorStorage<T> mv1 =>
                mv1.GetVectorCopy(),

            BivectorStorage<T> mv1 =>
                mv1.GetBivectorCopy(),

            KVectorStorage<T> mv1 =>
                mv1.GetKVectorCopy(),

            MultivectorGradedStorage<T> mv1 =>
                mv1.GetGradedMultivectorCopy(),

            _ => throw new InvalidOperationException()
        };
    }

    public static IMultivectorStorage<T> GetStorageCopy<T>(this IMultivectorStorage<T> mv)
    {
        return mv switch
        {
            VectorStorage<T> mv1 =>
                mv1.GetVectorCopy(),

            BivectorStorage<T> mv1 =>
                mv1.GetBivectorCopy(),

            KVectorStorage<T> mv1 =>
                mv1.GetKVectorCopy(),

            MultivectorGradedStorage<T> mv1 =>
                mv1.GetGradedMultivectorCopy(),

            MultivectorStorage<T> mv1 =>
                mv1.GetMultivectorCopy(),

            _ => throw new InvalidOperationException()
        };
    }
}