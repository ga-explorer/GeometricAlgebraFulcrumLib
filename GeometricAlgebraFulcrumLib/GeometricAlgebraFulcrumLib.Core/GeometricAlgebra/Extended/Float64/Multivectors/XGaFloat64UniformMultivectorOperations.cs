using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64UniformMultivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1)
    {
        return v1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Add(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Subtract(v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(double v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, XGaFloat64Scalar v2)
    {
        return v1.Times(v2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64Scalar v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator /(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(1d / v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector MapScalars(Func<double, double> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
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
    public XGaFloat64UniformMultivector MapScalars(Func<IndexSet, double, double> scalarMapping)
    {
        if (IsZero)
            return this;

        var idScalarPairs =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
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
    public XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, IndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
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
    public XGaFloat64UniformMultivector MapBasisBlades(Func<IndexSet, double, IndexSet> basisMapping)
    {
        if (IsZero)
            return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
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
    public XGaFloat64UniformMultivector MapTerms(Func<IndexSet, double, KeyValuePair<IndexSet, double>> termMapping)
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
    public XGaFloat64UniformMultivector Negative()
    {
        return MapScalars(s => -(s));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector Times(double scalar)
    {
        return MapScalars(s => s * scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector Divide(double scalar)
    {
        return MapScalars(s => s / scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.ReverseIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector Conjugate()
    {
        return MapScalars((basis, scalar) =>
            Metric.HermitianConjugateSign(basis) * scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64UniformMultivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Dual(XGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector EUnDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector UnDual(XGaFloat64KVector blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EGp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.UniformMultivectorZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetUniformMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Scalar mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }
}