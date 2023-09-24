using System.Diagnostics;
using System.Text;
using GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Tuples
{
    public sealed class MutableFloat64Tuple2D :
        IFloat64Vector2D
    {
        public Float64Scalar X { get; set; }

        public Float64Scalar Y { get; set; }

        public int VSpaceDimensions
            => 2;

        public double Item1
            => X.Value;

        public double Item2
            => Y.Value;

        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y);
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
                Debug.Assert(index is 0 or 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }
            set
            {
                Debug.Assert(!double.IsNaN(value));
                Debug.Assert(index is 0 or 1);

                if (index == 0) X = value;
                if (index == 1) Y = value;
            }
        }


        public MutableFloat64Tuple2D()
        {
        }

        public MutableFloat64Tuple2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        public MutableFloat64Tuple2D(IFloat64Vector2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;

            Debug.Assert(IsValid());
        }


        public MutableFloat64Tuple2D SetTuple(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple2D SetTuple(IFloat64Vector2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple2D SetToZero()
        {
            X = 0;
            Y = 0;

            return this;
        }

        public MutableFloat64Tuple2D SetDirection(IFloat64Vector2D direction)
        {
            var oldLength2 =
                direction.X * direction.X +
                direction.Y * direction.Y;

            var newLength2 = X * X + Y * Y;

            var s = Math.Sqrt(newLength2 / oldLength2);

            X = direction.X * s;
            Y = direction.Y * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple2D SetLength(double d)
        {
            var s = d / Math.Sqrt(X * X + Y * Y);

            X = X * s;
            Y = Y * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple2D SetLengthToUnit()
        {
            var s = 1 / Math.Sqrt(X * X + Y * Y);

            X = X * s;
            Y = Y * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple2D SetToNegative()
        {
            X = -X;
            Y = -Y;

            return this;
        }

        public MutableFloat64Tuple2D SetToSameSide(IFloat64Vector2D direction)
        {
            if (!(X * direction.X + Y * direction.Y).IsNegative())
                return this;

            X = -X;
            Y = -Y;

            return this;
        }

        public MutableFloat64Tuple2D SetToOtherSide(IFloat64Vector2D direction)
        {
            if (!(X * direction.X + Y * direction.Y).IsPositive())
                return this;

            X = -X;
            Y = -Y;

            return this;
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