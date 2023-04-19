using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Records.Restricted;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors
{
    /// <summary>
    /// This interface represent a sparse list with a single index of type ulong.
    /// The interface members are designed to serve GA data structures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILinVectorStorage<T> :
        ILinArrayStorage1D<T>
    {
        IEnumerable<ulong> GetIndices();

        T GetScalar(ulong index);

        IEnumerable<RGaKvIndexScalarRecord<T>> GetIndexScalarRecords();

        bool ContainsIndex(ulong index);

        bool TryGetScalar(ulong index, out T scalar);

        /// <summary>
        /// The smallest index stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMinIndex();

        /// <summary>
        /// The largest index stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMaxIndex();

        /// <summary>
        /// The indices not stored in this list in range 0 to maxKey
        /// </summary>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        IEnumerable<ulong> GetEmptyIndices(ulong maxCount);

        /// <summary>
        /// Copy the current state of this structure
        /// </summary>
        /// <returns></returns>
        ILinVectorStorage<T> GetCopy();

        /// <summary>
        /// Create a permutation of this structure, the index mapping must be one to one
        /// </summary>
        /// <param name="indexMapping"></param>
        /// <returns></returns>
        ILinVectorStorage<T> GetPermutation(Func<ulong, ulong> indexMapping);

        ILinVectorStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILinVectorStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexScalarMapping);

        ILinVectorStorage<T> FilterByIndex(Func<ulong, bool> indexFilter);

        ILinVectorStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexScalarFilter);

        ILinVectorStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, RGaGradeKvIndexRecord> indexToGradeIndexMapping);

        ILinVectorGradedStorage<T> ToVectorGradedStorage(Func<ulong, T, RGaGradeKvIndexScalarRecord<T>> indexScalarToGradeIndexScalarMapping);

        bool TryGetCompactStorage(out ILinVectorStorage<T> vectorStorage);
    }
}