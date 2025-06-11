namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record UInt64IndexedFloat64 :
    IUInt64IndexedValue<double>
    {
    public ulong Index { get; }

    public double Value { get; }


    
    public UInt64IndexedFloat64(ulong index, double value)
    {
        Index = index;
        Value = value;
    }
    
    
    public UInt64IndexedFloat64(IUInt64IndexedValue<double> indexedValue)
    {
        Index = indexedValue.Index;
        Value = indexedValue.Value;
    }


    
    public void Deconstruct(out ulong index, out double value)
    {
        index = Index;
        value = Value;
    }
    }
}