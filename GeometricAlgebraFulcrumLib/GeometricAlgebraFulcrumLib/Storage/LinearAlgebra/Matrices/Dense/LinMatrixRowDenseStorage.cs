using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Factories;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixRowDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public ILinVectorDenseStorage<T> VectorStorage { get; }

        public override int Count1 
            => 1;

        public override int Count2 
            => VectorStorage.Count;


        internal LinMatrixRowDenseStorage([NotNull] ILinVectorDenseStorage<T> sourceList)
        {
            VectorStorage = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return index1 == 0
                ? VectorStorage.GetScalar(index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            if (rowIndexList.Any(i => i == 0))
                yield return new IndexLinVectorStorageRecord<T>(
                    0,
                    VectorStorage
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            return columnIndexList
                .Where(VectorStorage.ContainsIndex)
                .Select(index => 
                    new IndexLinVectorStorageRecord<T>(
                        index,
                        VectorStorage.GetScalar(index).CreateLinVectorSingleScalarStorage(index)
                    )
                );
        }
    }
}