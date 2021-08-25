using GeometricAlgebraFulcrumLib.Processing.Scalars;

namespace GeometricAlgebraFulcrumLib.Processing.ScalarsGrids
{
    public interface IGaScalarsListProcessor<T> : 
        IGaScalarProcessor<T>
    {
        IGaScalarProcessor<T> ScalarProcessor { get; }
    }
}