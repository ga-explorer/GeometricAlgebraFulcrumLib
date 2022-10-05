using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed class LinMatrixColumnScaled<TMatrix, TScalar> :
        LinMatrixColumnBase<TMatrix, TScalar>
    {
        public TScalar ScalingFactor { get; set; }


        internal LinMatrixColumnScaled([NotNull] IMatrixAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] TScalar scalingFactor)
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
        public override ILinVectorStorage<TScalar> GetCopy()
        {
            return new LinMatrixColumnScaled<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                ColumnIndex,
                ScalingFactor
            );
        }
    }
}