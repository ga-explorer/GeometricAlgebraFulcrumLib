using System;
using System.Collections.Generic;
using GeometricAlgebraLib.Multivectors.Terms;

namespace GeometricAlgebraLib.Storage
{
    public interface IGaKVectorStorage<TScalar> 
        : IGaMultivectorGradedStorage<TScalar>
    {
        int Grade { get; }

        bool ContainsTermWithIndex(ulong index);

        TScalar GetTermScalarByIndex(ulong index);

        bool TryGetTermScalarByIndex(ulong index, out TScalar value);

        GaTerm<TScalar> GetTermByIndex(int index);

        GaTerm<TScalar> GetTermByIndex(ulong index);

        bool TryGetTermByIndex(int index, out GaTerm<TScalar> term);

        bool TryGetTermByIndex(ulong index, out GaTerm<TScalar> term);


        IEnumerable<ulong> GetIndices();

        IEnumerable<KeyValuePair<ulong, TScalar>> GetIndexScalarPairs();
        
        IEnumerable<Tuple<ulong, TScalar>> GetIndexScalarTuples();
        
        IReadOnlyDictionary<ulong, TScalar> GetIndexScalarDictionary();

        IGaKVectorStorage<TScalar> Op(IGaKVectorStorage<TScalar> mv2);
    }
}