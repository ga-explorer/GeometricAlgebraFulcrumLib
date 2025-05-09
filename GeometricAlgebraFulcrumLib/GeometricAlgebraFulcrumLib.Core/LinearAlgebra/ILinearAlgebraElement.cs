namespace GeometricAlgebraFulcrumLib.Core.LinearAlgebra;

public interface ILinearAlgebraElement :
    IAlgebraicElement
{
    int VSpaceDimensions { get; }
}