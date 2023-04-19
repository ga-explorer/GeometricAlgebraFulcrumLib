namespace GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars
{
    public interface IScalarAlgebraElement<T>
    {
        IScalarProcessor<T> ScalarProcessor { get; }
    }
}