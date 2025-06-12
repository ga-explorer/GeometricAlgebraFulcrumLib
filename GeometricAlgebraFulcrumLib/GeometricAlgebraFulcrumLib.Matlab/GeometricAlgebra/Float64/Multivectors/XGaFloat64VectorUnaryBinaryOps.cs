using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Vector
{
    
    public static XGaFloat64Vector operator -(XGaFloat64Vector mv1)
    {
        return mv1.Negative();
    }


    
    public static XGaFloat64Vector operator +(XGaFloat64Vector mv1, XGaFloat64Vector mv2)
    {
        return mv1.Add(mv2);
    }

    
    public static XGaFloat64Vector operator -(XGaFloat64Vector mv1, XGaFloat64Vector mv2)
    {
        return mv1.Subtract(mv2);
    }


    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.VectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64Vector operator *(IntegerSign mv1, XGaFloat64Vector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.VectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, int mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator *(int mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator *(uint mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, long mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator *(long mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator *(ulong mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, float mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator *(float mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, double mv2)
    {
        return mv1.Times(
            mv2
        );
    }
        
    
    public static XGaFloat64Vector operator *(double mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(mv1);
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Vector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    
    public static XGaFloat64Vector operator *(XGaFloat64Scalar mv1, XGaFloat64Vector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, int mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, uint mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, long mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, ulong mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, float mv2)
    {
        return mv1.Divide(
            mv2
        );
    }
        
    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    
    public static XGaFloat64Vector operator /(XGaFloat64Vector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    
    
    public XGaFloat64Vector Add(XGaFloat64Vector mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateVectorComposer()
            .SetKVector(this)
            .AddKVector(mv2)
            .GetVector();
    }
        
    
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Vector vector)
            return Add(vector);

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


    
    public XGaFloat64Vector Subtract(XGaFloat64Vector mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateVectorComposer()
            .SetKVector(this)
            .SubtractKVector(mv2)
            .GetVector();
    }
        
    
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Vector vector)
            return Subtract(vector);

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


    
    public new XGaFloat64Vector Times(double scalarValue)
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
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    
    public new XGaFloat64Vector Divide(double scalarValue)
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
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }
        
    
    public new XGaFloat64Vector DivideByENorm()
    {
        return Divide(ENorm());
    }
        
    
    public new XGaFloat64Vector DivideByENormSquared()
    {
        return Divide(ENormSquared());
    }
        
    
    public new XGaFloat64Vector DivideByNorm()
    {
        return Divide(Norm());
    }
        
    
    public new XGaFloat64Vector DivideByNormSquared()
    {
        return Divide(NormSquared());
    }

    
    
    public new XGaFloat64Vector Negative()
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
            .CreateVectorComposer()
            .SetTerms(termList)
            .GetVector();
    }

    
    public new XGaFloat64Vector Reverse()
    {
        return this;
    }

    
    public new XGaFloat64Vector GradeInvolution()
    {
        return Negative();
    }

    
    public new XGaFloat64Vector CliffordConjugate()
    {
        return Negative();
    }

    
    public new XGaFloat64Vector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Metric.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    
    public new XGaFloat64Vector EInverse()
    {
        return Divide(
            ESpSquared()
        );
    }

    
    public new XGaFloat64Vector Inverse()
    {
        return Divide(
            SpSquared()
        );
    }

    
    public new XGaFloat64Vector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }


}