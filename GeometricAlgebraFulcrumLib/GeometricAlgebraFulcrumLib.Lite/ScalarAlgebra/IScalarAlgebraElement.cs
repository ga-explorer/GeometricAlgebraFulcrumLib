namespace GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

public interface IScalarAlgebraElement<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }
}