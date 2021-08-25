using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    /// <summary>
    /// This interface represent a sparse list with a single key of type ulong.
    /// The interface members are designed to serve GA data structures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGaListEven<T> :
        IGaList<T>
    {
        IEnumerable<ulong> GetKeys();

        T GetValue(ulong key);

        IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords();

        bool ContainsKey(ulong key);

        bool TryGetValue(ulong key, out T value);

        /// <summary>
        /// The smallest key stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMinKey();

        /// <summary>
        /// The largest key stored in this structure
        /// </summary>
        /// <returns></returns>
        ulong GetMaxKey();

        /// <summary>
        /// The keys not stored in this list in range 0 to maxKey
        /// </summary>
        /// <param name="maxKey"></param>
        /// <returns></returns>
        IEnumerable<ulong> GetEmptyKeys(ulong maxKey);

        /// <summary>
        /// Copy the current state of this structure
        /// </summary>
        /// <returns></returns>
        IGaListEven<T> GetCopy();

        /// <summary>
        /// Create a permutation of this structure, the key mapping must be one to one
        /// </summary>
        /// <param name="keyMapping"></param>
        /// <returns></returns>
        IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping);

        IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping);

        IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping);

        IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter);

        IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter);

        IGaListEven<T> FilterByValue(Func<T, bool> valueFilter);

        IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping);

        bool TryGetCompactList(out IGaListEven<T> evenList);
    }
}