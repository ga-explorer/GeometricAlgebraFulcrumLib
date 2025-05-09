using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Generic.Multivectors;

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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> ESpSquared()
    {
        if (IsZero)
            return ScalarProcessor.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                ScalarProcessor.Times(
                    Processor.EGpSquaredSign(p.Key),
                    p.Value,
                    p.Value
                )
            );

        return ScalarProcessor.Add(scalarList);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Scalar<T> SpSquared()
    {
        if (IsZero)
            return ScalarProcessor.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                ScalarProcessor.Times(
                    Processor.GpSquaredSign(p.Key),
                    p.Value,
                    p.Value
                )
            );

        return ScalarProcessor.Add(scalarList);
    }
        
    
    public abstract XGaMultivector<T> Add(XGaMultivector<T> mv2);
        
    public abstract XGaMultivector<T> Subtract(XGaMultivector<T> mv2);
        
    public abstract XGaMultivector<T> Op(XGaMultivector<T> mv2);
        
    public abstract XGaMultivector<T> EGp(XGaMultivector<T> mv2);

    public abstract XGaMultivector<T> Gp(XGaMultivector<T> mv2);

    public abstract XGaMultivector<T> ELcp(XGaMultivector<T> mv2);

    public abstract XGaMultivector<T> Lcp(XGaMultivector<T> mv2);

    public abstract XGaMultivector<T> ERcp(XGaMultivector<T> mv2);

    public abstract XGaMultivector<T> Rcp(XGaMultivector<T> mv2);


    public abstract XGaScalar<T> ESp(XGaScalar<T> mv2);

    public abstract XGaScalar<T> ESp(XGaVector<T> mv2);

    public abstract XGaScalar<T> ESp(XGaBivector<T> mv2);

    public abstract XGaScalar<T> ESp(XGaHigherKVector<T> mv2);

    public abstract XGaScalar<T> ESp(XGaGradedMultivector<T> mv2);
        
    public abstract XGaScalar<T> ESp(XGaUniformMultivector<T> mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ESp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => ESp(mv),
            XGaVector<T> mv => ESp(mv),
            XGaBivector<T> mv => ESp(mv),
            XGaHigherKVector<T> mv => ESp(mv),
            XGaGradedMultivector<T> mv => ESp(mv),
            XGaUniformMultivector<T> mv => ESp(mv),
            _ => throw new InvalidOperationException()
        };
    }
        

    public abstract XGaScalar<T> Sp(XGaScalar<T> mv2);

    public abstract XGaScalar<T> Sp(XGaVector<T> mv2);

    public abstract XGaScalar<T> Sp(XGaBivector<T> mv2);

    public abstract XGaScalar<T> Sp(XGaHigherKVector<T> mv2);

    public abstract XGaScalar<T> Sp(XGaGradedMultivector<T> mv2);
        
    public abstract XGaScalar<T> Sp(XGaUniformMultivector<T> mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Sp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => Sp(mv),
            XGaVector<T> mv => Sp(mv),
            XGaBivector<T> mv => Sp(mv),
            XGaHigherKVector<T> mv => Sp(mv),
            XGaGradedMultivector<T> mv => Sp(mv),
            XGaUniformMultivector<T> mv => Sp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EFdp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEFdpTerms(this, mv2)
            .GetSimpleMultivector();
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Fdp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddFdpTerms(this, mv2)
            .GetSimpleMultivector();
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EHip(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEHipTerms(this, mv2)
            .GetSimpleMultivector();
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Hip(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddHipTerms(this, mv2)
            .GetSimpleMultivector();
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EAcp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) + mv2.EGp(this)).Divide(ScalarProcessor.TwoValue);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> ECp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) - mv2.EGp(this)).Divide(ScalarProcessor.TwoValue);

    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Acp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) + mv2.Gp(this)).Divide(ScalarProcessor.TwoValue);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Cp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) - mv2.Gp(this)).Divide(ScalarProcessor.TwoValue);
    }
        
    /// <summary>
    /// The Delta Product (See chapter 21 in Geometric Algebra for Computer Science)
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Dp(XGaMultivector<T> mv2)
    {
        var gp = Gp(mv2);

        return gp.IsNearZero() 
            ? Processor.ScalarZero 
            : gp.GetKVectorParts()
                .OrderByDescending(kv => kv.Grade)
                .First(kv => !kv.IsNearZero());
    }
}