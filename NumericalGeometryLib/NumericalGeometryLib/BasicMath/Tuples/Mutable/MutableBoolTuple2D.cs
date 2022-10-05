using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableBoolTuple2D : IEnumerable<bool>
    {
        public static MutableBoolTuple2D operator !(MutableBoolTuple2D v1)
        {
            return new MutableBoolTuple2D(!v1.X, !v1.Y);
        }

        public static MutableBoolTuple2D operator |(MutableBoolTuple2D v1, MutableBoolTuple2D v2)
        {
            return new MutableBoolTuple2D(v1.X | v2.X, v1.Y | v2.Y);
        }

        public static MutableBoolTuple2D operator &(MutableBoolTuple2D v1, MutableBoolTuple2D v2)
        {
            return new MutableBoolTuple2D(v1.X & v2.X, v1.Y & v2.Y);
        }

        public static MutableBoolTuple2D operator ^(MutableBoolTuple2D v1, MutableBoolTuple2D v2)
        {
            return new MutableBoolTuple2D(v1.X ^ v2.X, v1.Y ^ v2.Y);
        }

        //public static bool operator ==(MutableTupleBool2D v1, MutableTupleBool2D v2)
        //{
        //    return
        //        v1.X == v2.X &&
        //        v1.Y == v2.Y;
        //}

        //public static bool operator !=(MutableTupleBool2D v1, MutableTupleBool2D v2)
        //{
        //    return
        //        v1.X != v2.X ||
        //        v1.Y != v2.Y;
        //}


        /// <summary>
        /// First Component of tuple
        /// </summary>
        public bool X { get; set; }

        /// <summary>
        /// Second Component of tuple
        /// </summary>
        public bool Y { get; set; }


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return false;
            }
        }


        public MutableBoolTuple2D()
        {
        }

        public MutableBoolTuple2D(bool x, bool y)
        {
            X = x;
            Y = y;
        }

        public MutableBoolTuple2D(MutableBoolTuple2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        public bool Equals(MutableBoolTuple2D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString())
                .Append(", ")
                .Append(Y.ToString())
                .Append(")")
                .ToString();
        }
    }
}