using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageBivector<T> 
        : IGaStorageKVector<T>
    {
        bool TryGetTermScalar(ulong index1, ulong index2, out T value);
        
        bool TryGetTerm(ulong index1, ulong index2, out GaBasisTerm<T> term);

        IEnumerable<GaRecordKeyPairValue<T>> GetBasisVectorsIndexScalarRecords();

        IGaStorageBivector<T> GetBivectorCopy();

        IGaStorageBivector<T2> MapBivectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaStorageBivector<T2> MapBivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaStorageBivector<T2> MapBivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaStorageBivector<T2> MapBivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        IGaStorageBivector<T> FilterBivectorByScalar(Func<T, bool> scalarFilter);

        IGaStorageBivector<T> FilterBivectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaStorageBivector<T> FilterBivectorByIndex(Func<ulong, bool> indexFilter);
    }
}