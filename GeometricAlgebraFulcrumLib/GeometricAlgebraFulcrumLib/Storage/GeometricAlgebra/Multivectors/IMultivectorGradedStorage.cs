using System;

namespace GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors
{
    public interface IMultivectorGradedStorage<T>
        : IMultivectorStorage<T>
    {
        IMultivectorGradedStorage<T> GetGradedMultivectorCopy();

        IMultivectorGradedStorage<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping);

        IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IMultivectorGradedStorage<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);
    }
}