using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public abstract class LinVectorMutableDenseStorageBase<T> :
        LinVectorDenseStorageBase<T>, 
        ILinVectorMutableDenseStorage<T>
    {
        public abstract T this[int index] { get; set; }

        public abstract T this[ulong index] { get; set; }

        public abstract IReadOnlyList<T> GetScalarsList();

        public abstract ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
    }
}