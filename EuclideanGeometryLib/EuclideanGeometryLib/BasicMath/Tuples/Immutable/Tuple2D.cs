using System;
using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public readonly struct Tuple2D : 
        ITuple2D,
        IEquatable<Tuple2D>,
        IEquatable<Tuple2D?>
    {
        public static Tuple2D Zero { get; } = new Tuple2D(0, 0);

        public static Tuple2D E1 { get; } = new Tuple2D(1, 0);

        public static Tuple2D E2 { get; } = new Tuple2D(0, 1);


        public static Tuple2D operator -(Tuple2D v1)
        {
            Debug.Assert(!v1.IsInvalid);

            return new Tuple2D(-v1.X, -v1.Y);
        }

        public static Tuple2D operator +(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Tuple2D operator -(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Tuple2D operator *(Tuple2D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static Tuple2D operator *(double s, Tuple2D v1)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static Tuple2D operator /(Tuple2D v1, double s)
        {
            Debug.Assert(v1.IsValid && !double.IsNaN(s));

            s = 1.0d / s;

            return new Tuple2D(v1.X * s, v1.Y * s);
        }

        public static bool operator ==(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(v1.IsValid && v2.IsValid);

            return v1.Equals(v2);
        }

        public static bool operator !=(Tuple2D v1, Tuple2D v2)
        {
            Debug.Assert(v1.IsValid && v2.IsValid);

            return !v1.Equals(v2);
        }


        public double X { get; }

        public double Y { get; }

        public double Item1
            => X;

        public double Item2
            => Y;

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }

        }

        public bool IsValid =>
            !double.IsNaN(X) && 
            !double.IsNaN(Y);

        public bool IsInvalid =>
            double.IsNaN(X) || 
            double.IsNaN(Y);


        public Tuple2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(!IsInvalid);
        }

        public Tuple2D(ITuple2D tuple)
        {
            Debug.Assert(!tuple.IsInvalid);

            X = tuple.X;
            Y = tuple.Y;
        }


        public bool Equals(Tuple2D tuple)
        {
            return X.Equals(tuple.X) && Y.Equals(tuple.Y);
        }

        public bool Equals(Tuple2D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;

            return obj is Tuple2D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .Append(X.ToString("G"))
                .Append(", ")
                .Append(Y.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}
