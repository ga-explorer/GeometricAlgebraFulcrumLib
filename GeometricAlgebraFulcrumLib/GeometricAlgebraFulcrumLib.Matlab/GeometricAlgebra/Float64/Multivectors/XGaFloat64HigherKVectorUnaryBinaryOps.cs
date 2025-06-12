using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64HigherKVector
{
    
    public static XGaFloat64HigherKVector operator -(XGaFloat64HigherKVector mv1)
    {
        return mv1.Negative();
    }


    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.HigherKVectorZero(mv1.Grade);

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64HigherKVector operator *(IntegerSign mv1, XGaFloat64HigherKVector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.HigherKVectorZero(mv2.Grade);

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator *(int mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator *(uint mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator *(long mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator *(ulong mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator *(float mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(
            mv1
        );
    }
        
    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    
    public static XGaFloat64HigherKVector operator *(double mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1);
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64HigherKVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    
    public static XGaFloat64HigherKVector operator *(XGaFloat64Scalar mv1, XGaFloat64HigherKVector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, int mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, uint mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, long mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, ulong mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, float mv2)
    {
        return mv1.Divide(
            mv2
        );
    }
        
    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    
    public static XGaFloat64HigherKVector operator /(XGaFloat64HigherKVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    
    
    public XGaFloat64HigherKVector AddSameGrade(XGaFloat64HigherKVector mv2)
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

    
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64HigherKVector mv && mv.Grade == Grade)
            return AddSameGrade(mv);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetKVector(this)
            .AddMultivector(mv2)
            .GetMultivector();
    }
    
    
    public XGaFloat64HigherKVector SubtractSameGrade(XGaFloat64HigherKVector mv2)
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

    
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64HigherKVector mv && mv.Grade == Grade)
            return SubtractSameGrade(mv);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetKVector(this)
            .SubtractMultivector(mv2)
            .GetMultivector();
    }


    
    public new XGaFloat64HigherKVector Times(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            return Processor.HigherKVectorZero(Grade);

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    term.Value * scalarValue
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    
    public new XGaFloat64HigherKVector Divide(double scalarValue)
    {
        if (IsZero || scalarValue.IsOne()) return this;

        if (scalarValue.IsZero())
            throw new DivideByZeroException();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    term.Value / scalarValue
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }
        
    
    public new XGaFloat64HigherKVector DivideByENorm()
    {
        return Divide(ENorm());
    }
        
    
    public new XGaFloat64HigherKVector DivideByENormSquared()
    {
        return Divide(ENormSquared());
    }
        
    
    public new XGaFloat64HigherKVector DivideByNorm()
    {
        return Divide(Norm());
    }
        
    
    public new XGaFloat64HigherKVector DivideByNormSquared()
    {
        return Divide(NormSquared());
    }
    

    
    public new XGaFloat64HigherKVector Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, double>(
                    term.Key,
                    -term.Value
                )
            );

        return Processor
            .CreateKVectorComposer(Grade)
            .SetTerms(termList)
            .GetHigherKVector();
    }

    
    public new XGaFloat64HigherKVector Reverse()
    {
        return IsZero || Grade.ReverseIsPositiveOfGrade()
            ? this
            : Negative();
    }

    
    public new XGaFloat64HigherKVector GradeInvolution()
    {
        return IsZero || Grade.GradeInvolutionIsPositiveOfGrade()
            ? this
            : Negative();
    }

    
    public new XGaFloat64HigherKVector CliffordConjugate()
    {
        return IsZero || Grade.CliffordConjugateIsPositiveOfGrade()
            ? this
            : Negative();
    }

    
    public new XGaFloat64HigherKVector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisKVector, scalar) =>
                Metric.HermitianConjugateSign(basisKVector) * scalar
            );
    }
        
    
    public new XGaFloat64HigherKVector EInverse()
    {
        return Divide(
            ESpSquared()
        );
    }

    
    public new XGaFloat64HigherKVector Inverse()
    {
        return Divide(
            SpSquared()
        );
    }

    
    public new XGaFloat64HigherKVector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
}