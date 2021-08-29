using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class LaMatrixColumn<TMatrix, TScalar> :
        LaMatrixColumnBase<TMatrix, TScalar>
    {
        internal LaMatrixColumn([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex)
            : base(matrixProcessor, matrix, columnIndex)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<TScalar> GetCopy()
        {
            return new LaMatrixColumn<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex
            );
        }
    }
}