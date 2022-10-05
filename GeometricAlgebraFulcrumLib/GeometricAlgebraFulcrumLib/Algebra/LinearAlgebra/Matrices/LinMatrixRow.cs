using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed class LinMatrixRow<TMatrix, TScalar> :
        LinMatrixRowBase<TMatrix, TScalar>
    {
        public override TScalar GetScalar(ulong index) => MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index);


        internal LinMatrixRow([NotNull] IMatrixAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex)
            : base(matrixProcessor, matrix, rowIndex)
        {
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<TScalar> GetCopy()
        {
            return new LinMatrixRow<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                RowIndex
            );
        }
    }
}