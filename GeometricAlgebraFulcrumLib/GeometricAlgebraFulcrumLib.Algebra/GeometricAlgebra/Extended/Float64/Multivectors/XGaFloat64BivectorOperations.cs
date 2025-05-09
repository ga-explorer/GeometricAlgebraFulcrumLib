using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator +(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(IntegerSign mv1, XGaFloat64Bivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(int mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(uint mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(long mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(ulong mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(float mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(double mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Scalar mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis()
    {
        var closestVector = Processor.VectorTerm(0);

        return GetVectorBasis(closestVector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis(int closestBasisVectorIndex)
    {
        var closestVector = Processor.VectorTerm(closestBasisVectorIndex);

        return GetVectorBasis(closestVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<XGaFloat64Vector> GetVectorBasis(XGaFloat64Vector closestVector)
    {
        var e1 = closestVector.Lcp(this).DivideByNorm();
        var e2 = e1.Lcp(this);

        Debug.Assert((e1.Op(e2) - this).IsNearZero());

        return new Pair<XGaFloat64Vector>(e1, e2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapScalars(Func<double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapScalars(Func<IndexSet, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapScalars(Func<int, int, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Count == 1)
                .Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value)
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    basisMapping(term.Key.FirstIndex, term.Key.LastIndex).IndexPairToIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapBasisBivectors(Func<int, int, double, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    basisMapping(term.Key.FirstIndex, term.Key.LastIndex, term.Value).IndexPairToIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector MapTerms(Func<int, int, double, KeyValuePair<IPair<int>, double>> termMapping)
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

                    return new KeyValuePair<IndexSet, double>(
                        indexPair.IndexPairToIndexSet(),
                        scalar
                    );
                }
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetBivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    -(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    term.Value * scalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    term.Value / scalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Metric.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Add(XGaFloat64Bivector mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Bivector bivector)
            return Add(bivector);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Subtract(XGaFloat64Bivector mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Bivector bivector)
            return Subtract(bivector);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Op(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Op(XGaFloat64KVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(2 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64KVector kVector)
            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetKVector(2 + kVector.Grade);

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector EGp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EGp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return EGp(scalar);
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Gp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Gp(scalar);
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Bivector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ELcp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ELcp(XGaFloat64KVector mv2)
    {
        if (mv2 is XGaFloat64Scalar or XGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar or XGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is XGaFloat64HigherKVector kv)
            return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(kv.Grade - 2);

        if (mv2 is XGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddELcpTerms(this, mv).GetSimpleMultivector();

        return Processor.CreateComposer().AddELcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Bivector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Lcp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Lcp(XGaFloat64KVector mv2)
    {
        if (mv2 is XGaFloat64Scalar or XGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar or XGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is XGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is XGaFloat64HigherKVector kv)
            return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(kv.Grade - 2);
            
        if (mv2 is XGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddLcpTerms(this, mv).GetSimpleMultivector();
            
        return Processor.CreateComposer().AddLcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector ERcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ERcp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;
            
        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ERcp(XGaFloat64Bivector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ERcp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ERcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar s => ERcp(s),
            XGaFloat64Vector v => ERcp(v),
            XGaFloat64Bivector bv => ESp(bv),
            _ => Processor.ScalarZero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar s)
            return ERcp(s);

        if (mv2 is XGaFloat64Vector v)
            return ERcp(v);

        if (mv2 is XGaFloat64Bivector bv)
            return ESp(bv);

        if (mv2 is XGaFloat64HigherKVector)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is XGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddERcpTerms(this, mv).GetSimpleMultivector();

        return Processor.CreateComposer().AddERcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Rcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Rcp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;
            
        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Rcp(XGaFloat64Bivector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Rcp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Rcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar s => Rcp(s),
            XGaFloat64Vector v => Rcp(v),
            XGaFloat64Bivector bv => Sp(bv),
            _ => Processor.ScalarZero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar s)
            return Rcp(s);

        if (mv2 is XGaFloat64Vector v)
            return Rcp(v);

        if (mv2 is XGaFloat64Bivector bv)
            return Sp(bv);

        if (mv2 is XGaFloat64HigherKVector)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is XGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddRcpTerms(this, mv).GetSimpleMultivector();
            
        return Processor.CreateComposer().AddRcpTerms(this, mv2).GetSimpleMultivector();
    }

        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64KVector mv2)
    {
        if (mv2 is not XGaFloat64Bivector bv)
            return Processor.ScalarZero;
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, bv)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
    {
        if (!mv2.TryGetKVector(2, out var kv))
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, (XGaFloat64Bivector)kv)
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64KVector mv2)
    {
        if (mv2 is not XGaFloat64Bivector bv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, bv)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
    {
        if (!mv2.TryGetKVector(2, out var kv))
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, (XGaFloat64Bivector)kv)
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