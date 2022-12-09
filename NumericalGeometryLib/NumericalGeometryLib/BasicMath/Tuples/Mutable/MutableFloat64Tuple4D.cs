using System;
using System.Diagnostics;
using System.Text;

namespace NumericalGeometryLib.BasicMath.Tuples.Mutable
{
    public sealed class MutableFloat64Tuple4D : 
        IFloat64Tuple4D
    {
        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double W { get; set; }

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public double Item4 => W;

        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y) &&
                   !double.IsNaN(Z) &&
                   !double.IsNaN(W);
        }


        public MutableFloat64Tuple4D()
        {
        }

        public MutableFloat64Tuple4D(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;

            Debug.Assert(IsValid());
        }

        public MutableFloat64Tuple4D(IFloat64Tuple4D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
            W = tuple.W;

            Debug.Assert(IsValid());
        }


        public MutableFloat64Tuple4D SetTuple(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple4D SetTuple(IFloat64Tuple4D tuple)
        {
            X = tuple.X;
            Y = tuple.Y;
            Z = tuple.Z;
            W = tuple.W;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple4D SetToZero()
        {
            X = 0;
            Y = 0;
            Z = 0;
            W = 0;

            return this;
        }

        public MutableFloat64Tuple4D SetDirection(IFloat64Tuple4D direction)
        {
            var oldLength2 = 
                direction.X * direction.X + 
                direction.Y * direction.Y + 
                direction.Z * direction.Z +
                direction.W * direction.W;

            var newLength2 = X * X + Y * Y + Z * Z + W * W;

            var s = Math.Sqrt(newLength2 / oldLength2);

            X = direction.X * s;
            Y = direction.Y * s;
            Z = direction.Z * s;
            W = direction.W * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple4D SetLength(double d)
        {
            var s = d / Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

            X = X * s;
            Y = Y * s;
            Z = Z * s;
            W = W * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple4D SetLengthToUnit()
        {
            var s = 1 / Math.Sqrt(X * X + Y * Y + Z * Z + W * W);

            X = X * s;
            Y = Y * s;
            Z = Z * s;
            W = W * s;

            Debug.Assert(IsValid());

            return this;
        }

        public MutableFloat64Tuple4D SetToNegative()
        {
            X = -X;
            Y = -Y;
            Z = -Z;
            W = -W;

            return this;
        }

        public MutableFloat64Tuple4D SetToSameSide(IFloat64Tuple4D direction)
        {
            if (X * direction.X + Y * direction.Y + Z * direction.Z + W * direction.W >= 0)
                return this;

            X = -X;
            Y = -Y;
            Z = -Z;
            W = -W;

            return this;
        }

        public MutableFloat64Tuple4D SetToOtherSide(IFloat64Tuple4D direction)
        {
            if (X * direction.X + Y * direction.Y + Z * direction.Z + W * direction.W <= 0)
                return this;

            X = -X;
            Y = -Y;
            Z = -Z;
            W = -W;

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
                .Append(", ")
                .Append(W.ToString("G"))
                .Append(")")
                .ToString();
        }
    }
}