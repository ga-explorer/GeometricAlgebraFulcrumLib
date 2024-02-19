using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Lists;

public interface IGaFuLReadOnlyList<T> :
    IReadOnlyList<T>
{
    int SparseCount { get; }

    T this[ulong index] { get; }
        
    bool IsEmpty();

    IEnumerable<ulong> GetIndices();

    IEnumerable<T> GetValues();

    bool ContainsIndex(ulong index);

    bool TryGetValue(ulong index, out T value);

    IEnumerable<KeyValuePair<ulong, T>> GetIndexValuePairs();
}