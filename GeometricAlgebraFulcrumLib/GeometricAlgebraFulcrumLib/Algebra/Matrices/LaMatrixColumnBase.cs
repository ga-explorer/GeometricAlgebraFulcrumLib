using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public abstract class LaMatrixColumnBase<TMatrix, TScalar> :
        LaVectorDenseStorageBase<TScalar>
    {
        public ILaProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int ColumnIndex { get; }

        public override int Count
            => MatrixProcessor.GetDenseRowsCount(MatrixStorage);


        protected LaMatrixColumnBase([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex)
        {
            if (columnIndex < 0 || columnIndex >= matrixProcessor.GetDenseColumnsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(columnIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            ColumnIndex = columnIndex;
        }
    }
}