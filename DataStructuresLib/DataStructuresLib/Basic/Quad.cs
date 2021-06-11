using System;
using System.Text;

namespace DataStructuresLib.Basic
{
    /// <summary>
    /// This structure represents an immutable quad of items of the same type
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public readonly struct Quad<TValue> : 
        IQuad<TValue>,
        IEquatable<Quad<TValue>>, 
        IEquatable<Quad<TValue>?>
    {
        public static bool operator ==(Quad<TValue> p1, Quad<TValue> p2)
        {
            return p1.Equals(p2);
        }

        public static bool operator !=(Quad<TValue> p1, Quad<TValue> p2)
        {
            return !p1.Equals(p2);
        }
        
        
        public TValue Item1 { get; }

        public TValue Item2 { get; }

        public TValue Item3 { get; }

        public TValue Item4 { get; }


        public Quad(TValue item1, TValue item2, TValue item3, TValue item4)
        {
            Item1 = item1;
            Item2 = item2;
            Item3 = item3;
            Item4 = item4;
        }

        public Quad(IQuad<TValue> quad)
        {
            Item1 = quad.Item1;
            Item2 = quad.Item2;
            Item3 = quad.Item3;
            Item4 = quad.Item4;
        }

        public Quad(Tuple<TValue, TValue, TValue, TValue> quad)
        {
            (Item1, Item2, Item3, Item4) = quad;
        }


        public Quad<TValue> GetCopy()
        {
            return new Quad<TValue>(this);
        }

        /// <summary>
        /// Returns a new pair containing (this.Item2, nextItem)
        /// </summary>
        /// <param name="nextItem"></param>
        /// <returns></returns>
        public Quad<TValue> NextQuad(TValue nextItem)
        {
            return new Quad<TValue>(Item2, Item3, Item4, nextItem);
        }

        /// <summary>
        /// Returns a new pair containing (previousItem, this.Item1)
        /// </summary>
        /// <param name="previousItem"></param>
        /// <returns></returns>
        public Quad<TValue> PreviousQuad(TValue previousItem)
        {
            return new Quad<TValue>(previousItem, Item1, Item2, Item3);
        }

        public Quad<TValue> RotateForward()
        {
            return new Quad<TValue>(Item4, Item1, Item2, Item3);
        }

        public Quad<TValue> RotateBackward()
        {
            return new Quad<TValue>(Item2, Item3, Item4, Item1);
        }
        
        public bool Equals(Quad<TValue> other)
        {
            return 
                Item1.Equals(other.Item1) && 
                Item2.Equals(other.Item2) &&
                Item3.Equals(other.Item3) &&
                Item3.Equals(other.Item4);
        }

        public bool Equals(Quad<TValue>? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            return obj is Quad<TValue> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Item1.GetHashCode(), 
                Item2.GetHashCode(),
                Item3.GetHashCode(),
                Item4.GetHashCode()
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
                .Append(", ")
                .Append(Item4)
                .AppendLine(")")
                .ToString();
        }
    }
}