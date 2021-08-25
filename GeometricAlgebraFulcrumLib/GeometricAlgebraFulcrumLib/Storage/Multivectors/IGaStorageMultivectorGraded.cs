using System;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageMultivectorGraded<T>
        : IGaStorageMultivector<T>
    {
        IGaListGraded<T> GradeIndexScalarList { get; }

        IGaStorageMultivectorGraded<T> GetGradedMultivectorCopy();

        IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaStorageMultivectorGraded<T2> MapGradedMultivectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);
    }
}