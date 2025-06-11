using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
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
    public override XGaMultivector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(scalarMapping);

        return IsZero 
            ? this 
            : MapKVectorsSimplify(kv => kv.MapScalars(scalarMapping));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectorsSimplify(
                kv => kv.MapScalars(processor, scalarMapping), 
                processor
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectorsSimplify(
                kv => kv.MapScalars(processor, scalarMapping), 
                processor
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(scalarMapping);

        return IsZero 
            ? Processor.ScalarZero  
            : MapKVectorsSimplify(kv => kv.MapScalars(scalarMapping));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectorsSimplify(
                kv => kv.MapScalars(processor, scalarMapping), 
                processor
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectorsSimplify(
                kv => kv.MapScalars(processor, scalarMapping), 
                processor
            );
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaKVector<T>> MapKVectorPairs(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        foreach (var kv2 in mv2)
        foreach (var kv1 in _gradeKVectorDictionary.Values)
            yield return kVectorMapping(kv1, kv2);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<XGaKVector<T>> MapKVectorPairs(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        foreach (var kv2 in mv2)
        foreach (var kv1 in _gradeKVectorDictionary.Values)
            if (pairFilter(kv1, kv2))
                yield return kVectorMapping(kv1, kv2);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> MapKVectors(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.GradedMultivectorFromSum(
            MapKVectorPairs(mv2, kVectorMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> MapKVectors(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.GradedMultivectorFromSum(
            MapKVectorPairs(mv2, pairFilter, kVectorMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> MapKVectors(Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T> MapKVectors(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapKVectorsSimplify(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.MultivectorFromSum(
            MapKVectorPairs(mv2, kVectorMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapKVectorsSimplify(IEnumerable<XGaKVector<T>> mv2, Func<XGaKVector<T>, XGaKVector<T>, bool> pairFilter, Func<XGaKVector<T>, XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.MultivectorFromSum(
            MapKVectorPairs(mv2, pairFilter, kVectorMapping)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapKVectorsSimplify(Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> MapKVectorsSimplify(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T>> kVectorMapping)
    {
        return Processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivector MapKVectors(Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
    {
        return processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T1> MapKVectors<T1>(Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
    {
        return processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64GradedMultivector MapKVectors(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
    {
        return processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaGradedMultivector<T1> MapKVectors<T1>(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
    {
        return processor.GradedMultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
    {
        return processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T1> MapKVectorsSimplify<T1>(Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
    {
        return processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Select(kVectorMapping)
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector MapKVectorsSimplify(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaFloat64KVector> kVectorMapping, XGaFloat64Processor processor)
    {
        return processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T1> MapKVectorsSimplify<T1>(Func<XGaKVector<T>, bool> kVectorFilter, Func<XGaKVector<T>, XGaKVector<T1>> kVectorMapping, XGaProcessor<T1> processor)
    {
        return processor.MultivectorFromSum(
            _gradeKVectorDictionary.Values.Where(kVectorFilter).Select(kVectorMapping)
        );
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
            .GetSimpleMultivector();
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
            .GetSimpleMultivector();
    }

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Op(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddOpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> EGp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddEGpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Gp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddGpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> ELcp(XGaScalar<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    Debug.Assert(mv2.ScalarValue != null, "mv2.ScalarValue != null");

    //    return Processor.Scalar(
    //        Scalar() * mv2.ScalarValue
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return ELcp(scalar);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> Lcp(XGaScalar<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    Debug.Assert(mv2.ScalarValue != null, "mv2.ScalarValue != null");

    //    return Processor.Scalar(
    //        Scalar() * mv2.ScalarValue
    //    );
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Lcp(scalar);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> ERcp(XGaScalar<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Rcp(XGaScalar<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
    //        ? ((XGaScalar<T>)scalarPart).ESp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaVector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
    //        ? ((XGaVector<T>)vectorPart).ESp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaBivector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
    //        ? ((XGaBivector<T>)bivectorPart).ESp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
    //        ? ((XGaHigherKVector<T>)kVectorPart).ESp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaScalar<T> mv2)
    //{
    //    Debug.Assert(HasSameMetric(mv2));

    //    return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
    //        ? ((XGaScalar<T>)scalarPart).Sp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaVector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
    //        ? ((XGaVector<T>)vectorPart).Sp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaBivector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
    //        ? ((XGaBivector<T>)bivectorPart).Sp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    //{
    //    return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
    //        ? ((XGaHigherKVector<T>)kVectorPart).Sp(mv2)
    //        : Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}
}