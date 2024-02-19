﻿using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Processors.MatrixAlgebra;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors;

namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Matrices;

public sealed class LinMatrixRowScaled<TMatrix, TScalar> :
    LinMatrixRowBase<TMatrix, TScalar>
{
    public TScalar ScalingFactor { get; set; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override TScalar GetScalar(ulong index)
    {
        return MatrixProcessor.Times(
            ScalingFactor,
            MatrixProcessor.GetScalar(MatrixStorage, RowIndex, (int) index)
        );
    }


    internal LinMatrixRowScaled(IMatrixProcessor<TMatrix, TScalar> matrixProcessor, TMatrix matrix, int rowIndex, TScalar scalingFactor)
        : base(matrixProcessor, matrix, rowIndex)
    {
        ScalingFactor = scalingFactor;
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinVectorStorage<TScalar> GetCopy()
    {
        return new LinMatrixRowScaled<TMatrix, TScalar>(
            MatrixProcessor, 
            MatrixStorage, 
            RowIndex, ScalingFactor);
    }
}