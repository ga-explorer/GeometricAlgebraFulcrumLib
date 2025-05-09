namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Float64.LinearMaps.Outermorphisms;

public interface IRGaFloat64OutermorphismSequence
{
    IEnumerable<IRGaFloat64Outermorphism> GetLeafOutermorphisms();
}