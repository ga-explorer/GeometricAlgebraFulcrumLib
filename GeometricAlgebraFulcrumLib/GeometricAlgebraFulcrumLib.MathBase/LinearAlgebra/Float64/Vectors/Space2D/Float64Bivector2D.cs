using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra.Float64.Vectors.Space2D
{
    public sealed record Float64Bivector2D : 
        IFloat64Multivector2D
    {
        public static Float64Bivector2D Zero { get; }
            = new Float64Bivector2D(Float64Scalar.Zero);

        public static Float64Bivector2D E12 { get; }
            = new Float64Bivector2D(Float64Scalar.One);
        
        public static Float64Bivector2D E21 { get; }
            = new Float64Bivector2D(Float64Scalar.NegativeOne);
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D Create(Float64Scalar scalar12)
        {
            return new Float64Bivector2D(scalar12);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator +(Float64Bivector2D mv1)
        {
            return mv1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator -(Float64Bivector2D mv1)
        {
            return new Float64Bivector2D(-mv1.Scalar12);
        }
        
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator +(Float64Bivector2D mv1, Float64Bivector2D mv2)
        {
            return new Float64Bivector2D(mv1.Scalar12 + mv2.Scalar12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator -(Float64Bivector2D mv1, Float64Bivector2D mv2)
        {
            return new Float64Bivector2D(mv1.Scalar12 - mv2.Scalar12);
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator *(Float64Bivector2D mv1, double mv2)
        {
            return new Float64Bivector2D(mv1.Scalar12 * mv2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator *(double mv1, Float64Bivector2D mv2)
        {
            return new Float64Bivector2D(mv1 * mv2.Scalar12);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Float64Bivector2D operator /(Float64Bivector2D mv1, double mv2)
        {
            mv2 = 1d / mv2;

            return new Float64Bivector2D(mv1.Scalar12 * mv2);
        }

        
        public int VSpaceDimensions 
            => 2;

        public double Item1 
            => Scalar12;
        
        public Float64Scalar Xy 
            => Scalar12;

        public Float64Scalar Yx 
            => -Scalar12;
        
        public Float64Scalar Scalar
            => Float64Scalar.Zero;

        public Float64Scalar Scalar1
            => Float64Scalar.Zero;
        
        public Float64Scalar Scalar2
            => Float64Scalar.Zero;
        
        public Float64Scalar Scalar12 { get; }
        
        public int Count 
            => 4;

        /// <summary>
        /// Get or set the ith component of this multivector
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
                    3 => Scalar12,
                    _ => Float64Scalar.Zero
                };
            }
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Float64Bivector2D(Float64Scalar scalar12)
        {
            Scalar12 = scalar12;
            
            Debug.Assert(IsValid());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsValid()
        {
            return Scalar12.IsValid();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsZero()
        {
            return Scalar12.IsZero();
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
            return Scalar12.Square();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D ToUnitBivector(bool zeroAsSymmetric = true)
        {
            var normSquared = NormSquared();

            if (normSquared.IsZero())
                return zeroAsSymmetric ? E12 : Zero;
            
            return E12;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Multivector2D ToMultivector2D()
        {
            return Float64Multivector2D.Create(this);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D Negative()
        {
            return new Float64Bivector2D(-Scalar12);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D Negative(double scalingFactor)
        {
            return new Float64Bivector2D(
                -Scalar12 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D GradeInvolution()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D Reverse()
        {
            return Negative();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D CliffordConjugate()
        {
            return Negative();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Bivector2D Inverse()
        {
            return Negative(1d / NormSquared().Value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar2D Dual2D()
        {
            return Float64Scalar2D.Create(Scalar12);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar2D Dual2D(double scalingFactor)
        {
            return Float64Scalar2D.Create(
                Scalar12 * scalingFactor
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar2D UnDual2D()
        {
            return Float64Scalar2D.Create(-Scalar12);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Float64Scalar2D UnDual2D(double scalingFactor)
        {
            return Float64Scalar2D.Create(
                -Scalar12 * scalingFactor
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
            return $"({Scalar12})<1,2>";
        }

    }
}