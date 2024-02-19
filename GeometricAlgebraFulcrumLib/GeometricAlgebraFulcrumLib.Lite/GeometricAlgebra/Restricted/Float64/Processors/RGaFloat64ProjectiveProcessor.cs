namespace GeometricAlgebraFulcrumLib.Lite.GeometricAlgebra.Restricted.Float64.Processors;

public class RGaFloat64ProjectiveProcessor :
    RGaFloat64Processor
{
    public static RGaFloat64ProjectiveProcessor Instance { get; }
        = new RGaFloat64ProjectiveProcessor();


    private RGaFloat64ProjectiveProcessor()
        : base(0, 1)
    {
    }
}