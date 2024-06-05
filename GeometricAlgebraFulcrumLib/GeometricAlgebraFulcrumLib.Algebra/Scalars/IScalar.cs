namespace GeometricAlgebraFulcrumLib.Algebra.Scalars;

public interface IScalar<T> : 
    IScalarAlgebraElement<T>
{
    T ScalarValue { get; }

    Scalar<T> ToScalar();
}