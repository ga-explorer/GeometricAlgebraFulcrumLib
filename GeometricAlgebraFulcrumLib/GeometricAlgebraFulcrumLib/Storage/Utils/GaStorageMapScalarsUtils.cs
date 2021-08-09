using System;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageMapScalarsUtils
    {
        public static IGaStorageScalar<T2> MapScalars<T, T2>(this IGaStorageScalar<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalars<T, T2>(this IGaStorageVector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalars<T, T2>(this IGaStorageBivector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageKVector<T2> MapScalars<T, T2>(this IGaStorageKVector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalars<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalars<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalars<T, T2>(this IGaStorageMultivector<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsById<T, T2>(this IGaStorageKVector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsById<T, T2>(this IGaStorageVector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsById<T, T2>(this IGaStorageBivector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsById<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsById<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsById<T, T2>(this IGaStorageMultivector<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsByIndex<T, T2>(this IGaStorageKVector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsByIndex<T, T2>(this IGaStorageVector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsByIndex<T, T2>(this IGaStorageBivector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsByIndex<T, T2>(this IGaStorageMultivector<T> mv, Func<ulong, T, T2> indexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsByIndex(indexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IGaStorageKVector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageKVector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageVector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageVector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageBivector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageBivector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
        
        public static IGaStorageMultivectorGraded<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivectorGraded<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivectorSparse<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivectorSparse<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IGaStorageMultivector<T2> MapScalarsByGradeIndex<T, T2>(this IGaStorageMultivector<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                GaStorageScalar<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageBivector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageKVector<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorGraded<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                GaStorageMultivectorSparse<T> mv1 => 
                    mv1.MapScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
    }
}