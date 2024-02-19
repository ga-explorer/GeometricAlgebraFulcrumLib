using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.BitManipulation;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64Vector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator -(RGaFloat64Vector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator +(RGaFloat64Vector mv1, RGaFloat64Vector mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator -(RGaFloat64Vector mv1, RGaFloat64Vector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.CreateZeroVector();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(IntegerSign mv1, RGaFloat64Vector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.CreateZeroVector();

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(int mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(uint mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(long mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(ulong mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(float mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, double mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(double mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Vector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator *(RGaFloat64Scalar mv1, RGaFloat64Vector mv2)
    {
        return mv2.Times(mv1.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64Vector operator /(RGaFloat64Vector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapScalars(Func<double, double> scalarMapping)
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
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapScalars(Func<ulong, double, double> scalarMapping)
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
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapScalars(Func<int, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Grade() == 1)
                .Select(
                    term => new KeyValuePair<ulong, double>(
                        term.Key,
                        scalarMapping(term.Key.FirstOneBitPosition(), term.Value)
                    )
                );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapBasisVectors(Func<int, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    basisMapping(term.Key.FirstOneBitPosition()).BasisVectorIndexToId(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapBasisVectors(Func<int, double, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    basisMapping(term.Key.FirstOneBitPosition(), term.Value).BasisVectorIndexToId(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector MapTerms(Func<int, double, KeyValuePair<int, double>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (index, scalar) = termMapping(term.Key.FirstOneBitPosition(), term.Value);

                    return new KeyValuePair<ulong, double>(
                        index.BasisVectorIndexToId(),
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
    public RGaFloat64Vector Negative()
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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.CreateZeroVector();

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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.CreateZeroVector();

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
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DivideByENorm()
    {
        var norm = ENorm().ScalarValue();

        if (norm.IsZero()) 
            throw new InvalidOperationException();

        return Times(1d / norm);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DivideByENormSquared()
    {
        var normSquared = ENormSquared().ScalarValue();

        if (normSquared.IsZero()) 
            throw new InvalidOperationException();

        return Times(1d / normSquared);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DivideByNorm()
    {
        var norm = Norm().ScalarValue();

        if (norm.IsZero()) 
            throw new InvalidOperationException();

        return Times(1d / norm);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector DivideByNormSquared()
    {
        var normSquared = NormSquared().ScalarValue();

        if (normSquared.IsZero()) 
            throw new InvalidOperationException();

        return Times(1d / normSquared);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Processor.ConjugateSign(basisVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Add(RGaFloat64Vector mv2)
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
    public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Vector vector)
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
    public RGaFloat64Vector Subtract(RGaFloat64Vector mv2)
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
    public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Vector vector)
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
    public RGaFloat64Vector Op(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Bivector Op(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroBivector();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Op(RGaFloat64KVector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Op(scalar);

        if (mv2 is RGaFloat64Vector vector)
            return Op(vector);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(1 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Op(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Op(scalar);

        if (mv2 is RGaFloat64Vector vector)
            return Op(vector);

        if (mv2 is RGaFloat64KVector kVector)
            return Op(kVector);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector EGp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector EGp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return EGp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Gp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Gp(RGaFloat64Multivector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Gp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ELcp(RGaFloat64Vector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector ELcp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroVector();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ELcp(RGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ELcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => ELcp(mv),
            RGaFloat64Vector mv => ELcp(mv),
            RGaFloat64Bivector mv => ELcp(mv),
            RGaFloat64HigherKVector mv => ELcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ELcp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => ELcp(mv),
            RGaFloat64Vector mv => ELcp(mv),
            RGaFloat64Bivector mv => ELcp(mv),
            RGaFloat64HigherKVector mv => ELcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Lcp(RGaFloat64Vector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Lcp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroVector();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Lcp(RGaFloat64HigherKVector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Lcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => Lcp(mv),
            RGaFloat64Vector mv => Lcp(mv),
            RGaFloat64Bivector mv => Lcp(mv),
            RGaFloat64HigherKVector mv => Lcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Lcp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => Lcp(mv),
            RGaFloat64Vector mv => Lcp(mv),
            RGaFloat64Bivector mv => Lcp(mv),
            RGaFloat64HigherKVector mv => Lcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector ERcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ERcp(RGaFloat64Vector mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ERcp(RGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar ERcp(RGaFloat64HigherKVector mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ERcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => ERcp(mv),
            RGaFloat64Vector mv => ERcp(mv),
            RGaFloat64Bivector mv => ERcp(mv),
            RGaFloat64HigherKVector mv => ERcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector ERcp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => ERcp(mv),
            RGaFloat64Vector mv => ERcp(mv),
            RGaFloat64Bivector mv => ERcp(mv),
            RGaFloat64HigherKVector mv => ERcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Vector Rcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Rcp(RGaFloat64Vector mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Rcp(RGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64Scalar Rcp(RGaFloat64HigherKVector mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Rcp(RGaFloat64KVector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => Rcp(mv),
            RGaFloat64Vector mv => Rcp(mv),
            RGaFloat64Bivector mv => Rcp(mv),
            RGaFloat64HigherKVector mv => Rcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Rcp(RGaFloat64Multivector mv2)
    {
        return mv2 switch
        {
            RGaFloat64Scalar mv => Rcp(mv),
            RGaFloat64Vector mv => Rcp(mv),
            RGaFloat64Bivector mv => Rcp(mv),
            RGaFloat64HigherKVector mv => Rcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64KVector mv2)
    {
        if (mv2 is not RGaFloat64Vector mv)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? ESp(mv)
            : Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64KVector mv2)
    {
        if (mv2 is not RGaFloat64Vector mv)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();
            
        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? Sp(mv)
            : Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

}