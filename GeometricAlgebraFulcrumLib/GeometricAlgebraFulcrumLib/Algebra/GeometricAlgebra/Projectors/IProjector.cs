using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Projectors
{
    public interface IProjector<T> : 
        IOutermorphism<T>
    {
        KVectorStorage<T> Blade { get; }
    }
}