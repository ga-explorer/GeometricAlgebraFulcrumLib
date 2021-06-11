using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    public readonly struct BoolTuple2D : IEnumerable<bool>
    {
        public static BoolTuple2D operator !(BoolTuple2D v1)
        {
            return new BoolTuple2D(!v1.ItemX, !v1.ItemY);
        }

        public static BoolTuple2D operator |(BoolTuple2D v1, BoolTuple2D v2)
        {
            return new BoolTuple2D(v1.ItemX | v2.ItemX, v1.ItemY | v2.ItemY);
        }

        public static BoolTuple2D operator &(BoolTuple2D v1, BoolTuple2D v2)
        {
            return new BoolTuple2D(v1.ItemX & v2.ItemX, v1.ItemY & v2.ItemY);
        }

        public static BoolTuple2D operator ^(BoolTuple2D v1, BoolTuple2D v2)
        {
            return new BoolTuple2D(v1.ItemX ^ v2.ItemX, v1.ItemY ^ v2.ItemY);
        }

        public static bool operator ==(BoolTuple2D v1, BoolTuple2D v2)
        {
            return
                v1.ItemX == v2.ItemX &&
                v1.ItemY == v2.ItemY;
        }

        public static bool operator !=(BoolTuple2D v1, BoolTuple2D v2)
        {
            return
                v1.ItemX != v2.ItemX ||
                v1.ItemY != v2.ItemY;
        }


        /// <summary>
        /// First Component of tuple
        /// </summary>
        public bool ItemX { get; }

        /// <summary>
        /// Second Component of tuple
        /// </summary>
        public bool ItemY { get; }


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) return ItemX;
                if (index == 1) return ItemY;

                return false;
            }
        }


        public BoolTuple2D(bool x, bool y)
        {
            ItemX = x;
            ItemY = y;
        }

        public BoolTuple2D(BoolTuple2D tuple)
        {
            ItemX = tuple.ItemX;
            ItemY = tuple.ItemY;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
        }

        public bool Equals(BoolTuple2D tuple)
        {
            return ItemX.Equals(tuple.ItemX) && ItemY.Equals(tuple.ItemY);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is BoolTuple2D && Equals((BoolTuple2D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ItemX.GetHashCode() * 397) ^ ItemY.GetHashCode();
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(ItemX.ToString())
                .Append(", ")
                .Append(ItemY.ToString())
                .Append(")")
                .ToString();
        }
    }
}