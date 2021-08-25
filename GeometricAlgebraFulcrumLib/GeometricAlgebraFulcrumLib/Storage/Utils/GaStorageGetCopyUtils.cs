using System;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageGetCopyUtils
    {
        public static IGaStorageVector<T> GetStorageCopy<T>(this IGaStorageVector<T> mv)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.GetVectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T> GetStorageCopy<T>(this IGaStorageBivector<T> mv)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.GetBivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageKVector<T> GetStorageCopy<T>(this IGaStorageKVector<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetKVectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T> GetStorageCopy<T>(this IGaStorageMultivectorSparse<T> mv)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.GetSparseMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorGraded<T> GetStorageCopy<T>(this IGaStorageMultivectorGraded<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetKVectorCopy(),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.GetGradedMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T> GetStorageCopy<T>(this IGaStorageMultivector<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetScalarCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetVectorCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetBivectorCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetKVectorCopy(),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.GetGradedMultivectorCopy(),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.GetSparseMultivectorCopy(),

                _ => throw new InvalidOperationException()
            };
        }
    }
}