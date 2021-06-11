using System;
using System.Diagnostics;
using System.Text;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    public readonly struct Tuple4D : 
        ITuple4D,
        IEquatable<Tuple4D>,
        IEquatable<Tuple4D?>
    {
        public Tuple4D CreateAffineVector(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 0);
        }

        public Tuple4D CreateAffinePoint(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 1);
        }


        public static Tuple4D operator -(Tuple4D v1)
        {
            Debug.Assert(!v1.IsInvalid);

            return new Tuple4D(-v1.X, -v1.Y, -v1.Z, -v1.W);
        }

        public static Tuple4D operator +(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        public static Tuple4D operator -(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        public static Tuple4D operator *(Tuple4D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static Tuple4D operator *(double s, Tuple4D v1)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static Tuple4D operator /(Tuple4D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        public static bool operator ==(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(v1.IsValid && v2.IsValid);

            return v1.Equals(v2);
        }

        public static bool operator !=(Tuple4D v1, Tuple4D v2)
        {
            Debug.Assert(v1.IsValid && v2.IsValid);

            return !v1.Equals(v2);
        }

        
        /// <summary>
        /// The 1st component of this tuple. If this tuple holds a quaternion, this is the 1st component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double X { get; }

        /// <summary>
        /// The 2nd component of this tuple. If this tuple holds a quaternion, this is the 2nd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// The 3rd component of this tuple. If this tuple holds a quaternion, this is the 3rd component
        /// of its imaginary (i.e. vector) part
        /// </summary>
        public double Z { get; }

        /// <summary>
        /// The 4th component of this tuple. If this tuple holds a quaternion, this is its scalar part
        /// </summary>
        public double W { get; }

        public double Item1
            => X;

        public double Item2
            => Y;

        public double Item3
            => Z;

        public double Item4
            => W;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index <= 3);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;
                if (index == 3) return W;

                return 0.0d;
            }
        }

        
        public bool IsValid =>
            !double.IsNaN(X) && 
            !double.IsNaN(Y) && 
            !double.IsNaN(Z) && 
            !double.IsNaN(W);
        
        public bool IsInvalid =>
            double.IsNaN(X) || 
            double.IsNaN(Y) || 
            double.IsNaN(Z) || 
            double.IsNaN(W);


        public Tuple4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Tuple4D(ITuple4D v)
        {
            Debug.Assert(!v.IsInvalid);

            X = v.X;
            Y = v.Y;
            Z = v.Z;
            W = v.W;
        }


        public bool Equals(Tuple4D tuple)
        {
            return X.Equals(tuple.X) && 
                   Y.Equals(tuple.Y) && 
                   Z.Equals(tuple.Z) && 
                   W.Equals(tuple.W);
        }

        public bool Equals(Tuple4D? other)
        {
            return other.HasValue && Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return obj is Tuple4D other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z, W);
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
                .Append(", ")
                .Append(W.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}
