using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Terms;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGaStorageBivector<T> 
        : IGaStorageKVector<T>
    {
        bool TryGetTermScalar(ulong index1, ulong index2, out T value);
        
        bool TryGetTerm(ulong index1, ulong index2, out GaTerm<T> term);

        IEnumerable<Tuple<ulong, ulong, T>> GetBasisVectorsIndexScalarTuples();
    }
}