using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This class represents a mutable pair of items of the same type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class MutablePair<T> : 
        IPair<T>
    {
        public T Item1 { get; set; }

        public T Item2 { get; set; }


        public MutablePair()
        {
        }

        public MutablePair(T firstItem)
        {
            Item1 = firstItem;
            Item2 = firstItem;
        }

        public MutablePair(T firstItem, T secondItem)
        {
            Item1 = firstItem;
            Item2 = secondItem;
        }

        public MutablePair(IPair<T> pair)
        {
            Item1 = pair.Item1;
            Item2 = pair.Item2;
        }

        public MutablePair(Tuple<T, T> tuple)
        {
            (Item1, Item2) = tuple;
        }


        public MutablePair<T> GetCopy()
        {
            return new MutablePair<T>(Item1, Item2);
        }

        /// <summary>
        /// Updates this pair to contain (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public MutablePair<T> NextPair(T nextItem)
        {
            Item1 = Item2;
            Item2 = nextItem;

            return this;
        }

        /// <summary>
        /// Updates this pair to contain (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public MutablePair<T> PreviousPair(T previousItem)
        {
            Item2 = Item1;
            Item1 = previousItem;

            return this;
        }

        /// <summary>
        /// Updates this pair to contain (this.Item2, this.Item1)
        /// </summary>
        /// <returns></returns>
        public MutablePair<T> SwapItems()
        {
            var a = Item2;
            Item2 = Item1;
            Item1 = a;

            return this;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine("(")
                .Append(Item1)
                .Append(", ")
                .Append(Item2)
                .AppendLine(")")
                .ToString();
        }
    }
}