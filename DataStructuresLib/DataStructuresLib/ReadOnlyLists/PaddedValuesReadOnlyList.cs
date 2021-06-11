using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructuresLib.ReadOnlyLists
{
    public sealed class PaddedValuesReadOnlyList<T>
        : IReadOnlyList<T> where T : struct
    {
        public IReadOnlyList<T> BaseList { get; }

        private int _prePaddingSize = 0;
        public int PrePaddingSize
        {
            get => _prePaddingSize;
            set
            {
                if (value < 0) 
                    throw new InvalidOperationException();

                _prePaddingSize = value;
            }
        }

        private int _postPaddingSize = 0;
        public int PostPaddingSize
        {
            get => _postPaddingSize;
            set
            {
                if (value < 0) 
                    throw new InvalidOperationException();

                _postPaddingSize = value;
            }
        }

        public T DefaultValue { get; }

        public int Count 
            => PrePaddingSize + BaseList.Count + PostPaddingSize;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= Count)
                    throw new IndexOutOfRangeException();

                if (index < PrePaddingSize)
                    return DefaultValue;

                if (index > BaseList.Count + PrePaddingSize)
                    return DefaultValue;

                return BaseList[index - PrePaddingSize];
            }
        }


        public PaddedValuesReadOnlyList(IReadOnlyList<T> baseList, int prePaddingSize, int postPaddingSize)
        {
            DefaultValue = default;
            BaseList = baseList;
            PrePaddingSize = prePaddingSize;
            PostPaddingSize = postPaddingSize;
        }

        public PaddedValuesReadOnlyList(IReadOnlyList<T> baseList, T defaultValue, int prePaddingSize, int postPaddingSize)
        {
            DefaultValue = defaultValue;
            BaseList = baseList;
            PrePaddingSize = prePaddingSize;
            PostPaddingSize = postPaddingSize;
        }


        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < PrePaddingSize; i++)
                yield return DefaultValue;

            foreach (var item in BaseList)
                yield return item;

            for (var i = 0; i < PostPaddingSize; i++)
                yield return DefaultValue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}