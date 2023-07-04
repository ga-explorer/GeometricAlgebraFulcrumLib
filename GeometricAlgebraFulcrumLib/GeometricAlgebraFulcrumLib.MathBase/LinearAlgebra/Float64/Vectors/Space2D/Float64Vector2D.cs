using System.Collections;
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
        IFloat64Vector2D,
        IReadOnlyList<double>
    {
        public static Float64Vector2D Zero { get; } 
            = new Float64Vector2D(
                Float64Scalar.Zero, 
                Float64Scalar.Zero
            );

        public static Float64Vector2D E1 { get; } 
            = new Float64Vector2D(
                Float64Scalar.One, 
                Float64Scalar.Zero
            );

        public static Float64Vector2D E2 { get; } 
            = new Float64Vector2D(
                Float64Scalar.Zero, 
                Float64Scalar.One
            );

        public static Float64Vector2D NegativeE1 { get; }
            = new Float64Vector2D(
                Float64Scalar.NegativeOne, 
                Float64Scalar.Zero
            );

        public static Float64Vector2D NegativeE2 { get; } 
            = new Float64Vector2D(
                Float64Scalar.Zero, 
                Float64Scalar.NegativeOne
            );

        public static Float64Vector2D Symmetric { get; } 
            = new Float64Vector2D(
                Float64Scalar.One, 
                Float64Scalar.One
            );

        public static Float64Vector2D UnitSymmetric { get; } 
            = new Float64Vector2D(
                Float64Scalar.InvSqrt2, 
                Float64Scalar.InvSqrt2
            );

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(int x, int y)
        {
            return new Float64Vector2D(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(long x, long y)
        {
            return new Float64Vector2D(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(float x, float y)
        {
            return new Float64Vector2D(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(double x, double y)
        {
            return new Float64Vector2D(x, y);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(Float64Scalar x, Float64Scalar y)
        {
            return new Float64Vector2D(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D Create(IPair<double> tuple)
        {
            return new Float64Vector2D(tuple.Item1, tuple.Item2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D CreateFromPolar(Float64PlanarAngle angle)
        {
            return new Float64Vector2D(
                angle.Cos(), 
                angle.Sin()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D CreateFromPolar(Float64Scalar length, Float64PlanarAngle angle)
        {
            return new Float64Vector2D(
                length * angle.Cos(), 
                length * angle.Sin()
            );
        }


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
        public static Float64Vector2D operator +(Float64Vector2D v1, IFloat64Vector2D v2)
        {
            return new Float64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator +(IFloat64Vector2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(Float64Vector2D v1, Float64Vector2D v2)
        {
            return new Float64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(Float64Vector2D v1, IFloat64Vector2D v2)
        {
            return new Float64Vector2D(v1.X - v2.X, v1.Y - v2.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D operator -(IFloat64Vector2D v1, Float64Vector2D v2)
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
            s = Float64Scalar.One / s;

            return new Float64Vector2D(v1.X * s, v1.Y * s);
        }

        
        public int VSpaceDimensions 
            => 2;
        
        public int Count 
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
        private Float64Vector2D(Float64Scalar x, Float64Scalar y)
        {
            X = x;
            Y = y;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Deconstruct(out double x, out double y)
        {
            x = X.Value;
            y = Y.Value;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<double> GetEnumerator()
        {
            yield return X.Value;
            yield return Y.Value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G})<1> + ({Y:G})<2>";
        }
    }
}
