using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseGridSlice<T> :
        GaListEvenDenseImmutableBase<T>
    {
        public IGaGridEven<T> SourceGrid { get; }

        public Func<ulong, GaRecordKeyPair> KeyMapping { get; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count { get; }
        

        internal GaListEvenDenseGridSlice([NotNull] IGaGridEven<T> array, int count, [NotNull] Func<ulong, GaRecordKeyPair> keyMapping, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            Count = count;
            KeyMapping = keyMapping;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong index)
        {
            return SourceGrid.GetValue(KeyMapping(index), DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            return this;
        }
    }
}