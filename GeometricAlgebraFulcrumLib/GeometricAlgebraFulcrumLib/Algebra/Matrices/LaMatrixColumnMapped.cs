using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class LaMatrixColumnMapped<TMatrix, TScalar> :
        LaMatrixColumnBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal LaMatrixColumnMapped([NotNull] ILaProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] Func<TScalar, TScalar> scalarMapping)
            : base(matrixProcessor, matrix, columnIndex)
        {
            ScalarMapping = scalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetScalar(ulong index)
        {
            return ScalarMapping(
                MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<TScalar> GetCopy()
        {
            return new LaMatrixColumnMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex, 
                ScalarMapping
            );
        }
    }
}