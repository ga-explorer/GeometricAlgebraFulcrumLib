using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.Processors.LinearAlgebra
{
    public interface ILinearAlgebraProcessor<T>
        : IScalarProcessor<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }

        //T LinDotProduct(ILinVectorStorage<T> v1, ILinVectorStorage<T> v2);
    }
}