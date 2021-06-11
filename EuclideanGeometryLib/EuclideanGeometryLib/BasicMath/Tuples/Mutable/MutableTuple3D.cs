using System;
using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableTuple3D : ITuple3D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public bool IsValid =>
            !double.IsNaN(X) && 
            !double.IsNaN(Y) && 
            !double.IsNaN(Z);

        public bool IsInvalid =>
            double.IsNaN(X) || 
            double.IsNaN(Y) || 
            double.IsNaN(Z);
        

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return 0.0d;
            }
            set
            {
                Debug.Assert(!double.IsNaN(value));
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0) X = value;
                if (index == 1) Y = value;
                if (index == 2) Z = value;
            }
        }


        public MutableTuple3D()
        {
        }

        public MutableTuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(!IsInvalid);
        }

        public MutableTuple3D(ITuple3D tuple)
        {
            Debug.Assert(!tuple.IsInvalid);

            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
        }


        public MutableTuple3D SetTuple(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            return this;
        }

        public MutableTuple3D SetTuple(ITuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;

            return this;
        }

        public MutableTuple3D SetToZero()
        {
            X = 0;
            Y = 0;
            Z = 0;

            return this;
        }

        public MutableTuple3D SetDirection(ITuple3D direction)
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

            return this;
        }

        public MutableTuple3D SetLength(double d)
        {
            var s = d / Math.Sqrt(X * X + Y * Y + Z * Z);

            X = X * s;
            Y = Y * s;
            Z = Z * s;

            return this;
        }

        public MutableTuple3D SetLengthToUnit()
        {
            var s = 1 / Math.Sqrt(X * X + Y * Y + Z * Z);

            X = X * s;
            Y = Y * s;
            Z = Z * s;

            return this;
        }

        public MutableTuple3D SetToNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }

        public MutableTuple3D SetToSameSide(ITuple3D direction)
        {
            if (X * direction.X + Y * direction.Y + Z * direction.Z >= 0)
                return this;

            X = -X;
            Y = -Y;
            Z = -Z;

            return this;
        }

        public MutableTuple3D SetToOtherSide(ITuple3D direction)
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