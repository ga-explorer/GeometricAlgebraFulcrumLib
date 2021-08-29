using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices;
using GeometricAlgebraFulcrumLib.Utilities.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorDenseArrayRowStorage<T> :
        LaVectorImmutableDenseStorageBase<T>
    {
        public ILaMatrixEvenStorage<T> SourceGrid { get; }

        public ulong RowIndex { get; set; }

        public Func<ulong, ulong, T> DefaultScalarFunc { get; }

        public override int Count 
            => SourceGrid.GetDenseCount2();
        
        
        internal LaVectorDenseArrayRowStorage([NotNull] ILaMatrixEvenStorage<T> array, ulong index1, [NotNull] Func<ulong, ulong, T> defaultValueFunc)
        {
            SourceGrid = array;
            RowIndex = index1;
            DefaultScalarFunc = defaultValueFunc;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return SourceGrid.GetValue(RowIndex, index, DefaultScalarFunc);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return new LaVectorDenseArrayRowStorage<T>(SourceGrid, RowIndex, DefaultScalarFunc);
        }
    }
}