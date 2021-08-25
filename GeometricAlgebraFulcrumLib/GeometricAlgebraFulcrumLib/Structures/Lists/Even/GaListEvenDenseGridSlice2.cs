using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseGridSlice2<T> :
        GaListEvenDenseImmutableBase<T>
    {
        public IGaGridEven<T> SourceGrid { get; }

        public ulong Key2 { get; set; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count 
            => SourceGrid.GetDenseCount2();
        
        
        internal GaListEvenDenseGridSlice2([NotNull] IGaGridEven<T> array, ulong key2, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            Key2 = key2;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong index)
        {
            return SourceGrid.GetValue(Key2, index, DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            return new GaListEvenDenseGridSlice2<T>(SourceGrid, Key2, DefaultValueFunc);
        }
    }
}