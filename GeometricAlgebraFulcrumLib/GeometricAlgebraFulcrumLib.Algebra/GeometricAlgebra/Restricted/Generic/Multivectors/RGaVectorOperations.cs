using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;
using GeometricAlgebraFulcrumLib.Utilities.Structures.BitManipulation;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Processors;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Generic.Multivectors;

public sealed partial class RGaVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator -(RGaVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator +(RGaVector<T> mv1, RGaVector<T> mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator -(RGaVector<T> mv1, RGaVector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.VectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(IntegerSign mv1, RGaVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.VectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(int mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(uint mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(long mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(ulong mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(float mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(double mv1, RGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ValueFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(T mv1, RGaVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(Scalar<T> mv1, RGaVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaVector<T> mv1, RGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator *(RGaScalar<T> mv1, RGaVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ValueFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaVector<T> operator /(RGaVector<T> mv1, RGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaFloat64Vector MapScalars(RGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T1>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> MapScalars(Func<ulong, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaFloat64Vector MapScalars(RGaFloat64Processor processor, Func<ulong, T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T1> MapScalars<T1>(RGaProcessor<T1> processor, Func<ulong, T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T1>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> MapScalars(Func<int, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Grade() == 1)
                .Select(
                    term => new KeyValuePair<ulong, T>(
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
    public RGaVector<T> MapBasisVectors(Func<int, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaVector<T> MapBasisVectors(Func<int, T, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
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
    public RGaVector<T> MapTerms(Func<int, T, KeyValuePair<int, T>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (index, scalar) = termMapping(term.Key.FirstOneBitPosition(), term.Value);

                    return new KeyValuePair<ulong, T>(
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
    public RGaVector<T> Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    ScalarProcessor.Negative(term.Value).ScalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    ScalarProcessor.Times(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.VectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<ulong, T>(
                    term.Key,
                    ScalarProcessor.Divide(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Divide(Scalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Conjugate()
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
    public RGaVector<T> EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Add(RGaVector<T> mv2)
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
    public override RGaMultivector<T> Add(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaVector<T> vector)
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
    public RGaVector<T> Subtract(RGaVector<T> mv2)
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
    public override RGaMultivector<T> Subtract(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaVector<T> vector)
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
    public RGaVector<T> Op(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaBivector<T> Op(RGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.BivectorZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> Op(RGaKVector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Op(scalar);

        if (mv2 is RGaVector<T> vector)
            return Op(vector);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(1 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Op(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Op(scalar);

        if (mv2 is RGaVector<T> vector)
            return Op(vector);

        if (mv2 is RGaKVector<T> kVector)
            return Op(kVector);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> EGp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> EGp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return EGp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Gp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Gp(RGaMultivector<T> mv2)
    {
        if (mv2 is RGaScalar<T> scalar)
            return Gp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ELcp(RGaScalar<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ELcp(RGaVector<T> mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> ELcp(RGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> ELcp(RGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> ELcp(RGaKVector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => ELcp(mv),
            RGaVector<T> mv => ELcp(mv),
            RGaBivector<T> mv => ELcp(mv),
            RGaHigherKVector<T> mv => ELcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ELcp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => ELcp(mv),
            RGaVector<T> mv => ELcp(mv),
            RGaBivector<T> mv => ELcp(mv),
            RGaHigherKVector<T> mv => ELcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Lcp(RGaScalar<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Lcp(RGaVector<T> mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Lcp(RGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.VectorZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaKVector<T> Lcp(RGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> Lcp(RGaKVector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => Lcp(mv),
            RGaVector<T> mv => Lcp(mv),
            RGaBivector<T> mv => Lcp(mv),
            RGaHigherKVector<T> mv => Lcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Lcp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => Lcp(mv),
            RGaVector<T> mv => Lcp(mv),
            RGaBivector<T> mv => Lcp(mv),
            RGaHigherKVector<T> mv => Lcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> ERcp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ERcp(RGaVector<T> mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ERcp(RGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> ERcp(RGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> ERcp(RGaKVector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => ERcp(mv),
            RGaVector<T> mv => ERcp(mv),
            RGaBivector<T> mv => ERcp(mv),
            RGaHigherKVector<T> mv => ERcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> ERcp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => ERcp(mv),
            RGaVector<T> mv => ERcp(mv),
            RGaBivector<T> mv => ERcp(mv),
            RGaHigherKVector<T> mv => ERcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaVector<T> Rcp(RGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Rcp(RGaVector<T> mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Rcp(RGaBivector<T> mv2)
    {
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaScalar<T> Rcp(RGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaKVector<T> Rcp(RGaKVector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => Rcp(mv),
            RGaVector<T> mv => Rcp(mv),
            RGaBivector<T> mv => Rcp(mv),
            RGaHigherKVector<T> mv => Rcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaMultivector<T> Rcp(RGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            RGaScalar<T> mv => Rcp(mv),
            RGaVector<T> mv => Rcp(mv),
            RGaBivector<T> mv => Rcp(mv),
            RGaHigherKVector<T> mv => Rcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.ScalarZero
                : Processor
                    .CreateComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaScalar<T> mv2)
    {
        return Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaKVector<T> mv2)
    {
        if (mv2 is not RGaVector<T> mv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> ESp(RGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? ESp(mv)
            : Processor.ScalarZero;
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
        return Processor.ScalarZero;
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaHigherKVector<T> mv2)
    {
        return Processor.ScalarZero;
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaKVector<T> mv2)
    {
        if (mv2 is not RGaVector<T> mv)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaScalar<T> Sp(RGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? Sp(mv)
            : Processor.ScalarZero;
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