using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public abstract class LinMatrixColumnBase<TMatrix, TScalar> :
        LinVectorDenseStorageBase<TScalar>
    {
        public ILinearAlgebraProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int ColumnIndex { get; }

        public override int Count
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);


        protected LinMatrixColumnBase([NotNull] ILinearAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= matrixProcessor.GetDenseColumnsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            ColumnIndex = columnIndex;
        }
    }
}