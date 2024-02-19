namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Extended.Generic.LinearMaps.Outermorphisms;

public interface IXGaOutermorphismSequence<T>
{
    IEnumerable<IXGaOutermorphism<T>> GetLeafOutermorphisms();
}