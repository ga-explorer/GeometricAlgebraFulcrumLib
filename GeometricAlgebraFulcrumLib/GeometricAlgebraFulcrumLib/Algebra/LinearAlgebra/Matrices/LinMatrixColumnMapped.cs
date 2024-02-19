using System;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;

public sealed class LinMatrixColumnMapped<TMatrix, TScalar> :
    LinMatrixColumnBase<TMatrix, TScalar>
{
    public Func<TScalar, TScalar> ScalarMapping { get; }


    internal LinMatrixColumnMapped(IMatrixProcessor<TMatrix, TScalar> matrixProcessor, TMatrix matrix, int columnIndex, Func<TScalar, TScalar> scalarMapping)
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