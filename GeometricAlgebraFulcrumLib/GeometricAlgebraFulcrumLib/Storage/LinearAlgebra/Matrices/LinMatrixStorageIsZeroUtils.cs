using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public static class LinMatrixStorageIsZeroUtils
{


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.IsEmpty() || matrixStorage.GetScalars().All(scalarProcessor.IsZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        return matrixGradedStorage.IsEmpty() || matrixGradedStorage.GetScalars().All(scalarProcessor.IsZero);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, bool nearZeroFlag)
    {
        return nearZeroFlag
            ? scalarProcessor.IsNearZero(matrixStorage)
            : scalarProcessor.IsZero(matrixStorage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage, bool nearZeroFlag)
    {
        return nearZeroFlag
            ? scalarProcessor.IsNearZero(matrixGradedStorage)
            : scalarProcessor.IsZero(matrixGradedStorage);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.IsEmpty() || matrixStorage.GetScalars().All(scalarProcessor.IsNearZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNearZero<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        return matrixGradedStorage.IsEmpty() || matrixGradedStorage.GetScalars().All(scalarProcessor.IsNearZero);
    }
}