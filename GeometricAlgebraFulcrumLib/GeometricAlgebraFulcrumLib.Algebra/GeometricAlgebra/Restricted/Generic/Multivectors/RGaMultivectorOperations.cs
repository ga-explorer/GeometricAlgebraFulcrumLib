using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

public abstract partial class RGaMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, int mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(int mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, uint mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(uint mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, long mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(long mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, ulong mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(ulong mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, float mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(float mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, double mv2)
    {
        var processor = mv1.Processor;

        return mv1.Add(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(double mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Add(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, T mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(T mv1, RGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(IScalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(Scalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator +(RGaMultivector<T> mv1, RGaMultivector<T> mv2)
    {
        return mv1.Add(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, int mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(int mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, uint mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(uint mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, long mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(long mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, ulong mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(ulong mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, float mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(float mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, double mv2)
    {
        var processor = mv1.Processor;

        return mv1.Subtract(
            mv1.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv2)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(double mv1, RGaMultivector<T> mv2)
    {
        var processor = mv2.Processor;

        return mv2.Subtract(
            mv2.Processor.Scalar(
                    
                processor.ScalarProcessor.ValueFromNumber(mv1)
            )
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, T mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(T mv1, RGaMultivector<T> mv2)
    {
        return mv2.Subtract(
            mv2.Processor.Scalar(mv1)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(IScalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Subtract(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(Scalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Subtract(
            mv2.Processor.Scalar(mv1.ScalarValue)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator -(RGaMultivector<T> mv1, RGaMultivector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(IntegerSign mv1, RGaMultivector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(int mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(uint mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(long mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(ulong mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(float mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(double mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(T mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(Scalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaMultivector<T> mv1, RGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator *(RGaScalar<T> mv1, RGaMultivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaMultivector<T> operator /(RGaMultivector<T> mv1, RGaScalar<T> mv2)
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
        
        
    public abstract RGaMultivector<T> Add(RGaMultivector<T> mv2);
        
    public abstract RGaMultivector<T> Subtract(RGaMultivector<T> mv2);
        
    public abstract RGaMultivector<T> Op(RGaMultivector<T> mv2);
        
    public abstract RGaMultivector<T> EGp(RGaMultivector<T> mv2);

    public abstract RGaMultivector<T> Gp(RGaMultivector<T> mv2);

    public abstract RGaMultivector<T> ELcp(RGaMultivector<T> mv2);

    public abstract RGaMultivector<T> Lcp(RGaMultivector<T> mv2);

    public abstract RGaMultivector<T> ERcp(RGaMultivector<T> mv2);

    public abstract RGaMultivector<T> Rcp(RGaMultivector<T> mv2);


    public abstract RGaScalar<T> ESp(RGaScalar<T> mv2);

    public abstract RGaScalar<T> ESp(RGaVector<T> mv2);

    public abstract RGaScalar<T> ESp(RGaBivector<T> mv2);

    public abstract RGaScalar<T> ESp(RGaHigherKVector<T> mv2);

    public abstract RGaScalar<T> ESp(RGaGradedMultivector<T> mv2);
        
    public abstract RGaScalar<T> ESp(RGaUniformMultivector<T> mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ESp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => ESp(mv),
            RGaVector<T> mv => ESp(mv),
            RGaBivector<T> mv => ESp(mv),
            RGaHigherKVector<T> mv => ESp(mv),
            RGaGradedMultivector<T> mv => ESp(mv),
            RGaUniformMultivector<T> mv => ESp(mv),
            _ => throw new InvalidOperationException()
        };
    }
        

    public abstract RGaScalar<T> Sp(RGaScalar<T> mv2);

    public abstract RGaScalar<T> Sp(RGaVector<T> mv2);

    public abstract RGaScalar<T> Sp(RGaBivector<T> mv2);

    public abstract RGaScalar<T> Sp(RGaHigherKVector<T> mv2);

    public abstract RGaScalar<T> Sp(RGaGradedMultivector<T> mv2);
        
    public abstract RGaScalar<T> Sp(RGaUniformMultivector<T> mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Sp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => Sp(mv),
            RGaVector<T> mv => Sp(mv),
            RGaBivector<T> mv => Sp(mv),
            RGaHigherKVector<T> mv => Sp(mv),
            RGaGradedMultivector<T> mv => Sp(mv),
            RGaUniformMultivector<T> mv => Sp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EFdp(RGaMultivector<T> mv2)
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
    public RGaMultivector<T> Fdp(RGaMultivector<T> mv2)
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
    public RGaMultivector<T> EHip(RGaMultivector<T> mv2)
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
    public RGaMultivector<T> Hip(RGaMultivector<T> mv2)
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
    public RGaMultivector<T> EAcp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) + mv2.EGp(this)).Divide(ScalarProcessor.TwoValue);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> ECp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) - mv2.EGp(this)).Divide(ScalarProcessor.TwoValue);

    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Acp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) + mv2.Gp(this)).Divide(ScalarProcessor.TwoValue);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Cp(RGaMultivector<T> mv2)
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
    public RGaKVector<T> Dp(RGaMultivector<T> mv2)
    {
        var gp = Gp(mv2);

        return gp.IsNearZero() 
            ? Processor.ScalarZero 
            : gp.GetKVectorParts()
                .OrderByDescending(kv => kv.Grade)
                .First(kv => !kv.IsNearZero());
    }
}