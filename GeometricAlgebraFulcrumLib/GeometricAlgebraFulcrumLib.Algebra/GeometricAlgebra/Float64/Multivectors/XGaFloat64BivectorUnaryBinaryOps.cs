using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Float64;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64Bivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator +(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator -(XGaFloat64Bivector mv1, XGaFloat64Bivector mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(IntegerSign mv1, XGaFloat64Bivector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(int mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(uint mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(long mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(ulong mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(float mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(
            mv1
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(double mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator *(XGaFloat64Scalar mv1, XGaFloat64Bivector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, int mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, uint mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, long mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, ulong mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, float mv2)
    {
        return mv1.Divide(
            mv2
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64Bivector operator /(XGaFloat64Bivector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
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


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Times(double scalarValue)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Divide(double scalarValue)
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
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Negative()
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Conjugate()
    {
        return IsZero
            ? this
            : MapScalars((basisVector, scalar) =>
                Metric.HermitianConjugateSign(basisVector) * scalar
            );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Bivector PseudoInverse()
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

            return alpha.Cos() + alpha.Sin() / alpha * this;
        }
        else
        {
            var alpha = bladeSignature.Sqrt();
            
            return alpha.Cosh() + alpha.Sinh() / alpha * this;
        }
    }
}