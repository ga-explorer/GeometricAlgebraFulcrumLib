using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed record GaListEvenSingleKeyZero<T> :
        IGaListEvenSingleKey<T>,
        IGaListEvenDenseMutable<T>
    {
        public ulong Key 
            => 0UL;

        public T Value { get; set; }

        public int Count 
            => 1;

        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public T this[ulong index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }


        internal GaListEvenSingleKeyZero([NotNull] T value)
        {
            Value = value;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key)
        {
            return key == 0UL
                ? Value
                : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            yield return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            yield return Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return key == 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            if (key == 0UL)
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
            return 0UL;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetEmptyKeys(ulong maxKey)
        {
            return maxKey > 0
                ? (maxKey - 1).GetRange(1)
                : Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            var key = keyMapping(0UL);

            return key == 0UL
                ? new GaListEvenSingleKeyZero<T>(Value)
                : new GaListEvenSingleKey<T>(key, Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaListEvenSingleKeyZero<T2>(valueMapping(Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return new GaListEvenSingleKeyZero<T2>(keyValueMapping(0UL, Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return keyFilter(0UL)
                ? this : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return keyValueFilter(0UL, Value)
                ? this : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return valueFilter(Value)
                ? this : GaListEvenEmpty<T>.EmptyList;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var (grade, index) = evenKeyToGradeKeyMapping(0UL);

            IGaListEven<T> evenDictionary = 
                index == 0
                    ? new GaListEvenSingleKeyZero<T>(Value) 
                    : new GaListEvenSingleKey<T>(index, Value);

            return new GaListGradedSingleGrade<T>(grade, evenDictionary);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            evenList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            yield return new GaRecordKeyValue<T>(0UL, Value);
        }
    }
}