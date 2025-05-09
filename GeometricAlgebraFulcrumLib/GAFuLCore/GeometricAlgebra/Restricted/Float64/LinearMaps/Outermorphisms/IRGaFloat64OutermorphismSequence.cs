using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public interface IRGaFloat64OutermorphismSequence
{
    IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms();
}