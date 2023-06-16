using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public class LinearAlgebraProcessor<T> : 
        ScalarProcessorContainer<T>,
        ILinearAlgebraProcessor<T>
    {
        internal LinearAlgebraProcessor(IScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor)
        {
        }
    }
}