using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    public readonly struct BoolTuple3D : IEnumerable<bool>
    {
        public static BoolTuple3D operator !(BoolTuple3D v1)
        {
            return new BoolTuple3D(!v1.ItemX, !v1.ItemY, !v1.ItemZ);
        }

        public static BoolTuple3D operator |(BoolTuple3D v1, BoolTuple3D v2)
        {
            return new BoolTuple3D(v1.ItemX | v2.ItemX, v1.ItemY | v2.ItemY, v1.ItemZ | v2.ItemZ);
        }

        public static BoolTuple3D operator &(BoolTuple3D v1, BoolTuple3D v2)
        {
            return new BoolTuple3D(v1.ItemX & v2.ItemX, v1.ItemY & v2.ItemY, v1.ItemZ & v2.ItemZ);
        }

        public static BoolTuple3D operator ^(BoolTuple3D v1, BoolTuple3D v2)
        {
            return new BoolTuple3D(v1.ItemX ^ v2.ItemX, v1.ItemY ^ v2.ItemY, v1.ItemZ ^ v2.ItemZ);
        }

        public static bool operator ==(BoolTuple3D v1, BoolTuple3D v2)
        {
            return
                v1.ItemX == v2.ItemX &&
                v1.ItemY == v2.ItemY &&
                v1.ItemZ == v2.ItemZ;
        }

        public static bool operator !=(BoolTuple3D v1, BoolTuple3D v2)
        {
            return
                v1.ItemX != v2.ItemX ||
                v1.ItemY != v2.ItemY ||
                v1.ItemZ != v2.ItemZ;
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
        /// Third Component of tuple
        /// </summary>
        public bool ItemZ { get; }


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0) return ItemX;
                if (index == 1) return ItemY;
                if (index == 2) return ItemZ;

                return false;
            }
        }


        public BoolTuple3D(bool x, bool y, bool z)
        {
            ItemX = x;
            ItemY = y;
            ItemZ = z;
        }

        public BoolTuple3D(BoolTuple3D tuple)
        {
            ItemX = tuple.ItemX;
            ItemY = tuple.ItemY;
            ItemZ = tuple.ItemZ;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
            yield return ItemZ;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return ItemX;
            yield return ItemY;
            yield return ItemZ;
        }

        public bool Equals(BoolTuple3D tuple)
        {
            return 
                ItemX.Equals(tuple.ItemX) && 
                ItemY.Equals(tuple.ItemY) &&
                ItemZ.Equals(tuple.ItemZ);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;

            return obj is BoolTuple3D && Equals((BoolTuple3D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ItemX.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemY.GetHashCode();
                hashCode = (hashCode * 397) ^ ItemZ.GetHashCode();
                return hashCode;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(ItemX.ToString())
                .Append(", ")
                .Append(ItemY.ToString())
                .Append(", ")
                .Append(ItemZ.ToString())
                .Append(")")
                .ToString();
        }
    }
}