﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixComputedDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public Func<ulong, ulong, T> IndexToScalarMapping { get; }

        public override int Count1 { get; }

        public override int Count2 { get; }
        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new NotImplementedException();
        }


        internal LinMatrixComputedDenseStorage(int count1, int count2, [NotNull] Func<ulong, ulong, T> indexScalarMapping)
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
    }
}