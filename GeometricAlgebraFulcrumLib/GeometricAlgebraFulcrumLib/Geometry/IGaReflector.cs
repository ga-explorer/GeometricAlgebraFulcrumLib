using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaReflector<T> : 
        IGaOutermorphism<T>
    {
        IGasKVector<T> BladeStorage { get; }

        T BladeNormSquared { get; }
    }
}