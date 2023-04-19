using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

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