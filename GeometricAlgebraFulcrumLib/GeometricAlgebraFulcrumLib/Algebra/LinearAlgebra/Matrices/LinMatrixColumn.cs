using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed class LinMatrixColumn<TMatrix, TScalar> :
        LinMatrixColumnBase<TMatrix, TScalar>
    {
        internal LinMatrixColumn(IMatrixProcessor<TMatrix, TScalar> matrixProcessor, TMatrix matrix, int columnIndex)
            : base(matrixProcessor, matrix, columnIndex)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<TScalar> GetCopy()
        {
            return new LinMatrixColumn<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex
            );
        }
    }
}