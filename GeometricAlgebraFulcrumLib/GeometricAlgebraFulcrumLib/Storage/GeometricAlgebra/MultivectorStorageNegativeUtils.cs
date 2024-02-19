using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

internal static class MultivectorStorageNegativeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv)
    {
        return mv.MapVectorScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv)
    {
        return mv.MapBivectorScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv)
    {
        return mv.MapGradedMultivectorScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KVectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv)
    {
        return mv.MapKVectorScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv)
    {
        return mv.MapScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv)
    {
        return mv.MapMultivectorScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv)
    {
        return mv.MapScalars(scalarProcessor.Negative);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return gradeToNegativePredicate(1)
            ? mv.MapVectorScalars(scalarProcessor.Negative)
            : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return mv.MapVectorScalarsByIndex(
            (index, scalar) =>
                gradeIndexToNegativePredicate(1, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, VectorStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapVectorScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BivectorStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return gradeToNegativePredicate(2)
            ? mv.MapBivectorScalars(scalarProcessor.Negative)
            : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BivectorStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return mv.MapBivectorScalarsByIndex(
            (index, scalar) =>
                gradeIndexToNegativePredicate(2, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static BivectorStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, BivectorStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapBivectorScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KVectorStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return gradeToNegativePredicate(mv.Grade)
            ? mv.MapKVectorScalars(scalarProcessor.Negative)
            : mv;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KVectorStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        var grade = mv.Grade;

        return mv.MapKVectorScalarsByIndex(
            (index, scalar) =>
                gradeIndexToNegativePredicate(grade, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static KVectorStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, KVectorStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapKVectorScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorGradedStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return (MultivectorGradedStorage<T>)mv.MapGradedMultivectorScalarsByGradeIndex(
            (grade, _, scalar) =>
                gradeToNegativePredicate(grade)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorGradedStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return (MultivectorGradedStorage<T>)mv.MapGradedMultivectorScalarsByGradeIndex(
            (grade, index, scalar) =>
                gradeIndexToNegativePredicate(grade, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorGradedStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, MultivectorGradedStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return (MultivectorGradedStorage<T>)mv.MapGradedMultivectorScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return mv.MapScalarsByGradeIndex(
            (grade, _, scalar) =>
                gradeToNegativePredicate(grade)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return mv.MapScalarsByGradeIndex(
            (grade, index, scalar) =>
                gradeIndexToNegativePredicate(grade, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorGradedStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorGradedStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return mv.MapMultivectorScalarsByGradeIndex(
            (grade, _, scalar) =>
                gradeToNegativePredicate(grade)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return mv.MapMultivectorScalarsByGradeIndex(
            (grade, index, scalar) =>
                gradeIndexToNegativePredicate(grade, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static MultivectorStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, MultivectorStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapMultivectorScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> NegativeByGrade<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, Predicate<uint> gradeToNegativePredicate)
    {
        return mv.MapScalarsByGradeIndex(
            (grade, _, scalar) =>
                gradeToNegativePredicate(grade)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> NegativeByGradeIndex<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, Func<uint, ulong, bool> gradeIndexToNegativePredicate)
    {
        return mv.MapScalarsByGradeIndex(
            (grade, index, scalar) =>
                gradeIndexToNegativePredicate(grade, index)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IMultivectorStorage<T> NegativeById<T>(this IScalarProcessor<T> scalarProcessor, IMultivectorStorage<T> mv, Predicate<ulong> idToNegativePredicate)
    {
        return mv.MapScalarsById(
            (id, scalar) =>
                idToNegativePredicate(id)
                    ? scalarProcessor.Negative(scalar)
                    : scalar
        );
    }
}