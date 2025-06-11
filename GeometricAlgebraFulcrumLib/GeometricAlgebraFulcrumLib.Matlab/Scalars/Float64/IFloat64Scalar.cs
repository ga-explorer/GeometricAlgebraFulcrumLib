namespace GeometricAlgebraFulcrumLib.Matlab.Scalars.Float64;

public interface IFloat64Scalar :
    IFloat64ScalarAlgebraElement
{
    double ScalarValue { get; }

    double ToScalar();
}