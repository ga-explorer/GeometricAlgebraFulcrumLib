using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Sparse;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public static class LinMatrixStorageUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId1<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.GetMaxIndex1();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId2<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.GetMaxIndex2();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId1<T>(this ILinMatrixStorage<T> matrixStorage, uint grade)
    {
        return matrixStorage.GetMaxIndex1().BasisBladeIndexToId(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId2<T>(this ILinMatrixStorage<T> matrixStorage, uint grade)
    {
        return matrixStorage.GetMaxIndex2().BasisBladeIndexToId(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId1<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        var grade = matrixGradedStorage.GetMaxGrade();

        return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage)
            ? matrixStorage.GetMaxIndex1().BasisBladeIndexToId(grade)
            : 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId1<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
    {
        return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage)
            ? matrixStorage.GetMaxIndex1().BasisBladeIndexToId(grade)
            : 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId2<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        var grade = matrixGradedStorage.GetMaxGrade();

        return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage)
            ? matrixStorage.GetMaxIndex2().BasisBladeIndexToId(grade)
            : 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong GetMaxBasisBladeId2<T>(this ILinMatrixGradedStorage<T> matrixGradedStorage, uint grade)
    {
        return matrixGradedStorage.TryGetMatrixStorage(grade, out var matrixStorage)
            ? matrixStorage.GetMaxIndex2().BasisBladeIndexToId(grade)
            : 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairRecord> GetIndicesUnion<T>(this ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    {
        return matrixStorage1.GetIndices().Union(matrixStorage2.GetIndices());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairRecord> GetIndicesIntersection<T>(this ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    {
        return matrixStorage1.GetIndices().Intersect(matrixStorage2.GetIndices());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<RGaKvIndexPairRecord> GetIndicesDifference<T>(this ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    {
        return matrixStorage1.GetIndices().Except(matrixStorage2.GetIndices());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static HashSet<RGaKvIndexPairRecord> GetIndicesSymmetricDifference<T>(this ILinMatrixStorage<T> matrixStorage1, ILinMatrixStorage<T> matrixStorage2)
    {
        var indexsSet = new HashSet<RGaKvIndexPairRecord>(matrixStorage1.GetIndices());

        indexsSet.SymmetricExceptWith(matrixStorage2.GetIndices());

        return indexsSet;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetScalars<T>(this ILinMatrixStorage<T> matrixStorage, Func<T, T> mappingFunc)
    {
        return matrixStorage
            .GetScalars()
            .Select(mappingFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetScalars<T>(this ILinMatrixStorage<T> matrixStorage, Func<ulong, ulong, T, T> mappingFunc)
    {
        return matrixStorage
            .GetIndexScalarRecords()
            .Select(indexScalar =>
                mappingFunc(indexScalar.KvIndex1, indexScalar.KvIndex2, indexScalar.Scalar)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<T> GetScalars<T>(this ILinMatrixStorage<T> matrixStorage, uint grade, Func<uint, ulong, ulong, T, T> mappingFunc)
    {
        return matrixStorage
            .GetIndexScalarRecords()
            .Select(indexScalar =>
                mappingFunc(grade, indexScalar.KvIndex1, indexScalar.KvIndex2, indexScalar.Scalar)
            );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ILinMatrixStorage<T> GetCompactMatrixStorage<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.TryGetCompactStorage(out var compactMatrix)
            ? compactMatrix
            : matrixStorage;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDenseCount1<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.IsEmpty()
            ? 0
            : (int)(matrixStorage.GetMaxIndex1() + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDenseCount2<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.IsEmpty()
            ? 0
            : (int)(matrixStorage.GetMaxIndex2() + 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDenseCount<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        if (matrixStorage.IsEmpty())
            return 0;

        var (index1, index2) = matrixStorage.GetMaxIndex();

        return (int)((index1 + 1) * (index2 + 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<int> GetDenseCountPair<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        if (matrixStorage.IsEmpty())
            return new Pair<int>(0, 0);

        var (index1, index2) = matrixStorage.GetMaxIndex();

        return new Pair<int>((int)(index1 + 1), (int)(index2 + 1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<int> GetSparseCountPair<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        if (matrixStorage.IsEmpty())
            return new Pair<int>(0, 0);

        var count1 = matrixStorage.GetSparseCount1();
        var count2 = matrixStorage.GetSparseCount2();

        return new Pair<int>(count1, count2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool ContainsIndex<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2)
    {
        return index1 >= 0 && index2 >= 0 && matrixStorage.ContainsIndex((ulong)index1, (ulong)index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2)
    {
        return index1 < 0 || index2 < 0
            ? throw new KeyNotFoundException()
            : matrixStorage.GetScalar((ulong)index1, (ulong)index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2, T defaultScalar)
    {
        return index1 >= 0 && index2 >= 0 && matrixStorage.TryGetScalar((ulong)index1, (ulong)index2, out var value)
            ? value ?? defaultScalar
            : defaultScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, ulong index1, ulong index2, T defaultScalar)
    {
        return matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value ?? defaultScalar
            : defaultScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, RGaKvIndexPairRecord index, T defaultScalar)
    {
        return matrixStorage.TryGetScalar(index, out var value)
            ? value ?? defaultScalar
            : defaultScalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2, Func<T> defaultScalarFunc)
    {
        return index1 >= 0 && index2 >= 0 && matrixStorage.TryGetScalar((ulong)index1, (ulong)index2, out var value)
            ? value ?? defaultScalarFunc()
            : defaultScalarFunc();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, ulong index1, ulong index2, Func<T> defaultScalarFunc)
    {
        return matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value ?? defaultScalarFunc()
            : defaultScalarFunc();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2, Func<int, int, T> defaultScalarFunc)
    {
        return index1 >= 0 && index2 >= 0 && matrixStorage.TryGetScalar((ulong)index1, (ulong)index2, out var value)
            ? value ?? defaultScalarFunc(index1, index2)
            : defaultScalarFunc(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, ulong index1, ulong index2, Func<ulong, ulong, T> defaultScalarFunc)
    {
        return matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value ?? defaultScalarFunc(index1, index2)
            : defaultScalarFunc(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this ILinMatrixStorage<T> matrixStorage, RGaKvIndexPairRecord index, Func<ulong, ulong, T> defaultScalarFunc)
    {
        return matrixStorage.TryGetScalar(index, out var value)
            ? value ?? defaultScalarFunc(index.KvIndex1, index.KvIndex2)
            : defaultScalarFunc(index.KvIndex1, index.KvIndex2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryGetScalar<T>(this ILinMatrixStorage<T> matrixStorage, int index1, int index2, out T value)
    {
        if (index1 >= 0 && index2 >= 0 && matrixStorage.TryGetScalar((ulong)index1, (ulong)index2, out value))
            return true;

        value = default;
        return false;
    }


    /// <summary>
    /// The value corresponding to the smallest index stored in this structure
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMinIndexScalar<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.GetScalar(matrixStorage.GetMinIndex());
    }

    /// <summary>
    /// The value corresponding to the largest index stored in this structure
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetMaxIndexScalar<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        return matrixStorage.GetScalar(matrixStorage.GetMaxIndex());
    }


    /// <summary>
    /// The smallest index and corresponding value stored in this structure
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKvIndexPairScalarRecord<T> GetMinIndexScalarRecord<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        var index = matrixStorage.GetMinIndex();

        return new RGaKvIndexPairScalarRecord<T>(index.KvIndex1, index.KvIndex2, matrixStorage.GetScalar(index));
    }

    /// <summary>
    /// The largest index and corresponding value stored in this structure
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaKvIndexPairScalarRecord<T> GetMaxIndexScalarRecord<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        var index = matrixStorage.GetMaxIndex();

        return new RGaKvIndexPairScalarRecord<T>(index.KvIndex1, index.KvIndex2, matrixStorage.GetScalar(index));
    }

    /// <summary>
    /// Convert this structure to a read only list and replace empty
    /// indexs using defaultScalarFunc
    /// </summary>
    /// <param name="matrixStorage"></param>
    /// <param name="count1"></param>
    /// <param name="count2"></param>
    /// <param name="defaultScalarFunc"></param>
    /// <returns></returns>
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, int count1, int count2, Func<int, int, T> defaultScalarFunc)
    {
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalarFunc((int)index1, (int)index2);

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    /// <summary>
    /// Convert this structure to a read only list and replace empty
    /// indexs using defaultScalar
    /// </summary>
    /// <param name="matrixStorage"></param>
    /// <param name="count1"></param>
    /// <param name="count2"></param>
    /// <param name="defaultScalar"></param>
    /// <returns></returns>
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, int count1, int count2, T defaultScalar)
    {
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalar;

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage)
    {
        var (count1, count2) = matrixStorage.GetDenseCountPair();

        var array = new T[count1, count2];

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, T defaultScalar)
    {
        var (count1, count2) = matrixStorage.GetDenseCountPair();
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalar;

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, int count1, int count2)
    {
        var array = new T[count1, count2];

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, Func<T> defaultScalarFunc)
    {
        var (count1, count2) = matrixStorage.GetDenseCountPair();
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalarFunc();

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, Func<int, int, T> defaultScalarFunc)
    {
        var (count1, count2) = matrixStorage.GetDenseCountPair();
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalarFunc((int)index1, (int)index2);

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[,] ToArray<T>(this ILinMatrixStorage<T> matrixStorage, int count1, int count2, Func<T> defaultScalarFunc)
    {
        var array = new T[count1, count2];

        var emptyIndices =
            matrixStorage.GetEmptyIndices((ulong)count1, (ulong)count2);

        foreach (var (index1, index2) in emptyIndices)
            array[index1, index2] = defaultScalarFunc();

        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            array[index1, index2] = value;

        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, int index)
    {
        return vector.GetScalar(index, scalarProcessor.ScalarZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILinVectorStorage<T> vector, ulong index)
    {
        return vector.GetScalar(index, scalarProcessor.ScalarZero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorRepeatedScalarStorage<T> CreateOnesScalarsList<T>(this IScalarProcessor<T> scalarProcessor, int size)
    {
        var scalar = scalarProcessor.ScalarOne;

        return scalar.CreateLinVectorRepeatedScalarStorage(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinVectorRepeatedScalarStorage<T> CreateUnitOnesScalarsList<T>(this IScalarProcessor<T> scalarProcessor, int size)
    {
        var scalar = scalarProcessor.Sqrt(
            scalarProcessor.Divide(scalarProcessor.ScalarOne, size)
        );

        return scalar.CreateLinVectorRepeatedScalarStorage(size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMatrixDiagonalStorage<T> CreateOnesDiagonalMatrixStorage<T>(this IScalarProcessor<T> scalarProcessor, int size)
    {
        return scalarProcessor.CreateOnesScalarsList(size).CreateLinMatrixDiagonalStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static LinMatrixDiagonalStorage<T> CreateUnitOnesDiagonalMatrixStorage<T>(this IScalarProcessor<T> scalarProcessor, int size)
    {
        return scalarProcessor.CreateUnitOnesScalarsList(size).CreateLinMatrixDiagonalStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T GetScalar<T>(this IScalarProcessor<T> scalarProcessor, ILinMatrixStorage<T> matrixStorage, int rowIndex, int colIndex)
    {
        return matrixStorage.GetScalar(rowIndex, colIndex, scalarProcessor.ScalarZero);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<uint> GetMinVSpaceDimensionOfVector<T>(this ILinMatrixStorage<T> indexScalarMatrix)
    {
        if (indexScalarMatrix.IsEmpty())
            return new Pair<uint>(0U, 0U);

        return indexScalarMatrix
            .GetMaxIndex()
            .MapRecordIndices(BasisVectorUtils.BasisVectorIndexToMinVSpaceDimension);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<uint> GetMinVSpaceDimensionOfBivector<T>(this ILinMatrixStorage<T> indexScalarMatrix)
    {
        if (indexScalarMatrix.IsEmpty())
            return new Pair<uint>(0U, 0U);

        return indexScalarMatrix
            .GetMaxIndex()
            .MapRecordIndices(BasisBivectorUtils.BasisBivectorIndexToMinVSpaceDimension);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<uint> GetMinVSpaceDimensionOfKVector<T>(this ILinMatrixStorage<T> indexScalarMatrix, uint grade)
    {
        if (indexScalarMatrix.IsEmpty())
            return new Pair<uint>(0U, 0U);

        return indexScalarMatrix
            .GetMaxIndex()
            .MapRecordIndices(index => index.BasisBladeIndexToMinVSpaceDimension(grade));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Pair<uint> GetMinVSpaceDimensionOfMultivector<T>(this ILinMatrixStorage<T> idScalarMatrix)
    {
        if (idScalarMatrix.IsEmpty())
            return new Pair<uint>(0U, 0U);

        return idScalarMatrix
            .GetMaxIndex()
            .MapRecordIndices(BasisBladeUtils.BasisBladeIdToMinVSpaceDimension);
    }


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static ILinMatrixGradedStorage<T> ToMatrixGradedStorage<T>(this ILinMatrixStorage<T> matrixStorage)
    //{
    //    return matrixStorage.ToMatrixGradedStorage(BasisBladeUtils.BasisBladeIdToGradeIndex);
    //}
}