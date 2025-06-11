using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Matlab.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Matlab.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector
{
    
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1)
    {
        return mv1.Negative();
    }


    
    public static XGaFloat64Bivector operator +(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Add(mv2);
    }

    
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64Bivector operator *(IntegerSign mv1, XGaFloat64Bivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator *(int mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator *(uint mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator *(long mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator *(ulong mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator *(float mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }
        
    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    
    public static XGaFloat64Bivector operator *(double mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    
    public static XGaFloat64Bivector operator *(XGaFloat64Scalar mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Divide(
            mv2
        );
    }
        
    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        

    
    public XGaFloat64Bivector Add(XGaFloat64Bivector mv2)
    {
        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .AddKVector(mv2)
            .GetBivector();
    }
        
    
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Bivector bivector)
            return Add(bivector);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetKVector(this)
            .AddMultivector(mv2)
            .GetSimpleMultivector();
    }

        
    
    public XGaFloat64Bivector Subtract(XGaFloat64Bivector mv2)
    {
        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .SubtractKVector(mv2)
            .GetBivector();
    }
        
    
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        if (mv2 is XGaFloat64Bivector bivector)
            return Subtract(bivector);

        if (IsZero)
            return mv2.Negative();

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetSimpleMultivector();
    }


    
    public new XGaFloat64Bivector Times(double scalarValue)
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    
    public new XGaFloat64Bivector Divide(double scalarValue)
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    
    public new XGaFloat64Bivector DivideByENorm()
    {
        return Divide(ENorm());
    }
        
    
    public new XGaFloat64Bivector DivideByENormSquared()
    {
        return Divide(ENormSquared());
    }
        
    
    public new XGaFloat64Bivector DivideByNorm()
    {
        return Divide(Norm());
    }
        
    
    public new XGaFloat64Bivector DivideByNormSquared()
    {
        return Divide(NormSquared());
    }

    
    
    public new XGaFloat64Bivector Negative()
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
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    
    public new XGaFloat64Bivector Reverse()
    {
        return Negative();
    }

    
    public new XGaFloat64Bivector GradeInvolution()
    {
        return this;
    }

    
    public new XGaFloat64Bivector CliffordConjugate()
    {
        return Negative();
    }

    
    public new XGaFloat64Bivector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Metric.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    
    public new XGaFloat64Bivector EInverse()
    {
        return Divide(
            ESpSquared()
        );
    }

    
    public new XGaFloat64Bivector Inverse()
    {
        return Divide(
            SpSquared()
        );
    }

    
    public new XGaFloat64Bivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }


    /// <summary>
    /// Create a pure rotor multivector from a 2-blade
    /// </summary>
    /// <param name="zeroEpsilon"></param>
    /// <returns></returns>
    public XGaFloat64Multivector Exp(double zeroEpsilon = Float64Utils.ZeroEpsilon)
    {
        var bv2 = 
            Gp(this).RemoveSmallTerms(zeroEpsilon);

        if (!bv2.IsScalar())
            throw new InvalidOperationException("Bivector is not a blade");

        var bladeSignature = bv2.Scalar();

        if (bladeSignature.IsNearZero(zeroEpsilon))
            return 1d + this;

        if (bladeSignature < 0)
        {
            var alpha = (-bladeSignature).Sqrt();

            return alpha.Cos() + (alpha.Sin() / alpha) * this;
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            
            return alpha.Cosh() + (alpha.Sinh() / alpha) * this;
        }
    }

}