using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors
{
    public sealed partial class RGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(RGaFloat64Scalar mv)
        {
            return mv.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                -(s1.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, int s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(int s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, uint s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(uint s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, long s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(long s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, ulong s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(ulong s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, float s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(float s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(RGaFloat64Scalar s1, double s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator +(double s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, int s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(int s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, uint s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(uint s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, long s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(long s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, ulong s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(ulong s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, float s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(float s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(RGaFloat64Scalar s1, double s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator -(double s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, int s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(int s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, uint s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(uint s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, long s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(long s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, ulong s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(ulong s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, float s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(float s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(RGaFloat64Scalar s1, double s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator *(double s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, int s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(int s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, uint s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(uint s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, long s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(long s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, ulong s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(ulong s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, float s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(float s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(RGaFloat64Scalar s1, double s2)
        {
            return new RGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static RGaFloat64Scalar operator /(double s1, RGaFloat64Scalar s2)
        {
            return new RGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() == s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() != s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int s1, RGaFloat64Scalar s2)
        {
            return s1 == s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int s1, RGaFloat64Scalar s2)
        {
            return s1 != s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() == s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() != s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double s1, RGaFloat64Scalar s2)
        {
            return s1 == s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double s1, RGaFloat64Scalar s2)
        {
            return s1 != s2.ScalarValue();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return s1.ScalarValue() < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return s1.ScalarValue() > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() < s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() > s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(int s1, RGaFloat64Scalar s2)
        {
            return s1 < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(int s1, RGaFloat64Scalar s2)
        {
            return s1 > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() < s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() > s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(double s1, RGaFloat64Scalar s2)
        {
            return s1 < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(double s1, RGaFloat64Scalar s2)
        {
            return s1 > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return s1.ScalarValue() <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaFloat64Scalar s1, RGaFloat64Scalar s2)
        {
            return s1.ScalarValue() >= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() <= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() >= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(int s1, RGaFloat64Scalar s2)
        {
            return s1 <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(int s1, RGaFloat64Scalar s2)
        {
            return s1 >= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() <= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(RGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() >= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(double s1, RGaFloat64Scalar s2)
        {
            return s1 <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(double s1, RGaFloat64Scalar s2)
        {
            return s1 >= s2.ScalarValue();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar MapScalar(Func<double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new RGaFloat64Scalar(
                    Processor, 
                    scalarMapping(ScalarValue())
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar MapScalar(Func<ulong, double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new RGaFloat64Scalar(
                    Processor, 
                    scalarMapping(
                        Processor.GetBasisScalarId(), 
                        ScalarValue()
                    )
                );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Negative()
        {
            return IsZero
                ? this
                : new RGaFloat64Scalar(
                    Processor, 
                    -(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Times(double scalarValue)
        {
            if (IsZero || scalarValue.IsOne()) return this;

            return scalarValue.IsZero()
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(),
                    scalarValue
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Divide(double scalarValue)
        {
            if (IsZero || scalarValue.IsOne()) return this;

            if (scalarValue.IsZero())
                return Processor.CreateZeroScalar();

            return new RGaFloat64Scalar(
                Processor, 
                ScalarValue() / scalarValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Conjugate()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ENormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(),
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar NormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(),
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ENorm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    ScalarValue().Abs()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Norm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    ScalarValue().Abs()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(), 
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar SpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(), 
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar EInverse()
        {
            return new RGaFloat64Scalar(
                Processor, 
                1d / ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Inverse()
        {
            return new RGaFloat64Scalar(
                Processor, 
                1d / ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar PseudoInverse()
        {
            return new RGaFloat64Scalar(
                Processor, 
                1d / ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Add(RGaFloat64Scalar mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new RGaFloat64Scalar(
                Processor, 
                ScalarValue() + mv2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
        {
            if (mv2 is RGaFloat64Scalar mv)
                return Add(mv);

            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetScalarTerm(ScalarValue())
                .AddMultivector(mv2)
                .GetSimpleMultivector();
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Subtract(RGaFloat64Scalar mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new RGaFloat64Scalar(
                Processor, 
                ScalarValue() - mv2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
        {
            if (mv2 is RGaFloat64Scalar mv)
                return Subtract(mv);

            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return Processor
                .CreateComposer()
                .SetScalarTerm(ScalarValue())
                .SubtractMultivector(mv2)
                .GetSimpleMultivector();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Op(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector Op(RGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector Op(RGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector Op(RGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector Op(RGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Op(RGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Op(RGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Op(RGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar EGp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector EGp(RGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector EGp(RGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector EGp(RGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector EGp(RGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector EGp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector EGp(RGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector EGp(RGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Gp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector Gp(RGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector Gp(RGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector Gp(RGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64KVector Gp(RGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Gp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Gp(RGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ELcp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector ELcp(RGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector ELcp(RGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector ELcp(RGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector ELcp(RGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector ELcp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector ELcp(RGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Lcp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Vector Lcp(RGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Bivector Lcp(RGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64HigherKVector Lcp(RGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector Lcp(RGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Multivector Lcp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64UniformMultivector Lcp(RGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ERcp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ERcp(RGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ERcp(RGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ERcp(RGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector ERcp(RGaFloat64KVector mv2)
        {
            return mv2 is RGaFloat64Scalar mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar ERcp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2)
        {
            return Times(mv2.Scalar());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Rcp(RGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Rcp(RGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Rcp(RGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Rcp(RGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64KVector Rcp(RGaFloat64KVector mv2)
        {
            return mv2 is RGaFloat64Scalar mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public RGaFloat64Scalar Rcp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2)
        {
            return Times(mv2.Scalar());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64KVector mv2)
        {
            return mv2 is RGaFloat64Scalar mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar ESp(RGaFloat64UniformMultivector mv2)
        {
            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64KVector mv2)
        {
            return mv2 is RGaFloat64Scalar mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((RGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override RGaFloat64Scalar Sp(RGaFloat64UniformMultivector mv2)
        {
            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetRGaScalar(Processor);
        }

    }

}
