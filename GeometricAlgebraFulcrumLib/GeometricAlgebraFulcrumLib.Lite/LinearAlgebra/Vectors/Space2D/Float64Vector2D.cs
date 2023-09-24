using System.Collections;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra.Vectors.Space2D
{
    /// <summary>
    /// An immutable 2-tuple of double precision numbers
    /// </summary>
    public sealed record Float64Vector2D :
        IFloat64Vector2D,
        IFloat64Multivector2D
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
        public static Float64Vector2D CreateEqualXy(double x)
        {
            var scalar = new Float64Scalar(x);

            return new Float64Vector2D(scalar, scalar);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D CreateUnitVector(double x, double y)
        {
            var s = x * x + y * y;

            if (s.IsZero()) return UnitSymmetric;

            s = 1d / Math.Sqrt(s);

            return new Float64Vector2D(x * s, y * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector2D CreateSymmetricVector(double vectorLength)
        {
            var scalar = new Float64Scalar(vectorLength / 2d.Sqrt());

            return new Float64Vector2D(scalar, scalar);
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
            s = 1d / s;

            return new Float64Vector2D(v1.X * s, v1.Y * s);
        }

        
        public int VSpaceDimensions 
            => 2;
        
        public Float64Scalar Scalar
            => Float64Scalar.Zero;

        public Float64Scalar Scalar1
            => X;
        
        public Float64Scalar Scalar2
            => Y;
        
        public Float64Scalar Scalar12 
            => Float64Scalar.Zero;

        public int Count 
            => 4;

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
        public Float64Scalar this[int index]
        {
            get
            {
                if (index is < 0 or > 3)
                    throw new IndexOutOfRangeException();

                return index switch
                {
                    1 => X,
                    2 => Y,
                    _ => Float64Scalar.Zero
                };
            }

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
        public bool IsValid()
        {
            return X.IsValid() &&
                   Y.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return X.IsZero() &&
                   Y.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1E-12)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64PlanarAngle GetPolarAngle()
        {
            return Math.Atan2(Y, X);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return (X.Square() + Y.Square()).Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return X.Square() + Y.Square();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Multivector2D ToMultivector2D()
        {
            return Float64Multivector2D.Create(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Negative()
        {
            return new Float64Vector2D(-Scalar1, -Scalar2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D GradeInvolution()
        {
            return Negative();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Reverse()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Inverse()
        {
            var normSquared = NormSquared();

            return normSquared.IsZero() 
                ? throw new InvalidOperationException() 
                : this / normSquared.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Normal2D()
        {
            return Float64Vector2D.Create(-Scalar2, Scalar1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Normal2D(Float64Scalar scalingFactor)
        {
            return Float64Vector2D.Create(
                -Scalar2 * scalingFactor,
                Scalar1 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Dual2D()
        {
            return Float64Vector2D.Create(Scalar2, -Scalar1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D Dual2D(Float64Scalar scalingFactor)
        {
            return Float64Vector2D.Create(
                Scalar2 * scalingFactor,
                -Scalar1 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D UnDual2D()
        {
            return Float64Vector2D.Create(-Scalar2, Scalar1);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector2D UnDual2D(Float64Scalar scalingFactor)
        {
            return Float64Vector2D.Create(
                -Scalar2 * scalingFactor,
                Scalar1 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Float64Scalar> GetEnumerator()
        {
            yield return Scalar;
            yield return Scalar1;
            yield return Scalar2;
            yield return Scalar12;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({X:G10})<1> + ({Y:G10})<2>";
        }
    }
}
