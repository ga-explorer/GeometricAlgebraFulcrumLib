namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public interface IScalar<T> : 
    IScalarAlgebraElement<T>
{
    T ScalarValue { get; }

    Scalar<T> ToScalar();
}