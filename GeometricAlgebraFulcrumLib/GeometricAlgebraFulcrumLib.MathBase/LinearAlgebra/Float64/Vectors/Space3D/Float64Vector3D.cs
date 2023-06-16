using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Basis;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space3D
{
    /// <inheritdoc cref="IFloat64Tuple3D" />
    /// <summary>
    /// A 3-tuple of double precision coordinates
    /// </summary>
    public sealed record Float64Vector3D :
        IFloat64Tuple3D,
        IFloat64Multivector3D
    {
        public static Float64Vector3D Zero { get; } 
            = new Float64Vector3D(
                Float64Scalar.Zero, 
                Float64Scalar.Zero, 
                Float64Scalar.Zero
            );

        public static Float64Vector3D E1 { get; } 
            = new Float64Vector3D(
                Float64Scalar.One, 
                Float64Scalar.Zero, 
                Float64Scalar.Zero
            );

        public static Float64Vector3D E2 { get; } 
            = new Float64Vector3D(
                Float64Scalar.Zero, 
                Float64Scalar.One, 
                Float64Scalar.Zero
            );

        public static Float64Vector3D E3 { get; } 
            = new Float64Vector3D(
                Float64Scalar.Zero, 
                Float64Scalar.Zero, 
                Float64Scalar.One
            );

        public static Float64Vector3D NegativeE1 { get; } 
            = new Float64Vector3D(
                Float64Scalar.NegativeOne, 
                Float64Scalar.Zero, 
                Float64Scalar.Zero
            );

        public static Float64Vector3D NegativeE2 { get; } 
            = new Float64Vector3D(
                Float64Scalar.Zero, 
                Float64Scalar.NegativeOne, 
                Float64Scalar.Zero
            );

        public static Float64Vector3D NegativeE3 { get; } 
            = new Float64Vector3D(
                Float64Scalar.Zero, 
                Float64Scalar.Zero, 
                Float64Scalar.NegativeOne
            );

        public static Float64Vector3D Symmetric { get; } 
            = new Float64Vector3D(
                Float64Scalar.One,
                Float64Scalar.One, 
                Float64Scalar.One
            );

        public static Float64Vector3D UnitSymmetric { get; } 
            = new Float64Vector3D(
                Float64Scalar.InvSqrt3,
                Float64Scalar.InvSqrt3, 
                Float64Scalar.InvSqrt3
            );

        public static IReadOnlyList<Float64Vector3D> BasisVectors { get; }
            = new[] { E1, E2, E3 };

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D Create(Float64Scalar x, Float64Scalar y, Float64Scalar z)
        {
            return new Float64Vector3D(x, y, z);
        }
        
        //[MethodImpl(MethodImplOptions.AggressiveInlining)]
        //public static Float64Vector3D CreateInstance(double x, double y, double z)
        //{
        //    return new Float64Vector3D(x, y, z);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D CreateAffineVector(Float64Scalar x, Float64Scalar y)
        {
            return new Float64Vector3D(x, y, 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D CreateAffinePoint(Float64Scalar x, Float64Scalar y)
        {
            return new Float64Vector3D(x, y, 1);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator -(Float64Vector3D v1)
        {
            return new Float64Vector3D(-v1.X, -v1.Y, -v1.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator +(Float64Vector3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator +(IFloat64Tuple3D v1, Float64Vector3D v2)
        {
            return new Float64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator +(Float64Vector3D v1, Float64Vector3D v2)
        {
            return new Float64Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator -(Float64Vector3D v1, IFloat64Tuple3D v2)
        {
            return new Float64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator -(IFloat64Tuple3D v1, Float64Vector3D v2)
        {
            return new Float64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator -(Float64Vector3D v1, Float64Vector3D v2)
        {
            return new Float64Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator *(Float64Vector3D v1, double s)
        {
            var x = v1.X * s;
            var y = v1.Y * s;
            var z = v1.Z * s;

            return new Float64Vector3D(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator *(double s, Float64Vector3D v1)
        {
            var x = s * v1.X;
            var y = s * v1.Y;
            var z = s * v1.Z;

            return new Float64Vector3D(x, y, z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Float64Vector3D v1, Complex s)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ComplexTuple3D operator *(Complex s, Float64Vector3D v1)
        {
            return new ComplexTuple3D(v1.X * s, v1.Y * s, v1.Z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D operator /(Float64Vector3D v1, double s)
        {
            s = 1.0d / s;

            return new Float64Vector3D(v1.X * s, v1.Y * s, v1.Z * s);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D CreateUnitVector(double x, double y, double z)
        {
            var s = x * x + y * y + z * z;

            if (s.IsZero()) return UnitSymmetric;

            s = 1d / Math.Sqrt(s);

            return new Float64Vector3D(x * s, y * s, z * s);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Vector3D CreateSymmetricVector(double x)
        {
            var scalar = new Float64Scalar(x);

            return new Float64Vector3D(scalar, scalar, scalar);
        }


        public int VSpaceDimensions 
            => 3;

        public Float64Scalar X 
            => Scalar1;

        public Float64Scalar Y 
            => Scalar2;

        public Float64Scalar Z 
            => Scalar3;
        
        public double Item1
            => X.Value;

        public double Item2
            => Y.Value;

        public double Item3
            => Z.Value;

        public Float64Scalar Scalar
            => Float64Scalar.Zero;

        public Float64Scalar Scalar1 { get; }
        
        public Float64Scalar Scalar2 { get; }
        
        public Float64Scalar Scalar3 { get; }
        
        public Float64Scalar Scalar12
            => Float64Scalar.Zero;
        
        public Float64Scalar Scalar13
            => Float64Scalar.Zero;
        
        public Float64Scalar Scalar23
            => Float64Scalar.Zero;
        
        public Float64Scalar Scalar123
            => Float64Scalar.Zero;

        public int Count 
            => 8;

        /// <summary>
        /// Get or set the ith component of this multivector
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Float64Scalar this[int index]
        {
            get
            {
                if (index is < 0 or > 7)
                    throw new IndexOutOfRangeException();

                return index switch
                {
                    1 => Scalar1,
                    2 => Scalar2,
                    4 => Scalar3,
                    _ => Float64Scalar.Zero
                };
            }
        }

        public double this[LinUnitBasisVector3D axis]
        {
            get
            {
                return axis switch
                {
                    LinUnitBasisVector3D.PositiveX => X.Value,
                    LinUnitBasisVector3D.PositiveY => Y.Value,
                    LinUnitBasisVector3D.PositiveZ => Z.Value,
                    LinUnitBasisVector3D.NegativeX => -X.Value,
                    LinUnitBasisVector3D.NegativeY => -Y.Value,
                    LinUnitBasisVector3D.NegativeZ => -Z.Value,
                    _ => 0.0d
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Vector3D(Float64Scalar scalar1, Float64Scalar scalar2, Float64Scalar scalar3)
        {
            Scalar1 = scalar1;
            Scalar2 = scalar2;
            Scalar3 = scalar3;
        }

        //public static Float64Vector3D Create(Float64Scalar scalar1, Float64Scalar scalar2, Float64Scalar scalar3)
        //{
        //    return new Float64Vector3D(scalar1, scalar2, scalar3);
        //}

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D(ITriplet<double> v)
        {
            Scalar1 = v.Item1;
            Scalar2 = v.Item2;
            Scalar3 = v.Item3;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Scalar1.IsValid() &&
                   Scalar2.IsValid() &&
                   Scalar3.IsValid();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return X.IsZero() &&
                   Y.IsZero() &&
                   Z.IsZero();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsNearZero(double epsilon = 1E-12)
        {
            return Norm().IsNearZero(epsilon);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar Norm()
        {
            return NormSquared().Sqrt();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar NormSquared()
        {
            return X.Square() + Y.Square() + Z.Square();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Multivector3D ToMultivector3D()
        {
            return Float64Multivector3D.Create(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D Negative()
        {
            return new Float64Vector3D(-Scalar1,
                -Scalar2,
                -Scalar3);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D GradeInvolution()
        {
            return Negative();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D Reverse()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Vector3D Inverse()
        {
            var normSquared = NormSquared();

            return normSquared.IsZero() 
                ? throw new InvalidOperationException() 
                : this / normSquared.Value;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D Dual3D()
        {
            return Float64Bivector3D.Create(
                Scalar3,
                -Scalar2,
                Scalar1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D Dual3D(Float64Scalar scalingFactor)
        {
            return Float64Bivector3D.Create(
                Scalar3 * scalingFactor,
                -Scalar2 * scalingFactor,
                Scalar1 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D UnDual3D()
        {
            return Float64Bivector3D.Create(
                -Scalar3,
                Scalar2,
                -Scalar1
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector3D UnDual3D(Float64Scalar scalingFactor)
        {
            return Float64Bivector3D.Create(
                -Scalar3 * scalingFactor,
                Scalar2 * scalingFactor,
                -Scalar1 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IEnumerator<Float64Scalar> GetEnumerator()
        {
            yield return Scalar;
            yield return Scalar1;
            yield return Scalar2;
            yield return Scalar12;
            yield return Scalar3;
            yield return Scalar13;
            yield return Scalar23;
            yield return Scalar123;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            return $"({Scalar1})<1> + ({Scalar2})<2> + ({Scalar3})<3>";
        }

    }
}