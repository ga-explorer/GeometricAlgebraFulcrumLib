﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public static class LinMatrixStorageTimesUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.MapScalars(value => scalarProcessor.Times(scalar, value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, T scalar)
    {
        return matrixStorage.MapScalars(value => scalarProcessor.Times(value, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    {
        return scalarProcessor.MapScalarsIndicesIntersection(matrixStorage1, matrixStorage2, scalarProcessor.Times);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> TimesOuter<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector1, ILinVectorStorage<T> vector2)
    {
        return scalarProcessor.MapScalarsOuter(vector1, vector2, scalarProcessor.Times);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage, T scalar)
    {
        return matrixGradedStorage.MapScalars(s => scalarProcessor.Times(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixGradedStorage<T> Times<T>(this IScalarProcessor<T> scalarProcessor, T scalar, ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        return matrixGradedStorage.MapScalars(s => scalarProcessor.Times(scalar, s));
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinVectorStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix, ILinVectorStorage<T> vector)
    //{
    //    return matrix.CombineColumns(
    //        vector,
    //        scalarProcessor.Times,
    //        scalarProcessor.Add
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinVectorStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, ILinMatrixStorage<T> matrix)
    //{
    //    return matrix.CombineRows(
    //        vector,
    //        scalarProcessor.Times,
    //        scalarProcessor.Add
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinMatrixStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrix1, ILinMatrixStorage<T> matrix2)
    //{
    //    var columnVectorsDictionary = new Dictionary<ulong, ILinVectorStorage<T>>();

    //    foreach (var (index, vector) in matrix2.GetColumns())
    //    {
    //        var columnVector = scalarProcessor.MatrixProduct(matrix1, vector);

    //        columnVectorsDictionary.Add(index, columnVector);
    //    }

    //    return columnVectorsDictionary.CreateLinMatrixColumnsListStorage();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinVectorGradedStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix, ILinVectorGradedStorage<T> vector)
    //{
    //    return matrix.CombineColumns(
    //        vector,
    //        scalarProcessor.Times,
    //        scalarProcessor.Add
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinVectorGradedStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorGradedStorage<T> vector, ILinMatrixGradedStorage<T> matrix)
    //{
    //    return matrix.CombineRows(
    //        vector,
    //        scalarProcessor.Times,
    //        scalarProcessor.Add
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinMatrixGradedStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrix1, ILinMatrixGradedStorage<T> matrix2)
    //{
    //    return matrix1.CombineColumns(
    //        matrix2,
    //        scalarProcessor.Times,
    //        scalarProcessor.Add
    //    );
    //}

    //public static ILinMatrixDenseStorage<T> MatrixProduct<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixDenseStorage<T> matrixStorage1, ILinMatrixDenseStorage<T> matrixStorage2)
    //{
    //    if (matrixStorage1.IsEmpty() || matrixStorage2.IsEmpty())
    //        return LinMatrixEmptyStorage<T>.EmptyStorage;

    //    var count1 = matrixStorage1.Count1;
    //    var count2 = matrixStorage2.Count2;
    //    var countInner = Math.Min(matrixStorage1.Count2, matrixStorage2.Count1);
    //    var array = new T[count1, count2];
    //    var composer = scalarProcessor.CreateScalarComposer();

    //    for (var i = 0; i < count1; i++)
    //        for (var j = 0; j < count2; j++)
    //        {
    //            composer.Clear();

    //            for (var k = 0; k < countInner; k++)
    //                composer.AddScalarValue(
    //                    scalarProcessor.Times(
    //                        matrixStorage1.GetScalar((ulong)i, (ulong)k),
    //                        matrixStorage2.GetScalar((ulong)k, (ulong)j)
    //                    )
    //                );

    //            array[i, j] = composer.ScalarValue;
    //        }

    //    return array.CreateLinMatrixDenseStorage();
    //}

    //public static ILinMatrixStorage<T> MatrixProduct<T>(this IScalarAlgebraProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    //{
    //    if (matrixStorage1.IsEmpty() || matrixStorage2.IsEmpty())
    //        return LinMatrixEmptyStorage<T>.EmptyStorage;

    //    if (matrixStorage1 is ILinMatrixDenseStorage<T> g1 && matrixStorage2 is ILinMatrixDenseStorage<T> g2)
    //        return scalarProcessor.MatrixProduct(g1, g2);

    //    var composer = scalarProcessor.CreateLinMatrixSparseStorageComposer();

    //    foreach (var (key11, key12, scalar1) in matrixStorage1.GetIndexScalarRecords())
    //    {
    //        foreach (var (key21, key22, scalar2) in matrixStorage2.GetIndexScalarRecords())
    //        {
    //            if (key12 != key21) 
    //                continue;

    //            composer.AddTerm(
    //                key11, 
    //                key22, 
    //                scalarProcessor.Times(scalar1, scalar2)
    //            );
    //        }
    //    }

    //    return composer.CreateEvenMatrixStorage();
    //}
}