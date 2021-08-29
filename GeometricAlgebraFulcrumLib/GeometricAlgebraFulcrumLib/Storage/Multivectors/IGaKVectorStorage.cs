using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Algebra.Multivectors.Basis;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Multivectors
{
    public interface IGaKVectorStorage<T> 
        : IGaMultivectorGradedStorage<T>
    {
        uint Grade { get; }

        ILaVectorSingleGradeStorage<T> SingleGradeIndexScalarList { get; }

        ILaVectorEvenStorage<T> IndexScalarList { get; }

        ulong GetMinIndex();

        ulong GetMaxIndex();

        bool ContainsTermWithIndex(ulong index);

        bool TryGetTermScalarByIndex(ulong index, out T value);

        bool TryGetTermByIndex(int index, out GaBasisTerm<T> term);

        bool TryGetTermByIndex(ulong index, out GaBasisTerm<T> term);

        IGaKVectorStorage<T> GetKVectorCopy();

        IGaKVectorStorage<T2> MapKVectorScalars<T2>(Func<T, T2> scalarMapping);

        IGaKVectorStorage<T2> MapKVectorScalarsById<T2>(Func<ulong, T, T2> idScalarMapping);

        IGaKVectorStorage<T2> MapKVectorScalarsByIndex<T2>(Func<ulong, T, T2> indexScalarMapping);

        IGaKVectorStorage<T2> MapKVectorScalarsByGradeIndex<T2>(Func<uint, ulong, T, T2> gradeIndexScalarMapping);


        IGaKVectorStorage<T> FilterKVectorByScalar(Func<T, bool> scalarFilter);

        IGaKVectorStorage<T> FilterKVectorByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        IGaKVectorStorage<T> FilterKVectorByIndex(Func<ulong, bool> indexFilter);

        IEnumerable<ulong> GetIndices();
    }
}