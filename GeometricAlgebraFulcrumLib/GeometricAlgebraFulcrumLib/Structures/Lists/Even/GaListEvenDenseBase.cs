using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    /// <summary>
    /// A base class for dense lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class GaListEvenDenseBase<T> :
        IGaListEven<T>
    {
        public abstract int Count { get; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count;
        }

        public abstract T GetValue(ulong key);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            return ((ulong) GetSparseCount()).GetRange();
        }

        public virtual IEnumerable<T> GetValues()
        {
            return ((ulong) GetSparseCount()).MapRange(GetValue);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return GetSparseCount() == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return key < (ulong) GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            if (ContainsKey(key))
            {
                value = GetValue(key);
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey()
        {
            return (ulong) (GetSparseCount() - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyKeys(ulong maxKey)
        {
            var lastKey = (ulong) (GetSparseCount() - 1);

            return maxKey <= lastKey
                ? Enumerable.Empty<ulong>()
                : (maxKey - lastKey).GetRange(lastKey + 1);
        }

        public abstract IGaListEven<T> GetCopy();

        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < count; key++)
            {
                valueDictionary.Add(
                    keyMapping(key), 
                    GetValue(key)
                );
            }

            return valueDictionary.CreateEvenList();
        }

        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var count = (ulong) GetSparseCount();
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = valueMapping(GetValue(i));

            return valuesList.CreateEvenListDense();
        }

        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var count = (ulong) GetSparseCount();
            var valuesList = new T2[count];

            for (var i = 0UL; i < count; i++)
                valuesList[i] = keyValueMapping(i, GetValue(i));

            return valuesList.CreateEvenListDense();
        }

        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < count; key++)
            {
                if (keyFilter(key))
                    valueDictionary.Add(key, GetValue(key));
            }

            return valueDictionary.CreateEvenList();
        }

        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < count; key++)
            {
                var value = GetValue(key);

                if (keyValueFilter(key, value))
                    valueDictionary.Add(key, value);
            }

            return valueDictionary.CreateEvenList();
        }

        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var count = (ulong) GetSparseCount();
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < count; key++)
            {
                var value = GetValue(key);

                if (valueFilter(value))
                    valueDictionary.Add(key, value);
            }

            return valueDictionary.CreateEvenList();
        }

        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var count = (ulong) GetSparseCount();
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < count; id++)
            {
                var value = GetValue(id);
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = value;
                else
                    keyValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            if (Count > 1)
            {
                evenList = this;
                return false;
            }

            evenList = 
                Count == 0
                    ? GaListEvenEmpty<T>.EmptyList
                    : new GaListEvenSingleKeyZero<T>(GetValue(0UL));

            return true;
        }

        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            var count = (ulong) GetSparseCount();

            for (var key = 0UL; key < count; key++)
                yield return new GaRecordKeyValue<T>(key, GetValue(key));
        }
    }
}