using System;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public abstract class LinMatrixColumnBase<TMatrix, TScalar> :
        LinVectorDenseStorageBase<TScalar>
    {
        public IMatrixAlgebraProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int ColumnIndex { get; }

        public override int Count
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);


        protected LinMatrixColumnBase(IMatrixAlgebraProcessor<TMatrix, TScalar> matrixProcessor, TMatrix matrix, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= matrixProcessor.GetDenseColumnsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            ColumnIndex = columnIndex;
        }
    }
}