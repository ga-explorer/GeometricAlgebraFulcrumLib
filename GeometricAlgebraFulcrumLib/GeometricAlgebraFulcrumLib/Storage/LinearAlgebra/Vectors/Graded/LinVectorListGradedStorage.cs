using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Graded;

public sealed record LinVectorListGradedStorage<T> :
    ILinVectorGradedStorage<T>
{
    private readonly List<ILinVectorStorage<T>> _evenLists;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<uint> GetGrades()
    {
        return ((uint) _evenLists.Count).GetRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimensions)
    {
        var count = (uint) _evenLists.Count;

        return vSpaceDimensions > count
            ? (1U + vSpaceDimensions - count).GetRange(count)
            : (1U + vSpaceDimensions).GetRange();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ILinVectorStorage<T>> GetVectorStorages()
    {
        return _evenLists;
    }

    public int GradesCount 
        => _evenLists.Count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetSparseCount()
    {
        return _evenLists.Sum(vectorStorage => vectorStorage.GetSparseCount());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetScalars()
    {
        return _evenLists.SelectMany(vectorStorage => vectorStorage.GetScalars());
    }


    internal LinVectorListGradedStorage()
    {
        _evenLists = new List<ILinVectorStorage<T>>();
    }

    internal LinVectorListGradedStorage(int capacity)
    {
        _evenLists = new List<ILinVectorStorage<T>>(capacity);
    }

    internal LinVectorListGradedStorage(IEnumerable<ILinVectorStorage<T>> evenLists)
    {
        _evenLists = new List<ILinVectorStorage<T>>(
            evenLists.Select(
                list => list.IsNullOrEmpty() ? LinVectorEmptyStorage<T>.EmptyStorage : list
            )
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> Clear()
    {
        _evenLists.Clear();
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> SetList(int index, ILinVectorStorage<T> vectorStorage)
    {
        _evenLists[index] = vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> SetList(uint index, ILinVectorStorage<T> vectorStorage)
    {
        _evenLists[(int) index] = vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage;
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> AppendList(ILinVectorStorage<T> vectorStorage)
    {
        _evenLists.Add(vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> PrependList(ILinVectorStorage<T> vectorStorage)
    {
        _evenLists.Insert(0, vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> InsertList(int index, ILinVectorStorage<T> vectorStorage)
    {
        _evenLists.Insert(index, vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> InsertList(uint index, ILinVectorStorage<T> vectorStorage)
    {
        _evenLists.Insert((int) index, vectorStorage ?? LinVectorEmptyStorage<T>.EmptyStorage);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> Remove(int index)
    {
        _evenLists.RemoveAt(index);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinVectorListGradedStorage<T> Remove(uint index)
    {
        _evenLists.RemoveAt((int) index);
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return _evenLists.Count == 0 || 
               _evenLists.All(d => d.IsNullOrEmpty());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinVectorStorage<T> GetVectorStorage(uint grade)
    {
        return grade < _evenLists.Count
            ? _evenLists[(int) grade] 
            : LinVectorEmptyStorage<T>.EmptyStorage;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(uint grade, ulong index)
    {
        return grade < _evenLists.Count
            ? _evenLists[(int) grade].GetScalar(index)
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(RGaGradeKvIndexRecord gradeKey)
    {
        var (grade, index) = gradeKey;

        return grade < _evenLists.Count
            ? _evenLists[(int) grade].GetScalar(index)
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsGrade(uint grade)
    {
        return grade < _evenLists.Count;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(uint grade, ulong index)
    {
        return grade < _evenLists.Count && 
               _evenLists[(int) grade].ContainsIndex(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetVectorStorage(uint grade, out ILinVectorStorage<T> vectorStorage)
    {
        if (grade < _evenLists.Count)
        {
            vectorStorage = _evenLists[(int) grade];
            return true;
        }

        vectorStorage = null;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalar(uint grade, ulong index, out T value)
    {
        if (grade < _evenLists.Count)
            return _evenLists[(int) grade].TryGetScalar(index, out value);

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
        return (uint) (_evenLists.Count - 1);
    }

    public IEnumerable<RGaGradeKvIndexRecord> GetGradeIndexRecords()
    {
        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = _evenLists[(int) grade];

            foreach (var index in evenDictionary.GetIndices())
                yield return new RGaGradeKvIndexRecord(grade, index);
        }
    }

    public IEnumerable<RGaGradeKvIndexScalarRecord<T>> GetGradeIndexScalarRecords()
    {
        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = _evenLists[(int) grade];

            foreach (var (index, value) in evenDictionary.GetIndexScalarRecords())
                yield return new RGaGradeKvIndexScalarRecord<T>(grade, index, value);
        }
    }

    public ILinVectorGradedStorage<T> GetCopy()
    {
        var evenDictionariesList = new ILinVectorStorage<T>[_evenLists.Count];

        for (var grade = 0U; grade < _evenLists.Count; grade++)
            evenDictionariesList[grade] = 
                _evenLists[(int) grade];

        return new LinVectorListGradedStorage<T>(evenDictionariesList);
    }

    public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
    {
        var evenDictionariesList = new ILinVectorStorage<T2>[_evenLists.Count];

        for (var grade = 0U; grade < _evenLists.Count; grade++)
            evenDictionariesList[grade] = 
                _evenLists[(int) grade].MapScalars(valueMapping);

        return new LinVectorListGradedStorage<T2>(evenDictionariesList);
    }

    public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<ulong, T, T2> indexValueMapping)
    {
        var evenDictionariesList = new ILinVectorStorage<T2>[_evenLists.Count];

        for (var grade = 0U; grade < _evenLists.Count; grade++)
            evenDictionariesList[grade] = 
                _evenLists[(int) grade].MapScalars(indexValueMapping);

        return new LinVectorListGradedStorage<T2>(evenDictionariesList);
    }

    public ILinVectorGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping)
    {
        var evenDictionariesList = new ILinVectorStorage<T2>[_evenLists.Count];

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var g = grade;

            evenDictionariesList[grade] =
                _evenLists[(int) grade]
                    .MapScalars((index, value) => gradeKeyValueMapping(g, index, value));
        }

        return new LinVectorListGradedStorage<T2>(evenDictionariesList);
    }

    public ILinVectorGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            if (!gradeFilter(grade)) continue;

            var evenDictionary = 
                _evenLists[(int) grade];

            if (evenDictionary.IsEmpty())
                continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByIndex(Func<ulong, bool> indexFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = 
                _evenLists[(int) grade].FilterByIndex(indexFilter);

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, bool> gradeKeyFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var g = grade;

            var evenDictionary = _evenLists[(int) grade].FilterByIndex(
                index => gradeKeyFilter(g, index)
            );

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var g = grade;

            var evenDictionary = _evenLists[(int) grade].FilterByScalar(
                value => gradeValueFilter(g, value)
            );

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByIndexScalar(Func<ulong, T, bool> indexValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = 
                _evenLists[(int) grade].FilterByIndexScalar(indexValueFilter);

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, T, bool> gradeKeyValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var g = grade;

            var evenDictionary = _evenLists[(int) grade].FilterByIndexScalar(
                (index, value) => gradeKeyValueFilter(g, index, value)
            );

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinVectorStorage<T>>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = _evenLists[(int) grade].FilterByScalar(valueFilter);

            if (evenDictionary.IsEmpty()) continue;

            gradeIndexScalarDictionary.Add(grade, evenDictionary);
        }

        return gradeIndexScalarDictionary.CreateLinVectorGradedStorage();
    }

    public ILinVectorStorage<T> ToVectorStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
    {
        var indexValueDictionary = new Dictionary<ulong, T>();

        for (var grade = 0U; grade < _evenLists.Count; grade++)
        {
            var evenDictionary = _evenLists[(int) grade];

            foreach (var (index, value) in evenDictionary.GetIndexScalarRecords())
                indexValueDictionary.Add(
                    gradeIndexToIndexMapping(grade, index), 
                    value
                );
        }

        return indexValueDictionary.CreateLinVectorStorage();
    }

    public bool TryGetCompactStorage(out ILinVectorGradedStorage<T> vectorGradedStorage)
    {
        if (GradesCount > 1)
        {
            var flag = false;
            for (var i = 0; i < _evenLists.Count; i++)
            {
                if (!_evenLists[i].TryGetCompactStorage(out var vectorStorage)) 
                    continue;

                flag = true;
                _evenLists[i] = vectorStorage;
            }

            vectorGradedStorage = this;
            return flag;
        }

        if (GradesCount == 0)
        {
            vectorGradedStorage = LinVectorEmptyGradedStorage<T>.EmptyStorage;
            return true;
        }

        vectorGradedStorage = _evenLists[0].GetCompactList().CreateLinVectorSingleGradeStorage(0);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<GaGradeLinVectorStorageRecord<T>> GetGradeStorageRecords()
    {
        return _evenLists.Select(
            (vectorStorage, grade) => new GaGradeLinVectorStorageRecord<T>((uint) grade, vectorStorage)
        );
    }
}