using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaUniformMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator +(XGaUniformMultivector<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator -(XGaUniformMultivector<T> v1)
    {
        return v1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator +(XGaUniformMultivector<T> v1, XGaMultivector<T> v2)
    {
        return (XGaUniformMultivector<T>)v1.Add(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator -(XGaUniformMultivector<T> v1, XGaMultivector<T> v2)
    {
        return (XGaUniformMultivector<T>)v1.Subtract(v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaUniformMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        return v1.Times(processor.ScalarProcessor.ValueFromNumber(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaUniformMultivector<T> v1, T v2)
    {
        return v1.Times(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(double v1, XGaUniformMultivector<T> v2)
    {
        var scalarProcessor = v2.Processor;

        return v2.Times(scalarProcessor.ScalarProcessor.ValueFromNumber(v1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(T v1, XGaUniformMultivector<T> v2)
    {
        return v2.Times(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaUniformMultivector<T> v1, XGaScalar<T> v2)
    {
        return v1.Times(v2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaScalar<T> v1, XGaUniformMultivector<T> v2)
    {
        return v2.Times(v1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator /(XGaUniformMultivector<T> v1, double v2)
    {
        var scalarProcessor = v1.Processor;

        return v1.Times(scalarProcessor.ScalarProcessor.ValueFromNumber(1d / v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator /(XGaUniformMultivector<T> v1, T v2)
    {
        var s2 = v1.ScalarProcessor.Divide(1, v2).ScalarValue;

        return v1.Times(s2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return Processor
            .CreateUniformComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateUniformComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateMultivectorComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return Processor
            .CreateUniformComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateUniformComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateMultivectorComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return Processor
            .CreateUniformComposer()
            .AddTerms(termList)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> MapBasisBlades(Func<IndexSet, T, IndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return Processor
            .CreateUniformComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> MapTerms(Func<IndexSet, T, KeyValuePair<IndexSet, T>> termMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                    termMapping(term.Key, term.Value)
            );

        return Processor
            .CreateUniformComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Times(int scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Times(double scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Times(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalar).ScalarValue);
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
    public override XGaUniformMultivector<T> Divide(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalar).ScalarValue);
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
    public override XGaUniformMultivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Conjugate()
    {
        return MapScalars((basis, scalar) =>
            ScalarProcessor.Times(Processor.HermitianConjugateSign(basis), scalar).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaUniformMultivector<T> PseudoInverse()
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
        return Processor
            .CreateUniformComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        return Processor
            .CreateUniformComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddOpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddEGpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddGpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.UniformMultivectorZero;

    //    return Processor
    //        .CreateUniformComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetUniformMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaBivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
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
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaBivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
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