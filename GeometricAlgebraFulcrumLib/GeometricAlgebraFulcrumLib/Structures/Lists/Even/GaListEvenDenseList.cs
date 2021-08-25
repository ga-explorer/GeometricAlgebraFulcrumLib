using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Structures.Lists.Even
{
    public sealed class GaListEvenDenseList<T> :
        GaListEvenDenseMutableBase<T>
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


        internal GaListEvenDenseList()
        {
            ValuesList = new List<T>();
        }

        internal GaListEvenDenseList(int capacity)
        {
            ValuesList = new List<T>(capacity);
        }

        internal GaListEvenDenseList(IEnumerable<T> itemsList)
        {
            ValuesList = new List<T>(itemsList);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override T GetValue(ulong key)
        {
            return ValuesList[(int) key];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IEnumerable<T> GetValues()
        {
            return ValuesList;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Clear()
        {
            ValuesList.Clear();
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Append(T value)
        {
            ValuesList.Add(value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Prepend(T value)
        {
            ValuesList.Insert(0, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Insert(int index, T value)
        {
            ValuesList.Insert(index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Insert(ulong index, T value)
        {
            ValuesList.Insert((int) index, value);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Remove(int index)
        {
            ValuesList.RemoveAt(index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GaListEvenDenseList<T> Remove(ulong index)
        {
            ValuesList.RemoveAt((int) index);
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override IGaListEven<T> GetCopy()
        {
            return new GaListEvenDenseList<T>(ValuesList);
        }
    }
}