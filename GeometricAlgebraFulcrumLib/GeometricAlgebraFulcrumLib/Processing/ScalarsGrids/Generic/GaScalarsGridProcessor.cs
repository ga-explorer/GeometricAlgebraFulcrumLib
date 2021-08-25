using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids.Generic
{
    public class GaScalarsGridProcessor<T> : 
        GaScalarsListProcessor<T>,
        IGaScalarsGridProcessor<T>
    {
        internal GaScalarsGridProcessor(IGaScalarProcessor<T> scalarProcessor) 
            : base(scalarProcessor)
        {
        }
    }
}