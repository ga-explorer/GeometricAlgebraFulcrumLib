using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Generic.Multivectors;

public abstract partial class XGaKVector<T>
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator -(XGaKVector<T> mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(IntegerSign mv1, XGaKVector<T> mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, int mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(int mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, uint mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(uint mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, long mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(long mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, ulong mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(ulong mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, float mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(float mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, double mv2)
    {
        return mv1.Times(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(double mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(
            mv2.ScalarProcessor.ScalarFromNumber(mv1)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, T mv2)
    {
        return mv1.Times(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(T mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(Scalar<T> mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator *(XGaScalar<T> mv1, XGaKVector<T> mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, int mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, uint mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, long mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, ulong mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, float mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, double mv2)
    {
        return mv1.Divide(
            mv1.ScalarProcessor.ScalarFromNumber(mv2)
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, T mv2)
    {
        return mv1.Divide(mv2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, Scalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaKVector<T> operator /(XGaKVector<T> mv1, XGaScalar<T> mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
        
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaKVector<T> AddSameGrade(XGaKVector<T> mv2)
    {
        Debug.Assert(Grade == mv2.Grade);

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Add((XGaScalar<T>) mv2),
            XGaVector<T> mv1 => mv1.Add((XGaVector<T>) mv2),
            XGaBivector<T> mv1 => mv1.Add((XGaBivector<T>) mv2),
            XGaHigherKVector<T> mv1 => mv1.AddSameGrade((XGaHigherKVector<T>) mv2),
            _ => throw new InvalidOperationException()
        };
    }


    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Negative()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Negative(),
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Reverse()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> => this,
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> GradeInvolution()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1,
            XGaHigherKVector<T> mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> CliffordConjugate()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Negative(),
            XGaBivector<T> mv1 => mv1.Negative(),
            XGaHigherKVector<T> mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Conjugate()
    {
        return this switch
        {
            XGaScalar<T> => this,
            XGaVector<T> mv1 => mv1.Conjugate(),
            XGaBivector<T> mv1 => mv1.Conjugate(),
            XGaHigherKVector<T> mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
        

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(int scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(double scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(T scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalarValue),
            XGaVector<T> mv1 => mv1.Times(scalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
      
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(Scalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Times(IScalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Times(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Times(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(int scalar)
    {
        var scalarValue = ScalarProcessor.ScalarFromNumber(scalar).ScalarValue;

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(double scalar)
    {
        var scalarValue = ScalarProcessor.ScalarFromNumber(scalar).ScalarValue;

        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(T scalarValue)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(Scalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Divide(IScalar<T> scalar)
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaBivector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            XGaHigherKVector<T> mv1 => mv1.Divide(scalar.ScalarValue),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByENorm()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENorm(),
            XGaVector<T> mv1 => mv1.DivideByENorm(),
            XGaBivector<T> mv1 => mv1.DivideByENorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByENormSquared()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByENormSquared(),
            XGaVector<T> mv1 => mv1.DivideByENormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByENormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByNorm()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNorm(),
            XGaVector<T> mv1 => mv1.DivideByNorm(),
            XGaBivector<T> mv1 => mv1.DivideByNorm(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> DivideByNormSquared()
    {
        return this switch
        {
            XGaScalar<T> mv1 => mv1.DivideByNormSquared(),
            XGaVector<T> mv1 => mv1.DivideByNormSquared(),
            XGaBivector<T> mv1 => mv1.DivideByNormSquared(),
            XGaHigherKVector<T> mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EInverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.EInverse(),
            XGaVector<T> v => v.EInverse(),
            XGaBivector<T> bv => bv.EInverse(),
            XGaHigherKVector<T> kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Inverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.Inverse(),
            XGaVector<T> v => v.Inverse(),
            XGaBivector<T> bv => bv.Inverse(),
            XGaHigherKVector<T> kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> PseudoInverse()
    {
        return this switch
        {
            XGaScalar<T> s => s.PseudoInverse(),
            XGaVector<T> v => v.PseudoInverse(),
            XGaBivector<T> bv => bv.PseudoInverse(),
            XGaHigherKVector<T> kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EDual(XGaKVector<T> blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> Dual(XGaKVector<T> blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> EUnDual(XGaKVector<T> blade)
    {
        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaKVector<T> UnDual(XGaKVector<T> blade)
    {
        return Lcp(blade);
    }
    
}