using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseColumn<T> :
        GaGridEvenDenseImmutableBase<T>
    {
        public IGaListEvenDense<T> SourceList { get; }

        public override int Count1 
            => SourceList.Count;

        public override int Count2 
            => 1;


        internal GaGridEvenDenseColumn([NotNull] IGaListEvenDense<T> sourceList)
        {
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            return key2 == 0
                ? SourceList.GetValue(key1)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return this;
        }
    }
}