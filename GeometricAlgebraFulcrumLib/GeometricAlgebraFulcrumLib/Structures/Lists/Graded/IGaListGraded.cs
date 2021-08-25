using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Graded
{
    public interface IGaListGraded<T> :
        IGaList<T>, IGaCollectionGraded<T>
    {
        IEnumerable<IGaListEven<T>> GetLists();

        IEnumerable<GaRecordGradeEvenList<T>> GetGradeListRecords();

        IEnumerable<GaRecordGradeKey> GetGradeKeyRecords();

        IEnumerable<GaRecordGradeKeyValue<T>> GetGradeKeyValueRecords();

        IGaListEven<T> GetList(uint grade);

        T GetValue(uint grade, ulong key);

        T GetValue(GaRecordGradeKey gradeKey);
        
        bool ContainsKey(uint grade, ulong key);

        bool TryGetList(uint grade, out IGaListEven<T> evenList);

        bool TryGetValue(uint grade, ulong key, out T value);
        
        IGaListGraded<T> GetCopy();

        IGaListGraded<T2> MapValues<T2>(Func<T, T2> valueMapping);

        IGaListGraded<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping);

        IGaListGraded<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping);

        IGaListGraded<T> FilterByGrade(Func<uint, bool> gradeFilter);

        IGaListGraded<T> FilterByKey(Func<ulong, bool> keyFilter);

        IGaListGraded<T> FilterByGradeKey(Func<uint, ulong, bool> gradeKeyFilter);

        IGaListGraded<T> FilterByValue(Func<T, bool> valueFilter);

        IGaListGraded<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter);

        IGaListGraded<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter);

        IGaListGraded<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter);
        
        bool TryGetCompactList(out IGaListGraded<T> gradedList);

        IGaListEven<T> ToEvenList(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);
    }
}