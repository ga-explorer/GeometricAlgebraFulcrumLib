namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public interface IScalarAlgebraElement<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }
}