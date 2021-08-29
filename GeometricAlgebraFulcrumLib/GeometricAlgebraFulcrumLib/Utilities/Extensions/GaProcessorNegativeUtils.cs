using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaProcessorNegativeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaScalarStorage<T> mv)
        {
            return mv.MapScalar(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv)
        {
            return mv.MapVectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv)
        {
            return mv.MapBivectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv)
        {
            return mv.MapKVectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv)
        {
            return mv.MapGradedMultivectorScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv)
        {
            return mv.MapSparseMultivectorScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaScalarStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(0)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaBivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(2)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? mv.MapVectorScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaBivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(2)
                ? mv.MapBivectorScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaKVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(mv.Grade)
                ? mv.MapKVectorScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaKVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(mv.Grade)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapGradedMultivectorScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaMultivectorSparseStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, GaMultivectorSparseStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapSparseMultivectorScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorSparseStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorSparseStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaMultivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
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