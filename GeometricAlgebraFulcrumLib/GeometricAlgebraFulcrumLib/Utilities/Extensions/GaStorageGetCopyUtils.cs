using System;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaStorageGetCopyUtils
    {
        public static IGaVectorStorage<T> GetStorageCopy<T>(this IGaVectorStorage<T> mv)
        {
            return mv switch
            {
                GaVectorStorage<T> mv1 => 
                    mv1.GetVectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaBivectorStorage<T> GetStorageCopy<T>(this IGaBivectorStorage<T> mv)
        {
            return mv switch
            {
                GaBivectorStorage<T> mv1 => 
                    mv1.GetBivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaKVectorStorage<T> GetStorageCopy<T>(this IGaKVectorStorage<T> mv)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaVectorStorage<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaBivectorStorage<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaKVectorStorage<T> mv1 => 
                    mv1.GetKVectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorSparseStorage<T> GetStorageCopy<T>(this IGaMultivectorSparseStorage<T> mv)
        {
            return mv switch
            {
                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.GetSparseMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorGradedStorage<T> GetStorageCopy<T>(this IGaMultivectorGradedStorage<T> mv)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaVectorStorage<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaBivectorStorage<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaKVectorStorage<T> mv1 => 
                    mv1.GetKVectorCopy(),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.GetGradedMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaMultivectorStorage<T> GetStorageCopy<T>(this IGaMultivectorStorage<T> mv)
        {
            return mv switch
            {
                GaScalarStorage<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaVectorStorage<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaBivectorStorage<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaKVectorStorage<T> mv1 => 
                    mv1.GetKVectorCopy(),

                GaMultivectorGradedStorage<T> mv1 => 
                    mv1.GetGradedMultivectorCopy(),

                GaMultivectorSparseStorage<T> mv1 => 
                    mv1.GetSparseMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }
    }
}