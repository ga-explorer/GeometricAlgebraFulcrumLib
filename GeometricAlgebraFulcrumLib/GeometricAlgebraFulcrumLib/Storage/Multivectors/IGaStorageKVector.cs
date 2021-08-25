using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaStorageKVector<T> 
        : IGaStorageMultivectorGraded<T>
    {
        uint Grade { get; }

        IGaListGradedSingleGrade<T> SingleGradeIndexScalarList { get; }

        IGaListEven<T> IndexScalarList { get; }

        ulong GetMinIndex();

        ulong GetMaxIndex();

        bool ContainsTermWithIndex(ulong index);

        bool TryGetTermScalarByIndex(ulong index, out T value);

        bool TryGetTermByIndex(int index, out GaBasisTerm<T> term);

        bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term);

        IGaStorageKVector<T> GetKVectorCopy();

        IGaStorageKVector<T2> MapKVectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaStorageKVector<T2> MapKVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaStorageKVector<T2> MapKVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaStorageKVector<T2> MapKVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);


        IGaStorageKVector<T> FilterKVectorByScalar(Func<T, bool> scalarFilter);

        IGaStorageKVector<T> FilterKVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaStorageKVector<T> FilterKVectorByIndex(Func<ulong, bool> indexFilter);

        IEnumerable<ulong> GetIndices();
    }
}