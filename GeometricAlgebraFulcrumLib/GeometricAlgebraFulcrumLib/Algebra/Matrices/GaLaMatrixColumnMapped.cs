using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processing.Matrices;
using GeometricAlgebraFulcrumLib.Structures.Lists.Even;

namespace GeometricAlgebraFulcrumLib.Algebra.Matrices
{
    public sealed class GaLaMatrixColumnMapped<TMatrix, TScalar> :
        GaLaMatrixColumnBase<TMatrix, TScalar>
    {
        public Func<TScalar, TScalar> ScalarMapping { get; }


        internal GaLaMatrixColumnMapped([NotNull] IGaMatrixProcessor<TMatrix, TScalar> matrixProcessor, [NotNull] TMatrix matrix, int columnIndex, [NotNull] Func<TScalar, TScalar> scalarMapping)
            : base(matrixProcessor, matrix, columnIndex)
        {
            ScalarMapping = scalarMapping;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override TScalar GetValue(ulong index)
        {
            return ScalarMapping(
                MatrixProcessor.GetScalar(MatrixStorage, (int) index, ColumnIndex)
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<TScalar> GetCopy()
        {
            return new GaLaMatrixColumnMapped<TMatrix, TScalar>(
                MatrixProcessor,
                MatrixStorage,
                ColumnIndex, 
                ScalarMapping
            );
        }
    }
}