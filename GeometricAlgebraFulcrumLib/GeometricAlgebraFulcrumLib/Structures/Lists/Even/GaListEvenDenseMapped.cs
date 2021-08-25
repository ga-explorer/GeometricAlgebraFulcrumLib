using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseMapped<T> :
        GaListEvenDenseImmutableBase<T>
    {
        public IGaListEvenDense<T> SourceList { get; }

        public Func<ulong, T, T> ValueMapping { get; }

        public override int Count 
            => SourceList.Count;
        

        internal GaListEvenDenseMapped([NotNull] IGaListEvenDense<T> source, [NotNull] Func<ulong, T, T> valueMapping)
        {
            SourceList = source;
            ValueMapping = valueMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key)
        {
            return ValueMapping(key, SourceList.GetValue(key));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            return this;
        }
    }
}