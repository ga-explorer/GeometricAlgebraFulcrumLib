namespace GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra
{
    public interface IScalarAlgebraElement<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }
    }
}