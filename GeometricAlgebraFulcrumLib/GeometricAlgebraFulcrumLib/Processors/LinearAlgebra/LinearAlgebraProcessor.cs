using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public class LinearAlgebraProcessor<T> : 
        ScalarAlgebraProcessorContainer<T>,
        ILinearAlgebraProcessor<T>
    {
        internal LinearAlgebraProcessor(IScalarAlgebraProcessor<T> scalarProcessor) 
            : base(scalarProcessor)
        {
        }
    }
}