using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This structure represents an immutable pair of items of the same type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public readonly struct Pair<TValue> : 
        IPair<TValue>,
        IEquatable<Pair<TValue>>, 
        IEquatable<Pair<TValue>?>
    {
        public static bool operator ==(Pair<TValue> p1, Pair<TValue> p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Pair<TValue> p1, Pair<TValue> p2)
        {
            return !p1.Equals(p2);
        }


        public TValue Item1 { get; }

        public TValue Item2 { get; }


        public Pair(TValue firstItem)
        {
            Item1 = firstItem;
            Item2 = firstItem;
        }

        public Pair(TValue firstItem, TValue secondItem)
        {
            Item1 = firstItem;
            Item2 = secondItem;
        }

        public Pair(IPair<TValue> pair)
        {
            Item1 = pair.Item1;
            Item2 = pair.Item2;
        }

        public Pair(Tuple<TValue, TValue> tuple)
        {
            (Item1, Item2) = tuple;
        }


        public Pair<TValue> GetCopy()
        {
            return new Pair<TValue>(Item1, Item2);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public Pair<TValue> NextPair(TValue nextItem)
        {
            return new Pair<TValue>(Item2, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public Pair<TValue> PreviousPair(TValue previousItem)
        {
            return new Pair<TValue>(previousItem, Item1);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, this.Item1)
        /// </summary>
        /// <returns></returns>
        public Pair<TValue> SwapItems()
        {
            return new Pair<TValue>(Item2, Item1);
        }

        public bool Equals(Pair<TValue> other)
        {
            return Item1.Equals(other.Item1) && Item2.Equals(other.Item2);
        }

        public bool Equals(Pair<TValue>? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is Pair<TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Item1.GetHashCode(), 
                Item2.GetHashCode()
            );
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
