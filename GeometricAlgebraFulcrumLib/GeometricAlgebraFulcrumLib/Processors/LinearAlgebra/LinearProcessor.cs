using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public class LinearProcessor<T> : 
        ScalarProcessorContainer<T>,
        ILinearProcessor<T>
    {
        internal LinearProcessor(IScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor)
        {
        }
    }
}