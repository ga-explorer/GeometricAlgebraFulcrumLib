﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public static class LinMatrixStorageNegativeUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.MapScalars(scalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixGradedStorage<T> Negative<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        return matrixGradedStorage.MapScalars(scalarProcessor.Negative);
    }
}