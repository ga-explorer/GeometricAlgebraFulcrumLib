using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;
using GeometricAlgebraFulcrumLib.Utilities.Factories;

namespace GeometricAlgebraFulcrumLib.Utilities.Extensions
{
    public static class GaStorageGetPartUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> GetScalarPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.TryGetTermScalar(0UL, out var scalar)
                ? scalarProcessor.CreateStorageScalar(scalar)
                : scalarProcessor.CreateStorageZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> GetScalarPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, Func<T, T> scalarMapping)
        {
            var scalar = mv.TryGetTermScalar(0UL, out var value)
                ? value : scalarProcessor.ScalarZero;

            return scalarProcessor.CreateStorageScalar(scalarMapping(scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> GetVectorPart<T>(this IGaMultivectorStorage<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalars(scalarMapping)
                : GaVectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> GetVectorPart<T>(this IGaMultivectorStorage<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalarsByIndex(indexScalarMapping)
                : GaVectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> GetVectorPart<T>(this IGaMultivectorStorage<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalarsByGradeIndex(gradeIndexScalarMapping)
                : GaVectorStorage<T>.ZeroVector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> GetBivectorPart<T>(this IGaMultivectorStorage<T> mv)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector
                : GaBivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> GetBivectorPart<T>(this IGaMultivectorStorage<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalars(scalarMapping)
                : GaBivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> GetBivectorPart<T>(this IGaMultivectorStorage<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalarsByIndex(indexScalarMapping)
                : GaBivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> GetBivectorPart<T>(this IGaMultivectorStorage<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalarsByGradeIndex(gradeIndexScalarMapping)
                : GaBivectorStorage<T>.ZeroBivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> GetKVectorPart<T>(this IGaMultivectorStorage<T> mv, uint grade)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar : GaKVectorStorage<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector : GaVectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector : GaBivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector : GaKVectorStorage<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> GetKVectorPart<T>(this IGaMultivectorStorage<T> mv, uint grade, Func<T, T> scalarMapping)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar.MapScalars(scalarMapping) : GaKVectorStorage<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapScalars(scalarMapping) : GaVectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapScalars(scalarMapping) : GaBivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapScalars(scalarMapping) : GaKVectorStorage<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> GetKVectorPart<T>(this IGaMultivectorStorage<T> mv, uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar.MapScalarsByIndex(indexScalarMapping) : GaKVectorStorage<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapScalarsByIndex(indexScalarMapping) : GaVectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapScalarsByIndex(indexScalarMapping) : GaBivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapScalarsByIndex(indexScalarMapping) : GaKVectorStorage<T>.ZeroKVector(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaScalarStorage<T> GetNegativeScalarPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return scalarProcessor.GetScalarPart(
                mv,
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaVectorStorage<T> GetNegativeVectorPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetVectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaBivectorStorage<T> GetNegativeBivectorPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv)
        {
            return mv.GetBivectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaKVectorStorage<T> GetNegativeKVectorPart<T>(this IScalarProcessor<T> scalarProcessor, IGaMultivectorStorage<T> mv, uint grade)
        {
            return mv.GetKVectorPart(
                grade,
                scalarProcessor.Negative
            );
        }
    }
}