using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Terms;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGaStorageKVector<T> 
        : IGaStorageMultivectorGraded<T>
    {
        uint Grade { get; }

        ulong FirstIndex { get; }

        ulong LastIndex { get; }

        T FirstScalar { get; }

        T LastScalar { get; }

        IGaEvenDictionary<T> IndexScalarDictionary { get; }

        bool ContainsTermWithIndex(ulong index);

        bool TryGetTermScalarByIndex(ulong index, out T value);

        bool TryGetTermByIndex(int index, out GaTerm<T> term);

        bool TryGetTermByIndex(ulong index, out GaTerm<T> term);


        IEnumerable<ulong> GetIndices();
    }
}