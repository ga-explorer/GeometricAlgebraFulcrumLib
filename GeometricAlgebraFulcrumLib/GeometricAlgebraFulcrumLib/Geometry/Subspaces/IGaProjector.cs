using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGaProjector<T> : 
        IGaOutermorphism<T>
    {
        IGaKVectorStorage<T> UnitBladeStorage { get; }
    }
}