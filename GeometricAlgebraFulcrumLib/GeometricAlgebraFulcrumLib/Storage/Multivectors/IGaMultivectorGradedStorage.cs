using System;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaMultivectorGradedStorage<T>
        : IGaMultivectorStorage<T>
    {
        ILaVectorGradedStorage<T> GradeIndexScalarList { get; }

        IGaMultivectorGradedStorage<T> GetGradedMultivectorCopy();

        IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);
    }
}