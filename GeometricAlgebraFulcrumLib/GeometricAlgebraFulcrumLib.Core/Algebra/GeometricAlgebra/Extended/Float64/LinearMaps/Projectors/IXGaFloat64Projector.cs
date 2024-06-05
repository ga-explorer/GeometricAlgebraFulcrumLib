using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;
using GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.Multivectors;

namespace GeometricAlgebraFulcrumLib.Core.Algebra.GeometricAlgebra.Extended.Float64.LinearMaps.Projectors;

public interface IXGaFloat64Projector : 
    IXGaFloat64Outermorphism
{
    XGaFloat64KVector Blade { get; }
}