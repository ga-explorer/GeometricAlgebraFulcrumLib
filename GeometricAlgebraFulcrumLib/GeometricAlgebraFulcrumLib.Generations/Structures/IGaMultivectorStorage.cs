namespace GeometricAlgebraFulcrumLib.Generations.Structures
{
    public interface IGaKVectorStorage 
    {
        int VSpaceDimensions { get; }

        int Grade { get; }

        int SparseCount { get; }
        
        double GetScalarByIndex(int index);

        double GetScalarById(ulong id);

        IEnumerable<KeyValuePair<int, double>> GetIndexScalarPairs();

        IEnumerable<KeyValuePair<ulong, double>> GetIdScalarPairs();


    }
}
