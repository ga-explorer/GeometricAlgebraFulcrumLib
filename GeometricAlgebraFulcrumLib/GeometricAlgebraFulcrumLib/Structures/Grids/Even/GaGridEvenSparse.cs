using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed record GaGridEvenSparse<T> :
        IGaGridEvenSparse<T>
    {
        private readonly Dictionary<GaRecordKeyPair, T> _keyValueDictionary;


        public int GetSparseCount1()
        {
            return GetKeys1().Count();
        }

        public int GetSparseCount2()
        {
            return GetKeys2().Count();
        }

        public int GetSparseCount()
        {
            return _keyValueDictionary.Count;
        }

        public T GetValue(ulong key1, ulong key2)
        {
            var keyPair = new GaRecordKeyPair(key1, key2);

            return _keyValueDictionary.TryGetValue(keyPair, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        public T GetValue(GaRecordKeyPair key)
        {
            return _keyValueDictionary.TryGetValue(key, out var value)
                ? value
                : throw new KeyNotFoundException();
        }

        public IEnumerable<ulong> GetKeys1()
        {
            return _keyValueDictionary.Keys.Select(key => key.Key1).Distinct();
        }

        public IEnumerable<ulong> GetKeys2()
        {
            return _keyValueDictionary.Keys.Select(key => key.Key2).Distinct();
        }

        public IEnumerable<GaRecordKeyPair> GetKeys()
        {
            return _keyValueDictionary.Keys;
        }

        public IEnumerable<T> GetValues()
        {
            return _keyValueDictionary.Values;
        }


        internal GaGridEvenSparse()
        {
            _keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();
        }

        internal GaGridEvenSparse([NotNull] Dictionary<GaRecordKeyPair, T> keyValueDictionary)
        {
            _keyValueDictionary = keyValueDictionary;
        }


        public void Clear()
        {
            _keyValueDictionary.Clear();
        }

        public void SetValue(GaRecordKeyPair key, [NotNull] T value)
        {
            if (_keyValueDictionary.ContainsKey(key))
                _keyValueDictionary[key] = value;
            else
                _keyValueDictionary.Add(key, value);
        }

        public void AddValue(GaRecordKeyPair key, [NotNull] T value)
        {
            _keyValueDictionary.Add(key, value);
        }

        public bool Remove(GaRecordKeyPair key)
        {
            return _keyValueDictionary.Remove(key);
        }

        public void Remove(params GaRecordKeyPair[] keysList)
        {
            foreach (var key in keysList)
                _keyValueDictionary.Remove(key);
        }

        public void Remove(IEnumerable<GaRecordKeyPair> keysList)
        {
            foreach (var key in keysList.ToArray())
                _keyValueDictionary.Remove(key);
        }

        public bool IsEmpty()
        {
            return _keyValueDictionary.Count == 0;
        }

        public bool ContainsKey(ulong key1, ulong key2)
        {
            var keyPair = new GaRecordKeyPair(key1, key2);

            return _keyValueDictionary.ContainsKey(keyPair);
        }

        public ulong GetMinKey1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMinKey2()
        {
            throw new NotImplementedException();
        }

        public GaRecordKeyPair GetMinKey()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxKey1()
        {
            throw new NotImplementedException();
        }

        public ulong GetMaxKey2()
        {
            throw new NotImplementedException();
        }

        public GaRecordKeyPair GetMaxKey()
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(GaRecordKeyPair key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(GaRecordKeyPair key, out T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(ulong key1, ulong key2, out T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey)
        {
            throw new NotImplementedException();
        }

        public IGaGridEven<T> GetCopy()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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

        public IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords()
        {
            throw new NotImplementedException();
        }
    }
}