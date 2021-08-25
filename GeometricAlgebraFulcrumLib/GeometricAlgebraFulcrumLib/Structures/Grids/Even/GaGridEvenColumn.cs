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
    public sealed class GaGridEvenColumn<T> :
        IGaGridEvenSparse<T>
    {
        public ulong Key2 { get; }

        public IGaListEven<T> SourceList { get; }


        internal GaGridEvenColumn(ulong key2, [NotNull] IGaListEven<T> sourceList)
        {
            Key2 = key2;
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return SourceList.IsEmpty();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return SourceList.GetValues();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return SourceList.GetSparseCount();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return SourceList.IsEmpty() ? 0 : 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys1()
        {
            return SourceList.GetKeys();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            if (!SourceList.IsEmpty()) 
                yield return Key2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            return SourceList.GetKeys().Select(key => new GaRecordKeyPair(key, Key2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            return SourceList.GetKeyValueRecords().Select(keyValue => new GaRecordKeyPairValue<T>(keyValue.Key, Key2, keyValue.Value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            var maxKey = new GaRecordKeyPair(maxKey1, maxKey2);

            return new HashSet<GaRecordKeyPair>(maxKey.GetKeyPairsInRange())
                .Except(SourceList
                    .GetKeys()
                    .Select(key => new GaRecordKeyPair(key, Key2))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            return new HashSet<GaRecordKeyPair>(maxKey.GetKeyPairsInRange())
                .Except(SourceList
                    .GetKeys()
                    .Select(key => new GaRecordKeyPair(key, Key2))
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key1, ulong key2)
        {
            return key2 == Key2
                ? SourceList.GetValue(key1)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            var (key1, key2) = key;

            return key2 == Key2
                ? SourceList.GetValue(key1)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            return key2 == Key2 && SourceList.ContainsKey(key1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            var (key1, key2) = key;

            return key2 == Key2 && SourceList.ContainsKey(key1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey1()
        {
            return SourceList.GetMinKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey2()
        {
            return !SourceList.IsEmpty()
                ? Key2 
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMinKey()
        {
            var key = SourceList.GetMinKey();

            return new GaRecordKeyPair(key, Key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            return SourceList.GetMaxKey();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            return !SourceList.IsEmpty()
                ? Key2 
                : throw new InvalidOperationException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            var key = SourceList.GetMaxKey();

            return new GaRecordKeyPair(key, Key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            if (key2 == Key2 && SourceList.TryGetValue(key1, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            var (key1, key2) = key;

            if (key2 == Key2 && SourceList.TryGetValue(key1, out value))
                return true;

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetCopy()
        {
            return this;
        }

        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            var keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();

            foreach (var (k, value) in SourceList.GetKeyValueRecords())
            {
                var (key1, key2) = keyMapping(k, Key2);

                var key = new GaRecordKeyPair(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = value;
                else
                    keyValueDictionary.Add(key, value);
            }

            return keyValueDictionary.CreateEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return new GaGridEvenDiagonal<T2>(SourceList.MapValues(valueMapping));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            return new GaGridEvenDiagonal<T2>(SourceList.MapValues((key, value) => keyValueMapping(key, Key2, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            return new GaGridEvenDiagonal<T>(SourceList.FilterByKey(key => keyFilter(key, Key2)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            return new GaGridEvenDiagonal<T>(SourceList.FilterByKeyValue((key, value) => keyValueFilter(key, Key2, value)));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return new GaGridEvenDiagonal<T>(SourceList.FilterByValue(valueFilter));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> Transpose()
        {
            return this;
        }

        public bool TryGetCompactGrid(out IGaGridEven<T> evenGrid)
        {
            if (SourceList.TryGetCompactList(out var sourceList))
            {
                var count = sourceList.GetSparseCount();

                evenGrid = count switch
                {
                    0 => GaGridEvenEmpty<T>.EmptyGrid,
                    1 => sourceList.GetMinKeyValueRecord().CreateEvenGridSingleKey(),
                    _ => new GaGridEvenColumn<T>(Key2, sourceList)
                };

                return true;
            }
            else
            {
                var count = SourceList.GetSparseCount();

                if (count > 1)
                {
                    evenGrid = this;
                    return false;
                }
                
                evenGrid = count == 0
                    ? GaGridEvenEmpty<T>.EmptyGrid
                    : sourceList.GetMinKeyValueRecord().CreateEvenGridSingleKey();

                return true;
            }
        }

        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<GaRecordKeyPair, T>>();

            foreach (var (k, value) in SourceList.GetKeyValueRecords())
            {
                var (grade, key1, key2) = evenKeyToGradeKeyMapping(k, Key2);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                var key = new GaRecordKeyPair(key1, key2); 

                if (keyValueDictionary.ContainsKey(key))
                    keyValueDictionary[key] = value;
                else
                    keyValueDictionary.Add(key, value);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetRow(ulong key1)
        {
            return SourceList.TryGetValue(key1, out var value)
                ? value.CreateEvenListSingleKey(key1)
                : GaListEvenEmpty<T>.EmptyList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetColumn(ulong key2)
        {
            return key2 == Key2
                ? SourceList
                : GaListEvenEmpty<T>.EmptyList;
        }
    }
}