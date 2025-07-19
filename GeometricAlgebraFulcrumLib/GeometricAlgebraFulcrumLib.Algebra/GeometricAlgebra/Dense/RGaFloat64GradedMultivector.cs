namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public sealed class RGaFloat64GradedMultivector :
    RGaFloat64Multivector 
{
    public RGaFloat64Scalar Scalar { get; }

    public RGaFloat64Vector Vector { get; }

    public RGaFloat64Bivector Bivector { get; }

    public RGaFloat64Trivector Trivector { get; }


    public RGaFloat64GradedMultivector(RGaFloat64Processor processor)
        : base(processor)
    {
        
    }
}