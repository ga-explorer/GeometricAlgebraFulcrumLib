using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixTransposedDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public LinMatrixDenseStorageBase<T> MatrixStorage { get; }

        public override int Count1 
            => MatrixStorage.Count1;

        public override int Count2 
            => MatrixStorage.Count2;

        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new System.NotImplementedException();
        }


        internal LinMatrixTransposedDenseStorage([NotNull] LinMatrixDenseStorageBase<T> source)
        {
            MatrixStorage = source;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return MatrixStorage.GetScalar(index2, index1);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetCopy()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetTranspose()
        {
            return MatrixStorage;
        }
    }
}