namespace GeometricAlgebraFulcrumLib.Algebra.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

public interface IXGaOutermorphismSequence<T>
{
    IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms();
}