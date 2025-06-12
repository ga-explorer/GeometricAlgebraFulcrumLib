using System;
using System.Diagnostics;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Scalar
{
    
    public static implicit operator double(XGaFloat64Scalar mv)
    {
        return mv.ScalarValue;
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar mv)
    {
        return mv;
    }

    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            -s1.ScalarValue
        );
    }


    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    
    public static XGaFloat64Scalar operator +(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    
    public static XGaFloat64Scalar operator +(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    
    public static XGaFloat64Scalar operator +(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    
    public static XGaFloat64Scalar operator +(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }

    
    public static XGaFloat64Scalar operator +(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator +(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue + s2
        );
    }
        
    
    public static XGaFloat64Scalar operator +(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 + s2.ScalarValue
        );
    }


    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator -(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue - s2
        );
    }

    
    public static XGaFloat64Scalar operator -(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 - s2.ScalarValue
        );
    }


    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator *(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue * s2
        );
    }

    
    public static XGaFloat64Scalar operator *(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 * s2.ScalarValue
        );
    }


    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2.ScalarValue
        );
    }
        
    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, int s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(int s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, uint s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(uint s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, long s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(long s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, ulong s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(ulong s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, float s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(float s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }
    
    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, IFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2.ScalarValue
        );
    }

    
    public static XGaFloat64Scalar operator /(XGaFloat64Scalar s1, double s2)
    {
        return new XGaFloat64Scalar(
            s1.Processor,
            s1.ScalarValue / s2
        );
    }

    
    public static XGaFloat64Scalar operator /(double s1, XGaFloat64Scalar s2)
    {
        return new XGaFloat64Scalar(
            s2.Processor,
            s1 / s2.ScalarValue
        );
    }


    
    public static bool operator ==(XGaFloat64Scalar s1, int s2)
    {
        Debug.Assert(s1 != null, nameof(s1) + " != null");

        return s1.ScalarValue == s2;
    }

    
    public static bool operator !=(XGaFloat64Scalar s1, int s2)
    {
        Debug.Assert(s1 != null, nameof(s1) + " != null");

        return s1.ScalarValue != s2;
    }

    
    public static bool operator ==(int s1, XGaFloat64Scalar s2)
    {
        Debug.Assert(s2 != null, nameof(s2) + " != null");

        return s1 == s2.ScalarValue;
    }

    
    public static bool operator !=(int s1, XGaFloat64Scalar s2)
    {
        Debug.Assert(s2 != null, nameof(s2) + " != null");

        return s1 != s2.ScalarValue;
    }

    
    public static bool operator ==(XGaFloat64Scalar s1, double s2)
    {
        Debug.Assert(s1 != null, nameof(s1) + " != null");

        return s1.ScalarValue == s2;
    }

    
    public static bool operator !=(XGaFloat64Scalar s1, double s2)
    {
        Debug.Assert(s1 != null, nameof(s1) + " != null");

        return s1.ScalarValue != s2;
    }

    
    public static bool operator ==(double s1, XGaFloat64Scalar s2)
    {
        Debug.Assert(s2 != null, nameof(s2) + " != null");

        return s1 == s2.ScalarValue;
    }

    
    public static bool operator !=(double s1, XGaFloat64Scalar s2)
    {
        Debug.Assert(s2 is not null, nameof(s2) + " != null");

        return s1 != s2.ScalarValue;
    }
        
    
    public static bool operator <(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue < s2.ScalarValue;
    }

    
    public static bool operator >(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue > s2.ScalarValue;
    }

    
    public static bool operator <(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue < s2;
    }

    
    public static bool operator >(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue > s2;
    }

    
    public static bool operator <(int s1, XGaFloat64Scalar s2)
    {
        return s1 < s2.ScalarValue;
    }

    
    public static bool operator >(int s1, XGaFloat64Scalar s2)
    {
        return s1 > s2.ScalarValue;
    }

    
    public static bool operator <(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue < s2;
    }

    
    public static bool operator >(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue > s2;
    }

    
    public static bool operator <(double s1, XGaFloat64Scalar s2)
    {
        return s1 < s2.ScalarValue;
    }

    
    public static bool operator >(double s1, XGaFloat64Scalar s2)
    {
        return s1 > s2.ScalarValue;
    }

    
    public static bool operator <=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue <= s2.ScalarValue;
    }

    
    public static bool operator >=(XGaFloat64Scalar s1, XGaFloat64Scalar s2)
    {
        return s1.ScalarValue >= s2.ScalarValue;
    }

    
    public static bool operator <=(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue <= s2;
    }

    
    public static bool operator >=(XGaFloat64Scalar s1, int s2)
    {
        return s1.ScalarValue >= s2;
    }

    
    public static bool operator <=(int s1, XGaFloat64Scalar s2)
    {
        return s1 <= s2.ScalarValue;
    }

    
    public static bool operator >=(int s1, XGaFloat64Scalar s2)
    {
        return s1 >= s2.ScalarValue;
    }

    
    public static bool operator <=(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue <= s2;
    }

    
    public static bool operator >=(XGaFloat64Scalar s1, double s2)
    {
        return s1.ScalarValue >= s2;
    }

    
    public static bool operator <=(double s1, XGaFloat64Scalar s2)
    {
        return s1 <= s2.ScalarValue;
    }

    
    public static bool operator >=(double s1, XGaFloat64Scalar s2)
    {
        return s1 >= s2.ScalarValue;
    }

   
    
    
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
            .GetMultivector();
    }

        
    
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
            .GetMultivector();
    }


    
    public new XGaFloat64Scalar Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;
        
        if (scalarValue.IsZero())
            return Processor.ScalarZero;

        return new XGaFloat64Scalar(
            Processor,
            ScalarValue * scalarValue
        );
    }
        
    
    public new XGaFloat64Scalar Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            throw new DivideByZeroException();

        return new XGaFloat64Scalar(
            Processor,
            ScalarValue / scalarValue
        );
    }
        
    
    public new XGaFloat64Scalar DivideByENorm()
    {
        return Divide(ENorm());
    }
        
    
    public new XGaFloat64Scalar DivideByENormSquared()
    {
        return Divide(ENormSquared());
    }
        
    
    public new XGaFloat64Scalar DivideByNorm()
    {
        return Divide(Norm());
    }
        
    
    public new XGaFloat64Scalar DivideByNormSquared()
    {
        return Divide(NormSquared());
    }

    
    
    public new XGaFloat64Scalar Negative()
    {
        return IsZero
            ? this
            : new XGaFloat64Scalar(
                Processor,
                -ScalarValue
            );
    }

    
    public new XGaFloat64Scalar Reverse()
    {
        return this;
    }

    
    public new XGaFloat64Scalar GradeInvolution()
    {
        return this;
    }

    
    public new XGaFloat64Scalar CliffordConjugate()
    {
        return this;
    }

    
    public new XGaFloat64Scalar Conjugate()
    {
        return this;
    }
        
    
    public new XGaFloat64Scalar EInverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    
    public new XGaFloat64Scalar Inverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    
    public new XGaFloat64Scalar PseudoInverse()
    {
        return new XGaFloat64Scalar(
            Processor,
            1d / ScalarValue
        );
    }

    
    
    public override double ENormSquared()
    {
        return IsZero
            ? 0d
            : ScalarValue.Square();
    }

    
    public override double NormSquared()
    {
        return IsZero
            ? 0d
            : ScalarValue.Square();
    }
        
    
    public override double ENorm()
    {
        return IsZero
            ? 0d
            : ScalarValue.Abs();
    }

    
    public override double Norm()
    {
        return IsZero
            ? 0d
            : ScalarValue.Abs();
    }

    
}