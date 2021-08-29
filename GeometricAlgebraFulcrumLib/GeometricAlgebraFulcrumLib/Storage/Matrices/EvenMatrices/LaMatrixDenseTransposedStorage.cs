using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixDenseTransposedStorage<T> :
        LaMatrixImmutableDenseStorageBase<T>
    {
        public LaMatrixDenseStorageBase<T> SourceGrid { get; }

        public override int Count1 
            => SourceGrid.Count1;

        public override int Count2 
            => SourceGrid.Count2;


        internal LaMatrixDenseTransposedStorage([NotNull] LaMatrixDenseStorageBase<T> source)
        {
            SourceGrid = source;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return SourceGrid.GetScalar(index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> Transpose()
        {
            return SourceGrid;
        }
    }
}