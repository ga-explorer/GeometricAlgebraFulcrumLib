using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaGradedMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator +(XGaGradedMultivector<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator -(XGaGradedMultivector<T> v1)
    {
        return v1.IsZero ? v1 : v1.MapKVectors(kv => kv.Negative());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator +(XGaGradedMultivector<T> v1, XGaGradedMultivector<T> v2)
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
    public static XGaGradedMultivector<T> operator -(XGaGradedMultivector<T> v1, XGaGradedMultivector<T> v2)
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
    public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;
            
        if (v1.IsZero || v2.IsZero())
            return processor.GradedMultivectorZero;

        if (v2.IsOne())
            return v1;

        var s2 = processor.ScalarProcessor.ScalarFromNumber(v2);

        return v1.MapKVectors(kv => kv.Times(s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, T v2)
    {
        var processor = v1.Processor;

        if (v1.IsZero || processor.ScalarProcessor.IsZero(v2))
            return processor.GradedMultivectorZero;

        if (processor.ScalarProcessor.IsOne(v2))
            return v1;

        return v1.MapKVectors(kv => kv.Times(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator *(double v1, XGaGradedMultivector<T> v2)
    {
        var processor = v2.Processor;

        if (v2.IsZero || v1.IsZero())
            return processor.GradedMultivectorZero;

        if (v1.IsOne()) return v2;

        var s1 = processor.ScalarProcessor.ScalarFromNumber(v1);

        return v2.MapKVectors(kv => kv.Times(s1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator *(T v1, XGaGradedMultivector<T> v2)
    {
        var processor = v2.Processor;

        if (v2.IsZero || processor.ScalarProcessor.IsZero(v1))
            return processor.GradedMultivectorZero;

        if (processor.ScalarProcessor.IsOne(v1)) return v2;

        return v2.MapKVectors(kv => kv.Times(v1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator *(XGaGradedMultivector<T> v1, XGaScalar<T> v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v2.IsOne) return v1;

        var processor = v1.Processor;

        if (v1.IsZero || v2.IsZero)
            return processor.GradedMultivectorZero;

        var s2 = v2.ScalarValue;

        return v1.MapKVectors(kv => kv.Times(s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator *(XGaScalar<T> v1, XGaGradedMultivector<T> v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v1.IsOne) return v2;

        var processor = v1.Processor;

        if (v1.IsZero || v2.IsZero)
            return processor.GradedMultivectorZero;

        var s1 = v1.ScalarValue;

        return v2.MapKVectors(kv => kv.Times(s1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator /(XGaGradedMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        if (v2.IsOne()) return v1;

        var s2 = processor.ScalarProcessor.ScalarFromNumber(1d / v2);

        return v1.MapKVectors(kv => kv.Times(s2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaGradedMultivector<T> operator /(XGaGradedMultivector<T> v1, T v2)
    {
        var processor = v1.Processor;

        if (processor.ScalarProcessor.IsOne(v2)) return v1;

        return v1.MapKVectors(kv => kv.Divide(v2));
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Negative()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Negative();

        return IsZero 
            ? Processor.ScalarZero 
            : MapKVectorsSimplify(kv => kv.Negative());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Times(int scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Times(double scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Times(T scalar)
    {
        if (IsZero || ScalarProcessor.IsOne(scalar))
            return this;

        return _gradeKVectorDictionary.Count == 1 
            ? _gradeKVectorDictionary.Values.First().Times(scalar) 
            : MapKVectorsSimplify(kv => kv.Times(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Times(Scalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Divide(int scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Divide(double scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Divide(T scalar)
    {
        if (IsZero || ScalarProcessor.IsOne(scalar))
            return this;

        return _gradeKVectorDictionary.Count == 1 
            ? _gradeKVectorDictionary.Values.First().Divide(scalar) 
            : MapKVectorsSimplify(kv => kv.Divide(scalar));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Divide(Scalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Reverse()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Reverse();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> GradeInvolution()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().GradeInvolution();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.GradeInvolution());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> CliffordConjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().CliffordConjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.CliffordConjugate());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Conjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Conjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectorsSimplify(kVector => kVector.Conjugate());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EDual(XGaKVector<T> blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Dual(XGaKVector<T> blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EUnDual(XGaKVector<T> blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> UnDual(XGaKVector<T> blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetMultivector();
    }

}