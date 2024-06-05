using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Basic;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

public abstract partial class XGaFloat64Multivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(int mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(uint mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(long mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(ulong mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(float mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Add(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(double mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Add(
            mv2.Processor.Scalar(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator +(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return mv1.Add(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(int mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(uint mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(long mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(ulong mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(float mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Subtract(
            mv1.Processor.Scalar(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(double mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Processor.Scalar(mv1).Subtract(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator -(XGaFloat64Multivector mv1, XGaFloat64Multivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(IntegerSign mv1, XGaFloat64Multivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(int mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(uint mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(long mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(ulong mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(float mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(double mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator *(XGaFloat64Scalar mv1, XGaFloat64Multivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, int mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, uint mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, long mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, ulong mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, float mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Multivector operator /(XGaFloat64Multivector mv1, XGaFloat64Scalar mv2)
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
                Metric.Signature(p.Key) * p.Value * p.Value
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
                Metric.EGpSquaredSign(p.Key) * p.Value * p.Value
            );

        return scalarList.Sum();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual Float64Scalar SpSquared()
    {
        if (IsZero)
            return Float64Scalar.Zero;

        var scalarList =
            IdScalarPairs.Select(p => 
                Metric.GpSquaredSign(p.Key) * p.Value * p.Value
            );

        return scalarList.Sum();
    }
        
        
    public abstract XGaFloat64Multivector Add(XGaFloat64Multivector mv2);
        
    public abstract XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2);
        
    public abstract XGaFloat64Multivector Op(XGaFloat64Multivector mv2);
        
    public abstract XGaFloat64Multivector EGp(XGaFloat64Multivector mv2);

    public abstract XGaFloat64Multivector Gp(XGaFloat64Multivector mv2);

    public abstract XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2);

    public abstract XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2);

    public abstract XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2);

    public abstract XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2);


    public abstract XGaFloat64Scalar ESp(XGaFloat64Scalar mv2);

    public abstract XGaFloat64Scalar ESp(XGaFloat64Vector mv2);

    public abstract XGaFloat64Scalar ESp(XGaFloat64Bivector mv2);

    public abstract XGaFloat64Scalar ESp(XGaFloat64HigherKVector mv2);

    public abstract XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2);
        
    public abstract XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ESp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => ESp(mv),
            XGaFloat64Vector mv => ESp(mv),
            XGaFloat64Bivector mv => ESp(mv),
            XGaFloat64HigherKVector mv => ESp(mv),
            XGaFloat64GradedMultivector mv => ESp(mv),
            XGaFloat64UniformMultivector mv => ESp(mv),
            _ => throw new InvalidOperationException()
        };
    }
        

    public abstract XGaFloat64Scalar Sp(XGaFloat64Scalar mv2);

    public abstract XGaFloat64Scalar Sp(XGaFloat64Vector mv2);

    public abstract XGaFloat64Scalar Sp(XGaFloat64Bivector mv2);

    public abstract XGaFloat64Scalar Sp(XGaFloat64HigherKVector mv2);

    public abstract XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2);
        
    public abstract XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2);
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Sp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => Sp(mv),
            XGaFloat64Vector mv => Sp(mv),
            XGaFloat64Bivector mv => Sp(mv),
            XGaFloat64HigherKVector mv => Sp(mv),
            XGaFloat64GradedMultivector mv => Sp(mv),
            XGaFloat64UniformMultivector mv => Sp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EFdp(XGaFloat64Multivector mv2)
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
    public XGaFloat64Multivector Fdp(XGaFloat64Multivector mv2)
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
    public XGaFloat64Multivector EHip(XGaFloat64Multivector mv2)
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
    public XGaFloat64Multivector Hip(XGaFloat64Multivector mv2)
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
    public XGaFloat64Multivector EAcp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) + mv2.EGp(this)).Divide(2d);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ECp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (EGp(mv2) - mv2.EGp(this)).Divide(2d);

    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Acp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) + mv2.Gp(this)).Divide(2d);
    }

    //TODO: This is not efficient
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Cp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return (Gp(mv2) - mv2.Gp(this)).Divide(2d);
    }
        
    /// <summary>
    /// The Delta Product (See chapter 21 in Geometric Algebra for Computer Science)
    /// </summary>
    /// <param name="mv2"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Dp(XGaFloat64Multivector mv2, double epsilon = 1e-12)
    {
        var gp = Gp(mv2);

        return gp.IsNearZero(epsilon) 
            ? Processor.ScalarZero 
            : gp.GetKVectorParts()
                .OrderByDescending(kv => kv.Grade)
                .First(kv => !kv.IsNearZero(epsilon));
    }
}