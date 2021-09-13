using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

namespace GeometricAlgebraFulcrumLib.Storage.LinearAlgebra.Vectors.Dense
{
    public sealed class LinVectorListStorage<T> :
        LinVectorMutableDenseStorageBase<T>
    {
        public List<T> ScalarsList { get; }

        public override T this[int index]
        {
            get => ScalarsList[index];
            set => ScalarsList[index] = value;
        }

        public override T this[ulong index]
        {
            get => ScalarsList[(int) index];
            set => ScalarsList[(int) index] = value;
        }

        public override int Count 
            => ScalarsList.Count;
        

        internal LinVectorListStorage()
        {
            ScalarsList = new List<T>();
        }

        internal LinVectorListStorage(int capacity)
        {
            ScalarsList = new List<T>(capacity);
        }

        internal LinVectorListStorage(IEnumerable<T> itemsList)
        {
            ScalarsList = new List<T>(itemsList);
        }

        internal LinVectorListStorage([NotNull] List<T> itemsList)
        {
            ScalarsList = itemsList;
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetScalar(ulong index)
        {
            return ScalarsList[(int) index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<T> GetScalars()
        {
            return ScalarsList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Clear()
        {
            ScalarsList.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Append(T value)
        {
            ScalarsList.Add(value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Prepend(T value)
        {
            ScalarsList.Insert(0, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Insert(int index, T value)
        {
            ScalarsList.Insert(index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Insert(ulong index, T value)
        {
            ScalarsList.Insert((int) index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Remove(int index)
        {
            ScalarsList.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LinVectorListStorage<T> Remove(ulong index)
        {
            ScalarsList.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override ILinVectorStorage<T> GetCopy()
        {
            return new LinVectorListStorage<T>(ScalarsList);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IReadOnlyList<T> GetScalarsList()
        {
            return ScalarsList.ToArray();
        }

        public override ILinVectorDenseStorage<T> GetDensePermutation(Func<ulong, ulong> indexMapping)
        {
            var scalarsArray = new T[Count];

            for (var index = 0; index < Count; index++)
                scalarsArray[indexMapping((ulong) index)] = ScalarsList[index];

            return new LinVectorListStorage<T>(scalarsArray);
        }
    }
}