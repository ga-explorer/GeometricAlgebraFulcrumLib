using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using TextComposerLib;

namespace EuclideanGeometryLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public readonly struct ComplexTuple3D : IComplexTuple3D, IEnumerable<Complex>
    {
        public static ComplexTuple3D Zero { get; }
            = new ComplexTuple3D(0, 0, 0);


        public static ComplexTuple3D operator -(ComplexTuple3D v1)
        {
            Debug.Assert(!v1.IsInvalid);

            return new ComplexTuple3D(
                -v1.X, 
                -v1.Y,
                -v1.Z
            );
        }

        public static ComplexTuple3D operator +(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new ComplexTuple3D(
                v1.X + v2.X, 
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        public static ComplexTuple3D operator -(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return new ComplexTuple3D(
                v1.X - v2.X, 
                v1.Y - v2.Y,
                v1.Z - v2.Z
            );
        }

        public static ComplexTuple3D operator *(ComplexTuple3D v1, double s)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }

        public static ComplexTuple3D operator *(double s, ComplexTuple3D v1)
        {
            Debug.Assert(!v1.IsInvalid && !double.IsNaN(s));

            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }

        public static ComplexTuple3D operator /(ComplexTuple3D v1, Complex s)
        {
            Debug.Assert(!v1.IsInvalid && !s.IsNaN());

            s = s.Reciprocal();

            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }

        public static bool operator ==(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return
                v1.X.IsAlmostEqual(v2.X) &&
                v1.Y.IsAlmostEqual(v2.Y) &&
                v1.Z.IsAlmostEqual(v2.Z);
        }

        public static bool operator !=(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            Debug.Assert(!v1.IsInvalid && !v2.IsInvalid);

            return
                !v1.X.IsAlmostEqual(v2.X) ||
                !v1.Y.IsAlmostEqual(v2.Y) ||
                !v1.Z.IsAlmostEqual(v2.Z);
        }


        public Complex X { get; }

        public Complex Y { get; }

        public Complex Z { get; }

        public Complex Item1
            => X;

        public Complex Item2
            => Y;

        public Complex Item3
            => Z;

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Complex this[int index]
        {
            get
            {
                Debug.Assert(index == 0 || index == 1 || index == 2);

                if (index == 0) return X;
                if (index == 1) return Y;
                if (index == 2) return Z;

                return 0.0d;
            }

        }

        public bool IsValid 
            => !X.IsNaN() && !Y.IsNaN() && !Z.IsNaN();

        public bool IsInvalid =>
            X.IsNaN() || Y.IsNaN() || Z.IsNaN();


        public ComplexTuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(!IsInvalid);
        }

        public ComplexTuple3D(Complex x, Complex y, Complex z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(!IsInvalid);
        }

        public ComplexTuple3D(IComplexTuple3D tuple)
        {
            Debug.Assert(!tuple.IsInvalid);

            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
        }


        public bool Equals(ComplexTuple3D tuple)
        {
            return X.Equals(tuple.X) && 
                   Y.Equals(tuple.Y) &&
                   Z.Equals(tuple.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            return obj is ComplexTuple3D && Equals((ComplexTuple3D)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X.GetHashCode();
                hashCode = (hashCode * 397) ^ Y.GetHashCode();
                hashCode = (hashCode * 397) ^ Z.GetHashCode();
                return hashCode;
            }
        }

        public IEnumerator<Complex> GetEnumerator()
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

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(X.Real, X.Imaginary, "G")
                .Append(", ")
                .AppendComplexNumber(Y.Real, Y.Imaginary, "G")
                .Append(", ")
                .AppendComplexNumber(Z.Real, Z.Imaginary, "G")
                .Append(")")
                .ToString();
        }
    }
}