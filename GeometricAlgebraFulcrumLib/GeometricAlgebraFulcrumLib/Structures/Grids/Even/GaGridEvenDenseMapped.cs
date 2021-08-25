using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseMapped<T> :
        GaGridEvenDenseImmutableBase<T>
    {
        public GaGridEvenDenseBase<T> SourceGrid { get; }

        public Func<ulong, ulong, T, T> ValueMapping { get; }

        public override int Count1 
            => SourceGrid.Count1;

        public override int Count2 
            => SourceGrid.Count2;


        internal GaGridEvenDenseMapped([NotNull] GaGridEvenDenseBase<T> source, [NotNull] Func<ulong, ulong, T, T> valueMapping)
        {
            SourceGrid = source;
            ValueMapping = valueMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            return ValueMapping(key1, key2, SourceGrid.GetValue(key1, key2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return this;
        }
    }
}