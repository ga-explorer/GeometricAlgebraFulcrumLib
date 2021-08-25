using System;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageMultivectorSparse<T> 
        : IGaStorageMultivector<T>
    {
        IGaListEven<T> IdScalarList { get; }

        GaStorageMultivectorSparse<T> GetSparseMultivectorCopy();

        GaStorageMultivectorSparse<T2> MapSparseMultivectorScalars<T2>(Func<T, T2> scalarMapping);

        GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        GaStorageMultivectorSparse<T2> MapSparseMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);


        GaStorageMultivectorSparse<T> FilterSparseMultivectorByScalar(Func<T, bool> scalarFilter);

        GaStorageMultivectorSparse<T> FilterSparseMultivectorByIdScalar(Func<ulong, T, bool> idScalarFilter);

        GaStorageMultivectorSparse<T> FilterSparseMultivectorById(Func<ulong, bool> idFilter);
    }
}