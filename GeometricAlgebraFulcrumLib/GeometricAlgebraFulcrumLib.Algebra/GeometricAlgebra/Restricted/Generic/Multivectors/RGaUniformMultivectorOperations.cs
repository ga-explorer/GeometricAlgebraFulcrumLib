using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

public sealed partial class RGaUniformMultivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator +(RGaUniformMultivector<T> v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator -(RGaUniformMultivector<T> v1)
    {
        return v1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator +(RGaUniformMultivector<T> v1, RGaMultivector<T> v2)
    {
        return (RGaUniformMultivector<T>)v1.Add(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator -(RGaUniformMultivector<T> v1, RGaMultivector<T> v2)
    {
        return (RGaUniformMultivector<T>)v1.Subtract(v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(RGaUniformMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        return v1.Times(processor.ScalarProcessor.ValueFromNumber(v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(RGaUniformMultivector<T> v1, T v2)
    {
        return v1.Times(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(double v1, RGaUniformMultivector<T> v2)
    {
        var processor = v2.Processor;

        return v2.Times(processor.ScalarProcessor.ValueFromNumber(v1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(T v1, RGaUniformMultivector<T> v2)
    {
        return v2.Times(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(RGaUniformMultivector<T> v1, RGaScalar<T> v2)
    {
        return v1.Times(v2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator *(RGaScalar<T> v1, RGaUniformMultivector<T> v2)
    {
        return v2.Times(v1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator /(RGaUniformMultivector<T> v1, double v2)
    {
        var processor = v1.Processor;

        return v1.Times(processor.ScalarProcessor.ValueFromNumber(1d / v2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaUniformMultivector<T> operator /(RGaUniformMultivector<T> v1, T v2)
    {
        var s2 = v1.ScalarProcessor.Divide(1, v2).ScalarValue;

        return v1.Times(s2);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaUniformMultivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T1>(
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
    public RGaFloat64UniformMultivector MapScalars(RGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaUniformMultivector<T> MapScalars(Func<ulong, T, T> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaUniformMultivector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<ulong, T, T1> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T1>(
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
    public RGaFloat64UniformMultivector MapScalars(RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
    {
        if (IsZero)
            return processor.UniformMultivectorZero;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaUniformMultivector<T> MapBasisBlades(Func<ulong, ulong> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaUniformMultivector<T> MapBasisBlades(Func<ulong, T, ulong> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaUniformMultivector<T> MapTerms(Func<ulong, T, KeyValuePair<ulong, T>> termMapping)
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
    public RGaUniformMultivector<T> Negative()
    {
        return MapScalars(s => ScalarProcessor.Negative(s).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> Times(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Times(s, scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> Divide(T scalar)
    {
        return MapScalars(s => ScalarProcessor.Divide(s, scalar).ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Grade().ReverseIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Grade().GradeInvolutionIsNegativeOfGrade()
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? ScalarProcessor.Negative(scalar).ScalarValue
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> Conjugate()
    {
        return MapScalars((basis, scalar) =>
            ScalarProcessor.Times(Processor.HermitianConjugateSign(basis), scalar).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaUniformMultivector<T> PseudoInverse()
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
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Subtract(RGaMultivector<T> mv2)
    {
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Op(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> EGp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Gp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ELcp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Lcp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ERcp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Rcp(RGaMultivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaScalar<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
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
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
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