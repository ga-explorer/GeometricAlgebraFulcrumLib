using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixColumn<TMatrix, TScalar> :
        GaLaMatrixColumnBase<TMatrix, TScalar>
    {
        internal GaLaMatrixColumn([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex)
            : base(matrixProcessor, matrix, columnIndex)
        {
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetValue(ulong index)
        {
            return MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixColumn<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex
            );
        }
    }
}