using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public abstract class LinVectorMutableDenseStorageBase<T> :
        LinVectorDenseStorageBase<T>, 
        ILinVectorMutableDenseStorage<T>
    {
        public abstract T this[int index] { get; set; }

        public abstract T this[ulong index] { get; set; }

        public abstract ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
        
        public abstract IEnumerator<T> GetEnumerator();
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}