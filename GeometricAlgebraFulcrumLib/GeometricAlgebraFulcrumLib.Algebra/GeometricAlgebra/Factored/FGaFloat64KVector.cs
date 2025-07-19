namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Factored;

public abstract class FGaFloat64KVector :
    FGaFloat64Multivector
{
    public abstract int Grade { get; }


    protected FGaFloat64KVector(FGaFloat64Processor processor)
        : base(processor)
    {
    }
}