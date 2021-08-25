using System;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDensePermuted<T> :
        GaListEvenDenseImmutableBase<T>
    {
        public IGaListEvenDense<T> SourceList { get; }

        public Func<ulong, ulong> KeyMapping { get; }

        public override int Count 
            => SourceList.Count;
        

        internal GaListEvenDensePermuted([NotNull] IGaListEvenDense<T> source, [NotNull] Func<ulong, ulong> keyMapping)
        {
            SourceList = source;
            KeyMapping = keyMapping;
        }


        public override T GetValue(ulong key)
        {
            return SourceList.GetValue(KeyMapping(key));
        }

        public override IGaListEven<T> GetCopy()
        {
            return this;
        }
    }
}