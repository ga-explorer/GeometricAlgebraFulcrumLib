using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense;

public sealed class LinMatrixComputedDenseStorage<T> :
    LinMatrixImmutableDenseStorageBase<T>
{
    public Func<ulong, ulong, T> IndexToScalarMapping { get; }

    public override int Count1 { get; }

    public override int Count2 { get; }


    internal LinMatrixComputedDenseStorage(int count1, int count2, Func<ulong, ulong, T> indexScalarMapping)
    {
        if (count1 < 0)
            throw new ArgumentOutOfRangeException(nameof(count1));

        if (count2 < 0)
            throw new ArgumentOutOfRangeException(nameof(count2));

        Count1 = count1;
        Count2 = count2;
        IndexToScalarMapping = indexScalarMapping;
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override T GetScalar(ulong index1, ulong index2)
    {
        return index1 < (ulong) Count1 && index2 < (ulong) Count2
            ? IndexToScalarMapping(index1, index2)
            : throw new KeyNotFoundException();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override ILinMatrixStorage<T> GetCopy()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
    {
        return rowIndexList
            .Where(index => index < (ulong) Count1)
            .Select(index1 => 
                new RGaKvIndexLinVectorStorageRecord<T>(
                    index1,
                    new LinVectorComputedDenseStorage<T>(
                        Count2, 
                        index2 => IndexToScalarMapping(index1, index2)
                    )
                )
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
    {
        return columnIndexList
            .Where(index => index < (ulong) Count2)
            .Select(index2 => 
                new RGaKvIndexLinVectorStorageRecord<T>(
                    index2,
                    new LinVectorComputedDenseStorage<T>(
                        Count1, 
                        index1 => IndexToScalarMapping(index1, index2)
                    )
                )
            );
    }
}