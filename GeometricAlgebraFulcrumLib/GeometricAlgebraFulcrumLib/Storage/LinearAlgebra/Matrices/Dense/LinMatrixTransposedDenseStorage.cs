using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixTransposedDenseStorage<T> :
        LinMatrixImmutableDenseStorageBase<T>
    {
        public ILinMatrixDenseStorage<T> MatrixStorage { get; }

        public override int Count1 
            => MatrixStorage.Count1;

        public override int Count2 
            => MatrixStorage.Count2;


        internal LinMatrixTransposedDenseStorage(ILinMatrixDenseStorage<T> source)
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            return MatrixStorage.GetDenseColumns(rowIndexList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<RGaKvIndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            return MatrixStorage.GetDenseRows(columnIndexList);
        }
    }
}