using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using MathNet.Numerics;
using TextComposerLib;

namespace NumericalGeometryLib.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public sealed record ComplexTuple3D : 
        IComplexTuple3D, 
        IEnumerable<Complex>
    {
        public static ComplexTuple3D Zero { get; }
            = new ComplexTuple3D(0, 0, 0);


        public static ComplexTuple3D operator -(ComplexTuple3D v1)
        {
            return new ComplexTuple3D(
                -v1.X, 
                -v1.Y,
                -v1.Z
            );
        }

        public static ComplexTuple3D operator +(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            return new ComplexTuple3D(
                v1.X + v2.X, 
                v1.Y + v2.Y,
                v1.Z + v2.Z
            );
        }

        public static ComplexTuple3D operator -(ComplexTuple3D v1, ComplexTuple3D v2)
        {
            return new ComplexTuple3D(
                v1.X - v2.X, 
                v1.Y - v2.Y,
                v1.Z - v2.Z
            );
        }

        public static ComplexTuple3D operator *(ComplexTuple3D v1, double s)
        {
            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }

        public static ComplexTuple3D operator *(double s, ComplexTuple3D v1)
        {
            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }

        public static ComplexTuple3D operator /(ComplexTuple3D v1, Complex s)
        {
            s = s.Reciprocal();

            return new ComplexTuple3D(
                v1.X * s, 
                v1.Y * s,
                v1.Z * s
            );
        }
        

        public Complex X { get; }

        public Complex Y { get; }

        public Complex Z { get; }

        public Complex Item1 => X;

        public Complex Item2 => Y;

        public Complex Item3 => Z;

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

                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => 0.0d
                };
            }

        }

        public bool IsValid()
        {
            return !X.IsNaN() && !Y.IsNaN() && !Z.IsNaN();
        }


        public ComplexTuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }

        public ComplexTuple3D(Complex x, Complex y, Complex z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }

        public ComplexTuple3D(IComplexTuple3D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;

            Debug.Assert(IsValid());
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