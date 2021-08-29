using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class LaMatrixRowScaled<TMatrix, TScalar> :
        LaMatrixRowBase<TMatrix, TScalar>
    {
        public TScalar ScalingFactor { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return MatrixProcessor.Times(
                ScalingFactor,
                MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index)
            );
        }


        internal LaMatrixRowScaled([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex, [NotNull] TScalar scalingFactor)
            : base(matrixProcessor, matrix, rowIndex)
        {
            ScalingFactor = scalingFactor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<TScalar> GetCopy()
        {
            return new LaMatrixRowScaled<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                RowIndex, ScalingFactor);
        }
    }
}