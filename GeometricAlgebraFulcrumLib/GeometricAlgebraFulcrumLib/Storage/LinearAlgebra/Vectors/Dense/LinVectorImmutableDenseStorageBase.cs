using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public abstract class LinVectorImmutableDenseStorageBase<T> :
        LinVectorDenseStorageBase<T>, ILinVectorImmutableDenseStorage<T>
    {
        public T this[int index] 
            => GetScalar((ulong) index);

        public T this[ulong index] 
            => GetScalar(index);

        public abstract IReadOnlyList<T> GetScalarsList();

        public abstract ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
    }
}