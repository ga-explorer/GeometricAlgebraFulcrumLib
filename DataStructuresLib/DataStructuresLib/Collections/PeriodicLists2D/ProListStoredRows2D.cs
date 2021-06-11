using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DataStructuresLib.Collections.PeriodicLists;

namespace DataStructuresLib.Collections.PeriodicLists2D
{
    public class ProListStoredRows2D<TValue> : 
        IPeriodicReadOnlyList2D<TValue>
    {
        private readonly List<IPeriodicReadOnlyList<TValue>> _rowsList
            = new List<IPeriodicReadOnlyList<TValue>>();


        public int Count1 
            => _rowsList.Count;

        public int Count2 { get; }

        public int Count 
            => _rowsList.Count * Count2;

        public TValue this[int index] 
        {
            get
            {
                var (index1, index2) = 
                    this.GetItemIndexTuple(index);

                var rowList = _rowsList[index1];

                return rowList[index2];
            }
        }

        public TValue this[int index1, int index2]
        {
            get
            {
                var rowList = _rowsList[index1];

                return rowList[index2];
            }
        }

        public IEnumerable<IPeriodicReadOnlyList<TValue>> SourceLists
            => _rowsList;


        public ProListStoredRows2D(int count2)
        {
            Debug.Assert(count2 > 0);

            Count2 = count2;
        }


        public ProListStoredRows2D<TValue> AppendRow(IPeriodicReadOnlyList<TValue> column)
        {
            Debug.Assert(column.Count == Count2);

            _rowsList.Add(column);

            return this;
        }

        public ProListStoredRows2D<TValue> PrependRow(IPeriodicReadOnlyList<TValue> column)
        {
            Debug.Assert(column.Count == Count2);

            _rowsList.Insert(0, column);

            return this;
        }

        public ProListStoredRows2D<TValue> InsertRow(IPeriodicReadOnlyList<TValue> column, int index)
        {
            Debug.Assert(column.Count == Count2);

            _rowsList.Insert(index, column);

            return this;
        }
        
        public ProListStoredRows2D<TValue> RemoveRow(int index)
        {
            _rowsList.RemoveAt(index);

            return this;
        }

        public TValue[,] ToArray2D()
        {
            var valuesArray = new TValue[Count1, Count2];

            for (var index1 = 0; index1 < Count1; index1++)
            {
                var rowList = _rowsList[index1];

                for (var index2 = 0; index2 < Count2; index2++)
                    valuesArray[index1, index2] = rowList[index1];
            }

            return valuesArray;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            for (var index2 = 0; index2 < Count2; index2++)
            for (var index1 = 0; index1 < Count1; index1++)
                yield return _rowsList[index1][index2];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}