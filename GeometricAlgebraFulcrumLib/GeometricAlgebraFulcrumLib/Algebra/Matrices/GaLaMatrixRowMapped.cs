using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixRowMapped<TMatrix, TScalar> :
        GaLaMatrixRowBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal GaLaMatrixRowMapped([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex, [NotNull] Func<TScalar, TScalar> scalarMapping)
            : base(matrixProcessor, matrix, rowIndex)
        {
            ScalarMapping = scalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetValue(ulong index)
        {
            return ScalarMapping(
                MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixRowMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                RowIndex, ScalarMapping);
        }
    }
}