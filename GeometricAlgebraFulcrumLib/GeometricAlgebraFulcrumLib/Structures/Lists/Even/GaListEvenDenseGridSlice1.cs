using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Structures.Grids.Even;
using GeometricAlgebraFulcrumLib.Structures.Utils;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseGridSlice1<T> :
        GaListEvenDenseImmutableBase<T>
    {
        public IGaGridEven<T> SourceGrid { get; }

        public ulong Key1 { get; set; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count 
            => SourceGrid.GetDenseCount2();
        
        
        internal GaListEvenDenseGridSlice1([NotNull] IGaGridEven<T> array, ulong key1, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            Key1 = key1;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong index)
        {
            return SourceGrid.GetValue(Key1, index, DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            return new GaListEvenDenseGridSlice1<T>(SourceGrid, Key1, DefaultValueFunc);
        }
    }
}