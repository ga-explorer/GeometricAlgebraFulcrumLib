using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Multivectors.Unary;
using GeometricAlgebraFulcrumLib.Processing.Scalars;
using GeometricAlgebraFulcrumLib.Storage.Factories;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Storage.Utils
{
    public static class GaStorageGetPartUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> GetScalarPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.TryGetTermScalar(0UL, out var scalar)
                ? scalarProcessor.CreateStorageScalar(scalar)
                : scalarProcessor.CreateStorageZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> GetScalarPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, Func<T, T> scalarMapping)
        {
            var scalar = mv.TryGetTermScalar(0UL, out var value)
                ? value : scalarProcessor.GetZeroScalar();

            return scalarProcessor.CreateStorageScalar(scalarMapping(scalar));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GetVectorPart<T>(this IGaStorageMultivector<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalars(scalarMapping)
                : GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GetVectorPart<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalarsByIndex(indexScalarMapping)
                : GaStorageVector<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GetVectorPart<T>(this IGaStorageMultivector<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapScalarsByGradeIndex(gradeIndexScalarMapping)
                : GaStorageVector<T>.ZeroVector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GetBivectorPart<T>(this IGaStorageMultivector<T> mv)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector
                : GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GetBivectorPart<T>(this IGaStorageMultivector<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalars(scalarMapping)
                : GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GetBivectorPart<T>(this IGaStorageMultivector<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalarsByIndex(indexScalarMapping)
                : GaStorageBivector<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GetBivectorPart<T>(this IGaStorageMultivector<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapScalarsByGradeIndex(gradeIndexScalarMapping)
                : GaStorageBivector<T>.ZeroBivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GetKVectorPart<T>(this IGaStorageMultivector<T> mv, uint grade)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar : GaStorageKVector<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector : GaStorageVector<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector : GaStorageBivector<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector : GaStorageKVector<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GetKVectorPart<T>(this IGaStorageMultivector<T> mv, uint grade, Func<T, T> scalarMapping)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar.MapScalars(scalarMapping) : GaStorageKVector<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapScalars(scalarMapping) : GaStorageVector<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapScalars(scalarMapping) : GaStorageBivector<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapScalars(scalarMapping) : GaStorageKVector<T>.ZeroKVector(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GetKVectorPart<T>(this IGaStorageMultivector<T> mv, uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade switch
            {
                0 => mv.TryGetScalarPart(out var scalar) ? scalar.MapScalarsByIndex(indexScalarMapping) : GaStorageKVector<T>.ZeroKVector(0),
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapScalarsByIndex(indexScalarMapping) : GaStorageVector<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapScalarsByIndex(indexScalarMapping) : GaStorageBivector<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapScalarsByIndex(indexScalarMapping) : GaStorageKVector<T>.ZeroKVector(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageScalar<T> GetNegativeScalarPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return scalarProcessor.GetScalarPart(
                mv,
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageVector<T> GetNegativeVectorPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetVectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageBivector<T> GetNegativeBivectorPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv)
        {
            return mv.GetBivectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IGaStorageKVector<T> GetNegativeKVectorPart<T>(this IGaScalarProcessor<T> scalarProcessor, IGaStorageMultivector<T> mv, uint grade)
        {
            return mv.GetKVectorPart(
                grade,
                scalarProcessor.Negative
            );
        }
    }
}