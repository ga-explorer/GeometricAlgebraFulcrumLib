using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorArrayStorage<T> :
        LinVectorMutableDenseStorageBase<T>
    {
        public T[] ScalarsArray { get; }

        public override T this[int index]
        {
            get => ScalarsArray[index];
            set => ScalarsArray[index] = value;
        }

        public override T this[ulong index]
        {
            get => ScalarsArray[index];
            set => ScalarsArray[index] = value;
        }

        public override int Count 
            => ScalarsArray.Length;


        internal LinVectorArrayStorage(int count)
        {
            ScalarsArray = new T[count];
        }

        internal LinVectorArrayStorage(params T[] itemsArray)
        {
            ScalarsArray = itemsArray;
        }
        
        internal LinVectorArrayStorage(IEnumerable<T> itemsList)
        {
            ScalarsArray = itemsList.ToArray();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return ScalarsArray[index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> GetCopy()
        {
            var scalarsArray = new T[ScalarsArray.Length];

            ScalarsArray.CopyTo(scalarsArray, 0);

            return new LinVectorArrayStorage<T>(scalarsArray);
        }

        public override ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            var scalarsArray = new T[ScalarsArray.Length];

            for (var index = 0; index < ScalarsArray.Length; index++)
                scalarsArray[indexMapping((ulong) index)] = ScalarsArray[index];

            return new LinVectorArrayStorage<T>(scalarsArray);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>) ScalarsArray).GetEnumerator();
        }
    }
}