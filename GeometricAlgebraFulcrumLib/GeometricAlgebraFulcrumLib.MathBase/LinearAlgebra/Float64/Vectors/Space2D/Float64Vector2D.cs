using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public sealed record Float64Vector2D :
        IFloat64Tuple2D
    {
        public static Float64Vector2D Zero { get; } = new Float64Vector2D(0, 0);

        public static Float64Vector2D E1 { get; } = new Float64Vector2D(1, 0);

        public static Float64Vector2D E2 { get; } = new Float64Vector2D(0, 1);

        public static Float64Vector2D NegativeE1 { get; } = new Float64Vector2D(-1, 0);

        public static Float64Vector2D NegativeE2 { get; } = new Float64Vector2D(0, -1);

        public static Float64Vector2D Symmetric { get; } = new Float64Vector2D(1, 1);

        public static Float64Vector2D UnitSymmetric { get; } = new Float64Vector2D(1d / 2d.Sqrt(), 1d / 2d.Sqrt());


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(Float64Vector2D v1)
        {
            return new Float64Vector2D(-v1.X, -v1.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator +(Float64Vector2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator +(Float64Vector2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator +(IFloat64Tuple2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(Float64Vector2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(Float64Vector2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(IFloat64Tuple2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator *(Float64Vector2D v1, double s)
        {
            return new Float64Vector2D(v1.X * s, v1.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator *(double s, Float64Vector2D v1)
        {
            return new Float64Vector2D(v1.X * s, v1.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator /(Float64Vector2D v1, double s)
        {
            s = 1.0d / s;

            return new Float64Vector2D(v1.X * s, v1.Y * s);
        }

        
        public int VSpaceDimensions 
            => 2;

        public Float64Scalar X { get; }

        public Float64Scalar Y { get; }

        public double Item1 
            => X.Value;

        public double Item2 
            => Y.Value;

        /// <summary>
        /// Get the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index is 0 or 1);

                return index switch
                {
                    0 => X,
                    1 => Y,
                    _ => 0.0d
                };
            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return X.IsValid() &&
                   Y.IsValid();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D(Float64Scalar x, Float64Scalar y)
        {
            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D(IPair<double> tuple)
        {
            X = tuple.Item1;
            Y = tuple.Item2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out double x, out double y)
        {
            x = X.Value;
            y = Y.Value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G})";
        }
    }
}
