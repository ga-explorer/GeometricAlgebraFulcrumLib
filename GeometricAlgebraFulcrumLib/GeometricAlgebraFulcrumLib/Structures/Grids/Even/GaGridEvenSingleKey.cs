using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed record GaGridEvenSingleKey<T> :
        IGaGridEvenSingleKey<T>
    {
        public ulong Key2 
            => Key.Key2;

        public ulong Key1 
            => Key.Key1;

        public GaRecordKeyPair Key { get; }

        public T Value { get; set; }

        public int Count1 
            => 1;

        public int Count2 
            => 1;

        public int Count 
            => 1;


        internal GaGridEvenSingleKey([NotNull] GaRecordKeyPair keyPair, T value)
        {
            Debug.Assert(keyPair.Key1 > 0 || keyPair.Key2 > 0);

            Key = keyPair;
            Value = value;
        }

        internal GaGridEvenSingleKey(ulong key1, ulong key2, T value)
        {
            Key = new GaRecordKeyPair(key1, key2);
            Value = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            return key == Key
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key1, ulong key2)
        {
            return key1 == Key.Key1 && key2 == Key.Key2
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys1()
        {
            yield return Key.Key1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            yield return Key.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            yield return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            yield return Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            return key1 == Key.Key1 && 
                   key2 == Key.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            return Key == key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey1()
        {
            return Key.Key1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey2()
        {
            return Key.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMinKey()
        {
            return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            return Key.Key1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            return Key.Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            return Key;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
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
        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            if (key1 == Key.Key1 && key2 == Key.Key2)
            {
                value = Value;
                return true;
            }

            value = default;
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            return new GaRecordKeyPair(maxKey1, maxKey2)
                .GetKeyPairsInRange()
                .Where(key => key != Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            return maxKey
                .GetKeyPairsInRange()
                .Where(key => key != Key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetCopy()
        {
            return new GaGridEvenSingleKey<T>(Key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            return new GaGridEvenSingleKey<T>(keyMapping(Key.Key1, Key.Key2), Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridEvenSingleKey<T2>(Key, valueMapping(Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridEvenSingleKey<T2>(Key, keyValueMapping(Key.Key1, Key.Key2, Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return keyFilter(Key.Key1, Key.Key2)
                ? this
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(Key.Key1, Key.Key2, Value)
                ? this
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> Transpose()
        {
            return new GaGridEvenSingleKey<T>(Key.SwapKeys(), Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            var (grade, key1, key2) = 
                evenKeyToGradeKeyMapping(Key.Key1, Key.Key2);

            IGaGridEven<T> evenList = 
                key1 == 0 && key2 == 0
                    ? new GaGridEvenSingleKeyZero<T>(Value) 
                    : new GaGridEvenSingleKey<T>(key1, key2, Value);

            return new GaGridGradedSingleGrade<T>(grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetRow(ulong key1)
        {
            return key1 == Key1
                ? GaListEvenEmpty<T>.EmptyList
                : new GaListEvenSingleKey<T>(key1, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetColumn(ulong key2)
        {
            return key2 == Key2
                ? GaListEvenEmpty<T>.EmptyList
                : new GaListEvenSingleKey<T>(key2, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactGrid(out IGaGridEven<T> evenGrid)
        {
            evenGrid = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            yield return new GaRecordKeyPairValue<T>(Key.Key1, Key.Key2, Value);
        }
    }
}
