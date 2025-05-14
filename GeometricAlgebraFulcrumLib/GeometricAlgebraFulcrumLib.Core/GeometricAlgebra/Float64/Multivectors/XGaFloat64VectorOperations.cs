using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Vector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator -(XGaFloat64Vector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator +(XGaFloat64Vector mv1, XGaFloat64Vector mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator -(XGaFloat64Vector mv1, XGaFloat64Vector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.VectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(IntegerSign mv1, XGaFloat64Vector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.VectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(int mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(uint mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(long mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(ulong mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(float mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, double mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(double mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator *(XGaFloat64Scalar mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapScalars(Func<double, double> scalarMapping)
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
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapScalars(Func<IndexSet, double, double> scalarMapping)
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
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapScalars(Func<int, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Count == 1)
                .Select(
                    term => new KeyValuePair<IndexSet, double>(
                        term.Key,
                        scalarMapping(term.Key.FirstIndex, term.Value)
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapBasisVectors(Func<int, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    basisMapping(term.Key.FirstIndex).ToUnitIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapBasisVectors(Func<int, double, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    basisMapping(term.Key.FirstIndex, term.Value).ToUnitIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector MapTerms(Func<int, double, KeyValuePair<int, double>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (index, scalar) = termMapping(term.Key.FirstIndex, term.Value);

                    return new KeyValuePair<IndexSet, double>(
                        index.ToUnitIndexSet(),
                        scalar
                    );
                }
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Negative()
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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.VectorZero;

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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.VectorZero;

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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Metric.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Add(XGaFloat64Vector mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Vector vector)
            return Add(vector);

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
    public XGaFloat64Vector Subtract(XGaFloat64Vector mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Vector vector)
            return Subtract(vector);

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
    public XGaFloat64Vector Op(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Bivector Op(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.BivectorZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Op(XGaFloat64KVector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Op(scalar);

        if (mv2 is XGaFloat64Vector vector)
            return Op(vector);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(1 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Op(scalar);

        if (mv2 is XGaFloat64Vector vector)
            return Op(vector);

        if (mv2 is XGaFloat64KVector kVector)
            return Op(kVector);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector EGp(XGaFloat64Scalar mv2)
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
    public XGaFloat64Vector Gp(XGaFloat64Scalar mv2)
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
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ELcp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ELcp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ELcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => ELcp(mv),
            XGaFloat64Vector mv => ELcp(mv),
            XGaFloat64Bivector mv => ELcp(mv),
            XGaFloat64HigherKVector mv => ELcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ELcp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => ELcp(mv),
            XGaFloat64Vector mv => ELcp(mv),
            XGaFloat64Bivector mv => ELcp(mv),
            XGaFloat64HigherKVector mv => ELcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Vector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Lcp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Lcp(XGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Lcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => Lcp(mv),
            XGaFloat64Vector mv => Lcp(mv),
            XGaFloat64Bivector mv => Lcp(mv),
            XGaFloat64HigherKVector mv => Lcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Lcp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => Lcp(mv),
            XGaFloat64Vector mv => Lcp(mv),
            XGaFloat64Bivector mv => Lcp(mv),
            XGaFloat64HigherKVector mv => Lcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector ERcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ERcp(XGaFloat64Vector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ERcp(XGaFloat64Bivector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ERcp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ERcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => ERcp(mv),
            XGaFloat64Vector mv => ERcp(mv),
            XGaFloat64Bivector mv => ERcp(mv),
            XGaFloat64HigherKVector mv => ERcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector ERcp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => ERcp(mv),
            XGaFloat64Vector mv => ERcp(mv),
            XGaFloat64Bivector mv => ERcp(mv),
            XGaFloat64HigherKVector mv => ERcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Vector Rcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Rcp(XGaFloat64Vector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Rcp(XGaFloat64Bivector mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Rcp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Rcp(XGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => Rcp(mv),
            XGaFloat64Vector mv => Rcp(mv),
            XGaFloat64Bivector mv => Rcp(mv),
            XGaFloat64HigherKVector mv => Rcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Rcp(XGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            XGaFloat64Scalar mv => Rcp(mv),
            XGaFloat64Vector mv => Rcp(mv),
            XGaFloat64Bivector mv => Rcp(mv),
            XGaFloat64HigherKVector mv => Rcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Scalar mv2)
    {
        return Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64KVector mv2)
    {
        if (mv2 is not XGaFloat64Vector mv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? ESp(mv)
            : Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64KVector mv2)
    {
        if (mv2 is not XGaFloat64Vector mv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? Sp(mv)
            : Processor.ScalarZero;
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