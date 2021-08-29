using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Storage.Matrices.EvenVectors
{
    public sealed class LaVectorListStorage<T> :
        LaVectorMutableDenseStorageBase<T>
    {
        public List<T> ValuesList { get; }

        public override T this[int index]
        {
            get => ValuesList[index];
            set => ValuesList[index] = value;
        }

        public override T this[ulong index]
        {
            get => ValuesList[(int) index];
            set => ValuesList[(int) index] = value;
        }

        public override int Count 
            => ValuesList.Count;


        internal LaVectorListStorage()
        {
            ValuesList = new List<T>();
        }

        internal LaVectorListStorage(int capacity)
        {
            ValuesList = new List<T>(capacity);
        }

        internal LaVectorListStorage(IEnumerable<T> itemsList)
        {
            ValuesList = new List<T>(itemsList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return ValuesList[(int) index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<T> GetScalars()
        {
            return ValuesList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Clear()
        {
            ValuesList.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Append(T value)
        {
            ValuesList.Add(value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Prepend(T value)
        {
            ValuesList.Insert(0, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Insert(int index, T value)
        {
            ValuesList.Insert(index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Insert(ulong index, T value)
        {
            ValuesList.Insert((int) index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Remove(int index)
        {
            ValuesList.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LaVectorListStorage<T> Remove(ulong index)
        {
            ValuesList.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILaVectorEvenStorage<T> GetCopy()
        {
            return new LaVectorListStorage<T>(ValuesList);
        }
    }
}