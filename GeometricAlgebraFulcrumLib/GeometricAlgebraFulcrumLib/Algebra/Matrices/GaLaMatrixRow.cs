using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixRow<TMatrix, TScalar> :
        GaLaMatrixRowBase<TMatrix, TScalar>
    {
        public override TScalar GetValue(ulong index) => MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index);


        internal GaLaMatrixRow([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex)
            : base(matrixProcessor, matrix, rowIndex)
        {
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixRow<TMatrix, TScalar>(
                MatrixProcessor, 
                MatrixStorage, 
                RowIndex
            );
        }
    }
}