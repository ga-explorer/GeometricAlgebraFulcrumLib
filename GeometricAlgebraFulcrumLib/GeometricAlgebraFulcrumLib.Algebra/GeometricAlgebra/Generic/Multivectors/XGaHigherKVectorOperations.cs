using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaHigherKVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator -(XGaHigherKVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.HigherKVectorZero(mv1.Grade);

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(IntegerSign mv1, XGaHigherKVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.HigherKVectorZero(mv2.Grade);

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, int mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(int mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, uint mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(uint mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, long mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(long mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, ulong mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(ulong mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, float mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(float mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, double mv2)
    {
        return mv1.Times(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(double mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(T mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(Scalar<T> mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaHigherKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator *(XGaScalar<T> mv1, XGaHigherKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, int mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, uint mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, long mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, float mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, double mv2)
    {
        return mv1.Divide(mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaHigherKVector<T> operator /(XGaHigherKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64HigherKVector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Negative(term.Value).ScalarValue
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Times(int scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Times(double scalar)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Times(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Times(Scalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Divide(int scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Divide(double scalar)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalar).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            throw new DivideByZeroException();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Divide(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Divide(Scalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Reverse()
    {
        return IsZero || Grade.ReverseIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> GradeInvolution()
    {
        return IsZero || Grade.GradeInvolutionIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> CliffordConjugate()
    {
        return IsZero || Grade.CliffordConjugateIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisKVector, scalar) =>
                ScalarProcessor.Times(
                    Processor.HermitianConjugateSign(basisKVector),
                    scalar
                ).ScalarValue
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaHigherKVector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> AddSameGrade(XGaHigherKVector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateKVectorComposer(Grade)
            .SetKVector(this)
            .AddKVector(mv2)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaHigherKVector<T> mv && mv.Grade == Grade)
            return AddSameGrade(mv);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaHigherKVector<T> SubtractSameGrade(XGaHigherKVector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateKVectorComposer(Grade)
            .SetKVector(this)
            .SubtractKVector(mv2)
            .GetHigherKVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaHigherKVector<T> mv && mv.Grade == Grade)
            return SubtractSameGrade(mv);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaHigherKVector<T> Op(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Op(XGaKVector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor
    //        .CreateKVectorComposer(Grade + mv2.Grade)
    //        .AddOpTerms(this, mv2)
    //        .GetKVector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaKVector<T> kVector)
    //        return Processor
    //            .CreateComposer()
    //            .AddOpTerms(this, mv2)
    //            .GetKVector(Grade + kVector.Grade);
            
    //    return Processor
    //        .CreateComposer()
    //        .AddOpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaHigherKVector<T> EGp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return EGp(scalar);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddEGpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaHigherKVector<T> Gp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> scalar)
    //        return Gp(scalar);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddGpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> ELcp(XGaScalar<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> ELcp(XGaVector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> ELcp(XGaBivector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ELcp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade > mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - Grade);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> ELcp(XGaKVector<T> mv2)
    //{
    //    if (Grade > mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - Grade);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> ELcp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> ELcp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> mv => ELcp(mv),
    //        XGaVector<T> mv => ELcp(mv),
    //        XGaBivector<T> mv => ELcp(mv),
    //        XGaHigherKVector<T> mv => ELcp(mv),
    //        XGaGradedMultivector<T> mv => ELcp(mv),
    //        XGaUniformMultivector<T> mv => ELcp(mv),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> Lcp(XGaScalar<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> Lcp(XGaVector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> Lcp(XGaBivector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Lcp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade > mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - Grade);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Lcp(XGaKVector<T> mv2)
    //{
    //    if (Grade > mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - Grade);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Lcp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Lcp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> mv => Lcp(mv),
    //        XGaVector<T> mv => Lcp(mv),
    //        XGaBivector<T> mv => Lcp(mv),
    //        XGaHigherKVector<T> mv => Lcp(mv),
    //        XGaGradedMultivector<T> mv => Lcp(mv),
    //        XGaUniformMultivector<T> mv => Lcp(mv),
    //        _ => throw new InvalidOperationException()
    //    };
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaHigherKVector<T> ERcp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ERcp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetKVector(Grade - 1);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ERcp(XGaBivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetKVector(Grade - 2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ERcp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade < mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetKVector(Grade - mv2.Grade);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> ERcp(XGaKVector<T> mv2)
    //{
    //    if (Grade < mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetKVector(Grade - mv2.Grade);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> ERcp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> ERcp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> mv => ERcp(mv),
    //        XGaVector<T> mv => ERcp(mv),
    //        XGaBivector<T> mv => ERcp(mv),
    //        XGaHigherKVector<T> mv => ERcp(mv),
    //        XGaGradedMultivector<T> mv => ERcp(mv),
    //        XGaUniformMultivector<T> mv => ERcp(mv),
    //        _ => throw new InvalidOperationException()
    //    };
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaHigherKVector<T> Rcp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Rcp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetKVector(Grade - 1);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Rcp(XGaBivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetKVector(Grade - 2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Rcp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade < mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetKVector(Grade - mv2.Grade);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Rcp(XGaKVector<T> mv2)
    //{
    //    if (Grade < mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetKVector(Grade - mv2.Grade);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Rcp(XGaGradedMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaMultivector<T> Rcp(XGaUniformMultivector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> mv => Rcp(mv),
    //        XGaVector<T> mv => Rcp(mv),
    //        XGaBivector<T> mv => Rcp(mv),
    //        XGaHigherKVector<T> mv => Rcp(mv),
    //        XGaGradedMultivector<T> mv => Rcp(mv),
    //        XGaUniformMultivector<T> mv => Rcp(mv),
    //        _ => throw new InvalidOperationException()
    //    };
    //}

        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaVector<T> mv2)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaBivector<T> mv2)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade != mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaKVector<T> mv2)
    //{
    //    if (Grade != mv2.Grade)
    //        return Processor.ScalarZero;

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
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaVector<T> mv2)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaBivector<T> mv2)
    //{
    //    return Processor.ScalarZero;
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    //{
    //    if (Grade != mv2.Grade)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, mv2)
    //        .GetXGaScalar(Processor);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaKVector<T> mv2)
    //{
    //    if (Grade != mv2.Grade)
    //        return Processor.ScalarZero;

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