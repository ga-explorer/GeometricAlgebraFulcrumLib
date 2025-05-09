using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Core.Structures.Dictionary;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.Multivectors;

public sealed partial class RGaGradedMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator +(RGaGradedMultivector<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator -(RGaGradedMultivector<T> v1)
    {
        if (v1.IsZero) return v1;

        return (RGaGradedMultivector<T>) v1.MapKVectors(kv => kv.Negative(), false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator +(RGaGradedMultivector<T> v1, RGaGradedMultivector<T> v2)
    {
        if (v1.IsZero)
            return v2;

        if (v2.IsZero)
            return v1;

        return v1.Processor
            .CreateComposer()
            .SetMultivector(v1)
            .AddMultivector(v2)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator -(RGaGradedMultivector<T> v1, RGaGradedMultivector<T> v2)
    {
        if (v1.IsZero)
            return v2;

        if (v2.IsZero)
            return v1;

        return v1.Processor
            .CreateComposer()
            .SetMultivector(v1)
            .SubtractMultivector(v2)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(RGaGradedMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        if (v1.IsZero || v2.IsZero())
            return processor.MultivectorZero;

        if (v2.IsOne())
            return v1;

        var s2 = processor.ScalarProcessor.ScalarFromNumber(v2);

        return (RGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2.ScalarValue), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(RGaGradedMultivector<T> v1, T v2)
    {
        var processor = v1.Processor;

        if (v1.IsZero || processor.ScalarProcessor.IsZero(v2))
            return processor.MultivectorZero;

        if (processor.ScalarProcessor.IsOne(v2))
            return v1;

        return (RGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(v2), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(double v1, RGaGradedMultivector<T> v2)
    {
        var processor = v2.Processor;

        if (v2.IsZero || v1.IsZero())
            return processor.MultivectorZero;

        if (v1.IsOne()) return v2;

        var s1 = processor.ScalarProcessor.ScalarFromNumber(v1);

        return (RGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(s1.ScalarValue), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(T v1, RGaGradedMultivector<T> v2)
    {
        var processor = v2.Processor;

        if (v2.IsZero || processor.ScalarProcessor.IsZero(v1))
            return processor.MultivectorZero;

        if (processor.ScalarProcessor.IsOne(v1)) return v2;

        return (RGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(v1), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(RGaGradedMultivector<T> v1, RGaScalar<T> v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v2.IsOne) return v1;

        var processor = v1.Processor;

        if (v1.IsZero || v2.IsZero)
            return processor.MultivectorZero;

        var s2 = v2.ScalarValue;

        return (RGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator *(RGaScalar<T> v1, RGaGradedMultivector<T> v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v1.IsOne) return v2;

        var processor = v1.Processor;

        if (v1.IsZero || v2.IsZero)
            return processor.MultivectorZero;

        var s1 = v1.ScalarValue;

        return (RGaGradedMultivector<T>)v2.MapKVectors(kv => kv.Times(s1), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator /(RGaGradedMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        if (v2.IsOne()) return v1;

        var s2 = processor.ScalarProcessor.ScalarFromNumber(1d / v2);

        return (RGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Times(s2.ScalarValue), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaGradedMultivector<T> operator /(RGaGradedMultivector<T> v1, T v2)
    {
        var processor = v1.Processor;

        if (processor.ScalarProcessor.IsOne(v2)) return v1;

        return (RGaGradedMultivector<T>)v1.MapKVectors(kv => kv.Divide(v2), false);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(scalarMapping);

        return IsZero 
            ? this 
            : MapKVectors(kv => kv.MapScalars(scalarMapping));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapScalars(RGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectors(processor, kv => kv.MapScalars(processor, scalarMapping));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectors(
                processor,
                kv => kv.MapScalars(processor, scalarMapping)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> MapScalars(Func<ulong, T, T> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(scalarMapping);

        return IsZero 
            ? Processor.ScalarZero  
            : MapKVectors(kv => kv.MapScalars(scalarMapping));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapScalars(RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectors(
                processor,
                kv => kv.MapScalars(processor, scalarMapping)
            );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<ulong, T, T1> scalarMapping)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary
                .Values
                .First()
                .MapScalars(processor, scalarMapping);

        return IsZero 
            ? processor.ScalarZero  
            : MapKVectors(
                processor, 
                kv => kv.MapScalars(processor, scalarMapping)
            );
    }
        

    public RGaMultivector<T> MapKVectors(Func<RGaKVector<T>, RGaKVector<T>> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaKVector<T>>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? Processor.ScalarZero
                : new RGaGradedMultivector<T>(
                    Processor, 
                    new EmptyDictionary<int, RGaKVector<T>>()
                );

        if (kVectorDictionary.Count == 1)
            return new RGaGradedMultivector<T>(
                Processor,
                new SingleItemDictionary<int, RGaKVector<T>>(kVectorDictionary.First())
            );

        var mv = new RGaGradedMultivector<T>(
            Processor, 
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }
        
    public RGaFloat64Multivector MapKVectors(RGaFloat64Processor processor, Func<RGaKVector<T>, RGaFloat64KVector> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? processor.ScalarZero
                : processor.Multivector(
                    new EmptyDictionary<int, RGaFloat64KVector>()
                );

        if (kVectorDictionary.Count == 1)
            return processor.Multivector(
                new SingleItemDictionary<int, RGaFloat64KVector>(kVectorDictionary.First())
            );

        var mv = processor.Multivector(
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }

    public RGaMultivector<T1> MapKVectors<T1>(RGaProcessor<T1> processor, Func<RGaKVector<T>, RGaKVector<T1>> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaKVector<T1>>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? processor.ScalarZero
                : processor.Multivector(
                    new EmptyDictionary<int, RGaKVector<T1>>()
                );

        if (kVectorDictionary.Count == 1)
            return processor.Multivector(
                new SingleItemDictionary<int, RGaKVector<T1>>(kVectorDictionary.First())
            );

        var mv = processor.Multivector(
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }

    public RGaMultivector<T> MapKVectors(Func<int, RGaKVector<T>, RGaKVector<T>> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaKVector<T>>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(grade, kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? Processor.ScalarZero
                : new RGaGradedMultivector<T>(
                    Processor,
                    new EmptyDictionary<int, RGaKVector<T>>()
                );

        if (kVectorDictionary.Count == 1)
            return new RGaGradedMultivector<T>(
                Processor,
                new SingleItemDictionary<int, RGaKVector<T>>(kVectorDictionary.First())
            );

        var mv = new RGaGradedMultivector<T>(
            Processor, 
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }
        
    public RGaFloat64Multivector MapKVectors(RGaFloat64Processor processor, Func<int, RGaKVector<T>, RGaFloat64KVector> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaFloat64KVector>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(grade, kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? processor.ScalarZero
                : processor.Multivector(
                    new EmptyDictionary<int, RGaFloat64KVector>()
                );

        if (kVectorDictionary.Count == 1)
            return processor.Multivector(
                new SingleItemDictionary<int, RGaFloat64KVector>(kVectorDictionary.First())
            );

        var mv = processor.Multivector(
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }

    public RGaMultivector<T1> MapKVectors<T1>(RGaProcessor<T1> processor, Func<int, RGaKVector<T>, RGaKVector<T1>> kVectorMapping, bool simplify = true)
    {
        var kVectorDictionary = new Dictionary<int, RGaKVector<T1>>();

        foreach (var (grade, kVector) in _gradeKVectorDictionary)
        {
            var kVector1 = kVectorMapping(grade, kVector);

            if (!kVector1.IsZero)
                kVectorDictionary.Add(grade, kVector1);
        }

        if (kVectorDictionary.Count == 0)
            return simplify
                ? processor.ScalarZero
                : processor.Multivector(
                    new EmptyDictionary<int, RGaKVector<T1>>()
                );

        if (kVectorDictionary.Count == 1)
            return processor.Multivector(
                new SingleItemDictionary<int, RGaKVector<T1>>(kVectorDictionary.First())
            );

        var mv = processor.Multivector(
            kVectorDictionary
        );

        return simplify ? mv.Simplify() : mv;
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Negative()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Negative();

        return IsZero 
            ? Processor.ScalarZero 
            : MapKVectors(kv => kv.Negative());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Times(T scalar)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Times(scalar);

        if (IsZero || ScalarProcessor.IsZero(scalar))
            return Processor.ScalarZero;

        return ScalarProcessor.IsOne(scalar) 
            ? this 
            : MapKVectors(kv => kv.Times(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Divide(T scalar)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Divide(scalar);

        if (IsZero || ScalarProcessor.IsZero(scalar))
            return Processor.ScalarZero;

        return ScalarProcessor.IsOne(scalar) 
            ? this 
            : MapKVectors(kv => kv.Divide(scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Reverse()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Reverse();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> GradeInvolution()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().GradeInvolution();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.GradeInvolution());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> CliffordConjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().CliffordConjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.CliffordConjugate());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Conjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Conjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.Conjugate());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EDual(RGaKVector<T> blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Dual(RGaKVector<T> blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EUnDual(RGaKVector<T> blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> UnDual(RGaKVector<T> blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Add(RGaMultivector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Subtract(RGaMultivector<T> mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return Simplify();

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Op(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Op(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> EGp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> EGp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Gp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Gp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ELcp(RGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor.Scalar(
            Scalar() * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ELcp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return ELcp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Lcp(RGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor.Scalar(
            Scalar() * mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Lcp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Lcp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> ERcp(RGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ERcp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaMultivector<T> Rcp(RGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Rcp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaScalar<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
            ? ((RGaScalar<T>)scalarPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaVector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
            ? ((RGaVector<T>)vectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaBivector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
            ? ((RGaBivector<T>)bivectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaHigherKVector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
            ? ((RGaHigherKVector<T>)kVectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaGradedMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaUniformMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaScalar<T> mv2)
    {
        Debug.Assert(HasSameMetric(mv2));

        return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
            ? ((RGaScalar<T>)scalarPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaVector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
            ? ((RGaVector<T>)vectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaBivector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
            ? ((RGaBivector<T>)bivectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaHigherKVector<T> mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
            ? ((RGaHigherKVector<T>)kVectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaGradedMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaUniformMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
}