using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64GradedMultivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator +(RGaFloat64GradedMultivector v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator -(RGaFloat64GradedMultivector v1)
    {
        if (v1.IsZero) return v1;

        return (RGaFloat64GradedMultivector) v1.MapKVectors(kv => kv.Negative(), false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator +(RGaFloat64GradedMultivector v1, RGaFloat64GradedMultivector v2)
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
    public static RGaFloat64GradedMultivector operator -(RGaFloat64GradedMultivector v1, RGaFloat64GradedMultivector v2)
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
    public static RGaFloat64GradedMultivector operator *(RGaFloat64GradedMultivector v1, double v2)
    {
        var metric = v1.Processor;
            
        if (v1.IsZero || v2.IsZero())
            return metric.MultivectorZero;

        if (v2.IsOne())
            return v1;

        return (RGaFloat64GradedMultivector)v1.MapKVectors(kv => kv.Times(v2), false);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator *(double v1, RGaFloat64GradedMultivector v2)
    {
        var metric = v2.Processor;

        if (v2.IsZero || v1.IsZero())
            return metric.MultivectorZero;

        if (v1.IsOne()) return v2;

        return (RGaFloat64GradedMultivector)v2.MapKVectors(kv => kv.Times(v1), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator *(RGaFloat64GradedMultivector v1, RGaFloat64Scalar v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v2.IsOne) return v1;

        var metric = v1.Processor;
            

        if (v1.IsZero || v2.IsZero)
            return metric.MultivectorZero;

        var s2 = v2.ScalarValue;

        return (RGaFloat64GradedMultivector)v1.MapKVectors(kv => kv.Times(s2), false);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator *(RGaFloat64Scalar v1, RGaFloat64GradedMultivector v2)
    {
        Debug.Assert(
            v1.HasSameMetric(v2)
        );

        if (v1.IsOne) return v2;

        var metric = v1.Processor;
            

        if (v1.IsZero || v2.IsZero)
            return metric.MultivectorZero;

        var s1 = v1.ScalarValue;

        return (RGaFloat64GradedMultivector)v2.MapKVectors(kv => kv.Times(s1), false);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64GradedMultivector operator /(RGaFloat64GradedMultivector v1, double v2)
    {
        if (v2.IsOne()) return v1;

        return (RGaFloat64GradedMultivector)v1.MapKVectors(kv => kv.Divide(v2), false);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector MapScalars(Func<double, double> scalarMapping)
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
    public RGaFloat64Multivector MapScalars(Func<ulong, double, double> scalarMapping)
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
    public RGaFloat64Multivector Negative()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Negative();

        return IsZero 
            ? Processor.ScalarZero 
            : MapKVectors(kv => kv.Negative());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Times(double scalar)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Times(scalar);

        if (IsZero || scalar.IsZero())
            return Processor.ScalarZero;

        return scalar.IsOne() 
            ? this 
            : MapKVectors(kv => kv.Times(scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Divide(double scalar)
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Divide(scalar);

        if (IsZero || scalar.IsZero())
            return Processor.ScalarZero;

        return scalar.IsOne() 
            ? this 
            : MapKVectors(kv => kv.Divide(scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Reverse()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Reverse();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector GradeInvolution()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().GradeInvolution();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.GradeInvolution());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector CliffordConjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().CliffordConjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.CliffordConjugate());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Conjugate()
    {
        if (_gradeKVectorDictionary.Count == 1)
            return _gradeKVectorDictionary.Values.First().Conjugate();

        return IsZero
            ? Processor.ScalarZero
            : MapKVectors(kVector => kVector.Conjugate());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EDual(RGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Dual(RGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EUnDual(RGaFloat64KVector blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector UnDual(RGaFloat64KVector blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
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
    public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
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
    public RGaFloat64Multivector Op(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Op(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector EGp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector EGp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Gp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor.Scalar(
            Scalar() * mv2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return ELcp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor.Scalar(
            Scalar() * mv2.ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Lcp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector ERcp(RGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Rcp(RGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Scalar mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
            ? ((RGaFloat64Scalar)scalarPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Vector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
            ? ((RGaFloat64Vector)vectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Bivector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
            ? ((RGaFloat64Bivector)bivectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
            ? ((RGaFloat64HigherKVector)kVectorPart).ESp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Scalar mv2)
    {
        Debug.Assert(HasSameMetric(mv2));

        return _gradeKVectorDictionary.TryGetValue(0, out var scalarPart)
            ? ((RGaFloat64Scalar)scalarPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Vector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(1, out var vectorPart)
            ? ((RGaFloat64Vector)vectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Bivector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(2, out var bivectorPart)
            ? ((RGaFloat64Bivector)bivectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
    {
        return _gradeKVectorDictionary.TryGetValue(mv2.Grade, out var kVectorPart)
            ? ((RGaFloat64HigherKVector)kVectorPart).Sp(mv2)
            : Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
}