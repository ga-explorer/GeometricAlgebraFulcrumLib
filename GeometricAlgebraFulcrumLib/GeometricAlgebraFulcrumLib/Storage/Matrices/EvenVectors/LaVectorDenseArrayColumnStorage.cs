using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorDenseArrayColumnStorage<T> :
        LaVectorImmutableDenseStorageBase<T>
    {
        public ILaMatrixEvenStorage<T> SourceGrid { get; }

        public ulong ColumnIndex { get; set; }

        public Func<ulong, ulong, T> DefaultValueFunc { get; }

        public override int Count 
            => SourceGrid.GetDenseCount2();
        
        
        internal LaVectorDenseArrayColumnStorage([NotNull] ILaMatrixEvenStorage<T> array, ulong index2, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            ColumnIndex = index2;
            DefaultValueFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return SourceGrid.GetValue(ColumnIndex, index, DefaultValueFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return new LaVectorDenseArrayColumnStorage<T>(SourceGrid, ColumnIndex, DefaultValueFunc);
        }
    }
}