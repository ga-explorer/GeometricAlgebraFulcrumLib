using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public sealed record ComplexVector2D :
        IComplexVector2D,
        IEnumerable<Complex>
    {
        public static ComplexVector2D Zero { get; }
            = new ComplexVector2D(0, 0);


        public static ComplexVector2D operator -(ComplexVector2D v1)
        {
            return new ComplexVector2D(-v1.X, -v1.Y);
        }

        public static ComplexVector2D operator +(ComplexVector2D v1, ComplexVector2D v2)
        {
            return new ComplexVector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static ComplexVector2D operator -(ComplexVector2D v1, ComplexVector2D v2)
        {
            return new ComplexVector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static ComplexVector2D operator *(ComplexVector2D v1, double s)
        {
            return new ComplexVector2D(v1.X * s, v1.Y * s);
        }

        public static ComplexVector2D operator *(double s, ComplexVector2D v1)
        {
            return new ComplexVector2D(v1.X * s, v1.Y * s);
        }

        public static ComplexVector2D operator /(ComplexVector2D v1, Complex s)
        {
            s = 1.0d / s;

            return new ComplexVector2D(v1.X * s, v1.Y * s);
        }


        public Complex X { get; }

        public Complex Y { get; }

        public Complex Item1
        {
            get { return X; }
        }

        public Complex Item2
        {
            get { return Y; }
        }

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Complex this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }

        }

        public bool IsValid()
        {
            return !X.IsNaN() && !Y.IsNaN();
        }


        public ComplexVector2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        public ComplexVector2D(Complex x, Complex y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        public ComplexVector2D(IComplexVector2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;

            Debug.Assert(IsValid());
        }


        public IEnumerator<Complex> GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return X;
            yield return Y;
        }

        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(X.Real, X.Imaginary, "G")
                .Append(", ")
                .AppendComplexNumber(Y.Real, Y.Imaginary, "G")
                .Append(")")
                .ToString();
        }
    }
}