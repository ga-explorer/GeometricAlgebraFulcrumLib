using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;

public sealed record Int32IndexedFloat64 :
    IInt32IndexedValue<double>
{
    public int Index { get; }

    public double Value { get; }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32IndexedFloat64(int index, double value)
    {
        Index = index;
        Value = value;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32IndexedFloat64(IInt32IndexedValue<double> indexedValue)
    {
        Index = indexedValue.Index;
        Value = indexedValue.Value;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Deconstruct(out int index, out double value)
    {
        index = Index;
        value = Value;
    }
}