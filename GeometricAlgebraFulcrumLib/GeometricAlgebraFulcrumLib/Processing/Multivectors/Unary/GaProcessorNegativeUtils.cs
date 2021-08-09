using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage;
using GeometricAlgebraFulcrumLib.Storage.Utils;

namespace GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary
{
    public static class GaProcessorNegativeUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageScalar<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageScalar<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageKVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.MapScalars(scalarProcessor.Negative);
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageScalar<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(0)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageVector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageBivector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(2)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageVector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(1)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageBivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageBivector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(2)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageKVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageKVector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(mv.Grade)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }
 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageKVector<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return gradeToNegativePredicate(mv.Grade)
                ? mv.MapScalars(scalarProcessor.Negative)
                : mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorGraded<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorGraded<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorGraded<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GaStorageMultivectorSparse<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, GaStorageMultivectorSparse<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivectorSparse<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivectorSparse<T> mv, Predicate<uint> gradeToNegativePredicate)
        {
            return mv.MapScalarsByGradeIndex(
                (grade, _, scalar) =>
                    gradeToNegativePredicate(grade) 
                        ? scalarProcessor.Negative(scalar) 
                        : scalar
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageMultivector<T> Negative<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, Predicate<uint> gradeToNegativePredicate)
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