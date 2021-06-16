using System;
using System.Collections.Generic;

namespace GeometricAlgebraLib.Storage
{
    public interface IGaBivectorStorage<TScalar> 
        : IGaKVectorStorage<TScalar>
    {
        IEnumerable<Tuple<ulong, ulong, TScalar>> GetBasisVectorsIndexScalarTuples();

        IGaBivectorStorage<TScalar> Add(IGaBivectorStorage<TScalar> mv2);

        IGaBivectorStorage<TScalar> Subtract(IGaBivectorStorage<TScalar> mv2);
    }
}