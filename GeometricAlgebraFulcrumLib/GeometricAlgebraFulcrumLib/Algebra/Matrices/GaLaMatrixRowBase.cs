using System;
using System.Diagnostics.CodeAnalysis;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public abstract class GaLaMatrixRowBase<TMatrix, TScalar> :
        GaListEvenDenseBase<TScalar>
    {
        public IGaMatrixProcessor<TMatrix, TScalar> MatrixProcessor { get; }

        public TMatrix MatrixStorage { get; }

        public int RowIndex { get; }

        public override int Count 
            => MatrixProcessor.GetDenseColumnsCount(MatrixStorage);


        protected GaLaMatrixRowBase([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= matrixProcessor.GetDenseRowsCount(matrix))
                throw new ArgumentOutOfRangeException(nameof(rowIndex));

            MatrixProcessor = matrixProcessor;
            MatrixStorage = matrix;
            RowIndex = rowIndex;
        }
    }
}