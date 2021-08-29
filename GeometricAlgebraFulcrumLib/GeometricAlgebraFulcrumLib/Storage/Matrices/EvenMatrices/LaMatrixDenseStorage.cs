using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenMatrices
{
    public sealed class LaMatrixDenseStorage<T> :
        LaMatrixMutableDenseStorageBase<T>
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
        

        internal LaMatrixDenseStorage(int count1, int count2)
        {
            ValuesArray = new T[count1, count2];
        }

        internal LaMatrixDenseStorage([NotNull] T[,] itemsArray)
        {
            ValuesArray = itemsArray;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index1, ulong index2)
        {
            return ValuesArray[index1, index2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> GetCopy()
        {
            return new LaMatrixDenseStorage<T>(ValuesArray.GetArrayCopy());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaMatrixEvenStorage<T> Transpose()
        {
            return new LaMatrixDenseTransposedStorage<T>(this);
        }
    }
}