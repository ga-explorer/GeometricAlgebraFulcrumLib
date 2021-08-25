using System;
using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Structures.Grids.Graded;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public interface IGaGridEven<T> :
        IGaGrid<T>
    {
        IEnumerable<ulong> GetKeys1();

        IEnumerable<ulong> GetKeys2();

        IEnumerable<GaRecordKeyPair> GetKeys();

        IEnumerable<GaRecordKeyPairValue<T>> GetKeyValueRecords();
        
        IEnumerable<GaRecordKeyPair> GetEmptyKeys(ulong maxKey1, ulong maxKey2);

        IEnumerable<GaRecordKeyPair> GetEmptyKeys(GaRecordKeyPair maxKey);

        T GetValue(ulong key1, ulong key2);

        T GetValue(GaRecordKeyPair key);

        bool ContainsKey(ulong key1, ulong key2);

        bool ContainsKey(GaRecordKeyPair key);
        
        ulong GetMinKey1();

        ulong GetMinKey2();

        GaRecordKeyPair GetMinKey();

        ulong GetMaxKey1();

        ulong GetMaxKey2();

        GaRecordKeyPair GetMaxKey();

        bool TryGetValue(ulong key1, ulong key2, out T value);

        bool TryGetValue(GaRecordKeyPair key, out T value);

        IGaGridEven<T> GetCopy();
        
        IGaGridEven<T> MapKeys(Func<ulong, ulong, GaRecordKeyPair> keyMapping);

        IGaGridEven<T2> MapValues<T2>(Func<T, T2> valueMapping);

        IGaGridEven<T2> MapValues<T2>(Func<ulong, ulong, T, T2> keyValueMapping);

        IGaGridEven<T> FilterByKey(Func<ulong, ulong, bool> keyFilter);

        IGaGridEven<T> FilterByKeyValue(Func<ulong, ulong, T, bool> keyValueFilter);

        IGaGridEven<T> FilterByValue(Func<T, bool> valueFilter);

        IGaGridEven<T> Transpose();
        
        bool TryGetCompactGrid(out IGaGridEven<T> evenGrid);
        
        IGaGridGraded<T> ToGradedGrid(Func<ulong, ulong, GaRecordGradeKeyPair> evenKeyToGradeKeyMapping);

        IGaListEven<T> GetRow(ulong key1);

        IGaListEven<T> GetColumn(ulong key2);
    }
}