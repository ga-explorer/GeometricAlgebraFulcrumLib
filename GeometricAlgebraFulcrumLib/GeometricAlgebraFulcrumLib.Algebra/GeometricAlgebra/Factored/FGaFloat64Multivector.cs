namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored;

public abstract class FGaFloat64Multivector
{
    public FGaFloat64Processor Processor { get; }


    protected FGaFloat64Multivector(FGaFloat64Processor processor)
    {
        Processor = processor;
    }
}