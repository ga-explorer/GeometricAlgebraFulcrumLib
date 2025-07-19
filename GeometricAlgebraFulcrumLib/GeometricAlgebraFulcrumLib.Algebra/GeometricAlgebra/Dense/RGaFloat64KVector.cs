namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Dense;

public abstract class RGaFloat64KVector :
    RGaFloat64Multivector
{
    public abstract int Grade { get; }


    protected RGaFloat64KVector(RGaFloat64Processor processor)
        : base(processor)
    {
    }
}