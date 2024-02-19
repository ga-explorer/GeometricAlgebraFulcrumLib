﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

public sealed class LinVectorMatrixSliceDenseStorage<T> :
    LinVectorImmutableDenseStorageBase<T>
{
    public ILinMatrixStorage<T> SourceMatrix { get; }

    public Func<ulong, RGaKvIndexPairRecord> KeyMapping { get; }

    public Func<ulong, ulong, T> DefaultValueFunc { get; }

    public override int Count { get; }
        
 
    internal LinVectorMatrixSliceDenseStorage(ILinMatrixStorage<T> array, int count, Func<ulong, RGaKvIndexPairRecord> indexMapping, Func<ulong, ulong, T> defaultValueFunc)
    {
        SourceMatrix = array;
        Count = count;
        KeyMapping = indexMapping;
        DefaultValueFunc = defaultValueFunc;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetScalar(ulong index)
    {
        return SourceMatrix.GetScalar(KeyMapping(index), DefaultValueFunc);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinVectorStorage<T> GetCopy()
    {
        return this;
    }
        
    public override ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
    {
        var scalarsArray = new T[Count];

        for (var index = 0UL; index < (ulong) Count; index++)
            scalarsArray[indexMapping(index)] = GetScalar(index);

        return new LinVectorArrayStorage<T>(scalarsArray);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerator<T> GetEnumerator()
    {
        return ((ulong) Count).GetRange().Select(GetScalar).GetEnumerator();
    }
}