using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class MultivectorStorageNegativeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv)
        {
            return mv.MapVectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv)
        {
            return mv.MapBivectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
        {
            return mv.MapGradedMultivectorScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv)
        {
            return mv.MapKVectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
        {
            return mv.MapMultivectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, VectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? mv.MapVectorScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, BivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(2)
                ? mv.MapBivectorScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, KVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(mv.Grade)
                ? mv.MapKVectorScalars(scalarProcessor.Negative)
                : mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorGradedStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return (MultivectorGradedStorage<T>) mv.MapGradedMultivectorScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorGradedStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MultivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapMultivectorScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IMultivectorStorage<T> Negative<T>(this IScalarAlgebraProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }
    }
}