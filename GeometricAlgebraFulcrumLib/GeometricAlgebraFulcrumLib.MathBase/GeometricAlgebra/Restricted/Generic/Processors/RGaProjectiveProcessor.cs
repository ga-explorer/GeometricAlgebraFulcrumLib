using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.Processors
{
    public class RGaProjectiveProcessor<T> :
        RGaProcessor<T>
    {
        internal RGaProjectiveProcessor(IScalarProcessor<T> scalarProcessor)
            : base(scalarProcessor, 0, 1)
        {
        }
    }
}