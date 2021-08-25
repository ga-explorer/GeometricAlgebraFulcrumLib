using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Graded
{
    public interface IGaGridGraded<T> :
        IGaGrid<T>, IGaCollectionGraded<T>
    {
        IEnumerable<IGaGridEven<T>> GetGrids();

        IEnumerable<GaRecordGradeEvenGrid<T>> GetGradeGridRecords();

        IEnumerable<GaRecordGradeKeyPair> GetGradeKeyRecords();

        IEnumerable<GaRecordGradeKeyPairValue<T>> GetGradeKeyValueRecords();

        IGaGridEven<T> GetGrid(uint grade);

        T GetValue(uint grade, ulong key1, ulong key2);

        T GetValue(GaRecordGradeKeyPair gradeKey);

        T GetValue(uint grade, GaRecordKeyPair key);
        
        bool ContainsKey(uint grade, ulong key1, ulong key2);
        
        bool ContainsKey(uint grade, GaRecordKeyPair key);

        bool TryGetGrid(uint grade, out IGaGridEven<T> evenGrid);

        bool TryGetValue(uint grade, GaRecordKeyPair key, out T value);

        bool TryGetValue(uint grade, ulong key1, ulong key2, out T value);

        IGaGridGraded<T> GetCopy();

        IGaGridGraded<T2> MapValues<T2>(Func<T, T2> valueMapping);
        
        IGaGridGraded<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping);

        IGaGridGraded<T2> MapValues<T2>(Func<uint, ulong, ulong, T, T2> gradeKeyValueMapping);

        IGaGridGraded<T> FilterByGrade(Func<uint, bool> gradeFilter);

        IGaGridGraded<T> FilterByKey(Func<ulong, ulong, bool> keyFilter);

        IGaGridGraded<T> FilterByGradeKey(Func<uint, ulong, ulong, bool> gradeKeyFilter);

        IGaGridGraded<T> FilterByValue(Func<T, bool> valueFilter);

        IGaGridGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter);

        IGaGridGraded<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter);

        IGaGridGraded<T> FilterByGradeKeyValue(Func<uint, ulong, ulong, T, bool> gradeKeyValueFilter);
        
        bool TryGetCompactGrid(out IGaGridGraded<T> gradedGrid);

        IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);

        IGaGridEven<T> ToEvenGrid(Func<uint, ulong, ulong, GaRecordKeyPair> gradeKeyToEvenKeyMapping);
    }
}