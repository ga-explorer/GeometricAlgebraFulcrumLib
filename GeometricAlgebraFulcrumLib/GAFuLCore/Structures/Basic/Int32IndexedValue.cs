using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

public sealed record Int32IndexedValue<T> :
    IInt32IndexedValue<T>
{
    public int Index { get; }

    public T Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32IndexedValue(int index, T value)
    {
        Index = index;
        Value = value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32IndexedValue(IInt32IndexedValue<T> indexedValue)
    {
        Index = indexedValue.Index;
        Value = indexedValue.Value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int index, out T value)
    {
        index = Index;
        value = Value;
    }
}