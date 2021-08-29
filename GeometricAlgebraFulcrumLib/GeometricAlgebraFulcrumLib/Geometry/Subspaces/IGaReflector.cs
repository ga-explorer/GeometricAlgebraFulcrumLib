using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.Multivectors;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGaReflector<T> : 
        IGaOutermorphism<T>
    {
        IGaKVectorStorage<T> BladeStorage { get; }

        T BladeNormSquared { get; }
    }
}