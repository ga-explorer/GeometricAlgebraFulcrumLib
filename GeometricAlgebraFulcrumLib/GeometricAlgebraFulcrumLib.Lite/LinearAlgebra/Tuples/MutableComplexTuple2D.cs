using System.Collections;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using MathNet.Numerics;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples
{
    public sealed class MutableComplexTuple2D :
        IComplexVector2D,
        IEnumerable<Complex>
    {
        public double RealX { get; set; }

        public double ImagX { get; set; }

        public double RealY { get; set; }

        public double ImagY { get; set; }


        public Complex X => new Complex(RealX, ImagX);

        public Complex Y => new Complex(RealY, ImagY);

        public Complex Item1 => X;

        public Complex Item2 => Y;

        public bool IsValid()
        {
            return !double.IsNaN(RealX) &&
                   !double.IsNaN(ImagX) &&
                   !double.IsNaN(RealY) &&
                   !double.IsNaN(ImagY);
        }


        /// <summary>
        /// Get or set the ith component of this tuple
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
            set
            {
                Debug.Assert(!value.IsNaN());
                Debug.Assert(index is 0 or 1);

                if (index == 0)
                {
                    RealX = value.Real;
                    ImagX = value.Imaginary;
                }
                else if (index == 1)
                {
                    RealY = value.Real;
                    ImagY = value.Imaginary;
                }
            }
        }


        public MutableComplexTuple2D()
        {
        }

        public MutableComplexTuple2D(double x, double y)
        {
            RealX = x;
            RealY = y;

            Debug.Assert(IsValid());
        }

        public MutableComplexTuple2D(Complex x, Complex y)
        {
            RealX = x.Real;
            RealY = y.Real;

            ImagX = x.Imaginary;
            ImagY = y.Imaginary;

            Debug.Assert(IsValid());
        }

        public MutableComplexTuple2D(IFloat64Vector2D tuple)
        {
            RealX = tuple.X;
            RealY = tuple.Y;

            Debug.Assert(IsValid());
        }

        public MutableComplexTuple2D(IComplexVector2D tuple)
        {
            RealX = tuple.X.Real;
            RealY = tuple.Y.Real;

            ImagX = tuple.X.Imaginary;
            ImagY = tuple.Y.Imaginary;

            Debug.Assert(IsValid());
        }


        public MutableComplexTuple2D SetTuple(double x, double y)
        {
            RealX = x;
            RealY = y;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableComplexTuple2D SetTuple(Complex x, Complex y)
        {
            RealX = x.Real;
            RealY = y.Real;

            ImagX = x.Imaginary;
            ImagY = y.Imaginary;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableComplexTuple2D SetTuple(IFloat64Vector2D tuple)
        {
            RealX = tuple.X;
            RealY = tuple.Y;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableComplexTuple2D SetTuple(IComplexVector2D tuple)
        {
            RealX = tuple.X.Real;
            RealY = tuple.Y.Real;

            ImagX = tuple.X.Imaginary;
            ImagY = tuple.Y.Imaginary;

            Debug.Assert(IsValid());

            return this;
        }


        public IEnumerator<Complex> GetEnumerator()
        {
            yield return new Complex(RealX, ImagX);
            yield return new Complex(RealY, ImagY);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return new Complex(RealX, ImagX);
            yield return new Complex(RealY, ImagY);
        }


        public override string ToString()
        {
            return new StringBuilder()
                .Append("(")
                .AppendComplexNumber(RealX, ImagX, "G")
                .Append(", ")
                .AppendComplexNumber(RealY, ImagY, "G")
                .Append(")")
                .ToString();
        }
    }
}