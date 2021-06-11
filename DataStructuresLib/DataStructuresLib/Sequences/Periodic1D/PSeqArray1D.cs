using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Sequences.Periodic1D
{
    public class PSeqArray1D<T>
        : IPeriodicSequence1D<T>
    {
        private readonly T[] _dataArray;


        public int Count 
            => _dataArray.Length;

        public T this[int index]
        {
            get => _dataArray[index.Mod(Count)];
            set => _dataArray[index.Mod(Count)] = value;
        }

        public bool IsBasic 
            => true;

        public bool IsOperator 
            => false;


        public PSeqArray1D(int count)
        {
            _dataArray = new T[count];
        }

        public PSeqArray1D(params T[] dataArray)
        {
            _dataArray = dataArray;
        }

        public PSeqArray1D(IEnumerable<T> dataList)
        {
            _dataArray = dataList.ToArray();
        }


        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)_dataArray).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)_dataArray).GetEnumerator();
        }
    }
}