namespace GeometricAlgebraFulcrumLib.Generations.Structures;

public sealed record GaEmptyKVectorStorage(int VSpaceDimensions, int Grade) :
    IGaKVectorStorage
{
    public int SparseCount 
        => 0;


    public double GetScalarByIndex(int index)
    {
        return 0d;
    }

    public double GetScalarById(ulong id)
    {
        return 0d;
    }

    public IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs()
    {
        return Enumerable.Empty<KeyValuePair<int, double>>();
    }

    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        return Enumerable.Empty<KeyValuePair<ulong, double>>();
    }

    public GaEmptyKVectorStorage Negative()
    {
        return this;
    }

    public GaEmptyKVectorStorage MapScalars(Func<double, double> scalarMap)
    {
        return this;
    }
    
    public GaEmptyKVectorStorage MapScalars(Func<ulong, double, double> scalarMap)
    {
        return this;
    }
    
    public GaEmptyKVectorStorage MapTerms(Func<ulong, double, Tuple<ulong, double>> termMap)
    {
        return this;
    }
}