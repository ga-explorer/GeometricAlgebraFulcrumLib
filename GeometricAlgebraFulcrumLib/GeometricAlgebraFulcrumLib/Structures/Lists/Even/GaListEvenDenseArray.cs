using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseArray<T> :
        GaListEvenDenseMutableBase<T>
    {
        public T[] ValuesArray { get; }

        public override T this[int index]
        {
            get => ValuesArray[index];
            set => ValuesArray[index] = value;
        }

        public override T this[ulong index]
        {
            get => ValuesArray[index];
            set => ValuesArray[index] = value;
        }

        public override int Count 
            => ValuesArray.Length;


        internal GaListEvenDenseArray(int count)
        {
            ValuesArray = new T[count];
        }

        internal GaListEvenDenseArray([NotNull] params T[] itemsArray)
        {
            ValuesArray = itemsArray;
        }
        
        internal GaListEvenDenseArray([NotNull] IEnumerable<T> itemsList)
        {
            ValuesArray = itemsList.ToArray();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key)
        {
            return ValuesArray[key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            var valuesArray = new T[GetSparseCount()];
            ValuesArray.CopyTo(valuesArray, 0);

            return new GaListEvenDenseArray<T>(valuesArray);
        }
    }
}