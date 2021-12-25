using System;
using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace NumericalGeometryLib.BasicMath.Tuples.Immutable
{
    /// <inheritdoc cref="ITuple3D" />
    /// <summary>
    /// A 3-tuple of double precision coordinates
    /// </summary>
    public sealed record Tuple3D : 
        ITuple3D
    {
        public static Tuple3D Zero { get; } = new Tuple3D(0, 0, 0);

        public static Tuple3D E1 { get; } = new Tuple3D(1, 0, 0);

        public static Tuple3D E2 { get; } = new Tuple3D(0, 1, 0);

        public static Tuple3D E3 { get; } = new Tuple3D(0, 0, 1);

        public static Tuple3D NegativeE1 { get; } = new Tuple3D(-1, 0, 0);

        public static Tuple3D NegativeE2 { get; } = new Tuple3D(0, -1, 0);

        public static Tuple3D NegativeE3 { get; } = new Tuple3D(0, 0, -1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D CreateAffineVector(double x, double y)
        {
            return new Tuple3D(x, y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D CreateAffinePoint(double x, double y)
        {
            return new Tuple3D(x, y, 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator -(Tuple3D v1)
        {
            return new Tuple3D(-v1.X, -v1.Y, -v1.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator +(Tuple3D v1, ITuple3D v2)
        {
            return new Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator +(ITuple3D v1, Tuple3D v2)
        {
            return new Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator +(Tuple3D v1, Tuple3D v2)
        {
            return new Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator -(Tuple3D v1, ITuple3D v2)
        {
            return new Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator -(ITuple3D v1, Tuple3D v2)
        {
            return new Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator -(Tuple3D v1, Tuple3D v2)
        {
            return new Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator *(Tuple3D v1, double s)
        {
            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator *(double s, Tuple3D v1)
        {
            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Tuple3D v1, Complex s)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Complex s, Tuple3D v1)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D operator /(Tuple3D v1, double s)
        {
            s = 1.0d / s;

            return new Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Tuple3D CreateUnitVector(double x, double y, double z)
        {
            var s = Math.Sqrt(x * x + y * y + z * z);
            
            if (s.IsAlmostZero()) return Tuple3D.Zero;

            s = 1d / s;
            return new Tuple3D(x * s, y * s, z * s);
        }


        public double X { get; }

        public double Y { get; }

        public double Z { get; }

        public double Item1 
            => X;

        public double Item2 
            => Y;

        public double Item3 
            => Z;

        /// <summary>
        /// Get or set the ith component of this tuple
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                Debug.Assert(index is >= 0 and <= 2);

                return index switch
                {
                    0 => X,
                    1 => Y,
                    2 => Z,
                    _ => 0.0d
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;

            Debug.Assert(IsValid());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Tuple3D(ITuple3D v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;

            Debug.Assert(IsValid());
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return !double.IsNaN(X) && 
                   !double.IsNaN(Y) && 
                   !double.IsNaN(Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G}, {Y:G}, {Z:G})";
        }
    }
}