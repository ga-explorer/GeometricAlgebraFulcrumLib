using System.Diagnostics;
using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;
using GeometricAlgebraFulcrumLib.Utilities.Structures.Tuples;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public abstract partial class XGaFloat64KVector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator -(XGaFloat64KVector mv1)
    {
        return mv1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            return mv1.Processor.ScalarZero;

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(IntegerSign mv1, XGaFloat64KVector mv2)
    {
        if (mv1.IsZero)
            return mv2.Processor.ScalarZero;

        return mv1.IsPositive ? mv2 : mv2.Negative().GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, int mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(int mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            mv1
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, uint mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(uint mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            mv1
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, long mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(long mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            mv1
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, ulong mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(ulong mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            mv1
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, float mv2)
    {
        return mv1.Times(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(float mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(
            mv1
        ).GetKVectorPart(mv2.Grade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, double mv2)
    {
        return mv1.Times(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(double mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(mv1);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64KVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Times(mv2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator *(XGaFloat64Scalar mv1, XGaFloat64KVector mv2)
    {
        return mv2.Times(mv1.ScalarValue);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, IntegerSign mv2)
    {
        if (mv2.IsZero)
            throw new DivideByZeroException();

        return mv2.IsPositive ? mv1 : mv1.Negative();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, int mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, uint mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, long mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, ulong mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, float mv2)
    {
        return mv1.Divide(
            mv2
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, double mv2)
    {
        return mv1.Divide(mv2);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64KVector operator /(XGaFloat64KVector mv1, XGaFloat64Scalar mv2)
    {
        return mv1.Divide(mv2.ScalarValue);
    }
    

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Negative()
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.Negative(),
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Negative(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Reverse()
    {
        return this switch
        {
            XGaFloat64Scalar => this,
            XGaFloat64Vector => this,
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.ReverseIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector GradeInvolution()
    {
        return this switch
        {
            XGaFloat64Scalar => this,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1,
            XGaFloat64HigherKVector mv1 => mv1.Grade.GradeInvolutionIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector CliffordConjugate()
    {
        return this switch
        {
            XGaFloat64Scalar => this,
            XGaFloat64Vector mv1 => mv1.Negative(),
            XGaFloat64Bivector mv1 => mv1.Negative(),
            XGaFloat64HigherKVector mv1 => mv1.Grade.CliffordConjugateIsNegativeOfGrade() ? mv1.Negative() : mv1,
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Conjugate()
    {
        return this switch
        {
            XGaFloat64Scalar => this,
            XGaFloat64Vector mv1 => mv1.Conjugate(),
            XGaFloat64Bivector mv1 => mv1.Conjugate(),
            XGaFloat64HigherKVector mv1 => mv1.Conjugate(),
            _ => throw new InvalidOperationException()
        };
    }
    
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public XGaFloat64KVector AddSameGrade(XGaFloat64KVector mv2)
    {
        Debug.Assert(Grade == mv2.Grade);

        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.Add((XGaFloat64Scalar) mv2),
            XGaFloat64Vector mv1 => mv1.Add((XGaFloat64Vector) mv2),
            XGaFloat64Bivector mv1 => mv1.Add((XGaFloat64Bivector) mv2),
            XGaFloat64HigherKVector mv1 => mv1.AddSameGrade((XGaFloat64HigherKVector) mv2),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Times(double scalarValue)
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.Times(scalarValue),
            XGaFloat64Vector mv1 => mv1.Times(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Times(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Times(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Divide(double scalarValue)
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.Divide(scalarValue),
            XGaFloat64Vector mv1 => mv1.Divide(scalarValue),
            XGaFloat64Bivector mv1 => mv1.Divide(scalarValue),
            XGaFloat64HigherKVector mv1 => mv1.Divide(scalarValue),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector DivideByENorm()
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENorm(),
            XGaFloat64Vector mv1 => mv1.DivideByENorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByENorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector DivideByENormSquared()
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByENormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByENormSquared(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector DivideByNorm()
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNorm(),
            XGaFloat64Vector mv1 => mv1.DivideByNorm(),
            XGaFloat64Bivector mv1 => mv1.DivideByNorm(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNorm(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector DivideByNormSquared()
    {
        return this switch
        {
            XGaFloat64Scalar mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Vector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64Bivector mv1 => mv1.DivideByNormSquared(),
            XGaFloat64HigherKVector mv1 => mv1.DivideByNormSquared(),
            _ => throw new InvalidOperationException()
        };
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector EInverse()
    {
        return this switch
        {
            XGaFloat64Scalar s => s.EInverse(),
            XGaFloat64Vector v => v.EInverse(),
            XGaFloat64Bivector bv => bv.EInverse(),
            XGaFloat64HigherKVector kv => kv.EInverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Inverse()
    {
        return this switch
        {
            XGaFloat64Scalar s => s.Inverse(),
            XGaFloat64Vector v => v.Inverse(),
            XGaFloat64Bivector bv => bv.Inverse(),
            XGaFloat64HigherKVector kv => kv.Inverse(),
            _ => throw new InvalidOperationException()
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector PseudoInverse()
    {
        return this switch
        {
            XGaFloat64Scalar s => s.PseudoInverse(),
            XGaFloat64Vector v => v.PseudoInverse(),
            XGaFloat64Bivector bv => bv.PseudoInverse(),
            XGaFloat64HigherKVector kv => kv.PseudoInverse(),
            _ => throw new InvalidOperationException()
        };
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector EDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector Dual(XGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector EUnDual(XGaFloat64KVector blade)
    {
        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalar(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64KVector UnDual(XGaFloat64KVector blade)
    {
        return Lcp(blade);
    }
    
    
}