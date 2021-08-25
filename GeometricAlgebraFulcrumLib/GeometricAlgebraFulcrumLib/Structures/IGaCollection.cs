using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Structures
{
    public interface IGaCollection<out T>
    {
        bool IsEmpty();

        int GetSparseCount();

        IEnumerable<T> GetValues();
    }
}