using System;
using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Storage
{
    public interface IGasBivector<T> 
        : IGasKVector<T>
    {
        IEnumerable<Tuple<ulong, ulong, T>> GetBasisVectorsIndexScalarTuples();

        IGasBivector<T> Add(IGasBivector<T> mv2);

        IGasBivector<T> Subtract(IGasBivector<T> mv2);
    }
}