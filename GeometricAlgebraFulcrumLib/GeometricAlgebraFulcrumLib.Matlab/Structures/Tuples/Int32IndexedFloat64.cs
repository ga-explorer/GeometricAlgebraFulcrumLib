namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record Int32IndexedFloat64 :
    IInt32IndexedValue<double>
    {
    public int Index { get; }

    public double Value { get; }


    
    public Int32IndexedFloat64(int index, double value)
    {
        Index = index;
        Value = value;
    }
    
    
    public Int32IndexedFloat64(IInt32IndexedValue<double> indexedValue)
    {
        Index = indexedValue.Index;
        Value = indexedValue.Value;
    }


    
    public void Deconstruct(out int index, out double value)
    {
        index = Index;
        value = Value;
    }
    }
}