using System;
using System.Diagnostics;
using System.Numerics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    /// <inheritdoc cref="ITuple3D" />
    /// <summary>
    /// A 3-tuple of double precision coordinates
    /// </summary>
    public readonly struct Tuple3D : 
        ITuple3D,
        IEquatable<Tuple3D>,
        IEquatable<Tuple3D?>
    {
        public static Tuple3D Zero { get; } = new Tuple3D(0, 0, 0);

        public static Tuple3D E1 { get; } = new Tuple3D(1, 0, 0);

        public static Tuple3D E2 { get; } = new Tuple3D(0, 1, 0);

        public static Tuple3D E3 { get; } = new Tuple3D(0, 0, 1);

        public static Tuple3D NegativeE1 { get; } = new Tuple3D(-1, 0, 0);

        public static Tuple3D NegativeE2 { get; } = new Tuple3D(0, -1, 0);

        public static Tuple3D NegativeE3 { get; } = new Tuple3D(0, 0, -1);


        public static Tuple3D CreateAffineVector(double x, double y)
        {
            return new Tuple3D(x, y, 0);
        }

        public static Tuple3D CreateAffinePoint(double x, double y)
        {
            return new Tuple3D(x, y, 1);
        }


        public static Tuple3D operator -(Tuple3D v1)
        {
            Debug.Assert(!v1.IsInvalid);

            return new Tuple3D(-v1.X, -v1.Y, -v1.Z);
        }

        public static Tuple3D operator +(Tuple3D v1, Tuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Tuple3D operator -(Tuple3D v1, Tuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static Tuple3D operator *(Tuple3D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        public static Tuple3D operator *(double s, Tuple3D v1)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        public static ComplexTuple3D operator *(Tuple3D v1, Complex s)
        {
            Debug.Assert(
                !v1.IsInvalid && 
                !double.IsNaN(s.Real) &&
                !double.IsNaN(s.Imaginary)
            );

            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        public static ComplexTuple3D operator *(Complex s, Tuple3D v1)
        {
            Debug.Assert(
                !v1.IsInvalid && 
                !double.IsNaN(s.Real) &&
                !double.IsNaN(s.Imaginary)
            );

            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        public static Tuple3D operator /(Tuple3D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            s = 1.0d / s;

            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        public static bool operator ==(Tuple3D v1, Tuple3D v2)
        {
            Debug.Assert(v1.IsValid && v2.IsValid);

            return v1.Equals(v2);
        }

        public static bool operator !=(Tuple3D v1, Tuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return !v1.Equals(v2);
        }


        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index <= 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return 0.0d;
            }
        }

        public bool IsValid =>
           !double.IsNaN(X) && !double.IsNaN(Y) && !double.IsNaN(Z);
        
        public bool IsInvalid =>
            double.IsNaN(X) || double.IsNaN(Y) || double.IsNaN(Z);


        public Tuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(!IsInvalid);
        }

        public Tuple3D(ITuple3D v)
        {
            Debug.Assert(!v.IsInvalid);

            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }


        public bool Equals(Tuple3D tuple)
        {
            return X.Equals(tuple.X) && 
                   Y.Equals(tuple.Y) && 
                   Z.Equals(tuple.Z);
        }

        public bool Equals(Tuple3D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj is Tuple3D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
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