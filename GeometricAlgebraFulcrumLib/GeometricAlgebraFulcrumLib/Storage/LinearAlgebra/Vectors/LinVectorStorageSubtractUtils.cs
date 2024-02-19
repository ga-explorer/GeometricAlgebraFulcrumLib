﻿using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

public static class LinVectorStorageSubtractUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
    {
        return vector.MapScalars(value => scalarProcessor.Subtract(value, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
    {
        return vector.MapScalars(value => scalarProcessor.Subtract(scalar, value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorDenseStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2)
    {
        return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Subtract);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2)
    {
        return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Subtract);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, uint grade1, ILinVectorStorage<T> v1, uint grade2, ILinVectorStorage<T> v2)
    {
        return grade1 == grade2
            ? new LinVectorSingleGradeStorage<T>(
                grade1,
                scalarProcessor.Subtract(v1, v2)
            )
            : new LinVectorSparseGradedStorage<T>()
                .AddList(grade1, v1)
                .AddList(grade2, scalarProcessor.Negative(v2))
                .GetCompactStorage();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorGradedStorage<T> Subtract<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> v1, ILinVectorGradedStorage<T> v2)
    {
        if (v1.IsEmpty())
            return v2.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : scalarProcessor.Negative(v2);

        return scalarProcessor
            .CreateVectorGradedStorageComposer()
            .SetTerms(v1)
            .SubtractTerms(v2)
            .RemoveZeroTerms()
            .CreateLinVectorGradedStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexScalarRecord<T>> Subtract<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<RGaKvIndexScalarRecord<T>> v1, IEnumerable<RGaKvIndexScalarRecord<T>> v2)
    {
        return scalarProcessor
            .CreateVectorStorageComposer()
            .AddTerms(v1)
            .SubtractTerms(v2)
            .RemoveZeroTerms()
            .GetIndexScalarRecords();
    }
}