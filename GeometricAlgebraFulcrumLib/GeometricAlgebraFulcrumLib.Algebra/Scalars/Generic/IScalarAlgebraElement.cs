namespace GeometricAlgebraFulcrumLib.Algebra.Scalars.Generic;

public interface IScalarAlgebraElement<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }
}