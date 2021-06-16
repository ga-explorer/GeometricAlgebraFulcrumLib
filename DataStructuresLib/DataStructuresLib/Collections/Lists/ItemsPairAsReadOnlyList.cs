using System;
using System.Collections;
using System.Collections.Generic;
using DataStructuresLib.Basic;

namespace DataStructuresLib.Collections.Lists
{
    public readonly struct ItemsPairAsReadOnlyList<T> : 
        IReadOnlyList<T>
    {
        public T Value1 { get; }

        public T Value2 { get; }

        public int Count => 2;

        public T this[int index]
        {
            get
            {
                return index switch
                {
                    0 => Value1,
                    1 => Value2,
                    _ => throw new IndexOutOfRangeException()
                };
            }
        }


        public ItemsPairAsReadOnlyList(T value1, T value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public ItemsPairAsReadOnlyList(Tuple<T, T> valueTuple)
        {
            Value1 = valueTuple.Item1;
            Value2 = valueTuple.Item2;
        }

        public ItemsPairAsReadOnlyList(IPair<T> valuePair)
        {
            Value1 = valuePair.Item1;
            Value2 = valuePair.Item2;
        }


        public IEnumerator<T> GetEnumerator()
        {
            yield return Value1;
            yield return Value2;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return Value1;
            yield return Value2;
        }
    }
}