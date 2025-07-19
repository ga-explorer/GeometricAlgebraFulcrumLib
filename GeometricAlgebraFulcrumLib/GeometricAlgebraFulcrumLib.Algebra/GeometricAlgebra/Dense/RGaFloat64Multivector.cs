namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public abstract class RGaFloat64Multivector
{
    public RGaFloat64Processor Processor { get; }


    protected RGaFloat64Multivector(RGaFloat64Processor processor)
    {
        Processor = processor;
    }
}