using GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage.GeometricAlgebra;

namespace GeometricAlgebraFulcrumLib.Geometry.Subspaces
{
    public interface IGeoReflector<T> : 
        IOutermorphism<T>
    {
        KVectorStorage<T> BladeStorage { get; }

        T BladeNormSquared { get; }
    }
}