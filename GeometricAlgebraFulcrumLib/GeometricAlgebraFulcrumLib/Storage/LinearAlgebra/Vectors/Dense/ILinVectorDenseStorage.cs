using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public interface ILinVectorDenseStorage<T> :
        ILinVectorStorage<T>
    {
        int Count { get; }

        IReadOnlyList<T> GetScalarsList();

        ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
    }
}