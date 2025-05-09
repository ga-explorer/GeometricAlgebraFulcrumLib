namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public interface IRGaOutermorphismSequence<T>
{
    IEnumerable<IRGaOutermorphism<T>> GetLeafOutermorphisms();
}