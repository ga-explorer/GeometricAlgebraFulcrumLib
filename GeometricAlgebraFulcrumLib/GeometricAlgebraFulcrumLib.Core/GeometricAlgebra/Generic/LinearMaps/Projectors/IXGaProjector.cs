using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Projectors;

public interface IXGaProjector<T> : 
    IXGaOutermorphism<T>
{
    XGaKVector<T> Blade { get; }
}