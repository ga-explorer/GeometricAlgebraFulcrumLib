namespace GeometricAlgebraFulcrumLib.Samples.Generations.Structures;

public sealed record GaTermKVectorStorage(int VSpaceDimensions, ulong Id, double Scalar) :
    IGaKVectorStorage
{
    public int Grade 
        => (int) ulong.PopCount(Id);

    public int SparseCount 
        => Scalar != 0 ? 1 : 0;


    public double GetScalarByIndex(int index)
    {
        throw new NotImplementedException();
    }

    public double GetScalarById(ulong id)
    {
        return id == Id ? Scalar : 0d;
    }

    public IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs()
    {
        if (Scalar != 0)
            yield return new KeyValuePair<ulong, double>(Id, Scalar);
    }
}