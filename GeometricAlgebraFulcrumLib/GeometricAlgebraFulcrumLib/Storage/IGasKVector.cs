using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Terms;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGasKVector<T> 
        : IGasGradedMultivector<T>
    {
        uint Grade { get; }

        bool ContainsTermWithIndex(ulong index);

        T GetTermScalarByIndex(ulong index);

        bool TryGetTermScalarByIndex(ulong index, out T value);

        GaTerm<T> GetTermByIndex(int index);

        GaTerm<T> GetTermByIndex(ulong index);

        bool TryGetTermByIndex(int index, out GaTerm<T> term);

        bool TryGetTermByIndex(ulong index, out GaTerm<T> term);


        IEnumerable<ulong> GetIndices();

        IEnumerable<KeyValuePair<ulong, T>> GetIndexScalarPairs();
        
        IEnumerable<Tuple<ulong, T>> GetIndexScalarTuples();
        
        IReadOnlyDictionary<ulong, T> GetIndexScalarDictionary();
    }
}