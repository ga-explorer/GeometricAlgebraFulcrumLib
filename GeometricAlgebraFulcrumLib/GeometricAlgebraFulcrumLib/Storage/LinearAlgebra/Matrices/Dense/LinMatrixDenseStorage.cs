using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Records;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Matrices.Dense
{
    public sealed class LinMatrixDenseStorage<T> :
        LinMatrixMutableDenseStorageBase<T>
    {
        public T[,] ValuesArray { get; }

        public override T this[int index1, int index2]
        {
            get => ValuesArray[index1, index2];
            set => ValuesArray[index1, index2] = value;
        }

        public override T this[ulong index1, ulong index2]
        {
            get => ValuesArray[index1, index2];
            set => ValuesArray[index1, index2] = value;
        }

        public override int Count1 
            => ValuesArray.GetLength(0);

        public override int Count2 
            => ValuesArray.GetLength(1);
        

        internal LinMatrixDenseStorage(int count1, int count2)
        {
            ValuesArray = new T[count1, count2];
        }

        internal LinMatrixDenseStorage([NotNull] T[,] itemsArray)
        {
            ValuesArray = itemsArray;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return ValuesArray[index1, index2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetCopy()
        {
            return new LinMatrixDenseStorage<T>(ValuesArray.GetArrayCopy());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinMatrixStorage<T> GetTranspose()
        {
            return new LinMatrixTransposedDenseStorage<T>(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseRows(IEnumerable<ulong> rowIndexList)
        {
            return rowIndexList
                .Where(index => index < (ulong) Count1)
                .Select(index => new IndexLinVectorStorageRecord<T>(
                    index,
                    GetRow(index)
                )
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            return columnIndexList
                .Where(index => index < (ulong) Count1)
                .Select(index => new IndexLinVectorStorageRecord<T>(
                        index,
                        GetColumn(index)
                    )
                );
        }
    }
}