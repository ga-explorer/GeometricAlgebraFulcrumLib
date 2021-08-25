using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Structures.Factories;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public sealed record GaGridGradedDenseList<T> :
        IGaGridGraded<T>
    {
        private readonly List<IGaGridEven<T>> _evenGrids;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<uint> GetGrades()
        {
            return ((uint) _evenGrids.Count).GetRange();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<IGaGridEven<T>> GetGrids()
        {
            return _evenGrids;
        }

        public int GradesCount 
            => _evenGrids.Count;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<T> GetValues()
        {
            return _evenGrids.SelectMany(evenList => evenList.GetValues());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount1()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount1());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetSparseCount2()
        {
            return _evenGrids.Sum(evenGrid => evenGrid.GetSparseCount2());
        }


        internal GaGridGradedDenseList()
        {
            _evenGrids = new List<IGaGridEven<T>>();
        }

        internal GaGridGradedDenseList(int capacity)
        {
            _evenGrids = new List<IGaGridEven<T>>(capacity);
        }

        internal GaGridGradedDenseList([NotNull] IEnumerable<IGaGridEven<T>> evenGrids)
        {
            _evenGrids = new List<IGaGridEven<T>>(
                evenGrids.Select(
                    list => list.IsNullOrEmpty() ? GaGridEvenEmpty<T>.EmptyGrid : list
                )
            );
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> Clear()
        {
            _evenGrids.Clear();
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> SetGrid(int index, IGaGridEven<T> evenGrid)
        {
            _evenGrids[index] = evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> SetGrid(uint index, IGaGridEven<T> evenGrid)
        {
            _evenGrids[(int) index] = evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid;
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> AppendGrid(IGaGridEven<T> evenGrid)
        {
            _evenGrids.Add(evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> PrependGrid(IGaGridEven<T> evenGrid)
        {
            _evenGrids.Insert(0, evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> InsertGrid(int index, IGaGridEven<T> evenGrid)
        {
            _evenGrids.Insert(index, evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> InsertGrid(uint index, IGaGridEven<T> evenGrid)
        {
            _evenGrids.Insert((int) index, evenGrid ?? GaGridEvenEmpty<T>.EmptyGrid);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> Remove(int index)
        {
            _evenGrids.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaGridGradedDenseList<T> Remove(uint index)
        {
            _evenGrids.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsEmpty()
        {
            return _evenGrids.Count == 0 || 
                   _evenGrids.All(d => d.IsNullOrEmpty());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IGaGridEven<T> GetGrid(uint grade)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade] 
                : GaGridEvenEmpty<T>.EmptyGrid;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, ulong key1, ulong key2)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetValue(key1, key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(GaRecordGradeKeyPair gradeKey)
        {
            var (grade, key1, key2) = gradeKey;

            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetValue(key1, key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public T GetValue(uint grade, GaRecordKeyPair key)
        {
            return grade < _evenGrids.Count
                ? _evenGrids[(int) grade].GetValue(key)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsGrade(uint grade)
        {
            return grade < _evenGrids.Count;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, ulong key1, ulong key2)
        {
            return grade < _evenGrids.Count && 
                   _evenGrids[(int) grade].ContainsKey(key1, key2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool ContainsKey(uint grade, GaRecordKeyPair key)
        {
            return grade < _evenGrids.Count && 
                   _evenGrids[(int) grade].ContainsKey(key);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetGrid(uint grade, out IGaGridEven<T> evenGrid)
        {
            if (grade < _evenGrids.Count)
            {
                evenGrid = _evenGrids[(int) grade];
                return true;
            }

            evenGrid = null;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, GaRecordKeyPair key, out T value)
        {
            if (grade < _evenGrids.Count)
                return _evenGrids[(int) grade].TryGetValue(key, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryGetValue(uint grade, ulong key1, ulong key2, out T value)
        {
            if (grade < _evenGrids.Count)
                return _evenGrids[(int) grade].TryGetValue(key1, key2, out value);

            value = default;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMinGrade()
        {
            return 0U;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetMaxGrade()
        {
            return (uint) (_evenGrids.Count - 1);
        }

        public IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords()
        {
            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (key1, key2) in evenDictionary.GetKeys())
                    yield return new GaRecordGradeKeyPair(grade, key1, key2);
            }
        }

        public IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords()
        {
            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (key1, key2, value) in evenDictionary.GetKeyValueRecords())
                    yield return new GaRecordGradeKeyPairValue<T>(grade, key1, key2, value);
            }
        }

        public IGaGridGraded<T> GetCopy()
        {
            var evenDictionariesGrid = new IGaGridEven<T>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
                evenDictionariesGrid[grade] = 
                    _evenGrids[(int) grade];

            return new GaGridGradedDenseList<T>(evenDictionariesGrid);
        }

        public IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping)
        {
            var evenDictionariesGrid = new IGaGridEven<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade].MapValues(valueMapping);
            }

            return new GaGridGradedDenseList<T2>(evenDictionariesGrid);
        }

        public IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping)
        {
            var evenDictionariesGrid = new IGaGridEven<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade].MapValues(keyValueMapping);
            }

            return new GaGridGradedDenseList<T2>(evenDictionariesGrid);
        }

        public IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
        {
            var evenDictionariesGrid = new IGaGridEven<T2>[_evenGrids.Count];

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                evenDictionariesGrid[grade] =
                    _evenGrids[(int) grade]
                        .MapValues((key1, key2, value) => 
                            gradeKeyValueMapping(g, key1, key2, value)
                        );
            }

            return new GaGridGradedDenseList<T2>(evenDictionariesGrid);
        }

        public IGaGridGraded<T> FilterByGrade(Func<uint, bool> gradeFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                if (gradeFilter(grade))
                    continue;

                var evenDictionary = 
                    _evenGrids[(int) grade];

                if (evenDictionary.IsEmpty())
                    continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByKey(keyFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByKey(
                    (key1, key2) => 
                        gradeKeyFilter(g, key1, key2)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByValue(valueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByValue(
                    value => gradeValueFilter(g, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = 
                    _evenGrids[(int) grade].FilterByKeyValue(keyValueFilter);

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
        {
            var gradeKeyValueDictionary = new Dictionary<uint, IGaGridEven<T>>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var g = grade;

                var evenDictionary = _evenGrids[(int) grade].FilterByKeyValue(
                    (key1, key2, value) => 
                        gradeKeyValueFilter(g, key1, key2, value)
                );

                if (evenDictionary.IsEmpty()) continue;

                gradeKeyValueDictionary.Add(grade, evenDictionary);
            }

            return gradeKeyValueDictionary.CreateGradedGrid();
        }

        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (key1, key2, value) in evenDictionary.GetKeyValueRecords())
                    keyValueDictionary.Add(
                        new GaRecordKeyPair(
                            gradeKeyToEvenKeyMapping(grade, key1), 
                            gradeKeyToEvenKeyMapping(grade, key2)
                        ),
                        value
                    );
            }

            return keyValueDictionary.CreateEvenGrid();
        }

        public IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping)
        {
            var keyValueDictionary = new Dictionary<GaRecordKeyPair, T>();

            for (var grade = 0U; grade < _evenGrids.Count; grade++)
            {
                var evenDictionary = _evenGrids[(int) grade];

                foreach (var (key1, key2, value) in evenDictionary.GetKeyValueRecords())
                    keyValueDictionary.Add(
                        gradeKeyToEvenKeyMapping(grade, key1, key2), 
                        value
                    );
            }

            return keyValueDictionary.CreateEvenGrid();
        }

        public bool TryGetCompactGrid(out IGaGridGraded<T> gradedGrid)
        {
            if (GradesCount > 1)
            {
                var flag = false;
                for (var i = 0; i < _evenGrids.Count; i++)
                {
                    if (!_evenGrids[i].TryGetCompactGrid(out var evenGrid)) 
                        continue;

                    flag = true;
                    _evenGrids[i] = evenGrid;
                }

                gradedGrid = this;
                return flag;
            }

            if (GradesCount == 0)
            {
                gradedGrid = GaGridGradedEmpty<T>.EmptyGrid;
                return true;
            }

            gradedGrid = _evenGrids[0].GetCompactGrid().CreateGradedGridSingleGrade(0);
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerable<GaRecordGradeEvenGrid<T>> GetGradeGridRecords()
        {
            return _evenGrids.Select(
                (evenGrid, grade) => new GaRecordGradeEvenGrid<T>((uint) grade, evenGrid)
            );
        }
    }
}