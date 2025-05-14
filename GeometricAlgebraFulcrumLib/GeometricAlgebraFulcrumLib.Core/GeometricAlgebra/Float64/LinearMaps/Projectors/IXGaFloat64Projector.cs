using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Float64.LinearMaps.Projectors;

public interface IXGaFloat64Projector : 
    IXGaFloat64Outermorphism
{
    XGaFloat64KVector Blade { get; }
}