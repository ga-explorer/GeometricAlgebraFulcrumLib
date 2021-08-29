using System;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaVectorStorage<T> 
        : IGaKVectorStorage<T>
    {
        IGaVectorStorage<T> GetVectorCopy();

        IGaVectorStorage<T2> MapVectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaVectorStorage<T2> MapVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaVectorStorage<T2> MapVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaVectorStorage<T2> MapVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);

        IGaVectorStorage<T> FilterVectorByScalar(Func<T, bool> scalarFilter);

        IGaVectorStorage<T> FilterVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaVectorStorage<T> FilterVectorByIndex(Func<ulong, bool> indexFilter);
    }
}