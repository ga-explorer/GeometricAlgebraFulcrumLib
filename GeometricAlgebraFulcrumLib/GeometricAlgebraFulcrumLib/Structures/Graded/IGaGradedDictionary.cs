using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Graded
{
    public interface IGaGradedDictionary<T> :
        IGaDictionary<T>, 
        IReadOnlyDictionary<uint, IGaEvenDictionary<T>>
    {
        IEnumerable<uint> Grades { get; }

        IEnumerable<IGaEvenDictionary<T>> EvenDictionaries { get; }

        uint GetMaxGrade();
                
        uint GetFirstGrade();

        uint GetLastGrade();

        IGaEvenDictionary<T> GetFirstEvenDictionary();

        IGaEvenDictionary<T> GetLastEvenDictionary();
        
        KeyValuePair<uint, IGaEvenDictionary<T>> GetFirstPair();

        KeyValuePair<uint, IGaEvenDictionary<T>> GetLastPair();

        IEnumerable<KeyValuePair<ulong, T>> GetKeyValuePairs(Func<uint, ulong, ulong> gradeKeyToKeyMapping);

        IEnumerable<Tuple<ulong, T>> GetKeyValueTuples(Func<uint, ulong, ulong> gradeKeyToKeyMapping);

        IEnumerable<Tuple<uint, ulong, T>> GetGradeKeyValueTuples();

        IGaGradedDictionary<T> GetCopy();

        IGaGradedDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping);

        IGaGradedDictionary<T2> MapValues<T2>(Func<uint, ulong, T, T2> gradeKeyValueMapping);

        IGaGradedDictionary<T> FilterByGrade(Func<ulong, bool> gradeFilter);

        IGaGradedDictionary<T> FilterByGradeValue(Func<uint, T, bool> gradeValueFilter);

        IGaGradedDictionary<T> FilterByGradeKeyValue(Func<uint, ulong, T, bool> gradeKeyValueFilter);

        IGaGradedDictionary<T> FilterByValue(Func<T, bool> valueFilter);

        IGaEvenDictionary<T> ToEvenDictionary();

        IGaEvenDictionary<T> ToEvenDictionary(Func<uint, ulong, ulong> gradeKeyToEvenKeyMapping);
    }
}