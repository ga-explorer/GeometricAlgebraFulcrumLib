using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices
{
    public interface ILaStorage<out T>
    {
        bool IsEmpty();

        int GetSparseCount();

        IEnumerable<T> GetScalars();
    }
}