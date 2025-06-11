using GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Basis;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.Multivectors;

public sealed partial class XGaFloat64UniformMultivector
{
    
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1)
    {
        return v1;
    }

    
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1)
    {
        return v1.Negative();
    }


    
    public static XGaFloat64UniformMultivector operator +(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Add(v2);
    }

    
    public static XGaFloat64UniformMultivector operator -(XGaFloat64UniformMultivector v1, XGaFloat64Multivector v2)
    {
        return (XGaFloat64UniformMultivector)v1.Subtract(v2);
    }


    
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(v2);
    }

    
    public static XGaFloat64UniformMultivector operator *(double v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1);
    }

    
    public static XGaFloat64UniformMultivector operator *(XGaFloat64UniformMultivector v1, XGaFloat64Scalar v2)
    {
        return v1.Times(v2.ScalarValue);
    }

    
    public static XGaFloat64UniformMultivector operator *(XGaFloat64Scalar v1, XGaFloat64UniformMultivector v2)
    {
        return v2.Times(v1.ScalarValue);
    }

    
    public static XGaFloat64UniformMultivector operator /(XGaFloat64UniformMultivector v1, double v2)
    {
        return v1.Times(1d / v2);
    }



    
    public override XGaFloat64Multivector Add(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .AddMultivector(mv2)
            .GetUniformMultivector();
    }
        
    
    public override XGaFloat64Multivector Subtract(XGaFloat64Multivector mv2)
    {
        return Processor
            .CreateMultivectorComposer()
            .SetMultivector(this)
            .SubtractMultivector(mv2)
            .GetUniformMultivector();
    }

    
    public new XGaFloat64UniformMultivector Times(double scalar)
    {
        return MapScalars(s => s * scalar);
    }

    
    public new XGaFloat64UniformMultivector Divide(double scalar)
    {
        return MapScalars(s => s / scalar);
    }
        
    
    public new XGaFloat64UniformMultivector DivideByENorm()
    {
        return Divide(ENorm());
    }
        
    
    public new XGaFloat64UniformMultivector DivideByENormSquared()
    {
        return Divide(ENormSquared());
    }
        
    
    public new XGaFloat64UniformMultivector DivideByNorm()
    {
        return Divide(Norm());
    }
        
    
    public new XGaFloat64UniformMultivector DivideByNormSquared()
    {
        return Divide(NormSquared());
    }

    
    
    public new XGaFloat64UniformMultivector Negative()
    {
        return MapScalars(s => -s);
    }

    
    public new XGaFloat64UniformMultivector Reverse()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.ReverseIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    
    public new XGaFloat64UniformMultivector GradeInvolution()
    {
        return MapScalars((basis, scalar) =>
            basis.Count.GradeInvolutionIsNegativeOfGrade()
                ? -scalar
                : scalar
        );
    }

    
    public new XGaFloat64UniformMultivector CliffordConjugate()
    {
        return MapScalars((basis, scalar) =>
            basis.CliffordConjugateSignOfBasisBladeId().IsNegative
                ? -scalar
                : scalar
        );
    }

    
    public new XGaFloat64UniformMultivector Conjugate()
    {
        return MapScalars((basis, scalar) =>
            Metric.HermitianConjugateSign(basis) * scalar
        );
    }
        
    
    public new XGaFloat64UniformMultivector EInverse()
    {
        return Reverse().Divide(
            ENormSquared()
        );
    }

    
    public new XGaFloat64UniformMultivector Inverse()
    {
        return Reverse().Divide(
            NormSquared()
        );
    }
        
    
    public new XGaFloat64UniformMultivector PseudoInverse()
    {
        var kVectorConjugate = Conjugate();

        return kVectorConjugate.Divide(
            kVectorConjugate.Sp(this).ScalarValue
        );
    }
        
    
    public new XGaFloat64Multivector EDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarEInverse(vSpaceDimensions);

        return ELcp(blade);
    }
        
    
    public new XGaFloat64Multivector EDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.EInverse());
    }

    
    public new XGaFloat64Multivector Dual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarInverse(vSpaceDimensions);

        return Lcp(blade);
    }

    
    public new XGaFloat64Multivector Dual(XGaFloat64KVector blade)
    {
        return Lcp(blade.Inverse());
    }

    
    public new XGaFloat64Multivector EUnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        return ELcp(blade);
    }

    
    public new XGaFloat64Multivector EUnDual(XGaFloat64KVector blade)
    {
        return ELcp(blade.Reverse());
    }

    
    public new XGaFloat64Multivector UnDual(int vSpaceDimensions)
    {
        var blade =
            Processor.PseudoScalarReverse(vSpaceDimensions);

        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade);
    }

    
    public new XGaFloat64Multivector UnDual(XGaFloat64KVector blade)
    {
        //TODO: Should this be: 'return mv.Lcp(blade.Conjugate());'?
        return Lcp(blade.Reverse());
    }

}