using System.Collections;
using System.Diagnostics;
using System.Text;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Mutable
{
    public sealed class MutableBoolTuple3D : IEnumerable<bool>
    {
        public static MutableBoolTuple3D operator !(MutableBoolTuple3D v1)
        {
            return new MutableBoolTuple3D(!v1.X, !v1.Y, !v1.Z);
        }

        public static MutableBoolTuple3D operator |(MutableBoolTuple3D v1, MutableBoolTuple3D v2)
        {
            return new MutableBoolTuple3D(v1.X | v2.X, v1.Y | v2.Y, v1.Z | v2.Z);
        }

        public static MutableBoolTuple3D operator &(MutableBoolTuple3D v1, MutableBoolTuple3D v2)
        {
            return new MutableBoolTuple3D(v1.X & v2.X, v1.Y & v2.Y, v1.Z & v2.Z);
        }

        public static MutableBoolTuple3D operator ^(MutableBoolTuple3D v1, MutableBoolTuple3D v2)
        {
            return new MutableBoolTuple3D(v1.X ^ v2.X, v1.Y ^ v2.Y, v1.Z ^ v2.Z);
        }

        //public static bool operator ==(MutableTupleBool3D v1, MutableTupleBool3D v2)
        //{
        //    return
        //        v1.X == v2.X &&
        //        v1.Y == v2.Y;
        //}

        //public static bool operator !=(MutableTupleBool3D v1, MutableTupleBool3D v2)
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
        /// Second Component of tuple
        /// </summary>
        public bool Z { get; set; }


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1 or 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return false;
            }
        }


        public MutableBoolTuple3D()
        {
        }

        public MutableBoolTuple3D(bool x, bool y, bool z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public MutableBoolTuple3D(MutableBoolTuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
        }


        public IEnumerator<bool> GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return X;
            yield return Y;
            yield return Z;
        }

        public bool Equals(MutableBoolTuple3D tuple)
        {
            return X.Equals(tuple.X) && 
                   Y.Equals(tuple.Y) &&
                   Z.Equals(tuple.Z);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString())
                .Append(", ")
                .Append(Y.ToString())
                .Append(", ")
                .Append(Z.ToString())
                .Append(")")
                .ToString();
        }
    }
}