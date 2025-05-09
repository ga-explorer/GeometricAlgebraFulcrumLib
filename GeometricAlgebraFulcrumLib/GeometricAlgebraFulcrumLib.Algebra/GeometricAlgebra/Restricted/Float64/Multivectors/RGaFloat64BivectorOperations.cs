using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64Bivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator -(RGaFloat64Bivector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator +(RGaFloat64Bivector mv1, RGaFloat64Bivector mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator -(RGaFloat64Bivector mv1, RGaFloat64Bivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(IntegerSign mv1, RGaFloat64Bivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(int mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(uint mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(long mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(ulong mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(float mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(double mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Bivector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator *(RGaFloat64Scalar mv1, RGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Bivector operator /(RGaFloat64Bivector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<RGaFloat64Vector> GetVectorBasis()
    {
        var closestVector = Processor.VectorTerm(0);

        return GetVectorBasis(closestVector);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<RGaFloat64Vector> GetVectorBasis(int closestBasisVectorIndex)
    {
        var closestVector = Processor.VectorTerm(closestBasisVectorIndex);

        return GetVectorBasis(closestVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Pair<RGaFloat64Vector> GetVectorBasis(RGaFloat64Vector closestVector)
    {
        var e1 = closestVector.Lcp(this).DivideByNorm();
        var e2 = e1.Lcp(this);

        Debug.Assert((e1.Op(e2) - this).IsNearZero());

        return new Pair<RGaFloat64Vector>(e1, e2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector MapScalars(Func<double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaFloat64Bivector MapScalars(Func<ulong, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaFloat64Bivector MapScalars(Func<int, int, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Grade() == 1)
                .Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition(), term.Value)
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector MapBasisBivectors(Func<int, int, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    basisMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition()).IndexPairToBivectorId(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector MapBasisBivectors(Func<int, int, double, IPair<int>> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    basisMapping(term.Key.FirstOneBitPosition(), term.Key.LastOneBitPosition(), term.Value).IndexPairToBivectorId(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector MapTerms(Func<int, int, double, KeyValuePair<IPair<int>, double>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (indexPair, scalar) = termMapping(
                        term.Key.FirstOneBitPosition(), 
                        term.Key.LastOneBitPosition(), 
                        term.Value
                    );

                    return new KeyValuePair<ulong, double>(
                        indexPair.IndexPairToBivectorId(),
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
    public RGaFloat64Bivector Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaFloat64Bivector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaFloat64Bivector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
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
    public RGaFloat64Bivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Processor.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Add(RGaFloat64Bivector mv2)
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
    public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Bivector bivector)
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
    public RGaFloat64Bivector Subtract(RGaFloat64Bivector mv2)
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
    public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Bivector bivector)
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
    public RGaFloat64Bivector Op(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Op(RGaFloat64KVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(2 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Op(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64KVector kVector)
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
    public RGaFloat64Bivector EGp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector EGp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return EGp(scalar);
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Gp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Gp(scalar);
            
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Bivector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ELcp(RGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ELcp(RGaFloat64KVector mv2)
    {
        if (mv2 is RGaFloat64Scalar or RGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector ELcp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar or RGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is RGaFloat64HigherKVector kv)
            return Processor.CreateComposer().AddELcpTerms(this, mv2).GetKVector(kv.Grade - 2);

        if (mv2 is RGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddELcpTerms(this, mv).GetSimpleMultivector();

        return Processor.CreateComposer().AddELcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Bivector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Lcp(RGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Lcp(RGaFloat64KVector mv2)
    {
        if (mv2 is RGaFloat64Scalar or RGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(mv2.Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Lcp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar or RGaFloat64Vector)
            return Processor.ScalarZero;

        if (mv2 is RGaFloat64Bivector bv)
            return ESp(bv);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is RGaFloat64HigherKVector kv)
            return Processor.CreateComposer().AddLcpTerms(this, mv2).GetKVector(kv.Grade - 2);
            
        if (mv2 is RGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddLcpTerms(this, mv).GetSimpleMultivector();
            
        return Processor.CreateComposer().AddLcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector ERcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector ERcp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;
            
        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ERcp(RGaFloat64Bivector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ERcp(RGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ERcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar s => ERcp(s),
            RGaFloat64Vector v => ERcp(v),
            RGaFloat64Bivector bv => ESp(bv),
            _ => Processor.ScalarZero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector ERcp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar s)
            return ERcp(s);

        if (mv2 is RGaFloat64Vector v)
            return ERcp(v);

        if (mv2 is RGaFloat64Bivector bv)
            return ESp(bv);

        if (mv2 is RGaFloat64HigherKVector)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is RGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddERcpTerms(this, mv).GetSimpleMultivector();

        return Processor.CreateComposer().AddERcpTerms(this, mv2).GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Rcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Rcp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;
            
        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Rcp(RGaFloat64Bivector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Rcp(RGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Rcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar s => Rcp(s),
            RGaFloat64Vector v => Rcp(v),
            RGaFloat64Bivector bv => Sp(bv),
            _ => Processor.ScalarZero
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Multivector Rcp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar s)
            return Rcp(s);

        if (mv2 is RGaFloat64Vector v)
            return Rcp(v);

        if (mv2 is RGaFloat64Bivector bv)
            return Sp(bv);

        if (mv2 is RGaFloat64HigherKVector)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        if (mv2 is RGaFloat64GradedMultivector mv)
            return Processor.CreateComposer().AddRcpTerms(this, mv).GetSimpleMultivector();
            
        return Processor.CreateComposer().AddRcpTerms(this, mv2).GetSimpleMultivector();
    }

        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64KVector mv2)
    {
        if (mv2 is not RGaFloat64Bivector bv)
            return Processor.ScalarZero;
            
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, bv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
    {
        if (!mv2.TryGetKVector(2, out var kv))
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, (RGaFloat64Bivector)kv)
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Vector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64KVector mv2)
    {
        if (mv2 is not RGaFloat64Bivector bv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, bv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
    {
        if (!mv2.TryGetKVector(2, out var kv))
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, (RGaFloat64Bivector)kv)
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