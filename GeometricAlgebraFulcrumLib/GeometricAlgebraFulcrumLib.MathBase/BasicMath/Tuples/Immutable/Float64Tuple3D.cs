using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Tuples.Immutable
{
    /// <inheritdoc cref="IFloat64Tuple3D" />
    /// <summary>
    /// A 3-tuple of double precision coordinates
    /// </summary>
    public sealed record Float64Tuple3D : 
        IFloat64Tuple3D
    {
        public static Float64Tuple3D Zero { get; } = new Float64Tuple3D(0, 0, 0);

        public static Float64Tuple3D E1 { get; } = new Float64Tuple3D(1, 0, 0);

        public static Float64Tuple3D E2 { get; } = new Float64Tuple3D(0, 1, 0);

        public static Float64Tuple3D E3 { get; } = new Float64Tuple3D(0, 0, 1);

        public static Float64Tuple3D NegativeE1 { get; } = new Float64Tuple3D(-1, 0, 0);

        public static Float64Tuple3D NegativeE2 { get; } = new Float64Tuple3D(0, -1, 0);

        public static Float64Tuple3D NegativeE3 { get; } = new Float64Tuple3D(0, 0, -1);


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D CreateAffineVector(double x, double y)
        {
            return new Float64Tuple3D(x, y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D CreateAffinePoint(double x, double y)
        {
            return new Float64Tuple3D(x, y, 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator -(Float64Tuple3D v1)
        {
            return new Float64Tuple3D(-v1.X, -v1.Y, -v1.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator +(Float64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator +(IFloat64Tuple3D v1, Float64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator +(Float64Tuple3D v1, Float64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator -(Float64Tuple3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator -(IFloat64Tuple3D v1, Float64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator -(Float64Tuple3D v1, Float64Tuple3D v2)
        {
            return new Float64Tuple3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator *(Float64Tuple3D v1, double s)
        {
            //var x = (v1.X * s).NaNToZero();
            //var y = (v1.Y * s).NaNToZero();
            //var z = (v1.Z * s).NaNToZero();

            var x = v1.X * s;
            var y = v1.Y * s;
            var z = v1.Z * s;

            return new Float64Tuple3D(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator *(double s, Float64Tuple3D v1)
        {
            //var x = (s * v1.X).NaNToZero();
            //var y = (s * v1.Y).NaNToZero();
            //var z = (s * v1.Z).NaNToZero();

            var x = s * v1.X;
            var y = s * v1.Y;
            var z = s * v1.Z;

            return new Float64Tuple3D(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Float64Tuple3D v1, Complex s)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Complex s, Float64Tuple3D v1)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D operator /(Float64Tuple3D v1, double s)
        {
            s = 1.0d / s;

            return double.IsInfinity(s) 
                ? Zero 
                : new Float64Tuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Tuple3D CreateUnitVector(double x, double y, double z)
        {
            var s = Math.Sqrt(x * x + y * y + z * z);
            
            if (s.IsAlmostZero()) return Zero;

            s = 1d / s;
            return new Float64Tuple3D(x * s, y * s, z * s);
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
        public Float64Tuple3D(double x, double y, double z)
        {
            Debug.Assert(
                x.IsNotNaN() &&
                y.IsNotNaN() &&
                z.IsNotNaN()
            );

            X = x;
            Y = y;
            Z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Tuple3D(ITriplet<double> v)
        {
            Debug.Assert(
                v.Item1.IsNotNaN() &&
                v.Item2.IsNotNaN() &&
                v.Item3.IsNotNaN()
            );

            X = v.Item1;
            Y = v.Item2;
            Z = v.Item3;
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
            return $"({X:g5}, {Y:g5}, {Z:g5})";
        }
    }
}