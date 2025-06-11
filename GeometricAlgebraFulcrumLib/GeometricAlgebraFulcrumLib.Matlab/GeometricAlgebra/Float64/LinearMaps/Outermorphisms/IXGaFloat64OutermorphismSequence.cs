using System.Collections.Generic;

namespace GeometricAlgebraFulcrumLib.Matlab.GeometricAlgebra.Float64.LinearMaps.Outermorphisms;

public interface IXGaFloat64OutermorphismSequence
{
    IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms();
}