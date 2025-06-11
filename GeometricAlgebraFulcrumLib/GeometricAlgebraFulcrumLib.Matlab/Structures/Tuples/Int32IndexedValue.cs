namespace GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples
{
    public sealed record Int32IndexedValue<T> :
    IInt32IndexedValue<T>
    {
    public int Index { get; }

    public T Value { get; }


    
    public Int32IndexedValue(int index, T value)
    {
        Index = index;
        Value = value;
    }
    
    
    public Int32IndexedValue(IInt32IndexedValue<T> indexedValue)
    {
        Index = indexedValue.Index;
        Value = indexedValue.Value;
    }


    
    public void Deconstruct(out int index, out T value)
    {
        index = Index;
        value = Value;
    }
    }
}