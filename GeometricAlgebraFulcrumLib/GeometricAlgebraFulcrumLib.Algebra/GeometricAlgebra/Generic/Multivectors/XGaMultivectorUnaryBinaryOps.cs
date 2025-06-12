using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract partial class XGaMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, int mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(int mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(uint mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, long mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(long mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(ulong mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, float mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(float mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, double mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(double mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, T mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(T mv1, XGaMultivector<T> mv2)
    {
        Debug.Assert(mv1 is not null);

        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(Scalar<T> mv1, XGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator +(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
    {
        return mv1.Add(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, int mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(int mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(uint mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, long mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(long mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(ulong mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, float mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(float mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, double mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(double mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, T mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(T mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(Scalar<T> mv1, XGaMultivector<T> mv2)
    {
        return mv2.Processor.Scalar(mv1.ScalarValue).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator -(XGaMultivector<T> mv1, XGaMultivector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(IntegerSign mv1, XGaMultivector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(int mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(uint mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(long mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(ulong mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(float mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(double mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(T mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(Scalar<T> mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaMultivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator *(XGaScalar<T> mv1, XGaMultivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaMultivector<T> operator /(XGaMultivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> ENormSquared()
    {
        if (IsZero)
            return ScalarProcessor.Zero;

        var scalarList =
            Scalars.Select(s => ScalarProcessor.Times(s, s));

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> NormSquared()
    {
        if (IsZero)
            return ScalarProcessor.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                ScalarProcessor.Times(
                    Processor.Signature(p.Key),
                    p.Value,
                    p.Value
                )
            );

        return ScalarProcessor.Add(scalarList);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> Norm()
    {
        return NormSquared().SqrtOfAbs();
    }
        
    
    public abstract XGaMultivector<T> Add(XGaMultivector<T> mv2);
        
    public abstract XGaMultivector<T> Subtract(XGaMultivector<T> mv2);

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Negative();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Reverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> GradeInvolution();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> CliffordConjugate();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Conjugate();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(T scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(int scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(double scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(Scalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Times(IScalar<T> scalarValue);


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(int scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(double scalarValue);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(T scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(Scalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Divide(IScalar<T> scalarValue);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByENorm();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByENormSquared();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByNorm();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> DivideByNormSquared();


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Inverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> PseudoInverse();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EDual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Dual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> Dual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EUnDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> EUnDual(XGaKVector<T> blade);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> UnDual(int vSpaceDimensions);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public abstract XGaMultivector<T> UnDual(XGaKVector<T> blade);

}