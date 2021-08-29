using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class LaMatrixColumnScaled<TMatrix, TScalar> :
        LaMatrixColumnBase<TMatrix, TScalar>
    {
        public TScalar ScalingFactor { get; set; }


        internal LaMatrixColumnScaled([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] TScalar scalingFactor)
            : base(matrixProcessor, matrix, columnIndex)
        {
            ScalingFactor = scalingFactor;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return MatrixProcessor.Times(
                ScalingFactor,
                MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<TScalar> GetCopy()
        {
            return new LaMatrixColumnScaled<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                ColumnIndex,
                ScalingFactor
            );
        }
    }
}