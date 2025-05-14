namespace GeometricAlgebraFulcrumLib.Core.GeometricAlgebra.Generic.LinearMaps.Outermorphisms;

public interface IXGaOutermorphismSequence<T>
{
    IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms();
}