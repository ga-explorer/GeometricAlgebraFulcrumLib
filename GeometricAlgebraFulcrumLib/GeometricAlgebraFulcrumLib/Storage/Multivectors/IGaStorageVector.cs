using System;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageVector<T> 
        : IGaStorageKVector<T>
    {
        IGaStorageVector<T> GetVectorCopy();

        IGaStorageVector<T2> MapVectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaStorageVector<T2> MapVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaStorageVector<T2> MapVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaStorageVector<T2> MapVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        IGaStorageVector<T> FilterVectorByScalar(Func<T, bool> scalarFilter);

        IGaStorageVector<T> FilterVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaStorageVector<T> FilterVectorByIndex(Func<ulong, bool> indexFilter);
    }
}