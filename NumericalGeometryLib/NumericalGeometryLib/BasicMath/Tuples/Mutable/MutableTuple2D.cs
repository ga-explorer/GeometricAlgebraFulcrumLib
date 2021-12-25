using System;
using System.Diagnostics;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableTuple2D : ITuple2D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Item1
        {
            get { return X; }
        }

        public double Item2
        {
            get { return Y; }
        }

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
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) return X;
                if (index == 1) return Y;

                return 0.0d;
            }
            set
            {
                Debug.Assert(!double.IsNaN(value));
                Debug.Assert(index == 0 || index == 1);

                if (index == 0) X = value;
                if (index == 1) Y = value;
            }
        }


        public MutableTuple2D()
        {
        }

        public MutableTuple2D(double x, double y)
        {
            X = x;
            Y = y;

            Debug.Assert(IsValid());
        }

        public MutableTuple2D(ITuple2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;

            Debug.Assert(IsValid());
        }


        public MutableTuple2D SetTuple(double x, double y)
        {
            X = x;
            Y = y;
            
            Debug.Assert(IsValid());

            return this;
        }

        public MutableTuple2D SetTuple(ITuple2D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            
            Debug.Assert(IsValid());

            return this;
        }

        public MutableTuple2D SetToZero()
        {
            X = 0;
            Y = 0;

            return this;
        }

        public MutableTuple2D SetDirection(ITuple2D direction)
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

        public MutableTuple2D SetLength(double d)
        {
            var s = d / Math.Sqrt(X * X + Y * Y);

            X = X * s;
            Y = Y * s;
            
            Debug.Assert(IsValid());

            return this;
        }

        public MutableTuple2D SetLengthToUnit()
        {
            var s = 1 / Math.Sqrt(X * X + Y * Y);

            X = X * s;
            Y = Y * s;
            
            Debug.Assert(IsValid());

            return this;
        }

        public MutableTuple2D SetToNegative()
        {
            X = -X;
            Y = -Y;

            return this;
        }

        public MutableTuple2D SetToSameSide(ITuple2D direction)
        {
            if (X * direction.X + Y * direction.Y >= 0)
                return this;

            X = -X;
            Y = -Y;

            return this;
        }

        public MutableTuple2D SetToOtherSide(ITuple2D direction)
        {
            if (X * direction.X + Y * direction.Y <= 0)
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