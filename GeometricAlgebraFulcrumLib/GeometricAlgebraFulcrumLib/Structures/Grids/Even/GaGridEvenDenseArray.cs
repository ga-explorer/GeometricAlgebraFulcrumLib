using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using DataStructuresLib.Extensions;

namespace GeometricAlgebraFulcrumLib.Structures.Grids.Even
{
    public sealed class GaGridEvenDenseArray<T> :
        GaGridEvenDenseMutableBase<T>
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
        

        internal GaGridEvenDenseArray(int count1, int count2)
        {
            ValuesArray = new T[count1, count2];
        }

        internal GaGridEvenDenseArray([NotNull] T[,] itemsArray)
        {
            ValuesArray = itemsArray;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key1, ulong key2)
        {
            return ValuesArray[key1, key2];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> GetCopy()
        {
            return new GaGridEvenDenseArray<T>(ValuesArray.GetArrayCopy());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaGridEven<T> Transpose()
        {
            return new GaGridEvenDenseTransposed<T>(this);
        }
    }
}