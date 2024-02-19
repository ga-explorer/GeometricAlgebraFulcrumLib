﻿using System.Collections.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;

public abstract class LinMatrixMutableDenseStorageBase<T> :
    LinMatrixDenseStorageBase<T>, ILinMatrixMutableDenseStorage<T>
{
    public abstract T this[int index1, int index2] { get; set; }

    public abstract T this[ulong index1, ulong index2] { get; set; }
        
    public bool IsSquare 
        => Count1 == Count2;

    public abstract IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList);

    public abstract IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList);
}