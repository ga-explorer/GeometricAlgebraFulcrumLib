using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

public interface ILinVectorDenseStorage<T> :
    ILinVectorStorage<T>, 
    IReadOnlyList<T>
{
    ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping);
}