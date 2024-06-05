namespace GeometricAlgebraFulcrumLib.Samples.Generations.Structures;

public sealed class GaDenseKVectorStorage :
    IGaKVectorStorage
{
    private readonly double[] _scalarArray;


    public int VSpaceDimensions { get; }

    public int Grade { get; }

    public int SparseCount 
        => _scalarArray.Count(s => s != 0);
    

    public GaDenseKVectorStorage(int vSpaceDimensions, int grade, double[] scalarArray)
    {
        VSpaceDimensions = vSpaceDimensions;
        Grade = grade;
        _scalarArray = scalarArray;
    }


    public double GetScalarByIndex(int index)
    {
        return _scalarArray[index];
    }

    public double GetScalarById(ulong id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs()
    {
        for (var i = 0; i < _scalarArray.Length; i++)
        {
            var scalar = _scalarArray[i];
            
            if (scalar == 0d) continue;
            
            yield return new KeyValuePair<int, double>(i, scalar);
        }
    }

    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        throw new NotImplementedException();
    }
}