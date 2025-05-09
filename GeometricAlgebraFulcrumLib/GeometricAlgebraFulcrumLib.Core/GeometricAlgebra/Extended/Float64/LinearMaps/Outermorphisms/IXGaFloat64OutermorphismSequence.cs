namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Extended.Float64.LinearMaps.Outermorphisms;

public interface IXGaFloat64OutermorphismSequence
{
    IEnumerable<IXGaFloat64Outermorphism> GetLeafOutermorphisms();
}