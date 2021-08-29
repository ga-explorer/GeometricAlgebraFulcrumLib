using System;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaMultivectorSparseStorage<T> 
        : IGaMultivectorStorage<T>
    {
        ILaVectorEvenStorage<T> IdScalarList { get; }

        GaMultivectorSparseStorage<T> GetSparseMultivectorCopy();

        GaMultivectorSparseStorage<T2> MapSparseMultivectorScalars<T2>(Func<T, T2> scalarMapping);

        GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        GaMultivectorSparseStorage<T2> MapSparseMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);


        GaMultivectorSparseStorage<T> FilterSparseMultivectorByScalar(Func<T, bool> scalarFilter);

        GaMultivectorSparseStorage<T> FilterSparseMultivectorByIdScalar(Func<ulong, T, bool> idScalarFilter);

        GaMultivectorSparseStorage<T> FilterSparseMultivectorById(Func<ulong, bool> idFilter);
    }
}