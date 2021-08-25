using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed record GaListEvenSingleKey<T> :
        IGaListEvenSingleKey<T>
    {
        public ulong Key { get; }

        public T Value { get; set; }

        public int Count 
            => 1;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key) =>
            key == Key
                ? Value
                : default;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            yield return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            yield return Value;
        }


        internal GaListEvenSingleKey(ulong key, [NotNull] T value)
        {
            Debug.Assert(key > 0);

            Key = key;
            Value = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return Key == key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            if (key == Key)
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
            return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey()
        {
            return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyKeys(ulong maxKey)
        {
            for (var key = 0UL; key < Key; key++)
                yield return key;

            for (var key = Key + 1; key <= maxKey; key++)
                yield return key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var key = keyMapping(Key);

            return key == 0UL
                ? new GaListEvenSingleKeyZero<T>(Value)
                : new GaListEvenSingleKey<T>(key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListEvenSingleKey<T2>(Key, valueMapping(Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaListEvenSingleKey<T2>(Key, keyValueMapping(Key, Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return keyFilter(Key)
                ? this
                : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(Key, Value)
                ? this
                : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : GaListEvenEmpty<T>.EmptyList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(Key);

            IGaListEven<T> evenDictionary = 
                index == 0
                    ? new GaListEvenSingleKeyZero<T>(Value) 
                    : new GaListEvenSingleKey<T>(index, Value);

            return new GaListGradedSingleGrade<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            if (Key > 0)
            {
                evenList = this;
                return false;
            }

            evenList = new GaListEvenSingleKeyZero<T>(Value);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            yield return new GaRecordKeyValue<T>(Key, Value);
        }
    }
}