namespace GeometricAlgebraFulcrumLib.Algebra.LinearAlgebra;

public interface ILinearAlgebraElement :
    IAlgebraicElement
{
    int VSpaceDimensions { get; }
}