namespace GeometricAlgebraFulcrumLib.Core.Algebra.LinearAlgebra;

public interface ILinearAlgebraElement :
    IAlgebraicElement
{
    int VSpaceDimensions { get; }
}