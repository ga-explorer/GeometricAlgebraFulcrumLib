using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseRepeatedValue<T> :
        IGaGridEvenDenseImmutable<T>
    {
        public T Value { get; set; }

        public int Count1 { get; }

        public int Count2 { get; }

        public int Count { get; }
        
        public T this[int index1, int index2] 
            => GetValue((ulong) index1, (ulong) index2);

        public T this[ulong index1, ulong index2] 
            => GetValue(index1, index2);


        internal GaGridEvenDenseRepeatedValue(int count1, int count2, [NotNull] T value)
        {
            Count1 = count1;
            Count2 = count2;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return Count1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count1 * Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys1()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            yield return Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key1, ulong key2)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey1()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey2()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMinKey()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            throw new NotImplementedException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetCopy()
        {
            return this;
        }

        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T> Transpose()
        {
            return new GaGridEvenDenseRepeatedValue<T>(Count2, Count1, Value);
        }

        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            throw new NotImplementedException();
        }

        public IGaListEven<T> GetRow(ulong key1)
        {
            throw new NotImplementedException();
        }

        public IGaListEven<T> GetColumn(ulong key2)
        {
            throw new NotImplementedException();
        }

        public bool TryGetCompactGrid(out IGaGridEven<T> evenGrid)
        {
            throw new NotImplementedException();
        }
    }
}