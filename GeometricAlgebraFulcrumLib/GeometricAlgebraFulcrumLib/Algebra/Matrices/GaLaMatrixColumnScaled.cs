using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixColumnScaled<TMatrix, TScalar> :
        GaLaMatrixColumnBase<TMatrix, TScalar>
    {
        public TScalar ScalingFactor { get; set; }


        internal GaLaMatrixColumnScaled([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] TScalar scalingFactor)
            : base(matrixProcessor, matrix, columnIndex)
        {
            ScalingFactor = scalingFactor;
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetValue(ulong index)
        {
            return MatrixProcessor.Times(
                ScalingFactor,
                MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixColumnScaled<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                ColumnIndex,
                ScalingFactor
            );
        }
    }
}