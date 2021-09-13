using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public override IEnumerable<IndexLinVectorStorageRecord<T>> GetDenseColumns(IEnumerable<ulong> columnIndexList)
        {
            throw new System.NotImplementedException();
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
    }
}