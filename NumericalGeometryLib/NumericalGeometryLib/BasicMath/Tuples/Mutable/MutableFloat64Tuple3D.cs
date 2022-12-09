using System;
using System.Diagnostics;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableFloat64Tuple3D : 
        IFloat64Tuple3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y) &&
                   !double.IsNaN(Z);
        }


        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1 or 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return 0.0d;
            }
            set
            {
                Debug.Assert(!double.IsNaN(value));
                Debug.Assert(index is 0 or 1 or 2);

                if (index == 0) X = value;
                if (index == 1) Y = value;
                if (index == 2) Z = value;
            }
        }


        public MutableFloat64Tuple3D()
        {
        }

        public MutableFloat64Tuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }

        public MutableFloat64Tuple3D(IFloat64Tuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;

            Debug.Assert(IsValid());
        }


        public MutableFloat64Tuple3D SetTuple(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple3D SetTuple(IFloat64Tuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple3D SetToZero()
        {
            X = 0;
            Y = 0;
            Z = 0;

            return this;
        }

        public MutableFloat64Tuple3D SetDirection(IFloat64Tuple3D direction)
        {
            var oldLength2 = 
                direction.X * direction.X + 
                direction.Y * direction.Y + 
                direction.Z * direction.Z;

            var newLength2 = X * X + Y * Y + Z * Z;

            var s = Math.Sqrt(newLength2 / oldLength2);

            X = direction.X * s;
            Y = direction.Y * s;
            Z = direction.Z * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple3D SetLength(double d)
        {
            var s = d / Math.Sqrt(X * X + Y * Y + Z * Z);

            X = X * s;
            Y = Y * s;
            Z = Z * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple3D SetLengthToUnit()
        {
            var s = 1 / Math.Sqrt(X * X + Y * Y + Z * Z);

            X = X * s;
            Y = Y * s;
            Z = Z * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple3D SetToNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }

        public MutableFloat64Tuple3D SetToSameSide(IFloat64Tuple3D direction)
        {
            if (X * direction.X + Y * direction.Y + Z * direction.Z >= 0)
                return this;

            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }

        public MutableFloat64Tuple3D SetToOtherSide(IFloat64Tuple3D direction)
        {
            if (X * direction.X + Y * direction.Y + Z * direction.Z <= 0)
                return this;

            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(", ")
                .Append(Z.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}