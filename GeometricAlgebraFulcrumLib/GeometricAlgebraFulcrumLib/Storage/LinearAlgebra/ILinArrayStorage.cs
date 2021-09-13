using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra
{
    public interface ILinArrayStorage<out T>
    {
        bool IsEmpty();

        int GetSparseCount();

        IEnumerable<T> GetScalars();
    }
}