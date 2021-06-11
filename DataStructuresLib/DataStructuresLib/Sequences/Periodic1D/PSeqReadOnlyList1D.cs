using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public class PSeqReadOnlyList1D<T>
        : IPeriodicSequence1D<T>
    {
        private readonly IReadOnlyList<T> _dataList;


        public int Count 
            => _dataList.Count;

        public T this[int index] 
            => _dataList[index.Mod(Count)];

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PSeqReadOnlyList1D(params T[] data)
        {
            _dataList = data;
        }

        public PSeqReadOnlyList1D(IReadOnlyList<T> data)
        {
            _dataList = data;
        }

        public PSeqReadOnlyList1D(IEnumerable<T> data)
        {
            _dataList = data.ToArray();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _dataList.GetEnumerator();
        }
    }
}