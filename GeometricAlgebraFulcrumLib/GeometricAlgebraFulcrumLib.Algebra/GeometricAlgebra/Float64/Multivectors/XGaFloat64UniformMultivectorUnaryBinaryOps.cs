using System.Runtime.CompilerServices;
using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64UniformMultivector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1)
    {
        return v1;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1)
    {
        return v1.Negative();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Add(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Subtract(v2);
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(v2);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(double v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, XGaFloat64Scalar v2)
    {
        return v1.Times(v2.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator *(XGaFloat64Scalar v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1.ScalarValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static XGaFloat64UniformMultivector operator /(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(1d / v2);
    }



    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Times(double scalar)
    {
        return MapScalars(s => s * scalar);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Divide(double scalar)
    {
        return MapScalars(s => s / scalar);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector DivideByENorm()
    {
        return Divide(ENorm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector DivideByENormSquared()
    {
        return Divide(ENormSquared().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector DivideByNorm()
    {
        return Divide(Norm().ScalarValue);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector DivideByNormSquared()
    {
        return Divide(NormSquared().ScalarValue);
    }

    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Negative()
    {
        return MapScalars(s => -s);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.ReverseIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? -scalar
                : scalar
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Conjugate()
    {
        return MapScalars((basis, scalar) =>
            Metric.HermitianConjugateSign(basis) * scalar
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector EInverse()
    {
        return Reverse().Divide(
            ENormSquared().ScalarValue
        );
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector Inverse()
    {
        return Reverse().Divide(
            NormSquared().ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64UniformMultivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector Dual(XGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector EUnDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.Reverse());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override XGaFloat64Multivector UnDual(XGaFloat64KVector blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }

}