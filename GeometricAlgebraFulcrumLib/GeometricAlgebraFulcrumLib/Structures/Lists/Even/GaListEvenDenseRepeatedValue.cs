using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseRepeatedValue<T> :
        IGaListEvenDense<T>
    {
        public T Value { get; set; }

        public int Count { get; }
        
        public T this[int index] 
            => GetValue((ulong) index);

        public T this[ulong index] 
            => GetValue(index);


        internal GaListEvenDenseRepeatedValue(int count, [NotNull] T value)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            Count = count;
            Value = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key)
        {
            return key < (ulong) Count
                ? Value
                : throw new KeyNotFoundException(nameof(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            return ((ulong) GetSparseCount()).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return Enumerable.Repeat(Value, GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return key < (ulong) GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            if (key < (ulong) GetSparseCount())
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetCopy()
        {
            return new GaListEvenDenseRepeatedValue<T>(GetSparseCount(), Value);
        }

        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) GetSparseCount(); key++)
                valueDictionary.Add(keyMapping(key), Value);

            return valueDictionary.CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListEvenDenseRepeatedValue<T2>(
                GetSparseCount(), 
                valueMapping(Value)
            );
        }

        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            var valuesList = new T2[GetSparseCount()];

            for (var i = 0; i < GetSparseCount(); i++)
                valuesList[i] = keyValueMapping((ulong) i, Value);

            return new GaListEvenDenseList<T2>(valuesList);
        }

        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) GetSparseCount(); key++)
                if (keyFilter(key))
                    valueDictionary.Add(key, Value);

            return valueDictionary.CreateEvenList();
        }

        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            var valueDictionary = new Dictionary<ulong, T>();

            for (var key = 0UL; key < (ulong) GetSparseCount(); key++)
                if (keyValueFilter(key, Value))
                    valueDictionary.Add(key, Value);

            return new GaListEvenSparse<T>(valueDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this 
                : GaListEvenEmpty<T>.EmptyList;
        }

        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            for (var id = 0UL; id < (ulong) GetSparseCount(); id++)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = Value;
                else
                    keyValueDictionary.Add(index, Value);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            if (Count == 0)
            {
                evenList = GaListEvenEmpty<T>.EmptyList;
                return true;
            }

            if (Count == 1)
            {
                evenList = new GaListEvenSingleKeyZero<T>(Value);
                return true;
            }

            evenList = this;
            return false;
        }

        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            var count = (ulong) GetSparseCount();

            for (var key = 0UL; key < count; key++)
                yield return new GaRecordKeyValue<T>(key, Value);
        }
    }
}