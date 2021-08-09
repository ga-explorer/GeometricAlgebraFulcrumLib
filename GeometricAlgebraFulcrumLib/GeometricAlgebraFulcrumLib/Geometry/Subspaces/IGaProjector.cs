using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGaProjector<T> : 
        IGaOutermorphism<T>
    {
        IGaStorageKVector<T> UnitBladeStorage { get; }
    }
}