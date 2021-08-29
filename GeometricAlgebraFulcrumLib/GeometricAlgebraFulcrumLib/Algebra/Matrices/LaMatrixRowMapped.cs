using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class LaMatrixRowMapped<TMatrix, TScalar> :
        LaMatrixRowBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal LaMatrixRowMapped([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int rowIndex, [NotNull] Func<TScalar, TScalar> scalarMapping)
            : base(matrixProcessor, matrix, rowIndex)
        {
            ScalarMapping = scalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return ScalarMapping(
                MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<TScalar> GetCopy()
        {
            return new LaMatrixRowMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                RowIndex, ScalarMapping);
        }
    }
}