using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Processors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaBivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator -(XGaBivector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator +(XGaBivector<T> mv1, XGaBivector<T> mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator -(XGaBivector<T> mv1, XGaBivector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(IntegerSign mv1, XGaBivector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(int mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(uint mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(long mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(ulong mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(float mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(double mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(T mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(Scalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(IScalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaScalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaVector<T>> GetVectorBasis()
    {
        var closestVector = Processor.VectorTerm(0);

        return GetVectorBasis(closestVector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaVector<T>> GetVectorBasis(int closestBasisVectorIndex)
    {
        var closestVector = Processor.VectorTerm(closestBasisVectorIndex);

        return GetVectorBasis(closestVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaVector<T>> GetVectorBasis(XGaVector<T> closestVector)
    {
        var e1 = closestVector.Lcp(this).DivideByNorm();
        var e2 = e1.Lcp(this);

        Debug.Assert((e1.Op(e2) - this).IsNearZero());

        return new Pair<XGaVector<T>>(e1, e2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> MapScalars(Func<T, T> scalarMapping)
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> MapScalars(Func<IndexSet, T, T> scalarMapping)
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector MapScalars(XGaFloat64Processor processor, Func<IndexSet, T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IndexSet, T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T1>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> MapScalars(Func<int, int, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Count == 1)
                .Select(
                    term => new KeyValuePair<IndexSet, T>(
                        term.Key,
                        scalarMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value)
                    )
                );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key.FirstIndex, term.Key.LastIndex).ToPairIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateBivectorComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> MapBasisBivectors(Func<int, int, T, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    basisMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value).ToPairIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateBivectorComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> MapTerms(Func<int, int, T, KeyValuePair<IPair<int>, T>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (indexPair, scalar) = termMapping(
                        term.Key.FirstIndex, 
                        term.Key.LastIndex, 
                        term.Value
                    );

                    return new KeyValuePair<IndexSet, T>(
                        indexPair.ToPairIndexSet(),
                        scalar
                    );
                }
            );

        return Processor
            .CreateBivectorComposer()
            .AddTerms(termList)
            .GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Negative()
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(int scalarValue)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(double scalarValue)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Times(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(Scalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(int scalarValue)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(double scalarValue)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(T scalarValue)
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(Scalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                ScalarProcessor.Times(
                    Processor.HermitianConjugateSign(basisVector),
                    scalar
                ).ScalarValue
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Add(XGaBivector<T> mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .AddKVector(mv2)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaMultivector<T> Add(IScalar<T> mv2)
    {
        if (IsZero)
            return Processor.Scalar(mv2);

        if (mv2.IsZero())
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .AddScalarTerm(mv2.ScalarValue)
            .GetSimpleMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaBivector<T> bivector)
            return Add(bivector);

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
    public XGaBivector<T> Subtract(XGaBivector<T> mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .SubtractKVector(mv2)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaBivector<T> bivector)
            return Subtract(bivector);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetKVector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaBivector<T> Op(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Op(XGaKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaScalar<T> scalar)
    //        return Times(scalar.ScalarValue);

    //    return Processor
    //        .CreateKVectorComposer(2 + mv2.Grade)
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
    //            .CreateKVectorComposer(2 + kVector.Grade)
    //            .AddOpTerms(this, mv2)
    //            .GetKVector();

    //    return Processor
    //        .CreateMultivectorComposer()
    //        .AddOpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaBivector<T> EGp(XGaScalar<T> mv2)
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
    //        .CreateMultivectorComposer()
    //        .AddEGpTerms(this, mv2)
    //        .GetSimpleMultivector();
    //}


    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaBivector<T> Gp(XGaScalar<T> mv2)
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
    //public XGaScalar<T> ELcp(XGaBivector<T> mv2)
    //{
    //    return ESp(mv2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ELcp(XGaHigherKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor
    //        .CreateComposer()
    //        .AddELcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - 2);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> ELcp(XGaKVector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> or XGaVector<T>)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaBivector<T> bv)
    //        return ESp(bv);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
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
    //public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> or XGaVector<T>)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaBivector<T> bv)
    //        return ESp(bv);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    if (mv2 is XGaHigherKVector<T> kv)
    //        return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(kv.Grade - 2);

    //    if (mv2 is XGaGradedMultivector<T> mv)
    //        return Processor.CreateComposer().AddELcpTerms(this, mv).GetSimpleMultivector();

    //    return Processor.CreateComposer().AddELcpTerms(this, mv2).GetSimpleMultivector();
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
    //public XGaScalar<T> Lcp(XGaBivector<T> mv2)
    //{
    //    return Sp(mv2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Lcp(XGaHigherKVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor
    //        .CreateComposer()
    //        .AddLcpTerms(this, mv2)
    //        .GetKVector(mv2.Grade - 2);
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Lcp(XGaKVector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> or XGaVector<T>)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaBivector<T> bv)
    //        return ESp(bv);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
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
    //public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> or XGaVector<T>)
    //        return Processor.ScalarZero;

    //    if (mv2 is XGaBivector<T> bv)
    //        return ESp(bv);

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    if (mv2 is XGaHigherKVector<T> kv)
    //        return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(kv.Grade - 2);
            
    //    if (mv2 is XGaGradedMultivector<T> mv)
    //        return Processor.CreateComposer().AddLcpTerms(this, mv).GetSimpleMultivector();
            
    //    return Processor.CreateComposer().AddLcpTerms(this, mv2).GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaBivector<T> ERcp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaVector<T> ERcp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.VectorZero;
            
    //    return Processor
    //        .CreateComposer()
    //        .AddERcpTerms(this, mv2)
    //        .GetVector();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> ERcp(XGaBivector<T> mv2)
    //{
    //    return ESp(mv2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> ERcp(XGaHigherKVector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> ERcp(XGaKVector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> s => ERcp(s),
    //        XGaVector<T> v => ERcp(v),
    //        XGaBivector<T> bv => ESp(bv),
    //        _ => Processor.ScalarZero
    //    };
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
    //public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> s)
    //        return ERcp(s);

    //    if (mv2 is XGaVector<T> v)
    //        return ERcp(v);

    //    if (mv2 is XGaBivector<T> bv)
    //        return ESp(bv);

    //    if (mv2 is XGaHigherKVector<T>)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    if (mv2 is XGaGradedMultivector<T> mv)
    //        return Processor.CreateComposer().AddERcpTerms(this, mv).GetSimpleMultivector();

    //    return Processor.CreateComposer().AddERcpTerms(this, mv2).GetSimpleMultivector();
    //}
        

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaBivector<T> Rcp(XGaScalar<T> mv2)
    //{
    //    return Times(mv2.ScalarValue);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaVector<T> Rcp(XGaVector<T> mv2)
    //{
    //    if (IsZero || mv2.IsZero)
    //        return Processor.VectorZero;
            
    //    return Processor
    //        .CreateComposer()
    //        .AddRcpTerms(this, mv2)
    //        .GetVector();
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaScalar<T> Rcp(XGaBivector<T> mv2)
    //{
    //    return Sp(mv2);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public XGaKVector<T> Rcp(XGaHigherKVector<T> _)
    //{
    //    return Processor.ScalarZero;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaKVector<T> Rcp(XGaKVector<T> mv2)
    //{
    //    return mv2 switch
    //    {
    //        XGaScalar<T> s => Rcp(s),
    //        XGaVector<T> v => Rcp(v),
    //        XGaBivector<T> bv => Sp(bv),
    //        _ => Processor.ScalarZero
    //    };
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
    //public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    //{
    //    if (mv2 is XGaScalar<T> s)
    //        return Rcp(s);

    //    if (mv2 is XGaVector<T> v)
    //        return Rcp(v);

    //    if (mv2 is XGaBivector<T> bv)
    //        return Sp(bv);

    //    if (mv2 is XGaHigherKVector<T>)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    if (mv2 is XGaGradedMultivector<T> mv)
    //        return Processor.CreateComposer().AddRcpTerms(this, mv).GetSimpleMultivector();
            
    //    return Processor.CreateComposer().AddRcpTerms(this, mv2).GetSimpleMultivector();
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
    //    return Processor.ScalarZero;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaKVector<T> mv2)
    //{
    //    if (mv2 is not XGaBivector<T> bv)
    //        return Processor.ScalarZero;
            
    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, bv)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
    //{
    //    if (!mv2.TryGetKVector(2, out var kv))
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;

    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddESpTerms(this, (XGaBivector<T>)kv)
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
    //    return Processor.ScalarZero;
    //}
        
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaKVector<T> mv2)
    //{
    //    if (mv2 is not XGaBivector<T> bv)
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, bv)
    //        .GetXGaScalar(Processor);
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
    //{
    //    if (!mv2.TryGetKVector(2, out var kv))
    //        return Processor.ScalarZero;

    //    if (IsZero || mv2.IsZero)
    //        return Processor.ScalarZero;
            
    //    return ScalarProcessor
    //        .CreateScalarComposer()
    //        .AddSpTerms(this, (XGaBivector<T>)kv)
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

    
    /// <summary>
    /// Create a pure rotor multivector from a 2-blade
    /// </summary>
    /// <returns></returns>
    public XGaMultivector<T> Exp()
    {
        var bv2 = 
            Gp(this);

        if (!bv2.IsScalar())
            throw new InvalidOperationException("Bivector is not a blade");

        var bladeSignature = bv2.Scalar();

        if (bladeSignature.IsZero())
            return 1d + this;

        if (bladeSignature < 0)
        {
            var alpha = (-bladeSignature).Sqrt();

            return alpha.Cos() + alpha.Sin() / alpha * this;
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            
            return alpha.Cosh() + alpha.Sinh() / alpha * this;
        }
    }
}