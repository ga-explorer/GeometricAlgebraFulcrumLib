namespace GeometricAlgebraFulcrumLib.MathBase.GeometricAlgebra.Restricted.Generic.LinearMaps.Outermorphisms;

public interface IRGaOutermorphismSequence<T>
{
    IEnumerable<IRGaOutermorphism<T>> GetLeafOutermorphisms();
}