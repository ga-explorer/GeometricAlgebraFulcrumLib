using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

public static class LinVectorStorageAddUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, T scalar)
    {
        return vector.MapScalars(value => scalarProcessor.Add(value, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinVectorStorage<T> vector)
    {
        return vector.MapScalars(value => scalarProcessor.Add(scalar, value));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorDenseStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorDenseStorage<T> v1, ILinVectorDenseStorage<T> v2)
    {
        return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> v1, ILinVectorStorage<T> v2)
    {
        return scalarProcessor.MapScalarsIndicesUnion(v1, v2, scalarProcessor.Add);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, params ILinVectorStorage<T>[] vectorsList)
    {
        return vectorsList.Aggregate(
            (ILinVectorStorage<T>)LinVectorEmptyStorage<T>.EmptyStorage,
            scalarProcessor.Add
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, IEnumerable<ILinVectorStorage<T>> vectorsList)
    {
        return vectorsList.Aggregate(
            (ILinVectorStorage<T>)LinVectorEmptyStorage<T>.EmptyStorage,
            scalarProcessor.Add
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorGradedStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, uint grade1, ILinVectorStorage<T> v1, uint grade2, ILinVectorStorage<T> v2)
    {
        return grade1 == grade2
            ? new LinVectorSingleGradeStorage<T>(
                grade1,
                scalarProcessor.Add(v1, v2)
            )
            : new LinVectorSparseGradedStorage<T>()
                .AddList(grade1, v1)
                .AddList(grade2, v2)
                .GetCompactStorage();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinVectorGradedStorage<T> Add<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> v1, ILinVectorGradedStorage<T> v2)
    {
        if (v1.IsEmpty())
            return v2.IsEmpty()
                ? LinVectorEmptyGradedStorage<T>.EmptyStorage
                : v2;

        return scalarProcessor
            .CreateVectorGradedStorageComposer()
            .SetTerms(v1)
            .AddTerms(v2)
            .RemoveZeroTerms()
            .CreateLinVectorGradedStorage();
    }
}