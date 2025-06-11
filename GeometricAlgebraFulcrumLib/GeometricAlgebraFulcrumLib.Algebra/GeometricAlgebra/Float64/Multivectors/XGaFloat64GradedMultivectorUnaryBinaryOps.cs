using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64GradedMultivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator +(XGaFloat64GradedMultivector v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator -(XGaFloat64GradedMultivector v1)
    {
        return v1.IsZero ? v1 : v1.MapKVectors(kv => kv.Negative());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator +(XGaFloat64GradedMultivector v1, XGaFloat64GradedMultivector v2)
    {
        if (v1.IsZero)
            return v2;

        if (v2.IsZero)
            return v1;

        return v1.Processor
            .CreateMultivectorComposer()
            .SetMultivector(v1)
            .AddMultivector(v2)
            .GetGradedMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator -(XGaFloat64GradedMultivector v1, XGaFloat64GradedMultivector v2)
    {
        if (v1.IsZero)
            return v2;

        if (v2.IsZero)
            return v1;

        return v1.Processor
            .CreateMultivectorComposer()
            .SetMultivector(v1)
            .SubtractMultivector(v2)
            .GetGradedMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator *(XGaFloat64GradedMultivector v1, double v2)
    {
        var metric = v1.Processor;
            
        if (v1.IsZero || v2.IsZero())
            return metric.GradedMultivectorZero;

        return v2.IsOne() ? v1 : v1.MapKVectors(kv => kv.Times(v2));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator *(double v1, XGaFloat64GradedMultivector v2)
    {
        var metric = v2.Processor;

        if (v2.IsZero || v1.IsZero())
            return metric.GradedMultivectorZero;

        return v1.IsOne() ? v2 : v2.MapKVectors(kv => kv.Times(v1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator *(XGaFloat64GradedMultivector v1, XGaFloat64Scalar v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v2.IsOne) return v1;

        return v1.IsZero || v2.IsZero 
            ? v1.Processor.GradedMultivectorZero 
            : v1.MapKVectors(kv => kv.Times(v2.ScalarValue));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator *(XGaFloat64Scalar v1, XGaFloat64GradedMultivector v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v1.IsOne) return v2;

        return v1.IsZero || v2.IsZero 
            ? v1.Processor.GradedMultivectorZero 
            : v2.MapKVectors(kv => kv.Times(v1.ScalarValue));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64GradedMultivector operator /(XGaFloat64GradedMultivector v1, double v2)
    {
        return v2.IsOne() ? v1 : v1.MapKVectors(kv => kv.Divide(v2));
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Times(double scalar)
    {
        if (IsZero || scalar.IsOne())
            return this;

        return _gradeKVectorDictionary.Count == 1 
            ? _gradeKVectorDictionary.Values.First().Times(scalar) 
            : MapKVectorsSimplify(kv => kv.Times(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Divide(double scalar)
    {
        if (IsZero || scalar.IsOne())
            return this;

        return _gradeKVectorDictionary.Count == 1 
            ? _gradeKVectorDictionary.Values.First().Divide(scalar) 
            : MapKVectorsSimplify(kv => kv.Divide(scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Negative()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Negative();

        return IsZero 
            ? Processor.ScalarZero 
            : MapKVectorsSimplify(kv => kv.Negative());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Reverse()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Reverse();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector GradeInvolution()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().GradeInvolution();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.GradeInvolution());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector CliffordConjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().CliffordConjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.CliffordConjugate());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Conjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Conjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.Conjugate());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Dual(XGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EUnDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector UnDual(XGaFloat64KVector blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }


}