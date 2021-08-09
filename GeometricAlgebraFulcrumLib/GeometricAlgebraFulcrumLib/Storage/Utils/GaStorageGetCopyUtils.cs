using System;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageGetCopyUtils
    {
        public static IGaStorageVector<T> GetStorageCopy<T>(this IGaStorageVector<T> mv)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T> GetStorageCopy<T>(this IGaStorageBivector<T> mv)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageKVector<T> GetStorageCopy<T>(this IGaStorageKVector<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T> GetStorageCopy<T>(this IGaStorageMultivectorSparse<T> mv)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorGraded<T> GetStorageCopy<T>(this IGaStorageMultivectorGraded<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T> GetStorageCopy<T>(this IGaStorageMultivector<T> mv)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageBivector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageKVector<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.GetStorageCopy(),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.GetStorageCopy(),

                _ => throw new InvalidOperationException()
            };
        }
    }
}