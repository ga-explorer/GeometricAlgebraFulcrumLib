using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.IndexSets;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public sealed partial class XGaBivector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator -(XGaBivector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator +(XGaBivector<T> mv1, XGaBivector<T> mv2)
    {
        return mv1.Add(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator -(XGaBivector<T> mv1, XGaBivector<T> mv2)
    {
        return mv1.Subtract(mv2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.BivectorZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(IntegerSign mv1, XGaBivector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.BivectorZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(int mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(uint mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(long mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(ulong mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(float mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(double mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(T mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(Scalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(IScalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaBivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator *(XGaScalar<T> mv1, XGaBivector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaBivector<T> operator /(XGaBivector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Negative()
    {
        if (IsZero) return this;
            
        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Negative(term.Value).ScalarValue
                )
            );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(int scalarValue)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(double scalarValue)
    {
        return Times(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            return Processor.BivectorZero;

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Times(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(Scalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Times(IScalar<T> scalar)
    {
        return Times(scalar.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(int scalarValue)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(double scalarValue)
    {
        return Divide(ScalarProcessor.ScalarFromNumber(scalarValue).ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(T scalarValue)
    {
        if (IsZero || ScalarProcessor.IsOne(scalarValue)) return this;

        if (ScalarProcessor.IsZero(scalarValue))
            throw new DivideByZeroException();

        var termList =
            IdScalarPairs.Select(
                term => new KeyValuePair<IndexSet, T>(
                    term.Key,
                    ScalarProcessor.Divide(term.Value, scalarValue).ScalarValue
                )
            );

        return Processor
            .CreateBivectorComposer()
            .SetTerms(termList)
            .GetBivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(Scalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Divide(IScalar<T> scalar)
    {
        return Divide(scalar.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Reverse()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> GradeInvolution()
    {
        return this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> CliffordConjugate()
    {
        return Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Conjugate()
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
    public override XGaBivector<T> EInverse()
    {
        return Divide(
            ESpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> Inverse()
    {
        return Divide(
            SpSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaBivector<T> PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Add(XGaBivector<T> mv2)
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
    public XGaMultivector<T> Add(IScalar<T> mv2)
    {
        if (IsZero)
            return Processor.Scalar(mv2);

        if (mv2.IsZero())
            return this;

        return Processor
            .CreateBivectorComposer()
            .SetKVector(this)
            .AddScalarTerm(mv2.ScalarValue)
            .GetMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaMultivector<T> Add(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaBivector<T> bivector)
            return Add(bivector);

        if (IsZero)
            return mv2;

        if (mv2.IsZero)
            return this;

        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetMultivector();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaBivector<T> Subtract(XGaBivector<T> mv2)
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
    public override XGaMultivector<T> Subtract(XGaMultivector<T> mv2)
    {
        if (mv2 is XGaBivector<T> bivector)
            return Subtract(bivector);

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

    
    /// <summary>
    /// Create a pure rotor multivector from a 2-blade
    /// </summary>
    /// <returns></returns>
    public XGaMultivector<T> Exp()
    {
        var bv2 = 
            Gp(this);

        if (!bv2.IsScalar())
            throw new InvalidOperationException("Bivector is not a blade");

        var bladeSignature = bv2.Scalar();

        if (bladeSignature.IsZero())
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