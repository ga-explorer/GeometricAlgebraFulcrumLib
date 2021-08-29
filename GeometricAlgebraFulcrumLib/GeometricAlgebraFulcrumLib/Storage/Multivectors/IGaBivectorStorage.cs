using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaBivectorStorage<T> 
        : IGaKVectorStorage<T>
    {
        bool TryGetTermScalar(ulong index1, ulong index2, out T value);
        
        bool TryGetTerm(ulong index1, ulong index2, out GaBasisTerm<T> term);

        IEnumerable<IndexPairScalarRecord<T>> GetBasisVectorsIndexScalarRecords();

        IGaBivectorStorage<T> GetBivectorCopy();

        IGaBivectorStorage<T2> MapBivectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaBivectorStorage<T2> MapBivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaBivectorStorage<T2> MapBivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaBivectorStorage<T2> MapBivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        IGaBivectorStorage<T> FilterBivectorByScalar(Func<T, bool> scalarFilter);

        IGaBivectorStorage<T> FilterBivectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaBivectorStorage<T> FilterBivectorByIndex(Func<ulong, bool> indexFilter);
    }
}