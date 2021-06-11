using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This structure represents an immutable triplet of items of the same type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public readonly struct Triplet<TValue> : 
        ITriplet<TValue>,
        IEquatable<Triplet<TValue>>, 
        IEquatable<Triplet<TValue>?>
    {
        public static bool operator ==(Triplet<TValue> p1, Triplet<TValue> p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Triplet<TValue> p1, Triplet<TValue> p2)
        {
            return !p1.Equals(p2);
        }


        public TValue Item1 { get; }

        public TValue Item2 { get; }

        public TValue Item3 { get; }


        public Triplet(TValue item1, TValue item2, TValue item3)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
        }

        public Triplet(ITriplet<TValue> triplet)
        {
            Item1 = triplet.Item1;
            Item2 = triplet.Item2;
            Item3 = triplet.Item3;
        }

        public Triplet(Tuple<TValue, TValue, TValue> triplet)
        {
            (Item1, Item2, Item3) = triplet;
        }


        public Triplet<TValue> GetCopy()
        {
            return new Triplet<TValue>(Item1, Item2, Item3);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public Triplet<TValue> NextTriplet(TValue nextItem)
        {
            return new Triplet<TValue>(Item2, Item3, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public Triplet<TValue> PreviousTriplet(TValue previousItem)
        {
            return new Triplet<TValue>(previousItem, Item1, Item2);
        }

        public Triplet<TValue> RotateForward()
        {
            return new Triplet<TValue>(Item3, Item1, Item2);
        }

        public Triplet<TValue> RotateBackward()
        {
            return new Triplet<TValue>(Item2, Item3, Item1);
        }

        public bool Equals(Triplet<TValue> other)
        {
            return 
                Item1.Equals(other.Item1) && 
                Item2.Equals(other.Item2) &&
                Item3.Equals(other.Item3);
        }

        public bool Equals(Triplet<TValue>? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is Triplet<TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Item1.GetHashCode(), 
                Item2.GetHashCode(),
                Item3.GetHashCode()
            );
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine("(")
                .Append(Item1)
                .Append(", ")
                .Append(Item2)
                .Append(", ")
                .Append(Item3)
                .AppendLine(")")
                .ToString();
        }
    }
}