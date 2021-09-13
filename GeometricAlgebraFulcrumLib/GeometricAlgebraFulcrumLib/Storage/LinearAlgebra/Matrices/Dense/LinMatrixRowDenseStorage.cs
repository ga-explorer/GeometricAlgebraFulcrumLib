using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixRowDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public ILinVectorDenseStorage<T> SourceStorage { get; }

        public override int Count1 
            => 1;

        public override int Count2 
            => SourceStorage.Count;

        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new System.NotImplementedException();
        }


        internal LinMatrixRowDenseStorage([NotNull] ILinVectorDenseStorage<T> sourceList)
        {
            SourceStorage = sourceList;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return index1 == 0
                ? SourceStorage.GetScalar(index2)
                : throw new KeyNotFoundException();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }
    }
}