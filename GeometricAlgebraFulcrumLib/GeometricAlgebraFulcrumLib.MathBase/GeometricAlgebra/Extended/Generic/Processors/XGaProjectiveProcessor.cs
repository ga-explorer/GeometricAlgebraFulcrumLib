using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.Processors
{
    public class XGaProjectiveProcessor<T> :
        XGaProcessor<T>
    {
        internal XGaProjectiveProcessor(IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor, 0, 1)
        {
        }
    }
}