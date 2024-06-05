using System.Collections;

namespace GeometricAlgebraFulcrumLib.MetaProgramming.Applications.CSharp.SparseKVectorsLib.SampleCode;

public static class Euclidean3DSpace
{
    public static uint VSpaceDimensions => 3u;

    public static uint GaSpaceDimensions => 8u;

    public static IReadOnlyList<uint> KvSpaceDimensions { get; }
        = new[] { 1u, 3u, 3u, 1u };
}

//public sealed partial class KVector
//{
//    private static void UpdateKVectorDictionary()

//    public double Sp(KVector kv2)
//    {
//        if (Grade != kv2.Grade)
//            return 0;


//    }
//}

public sealed partial class KVector :
    IReadOnlyDictionary<uint, double>
{
    private readonly Dictionary<uint, double> _indexScalarDictionary
        = new Dictionary<uint, double>();


    public int Count 
        => _indexScalarDictionary.Count;

    public uint Grade { get; }

    public uint MaxIndex 
        => Euclidean3DSpace.KvSpaceDimensions[(int) Grade];

    public double this[uint index]
    {
        get
        {
            if (index > MaxIndex)
                throw new IndexOutOfRangeException(nameof(index));

            return _indexScalarDictionary.TryGetValue(index, out var scalar)
                ? scalar : 0d;
        }
        set
        {
            if (index > MaxIndex)
                throw new IndexOutOfRangeException(nameof(index));

            if (value == 0d)
            {
                _indexScalarDictionary.Remove(index);

                return;
            }

            if (_indexScalarDictionary.ContainsKey(index))
                _indexScalarDictionary[index] = value;
            else
                _indexScalarDictionary.Add(index, value);
        }
    }

    public IEnumerable<uint> Keys 
        => _indexScalarDictionary.Keys;

    public IEnumerable<double> Values 
        => _indexScalarDictionary.Values;


    public KVector(uint grade)
    {
        Grade = grade;
    }


    public void Clear()
    {
        _indexScalarDictionary.Clear();
    }

    public void Remove(uint index)
    {
        _indexScalarDictionary.Remove(index);
    }

    public bool ContainsKey(uint index)
    {
        return _indexScalarDictionary.ContainsKey(index);
    }

    public bool TryGetValue(uint index, out double scalar)
    {
        return _indexScalarDictionary.TryGetValue(index, out scalar);
    }

    public IEnumerator<KeyValuePair<uint, double>> GetEnumerator()
    {
        return _indexScalarDictionary.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}