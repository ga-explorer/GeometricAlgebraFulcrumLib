using GeometricAlgebraFulcrumLib.Processors.ScalarAlgebra;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Processors.GeometricAlgebra.GuidedBinaryTraversal.Multivectors
{
    public interface IGeoGbtMultivectorStorageStack1<T> : 
        IGeoGbtStack1<T>
    {
        IScalarAlgebraProcessor<T> ScalarProcessor { get; }

        IMultivectorStorage<T> Storage { get; }
    }
}