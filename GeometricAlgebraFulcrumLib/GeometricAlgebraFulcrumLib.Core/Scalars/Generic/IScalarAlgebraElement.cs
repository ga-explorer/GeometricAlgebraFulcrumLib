namespace GeometricAlgebraFulcrumLib.Core.Scalars.Generic;

public interface IScalarAlgebraElement<T>
{
    IScalarProcessor<T> ScalarProcessor { get; }
}