using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

public abstract partial class XGaFloat64KVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator -(XGaFloat64KVector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(IntegerSign mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative().GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(int mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            (mv1)
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(uint mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            (mv1)
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(long mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            (mv1)
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(ulong mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            (mv1)
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(float mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            (mv1)
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(double mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64Scalar mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        
        
    public abstract XGaFloat64KVector Op(XGaFloat64KVector mv2);

    public abstract XGaFloat64KVector ELcp(XGaFloat64KVector mv2);
        
    public abstract XGaFloat64KVector Lcp(XGaFloat64KVector mv2);

    public abstract XGaFloat64KVector ERcp(XGaFloat64KVector mv2);

    public abstract XGaFloat64KVector Rcp(XGaFloat64KVector mv2);
        
    public abstract XGaFloat64Scalar ESp(XGaFloat64KVector mv2);

    public abstract XGaFloat64Scalar Sp(XGaFloat64KVector mv2);

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Fdp(XGaFloat64KVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        var grade = Math.Abs(Grade - mv2.Grade);

        return Processor
            .CreateComposer()
            .AddFdpTerms(this, mv2)
            .GetKVector(grade);
    }

}