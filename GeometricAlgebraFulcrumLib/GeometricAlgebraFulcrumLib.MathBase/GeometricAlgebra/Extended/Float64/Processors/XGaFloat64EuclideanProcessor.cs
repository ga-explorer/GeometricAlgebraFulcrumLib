namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors;

public class XGaFloat64EuclideanProcessor :
    XGaFloat64Processor
{
    public static XGaFloat64EuclideanProcessor Instance { get; }
        = new XGaFloat64EuclideanProcessor();


    private XGaFloat64EuclideanProcessor()
        : base(0, 0)
    {
    }
}