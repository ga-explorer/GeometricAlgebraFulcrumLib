using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra
{
    public static class MultivectorStorageGetPartUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GetVectorPart<T>(this IMultivectorStorage<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapVectorScalars(scalarMapping)
                : VectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GetVectorPart<T>(this IMultivectorStorage<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapVectorScalarsByIndex(indexScalarMapping)
                : VectorStorage<T>.ZeroVector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GetVectorPart<T>(this IMultivectorStorage<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetVectorPart(out var vector)
                ? vector.MapVectorScalarsByGradeIndex(gradeIndexScalarMapping)
                : VectorStorage<T>.ZeroVector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GetBivectorPart<T>(this IMultivectorStorage<T> mv)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector
                : BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GetBivectorPart<T>(this IMultivectorStorage<T> mv, Func<T, T> scalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapBivectorScalars(scalarMapping)
                : BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GetBivectorPart<T>(this IMultivectorStorage<T> mv, Func<ulong, T, T> indexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapBivectorScalarsByIndex(indexScalarMapping)
                : BivectorStorage<T>.ZeroBivector;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GetBivectorPart<T>(this IMultivectorStorage<T> mv, Func<uint, ulong, T, T> gradeIndexScalarMapping)
        {
            return mv.TryGetBivectorPart(out var bivector)
                ? bivector.MapBivectorScalarsByGradeIndex(gradeIndexScalarMapping)
                : BivectorStorage<T>.ZeroBivector;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> GetKVectorPart<T>(this IMultivectorStorage<T> mv, uint grade)
        {
            return grade switch
            {
                1 => mv.TryGetVectorPart(out var vector) ? vector : VectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector : BivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector : KVectorStorage<T>.CreateKVectorZero(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> GetKVectorPart<T>(this IMultivectorStorage<T> mv, uint grade, Func<T, T> scalarMapping)
        {
            return grade switch
            {
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapVectorScalars(scalarMapping) : VectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapBivectorScalars(scalarMapping) : BivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapKVectorScalars(scalarMapping) : KVectorStorage<T>.CreateKVectorZero(grade)
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> GetKVectorPart<T>(this IMultivectorStorage<T> mv, uint grade, Func<ulong, T, T> indexScalarMapping)
        {
            return grade switch
            {
                1 => mv.TryGetVectorPart(out var vector) ? vector.MapVectorScalarsByIndex(indexScalarMapping) : VectorStorage<T>.ZeroVector,
                2 => mv.TryGetBivectorPart(out var bivector) ? bivector.MapBivectorScalarsByIndex(indexScalarMapping) : BivectorStorage<T>.ZeroBivector,
                _ => mv.TryGetKVectorPart(grade, out var kVector) ? kVector.MapKVectorScalarsByIndex(indexScalarMapping) : KVectorStorage<T>.CreateKVectorZero(grade)
            };
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static VectorStorage<T> GetNegativeVectorPart<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetVectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BivectorStorage<T> GetNegativeBivectorPart<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            return mv.GetBivectorPart(
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static KVectorStorage<T> GetNegativeKVectorPart<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, uint grade)
        {
            return mv.GetKVectorPart(
                grade,
                scalarProcessor.Negative
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple<T, BivectorStorage<T>> GetScalarBivectorParts<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
        {
            var scalar = mv.TryGetScalar(out var s)
                ? s
                : scalarProcessor.ScalarZero;

            var bivector = mv.TryGetBivectorPart(out var bv)
                ? bv
                : KVectorStorage<T>.ZeroBivector;

            return new Tuple<T, BivectorStorage<T>>(scalar, bivector);
        }
    }
}