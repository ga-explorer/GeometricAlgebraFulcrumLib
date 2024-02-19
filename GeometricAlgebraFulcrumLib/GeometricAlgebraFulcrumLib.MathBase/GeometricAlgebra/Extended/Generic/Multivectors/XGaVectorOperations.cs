﻿using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Processors;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Multivectors;

public sealed partial class XGaVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator -(XGaVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator +(XGaVector<T> mv1, XGaVector<T> mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator -(XGaVector<T> mv1, XGaVector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.CreateZeroVector();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(IntegerSign mv1, XGaVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.CreateZeroVector();

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(int mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(uint mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(long mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(ulong mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(float mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(double mv1, XGaVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.GetScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(T mv1, XGaVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(Scalar<T> mv1, XGaVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator *(XGaScalar<T> mv1, XGaVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.GetScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaVector<T> operator /(XGaVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> MapScalars(Func<T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
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
    public XGaFloat64Vector MapScalars(XGaFloat64Processor processor, Func<T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
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
    public XGaVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T1>(
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
    public XGaVector<T> MapScalars(Func<IIndexSet, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
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
    public XGaFloat64Vector MapScalars(XGaFloat64Processor processor, Func<IIndexSet, T, double> scalarMapping)
    {
        if (IsZero) 
            return processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
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
    public XGaVector<T1> MapScalars<T1>(XGaProcessor<T1> processor, Func<IIndexSet, T, T1> scalarMapping)
    {
        if (IsZero) 
            return processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T1>(
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
    public XGaVector<T> MapScalars(Func<int, T, T> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs
                .Where(term => term.Key.Count == 1)
                .Select(
                    term => new KeyValuePair<IIndexSet, T>(
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
    public XGaVector<T> MapBasisVectors(Func<int, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    basisMapping(term.Key.FirstIndex).IndexToIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> MapBasisVectors(Func<int, T, int> basisMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    basisMapping(term.Key.FirstIndex, term.Value).IndexToIndexSet(),
                    term.Value
                )
            );

        return Processor
            .CreateComposer()
            .AddTerms(termList)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> MapTerms(Func<int, T, KeyValuePair<int, T>> termMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term =>
                {
                    var (index, scalar) = termMapping(term.Key.FirstIndex, term.Value);

                    return new KeyValuePair<IIndexSet, T>(
                        index.IndexToIndexSet(),
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
    public XGaVector<T> Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    ScalarProcessor.Negative(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    ScalarProcessor.Times(term.Value, scalarValue)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.CreateZeroVector();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, T>(
                    term.Key,
                    ScalarProcessor.Divide(term.Value, scalarValue)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Reverse()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> GradeInvolution()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                ScalarProcessor.Times(
                    Processor.ConjugateSign(basisVector),
                    scalar
                )
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Inverse()
    {
        return Divide(
            SpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Add(XGaVector<T> mv2)
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
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaVector<T> vector)
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
    public XGaVector<T> Subtract(XGaVector<T> mv2)
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
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaVector<T> vector)
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
    public XGaVector<T> Op(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Op(XGaVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroBivector();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Op(XGaKVector<T> mv2)
    {
        if (mv2 is XGaScalar<T> scalar)
            return Op(scalar);

        if (mv2 is XGaVector<T> vector)
            return Op(vector);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(1 + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Op(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaScalar<T> scalar)
            return Op(scalar);

        if (mv2 is XGaVector<T> vector)
            return Op(vector);

        if (mv2 is XGaKVector<T> kVector)
            return Op(kVector);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> EGp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> EGp(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaScalar<T> scalar)
            return EGp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Gp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Gp(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaScalar<T> scalar)
            return Gp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ELcp(XGaScalar<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ELcp(XGaVector<T> mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ELcp(XGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroVector();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> ELcp(XGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ELcp(XGaKVector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => ELcp(mv),
            XGaVector<T> mv => ELcp(mv),
            XGaBivector<T> mv => ELcp(mv),
            XGaHigherKVector<T> mv => ELcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ELcp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => ELcp(mv),
            XGaVector<T> mv => ELcp(mv),
            XGaBivector<T> mv => ELcp(mv),
            XGaHigherKVector<T> mv => ELcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddELcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Lcp(XGaScalar<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Lcp(XGaVector<T> mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Lcp(XGaBivector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroVector();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetVector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> Lcp(XGaHigherKVector<T> mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - 1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Lcp(XGaKVector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => Lcp(mv),
            XGaVector<T> mv => Lcp(mv),
            XGaBivector<T> mv => Lcp(mv),
            XGaHigherKVector<T> mv => Lcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Lcp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => Lcp(mv),
            XGaVector<T> mv => Lcp(mv),
            XGaBivector<T> mv => Lcp(mv),
            XGaHigherKVector<T> mv => Lcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddLcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> ERcp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaVector<T> mv2)
    {
        return ESp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaBivector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> ERcp(XGaHigherKVector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> ERcp(XGaKVector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => ERcp(mv),
            XGaVector<T> mv => ERcp(mv),
            XGaBivector<T> mv => ERcp(mv),
            XGaHigherKVector<T> mv => ERcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> ERcp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => ERcp(mv),
            XGaVector<T> mv => ERcp(mv),
            XGaBivector<T> mv => ERcp(mv),
            XGaHigherKVector<T> mv => ERcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddERcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaVector<T> Rcp(XGaScalar<T> mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaVector<T> mv2)
    {
        return Sp(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaBivector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaScalar<T> Rcp(XGaHigherKVector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Rcp(XGaKVector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => Rcp(mv),
            XGaVector<T> mv => Rcp(mv),
            XGaBivector<T> mv => Rcp(mv),
            XGaHigherKVector<T> mv => Rcp(mv),
            _ => throw new NotImplementedException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Rcp(XGaMultivector<T> mv2)
    {
        return mv2 switch
        {
            XGaScalar<T> mv => Rcp(mv),
            XGaVector<T> mv => Rcp(mv),
            XGaBivector<T> mv => Rcp(mv),
            XGaHigherKVector<T> mv => Rcp(mv),
            _ => IsZero || mv2.IsZero
                ? Processor.CreateZeroScalar()
                : Processor
                    .CreateComposer()
                    .AddRcpTerms(this, mv2)
                    .GetSimpleMultivector()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaScalar<T> mv2)
    {
        return Processor.CreateZeroScalar();
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
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaHigherKVector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaKVector<T> mv2)
    {
        if (mv2 is not XGaVector<T> mv)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return ScalarProcessor
            .CreateScalarComposer()
            .AddESpTerms(this, mv)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> ESp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? ESp(mv)
            : Processor.CreateZeroScalar();
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
        return Processor.CreateZeroScalar();
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
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaHigherKVector<T> mv2)
    {
        return Processor.CreateZeroScalar();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaKVector<T> mv2)
    {
        if (mv2 is not XGaVector<T> mv)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();
            
        return ScalarProcessor
            .CreateScalarComposer()
            .AddSpTerms(this, mv)
            .GetXGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaScalar<T> Sp(XGaGradedMultivector<T> mv2)
    {
        return mv2.TryGetKVector(1, out var mv)
            ? Sp(mv)
            : Processor.CreateZeroScalar();
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