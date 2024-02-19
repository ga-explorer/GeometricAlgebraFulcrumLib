using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.BitManipulation;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Records;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Graded;

public sealed record LinMatrixSparseGradedStorage<T> :
    ILinMatrixGradedStorage<T>
{
    private readonly Dictionary<uint, ILinMatrixStorage<T>> _gradeIndexScalarDictionary;


    public int GradesCount 
        => _gradeIndexScalarDictionary.Count;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<uint> GetGrades()
    {
        return _gradeIndexScalarDictionary.Keys;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<uint> GetEmptyGrades(uint vSpaceDimensions)
    {
        return (1 + vSpaceDimensions).GetRange().Except(_gradeIndexScalarDictionary.Keys);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<ILinMatrixStorage<T>> GetMatrixStorages()
    {
        return _gradeIndexScalarDictionary.Values;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetSparseCount()
    {
        return _gradeIndexScalarDictionary.Values.Sum(matrixStorage => matrixStorage.GetSparseCount());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<T> GetScalars()
    {
        return _gradeIndexScalarDictionary.Values.SelectMany(matrixStorage => matrixStorage.GetScalars());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetSparseCount1()
    {
        return _gradeIndexScalarDictionary.Values.Sum(matrixStorage => matrixStorage.GetSparseCount1());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetSparseCount2()
    {
        return _gradeIndexScalarDictionary.Values.Sum(matrixStorage => matrixStorage.GetSparseCount2());
    }


    internal LinMatrixSparseGradedStorage()
    {
        _gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();
    }

    internal LinMatrixSparseGradedStorage(Dictionary<uint, ILinMatrixStorage<T>> gradeIndexScalarDictionary)
    {
        _gradeIndexScalarDictionary = gradeIndexScalarDictionary;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixSparseGradedStorage<T> Clear()
    {
        _gradeIndexScalarDictionary.Clear();
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixSparseGradedStorage<T> Remove(uint grade)
    {
        _gradeIndexScalarDictionary.Remove(grade);
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixSparseGradedStorage<T> SetMatrixStorage(uint grade, ILinMatrixStorage<T> matrixStorage)
    {
        if (_gradeIndexScalarDictionary.ContainsKey(grade))
            _gradeIndexScalarDictionary[grade] = matrixStorage;
        else
            _gradeIndexScalarDictionary.Add(grade, matrixStorage);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public LinMatrixSparseGradedStorage<T> AddMatrixStorage(uint grade, ILinMatrixStorage<T> matrixStorage)
    {
        _gradeIndexScalarDictionary.Add(grade, matrixStorage);

        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty()
    {
        return _gradeIndexScalarDictionary
            .Values
            .All(dict => dict.IsEmpty());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinMatrixStorage<T> GetMatrixStorage(uint grade)
    {
        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage)
            ? matrixStorage 
            : LinMatrixEmptyStorage<T>.EmptyStorage;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(uint grade, ulong index1, ulong index2)
    {
        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
               matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(RGaGradeKvIndexPairRecord gradeKey)
    {
        var (grade, index1, index2) = gradeKey;

        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
               matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T GetScalar(uint grade, RGaKvIndexPairRecord index)
    {
        var (index1, index2) = index;

        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
               matrixStorage.TryGetScalar(index1, index2, out var value)
            ? value
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsGrade(uint grade)
    {
        return _gradeIndexScalarDictionary.ContainsKey(grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(uint grade, ulong index1, ulong index2)
    {
        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
               matrixStorage.ContainsIndex(index1, index2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsIndex(uint grade, RGaKvIndexPairRecord index)
    {
        return _gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage) &&
               matrixStorage.ContainsIndex(index);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetMatrixStorage(uint grade, out ILinMatrixStorage<T> matrixStorage)
    {
        return _gradeIndexScalarDictionary.TryGetValue(grade, out matrixStorage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalar(uint grade, RGaKvIndexPairRecord index, out T value)
    {
        if (_gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage))
            return matrixStorage.TryGetScalar(index, out value);
            
        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetScalar(uint grade, ulong index1, ulong index2, out T value)
    {
        if (_gradeIndexScalarDictionary.TryGetValue(grade, out var matrixStorage))
            return matrixStorage.TryGetScalar(index1, index2, out value);
            
        value = default;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint GetMinGrade()
    {
        return _gradeIndexScalarDictionary.Keys.TryGetMinValue(out var grade)
            ? grade
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public uint GetMaxGrade()
    {
        return _gradeIndexScalarDictionary.Keys.TryGetMaxValue(out var grade)
            ? grade
            : throw new InvalidOperationException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaGradeKvIndexPairRecord> GetGradeIndexRecords()
    {
        return _gradeIndexScalarDictionary.SelectMany(pair => 
            pair.Value.GetIndices().Select(index => 
                new RGaGradeKvIndexPairRecord(pair.Key, index.KvIndex1, index.KvIndex2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaGradeKvIndexPairScalarRecord<T>> GetGradeIndexScalarRecords()
    {
        return _gradeIndexScalarDictionary.SelectMany(pair => 
            pair.Value.GetIndexScalarRecords().Select(indexValuePair => 
                new RGaGradeKvIndexPairScalarRecord<T>(
                    pair.Key,
                    indexValuePair.KvIndex1,
                    indexValuePair.KvIndex2,
                    indexValuePair.Scalar
                )
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinMatrixGradedStorage<T> GetCopy()
    {
        return _gradeIndexScalarDictionary
            .ToDictionary(matrixStorage => matrixStorage.GetCopy())
            .CreateLinMatrixGradedStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<T, T2> valueMapping)
    {
        return _gradeIndexScalarDictionary
            .ToDictionary(matrixStorage => matrixStorage.MapScalars(valueMapping))
            .CreateLinMatrixGradedStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<ulong, ulong, T, T2> indexValueMapping)
    {
        return _gradeIndexScalarDictionary
            .ToDictionary(matrixStorage => matrixStorage.MapScalars(indexValueMapping))
            .CreateLinMatrixGradedStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ILinMatrixGradedStorage<T2> MapScalars<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping)
    {
        return _gradeIndexScalarDictionary
            .ToDictionary(
                pair => pair.Key,
                pair => pair.Value.MapScalars(
                    (index1, index2, value) => gradeKeyValueMapping(pair.Key, index1, index2, value))
            )
            .CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByGrade(Func<uint, bool> gradeFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            if (!gradeFilter(grade) || matrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, matrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByIndex(Func<ulong, ulong, bool> indexFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByIndex(indexFilter);

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByGradeIndex(Func<uint, ulong, ulong, bool> gradeKeyFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByIndex((index1, index2) => gradeKeyFilter(grade, index1, index2));

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByScalar(Func<T, bool> valueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByScalar(valueFilter);

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByGradeScalar(Func<uint, T, bool> gradeValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByScalar(value => gradeValueFilter(grade, value));

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByIndexScalar(Func<ulong, ulong, T, bool> indexValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByIndexScalar(indexValueFilter);

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixGradedStorage<T> FilterByGradeIndexScalar(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter)
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var filteredMatrixStorage = 
                matrixStorage.FilterByIndexScalar((index1, index2, value) => 
                    gradeKeyValueFilter(grade, index1, index2, value)
                );

            if (filteredMatrixStorage.IsEmpty()) 
                continue;

            gradeIndexScalarDictionary.Add(grade, filteredMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong> gradeIndexToIndexMapping)
    {
        var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            foreach (var (index1, index2, value) in matrixStorage.GetIndexScalarRecords())
                indexValueDictionary.Add(
                    new RGaKvIndexPairRecord(
                        gradeIndexToIndexMapping(grade, index1), 
                        gradeIndexToIndexMapping(grade, index2)
                    ), 
                    value
                );
        }

        return indexValueDictionary.CreateLinMatrixStorage();
    }

    public ILinMatrixStorage<T> ToMatrixStorage(Func<uint, ulong, ulong, RGaKvIndexPairRecord> gradeIndexToIndexMapping)
    {
        var indexValueDictionary = new Dictionary<RGaKvIndexPairRecord, T>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        foreach (var (index1, index2, scalar) in matrixStorage.GetIndexScalarRecords())
            indexValueDictionary.Add(
                gradeIndexToIndexMapping(grade, index1, index2), 
                scalar
            );

        return indexValueDictionary.CreateLinMatrixStorage();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetRows(uint grade)
    {
        return TryGetMatrixStorage(grade, out var matrixStorage) 
            ? matrixStorage.GetRows() 
            : Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaGradeKvIndexLinVectorStorageRecord<T>> GetRows()
    {
        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        foreach (var (index, vectorStorage) in matrixStorage.GetRows())
            yield return new RGaGradeKvIndexLinVectorStorageRecord<T>(grade, index, vectorStorage);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetColumns(uint grade)
    {
        return TryGetMatrixStorage(grade, out var matrixStorage) 
            ? matrixStorage.GetColumns() 
            : Enumerable.Empty<RGaKvIndexLinVectorStorageRecord<T>>();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<RGaGradeKvIndexLinVectorStorageRecord<T>> GetColumns()
    {
        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        foreach (var (index, vectorStorage) in matrixStorage.GetColumns())
            yield return new RGaGradeKvIndexLinVectorStorageRecord<T>(grade, index, vectorStorage);
    }

    public ILinMatrixGradedStorage<T> GetTranspose()
    {
        var gradeIndexScalarDictionary = new Dictionary<uint, ILinMatrixStorage<T>>();

        foreach (var (grade, matrixStorage) in _gradeIndexScalarDictionary)
        {
            var transposedMatrixStorage = 
                matrixStorage.GetTranspose();

            gradeIndexScalarDictionary.Add(grade, transposedMatrixStorage);
        }

        return gradeIndexScalarDictionary.CreateLinMatrixGradedStorage();
    }

    public bool TryGetCompactStorage(out ILinMatrixGradedStorage<T> matrixGradedStorage)
    {
        if (_gradeIndexScalarDictionary.Count == 0)
        {
            matrixGradedStorage = LinMatrixEmptyGradedStorage<T>.EmptyStorage;
            return true;
        }

        if (_gradeIndexScalarDictionary.Count == 1)
        {
            var (grade, matrixStorage) = _gradeIndexScalarDictionary.First();

            matrixGradedStorage = matrixStorage.TryGetCompactStorage(out var matrixStorage1)
                ? matrixStorage1.CreateLinMatrixSingleGradeStorage(grade)
                : matrixStorage.CreateLinMatrixSingleGradeStorage(grade);

            return true;
        }

        matrixGradedStorage = this;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<GaGradeLinMatrixStorageRecord<T>> GetGradeStorageRecords()
    {
        return _gradeIndexScalarDictionary.Select(pair => 
            new GaGradeLinMatrixStorageRecord<T>(pair.Key, pair.Value)
        );
    }
}