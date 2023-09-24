namespace GeometricAlgebraFulcrumLib.Generations.Structures;

public sealed class GaSparseKVectorStorage :
    IGaKVectorStorage
{
    private readonly Dictionary<ulong, double> _idScalarDictionary;


    public int VSpaceDimensions { get; }

    public int Grade { get; }

    public int SparseCount 
        => _idScalarDictionary.Count;

    public double this[ulong id]
    {
        get => _idScalarDictionary.TryGetValue(id, out var scalar)
                ? scalar : 0d;
        set
        {
            if (value == 0d)
            {
                _idScalarDictionary.Remove(id);

                return;
            }

            if (_idScalarDictionary.ContainsKey(id))
                _idScalarDictionary[id] = value;
            else
                _idScalarDictionary.Add(id, value);
        }
    }

    
    public GaSparseKVectorStorage(int vSpaceDimensions, int grade)
    {
        _idScalarDictionary = new Dictionary<ulong, double>();
        VSpaceDimensions = vSpaceDimensions;
        Grade = grade;
    }

    public GaSparseKVectorStorage(int vSpaceDimensions, int grade, Dictionary<ulong, double> idScalarDictionary)
    {
        _idScalarDictionary = idScalarDictionary;
        VSpaceDimensions = vSpaceDimensions;
        Grade = grade;
    }

    public double GetScalarByIndex(int index)
    {
        throw new NotImplementedException();
    }

    public double GetScalarById(ulong id)
    {
        return _idScalarDictionary.TryGetValue(id, out var scalar)
            ? scalar : 0d;
    }

    public IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        return _idScalarDictionary;
    }

    
}