using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed record GaGridEvenEmpty<T> :
        IGaGridEvenDense<T>, 
        IGaGridEvenSparse<T>
    {
        public static GaGridEvenEmpty<T> EmptyGrid { get; }
            = new GaGridEvenEmpty<T>();


        public int Count1 
            => 0;

        public int Count2 
            => 0;

        public int Count 
            => 0;


        private GaGridEvenEmpty()
        {
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key1, ulong key2)
        {
            throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys1()
        {
            return Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            return Enumerable.Empty<ulong>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            return Enumerable.Empty<GaRecordKeyPair>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return Enumerable.Empty<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey1()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey2()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMinKey()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new GaRecordKeyPair(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            var (maxKey1, maxKey2) = maxKey;

            for (var k1 = 0UL; k1 <= maxKey1; k1++)
            for (var k2 = 0UL; k2 <= maxKey2; k2++)
                yield return new GaRecordKeyPair(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetCopy()
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return GaGridEvenEmpty<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return GaGridEvenEmpty<T2>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> Transpose()
        {
            return EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            return GaGridGradedEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetRow(ulong key1)
        {
            return GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetColumn(ulong key2)
        {
            return GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetCompactGrid(out IGaGridEven<T> evenGrid)
        {
            evenGrid = GaGridEvenEmpty<T>.EmptyGrid;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            return Enumerable.Empty<GaRecordKeyPairValue<T>>();
        }
    }
}