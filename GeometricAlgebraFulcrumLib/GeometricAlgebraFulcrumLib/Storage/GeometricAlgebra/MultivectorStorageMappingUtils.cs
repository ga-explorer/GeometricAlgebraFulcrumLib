using System;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public static class MultivectorStorageMappingUtils
    {
        public static IMultivectorGradedStorage<T2> MapScalars<T, T2>(this IMultivectorGradedStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalars(scalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalars(scalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalars(scalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IMultivectorStorage<T2> MapScalars<T, T2>(this IMultivectorStorage<T> mv, Func<T, T2> scalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalars(scalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalars(scalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalars(scalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalars(scalarMapping),

                MultivectorStorage<T> mv1 =>
                    mv1.MapMultivectorScalars(scalarMapping),

                _ => throw new InvalidOperationException()
            };
        }


        public static IMultivectorGradedStorage<T2> MapScalarsById<T, T2>(this IMultivectorGradedStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalarsById(idScalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalarsById(idScalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalarsById(idScalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IMultivectorStorage<T2> MapScalarsById<T, T2>(this IMultivectorStorage<T> mv, Func<ulong, T, T2> idScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalarsById(idScalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalarsById(idScalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalarsById(idScalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalarsById(idScalarMapping),

                MultivectorStorage<T> mv1 =>
                    mv1.MapMultivectorScalarsById(idScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        
        public static IMultivectorGradedStorage<T2> MapScalarsByGradeIndex<T, T2>(this IMultivectorGradedStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }

        public static IMultivectorStorage<T2> MapScalarsByGradeIndex<T, T2>(this IMultivectorStorage<T> mv, Func<uint, ulong, T, T2> gradeIndexScalarMapping)
        {
            return mv switch
            {
                VectorStorage<T> mv1 =>
                    mv1.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                BivectorStorage<T> mv1 =>
                    mv1.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                KVectorStorage<T> mv1 =>
                    mv1.MapKVectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorGradedStorage<T> mv1 =>
                    mv1.MapGradedMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                MultivectorStorage<T> mv1 =>
                    mv1.MapMultivectorScalarsByGradeIndex(gradeIndexScalarMapping),

                _ => throw new InvalidOperationException()
            };
        }
    }
}