using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

public abstract class LinVectorImmutableDenseStorageBase<T> :
    LinVectorDenseStorageBase<T>, 
    ILinVectorImmutableDenseStorage<T>
{
    public T this[int index] 
        => GetScalar((ulong) index);

    public T this[ulong index] 
        => GetScalar(index);

    public abstract ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
        
    public abstract IEnumerator<T> GetEnumerator();
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}