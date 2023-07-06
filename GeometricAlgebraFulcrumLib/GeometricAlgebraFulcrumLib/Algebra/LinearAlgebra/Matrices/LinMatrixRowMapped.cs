using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices
{
    public sealed class LinMatrixRowMapped<TMatrix, TScalar> :
        LinMatrixRowBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal LinMatrixRowMapped(IMatrixProcessor<TMatrix, TScalar> matrixProcessor, TMatrix matrix, int rowIndex, Func<TScalar, TScalar> scalarMapping)
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
        public override ILinVectorStorage<TScalar> GetCopy()
        {
            return new LinMatrixRowMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                RowIndex, ScalarMapping);
        }
    }
}