using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public abstract partial class RGaFloat64Multivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(int mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(uint mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(long mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(ulong mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(float mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(double mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator +(RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
    {
        return mv1.Add(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(int mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(uint mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(long mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(ulong mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(float mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(double mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator -(RGaFloat64Multivector mv1, RGaFloat64Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(IntegerSign mv1, RGaFloat64Multivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(int mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(uint mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(long mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(ulong mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(float mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(double mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Multivector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator *(RGaFloat64Scalar mv1, RGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Multivector operator /(RGaFloat64Multivector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar ENormSquared()
    {
        if (IsZero)
            return Float64Scalar.Zero;

        var scalarList =
            Scalars.Select(s => s * s);

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar NormSquared()
    {
        if (IsZero)
            return Float64Scalar.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                Processor.Signature(p.Key) * p.Value * p.Value
            );

        return scalarList.Sum();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar ENorm()
    {
        return ENormSquared().Sqrt();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar Norm()
    {
        return NormSquared().SqrtOfAbs();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar ESpSquared()
    {
        if (IsZero)
            return Float64Scalar.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                Processor.EGpSquaredSign(p.Key) * p.Value * p.Value
            );

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar SpSquared()
    {
        if (IsZero)
            return Float64Scalar.Zero;;

        var scalarList =
            IdScalarPairs.Select(p => 
                Processor.GpSquaredSign(p.Key) * p.Value * p.Value
            );

        return scalarList.Sum();
    }
        
        
    public abstract RGaFloat64Multivector Add(RGaFloat64Multivector mv2);
        
    public abstract RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2);
        
    public abstract RGaFloat64Multivector Op(RGaFloat64Multivector mv2);
        
    public abstract RGaFloat64Multivector EGp(RGaFloat64Multivector mv2);

    public abstract RGaFloat64Multivector Gp(RGaFloat64Multivector mv2);

    public abstract RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2);

    public abstract RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2);

    public abstract RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2);

    public abstract RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2);


    public abstract RGaFloat64Scalar ESp(RGaFloat64Scalar mv2);

    public abstract RGaFloat64Scalar ESp(RGaFloat64Vector mv2);

    public abstract RGaFloat64Scalar ESp(RGaFloat64Bivector mv2);

    public abstract RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2);

    public abstract RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2);
        
    public abstract RGaFloat64Scalar ESp(RGaFloat64UniformMultivector mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ESp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => ESp(mv),
            RGaFloat64Vector mv => ESp(mv),
            RGaFloat64Bivector mv => ESp(mv),
            RGaFloat64HigherKVector mv => ESp(mv),
            RGaFloat64GradedMultivector mv => ESp(mv),
            RGaFloat64UniformMultivector mv => ESp(mv),
            _ => throw new InvalidOperationException()
        };
    }
        

    public abstract RGaFloat64Scalar Sp(RGaFloat64Scalar mv2);

    public abstract RGaFloat64Scalar Sp(RGaFloat64Vector mv2);

    public abstract RGaFloat64Scalar Sp(RGaFloat64Bivector mv2);

    public abstract RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2);

    public abstract RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2);
        
    public abstract RGaFloat64Scalar Sp(RGaFloat64UniformMultivector mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Sp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => Sp(mv),
            RGaFloat64Vector mv => Sp(mv),
            RGaFloat64Bivector mv => Sp(mv),
            RGaFloat64HigherKVector mv => Sp(mv),
            RGaFloat64GradedMultivector mv => Sp(mv),
            RGaFloat64UniformMultivector mv => Sp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EFdp(RGaFloat64Multivector mv2)
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
    public RGaFloat64Multivector Fdp(RGaFloat64Multivector mv2)
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
    public RGaFloat64Multivector EHip(RGaFloat64Multivector mv2)
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
    public RGaFloat64Multivector Hip(RGaFloat64Multivector mv2)
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
    public RGaFloat64Multivector EAcp(RGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) + mv2.EGp(this)).Divide(2d);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector ECp(RGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Acp(RGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) + mv2.Gp(this)).Divide(2d);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Cp(RGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) - mv2.Gp(this)).Divide(2d);
    }

    /// <summary>
    /// The Delta Product (See chapter 21 in Geometric Algebra for Computer Science)
    /// </summary>
    /// <param name="mv2"></param>
    /// <param name="epsilon"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Dp(RGaFloat64Multivector mv2, double epsilon = 1e-12)
    {
        var gp = Gp(mv2);

        return gp.IsNearZero(epsilon) 
            ? Processor.ScalarZero 
            : gp.GetKVectorParts()
                .OrderByDescending(kv => kv.Grade)
                .First(kv => !kv.IsNearZero(epsilon));
    }
}