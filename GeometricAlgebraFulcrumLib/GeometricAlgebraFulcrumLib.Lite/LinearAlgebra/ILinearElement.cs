using GeometricAlgebraFulcrumLib.Lite.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.Lite.LinearAlgebra;

public interface ILinearElement :
    IGeometricElement
{
    int VSpaceDimensions { get; }
}

public interface ILinearElement<T> :
    IScalarAlgebraElement<T>,
    ILinearElement
{

}