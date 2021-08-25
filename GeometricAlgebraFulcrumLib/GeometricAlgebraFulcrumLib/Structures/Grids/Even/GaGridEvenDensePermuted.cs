using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDensePermuted<T> :
        GaGridEvenDenseImmutableBase<T>
    {
        public GaGridEvenDenseBase<T> SourceGrid { get; }

        public Func<ulong, ulong, GaRecordKeyPair> KeyMapping { get; }

        public override int Count1 
            => SourceGrid.Count1;

        public override int Count2 
            => SourceGrid.Count2;


        internal GaGridEvenDensePermuted([NotNull] GaGridEvenDenseBase<T> source, [NotNull] Func<ulong, ulong, GaRecordKeyPair> keyMapping)
        {
            SourceGrid = source;
            KeyMapping = keyMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            var (k1, k2) = KeyMapping(key1, key2);

            return SourceGrid.GetValue(k1, k2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return this;
        }
    }
}