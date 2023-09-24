using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors
{
    public sealed partial class XGaFloat64Scalar
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator double(XGaFloat64Scalar mv)
        {
            return mv.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar mv)
        {
            return mv;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                -(s1.ScalarValue())
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, int s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(int s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, uint s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(uint s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, long s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(long s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, ulong s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(ulong s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, float s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(float s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, double s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() + s2
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator +(double s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 + s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, int s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(int s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, uint s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(uint s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, long s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(long s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, ulong s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(ulong s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, float s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(float s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, double s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() - s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator -(double s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 - s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, int s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(int s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, uint s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(uint s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, long s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(long s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, ulong s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(ulong s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, float s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(float s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, double s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() * s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator *(double s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 * s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, int s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(int s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, uint s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(uint s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, long s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(long s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, ulong s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(ulong s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, float s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(float s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, double s2)
        {
            return new XGaFloat64Scalar(
                s1.Processor,
                s1.ScalarValue() / s2
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static XGaFloat64Scalar operator /(double s1, XGaFloat64Scalar s2)
        {
            return new XGaFloat64Scalar(
                s2.Processor,
                s1 / s2.ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() == s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() != s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(int s1, XGaFloat64Scalar s2)
        {
            return s1 == s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(int s1, XGaFloat64Scalar s2)
        {
            return s1 != s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() == s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() != s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(double s1, XGaFloat64Scalar s2)
        {
            return s1 == s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(double s1, XGaFloat64Scalar s2)
        {
            return s1 != s2.ScalarValue();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return s1.ScalarValue() < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return s1.ScalarValue() > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() < s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() > s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(int s1, XGaFloat64Scalar s2)
        {
            return s1 < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(int s1, XGaFloat64Scalar s2)
        {
            return s1 > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() < s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() > s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <(double s1, XGaFloat64Scalar s2)
        {
            return s1 < s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >(double s1, XGaFloat64Scalar s2)
        {
            return s1 > s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return s1.ScalarValue() <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
        {
            return s1.ScalarValue() >= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() <= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(XGaFloat64Scalar s1, int s2)
        {
            return s1.ScalarValue() >= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(int s1, XGaFloat64Scalar s2)
        {
            return s1 <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(int s1, XGaFloat64Scalar s2)
        {
            return s1 >= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() <= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(XGaFloat64Scalar s1, double s2)
        {
            return s1.ScalarValue() >= s2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator <=(double s1, XGaFloat64Scalar s2)
        {
            return s1 <= s2.ScalarValue();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator >=(double s1, XGaFloat64Scalar s2)
        {
            return s1 >= s2.ScalarValue();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar MapScalar(Func<double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    scalarMapping(ScalarValue())
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar MapScalar(Func<IIndexSet, double, double> scalarMapping)
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    scalarMapping(
                        Metric.GetBasisScalarId(), 
                        ScalarValue()
                    )
                );
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Negative()
        {
            return IsZero
                ? this
                : new XGaFloat64Scalar(
                    Processor,
                    -(ScalarValue())
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Times(double scalarValue)
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
        public XGaFloat64Scalar Divide(double scalarValue)
        {
            if (IsZero || scalarValue.IsOne()) return this;

            if (scalarValue.IsZero())
                return Processor.CreateZeroScalar();

            return new XGaFloat64Scalar(
                Processor,
                ScalarValue() / scalarValue
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar DivideByENorm()
        {
            return Divide(ENorm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar DivideByENormSquared()
        {
            return Divide(ENormSquared().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar DivideByNorm()
        {
            return Divide(Norm().ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar DivideByNormSquared()
        {
            return Divide(NormSquared().ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Reverse()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar GradeInvolution()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar CliffordConjugate()
        {
            return this;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Conjugate()
        {
            return this;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ENormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(),
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar NormSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(),
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ENorm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    ScalarValue().Abs()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Norm()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalar(
                    ScalarValue().Abs()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(), 
                    ScalarValue()
                );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar SpSquared()
        {
            return IsZero
                ? Processor.CreateZeroScalar()
                : Processor.CreateScalarFromProduct(
                    ScalarValue(), 
                    ScalarValue()
                );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar EInverse()
        {
            return new XGaFloat64Scalar(
                Processor,
                1d / ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Inverse()
        {
            return new XGaFloat64Scalar(
                Processor,
                1d / ScalarValue()
            );
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar PseudoInverse()
        {
            return new XGaFloat64Scalar(
                Processor,
                1d / ScalarValue()
            );
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Add(XGaFloat64Scalar mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new XGaFloat64Scalar(
                Processor,
                ScalarValue() + mv2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
        {
            if (mv2 is XGaFloat64Scalar mv)
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
        public XGaFloat64Scalar Subtract(XGaFloat64Scalar mv2)
        {
            if (IsZero)
                return mv2;

            if (mv2.IsZero)
                return this;

            return new XGaFloat64Scalar(
                Processor,
                ScalarValue() - mv2.ScalarValue()
            );
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
        {
            if (mv2 is XGaFloat64Scalar mv)
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
        public XGaFloat64Scalar Op(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Op(XGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector Op(XGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector Op(XGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Op(XGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Op(XGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector Op(XGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar EGp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector EGp(XGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector EGp(XGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector EGp(XGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector EGp(XGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector EGp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector EGp(XGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector EGp(XGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Gp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Gp(XGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector Gp(XGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector Gp(XGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64KVector Gp(XGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Gp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector Gp(XGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ELcp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector ELcp(XGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector ELcp(XGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector ELcp(XGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ELcp(XGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector ELcp(XGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Lcp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Vector Lcp(XGaFloat64Vector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Bivector Lcp(XGaFloat64Bivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64HigherKVector Lcp(XGaFloat64HigherKVector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Lcp(XGaFloat64KVector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64UniformMultivector Lcp(XGaFloat64UniformMultivector mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
        {
            return mv2.Times(ScalarValue());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ERcp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ERcp(XGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ERcp(XGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ERcp(XGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector ERcp(XGaFloat64KVector mv2)
        {
            return mv2 is XGaFloat64Scalar mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar ERcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2)
        {
            return Times(mv2.Scalar());
        }
        

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Rcp(XGaFloat64Scalar mv2)
        {
            return mv2.Times(ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Rcp(XGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Rcp(XGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Rcp(XGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64KVector Rcp(XGaFloat64KVector mv2)
        {
            return mv2 is XGaFloat64Scalar mv
                ? mv.Times(ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public XGaFloat64Scalar Rcp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2)
        {
            return Times(mv2.Scalar());
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64KVector mv2)
        {
            return mv2 is XGaFloat64Scalar mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
        {
            return Float64ScalarComposer
                .Create()
                .AddESpTerms(this, mv2)
                .GetXGaFloat64Scalar(Processor);
        }

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64Scalar mv2)
        {
            return Times(mv2.ScalarValue());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64Vector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64Bivector mv2)
        {
            return Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector mv2)
        {
            return Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64KVector mv2)
        {
            return mv2 is XGaFloat64Scalar mv
                ? Times(mv.ScalarValue())
                : Processor.CreateZeroScalar();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
        {
            return mv2.TryGetKVector(0, out var mv)
                ? Times(((XGaFloat64Scalar) mv).ScalarValue())
                : Processor.CreateZeroScalar();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
        {
            return Float64ScalarComposer
                .Create()
                .AddSpTerms(this, mv2)
                .GetXGaFloat64Scalar(Processor);
        }

    }

}
