using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Sparse;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

public sealed record LinVectorSingleScalarDenseStorage<T> :
    ILinVectorSingleScalarStorage<T>,
    ILinVectorMutableDenseStorage<T>
{
    public ulong Index 
        => 0UL;

    public T Scalar { get; set; }

    public int Count 
        => 1;

    public T this[int index]
    {
        get => index == 0 
            ? Scalar 
            : throw new KeyNotFoundException();
        set
        {
            if (index == 0) 
                Scalar = value;
            else
                throw new KeyNotFoundException();
        }
    }

    public T this[ulong index]
    {
        get => index == 0UL 
            ? Scalar 
            : throw new KeyNotFoundException();
        set
        {
            if (index == 0UL) 
                Scalar = value;
            else
                throw new KeyNotFoundException();
        }
    }


    internal LinVectorSingleScalarDenseStorage(T value)
    {
        Scalar = value;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetSparseCount()
    {
        return 1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(ulong index)
    {
        return index == 0UL
            ? Scalar
            : default;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ulong> GetIndices()
    {
        yield return 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetScalars()
    {
        yield return Scalar;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(ulong index)
    {
        return index == 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalar(ulong index, out T value)
    {
        if (index == 0UL)
        {
            value = Scalar;
            return true;
        }

        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetMinIndex()
    {
        return 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ulong GetMaxIndex()
    {
        return 0UL;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ulong> GetEmptyIndices(ulong maxCount)
    {
        return maxCount > 0
            ? (maxCount - 1).GetRange(1)
            : Enumerable.Empty<ulong>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> GetCopy()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping)
    {
        var index = indexMapping(0UL);

        return index == 0UL
            ? new LinVectorSingleScalarDenseStorage<T>(Scalar)
            : new LinVectorSingleScalarSparseStorage<T>(index, Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
    {
        return new LinVectorSingleScalarDenseStorage<T2>(valueMapping(Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
    {
        return new LinVectorSingleScalarDenseStorage<T2>(indexValueMapping(0UL, Scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
    {
        return indexFilter(0UL)
            ? this : LinVectorEmptyStorage<T>.EmptyStorage;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
    {
        return indexValueFilter(0UL, Scalar)
            ? this : LinVectorEmptyStorage<T>.EmptyStorage;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> FilterByScalar(Func<T, bool> valueFilter)
    {
        return valueFilter(Scalar)
            ? this : LinVectorEmptyStorage<T>.EmptyStorage;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorGradedStorage<T> ToVectorGradedStorage(
        Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping)
    {
        var (grade, index) = indexToGradeIndexMapping(0UL);

        ILinVectorStorage<T> vectorStorage = 
            index == 0
                ? new LinVectorSingleScalarDenseStorage<T>(Scalar) 
                : new LinVectorSingleScalarSparseStorage<T>(index, Scalar);

        return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);
    }

    public ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, RGaGradeKvIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping)
    {
        var (grade, index, scalar) = indexScalarToGradeIndexScalarMapping(0UL, Scalar);

        ILinVectorStorage<T> vectorStorage = 
            index == 0
                ? new LinVectorSingleScalarDenseStorage<T>(scalar) 
                : new LinVectorSingleScalarSparseStorage<T>(index, scalar);

        return new LinVectorSingleGradeStorage<T>(grade, vectorStorage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage)
    {
        vectorStorage = this;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords()
    {
        yield return new RGaKvIndexScalarRecord<T>(0UL, Scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
    {
        return indexMapping(0) == 0
            ? this
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerator<T> GetEnumerator()
    {
        return Enumerable.Repeat(Scalar, Count).GetEnumerator();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}