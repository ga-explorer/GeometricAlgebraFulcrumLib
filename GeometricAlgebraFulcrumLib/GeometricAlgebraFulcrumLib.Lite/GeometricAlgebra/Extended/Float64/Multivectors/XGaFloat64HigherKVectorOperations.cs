﻿using System.Runtime.CompilerServices;
using DataStructuresLib.Basic;
using DataStructuresLib.IndexSets;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Extended.Float64.Multivectors;

public sealed partial class XGaFloat64HigherKVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator -(XGaFloat64HigherKVector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.CreateZeroHigherKVector(mv1.Grade);

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(IntegerSign mv1, XGaFloat64HigherKVector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.CreateZeroHigherKVector(mv2.Grade);

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(int mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(uint mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(long mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(ulong mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(float mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(double mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator *(XGaFloat64Scalar mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue());
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector MapScalars(Func<double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    scalarMapping(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetHigherKVector(Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector MapScalars(Func<IIndexSet, double, double> scalarMapping)
    {
        if (IsZero) return this;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    scalarMapping(term.Key, term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetHigherKVector(Grade);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    -(term.Value)
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.CreateZeroHigherKVector(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    term.Value * scalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.CreateZeroHigherKVector(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IIndexSet, double>(
                    term.Key,
                    term.Value / scalarValue
                )
            );

        return Processor
            .CreateComposer()
            .SetTerms(termList)
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector DivideByNorm()
    {
        return Divide(Norm().ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Reverse()
    {
        return IsZero || Grade.ReverseIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector GradeInvolution()
    {
        return IsZero || Grade.GradeInvolutionIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector CliffordConjugate()
    {
        return IsZero || Grade.CliffordConjugateIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisKVector, scalar) =>
                Metric.ConjugateSign(basisKVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue()
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue()
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
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
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
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
    public XGaFloat64HigherKVector Op(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Op(XGaFloat64KVector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue());

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(Grade + mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Op(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue());

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        if (mv2 is XGaFloat64KVector kVector)
            return Processor
                .CreateComposer()
                .AddOpTerms(this, mv2)
                .GetKVector(Grade + kVector.Grade);
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector EGp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EGp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return EGp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddEGpTerms(this, mv2)
            .GetSimpleMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Gp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Gp(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Scalar scalar)
            return Gp(scalar);

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddGpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Vector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar ELcp(XGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ELcp(XGaFloat64HigherKVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ELcp(XGaFloat64KVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ELcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ELcp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetSimpleMultivector();
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
            XGaFloat64GradedMultivector mv => ELcp(mv),
            XGaFloat64UniformMultivector mv => ELcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Vector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Scalar Lcp(XGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Lcp(XGaFloat64HigherKVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Lcp(XGaFloat64KVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Lcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Lcp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetSimpleMultivector();
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
            XGaFloat64GradedMultivector mv => Lcp(mv),
            XGaFloat64UniformMultivector mv => Lcp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector ERcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ERcp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ERcp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector ERcp(XGaFloat64HigherKVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector ERcp(XGaFloat64KVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ERcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector ERcp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetSimpleMultivector();
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
            XGaFloat64GradedMultivector mv => ERcp(mv),
            XGaFloat64UniformMultivector mv => ERcp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64HigherKVector Rcp(XGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Rcp(XGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Rcp(XGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector Rcp(XGaFloat64HigherKVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Rcp(XGaFloat64KVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Rcp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64Multivector Rcp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetSimpleMultivector();
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
            XGaFloat64GradedMultivector mv => Rcp(mv),
            XGaFloat64UniformMultivector mv => Rcp(mv),
            _ => throw new InvalidOperationException()
        };
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Vector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64HigherKVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64KVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar ESp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Scalar mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Vector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64Bivector mv2)
    {
        return Processor.CreateZeroScalar();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64HigherKVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64KVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.CreateZeroScalar();

        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Scalar Sp(XGaFloat64UniformMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.CreateZeroScalar();

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetXGaFloat64Scalar(Processor);
    }
}