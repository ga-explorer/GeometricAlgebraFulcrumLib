using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Lists.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenSparse<T> :
        IGaListEvenSparse<T>
    {
        private readonly Dictionary<ulong, T> _keyValueDictionary;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _keyValueDictionary.Count;
        }

        public T this[int key]
        {
            get => _keyValueDictionary[(ulong) key];
            set
            {
                if (_keyValueDictionary.ContainsKey((ulong) key))
                    _keyValueDictionary[(ulong) key] = value;
                else
                    _keyValueDictionary.Add((ulong) key, value);
            }
        }

        public T this[ulong key]
        {
            get => _keyValueDictionary[key];
            set
            {
                if (_keyValueDictionary.ContainsKey(key))
                    _keyValueDictionary[key] = value;
                else
                    _keyValueDictionary.Add(key, value);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(ulong key)
        {
            return _keyValueDictionary.TryGetValue(key, out var value)
                ? value
                : default;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<ulong> GetKeys()
        {
            return _keyValueDictionary.Keys;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _keyValueDictionary.Values;
        }


        internal GaListEvenSparse()
        {
            _keyValueDictionary = new Dictionary<ulong, T>();
        }

        internal GaListEvenSparse([NotNull] Dictionary<ulong, T> valueDictionary)
        {
            _keyValueDictionary = valueDictionary;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> Clear()
        {
            _keyValueDictionary.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> SetValue(ulong key, [NotNull] T value)
        {
            if (_keyValueDictionary.ContainsKey(key))
                _keyValueDictionary[key] = value;
            else
                _keyValueDictionary.Add(key, value);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> AddValue(ulong key, [NotNull] T value)
        {
            _keyValueDictionary.Add(key, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> Remove(ulong key)
        {
            _keyValueDictionary.Remove(key);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> Remove(params ulong[] keysList)
        {
            foreach (var key in keysList)
                _keyValueDictionary.Remove(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenSparse<T> Remove(IEnumerable<ulong> keysList)
        {
            foreach (var key in keysList.ToArray())
                _keyValueDictionary.Remove(key);

            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(ulong key)
        {
            return _keyValueDictionary.ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(ulong key, out T value)
        {
            return _keyValueDictionary.TryGetValue(key, out value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _keyValueDictionary.Count == 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMinKey()
        {
            if (_keyValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _keyValueDictionary.Keys.Min();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public ulong GetMaxKey()
        {
            if (_keyValueDictionary.Count == 0)
                throw new InvalidOperationException();

            return _keyValueDictionary.Keys.Max();
        }

        public IEnumerable<ulong> GetEmptyKeys(ulong maxKey)
        {
            var keysList = maxKey.GetRange().ToList();

            foreach (var key in _keyValueDictionary.Keys) 
                keysList.Remove(key);

            return keysList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> GetCopy()
        {
            return _keyValueDictionary
                .CopyToDictionary()
                .CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> MapKeys(Func<ulong, ulong> keyMapping)
        {
            return _keyValueDictionary
                .ToDictionary(
                    pair => keyMapping(pair.Key),
                    pair => pair.Value
                )
                .CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            return _keyValueDictionary
                .CopyToDictionary(valueMapping)
                .CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping)
        {
            return _keyValueDictionary.ToDictionary(
                pair => pair.Key,
                pair => keyValueMapping(pair.Key, pair.Value)
            ).CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKey(Func<ulong, bool> keyFilter)
        {
            return _keyValueDictionary
                .Where(pair => keyFilter(pair.Key))
                .CopyToDictionary()
                .CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter)
        {
            return _keyValueDictionary
                .Where(pair => keyValueFilter(pair.Key, pair.Value))
                .CopyToDictionary()
                .CreateEvenList();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaListEven<T> FilterByValue(Func<T, bool> valueFilter)
        {
            return _keyValueDictionary
                .Where(pair => valueFilter(pair.Value))
                .CopyToDictionary()
                .CreateEvenList();
        }

        public IGaListGraded<T> ToGradedList(Func<ulong, GaRecordGradeKey> evenKeyToGradeKeyMapping)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, Dictionary<ulong, T>>();

            foreach (var (id, value) in _keyValueDictionary)
            {
                var (grade, index) = evenKeyToGradeKeyMapping(id);

                if (!gradeKeyValueDictionary.TryGetValue(grade, out var keyValueDictionary))
                {
                    keyValueDictionary = new Dictionary<ulong, T>();
                    gradeKeyValueDictionary.Add(grade, keyValueDictionary);
                }

                if (keyValueDictionary.ContainsKey(index))
                    keyValueDictionary[index] = value;
                else
                    keyValueDictionary.Add(index, value);
            }

            return gradeKeyValueDictionary.CreateGradedList();
        }

        public bool TryGetCompactList(out IGaListEven<T> evenList)
        {
            if (_keyValueDictionary.Count == 0)
            {
                evenList = GaListEvenEmpty<T>.EmptyList;
                return true;
            }

            if (_keyValueDictionary.Count == 1)
            {
                var (key, value) = _keyValueDictionary.First();

                evenList = key == 0UL
                    ? new GaListEvenSingleKeyZero<T>(value)
                    : new GaListEvenSingleKey<T>(key, value);

                return true;
            }

            evenList = this;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordKeyValue<T>> GetKeyValueRecords()
        {
            return _keyValueDictionary.Select(
                pair => new GaRecordKeyValue<T>(pair.Key, pair.Value)
            );
        }
    }
}