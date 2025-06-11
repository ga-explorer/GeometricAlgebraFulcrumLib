using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract partial class XGaKVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator -(XGaKVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(IntegerSign mv1, XGaKVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(int mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(uint mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(long mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(ulong mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(float mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(double mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(T mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(Scalar<T> mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaScalar<T> mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> AddSameGrade(XGaKVector<T> mv2)
    {
        Debug.Assert(Grade == mv2.Grade);

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Add((XGaScalar<T>) mv2),
            XGaVector<T> mv1 => mv1.Add((XGaVector<T>) mv2),
            XGaBivector<T> mv1 => mv1.Add((XGaBivector<T>) mv2),
            XGaHigherKVector<T> mv1 => mv1.AddSameGrade((XGaHigherKVector<T>) mv2),
            _ => throw new InvalidOperationException()
        };
    }

    //public abstract XGaKVector<T> Op(XGaKVector<T> mv2);

    //public abstract XGaKVector<T> ELcp(XGaKVector<T> mv2);
        
    //public abstract XGaKVector<T> Lcp(XGaKVector<T> mv2);

    //public abstract XGaKVector<T> ERcp(XGaKVector<T> mv2);

    //public abstract XGaKVector<T> Rcp(XGaKVector<T> mv2);
        
    //public abstract XGaScalar<T> ESp(XGaKVector<T> mv2);

    //public abstract XGaScalar<T> Sp(XGaKVector<T> mv2);

    
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Fdp(XGaKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    var grade = Math.Abs(Grade - mv2.Grade);

    //    return Processor
    //        .CreateComposer()
    //        .AddFdpTerms(this, mv2)
    //        .GetKVector(grade);
    //}
    
    
}