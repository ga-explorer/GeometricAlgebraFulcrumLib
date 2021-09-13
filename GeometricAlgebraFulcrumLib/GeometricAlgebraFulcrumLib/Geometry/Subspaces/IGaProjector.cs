using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGeoProjector<T> : 
        IOutermorphism<T>
    {
        KVectorStorage<T> UnitBladeStorage { get; }
    }
}