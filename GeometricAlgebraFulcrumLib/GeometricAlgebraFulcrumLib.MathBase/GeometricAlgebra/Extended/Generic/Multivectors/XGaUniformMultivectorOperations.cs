﻿using System.Runtime.CompilerServices;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

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

        return v1.Times(processor.ScalarProcessor.GetScalarFromNumber(v2));
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

        return v2.Times(scalarProcessor.ScalarProcessor.GetScalarFromNumber(v1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(T v1, XGaUniformMultivector<T> v2)
    {
        return v2.Times(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaUniformMultivector<T> v1, XGaScalar<T> v2)
    {
        return v1.Times(v2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator *(XGaScalar<T> v1, XGaUniformMultivector<T> v2)
    {
        return v2.Times(v1.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator /(XGaUniformMultivector<T> v1, double v2)
    {
        var scalarProcessor = v1.Processor;

        return v1.Times(scalarProcessor.ScalarProcessor.GetScalarFromNumber(1d / v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaUniformMultivector<T> operator /(XGaUniformMultivector<T> v1, T v2)
    {
        var s2 = v1.ScalarProcessor.Divide(1, v2);

        return v1.Times(s2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.CreateZeroUniformMultivector();

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero)
            return processor.CreateZeroUniformMultivector();

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> MapScalars(Func<IIndexSet, T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IIndexSet, T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.CreateZeroUniformMultivector();

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector MapScalars(XGaFloat64Processor processor, Func<IIndexSet, T, double> scalarMapping)
    {
        if (IsZero)
            return processor.CreateZeroUniformMultivector();

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateComposer()
            .AddTerms(idScalarPairs)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> MapBasisBlades(Func<IIndexSet, IIndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    basisMapping(term.Key),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> MapBasisBlades(Func<IIndexSet, T, IIndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    basisMapping(term.Key, term.Value),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> MapTerms(Func<IIndexSet, T, KeyValuePair<IIndexSet, T>> termMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                    termMapping(term.Key, term.Value)
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetUniformMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Times(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalar));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Divide(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalar));
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? ScalarProcessor.Negative(scalar)
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Conjugate()
    {
        return MapScalars((basis, scalar) =>
            ScalarProcessor.Times(Processor.ConjugateSign(basis), scalar)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaUniformMultivector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue()
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.CreatePseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EDual(XGaKVector<T> blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.CreatePseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Dual(XGaKVector<T> blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.CreatePseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> EUnDual(XGaKVector<T> blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.CreatePseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> UnDual(XGaKVector<T> blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroUniformMultivector();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaUniformMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaUniformMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetXGaScalar(Processor);
    }
}