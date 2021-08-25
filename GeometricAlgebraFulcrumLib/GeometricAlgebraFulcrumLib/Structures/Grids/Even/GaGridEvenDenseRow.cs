using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseRow<T> :
        GaGridEvenDenseImmutableBase<T>
    {
        public IGaListEvenDense<T> SourceList { get; }

        public override int Count1 
            => 1;

        public override int Count2 
            => SourceList.Count;


        internal GaGridEvenDenseRow([NotNull] IGaListEvenDense<T> sourceList)
        {
            SourceList = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            return key1 == 0
                ? SourceList.GetValue(key2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return this;
        }
    }
}