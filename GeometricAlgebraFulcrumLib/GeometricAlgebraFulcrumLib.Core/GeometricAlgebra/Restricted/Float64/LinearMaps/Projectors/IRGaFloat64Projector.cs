using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Projectors;

public interface IRGaFloat64Projector : 
    IRGaFloat64Outermorphism
{
    RGaFloat64KVector Blade { get; }
}