using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors.Composers;
using GeometricAlgebraFulcrumLib.Core.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Core.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

public sealed partial class RGaFloat64HigherKVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator -(RGaFloat64HigherKVector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.HigherKVectorZero(mv1.Grade);

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(IntegerSign mv1, RGaFloat64HigherKVector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.HigherKVectorZero(mv2.Grade);

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(int mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(uint mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(long mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(ulong mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Times(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(float mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            (mv1)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(double mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64HigherKVector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator *(RGaFloat64Scalar mv1, RGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Divide(
            (mv2)
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RGaFloat64HigherKVector operator /(RGaFloat64HigherKVector mv1, RGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector MapScalars(Func<double, double> scalarMapping)
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
            .GetHigherKVector(Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector MapScalars(Func<ulong, double, double> scalarMapping)
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
            .GetHigherKVector(Grade);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Negative()
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
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.HigherKVectorZero(Grade);

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
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.HigherKVectorZero(Grade);

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
            .GetHigherKVector(Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Reverse()
    {
        return IsZero || Grade.ReverseIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector GradeInvolution()
    {
        return IsZero || Grade.GradeInvolutionIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector CliffordConjugate()
    {
        return IsZero || Grade.CliffordConjugateIsPositiveOfGrade()
            ? this
            : Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisKVector, scalar) =>
                Processor.HermitianConjugateSign(basisKVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Multivector Add(RGaFloat64Multivector mv2)
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
    public override RGaFloat64Multivector Subtract(RGaFloat64Multivector mv2)
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
    public RGaFloat64HigherKVector Op(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Op(RGaFloat64KVector mv2)
    {
        if (mv2 is RGaFloat64Scalar scalar)
            return Times(scalar.ScalarValue);

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetKVector(Grade + mv2.Grade);
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
                .GetKVector(Grade + kVector.Grade);
            
        return Processor
            .CreateComposer()
            .AddOpTerms(this, mv2)
            .GetSimpleMultivector();
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector EGp(RGaFloat64Scalar mv2)
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
    public RGaFloat64HigherKVector Gp(RGaFloat64Scalar mv2)
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ELcp(RGaFloat64HigherKVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ELcp(RGaFloat64KVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddELcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
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
    public RGaFloat64Multivector ELcp(RGaFloat64UniformMultivector mv2)
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
        return mv2 switch
        {
            RGaFloat64Scalar mv => ELcp(mv),
            RGaFloat64Vector mv => ELcp(mv),
            RGaFloat64Bivector mv => ELcp(mv),
            RGaFloat64HigherKVector mv => ELcp(mv),
            RGaFloat64GradedMultivector mv => ELcp(mv),
            RGaFloat64UniformMultivector mv => ELcp(mv),
            _ => throw new InvalidOperationException()
        };
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Lcp(RGaFloat64HigherKVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Lcp(RGaFloat64KVector mv2)
    {
        if (Grade > mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddLcpTerms(this, mv2)
            .GetKVector(mv2.Grade - Grade);
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
    public RGaFloat64Multivector Lcp(RGaFloat64UniformMultivector mv2)
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
        return mv2 switch
        {
            RGaFloat64Scalar mv => Lcp(mv),
            RGaFloat64Vector mv => Lcp(mv),
            RGaFloat64Bivector mv => Lcp(mv),
            RGaFloat64HigherKVector mv => Lcp(mv),
            RGaFloat64GradedMultivector mv => Lcp(mv),
            RGaFloat64UniformMultivector mv => Lcp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector ERcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ERcp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ERcp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector ERcp(RGaFloat64HigherKVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector ERcp(RGaFloat64KVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddERcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
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
    public RGaFloat64Multivector ERcp(RGaFloat64UniformMultivector mv2)
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
        return mv2 switch
        {
            RGaFloat64Scalar mv => ERcp(mv),
            RGaFloat64Vector mv => ERcp(mv),
            RGaFloat64Bivector mv => ERcp(mv),
            RGaFloat64HigherKVector mv => ERcp(mv),
            RGaFloat64GradedMultivector mv => ERcp(mv),
            RGaFloat64UniformMultivector mv => ERcp(mv),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64HigherKVector Rcp(RGaFloat64Scalar mv2)
    {
        return Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Rcp(RGaFloat64Vector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - 1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Rcp(RGaFloat64Bivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - 2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RGaFloat64KVector Rcp(RGaFloat64HigherKVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64KVector Rcp(RGaFloat64KVector mv2)
    {
        if (Grade < mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Processor
            .CreateComposer()
            .AddRcpTerms(this, mv2)
            .GetKVector(Grade - mv2.Grade);
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
    public RGaFloat64Multivector Rcp(RGaFloat64UniformMultivector mv2)
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
        return mv2 switch
        {
            RGaFloat64Scalar mv => Rcp(mv),
            RGaFloat64Vector mv => Rcp(mv),
            RGaFloat64Bivector mv => Rcp(mv),
            RGaFloat64HigherKVector mv => Rcp(mv),
            RGaFloat64GradedMultivector mv => Rcp(mv),
            RGaFloat64UniformMultivector mv => Rcp(mv),
            _ => throw new InvalidOperationException()
        };
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64HigherKVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64KVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar ESp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddESpTerms(this, mv2)
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
        return Processor.ScalarZero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64HigherKVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64KVector mv2)
    {
        if (Grade != mv2.Grade)
            return Processor.ScalarZero;

        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
            .GetRGaScalar(Processor);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override RGaFloat64Scalar Sp(RGaFloat64GradedMultivector mv2)
    {
        if (IsZero || mv2.IsZero)
            return Processor.ScalarZero;

        return Float64ScalarComposer
            .Create()
            .AddSpTerms(this, mv2)
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