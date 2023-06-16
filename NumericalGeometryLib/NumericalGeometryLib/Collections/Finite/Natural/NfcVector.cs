using System.Collections.Generic;
using System.Linq;

namespace NumericalGeometryLib.Collections.Finite.Natural
{
    /// <summary>
    /// This class represents a collection stored internally in an array of fixed size
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NfcVector<T> : NaturalFiniteCollection<T>
    {
        public static NfcVector<T> Create(int itemsCount)
        {
            return new NfcVector<T>(itemsCount);
        }

        public static NfcVector<T> CreateFromList(params T[] itemsList)
        {
            var map = new NfcVector<T>(itemsList.Length);

            for (var i = 0; i < itemsList.Length; i++)
                map._items[i] = itemsList[i];

            return map;
        }

        public static NfcVector<T> CreateFromList(IEnumerable<T> itemsList)
        {
            return CreateFromList(itemsList.ToArray());
        }

        public static NfcVector<T> CreateFromList(FiniteCollection<T> itemsList)
        {
            var map = new NfcVector<T>(itemsList.MaxIndex + 1);

            for (var i = itemsList.MinIndex; i <= itemsList.MaxIndex; i++)
                map._items[i] = itemsList.GetItem(i);

            return map;
        }


        private T[] _items;


        public override int Count => _items.Length;

        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value;
        }


        private NfcVector(int itemsCount)
        {
            _items = new T[itemsCount];
        }


        public override T GetItem(int index)
        {
            return _items[index];
        }

        public NfcVector<T> Reset(int itemsCount)
        {
            _items = new T[itemsCount];
            return this;
        }

        public NfcVector<T> ResetFromList(params T[] itemsList)
        {
            _items = new T[itemsList.Length];

            for (var i = 0; i < itemsList.Length; i++)
                _items[i] = itemsList[i];

            return this;
        }

        public NfcVector<T> ResetFromList(IEnumerable<T> itemsList)
        {
            return ResetFromList(itemsList.ToArray());
        }

        public NfcVector<T> ResetFromList(FiniteCollection<T> itemsList)
        {
            _items = new T[itemsList.MaxIndex + 1];

            for (var i = itemsList.MinIndex; i <= itemsList.MaxIndex; i++)
                _items[i] = itemsList.GetItem(i);

            return this;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_items).GetEnumerator();
        }
    }
}
