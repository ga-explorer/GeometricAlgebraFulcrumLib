using System.Runtime.CompilerServices;

namespace DataStructuresLib.Basic
{
    public sealed record UInt64IndexedFloat64 :
        IUInt64IndexedValue<double>
    {
        public ulong Index { get; }

        public double Value { get; }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64IndexedFloat64(ulong index, double value)
        {
            Index = index;
            Value = value;
        }
    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64IndexedFloat64(IUInt64IndexedValue<double> indexedValue)
        {
            Index = indexedValue.Index;
            Value = indexedValue.Value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out ulong index, out double value)
        {
            index = Index;
            value = Value;
        }
    }
}