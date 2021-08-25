using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public abstract class GaGridEvenDenseBase<T> :
        IGaGridEven<T>
    {
        public abstract int Count1 { get; }

        public abstract int Count2 { get; }

        public int Count 
            => Count1 * Count2;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return Count1 * Count2;
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

        public abstract T GetValue(ulong key1, ulong key2);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordKeyPair key)
        {
            return GetValue(key.Key1, key.Key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys1()
        {
            return ((ulong) Count1).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys2()
        {
            return ((ulong) Count2).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            return GaRecordsUtils.GetKeyPairsInRange(Count1, Count2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IEnumerable<T> GetValues()
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                yield return GetValue(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return Count1 == 0 || Count2 == 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key1, ulong key2)
        {
            return key1 < (ulong) Count1 && 
                   key2 < (ulong) Count2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(GaRecordKeyPair key)
        {
            var (key1, key2) = key;

            return key1 < (ulong) Count1 && key2 < (ulong) Count2;
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
            return new GaRecordKeyPair(0, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey1()
        {
            return (ulong) (Count1 - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey2()
        {
            return (ulong) (Count2 - 1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaRecordKeyPair GetMaxKey()
        {
            return new GaRecordKeyPair((ulong) (Count1 - 1), (ulong) (Count2 - 1));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            if (key1 < (ulong) Count1 && key2 < (ulong) Count2)
            {
                value = GetValue(key1, key2);
                return true;
            }

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            var (key1, key2) = key;

            if (key1 < (ulong) Count1 && key2 < (ulong) Count2)
            {
                value = GetValue(key1, key2);
                return true;
            }

            value = default;
            return false;
        }

        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            
            if (maxKey1 < count1)
            {
                if (maxKey2 < count2)
                {
                    yield break;
                }
                else
                {
                    for (var k1 = 0UL; k1 <= maxKey1; k1++)
                    for (var k2 = count2; k2 <= maxKey2; k2++)
                        yield return new GaRecordKeyPair(k1, k2);
                }
            }
            else
            {
                if (maxKey2 < (ulong) Count2)
                {
                    for (var k1 = count1; k1 <= maxKey1; k1++)
                    for (var k2 = 0UL; k2 <= maxKey2; k2++)
                        yield return new GaRecordKeyPair(k1, k2);
                }
                else
                {
                    for (var k1 = 0UL; k1 < count1; k1++)
                    for (var k2 = count2; k2 <= maxKey2; k2++)
                        yield return new GaRecordKeyPair(k1, k2);

                    for (var k1 = count1; k1 <= maxKey1; k1++)
                    for (var k2 = 0UL; k2 <= maxKey2; k2++)
                        yield return new GaRecordKeyPair(k1, k2);
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            var (maxKey1, maxKey2) = maxKey;

            return GetEmptyKeys(maxKey1, maxKey2);
        }

        public abstract IGaGridEven<T> GetCopy();

        public IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var (key1, key2) = keyMapping(k1, k2);

                valueDictionary.Add(
                    new GaRecordKeyPair(key1, key2),
                    GetValue(k1, k2)
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = valueMapping(GetValue(k1, k2));

            return valuesArray.CreateEvenGridDense();
        }

        public IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;
            var valuesArray = new T2[count1, count2];

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                valuesArray[k1, k2] = keyValueMapping(k1, k2, GetValue(k1, k2));

            return valuesArray.CreateEvenGridDense();
        }

        public IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                if (!keyFilter(k1, k2)) continue;

                valueDictionary.Add(
                    new GaRecordKeyPair(k1, k2),
                    GetValue(k1, k2)
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetValue(k1, k2);

                if (!keyValueFilter(k1, k2, value)) continue;

                valueDictionary.Add(
                    new GaRecordKeyPair(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        public IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var valueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetValue(k1, k2);

                if (!valueFilter(value)) continue;

                valueDictionary.Add(
                    new GaRecordKeyPair(k1, k2),
                    value
                );
            }

            return valueDictionary.CreateEvenGrid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual IGaGridEven<T> Transpose()
        {
            return new GaGridEvenDenseTransposed<T>(this);
        }

        public IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping)
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<GaRecordKeyPair, T>>();

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
            {
                var value = GetValue(k1, k2);
                var (grade, key1, key2) = evenKeyToGradeKeyMapping(k1, k2);

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
            return this.CreateEvenListDenseGridSlice2(key1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetColumn(ulong key2)
        {
            return this.CreateEvenListDenseGridSlice2(key2);
        }


        public bool TryGetCompactGrid(out IGaGridEven<T> evenGrid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            var count1 = (ulong) Count1;
            var count2 = (ulong) Count2;

            for (var k1 = 0UL; k1 < count1; k1++)
            for (var k2 = 0UL; k2 < count2; k2++)
                yield return new GaRecordKeyPairValue<T>(
                    k1, k2, GetValue(k1, k2)
                );
        }
    }
}