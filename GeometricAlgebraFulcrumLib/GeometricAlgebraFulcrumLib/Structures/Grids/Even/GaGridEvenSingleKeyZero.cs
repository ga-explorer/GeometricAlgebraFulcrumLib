using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed record GaGridEvenSingleKeyZero<T> :
        IGaGridEvenSingleKey<T>,
        IGaGridEvenDenseMutable<T>
    {
        public ulong Key1 
            => 0UL;

        public ulong Key2 
            => 0UL;

        public GaRecordKeyPair Key 
            => GaRecordsFactory.ZeroKeyPair;

        public T Value { get; set; }

        public int Count1 
            => 1;

        public int Count2 
            => 1;

        public int Count 
            => 1;

        public T this[int index1, int index2]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public T this[ulong index1, ulong index2]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        internal GaGridEvenSingleKeyZero([NotNull] T value)
        {
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
        public IEnumerable<ulong> GetKeys1()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            yield return GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            yield return Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            return key == GaRecordsFactory.ZeroKeyPair
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key1, ulong key2)
        {
            return key1 == 0UL && key2 == 0UL
                ? Value
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            return key1 == 0UL && 
                   key2 == 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            return key == GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey1()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey2()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMinKey()
        {
            return GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            return GaRecordsFactory.ZeroKeyPair;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            if (key == GaRecordsFactory.ZeroKeyPair)
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
            if (key1 == 0UL && key2 == 0UL)
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
                .Where(key => key != GaRecordsFactory.ZeroKeyPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            return maxKey
                .GetKeyPairsInRange()
                .Where(key => key != GaRecordsFactory.ZeroKeyPair);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetCopy()
        {
            return new GaGridEvenSingleKey<T>(GaRecordsFactory.ZeroKeyPair, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            return new GaGridEvenSingleKey<T>(keyMapping(0UL, 0UL), Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridEvenSingleKeyZero<T2>(
                valueMapping(Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridEvenSingleKeyZero<T2>(
                keyValueMapping(0UL, 0UL, Value)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return keyFilter(0UL, 0UL)
                ? this
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(0UL, 0UL, Value)
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
            return new GaGridEvenSingleKeyZero<T>(Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            var (grade, key1, key2) = 
                evenKeyToGradeKeyMapping(0UL, 0UL);

            IGaGridEven<T> evenList = 
                key1 == 0 && key2 == 0
                    ? new GaGridEvenSingleKeyZero<T>(Value) 
                    : new GaGridEvenSingleKey<T>(key1, key2, Value);

            return new GaGridGradedSingleGrade<T>(grade, evenList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetRow(ulong key1)
        {
            return key1 == 0
                ? GaListEvenEmpty<T>.EmptyList
                : new GaListEvenSingleKeyZero<T>(Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetColumn(ulong key2)
        {
            return key2 == 0
                ? GaListEvenEmpty<T>.EmptyList
                : new GaListEvenSingleKeyZero<T>(Value);
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
            yield return new GaRecordKeyPairValue<T>(0UL, 0UL, Value);
        }

    }
}