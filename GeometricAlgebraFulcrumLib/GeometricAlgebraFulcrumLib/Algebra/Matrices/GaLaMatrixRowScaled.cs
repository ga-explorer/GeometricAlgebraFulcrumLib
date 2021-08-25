using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixRowScaled<TMatrix, TScalar> :
        GaLaMatrixRowBase<TMatrix, TScalar>
    {
        public TScalar ScalingFactor { get; set; }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetValue(ulong index)
        {
            return MatrixProcessor.Times(
                ScalingFactor,
                MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index)
            );
        }


        internal GaLaMatrixRowScaled([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex, [NotNull] TScalar scalingFactor)
            : base(matrixProcessor, matrix, rowIndex)
        {
            ScalingFactor = scalingFactor;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixRowScaled<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                RowIndex, ScalingFactor);
        }
    }
}