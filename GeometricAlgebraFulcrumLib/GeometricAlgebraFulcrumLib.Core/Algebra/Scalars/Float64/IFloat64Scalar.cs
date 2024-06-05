namespace GeometricAlgebraFulcrumLib.Core.Algebra.Scalars.Float64;

public interface IFloat64Scalar :
    IFloat64ScalarAlgebraElement
{
    double ScalarValue { get; }

    Float64Scalar ToScalar();
}