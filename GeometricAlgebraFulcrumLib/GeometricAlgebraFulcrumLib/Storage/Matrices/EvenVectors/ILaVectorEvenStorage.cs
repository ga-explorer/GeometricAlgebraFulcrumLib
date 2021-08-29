using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Storage.Matrices.GradedVectors;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    /// <summary>
    /// This interface represent a sparse list with a single key of type ulong.
    /// The interface members are designed to serve GA data structures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ILaVectorEvenStorage<T> :
        ILaVectorStorage<T>
    {
        IEnumerable<ulong> GetIndices();

        T GetScalar(ulong index);

        IEnumerable<IndexScalarRecord<T>> GetIndexScalarRecords();

        bool ContainsIndex(ulong index);

        bool TryGetScalar(ulong index, out T scalar);

        /// <summary>
        /// The smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMinIndex();

        /// <summary>
        /// The largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMaxIndex();

        /// <summary>
        /// The keys not stored in this list in range 0 to maxKey
        /// </summary>
        /// <param name="maxKey"></param>
        /// <returns></returns>
        IEnumerable<ulong> GetEmptyIndices(ulong maxKey);

        /// <summary>
        /// Copy the current state of this structure
        /// </summary>
        /// <returns></returns>
        ILaVectorEvenStorage<T> GetCopy();

        /// <summary>
        /// Create a permutation of this structure, the key mapping must be one to one
        /// </summary>
        /// <param name="keyMapping"></param>
        /// <returns></returns>
        ILaVectorEvenStorage<T> MapIndices(Func<ulong, ulong> keyMapping);

        ILaVectorEvenStorage<T2> MapScalars<T2>(Func<T, T2> scalarMapping);

        ILaVectorEvenStorage<T2> MapScalars<T2>(Func<ulong, T, T2> keyValueMapping);

        ILaVectorEvenStorage<T> FilterByIndex(Func<ulong, bool> keyFilter);

        ILaVectorEvenStorage<T> FilterByIndexScalar(Func<ulong, T, bool> keyValueFilter);

        ILaVectorEvenStorage<T> FilterByScalar(Func<T, bool> scalarFilter);

        ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, GradeIndexRecord> evenIndexToGradeIndexMapping);

        ILaVectorGradedStorage<T> ToGradedStorage(Func<ulong, T, GradeIndexScalarRecord<T>> evenIndexScalarToGradeIndexScalarMapping);

        bool TryGetCompactStorage(out ILaVectorEvenStorage<T> evenList);
    }
}