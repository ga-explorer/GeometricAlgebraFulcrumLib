namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra.Float64.Vectors;

public interface ILinFloat64Vector :
    IFloat64LinearAlgebraElement
{
    double this[int i] { get; }
}