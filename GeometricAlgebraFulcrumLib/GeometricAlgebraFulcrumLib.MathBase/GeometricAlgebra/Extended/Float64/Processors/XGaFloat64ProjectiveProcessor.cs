namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Float64.Processors
{
    public class XGaFloat64ProjectiveProcessor :
        XGaFloat64Processor
    {
        public static XGaFloat64ProjectiveProcessor Instance { get; }
            = new XGaFloat64ProjectiveProcessor();


        private XGaFloat64ProjectiveProcessor()
            : base(0, 1)
        {
        }
    }
}