using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Scalar
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator double(XGaFloat64Scalar mv)
    {
        return mv.ScalarValue;
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
            -s1.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator +(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator -(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator *(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }
              
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, Float64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, IFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Scalar operator /(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue == s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue != s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(int s1, XGaFloat64Scalar s2)
    {
        return s1 == s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(int s1, XGaFloat64Scalar s2)
    {
        return s1 != s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue == s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue != s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(double s1, XGaFloat64Scalar s2)
    {
        return s1 == s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(double s1, XGaFloat64Scalar s2)
    {
        return s1 != s2.ScalarValue;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue < s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue > s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue < s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue > s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(int s1, XGaFloat64Scalar s2)
    {
        return s1 < s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(int s1, XGaFloat64Scalar s2)
    {
        return s1 > s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue < s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue > s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(double s1, XGaFloat64Scalar s2)
    {
        return s1 < s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(double s1, XGaFloat64Scalar s2)
    {
        return s1 > s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue <= s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue >= s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue <= s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue >= s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(int s1, XGaFloat64Scalar s2)
    {
        return s1 <= s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(int s1, XGaFloat64Scalar s2)
    {
        return s1 >= s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue <= s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue >= s2;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(double s1, XGaFloat64Scalar s2)
    {
        return s1 <= s2.ScalarValue;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(double s1, XGaFloat64Scalar s2)
    {
        return s1 >= s2.ScalarValue;
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
            ScalarValue + mv2.ScalarValue
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
            .CreateMultivectorComposer()
            .SetScalarTerm(ScalarValue)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Subtract(XGaFloat64Scalar mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return new XGaFloat64Scalar(
            Processor,
            ScalarValue - mv2.ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar mv)
            return Subtract(mv);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetScalarTerm(ScalarValue)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;
        
        if (scalarValue.IsZero())
            return Processor.ScalarZero;

        return new XGaFloat64Scalar(
            Processor,
            ScalarValue * scalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            throw new DivideByZeroException();

        return new XGaFloat64Scalar(
            Processor,
            ScalarValue / scalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Negative()
    {
        return IsZero
            ? this
            : new XGaFloat64Scalar(
                Processor,
                -ScalarValue
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar CliffordConjugate()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Conjugate()
    {
        return this;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar EInverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Inverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar PseudoInverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar ENormSquared()
    {
        return IsZero
            ? Float64Scalar.Zero
            : ScalarValue.Square();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar NormSquared()
    {
        return IsZero
            ? Float64Scalar.Zero
            : ScalarValue.Square();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar ENorm()
    {
        return IsZero
            ? Float64Scalar.Zero
            : ScalarValue.Abs();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override Float64Scalar Norm()
    {
        return IsZero
            ? Float64Scalar.Zero
            : ScalarValue.Abs();
    }

    
}