using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Graded;

namespace GeometricAlgebraFulcrumLib.Structures.Even
{
    public interface IGaEvenDictionary<T> :
        IGaDictionary<T>, 
        IReadOnlyDictionary<ulong, T>
    {
        ulong GetMaxBasisBladeId(uint grade);
                
        ulong GetFirstKey();

        ulong GetLastKey();

        T GetFirstValue();

        T GetLastValue();

        KeyValuePair<ulong, T> GetFirstPair();

        KeyValuePair<ulong, T> GetLastPair();

        IGaEvenDictionary<T> GetCopy();

        IGaEvenDictionary<T> MapKeys(Func<ulong, ulong> keyMapping);

        IGaEvenDictionary<T2> MapValues<T2>(Func<T, T2> valueMapping);

        IGaEvenDictionary<T2> MapValues<T2>(Func<ulong, T, T2> keyValueMapping);

        IGaEvenDictionary<T> FilterByKey(Func<ulong, bool> keyFilter);

        IGaEvenDictionary<T> FilterByKeyValue(Func<ulong, T, bool> keyValueFilter);

        IGaEvenDictionary<T> FilterByValue(Func<T, bool> valueFilter);

        IGaGradedDictionary<T> ToGradedDictionary();

        IGaGradedDictionary<T> ToGradedDictionary(Func<ulong, Tuple<uint, ulong>> evenKeyToGradeKeyMapping);
    }
}