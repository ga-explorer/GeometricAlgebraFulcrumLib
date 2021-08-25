using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseTransposed<T> :
        GaGridEvenDenseImmutableBase<T>
    {
        public GaGridEvenDenseBase<T> SourceGrid { get; }

        public override int Count1 
            => SourceGrid.Count1;

        public override int Count2 
            => SourceGrid.Count2;


        internal GaGridEvenDenseTransposed([NotNull] GaGridEvenDenseBase<T> source)
        {
            SourceGrid = source;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            return SourceGrid.GetValue(key2, key1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> Transpose()
        {
            return SourceGrid;
        }
    }
}