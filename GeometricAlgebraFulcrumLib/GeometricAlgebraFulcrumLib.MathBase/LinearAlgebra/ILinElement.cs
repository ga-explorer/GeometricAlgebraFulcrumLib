using GeometricAlgebraFulcrumLib.MathBase.BasicMath;
using GeometricAlgebraFulcrumLib.MathBase.BasicMath.Scalars;

namespace GeometricAlgebraFulcrumLib.MathBase.LinearAlgebra
{
    public interface ILinElement :
        IGeometricElement
    {

    }

    public interface ILinElement<T> :
        IScalarAlgebraElement<T>,
        ILinElement
    {

    }
}