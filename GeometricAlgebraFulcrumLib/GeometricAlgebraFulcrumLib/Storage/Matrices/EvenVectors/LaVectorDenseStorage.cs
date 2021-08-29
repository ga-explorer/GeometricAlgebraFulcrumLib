using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorDenseStorage<T> :
        LaVectorMutableDenseStorageBase<T>
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


        internal LaVectorDenseStorage(int count)
        {
            ValuesArray = new T[count];
        }

        internal LaVectorDenseStorage([NotNull] params T[] itemsArray)
        {
            ValuesArray = itemsArray;
        }
        
        internal LaVectorDenseStorage([NotNull] IEnumerable<T> itemsList)
        {
            ValuesArray = itemsList.ToArray();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return ValuesArray[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            var valuesArray = new T[GetSparseCount()];
            ValuesArray.CopyTo(valuesArray, 0);

            return new LaVectorDenseStorage<T>(valuesArray);
        }
    }
}