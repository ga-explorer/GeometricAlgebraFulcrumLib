namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.Processors;

public class RGaFloat64EuclideanProcessor :
    RGaFloat64Processor
{
    public static RGaFloat64EuclideanProcessor Instance { get; }
        = new RGaFloat64EuclideanProcessor();


    private RGaFloat64EuclideanProcessor()
        : base(0, 0)
    {
    }
}