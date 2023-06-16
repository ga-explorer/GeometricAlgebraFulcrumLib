using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.ScalarAlgebra;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra
{
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
}