using System.Diagnostics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;

namespace NumericalGeometryLib.BasicMath.Tuples.Immutable
{
    public sealed record Tuple4D : 
        ITuple4D
    {
        public static Tuple4D Zero { get; } = new Tuple4D(0, 0, 0, 0);

        public static Tuple4D E1 { get; } = new Tuple4D(1, 0, 0, 0);

        public static Tuple4D E2 { get; } = new Tuple4D(0, 1, 0, 0);

        public static Tuple4D E3 { get; } = new Tuple4D(0, 0, 1, 0);

        public static Tuple4D E4 { get; } = new Tuple4D(0, 0, 0, 1);

        public static Tuple4D NegativeE1 { get; } = new Tuple4D(-1, 0, 0, 0);

        public static Tuple4D NegativeE2 { get; } = new Tuple4D(0, -1, 0, 0);

        public static Tuple4D NegativeE3 { get; } = new Tuple4D(0, 0, -1, 0);
        
        public static Tuple4D NegativeE4 { get; } = new Tuple4D(0, 0, 0, -1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D CreateAffineVector(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D CreateAffinePoint(double x, double y, double z)
        {
            return new Tuple4D(x, y, z, 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1)
        {
            return new Tuple4D(-v1.X, -v1.Y, -v1.Z, -v1.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(Tuple4D v1, ITuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(ITuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator +(Tuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1, ITuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(ITuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator -(Tuple4D v1, Tuple4D v2)
        {
            return new Tuple4D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator *(Tuple4D v1, double s)
        {
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator *(double s, Tuple4D v1)
        {
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple4D operator /(Tuple4D v1, double s)
        {
            Debug.Assert(!s.IsAlmostZero());

            s = 1.0d / s;
            return new Tuple4D(v1.X * s, v1.Y * s, v1.Z * s, v1.W * s);
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

        public double Item1 => X;

        public double Item2 => Y;

        public double Item3 => Z;

        public double Item4 => W;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index is >= 0 and <= 3);

                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    3 => W,
                    _ => 0.0d
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(X) &&
                   !double.IsNaN(Y) &&
                   !double.IsNaN(Z) &&
                   !double.IsNaN(W);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(double x, double y, double z, double w)
        {
            Debug.Assert(
                x.IsNotNaN() &&
                y.IsNotNaN() &&
                z.IsNotNaN() &&
                w.IsNotNaN()
            );

            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple4D(IQuad<double> v)
        {
            Debug.Assert(
                v.Item1.IsNotNaN() &&
                v.Item2.IsNotNaN() &&
                v.Item3.IsNotNaN() &&
                v.Item4.IsNotNaN()
            );

            X = v.Item1;
            Y = v.Item2;
            Z = v.Item3;
            W = v.Item4;

            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G}, {Z:G}, {W:G})";
        }
    }
}
