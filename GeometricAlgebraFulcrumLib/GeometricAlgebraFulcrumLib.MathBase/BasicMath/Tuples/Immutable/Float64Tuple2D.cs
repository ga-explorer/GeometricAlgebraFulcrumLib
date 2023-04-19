using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public sealed record Float64Tuple2D : 
        IFloat64Tuple2D
    {
        public static Float64Tuple2D Zero { get; } = new Float64Tuple2D(0, 0);

        public static Float64Tuple2D E1 { get; } = new Float64Tuple2D(1, 0);

        public static Float64Tuple2D E2 { get; } = new Float64Tuple2D(0, 1);
        
        public static Float64Tuple2D NegativeE1 { get; } = new Float64Tuple2D(-1, 0);

        public static Float64Tuple2D NegativeE2 { get; } = new Float64Tuple2D(0, -1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator -(Float64Tuple2D v1)
        {
            return new Float64Tuple2D(-v1.X, -v1.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator +(Float64Tuple2D v1, Float64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator +(Float64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator +(IFloat64Tuple2D v1, Float64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator -(Float64Tuple2D v1, Float64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator -(Float64Tuple2D v1, IFloat64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator -(IFloat64Tuple2D v1, Float64Tuple2D v2)
        {
            return new Float64Tuple2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator *(Float64Tuple2D v1, double s)
        {
            return new Float64Tuple2D(v1.X * s, v1.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator *(double s, Float64Tuple2D v1)
        {
            return new Float64Tuple2D(v1.X * s, v1.Y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple2D operator /(Float64Tuple2D v1, double s)
        {
            s = 1.0d / s;

            return new Float64Tuple2D(v1.X * s, v1.Y * s);
        }
        

        public double X { get; }

        public double Y { get; }

        public double Item1 => X;

        public double Item2 => Y;

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
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple2D(double x, double y)
        {
            Debug.Assert(
                x.IsNotNaN() &&
                y.IsNotNaN()
            );

            X = x;
            Y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple2D(IPair<double> tuple)
        {
            Debug.Assert(
                tuple.Item1.IsNotNaN() &&
                tuple.Item2.IsNotNaN()
            );

            X = tuple.Item1;
            Y = tuple.Item2;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G})";
        }
    }
}
