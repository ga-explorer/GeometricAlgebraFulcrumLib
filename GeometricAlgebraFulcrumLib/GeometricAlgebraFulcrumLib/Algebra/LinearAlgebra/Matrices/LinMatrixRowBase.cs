using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public abstract class LinMatrixRowBase<TMatrix, TScalar> :
        LinVectorDenseStorageBase<TScalar>
    {
        public ILinearAlgebraProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowIndex { get; }

        public override int Count 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);


        protected LinMatrixRowBase([NotNull] ILinearAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= matrixProcessor.GetDenseRowsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            RowIndex = rowIndex;
        }
    }
}