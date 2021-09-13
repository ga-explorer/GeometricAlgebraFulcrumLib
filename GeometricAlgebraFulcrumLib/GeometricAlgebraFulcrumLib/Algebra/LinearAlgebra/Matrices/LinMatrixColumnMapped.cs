using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.LinearAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed class LinMatrixColumnMapped<TMatrix, TScalar> :
        LinMatrixColumnBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal LinMatrixColumnMapped([NotNull] ILinearAlgebraProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] Func<TScalar, TScalar> scalarMapping)
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
        public override ILinVectorStorage<TScalar> GetCopy()
        {
            return new LinMatrixColumnMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex, 
                ScalarMapping
            );
        }
    }
}