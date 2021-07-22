using GeometricAlgebraFulcrumLib.Algebra.Outermorphisms;
using GeometricAlgebraFulcrumLib.Storage;

namespace GeometricAlgebraFulcrumLib.Geometry
{
    public interface IGaProjector<T> : 
        IGaOutermorphism<T>
    {
        IGasKVector<T> UnitBladeStorage { get; }
    }
}