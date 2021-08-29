using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public abstract class LaMatrixRowBase<TMatrix, TScalar> :
        LaVectorDenseStorageBase<TScalar>
    {
        public ILaProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowIndex { get; }

        public override int Count 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);


        protected LaMatrixRowBase([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= matrixProcessor.GetDenseRowsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            RowIndex = rowIndex;
        }
    }
}