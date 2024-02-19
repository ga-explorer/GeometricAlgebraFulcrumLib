using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

public abstract class LinMatrixStorageComposerBase<T> :
    ILinMatrixStorageComposer<T>
{
    public IScalarProcessor<T> ScalarProcessor { get; }

    public abstract int Count { get; }


    protected LinMatrixStorageComposerBase(IScalarProcessor<T> scalarProcessor)
    {
        ScalarProcessor = scalarProcessor;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return Count == 0;
    }

    public abstract LinMatrixStorageComposerBase<T> Clear();

    public abstract LinMatrixStorageComposerBase<T> RemoveTerm(RGaKvIndexPairRecord index);

    public abstract LinMatrixStorageComposerBase<T> SetTerm(RGaKvIndexPairRecord index, T value);

    public abstract LinMatrixStorageComposerBase<T> AddTerm(RGaKvIndexPairRecord index, T value);

    public abstract LinMatrixStorageComposerBase<T> SubtractTerm(RGaKvIndexPairRecord index, T value);

    public abstract LinMatrixStorageComposerBase<T> MapValues(Func<T, T> valueMapping);

    public abstract LinMatrixStorageComposerBase<T> MapValues(Func<ulong, ulong, T, T> valueMapping);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> RemoveTerm(ulong index1, ulong index2)
    {
        return RemoveTerm(new RGaKvIndexPairRecord(index1, index2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> RemoveTerms(params RGaKvIndexPairRecord[] indexsList)
    {
        foreach (var index in indexsList)
            RemoveTerm(index);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> RemoveTerms(IEnumerable<RGaKvIndexPairRecord> indexsList)
    {
        foreach (var index in indexsList.ToArray())
            RemoveTerm(index);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract LinMatrixStorageComposerBase<T> RemoveZeroTerms();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SetTerm(ulong index1, ulong index2, T value)
    {
        return SetTerm(new RGaKvIndexPairRecord(index1, index2), value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SetTerms(ILinMatrixDenseStorage<T> matrixStorage)
    {
        var count1 = (ulong)matrixStorage.Count1;
        var count2 = (ulong)matrixStorage.Count2;

        for (var index1 = 0UL; index1 < count1; index1++)
        for (var index2 = 0UL; index2 < count2; index2++)
            SetTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SetTerms(ILinMatrixStorage<T> matrixStorage)
    {
        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            SetTerm(index1, index2, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SetTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
    {
        foreach (var (index1, index2, value) in indexTermRecords)
            SetTerm(new RGaKvIndexPairRecord(index1, index2), value);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> AddTerm(ulong index1, ulong index2, T value)
    {
        return AddTerm(new RGaKvIndexPairRecord(index1, index2), value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> AddTerms(ILinMatrixDenseStorage<T> matrixStorage)
    {
        var count1 = (ulong)matrixStorage.Count1;
        var count2 = (ulong)matrixStorage.Count2;

        for (var index1 = 0UL; index1 < count1; index1++)
        for (var index2 = 0UL; index2 < count2; index2++)
            AddTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> AddTerms(ILinMatrixStorage<T> matrixStorage)
    {
        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            AddTerm(index1, index2, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> AddTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
    {
        foreach (var (index1, index2, value) in indexTermRecords)
            AddTerm(new RGaKvIndexPairRecord(index1, index2), value);

        return this;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SubtractTerm(ulong index1, ulong index2, T value)
    {
        return SubtractTerm(new RGaKvIndexPairRecord(index1, index2), value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SubtractTerms(ILinMatrixDenseStorage<T> matrixStorage)
    {
        var count1 = (ulong)matrixStorage.Count1;
        var count2 = (ulong)matrixStorage.Count2;

        for (var index1 = 0UL; index1 < count1; index1++)
        for (var index2 = 0UL; index2 < count2; index2++)
            SubtractTerm(index1, index2, matrixStorage.GetScalar(index1, index2));

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SubtractTerms(ILinMatrixStorage<T> matrixStorage)
    {
        foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
            SubtractTerm(index1, index2, value);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> SubtractTerms(IEnumerable<RGaKvIndexPairScalarRecord<T>> indexTermRecords)
    {
        foreach (var (index1, index2, value) in indexTermRecords)
            SubtractTerm(new RGaKvIndexPairRecord(index1, index2), value);

        return this;
    }


    public abstract ILinMatrixStorage<T> CreateLinMatrixStorage();

    public abstract ILinMatrixDenseStorage<T> CreateLinMatrixDenseStorage();

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public ILinMatrixGradedStorage<T> CreateLinMatrixGradedStorage()
    //{
    //    return CreateLinMatrixStorage().ToMatrixGradedStorage();
    //}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> Negative()
    {
        return MapValues(ScalarProcessor.Negative);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> Times(T scalingFactor)
    {
        return MapValues(scalar => ScalarProcessor.Times(scalar, scalingFactor));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixStorageComposerBase<T> Divide(T scalingFactor)
    {
        return MapValues(scalar => ScalarProcessor.Divide(scalar, scalingFactor));
    }
}